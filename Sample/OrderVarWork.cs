using Greatbone.Core;

namespace Greatbone.Sample
{
    ///
    ///
    public abstract class OrderVarWork : Work
    {
        public OrderVarWork(WorkContext wc) : base(wc)
        {
        }

        public void my(ActionContext ac)
        {

        }

        public void ask(ActionContext ac)
        {
            string userid = ac[0];
            int orderid = ac[this];
            string reason = null;

            using (var dc = Service.NewDbContext())
            {
                dc.Sql("UPDATE orders SET reason = @1, ").setstate()._(" WHERE id = @2 AND userid = @3 AND ").statecond();
                if (dc.Query(p => p.Set(reason).Set(orderid).Set(userid)))
                {
                    var order = dc.ToArray<Order>();
                }
                else
                {
                }
            }
        }

        [User]
        public void @default(ActionContext ac)
        {
            string shopid = ac[0];
            int id = ac[this];

            using (var dc = Service.NewDbContext())
            {
                dc.Sql("SELECT ").columnlst(Order.Empty)._("FROM orders WHERE id = @1 AND shopid = @2");
                if (dc.Query(p => p.Set(id).Set(shopid)))
                {
                    var order = dc.ToArray<Order>();
                }
                else
                {
                }
            }
        }

        [Ui(Label = "取消")]
        public void cannel(ActionContext ac)
        {
            string shopid = ac[0];
            int orderid = ac[this];

            using (var dc = ac.NewDbContext())
            {
                dc.Sql("SELECT ").columnlst(Order.Empty)._("FROM orders WHERE id = @1 AND shopid = @2");
                if (dc.Query(p => p.Set(orderid).Set(shopid)))
                {
                    var order = dc.ToArray<Order>();
                }
                else
                {
                }
            }
        }

        [Ui(Label = "已备货")]
        public void fix(ActionContext ac)
        {
            string shopid = ac[0];
            int id = ac[this];

            using (var dc = ac.NewDbContext())
            {
                dc.Sql("UPDATE orders SET ").setstate()._(" WHERE id = @1 AND shopid = @2 AND ").statecond();
                if (dc.Query(p => p.Set(id).Set(shopid)))
                {
                    var order = dc.ToArray<Order>();
                }
                else
                {
                }
            }
        }

        public void close(ActionContext ac)
        {
        }


        [User]
        [Ui]
        public void exam(ActionContext ac)
        {

        }
    }

    public class UserOrderVarWork : OrderVarWork
    {
        public UserOrderVarWork(WorkContext wc) : base(wc)
        {
        }
    }

    public class ShopOrderVarWork : OrderVarWork
    {
        public ShopOrderVarWork(WorkContext wc) : base(wc)
        {
        }
    }

    public class ShopUnpaidOrderVarWork : ShopOrderVarWork
    {
        public ShopUnpaidOrderVarWork(WorkContext wc) : base(wc)
        {
        }
    }

    public class ShopPaidOrderVarWork : ShopOrderVarWork
    {
        public ShopPaidOrderVarWork(WorkContext wc) : base(wc)
        {
        }
    }

    public class ShopLockedOrderVarWork : ShopOrderVarWork
    {
        public ShopLockedOrderVarWork(WorkContext wc) : base(wc)
        {
        }
    }

    public class ShopClosedOrderVarWork : ShopOrderVarWork
    {
        public ShopClosedOrderVarWork(WorkContext wc) : base(wc)
        {
        }
    }

    public class ShopCancelledOrderVarWork : ShopOrderVarWork
    {
        public ShopCancelledOrderVarWork(WorkContext wc) : base(wc)
        {
        }
    }

}