﻿using System;
using System.Collections.Generic;
using Greatbone.Core;

namespace Greatbone.Sample
{
    /// 
    /// An order processing workflow.
    ///
    public class Order : IData, IStatable
    {
        // state
        public const int
            INITIAL = 0,
            PAID = 1,
            ASKED = 2,
            FIXED = 4,
            CLOSED = 4,
            CANCELLED = 8;

        // status
        static readonly Map<short> STATUS = new Map<short>
        {
            [0] = null,
            [1] = "已付款",
            [2] = "已锁定",
            [3] = "已结束",
            [7] = "已取消",
        };


        public static readonly Order Empty = new Order();

        internal int id;

        internal string shopid;

        internal string shop; // shop name

        internal string shopwx; // shop weixin openid

        internal string buyer; // buyer nickname or name

        internal string buyerwx; // buyer weixin openid

        internal DateTime created; // time created

        internal string pend; // reason

        internal DateTime @fixed; // time fixed

        internal DateTime closed; // time closed

        List<OrderLine> lines;

        decimal total;

        internal string payid; // payment id

        internal bool deliver;

        internal DateTime delivered;

        internal int state;
        internal short status;

        public void ReadData(IDataInput i, int proj = 0)
        {
            i.Get(nameof(id), ref id);

            i.Get(nameof(shopid), ref shopid);
            i.Get(nameof(shop), ref shop);
            i.Get(nameof(shopwx), ref shopwx);

            i.Get(nameof(buyer), ref buyer);
            i.Get(nameof(buyerwx), ref buyerwx);

            i.Get(nameof(created), ref created);
            if (proj.Detail())
            {
                i.Get(nameof(lines), ref lines);
            }
            i.Get(nameof(total), ref total);

            i.Get(nameof(deliver), ref deliver);
            i.Get(nameof(delivered), ref delivered);
            i.Get(nameof(state), ref state);
            i.Get(nameof(status), ref status);
        }

        public void WriteData<R>(IDataOutput<R> o, int proj = 0) where R : IDataOutput<R>
        {
            o.Put(nameof(id), id);

            o.Put(nameof(shopid), shopid);
            o.Put(nameof(shop), shop);
            o.Put(nameof(shopwx), shopwx);

            o.Put(nameof(buyer), buyer);
            o.Put(nameof(buyerwx), buyerwx);

            o.Put(nameof(created), created);
            if (proj.Detail())
            {
                o.Put(nameof(lines), lines);
            }
            o.Put(nameof(total), total);

            o.Put(nameof(deliver), deliver, Opt: b => b ? "派送" : "自提");
            o.Put(nameof(delivered), delivered);
            o.Put(nameof(state), state);
            o.Put(nameof(status), status, Opt: STATUS);
        }

        public int State => state;

        public void add(string item, short qty, decimal price, string note)
        {
            if (lines == null)
            {
                lines = new List<OrderLine>();
            }
            // var orderln = lines.Find(o => o.shopid.Equals(shopid));
            // if (orderln == null)
            // {
            //     orderln = new OrderLine();
            //     Add(order);
            // }
        }

    }
}