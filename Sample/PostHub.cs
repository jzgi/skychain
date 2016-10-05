﻿using Greatbone.Core;
using System.Collections.Generic;

namespace Greatbone.Sample
{
    public class PostHub : WebHub
    {
        public PostHub(WebBuild bld) : base(bld)
        {
            SetVarHub<PostVarHub>(false);
        }

        public void top(WebContext wc)
        {
            int page = 0;
            if (!wc.Get("page", ref page))
            {
                wc.StatusCode = 400; return;
            }

            using (var dc = Service.NewDbContext())
            {
                if (dc.Query(@"SELECT * FROM posts ORDER BY id DESC LIMIT @limit OFFSET @offset", p => p.Put(20).Put(20 * page)))
                {
                    List<Post> lst = dc.ToList<Post>();
                    wc.SendJson(200, jc => jc.Arr(lst));
                }
                else
                {
                    wc.StatusCode = 204; // no content
                }
            }
        }


        public void @new(WebContext wc)
        {
            IToken tok = wc.Token;

            using (var dc = Service.NewDbContext())
            {
                dc.Execute("INSERT INTO posts () VALUES ()", p => p.Put(tok.Key).Put(tok.Name));
            }
        }

        public void remove(WebContext wc)
        {
        }
    }
}