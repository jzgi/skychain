﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Greatbone.Core
{
    ///
    /// A virtual web folder that contains static/dynamic resources.
    ///
    public abstract class Folder : Nodule
    {
        internal static readonly AuthorizeException AuthorizeEx = new AuthorizeException();

        // max nesting levels
        const int MaxNesting = 6;

        // underlying file directory name
        const string _VAR_ = "VAR";

        // state-passing
        readonly FolderContext fc;

        readonly string major;

        readonly short minor;

        // declared actions 
        readonly Roll<ActionInfo> actions;

        // the default action
        readonly ActionInfo @default;

        // the null-key-recovering action
        readonly ActionInfo @null;

        // subfolders, if any
        internal Roll<Folder> subfolders;

        // the variable-key subfolder, if any
        internal Folder varfolder;

        internal Func<IData, string> varkeyer;

        protected Folder(FolderContext fc) : base(fc.Name, null)
        {
            this.fc = fc;
            // separate major and minor name parts
            int dash = Name.IndexOf('-');
            if (dash != -1)
            {
                major = Name.Substring(0, dash);
                minor = Name.Substring(dash + 1).ToShort();
            }
            else
            {
                major = Name;
            }

            // gather actions
            actions = new Roll<ActionInfo>(32);
            Type typ = GetType();
            foreach (MethodInfo mi in typ.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                // verify the return type
                Type ret = mi.ReturnType;
                bool async;
                if (ret == typeof(Task)) async = true;
                else if (ret == typeof(void)) async = false;
                else continue;

                ParameterInfo[] pis = mi.GetParameters();
                ActionInfo ai = null;
                if (pis.Length == 1 && pis[0].ParameterType == typeof(ActionContext))
                {
                    ai = new ActionInfo(this, mi, async, false);
                }
                else if (pis.Length == 2 && pis[0].ParameterType == typeof(ActionContext) && pis[1].ParameterType == typeof(int))
                {
                    ai = new ActionInfo(this, mi, async, true);
                }
                else continue;

                actions.Add(ai);
                if (ai.Name.Equals("default")) { @default = ai; }
                if (ai.Name.Equals("null")) { @null = ai; }
            }

            // to override annotated attributes
            if (fc.Ui != null)
            {
                ui = fc.Ui;
            }
            if (fc.Authorize != null)
            {
                authorize = fc.Authorize;
            }

            // preprocess start-end annotations
            AuthorizeAttribute start = null;
            for (int i = 0; i < actions.Count; i++)
            {
                ActionInfo ai = actions[i];
                AuthorizeAttribute auth = ai.authorize;

                if (start != null)
                {
                    if (auth == null) ai.authorize = start;
                    else auth.Or(start);
                }

                if (auth != null && auth.Start)
                {
                    auth.Start = false;
                    start = auth;
                }
                if (auth != null && auth.End)
                {
                    auth.End = false;
                    start = null;
                }
            }
        }

        ///
        /// Create a subfolder.
        ///
        public F AddSub<F>(string name, UiAttribute ui = null, AuthorizeAttribute auth = null) where F : Folder
        {
            if (Level >= MaxNesting)
            {
                throw new ServiceException("allowed folder nesting " + MaxNesting);
            }

            if (subfolders == null)
            {
                subfolders = new Roll<Folder>(32);
            }
            // create instance by reflection
            Type typ = typeof(F);
            ConstructorInfo ci = typ.GetConstructor(new[] { typeof(FolderContext) });
            if (ci == null)
            {
                throw new ServiceException(typ + " missing FolderContext");
            }
            FolderContext fc = new FolderContext(name)
            {
                Ui = ui,
                Authorize = auth,
                Parent = this,
                Level = Level + 1,
                Directory = (Parent == null) ? name : Path.Combine(Parent.Directory, name),
                Service = Service
            };
            F folder = (F)ci.Invoke(new object[] { fc });
            subfolders.Add(folder);

            return folder;
        }

        ///
        /// Create a variable-key subfolder.
        ///
        public F CreateVar<F>(Func<IData, string> keyer = null, UiAttribute ui = null, AuthorizeAttribute auth = null) where F : Folder, IVar
        {
            if (Level >= MaxNesting)
            {
                throw new ServiceException("allowed folder nesting " + MaxNesting);
            }

            // create instance
            Type typ = typeof(F);
            ConstructorInfo ci = typ.GetConstructor(new[] { typeof(FolderContext) });
            if (ci == null)
            {
                throw new ServiceException(typ + " missing FolderContext");
            }
            FolderContext fc = new FolderContext(_VAR_)
            {
                Ui = ui,
                Authorize = auth,
                Parent = this,
                Level = Level + 1,
                Directory = (Parent == null) ? _VAR_ : Path.Combine(Parent.Directory, _VAR_),
                Service = Service
            };
            F folder = (F)ci.Invoke(new object[] { fc });
            varkeyer = keyer;
            varfolder = folder;

            return folder;
        }

        public string Major => major;

        public short Minor => minor;

        public Roll<ActionInfo> Actions => actions;

        public Roll<Folder> SubFolders => subfolders;

        public Folder VarFolder => varfolder;

        public Func<IData, string> VarKeyer => varkeyer;

        public string Directory => fc.Directory;

        public Folder Parent => fc.Parent;

        public int Level => fc.Level;

        public override Service Service => fc.Service;

        public string GetVarKey(IData token) => varkeyer?.Invoke(token);

        internal void Describe(XmlContent cont)
        {
            cont.ELEM(Name,
            delegate
            {
                for (int i = 0; i < Actions.Count; i++)
                {
                    ActionInfo action = Actions[i];
                    cont.Put(action.Name, "");
                }
            },
            delegate
            {
                if (subfolders != null)
                {
                    for (int i = 0; i < subfolders.Count; i++)
                    {
                        Folder child = subfolders[i];
                        child.Describe(cont);
                    }
                }
                if (varfolder != null)
                {
                    varfolder.Describe(cont);
                }
            });
        }


        // public Roll<WebAction> Actions => actions;

        public ActionInfo GetAction(string method)
        {
            if (string.IsNullOrEmpty(method))
            {
                return @default;
            }
            return actions[method];
        }

        public List<ActionInfo> GetUiActions(ActionContext ac)
        {
            List<ActionInfo> lst = null;
            for (int i = 0; i < actions.Count; i++)
            {
                ActionInfo a = actions[i];
                if (a.HasUi && a.DoAuthorize(ac))
                {
                    if (lst == null) lst = new List<ActionInfo>();
                    lst.Add(a);
                }
            }
            return lst;
        }

        internal Folder ResolveFolder(ref string relative, ActionContext ac, ref bool recover)
        {
            int slash = relative.IndexOf('/');
            if (slash == -1)
            {
                return this;
            }

            // sub folder
            string key = relative.Substring(0, slash);
            relative = relative.Substring(slash + 1); // adjust relative
            Folder subfdr;
            if (subfolders != null && subfolders.TryGet(key, out subfdr)) // chiled
            {
                ac.Chain(key, subfdr);
                return subfdr.ResolveFolder(ref relative, ac, ref recover);
            }
            if (varfolder != null) // variable-key
            {
                if (key.Length == 0 && varkeyer != null) // resolve varkey
                {
                    if (ac.Principal == null) throw AuthorizeEx;
                    if ((key = varkeyer(ac.Principal)) == null)
                    {
                        if (@null != null) { recover = true; }
                        return null;
                    }
                }
                ac.Chain(key, varfolder);
                return varfolder.ResolveFolder(ref relative, ac, ref recover);
            }
            return null;
        }

        internal async Task HandleAsync(string rsc, ActionContext ac)
        {
            ac.Folder = this;

            // access check 
            if (!DoAuthorize(ac)) throw AuthorizeEx;

            // pre-
            FilterAttribute flt = Filter;
            if (flt != null) { if (flt.IsAsync) await flt.BeforeAsync(ac); else flt.Before(ac); }

            int dot = rsc.LastIndexOf('.');
            if (dot != -1) // file
            {
                // try in cache 

                DoFile(rsc, rsc.Substring(dot), ac);
            }
            else // action
            {
                string name = rsc;
                int subscpt = 0;
                int dash = rsc.LastIndexOf('-');
                if (dash != -1)
                {
                    name = rsc.Substring(0, dash);
                    ac.Subscript = subscpt = rsc.Substring(dash + 1).ToInt();
                }
                ActionInfo act = string.IsNullOrEmpty(name) ? @default : GetAction(name);
                if (act == null)
                {
                    ac.Give(404); // not found
                    return;
                }

                ac.Doer = act;

                // access check
                if (!act.DoAuthorize(ac)) throw AuthorizeEx;

                // try in cache

                // action filter before
                FilterAttribute aflt = act.Filter;
                if (aflt != null) { if (aflt.IsAsync) await aflt.BeforeAsync(ac); else aflt.Before(ac); }
                // method invocation
                if (act.IsAsync)
                {
                    await act.DoAsync(ac, subscpt); // invoke action method
                }
                else
                {
                    act.Do(ac, subscpt);
                }
                // action filter after
                if (aflt != null) { if (aflt.IsAsync) await aflt.AfterAsync(ac); else aflt.After(ac); }

                ac.Doer = null;
            }

            // post-
            if (flt != null) { if (flt.IsAsync) await flt.AfterAsync(ac); else flt.After(ac); }

            ac.Folder = null;
        }

        public void DoFile(string filename, ActionContext ac)
        {
            int dot = filename.LastIndexOf('.');
            DoFile(filename, filename.Substring(dot), ac);
        }

        void DoFile(string filename, string ext, ActionContext ac)
        {
            if (filename.StartsWith("$")) // private resource
            {
                ac.Give(403); // forbidden
                return;
            }

            string ctyp;
            if (!StaticContent.TryGetType(ext, out ctyp))
            {
                ac.Give(415); // unsupported media type
                return;
            }

            string path = Path.Combine(Directory, filename);
            if (!File.Exists(path))
            {
                ac.Give(404); // not found
                return;
            }

            DateTime modified = File.GetLastWriteTime(path);
            DateTime? since = ac.HeaderDateTime("If-Modified-Since");
            if (since != null && modified <= since)
            {
                ac.Give(304); // not modified
                return;
            }

            // load file content
            byte[] bytes = File.ReadAllBytes(path);
            StaticContent cont = new StaticContent(true, bytes, bytes.Length)
            {
                Name = filename,
                Type = ctyp,
                Modified = modified
            };
            ac.Give(200, cont, true, 3600 * 12);
        }

        //
        // LOGGING METHODS
        //

        public void TRC(string message, Exception exception = null)
        {
            Service.Log(LogLevel.Trace, 0, message, exception, null);
        }

        public void DBG(string message, Exception exception = null)
        {
            Service.Log(LogLevel.Debug, 0, message, exception, null);
        }

        public void INF(string message, Exception exception = null)
        {
            Service.Log(LogLevel.Information, 0, message, exception, null);
        }

        public void WAR(string message, Exception exception = null)
        {
            Service.Log(LogLevel.Warning, 0, message, exception, null);
        }

        public void ERR(string message, Exception exception = null)
        {
            Service.Log(LogLevel.Error, 0, message, exception, null);
        }
    }
}