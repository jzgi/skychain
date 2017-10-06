using System.Threading.Tasks;
using Greatbone.Core;

namespace Greatbone.Sample
{
    [User]
    public abstract class LessonVarWork : Work
    {
        protected LessonVarWork(WorkContext wc) : base(wc)
        {
        }
    }

    public class AdmLessonVarWork : LessonVarWork
    {
        public AdmLessonVarWork(WorkContext wc) : base(wc)
        {
        }

        [Ui("回复", Mode = UiMode.ButtonShow)]
        public async Task reply(ActionContext ac)
        {
            string shopid = ac[typeof(ShopVarWork)];
            User prin = (User) ac.Principal;
            string wx = ac[this];

            string text = null;
            if (ac.GET)
            {
                ac.GivePane(200, m =>
                {
                    m.FORM_();
                    m.TEXT(nameof(text), text, "发送信息", pattern: "[\\S]*", max: 30, required: true);
                    m._FORM();
                });
            }
            else
            {
                var f = await ac.ReadAsync<Form>();
                text = f[nameof(text)];
                ac.GivePane(200);
            }
        }
    }
}