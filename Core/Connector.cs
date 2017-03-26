﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static Greatbone.Core.EventQueue;
using System.Data;
using static Greatbone.Core.DataInputUtility;

namespace Greatbone.Core
{
    ///
    /// A client of RPC, service and/or event queue.
    ///
    public class Connector : HttpClient, IRollable
    {
        const int AHEAD = 1000 * 12;

        static readonly Uri PollUri = new Uri("*", UriKind.Relative);

        readonly Service service;

        // prepared header value
        readonly string x_event;

        // target serviceid
        readonly string peerid;

        // this field is only accessed by the scheduler
        Task pollTask;

        // point of time to retry, set due to timeout or disconnection
        volatile int retryat;

        internal long evtid;

        public Connector(string raddr) : this(null, null, raddr) { }

        internal Connector(Service service, string peerid, string raddr)
        {
            this.service = service;
            this.peerid = peerid;

            if (service != null) // build lastevent poll condition
            {
                Roll<EventInfo> eis = service.Events;
                if (eis != null)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < eis.Count; i++)
                    {
                        if (i > 0) sb.Append(',');
                        sb.Append(eis[i].Name);
                    }
                    x_event = sb.ToString();
                }
            }

            BaseAddress = new Uri(raddr);
            Timeout = TimeSpan.FromSeconds(5);
        }

        public string Name => peerid;


        public void TryPoll(int ticks)
        {
            if (ticks < retryat)
            {
                return;
            }
            if (pollTask != null && !pollTask.IsCompleted)
            {
                return;
            }

            pollTask = Task.Run(async () =>
            {
                for (;;)
                {
                    HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, PollUri);
                    HttpRequestHeaders reqhs = req.Headers;
                    reqhs.TryAddWithoutValidation("From", service.Id);
                    reqhs.TryAddWithoutValidation(X_EVENT, x_event);
                    reqhs.TryAddWithoutValidation(X_SHARD, service.Shard);

                    HttpResponseMessage rsp = null;
                    try
                    {
                        rsp = await SendAsync(req);
                        if (rsp.StatusCode == HttpStatusCode.NoContent)
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        retryat = Environment.TickCount + 15000;
                        return;
                    }

                    HttpResponseHeaders rsphs = rsp.Headers;
                    byte[] cont = await rsp.Content.ReadAsByteArrayAsync();
                    EventContext ec = new EventContext(this)
                    {
                        id = rsphs.GetValue(X_ID).ToLong(),
                        // time = rsphs.GetValue(X_ARG)
                    };

                    // parse and process one by one
                    long id = 0;
                    string name = rsp.Headers.GetValue(X_EVENT);
                    string arg = rsp.Headers.GetValue(X_ARG);
                    DateTime time;
                    EventInfo ei = null;

                    using (var dc = ec.NewDbContext(IsolationLevel.ReadUncommitted))
                    {
                        if (service.Events.TryGet(name, out ei))
                        {
                            if (ei.IsAsync)
                            {
                                await ei.DoAsync(ec, arg);
                            }
                            else
                            {
                                ei.Do(ec, arg);
                            }
                        }

                        // database last id
                        dc.Execute("UPDATE evtu SET evtid = @1 WHERE peerid = @2", p => p.Set(id).Set(peerid));
                    }
                }
            });
        }

        //
        // RPC
        //

        public async Task<byte[]> GetAsync(ActionContext ac, string uri)
        {
            try
            {
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, uri);
                if (peerid != null && ac != null)
                {
                    if (ac.Token != null)
                    {
                        req.Headers.Add("Authorization", "Bearer " + ac.Token);
                    }
                }
                HttpResponseMessage resp = await SendAsync(req, HttpCompletionOption.ResponseContentRead);
                return await resp.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                retryat = Environment.TickCount + AHEAD;
            }
            return null;
        }

        public async Task<M> GetAsync<M>(ActionContext ac, string uri) where M : class, IDataInput
        {
            try
            {
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, uri);
                if (peerid != null && ac != null)
                {
                    if (ac.Token != null)
                    {
                        req.Headers.Add("Authorization", "Bearer " + ac.Token);
                    }
                }
                HttpResponseMessage rsp = await SendAsync(req, HttpCompletionOption.ResponseContentRead);
                if (rsp.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }
                byte[] bytea = await rsp.Content.ReadAsByteArrayAsync();
                string ctyp = rsp.Content.Headers.GetValue("Content-Type");
                return (M)ParseContent(ctyp, bytea, bytea.Length, typeof(M));
            }
            catch
            {
                retryat = Environment.TickCount + AHEAD;
            }
            return null;
        }

        public async Task<D> GetObjectAsync<D>(ActionContext ac, string uri, int proj = 0) where D : IData, new()
        {
            try
            {
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, uri);
                if (peerid != null && ac != null)
                {
                    if (ac.Token != null)
                    {
                        req.Headers.Add("Authorization", "Bearer " + ac.Token);
                    }
                }
                HttpResponseMessage rsp = await SendAsync(req, HttpCompletionOption.ResponseContentRead);
                if (rsp.StatusCode != HttpStatusCode.OK)
                {
                    return default(D);
                }
                byte[] bytea = await rsp.Content.ReadAsByteArrayAsync();
                string ctyp = rsp.Content.Headers.GetValue("Content-Type");
                IDataInput inp = ParseContent(ctyp, bytea, bytea.Length);
                D obj = new D();
                obj.ReadData(inp, proj);
                return obj;
            }
            catch
            {
                retryat = Environment.TickCount + AHEAD;
            }
            return default(D);
        }

        public async Task<D[]> GetArrayAsync<D>(ActionContext ac, string uri, int proj = 0) where D : IData, new()
        {
            try
            {
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, uri);
                if (peerid != null && ac != null)
                {
                    if (ac.Token != null)
                    {
                        req.Headers.Add("Authorization", "Bearer " + ac.Token);
                    }
                }
                HttpResponseMessage rsp = await SendAsync(req, HttpCompletionOption.ResponseContentRead);
                if (rsp.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }
                byte[] bytea = await rsp.Content.ReadAsByteArrayAsync();
                string ctyp = rsp.Content.Headers.GetValue("Content-Type");
                IDataInput inp = ParseContent(ctyp, bytea, bytea.Length);
                return inp.ToArray<D>(proj);
            }
            catch
            {
                retryat = Environment.TickCount + AHEAD;
            }
            return null;
        }

        public async Task<List<D>> GetListAsync<D>(ActionContext ac, string uri, int proj = 0) where D : IData, new()
        {
            try
            {
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, uri);
                if (peerid != null && ac != null)
                {
                    if (ac.Token != null)
                    {
                        req.Headers.Add("Authorization", "Bearer " + ac.Token);
                    }
                }
                HttpResponseMessage rsp = await SendAsync(req, HttpCompletionOption.ResponseContentRead);
                if (rsp.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }
                byte[] bytea = await rsp.Content.ReadAsByteArrayAsync();
                string ctyp = rsp.Content.Headers.GetValue("Content-Type");
                IDataInput inp = ParseContent(ctyp, bytea, bytea.Length);
                return inp.ToList<D>(proj);
            }
            catch
            {
                retryat = Environment.TickCount + AHEAD;
            }
            return null;
        }

        public async Task<int> PostAsync(ActionContext ac, string uri, IContent content)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            if (peerid != null && ac != null)
            {
                if (ac.Token != null)
                {
                    req.Headers.Add("Authorization", "Bearer " + ac.Token);
                }
            }
            req.Content = (HttpContent)content;
            req.Headers.Add("Content-Type", content.Type);
            req.Headers.Add("Content-Length", content.Size.ToString());

            HttpResponseMessage rsp = await SendAsync(req, HttpCompletionOption.ResponseContentRead);
            return (int)rsp.StatusCode;
        }

        public async Task<M> PostAsync<M>(ActionContext ctx, string uri, IContent content) where M : class, IDataInput
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            if (ctx != null)
            {
                req.Headers.Add("Authorization", "Bearer " + ctx.Token);
            }
            req.Content = (HttpContent)content;
            req.Headers.Add("Content-Type", content.Type);
            req.Headers.Add("Content-Length", content.Size.ToString());

            HttpResponseMessage rsp = await SendAsync(req, HttpCompletionOption.ResponseContentRead);
            string ctyp = rsp.Headers.GetValue("Content-Type");
            if (ctyp == null) return null;

            byte[] bytes = await rsp.Content.ReadAsByteArrayAsync();
            return ParseContent(ctyp, bytes, bytes.Length, typeof(M)) as M;
        }
    }
}