﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Greatbone.Core
{
    ///
    /// <summary>
    /// The web controller pertaining to a virtual directory, that handles request for static and dynamic contents.
    /// </summary>
    ///
    public abstract class WebControl : IKeyed
    {
        // makes state-passing convenient
        internal readonly WebArg arg;

        // declared actions 
        readonly Roll<WebAction> actions;

        // the default action
        readonly WebAction defaction;

        protected WebControl(WebArg arg)
        {
            this.arg = arg;

            // init actions
            actions = new Roll<WebAction>(32);
            Type typ = GetType();
            foreach (MethodInfo mi in typ.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                ParameterInfo[] pis = mi.GetParameters();
                if (pis.Length == 2 && pis[0].ParameterType == typeof(WebContext) && pis[1].ParameterType == typeof(string))
                {
                    WebAction a = new WebAction(this, mi);
                    if (a.Key.Equals("default"))
                    {
                        defaction = a;
                    }
                    actions.Add(a);
                }
            }
        }

        ///
        /// The key by which this sub-controller is added to its parent
        ///
        public string Key => arg.Key;

        public bool Auth => arg.Auth;

        public bool IsMulti => arg.IsMulti;

        public string Folder => arg.Folder;

        public IParent Parent => arg.Parent;

        public WebService Service => arg.Service;


        public Roll<WebAction> Actions => actions;

        public WebAction GetAction(string method)
        {
            if (string.IsNullOrEmpty(method))
            {
                return defaction;
            }
            return actions[method];
        }

        internal bool CheckAuth(WebContext wc)
        {
            if (Auth && wc.Token == null)
            {
                wc.StatusCode = 401; // unauthorized
                wc.Response.Headers.Add("WWW-Authenticate", new StringValues("Bearer"));
                return false;
            }
            return true;
        }

        internal virtual void Handle(string relative, WebContext wc)
        {
            if (!CheckAuth(wc)) return;

            wc.Control = this;
            Do(relative, wc);
            wc.Control = null;
        }

        protected internal virtual void Do(string rsc, WebContext wc)
        {
            int dot = rsc.LastIndexOf('.');
            if (dot != -1) // static
            {
                DoStatic(rsc, rsc.Substring(dot), wc);
            }
            else // dynamic
            {
                string key = rsc;
                string subscpt = null;
                int dash = rsc.LastIndexOf('-');
                if (dash != -1)
                {
                    key = rsc.Substring(0, dash);
                    subscpt = rsc.Substring(dash + 1);
                }
                WebAction a = string.IsNullOrEmpty(key) ? defaction : GetAction(key);
                if (a == null)
                {
                    wc.StatusCode = 404;
                }
                else if (!a.TryDo(wc, subscpt))
                {
                    wc.StatusCode = 403; // forbidden
                }
            }

        }

        void DoStatic(string file, string ext, WebContext wc)
        {
            string ctyp;
            if (!StaticContent.TryGetType(ext, out ctyp))
            {
                wc.StatusCode = 415;  // unsupported media type
                return;
            }

            string path = Path.Combine(Folder, file);
            if (!File.Exists(path))
            {
                wc.StatusCode = 404; // not found
            }

            DateTime modified = File.GetLastWriteTime(path);
            DateTime? since = wc.HeaderDateTime("If-Modified-Since");
            if (since != null && modified <= since) // not modified
            {
                wc.StatusCode = 304;
                return;
            }

            // load file content
            byte[] content = File.ReadAllBytes(path);
            StaticContent sta = new StaticContent
            {
                Key = file.ToLower(),
                Type = ctyp,
                Buffer = content,
                LastModified = modified
            };
            wc.Out(200, sta, true, 5 * 60000);
        }


        public virtual void @default(WebContext wc, string subscpt)
        {
            DoStatic("default.html", ".html", wc);
        }

        //
        // LOGGING METHODS
        //

        public void Trace(string message, Exception exception = null)
        {
            Service.Log(LogLevel.Trace, 0, message, exception, null);
        }

        public void Debug(string message, Exception exception = null)
        {
            Service.Log(LogLevel.Debug, 0, message, exception, null);
        }

        public void Info(string message, Exception exception = null)
        {
            Service.Log(LogLevel.Information, 0, message, exception, null);
        }

        public void Warning(string message, Exception exception = null)
        {
            Service.Log(LogLevel.Warning, 0, message, exception, null);
        }

        public void Error(string message, Exception exception = null)
        {
            Service.Log(LogLevel.Error, 0, message, exception, null);
        }

    }

}