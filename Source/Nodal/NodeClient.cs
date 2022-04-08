using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static FabricQ.Nodal.NodeUtility;
using WebClient = FabricQ.Web.WebClient;

namespace FabricQ.Nodal
{
    /// <summary>
    /// A client connector to a specific remote peer..
    /// </summary>
    public class NodeClient : Web.WebClient, IKeyable<short>, INodeClient
    {
        const int REQUEST_TIMEOUT = 3;

        const short
            NORMAL = 1,
            NETWORK_ERROR = 2,
            DATA_ERROR = 3,
            PEER_ERROR = 4,
            INTERNAL_ERROR = 5;

        public static readonly Map<short, string> Statuses = new Map<short, string>
        {
            {0, null},
            {NORMAL, "normal"},
            {NETWORK_ERROR, "network error"},
            {DATA_ERROR, "data error"},
            {PEER_ERROR, "peer error"},
            {INTERNAL_ERROR, "internal error"},
        };

        // when a fed connector
        readonly Peer peer;

        // acceptable remote addresses
        readonly IPAddress[] addrs;

        Task<(short, JArr)> polltsk;

        // the status showing what is happening 
        short status;

        string err;


        /// <summary>
        /// To construct a federation client. 
        /// </summary>
        internal NodeClient(Peer peer, NodeClientHandler handler = null) : base(peer.domain, handler ?? new NodeClientHandler())
        {
            this.peer = peer;
            try
            {
                addrs = Dns.GetHostAddresses(BaseAddress.Host);
            }
            catch (Exception e)
            {
                // ServerEnviron.WAR(e.Message);
            }
            Timeout = TimeSpan.FromSeconds(REQUEST_TIMEOUT);
        }


        public short Key => peer.id;

        public Peer Info => peer;

        public string Err => err;

        ///
        /// <returns>0) void, 1) remoting, 200) ok, 204) no content, 500) server error</returns>
        /// 
        public short TryReap(out JArr block)
        {
            block = null;
            if (polltsk == null)
            {
                return 0;
            }
            if (!polltsk.IsCompleted)
            {
                return 1;
            }
            var (code, cnt) = polltsk.Result;

            if (code == 500)
            {
                status = PEER_ERROR;
            }
            else if (code == 200 || code == 204)
            {
                status = NORMAL;
            }

            block = cnt;
            return code;
        }

        public bool IsRemoteAddr(IPAddress addr)
        {
            for (int i = 0; i < addrs.Length; i++)
            {
                if (addrs[i].Equals(addr))
                {
                    return true;
                }
            }
            return false;
        }

        internal void SetDataError(string err)
        {
            this.status = DATA_ERROR;
            this.err = err;
        }

        internal void SetDataError(long seq)
        {
            this.status = DATA_ERROR;
            this.err = seq.ToString();
        }

        internal void SetInternalError(string seq)
        {
            status = INTERNAL_ERROR;
            // this.seq = seq;
        }

        public void ScheduleRemotePoll(int blockid)
        {
            if (status != DATA_ERROR)
            {
                // schedule to execute
                if (polltsk == null || polltsk.IsCompleted)
                {
                    // (polltsk = PollAsync(blockid)).Start();
                }
            }
        }

        //
        // RPC
        //

        async Task<(short, JArr)> PollAsync(int blockid)
        {
            try
            {
                // request
                var req = new HttpRequestMessage(HttpMethod.Post, "/onpoll");

                req.Headers.TryAddWithoutValidation(X_FROM, Home.Self.id.ToString());
                req.Headers.TryAddWithoutValidation(X_CRYPTO, peer.id.ToString());
                req.Headers.TryAddWithoutValidation(X_BLOCK_ID, blockid.ToString());

                // response
                var rsp = await SendAsync(req, HttpCompletionOption.ResponseContentRead);
                if (rsp.StatusCode != HttpStatusCode.OK)
                {
                    return ((short) rsp.StatusCode, null);
                }
                var hdrs = rsp.Content.Headers;

                var bytea = await rsp.Content.ReadAsByteArrayAsync();
                var arr = (JArr) new JsonParser(bytea, bytea.Length).Parse();
                return (200, arr);
            }
            catch
            {
                status = NETWORK_ERROR;
                return (0, null);
            }
        }

        const string
            CONTENT_TYPE = "Content-Type",
            CONTENT_LENGTH = "Content-Length",
            AUTHORIZATION = "Authorization";


        /// <summary>
        /// To call an operation which is on the remote peer.
        /// </summary>
        /// <param name="ctxid"></param>
        /// <param name="txtyp"></param>
        /// <param name="op"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        internal async Task<(int status, bool ok)> CallAsync(long ctxid, short txtyp, string op, DynamicContent content)
        {
            try
            {
                // request
                var req = new HttpRequestMessage(HttpMethod.Get, "/oncall");

                req.Headers.TryAddWithoutValidation(X_FROM, Home.Self.id.ToString());
                req.Headers.TryAddWithoutValidation(X_CRYPTO, peer.id.ToString());

                req.Content = content;
                req.Headers.TryAddWithoutValidation(CONTENT_TYPE, content.CType);
                req.Headers.TryAddWithoutValidation(CONTENT_LENGTH, content.Count.ToString());

                // response
                var rsp = await SendAsync(req, HttpCompletionOption.ResponseContentRead);
                if (rsp.StatusCode != HttpStatusCode.OK)
                {
                    return ((short) rsp.StatusCode, false);
                }
                var hdrs = rsp.Content.Headers;

                return (200, false);
            }
            catch
            {
                status = NETWORK_ERROR;
                return (0, false);
            }
        }


        public async Task<(int, NodeClientError)> InviteAsync(Peer peer)
        {
            try
            {
                // request
                var req = new HttpRequestMessage(HttpMethod.Get, "/oninvite");

                req.Headers.TryAddWithoutValidation(X_FROM, Home.Self.id.ToString());
                req.Headers.TryAddWithoutValidation(X_CRYPTO, peer.id.ToString());

                var jc = new JsonContent(true, 1024);
                peer.Write(jc);

                req.Content = jc;
                req.Headers.TryAddWithoutValidation(CONTENT_TYPE, jc.CType);
                req.Headers.TryAddWithoutValidation(CONTENT_LENGTH, jc.Count.ToString());

                // response
                var rsp = await SendAsync(req, HttpCompletionOption.ResponseContentRead);
                if (rsp.StatusCode != HttpStatusCode.OK)
                {
                    return ((int) rsp.StatusCode, 0);
                }
                var hdrs = rsp.Content.Headers;

                return (200, 0);
            }
            catch
            {
                status = NETWORK_ERROR;
                return (0, 0);
            }
        }

        public void ApproveAsk()
        {
            throw new NotImplementedException();
        }

        public void Quit()
        {
            throw new NotImplementedException();
        }

        public void ApproveQuit()
        {
            throw new NotImplementedException();
        }

        public void Transfer()
        {
            throw new NotImplementedException();
        }

        public void Poll()
        {
            throw new NotImplementedException();
        }

        public void Discover()
        {
            throw new NotImplementedException();
        }
    }
}