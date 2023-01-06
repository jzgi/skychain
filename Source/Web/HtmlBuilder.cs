using System;
using static ChainFx.Web.ToolAttribute;

namespace ChainFx.Web
{
    /// <summary>
    /// For generating dynamic HTML5 content tooled with UiKit.
    /// </summary>
    public class HtmlBuilder : ContentBuilder
    {
        public WebContext Web { get; set; }


        public HtmlBuilder(bool bytely, int capacity) : base(bytely, capacity)
        {
        }

        public override string CType { get; set; } = "text/html; charset=utf-8";

        public void AddEsc(string v)
        {
            if (v == null) return;

            for (int i = 0; i < v.Length; i++)
            {
                char c = v[i];
                if (c == '<')
                {
                    Add("&lt;");
                }
                else if (c == '>')
                {
                    Add("&gt;");
                }
                else if (c == '&')
                {
                    Add("&amp;");
                }
                else if (c == '"')
                {
                    Add("&quot;");
                }
                else
                {
                    Add(c);
                }
            }
        }

        public void AddEsc(string v, int offset, int length)
        {
            if (v == null) return;
            int len = Math.Min(length, v.Length);
            for (int i = offset; i < len; i++)
            {
                char c = v[i];
                if (c == '<')
                {
                    Add("&lt;");
                }
                else if (c == '>')
                {
                    Add("&gt;");
                }
                else if (c == '&')
                {
                    Add("&amp;");
                }
                else if (c == '"')
                {
                    Add("&quot;");
                }
                else
                {
                    Add(c);
                }
            }
        }


        public HtmlBuilder T(char v, bool @if = true)
        {
            if (@if)
            {
                Add(v);
            }

            return this;
        }

        public HtmlBuilder T(bool v, bool @if = true)
        {
            if (@if)
            {
                Add(v);
            }

            return this;
        }

        public HtmlBuilder T(short v, bool @if = true)
        {
            if (@if)
            {
                Add(v);
            }

            return this;
        }

        public HtmlBuilder T(long v, bool @if = true)
        {
            if (@if)
            {
                Add(v);
            }

            return this;
        }

        public HtmlBuilder T(DateTime v, byte date = 3, byte time = 3)
        {
            Add(v, date, time);
            return this;
        }

        public HtmlBuilder T(decimal v, bool @if = true)
        {
            if (@if)
            {
                Add(v);
            }

            return this;
        }

        public HtmlBuilder T(double v, bool @if = true)
        {
            if (@if)
            {
                Add(v);
            }

            return this;
        }

        public HtmlBuilder T(string v, bool @if = true)
        {
            if (@if)
            {
                Add(v);
            }

            return this;
        }

        public HtmlBuilder TT(string v, bool @if = true)
        {
            if (@if)
            {
                AddEsc(v);
            }

            return this;
        }

        public HtmlBuilder T(string v, int offset, int len, bool @if = true)
        {
            if (@if)
            {
                Add(v, offset, len);
            }

            return this;
        }

        public HtmlBuilder TT(string v, int offset, int len, bool @if = true)
        {
            if (@if)
            {
                AddEsc(v, offset, len);
            }

            return this;
        }

        public HtmlBuilder T(string[] v, bool @if = true)
        {
            if (@if && v != null)
            {
                for (int i = 0; i < v.Length; i++)
                {
                    if (i > 0) Add("&nbsp;");
                    Add(v[i]);
                }
            }

            return this;
        }

        public HtmlBuilder TT(string[] v, bool @if = true)
        {
            if (@if && v != null)
            {
                for (int i = 0; i < v.Length; i++)
                {
                    if (i > 0) Add("&nbsp;");
                    AddEsc(v[i]);
                }
            }

            return this;
        }

        public HtmlBuilder MARK(string v, string csspre = null, short idx = 0)
        {
            Add("<mark");
            if (csspre != null)
            {
                Add(" class=\"");
                Add(csspre);
                Add('-');
                Add(idx);
                Add('\"');
            }
            Add(">");
            AddPrimitive(v);
            Add("</mark>");

            return this;
        }


        public HtmlBuilder S<V>(V v, bool cond = true)
        {
            if (cond)
            {
                Add("<s>");
            }

            AddPrimitive(v);

            if (cond)
            {
                Add("</s>");
            }

            return this;
        }

        public HtmlBuilder S2<A, B>(A a, B b, bool cond = true)
        {
            if (cond)
            {
                Add("<s>");
            }

            AddPrimitive(a);

            Add("&nbsp;");

            AddPrimitive(b);

            if (cond)
            {
                Add("</s>");
            }

            return this;
        }


        public HtmlBuilder B_()
        {
            Add("<b>");
            return this;
        }

        public HtmlBuilder _B()
        {
            Add("</b>");
            return this;
        }

        public HtmlBuilder BR()
        {
            Add("<br>");
            return this;
        }

        public HtmlBuilder HR()
        {
            Add("<hr>");
            return this;
        }

        public HtmlBuilder SP()
        {
            Add("&nbsp;");
            return this;
        }

        public HtmlBuilder SUB<V>(V v)
        {
            if (v != null)
            {
                Add("<sub>");
                AddPrimitive(v);
                Add("</sub>");
            }
            return this;
        }

        public HtmlBuilder SUB_(string css = null)
        {
            Add("<sub");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _SUB()
        {
            Add("</sub>");
            return this;
        }

        public HtmlBuilder ABBR<V>(string title, V v, string color = null)
        {
            Add("<abbr title=\"");
            Add(title);
            if (color != null)
            {
                Add("\" style=\"background-color: ");
                Add(color);
            }

            Add("\">");
            AddPrimitive(v);
            Add("</abbr>");
            return this;
        }

        public HtmlBuilder ROW_(string css = null)
        {
            Add("<div class=\"uk-row");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }

            Add("\">");
            return this;
        }

        public HtmlBuilder _ROW()
        {
            Add("</div>");
            return this;
        }


        public HtmlBuilder TH(string caption = null)
        {
            Add("<th>");

            if (caption != null)
            {
                Add(caption);
            }

            Add("</th>");
            return this;
        }

        public HtmlBuilder TH_(string css = null)
        {
            Add("<th");

            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _TH()
        {
            Add("</th>");
            return this;
        }


        public HtmlBuilder TD(bool v)
        {
            Add("<td style=\"text-align: center\">");

            if (v)
            {
                Add("&radic;");
            }

            Add("</td>");
            return this;
        }

        public HtmlBuilder TD(short v, bool right = true, bool @if = true)
        {
            Add("<td");
            if (right)
            {
                Add(" style=\"text-align: right\"");
            }
            Add(">");

            if (@if)
            {
                Add(v);
            }

            Add("</td>");
            return this;
        }

        public HtmlBuilder TD(int v, bool right = true, bool @if = true)
        {
            Add("<td");
            if (right)
            {
                Add(" style=\"text-align: right\"");
            }
            Add(">");

            if (@if)
            {
                Add(v);
            }

            Add("</td>");
            return this;
        }

        public HtmlBuilder TD(long v, bool right = true, bool @if = true)
        {
            Add("<td");
            if (right)
            {
                Add(" style=\"text-align: right\"");
            }
            Add(">");

            if (@if)
            {
                Add(v);
            }

            Add("</td>");
            return this;
        }

        public HtmlBuilder TD(decimal v, bool currency = false, bool @if = true)
        {
            Add("<td style=\"text-align: right\">");
            if (@if)
            {
                if (currency)
                {
                    Add('￥');
                }

                Add(v);
            }

            Add("</td>");
            return this;
        }

        public HtmlBuilder TD(DateTime v)
        {
            Add("<td style=\"text-align: center\">");
            Add(v);
            Add("</td>");
            return this;
        }

        public HtmlBuilder TD(string v, bool @if = true)
        {
            Add("<td>");

            if (@if)
            {
                AddEsc(v);
            }

            Add("</td>");
            return this;
        }

        public HtmlBuilder TD2<A, B>(A a, B b, string css = null)
        {
            Add("<td");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add('>');

            AddPrimitive(a);

            Add("&nbsp;");

            AddPrimitive(b);

            Add("</td>");
            return this;
        }

        public HtmlBuilder TD(string[] v)
        {
            Add("<td>");

            if (v != null)
            {
                for (int i = 0; i < v.Length; i++)
                {
                    if (i > 0) Add(' ');
                    Add(v[i]);
                }
            }

            Add("</td>");
            return this;
        }

        public HtmlBuilder TD_(string css = null, short colspan = 0)
        {
            Add("<td");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            if (colspan > 0)
            {
                Add(" colspan=\"");
                Add(colspan);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _TD()
        {
            Add("</td>");
            return this;
        }

        public HtmlBuilder TDAVAR<K>(K key, string caption)
        {
            TD_();
            AVAR(key, caption);
            _TD();
            return this;
        }

        public HtmlBuilder TDFORM(Action rowform)
        {
            if (rowform != null)
            {
                Add("<td style=\"width: 1px\">");
                Add("<form class=\"uk-button-group\">");
                rowform();
                Add("</form>");
                Add("</td>");
            }

            return this;
        }


        public HtmlBuilder LABEL(string caption, string css = null)
        {
            if (caption != null)
            {
                Add("<label class=\"uk-label");
                if (css != null)
                {
                    Add(' ');
                    Add(css);
                }

                Add("\">");
                Add(caption);
                Add("</label>");
            }

            return this;
        }

        public HtmlBuilder STATIC_(string label)
        {
            LABEL(label);
            Add("<span class=\"uk-static\">");
            return this;
        }

        public HtmlBuilder _STATIC()
        {
            Add("</span>");
            return this;
        }

        public HtmlBuilder STATIC<V>(string label, V v)
        {
            STATIC_(label);
            AddPrimitive(v);
            _STATIC();
            return this;
        }

        public HtmlBuilder COL_(string css = null)
        {
            Add("<div class=\"uk-col");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }

            Add("\">");
            return this;
        }

        public HtmlBuilder _COL()
        {
            Add("</div>");
            return this;
        }

        public HtmlBuilder SPAN_(string css = null)
        {
            Add("<span");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _SPAN()
        {
            Add("</span>");
            return this;
        }

        public HtmlBuilder SPAN<V>(V v, string css = null)
        {
            SPAN_(css);
            AddPrimitive(v);
            _SPAN();
            return this;
        }

        public HtmlBuilder SPAN2<A, B>(A a, B b, bool brace = false, string css = null)
        {
            SPAN_(css);
            AddPrimitive(a);
            Add(brace ? "（" : "&nbsp;");
            AddPrimitive(b);
            if (brace)
            {
                Add("）");
            }
            _SPAN();
            return this;
        }

        public HtmlBuilder H2_(string css = null)
        {
            Add("<h2");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _H2()
        {
            Add("</h2>");
            return this;
        }

        public HtmlBuilder H2<V>(V v, string css = null)
        {
            H2_(css);
            AddPrimitive(v);
            _H2();
            return this;
        }


        public HtmlBuilder H3_(string css = null)
        {
            Add("<h3");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _H3()
        {
            Add("</h3>");
            return this;
        }

        public HtmlBuilder H3<V>(V v, string css = null)
        {
            H3_(css);
            AddPrimitive(v);
            _H3();
            return this;
        }


        public HtmlBuilder H4_(string css = null)
        {
            Add("<h4");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _H4()
        {
            Add("</h4>");
            return this;
        }

        public HtmlBuilder H4<V>(V v, string css = null)
        {
            H4_(css);
            AddPrimitive(v);
            _H4();
            return this;
        }

        public HtmlBuilder H5_(string css = null)
        {
            Add("<h5");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _H5()
        {
            Add("</h5>");
            return this;
        }

        public HtmlBuilder H5<V>(V v, string css = null)
        {
            H5_(css);
            AddPrimitive(v);
            _H5();
            return this;
        }

        public HtmlBuilder SMALL<V>(V v)
        {
            Add("<small>");
            AddPrimitive(v);
            Add("</small>");
            return this;
        }

        public HtmlBuilder SMALL_()
        {
            Add("<small>");
            return this;
        }

        public HtmlBuilder _SMALL()
        {
            Add("</small>");
            return this;
        }

        public HtmlBuilder P<V>(V v, string css = null)
        {
            Add("<p");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add(">");
            AddPrimitive(v);
            Add("</p>");
            return this;
        }

        public HtmlBuilder P2<A, B>(A a, B b, bool brace = false, string css = null)
        {
            Add("<p");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            AddPrimitive(a);

            Add(brace ? "（" : "&nbsp;");

            AddPrimitive(b);

            if (brace)
            {
                Add("）");
            }
            Add("</p>");
            return this;
        }

        public HtmlBuilder P_(string css = null)
        {
            Add("<p");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _P()
        {
            Add("</p>");
            return this;
        }

        public HtmlBuilder Q<V>(V v, string css = null)
        {
            Add("<q");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add(">");
            AddPrimitive(v);
            Add("</q>");
            return this;
        }

        public HtmlBuilder Q2(string a, string b, bool brace = false, string css = null)
        {
            Add("<q");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");

            Add(a);

            if (a != null)
            {
                Add(brace ? "（" : "；");
            }
            else
            {
                if (brace) Add('（');
            }

            Add(b);

            if (brace)
            {
                Add("）");
            }
            Add("</q>");
            return this;
        }

        public HtmlBuilder Q_(string css = null)
        {
            Add("<q");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _Q()
        {
            Add("</q>");
            return this;
        }

        public HtmlBuilder ASIDE_(string css = null)
        {
            Add("<aside");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _ASIDE()
        {
            Add("</aside>");
            return this;
        }

        public HtmlBuilder DIV_(string css = null, bool grid = false)
        {
            Add("<div");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            if (grid)
            {
                Add(" uk-grid");
            }
            Add(">");
            return this;
        }

        public HtmlBuilder _DIV()
        {
            Add("</div>");
            return this;
        }

        public HtmlBuilder SECTION_(string css = null)
        {
            Add("<section");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _SECTION()
        {
            Add("</section>");
            return this;
        }

        public HtmlBuilder MAIN_(string css = null, bool grid = false)
        {
            Add("<main");
            if (grid)
            {
                Add(" uk-grid class=\"uk-child-width-1-1 uk-child-width-1-2@s uk-child-width-1-3@m uk-child-width-1-4@l uk-child-width-1-5@xl");
                if (css != null)
                {
                    Add(' ');
                    Add(css);
                }
                Add("\"");
            }
            else if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add(">");
            return this;
        }

        public HtmlBuilder _MAIN()
        {
            Add("</main>");
            return this;
        }

        public HtmlBuilder HEADER_(string css = null)
        {
            Add("<header");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _HEADER()
        {
            Add("</header>");
            return this;
        }

        public HtmlBuilder HEADER<V>(V v, string css = null)
        {
            HEADER_(css);
            AddPrimitive(v);
            _HEADER();
            return this;
        }


        public HtmlBuilder FOOTER_(string css = null)
        {
            Add("<footer");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _FOOTER()
        {
            Add("</footer>");
            return this;
        }

        public HtmlBuilder ARTICLE_(string css = null, bool grid = false)
        {
            Add("<article");
            if (grid)
            {
                Add(" uk-grid class=\"uk-child-width-expand@s uk-child-width-expand@m");
                if (css != null)
                {
                    Add(' ');
                    Add(css);
                }
                Add("\"");
            }
            else if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add(">");
            return this;
        }

        public HtmlBuilder _ARTICLE()
        {
            Add("</article>");
            return this;
        }

        public HtmlBuilder NAV_(string css = null)
        {
            Add("<nav");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _NAV()
        {
            Add("</nav>");
            return this;
        }

        public HtmlBuilder UL_(string css = null, bool grid = false)
        {
            Add("<ul");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            if (grid)
            {
                Add(" uk-grid");
            }
            Add(">");
            return this;
        }

        public HtmlBuilder _UL()
        {
            Add("</ul>");
            return this;
        }

        public HtmlBuilder OL_(string css = null)
        {
            Add("<ol");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _OL()
        {
            Add("</ol>");
            return this;
        }


        public HtmlBuilder LI_(string css = null)
        {
            Add("<li");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _LI()
        {
            Add("</li>");
            return this;
        }

        public HtmlBuilder DL_(string css = null)
        {
            Add("<dl");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _DL()
        {
            Add("</dl>");
            return this;
        }

        public HtmlBuilder DT_(string css = null)
        {
            Add("<dt");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add(">");
            return this;
        }

        public HtmlBuilder _DT()
        {
            Add("</dt>");
            return this;
        }

        public HtmlBuilder DT(string v)
        {
            Add("<dt>");
            AddEsc(v);
            Add("</dt>");
            return this;
        }

        public HtmlBuilder DT2(string a, string b)
        {
            Add("<dt>");
            AddEsc(a);
            Add("&nbsp;");
            AddEsc(b);
            Add("</dt>");
            return this;
        }

        public HtmlBuilder DT3(string a, string b, string c)
        {
            Add("<dt>");
            AddEsc(a);
            Add("&nbsp;");
            AddEsc(b);
            Add("&nbsp;");
            AddEsc(c);
            Add("</dt>");
            return this;
        }


        public HtmlBuilder DD(string v)
        {
            Add("<dd>");
            AddEsc(v);
            Add("</dd>");
            return this;
        }

        public HtmlBuilder FIELD<V>(string label, V v, string css = null)
        {
            LABEL(label, css);
            Add("<span class=\"uk-static\">");
            AddPrimitive(v);
            Add("</span>");
            return this;
        }

        public HtmlBuilder FIELD<V>(string label, V[] arr)
        {
            LABEL(label);
            Add("<span class=\"uk-static\">");
            if (arr != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i > 0)
                    {
                        Add("&nbsp;");
                    }
                    AddPrimitive(arr[i]);
                }
            }
            Add("</span>");
            return this;
        }

        public HtmlBuilder FIELD2<V, X>(string label, V v, X x, string sep = "&nbsp;")
        {
            LABEL(label);
            Add("<span class=\"uk-static\">");

            AddPrimitive(v);

            if (sep != null)
            {
                Add(sep);
            }

            AddPrimitive(x);

            if (sep == "（")
            {
                Add("）");
            }
            Add("</span>");
            return this;
        }

        public HtmlBuilder FIELD<K, V>(string label, K v, Map<K, V> opts)
        {
            LABEL(label);
            Add("<span class=\"uk-static\">");
            Add(opts[v]?.ToString());
            Add("</span>");
            return this;
        }

        public HtmlBuilder FIELD<K, V>(string label, K[] vals, Map<K, V> opts, Func<V, string> capt = null)
        {
            LABEL(label);
            Add("<span class=\"uk-static\">");
            for (int i = 0; i < vals?.Length; i++)
            {
                if (i > 0)
                {
                    Add("&nbsp;");
                }
                var val = vals[i];
                var opt = opts[val];

                var strv = capt?.Invoke(opt) ?? opt.ToString();

                Add(strv);
            }
            Add("</span>");
            return this;
        }

        public HtmlBuilder FIELD(string label, decimal v, bool money = false)
        {
            LABEL(label);
            Add("<span class=\"uk-static\">");
            if (money)
            {
                Add('￥');
            }
            Add(v, money);
            Add("</span>");
            return this;
        }

        public HtmlBuilder FIELD(string label, bool v)
        {
            LABEL(label);
            Add("<span class=\"uk-static\">");
            Add(v ? '✔' : '✘');
            Add("</span>");
            return this;
        }

        public HtmlBuilder FIELDA<K, V>(string label, K[] keys, Map<K, V> map)
        {
            LABEL(label);
            Add("<span class=\"uk-static\">");
            if (keys != null)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    var k = keys[i];
                    var v = map[k];
                    if (i > 0)
                    {
                        Add("，");
                    }
                    Add(v.ToString());
                }
            }
            Add("</span>");
            return this;
        }

        public HtmlBuilder CNY(decimal v, bool em = false, bool s = false)
        {
            if (em)
            {
                Add("<em>");
            }
            Add('￥');

            if (s)
            {
                Add("<s>");
            }
            Add(v);
            if (s)
            {
                Add("</s>");
            }

            if (em)
            {
                Add("</em>");
            }

            return this;
        }

        public HtmlBuilder ACLOSEUP(string caption)
        {
            Add("<a href=\"#\" onclick=\"closeUp(false); return false;\"");
            Add(" class=\"uk-button uk-button-default uk-border-rounded\"");
            Add(">");
            Add(caption);
            Add("</a>");
            return this;
        }

        public HtmlBuilder AGOTO(string caption, string href = null, string css = null)
        {
            Add("<a target=\"_parent\" href=\"");
            Add(href);
            Add("\" class=\"uk-button");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }
            Add("\" onclick=\"return goto(this, event);\">");
            Add(caption);
            Add("</a>");
            return this;
        }

        public HtmlBuilder AVAR<K>(K key, string caption)
        {
            var vw = Web.Work.VarWork;
            if (vw?.Default != null)
            {
                Add("<a class=\"uk-button uk-button-link");
                Add("\" href=\"");
                PutKey(key);
                Add("/\" onclick=\"return dialog(this,4,false,2,'');\">");
                Add(caption);
                Add("</a>");
            }
            else
            {
                Add(caption);
            }
            return this;
        }

        public HtmlBuilder BUTTONVAR<A, B, C>(A a, B b, C c, string icon, bool disabled = false)
        {
            var vw = Web.Work.VarWork;
            if (vw?.Default != null)
            {
                Add("<button class=\"uk-button uk-button-link");
                Add("\" formmethod=\"get\" formaction=\"");
                PutKey(a);
                PutKey(b);
                PutKey(c);
                Add("\" onclick=\"event.preventDefault(); event.stopPropagation(); return dialog(this,64,false,'');\"");
                if (disabled)
                {
                    Add(" disabled");
                }
                Add("><span uk-icon=\"");
                Add(icon);
                Add("\"</span>");
                Add("</button>");
            }
            else
            {
                Add(icon);
            }
            return this;
        }

        public HtmlBuilder A_<A>(A href, string css = null, int id = 0, string onclick = null)
        {
            Add("<a");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            if (id > 0)
            {
                Add(" id=\"");
                Add(id);
                Add("\"");
            }
            if (onclick != null)
            {
                Add(" onclick=\"");
                Add(onclick);
                Add("\"");
            }
            Add(" href=\"");
            PutKey(href);
            Add("\">");
            return this;
        }

        public HtmlBuilder A_<A, B>(A a, B b, string css = null, int id = 0, string onclick = null)
        {
            Add("<a");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            if (id > 0)
            {
                Add(" id=\"");
                Add(id);
                Add("\"");
            }
            if (onclick != null)
            {
                Add(" onclick=\"");
                Add(onclick);
                Add("\"");
            }
            Add(" href=\"");
            PutKey(a);
            PutKey(b);
            Add("\">");
            return this;
        }

        public HtmlBuilder A_<A, B, C>(A a, B b, C c, string css = null)
        {
            Add("<a");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add(" href=\"");
            PutKey(a);
            PutKey(b);
            PutKey(c);
            Add("\">");
            return this;
        }

        public HtmlBuilder ADIALOG_<A, B>(A a, B b, int mode, bool pick, string tip = null, string css = null)
        {
            Add("<a");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add(" href=\"");
            PutKey(a);
            PutKey(b);
            Add("\"");

            _DIALOG_(mode, pick, tip);

            Add(">");
            return this;
        }

        public HtmlBuilder ADIALOG_<A, B, C>(A a, B b, C c, int mode, bool pick, string tip = null, string css = null)
        {
            Add("<a");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add(" href=\"");
            PutKey(a);
            PutKey(b);
            PutKey(c);
            Add("\"");

            _DIALOG_(mode, pick, tip);

            Add(">");
            return this;
        }

        public HtmlBuilder _A()
        {
            Add("</a>");
            return this;
        }

        public HtmlBuilder ICON(string name, string css = null)
        {
            Add("<span uk-icon=\"");
            Add(name);
            if (css != null)
            {
                Add("\" class=\"");
                Add(css);
            }
            Add("\"></span>");
            return this;
        }

        public HtmlBuilder PIC_(string css = null, bool circle = false)
        {
            Add("<div class=\"");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }
            Add("\"><img style=\"width: 100%\"");
            if (circle)
            {
                Add(" class=\"uk-border-circle\"");
            }

            Add(" src=\"");
            return this;
        }

        public HtmlBuilder _PIC()
        {
            Add("\" alt=\"✰\"></div>");
            return this;
        }

        public HtmlBuilder PIC<A>(A a, bool circle = false, string css = "uk-margin-auto-vertical")
        {
            PIC_(css, circle);
            PutKey(a);
            _PIC();
            return this;
        }

        public HtmlBuilder PIC<A, B>(A a, B b, bool circle = false, string css = "uk-margin-auto-vertical")
        {
            PIC_(css, circle);
            PutKey(a);
            PutKey(b);
            _PIC();
            return this;
        }

        public HtmlBuilder PIC<A, B, C>(A a, B b, C c, bool circle = false, string css = "uk-margin-auto-vertical")
        {
            PIC_(css, circle);
            PutKey(a);
            PutKey(b);
            PutKey(c);
            _PIC();
            return this;
        }

        public HtmlBuilder PROGRESS(int v, int max, string css = null)
        {
            Add("<div class=\"uk-progress");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }

            Add("\"><span style=\"width: ");
            Add(v * 100 / max);
            Add("%\"></div>");
            return this;
        }

        public HtmlBuilder QRCODE(string v, string css = null)
        {
            Add("<div class=\"uk-qrcode");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }

            Add("\"><script type=\"text/javascript\">");
            Add("var scripte = document.scripts[document.scripts.length - 1];");
            Add("new QRCode(scripte.parentNode, \"");
            Add(v);
            Add("\");");
            Add("</script>");
            Add("</div>");
            return this;
        }

        //
        // UIKIT COMPONENTS
        //

        public HtmlBuilder MSG_(bool yes, string title, string msg)
        {
            Add("<div class=\"uk-msg\">");

            // add an icon
            Add("<header class=\"");
            Add(yes ? "uk-msg-yes" : "uk-msg-no");
            Add("\">");

            Add("<span class=\"uk-width-auto\" uk-icon=\"icon: ");
            Add(yes ? "check" : "close");
            Add("; ratio: 2\"></span>");

            Add("<h3 class=\"uk-width-expand\">");
            Add(title);
            Add("</h3>");

            Add("</header>");

            Add("<div>");
            Add(msg);
            Add("</div>");

            Add("</div>");
            return this;
        }

        public HtmlBuilder ALERT_(string css = null)
        {
            Add("<div class=\"");
            if (css != null)
            {
                Add(css);
            }
            Add("\" uk-alert>");

            return this;
        }

        public HtmlBuilder _ALERT()
        {
            Add("</div>");
            return this;
        }

        public HtmlBuilder ALERT(string head, string p = null, string css = null)
        {
            ALERT_(css);

            if (head != null)
            {
                Add("<header>");
                Add(head);
                Add("</header>");
            }

            Add("<p>");
            Add(p);
            Add("</p>");

            _ALERT();
            return this;
        }

        public HtmlBuilder SUBNAV(params string[] actions)
        {
            Add("<ul class=\"uk-subnav\">");
            for (int i = 0; i < actions.Length; i++)
            {
                var act = Web.Work.Actions[actions[i]];
                if (act == null) continue;

                Add("<li");
                if (act == Web.Action)
                {
                    Add(" class=\"uk-active\"");
                }

                Add("><a href=\"");
                Add(act.Key);
                Add("\" onclick=\"goto(this.href, event);\">");
                Add(act.Label);
                Add("</a></li>");
            }

            Add("</ul>");
            return this;
        }

        public HtmlBuilder NAVBAR<V>(string uri, int subscript, Map<short, V> map, Func<short, V, bool> filter = null, string zeroicon = null)
        {
            Add("<nav class=\"uk-top-bar uk-flex-center\">");
            Add("<ul class=\"uk-subnav\">");
            int num = 0;
            var count = map.Count;
            for (int i = 0; i < count; i++)
            {
                var ety = map.EntryAt(i);
                var k = ety.Key;
                var v = ety.Value;
                if (filter != null && !filter(k, v))
                {
                    continue;
                }
                Add("<li");
                if (k == subscript || (zeroicon == null && num == 0 && subscript == 0))
                {
                    Add(" class=\"uk-active\"");
                }
                Add("><a href=\"");
                Add(uri);
                Add('-');
                Add(k);
                Add("\" onclick=\"goto(this.href, event);\">");
                Add(v.ToString());
                Add("</a></li>");
                num++;
            }

            // subzero
            if (zeroicon != null)
            {
                Add("<li");
                if (subscript == 0)
                {
                    Add(" class=\"uk-active\"");
                }
                Add("><a href=\"");
                Add(string.IsNullOrEmpty(uri) ? "./" : uri);
                Add("\" onclick=\"goto(this.href, event);\"><span uk-icon=\"");
                Add(zeroicon);
                Add("\"></a></li>");
            }

            Add("</ul>");
            Add("</nav>");
            Add("<div class=\"uk-top-placeholder\"></div>");

            return this;
        }

        public HtmlBuilder FORM_(string css = null, string action = null, bool post = true, bool mp = false, string oninput = null, string onsubmit = null)
        {
            Add("<form class=\"");
            if (css != null)
            {
                Add(css);
            }

            Add('"');
            if (action != null)
            {
                Add(" action=\"");
                Add(action);
                Add('"');
            }

            if (post)
            {
                Add(" method=\"post\"");
            }

            if (mp)
            {
                Add(" enctype=\"multipart/form-data\"");
            }

            if (oninput != null)
            {
                Add(" oninput=\"");
                Add(oninput);
                Add('"');
            }

            if (onsubmit != null)
            {
                Add(" onsubmit=\"");
                Add(onsubmit);
                Add('"');
            }

            Add(">");
            return this;
        }

        public HtmlBuilder _FORM()
        {
            Add("</form>");
            return this;
        }

        /// <summary>
        /// The combination of fieldset and ul elements.
        /// </summary>
        public HtmlBuilder FIELDSUL_(string legend = null, bool disabled = false, string css = null)
        {
            Add("<fieldset class=\"uk-fieldset uk-width-1-1");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }
            if (disabled)
            {
                Add("\" disabled>");
            }
            else
            {
                Add("\">");
            }

            if (legend != null)
            {
                Add("<legend>");
                AddEsc(legend);
                Add("</legend>");
            }

            Add("<ul class=\"uk-list uk-list-divider\">");
            return this;
        }

        public HtmlBuilder _FIELDSUL()
        {
            Add("</ul>");
            Add("</fieldset>");
            return this;
        }

        public HtmlBuilder LEGEND(string v)
        {
            if (v != null)
            {
                Add("<legend>");
                AddEsc(v);
                Add("</legend>");
            }
            return this;
        }

        public HtmlBuilder BUTTON_(string action, int subscript = -1, bool post = true, string onclick = null, bool chk = false, bool disabled = false, string css = null)
        {
            Add("<button type=\"button\" class=\"uk-button ");
            Add(css);
            Add("\" formmethod=\"");
            Add(post ? "post" : "get");
            if (action != null)
            {
                Add("\" formaction=\"");
                Add(action);
            }
            if (subscript > -1)
            {
                Add('-');
                Add(subscript);
            }
            Add("\" onclick=\"");
            if (onclick != null)
            {
                Add(onclick);
                Add(";");
            }
            else
            {
                Add("btnSubmit(this");
                if (chk)
                {
                    Add(", true");
                }
                Add(");");
            }
            Add("\"");
            if (disabled)
            {
                Add(" disabled");
            }
            Add(">");
            return this;
        }

        public HtmlBuilder _BUTTON()
        {
            Add("</button>");
            return this;
        }

        public HtmlBuilder BUTTON(string caption, string action = null, int subscript = -1, bool post = true, string onclick = null, bool chk = false, bool disabled = false, string css = "uk-button-default")
        {
            BUTTON_(action, subscript, post, onclick, chk, disabled, css);
            AddEsc(caption);
            _BUTTON();
            return this;
        }

        public HtmlBuilder BOTTOM_BUTTON(string caption, string action = null, int subscript = -1, bool post = true, string onclick = null, bool chk = false, bool disabled = false, string css = "uk-button-default")
        {
            BOTTOMBAR_();
            BUTTON_(action, subscript, post, onclick, chk, disabled, css);
            AddEsc(caption);
            _BUTTON();
            _BOTTOMBAR();
            return this;
        }

        public void PAGINATION(bool more, int begin = 0, int step = 1)
        {
            var act = Web.Action;
            if (act.Subscript != null)
            {
                Add("<ul class=\"uk-pagination uk-flex-center\">");

                int page = Web.Subscript;
                if (page > begin)
                {
                    Add("<li class=\"uk-active\">");
                    Add("<a href=\"");
                    Add(act.Key);
                    Add('-');
                    Add(page - step);
                    Add(Web.QueryStr);
                    Add("\">≪</a>");
                    Add("</li>");
                }
                else
                {
                    Add("<li class=\"uk-disabled\">≪</li>");
                }

                if (more)
                {
                    Add("<li class=\"uk-active\">");
                    Add("<a href=\"");
                    Add(act.Key);
                    Add('-');
                    Add(page + step);
                    Add(Web.QueryStr);
                    Add("\">≫</a>");
                    Add("</li>");
                }
                else
                {
                    Add("<li class=\"uk-disabled\">≫</li>");
                }

                Add("</ul>");
            }
        }

        public HtmlBuilder LIST<M>(M[] arr, Action<M> item, string ul = null, string li = null)
        {
            Add("<ul class=\"uk-list uk-list-divider");
            if (ul != null)
            {
                Add(' ');
                Add(ul);
            }

            Add("\">");

            if (arr != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    var obj = arr[i];
                    Add("<li class=\"uk-flex");
                    if (li != null)
                    {
                        Add(' ');
                        Add(li);
                    }

                    Add("\">");
                    item(obj);
                    Add("</li>");
                }
            }

            Add("</ul>");
            return this;
        }

        public void LIST<M, K>(Map<K, M> map, Action<Map<K, M>.Entry> item, string ul = null, string li = null)
        {
            Add("<ul class=\"uk-list");
            if (ul != null)
            {
                Add(' ');
                Add(ul);
            }

            Add("\">");

            if (map != null)
            {
                for (int i = 0; i < map.Count; i++)
                {
                    var ety = map.EntryAt(i);
                    Add("<li class=\"");
                    if (li != null)
                    {
                        Add(' ');
                        Add(li);
                    }

                    Add("\">");
                    item(ety);
                    Add("</li>");
                }
            }

            Add("</ul>");
        }

        public HtmlBuilder LIST<S>(S src, Action<S> item, string ul = null, string li = null) where S : ISource
        {
            Add("<ul class=\"uk-list uk-list-divider");
            if (ul != null)
            {
                Add(' ');
                Add(ul);
            }

            Add("\">");

            if (src != null && src.IsDataSet)
            {
                while (src.Next())
                {
                    Add("<li class=\"uk-flex");
                    if (li != null)
                    {
                        Add(' ');
                        Add(li);
                    }

                    Add("\">");
                    item(src);
                    Add("</li>");
                }
            }

            Add("</ul>");
            return this;
        }

        public HtmlBuilder ACCORDIONUL_(string css = "uk-list uk-list-divider", bool collapse = false, bool multiple = true)
        {
            Add("<ul uk-accordion=\"");
            if (collapse)
            {
                Add("collapsible: false;");
            }
            if (multiple)
            {
                Add("multiple: true");
            }
            Add("\" multiple: true\" class=\"");
            Add(css);
            Add("\">");
            return this;
        }

        public HtmlBuilder _ACCORDIONUL()
        {
            Add("</ul>");
            return this;
        }

        public HtmlBuilder ACCORDION<M>(M[] arr, Action<M> item, string ul = null, string li = "uk-card-default")
        {
            Add("<ul uk-accordion=\"multiple: true\" class=\"");
            if (ul != null)
            {
                Add(' ');
                Add(ul);
            }

            Add("\">");

            if (arr != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    M obj = arr[i];
                    Add("<li class=\"uk-flex uk-card");
                    if (li != null)
                    {
                        Add(' ');
                        Add(li);
                    }

                    Add("\">");
                    item(obj);
                    Add("</li>");
                }
            }

            // pagination if any
            Add("</ul>");
            return this;
        }

        public HtmlBuilder ACCORDION<K, M>(Map<K, M> map, Action<Map<K, M>.Entry> card, string ul = null, string li = "uk-card-default")
        {
            Add("<ul uk-accordion=\"multiple: true\" class=\"");
            if (ul != null)
            {
                Add(' ');
                Add(ul);
            }

            Add("\">");

            if (map != null)
            {
                for (int i = 0; i < map.Count; i++)
                {
                    var ety = map.EntryAt(i);
                    Add("<li class=\"uk-card");
                    if (li != null)
                    {
                        Add(' ');
                        Add(li);
                    }

                    Add("\">");
                    card(ety);
                    Add("</li>");
                }
            }

            // pagination if any
            Add("</ul>");
            return this;
        }

        public HtmlBuilder TABLE_()
        {
            // Add("<div class=\"uk-overflow-auto\">");
            Add("<table class=\"uk-table uk-table-divider uk-table-hover\">");
            return this;
        }

        public HtmlBuilder _TABLE()
        {
            Add("</table>");
            return this;
        }

        public HtmlBuilder TABLE(Action tbody, Action thead = null)
        {
            Add("<table class=\"uk-table uk-table-divider uk-table-hover\">");
            if (thead != null)
            {
                Add("<thead>");
                thead();
                Add("</thead>");
            }
            Add("<tbody>");
            tbody();
            Add("</tbody>");
            Add("</table>");
            return this;
        }

        public HtmlBuilder TR_()
        {
            Add("<tr>");
            return this;
        }

        public HtmlBuilder _TR()
        {
            Add("</tr>");
            return this;
        }

        public HtmlBuilder TDCHECK<K>(K key, bool toolbar = true, bool disabled = false)
        {
            Add("<td style=\"width: 1%\"><input");
            if (toolbar)
            {
                Add(" form=\"tool-bar-form\"");
            }

            Add(" name=\"key\" type=\"checkbox\" class=\"uk-checkbox\" value=\"");
            PutKey(key);
            Add("\" onchange=\"checkToggle(this);\"");
            if (disabled)
            {
                Add(" disabled");
            }
            Add("></td>");
            return this;
        }

        public HtmlBuilder TDRADIO<K>(K key, bool toolbar = true)
        {
            Add("<td style=\"width: 1%\"><input");
            if (toolbar)
            {
                Add(" form=\"tool-bar-form\"");
            }

            Add(" name=\"key\" type=\"radio\" class=\"uk-radio\" value=\"");
            PutKey(key);
            Add("\"></td>");
            return this;
        }


        public void TABLE<M>(M[] arr, Action<M> tr, Action thead = null, string caption = null)
        {
            Add("<table class=\"uk-table uk-table-hover uk-table-divider\">");
            if (caption != null)
            {
                Add("<caption>");
                Add(caption);
                Add("</caption>");
            }
            if (arr != null && tr != null) // tbody if having data objects
            {
                Add("<tbody>");
                thead?.Invoke();
                for (int i = 0; i < arr.Length; i++)
                {
                    var obj = arr[i];
                    Add("<tr>");
                    tr(obj);
                    Add("</tr>");
                }
                Add("</tbody>");
            }
            Add("</table>");
        }

        public void TABLE<K, M>(Map<K, M> arr, Action<Map<K, M>.Entry> tr, Action thead = null, string caption = null)
        {
            Add("<table class=\"uk-table uk-table-hover uk-table-divider\">");
            if (caption != null)
            {
                Add("<caption>");
                Add(caption);
                Add("</caption>");
            }
            if (arr != null && tr != null) // tbody if having data objects
            {
                Add("<tbody>");
                thead?.Invoke();

                for (int i = 0; i < arr.Count; i++)
                {
                    var obj = arr.EntryAt(i);
                    Add("<tr>");
                    tr(obj);
                    Add("</tr>");
                }
                Add("</tbody>");
            }
            Add("</table>");
        }


        public HtmlBuilder MAINGRID<M>(M[] arr, Action<M> card, Predicate<M> filter = null, string css = null)
        {
            Add("<main uk-grid class=\"uk-child-width-1-1 uk-child-width-1-2@s uk-child-width-1-3@m uk-child-width-1-4@l uk-child-width-1-5@xl\">");
            if (arr != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    var obj = arr[i];
                    if (filter != null && !filter(obj))
                    {
                        continue;
                    }
                    Add("<article class=\"uk-card uk-card-default");
                    if (css != null)
                    {
                        Add(' ');
                        Add(css);
                    }
                    Add("\">");

                    card(obj);

                    Add("</article>");
                }
            }
            Add("</main>");

            return this;
        }


        public void GRID<M>(M[] arr, Action<M> card, Predicate<M> filter = null, int min = 1, string css = "uk-card-default")
        {
            Add("<div uk-grid class=\"uk-child-width-1-");
            Add(min++);
            Add(" uk-child-width-1-");
            Add(min++);
            Add("@s uk-child-width-1-");
            Add(min++);
            Add("@m uk-child-width-1-");
            Add(min++);
            Add("@l uk-child-width-1-");
            Add(min);
            Add("@xl\">");
            if (arr != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    var obj = arr[i];
                    if (filter != null && !filter(obj))
                    {
                        continue;
                    }
                    Add("<article class=\"uk-card");
                    if (css != null)
                    {
                        Add(' ');
                        Add(css);
                    }
                    Add("\">");
                    card(obj);
                    Add("</article>");
                }
            }
            Add("</div>");
        }

        public void MAINGRID<S>(S src, Action<S> card, string css = null) where S : ISource
        {
            Add("<main uk-grid class=\"uk-child-width-1-1 uk-child-width-1-2@s uk-child-width-1-3@m uk-child-width-1-4@l uk-child-width-1-5@xl\">");
            if (src != null && src.IsDataSet)
            {
                while (src.Next())
                {
                    Add("<article class=\"uk-card uk-card-default");
                    if (css != null)
                    {
                        Add(' ');
                        Add(css);
                    }
                    Add("\">");

                    card(src);

                    Add("</article>");
                }
            }
            Add("</main>");
        }


        HtmlBuilder _DIALOG_(int mode, bool pick, string tip = null)
        {
            Add(" onclick=\"event.preventDefault(); return dialog(this,");
            Add(mode);
            Add(",");
            Add(pick);
            Add(",'");
            if (tip != null)
            {
                Add(tip);
            }
            Add("');\"");

            return this;
        }

        public HtmlBuilder TOPBAR_()
        {
            Add("<form class=\"uk-top-bar\">");
            return this;
        }

        public HtmlBuilder _TOPBAR()
        {
            Add("</form>");
            Add("<div class=\"uk-top-placeholder\"></div>");
            return this;
        }

        public HtmlBuilder TOPBARXL_(string css = null)
        {
            Add("<form class=\"uk-top-bar uk-flex uk-large");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }
            Add("\">");
            return this;
        }

        public HtmlBuilder _TOPBARXL()
        {
            Add("</form>");
            Add("<div class=\"uk-top-placeholder uk-large\"></div>");
            return this;
        }

        public HtmlBuilder TOOLBAR(byte group = 0, int subscript = -1, bool toggle = false, string tip = null, bool bottom = false, short status = 0, short state = 0)
        {
            var ctxgrp = group > 0 ? group : Web.Action.Group; // the contextual group

            Add("<form id=\"tool-bar-form\" class=\"");
            Add(bottom ? "uk-bottom-bar" : "uk-top-bar");
            Add("\">");

            if (bottom)
            {
                Add("<span class=\"uk-margin-auto-right\">");
                if (tip != null)
                {
                    Add(tip);
                }
                Add("</span>");
            }

            bool astack = Web.Query[nameof(astack)];
            if (astack)
            {
                Add("<a class=\"uk-icon-button\" href=\"javascript: window.parent.closeUp(false);\" uk-icon=\"icon: chevron-left; ratio: 1.75\"></a>");
            }
            Add("<nav"); // the button group
            if (bottom)
            {
                Add(" class=\"uk-flex-center\"");
            }
            Add(">");
            if (toggle)
            {
                Add("<input type=\"checkbox\" class=\"uk-checkbox\" onchange=\"return toggleAll(this);\">&nbsp;");
            }
            var acts = Web.Work.Tooled;
            if (acts != null)
            {
                foreach (var act in acts)
                {
                    int g = act.Group;
                    var tool = act.Tool;

                    if (!tool.Meets(status, state)) continue;

                    if (tool.IsAnchorTag || ctxgrp == g || (g & ctxgrp) > 0)
                    {
                        // provide the state about current anchor as subscript 
                        PutTool(act, tool, tool.IsAnchorTag ? -1 : subscript, astack: astack, css: "uk-button-primary");
                    }
                }
            }
            Add("</nav>");

            if (!bottom)
            {
                if (tip != null)
                {
                    Add("<span class=\"uk-label uk-padding\">");
                    Add(tip);
                    Add("</span>");
                }
            }

            Add("</form>");
            Add("<div class=\"");
            Add(bottom ? "uk-bottom-placeholder" : "uk-top-placeholder");
            Add("\"></div>");
            return this;
        }

        public HtmlBuilder TOOLBAR<K, V>(int subscript, Map<K, V> opts = null, Func<K, V, bool> filter = null, byte group = 0, bool toggle = false, string tip = null)
        {
            var ctxgrp = group > 0 ? group : Web.Action.Group; // the contextual group

            Add("<form id=\"tool-bar-form\" class=\"");
            Add("uk-top-bar");
            Add("\">");
            Add("<span class=\"uk-button-group\">");

            bool astack = Web.Query[nameof(astack)];

            if (astack)
            {
                Add("<a class=\"uk-icon-button\" href=\"javascript: closeUp(false);\" uk-icon=\"icon: chevron-left; ratio: 1.75\"></a>");
            }

            if (toggle)
            {
                Add("<input type=\"checkbox\" class=\"uk-checkbox\" onchange=\"return toggleAll(this);\">&nbsp;");
            }

            if (opts != null)
            {
                Add("<select class=\"uk-select uk-button-primary\" onchange=\"goto(subscriptUri(location.href, this.value), event);\">");
                var grpopen = false;
                for (int i = 0; i < opts.Count; i++)
                {
                    var e = opts.EntryAt(i);
                    var key = e.key;
                    var val = e.value;
                    if (filter != null && !filter(key, val)) continue;
                    if (e.IsHead)
                    {
                        if (grpopen)
                        {
                            Add("</optgroup>");
                            grpopen = false;
                        }

                        Add("<optgroup label=\"");
                        AddPrimitive(val.ToString());
                        Add("\">");
                        grpopen = true;
                    }
                    else
                    {
                        Add("<option value=\"");
                        AddPrimitive(key);
                        Add("\"");
                        switch (key)
                        {
                            case short shortkey when shortkey == subscript:
                            case int intkey when intkey == subscript:
                                Add(" selected");
                                break;
                        }
                        Add(">");
                        AddPrimitive(e.Value.ToString());
                        Add("</option>");
                    }
                }

                if (grpopen)
                {
                    Add("</optgroup>");
                    grpopen = false;
                }

                Add("</select>");
            }

            var acts = Web.Work.Tooled;
            if (acts != null)
            {
                for (int i = 0; i < acts.Length; i++)
                {
                    var act = acts[i];
                    int g = act.Group;
                    var tool = act.Tool;

                    if (tool.IsAnchorTag || ctxgrp == g || (g & ctxgrp) > 0)
                    {
                        // provide the state about current anchor as subscript 
                        PutTool(act, tool, tool.IsAnchorTag ? -1 : subscript, astack: astack, css: "uk-button-primary");
                    }
                }
            }

            Add("</span>");

            if (tip != null)
            {
                Add("<span class=\"uk-label uk-padding\">");
                Add(tip);
                Add("</span>");
            }

            Add("</span>");

            Add("</form>");
            Add("<div class=\"");
            Add("uk-top-placeholder");
            Add("\"></div>");
            return this;
        }

        public HtmlBuilder BOTTOMBAR_(string css = null, bool toggle = false, bool large = false)
        {
            if (large)
            {
                Add("<div class=\"uk-bottom-placeholder uk-large\"></div>");
                Add("<div class=\"uk-bottom-bar uk-large");
            }
            else
            {
                Add("<div class=\"uk-bottom-placeholder\"></div>");
                Add("<div class=\"uk-bottom-bar");
            }
            if (css != null)
            {
                Add(' ');
                Add(css);
            }
            if (toggle)
            {
                Add("\" id=\"tool-bar-form");
            }
            Add("\">");
            if (toggle)
            {
                Add("<span style=\"position: absolute; left: 0.25rem\"><input type=\"checkbox\" class=\"uk-checkbox\" onchange=\"return toggleAll(this);\"></span>");
            }
            return this;
        }

        public HtmlBuilder _BOTTOMBAR()
        {
            Add("</div>");
            return this;
        }

        public HtmlBuilder PICK<K>(K varkey, string label = null, bool toolbar = false, bool only = false, short follow = 0, string css = null)
        {
            Add("<label");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add("><input");
            if (toolbar)
            {
                Add(" form=\"tool-bar-form\"");
            }
            Add(" name=\"key\" type=\"checkbox\" class=\"uk-checkbox\" value=\"");
            PutKey(varkey);
            Add("\" onchange=\"checkToggle(this");
            if (only)
            {
                Add(", true, ");
                Add(follow);
            }
            Add(");\">&nbsp;");
            Add(label);
            Add("</label>");
            return this;
        }

        public HtmlBuilder VARTOOLSET<K>(K varkey, int subscript = -1, string pick = null, string css = null, bool nav = true)
        {
            if (nav)
            {
                Add("<nav class=\"uk-button-group");
                if (css != null)
                {
                    Add(' ');
                    Add(css);
                }

                Add("\">");
            }

            var w = Web.Work;
            var vw = w.VarWork;

            // output a pick checkbox
            if (vw != null && pick != null)
            {
                PICK(varkey, pick);
            }

            // output button group
            var actgrp = Web.Action.Group;
            var acts = vw?.Tooled;
            if (acts != null)
            {
                for (int i = 0; i < acts.Length; i++)
                {
                    var act = acts[i];

                    int g = act.Group;
                    if (g == actgrp || (g & actgrp) > 0)
                    {
                        var tool = act.Tool;

                        PutToolVar(act, tool, varkey, tool.IsAnchorTag ? -1 : subscript, null, null, true, null);
                    }
                }
            }

            if (nav)
            {
                Add("</nav>");
            }

            return this;
        }

        public HtmlBuilder TOOL(string action, int subscript = -1, string caption = null, string tip = null, ToolAttribute toolattr = null, bool enabled = true, string css = null)
        {
            // locate the proper work
            var w = Web.Work;
            var act = w[action];

            var tool = toolattr ?? act?.Tool;
            if (tool != null)
            {
                PutTool(act, tool, subscript, caption, tip, enabled, false, css);
            }

            return this;
        }

        public void WORKBOARD(byte group = 0)
        {
            var wc = Web;
            var wrk = wc.Work;
            // render tabs

            string last = null;

            for (int i = 0; i < wrk.SubWorks?.Count; i++)
            {
                var sub = wrk.SubWorks.ValueAt(i);
                if (sub == null)
                {
                    continue;
                }

                if (sub.Group != 0 && (sub.Group & group) != sub.Group)
                {
                    continue;
                }

                if (sub.Ui == null || !sub.DoAuthorize(wc, true))
                {
                    continue;
                }


                if (last == null || last != sub.Tip)
                {
                    if (last != null)
                    {
                        _FORM();
                        _UL();
                    }
                    FORM_("uk-card uk-card-primary");
                    H3(sub.Tip, "uk-card-header");
                    UL_("uk-card-body uk-child-width-1-2", grid: true);
                }

                LI_();

                int mode = sub.HasVarWork ? MOD_ASTACK : MOD_OPEN;
                ADIALOG_(sub.Key, "/", mode, false, tip: sub.Label, css: "uk-width-1-1").SPAN(sub.Label).ICON("chevron-right")._A();
                _LI();

                last = sub.Tip;
            }
            _FORM();
            _UL();
        }


        public HtmlBuilder CROPPIE(short wid, short hei, string caption, bool large = false)
        {
            Add("<main id=\"imgbnd\" class=\"");
            Add(large ? "uk-height-large" : "uk-height-medium");
            Add("\"><input class=\"uk-button uk-button-secondary\" type=\"file\" id=\"imginp\" style=\"display: none;\" required onchange=\"bind(this.parentNode, window.URL.createObjectURL(this.files[0]),");
            Add(wid);
            Add(", ");
            Add(hei);
            Add(");\">");
            Add("<input type=\"hidden\" id=\"img\" name=\"img\">");
            Add("</main>");
            Add("<hr>");
            Add("<div class=\"uk-card-footer uk-flex-center\">");
            Add("<button type=\"button\" class=\"uk-button uk-button-secondary\" onclick=\"$('#imginp').click(); return;\">");
            Add(caption);
            Add("</button>");
            Add("</div>");

            return this;
        }

        void PutKey<K>(K k)
        {
            if (k == null) return;

            if (k is short shortv) Add(shortv);
            else if (k is int intv) Add(intv);
            else if (k is long longv) Add(longv);
            else if (k is string strv) Add(strv);
            else if (k is DateTime dtv) Add(dtv, 3, 0);
            else if (k is ValueTuple<short, short> ss)
            {
                Add(ss.Item1);
                Add('-');
                Add(ss.Item2);
            }
            else if (k is ValueTuple<int, int> ii)
            {
                Add(ii.Item1);
                Add('-');
                Add(ii.Item2);
            }
            else if (k is ValueTuple<long, long> ll)
            {
                Add(ll.Item1);
                Add('-');
                Add(ll.Item2);
            }
            else if (k is ValueTuple<string, string> tt)
            {
                Add(tt.Item1);
                Add('-');
                Add(tt.Item2);
            }
            else if (k is ValueTuple<string, int> ti)
            {
                Add(ti.Item1);
                Add('-');
                Add(ti.Item2);
            }
            else if (k is ValueTuple<int, string> it)
            {
                Add(it.Item1);
                Add('-');
                Add(it.Item2);
            }
            else if (k is ValueTuple<long, short> ls)
            {
                Add(ls.Item1);
                Add('-');
                Add(ls.Item2);
            }
            else if (k is ValueTuple<short, long> sl)
            {
                Add(sl.Item1);
                Add('-');
                Add(sl.Item2);
            }
        }

        public void PutTool(WebAction act, ToolAttribute tool, int subscript = -1, string caption = null, string tip = null, bool enabled = true, bool astack = false, string css = null)
        {
            // check action's availability
            //
            string cap = caption ?? act.Label;
            string icon = act.Icon;
            bool ok = enabled && (Web.Principal == null || act.DoAuthorize(Web, true));
            tip ??= act.Tip;

            if (tool.IsAnchorTag) // A
            {
                Add("<a class=\"uk-button ");
                Add(css ?? "uk-button-link");
                if (act == Web.Action) // if current action
                {
                    Add(" uk-active");
                }
                if (!ok)
                {
                    Add(" disabled");
                }
                Add("\" href=\"");
                Add(act == Web.Action ? act.Key : act.Relative);
                if (subscript == -1 && Web.Subscript > 0)
                {
                    subscript = Web.Subscript;
                }
                if (subscript > 0 && act.Subscript != null)
                {
                    Add('-');
                    Add(subscript);
                }
                if (astack)
                {
                    Add("?astack=true");
                }
                Add("\"");
            }
            else // BUTTON
            {
                Add("<button class=\"uk-button ");
                Add(css ?? "uk-button-primary");
                if (cap?.Length == 1)
                {
                    Add(" uk-width-micro");
                }
                Add("\" name=\"");
                Add(act.Key);
                Add("\" formaction=\"");
                Add(act.Key);
                if (subscript != -1 && act.Subscript != null)
                {
                    Add('-');
                    Add(subscript);
                }
                Add("\"");
                if (tool.IsPost)
                {
                    Add(" formmethod=\"post\"");
                }
            }

            if (!ok)
            {
                Add(" disabled=\"disabled\" onclick=\"return false;\"");
            }
            else if (tool.IsAnchor)
            {
                Add(" onclick=\"goto(this.href, event);\"");
            }
            else if (tool.HasScript)
            {
                Add(" onclick=\"return call_"); // prefix to avoid js naming conflict
                Add(act.Name);
                Add("(this);\"");
            }
            else if (tool.HasConfirm)
            {
                Add(" onclick=\"return ");
                if (tool.MustPick)
                {
                    Add("!serialize(this.form) ? false : ");
                }

                Add("askSend(this, '");
                Add(tip ?? act.Label);
                Add("');\"");
            }
            else if (tool.HasCrop)
            {
                Add(" onclick=\"return crop(this,");
                Add(tool.Size);
                Add(",'");
                Add(tip);
                Add("',");
                Add(tool.Subs);
                Add(");\"");
            }
            else if (tool.HasAnyDialog)
            {
                _DIALOG_(tool.Mode, tool.MustPick, tip);
            }

            if (tip != null)
            {
                Add(" title=\"");
                Add(tip);
                Add("\"");
            }
            Add(">");

            if (icon != null)
            {
                Add("<span uk-icon=\"");
                Add(icon);
                Add("\"></span>");
            }
            if (!string.IsNullOrEmpty(cap))
            {
                Add("<span>");
                if (icon != null)
                {
                    Add("&nbsp;");
                }
                Add(cap);
                Add("</span>");
            }

            // put the closing tag
            Add(tool.IsAnchorTag ? "</a>" : "</button>");
        }

        void PutToolVar<K>(WebAction act, ToolAttribute tool, K varkey, int subscript = -1, string caption = null, string tip = null, bool enabled = true, string css = null)
        {
            // check action's availability
            //
            var cap = caption ?? act.Label;
            string icon = act.Icon;
            var ok = enabled && (Web.Principal == null || act.DoAuthorize(Web, true));
            tip ??= act.Tip;

            if (tool.IsAnchorTag)
            {
                Add("<a class=\"uk-button uk-button-link");
                if (css != null)
                {
                    Add(' ');
                    Add(css);
                }
                if (act == Web.Action) // if current action
                {
                    Add(" uk-active");
                }

                if (!ok)
                {
                    Add(" disabled");
                }

                Add("\" href=\"");
                PutKey(varkey);
                Add('/');
                Add(act == Web.Action ? act.Key : act.Relative);
                if (subscript == -1 && Web.Subscript > 0)
                {
                    subscript = Web.Subscript;
                }
                if (subscript > 0 && act.Subscript != null)
                {
                    Add('-');
                    Add(subscript);
                }

                Add("\"");
            }
            else
            {
                Add("<button class=\"uk-button uk-button-small");
                if (css != null)
                {
                    Add(' ');
                    Add(css);
                }
                Add("\" name=\"");
                Add(act.Key);
                Add("\" formaction=\"");
                PutKey(varkey);
                Add('/');
                Add(act.Key);
                if (subscript != -1 && act.Subscript != null)
                {
                    Add('-');
                    Add(subscript);
                }

                Add("\"");
                if (tool.IsPost)
                {
                    Add(" formmethod=\"post\"");
                }
            }

            if (!ok)
            {
                Add(" disabled=\"disabled\" onclick=\"return false;\"");
            }
            else if (tool.HasConfirm)
            {
                Add(" onclick=\"return ");
                if (tool.MustPick)
                {
                    Add("!serialize(this.form) ? false : ");
                }

                Add("askSend(this, '");
                Add(tip ?? act.Label);
                Add("');\"");
            }
            else if (tool.IsAnchor)
            {
                Add(" onclick=\"goto(this.href, event);\"");
            }
            else if (tool.HasScript)
            {
                Add(" onclick=\"return call_"); // prefix to avoid js naming conflict
                Add(act.Name);
                Add("(this);\"");
            }
            else if (tool.HasCrop)
            {
                Add(" onclick=\"return crop(this,");
                Add(tool.Size);
                Add(",'");
                Add(tip);
                Add("',");
                Add(tool.Subs);
                Add(");\"");
            }
            else if (tool.HasAnyDialog)
            {
                _DIALOG_(tool.Mode, tool.MustPick, tip);
            }

            if (tip != null)
            {
                Add(" title=\"");
                Add(tip);
                Add("\"");
            }
            Add(">");
            if (icon != null)
            {
                Add("<span uk-icon=\"");
                Add(icon);
                Add("\"></span>");
            }
            if (!string.IsNullOrEmpty(cap))
            {
                Add("<span");
                if (icon != null)
                {
                    Add(" class=\"uk-visible@m\"");
                }
                Add(">");
                if (icon != null)
                {
                    Add("&nbsp;");
                }
                Add(cap);
                Add("</span>");
            }
            // put the closing tag
            Add(tool.IsAnchorTag ? "</a>" : "</button>");
        }

        //
        // CONTROLS
        //

        public HtmlBuilder HIDDEN<V>(string name, V val)
        {
            Add("<input type=\"hidden\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddPrimitive(val);
            Add("\">");
            return this;
        }

        public HtmlBuilder HIDDEN(string name, string val)
        {
            Add("<input type=\"hidden\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddEsc(val);
            Add("\">");
            return this;
        }

        public HtmlBuilder HIDDENS<V>(string name, V[] vals)
        {
            for (int i = 0; i < vals?.Length; i++)
            {
                Add("<input type=\"hidden\" name=\"");
                Add(name);
                Add("\" value=\"");
                AddPrimitive(vals[i]);
                Add("\">");
            }
            return this;
        }

        public HtmlBuilder TEXT(string label, string name, string v, string tip = null, string pattern = null, sbyte max = 0, sbyte min = 0, bool @readonly = false, bool required = false, string[] datalst = null, string css = null)
        {
            LABEL(label);
            Add("<input type=\"text\" class=\"uk-input");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }
            Add("\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddEsc(v);
            Add("\"");
            if (tip != null)
            {
                Add(" placeholder=\"");
                Add(tip);
                Add("\"");
            }

            if (pattern != null)
            {
                Add(" pattern=\"");
                AddEsc(pattern);
                Add("\"");
            }

            if (max > 0)
            {
                Add(" maxlength=\"");
                Add(max);
                Add("\"");
            }

            if (min > 0)
            {
                Add(" minlength=\"");
                Add(min);
                Add("\"");
            }

            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            if (datalst != null)
            {
                Add(" list=\"for-");
                Add(name);
                Add("\"");
            }
            Add(">");

            if (datalst != null)
            {
                Add("<datalist id=\"for-");
                Add(name);
                Add("\">");
                for (int i = 0; i < datalst.Length; i++)
                {
                    string dat = datalst[i];
                    Add("<option value=\"");
                    Add(dat);
                    Add("\">");
                }
                Add("</datalist>");
            }
            return this;
        }

        public HtmlBuilder TEXT(string label, string name, short[] v, string tip = null, sbyte max = 0, sbyte min = 0, bool @readonly = false, bool required = false)
        {
            LABEL(label);
            Add("<input type=\"text\" class=\"uk-input\" name=\"");
            Add(name);
            Add("\" value=\"");
            for (int i = 0; i < v?.Length; i++)
            {
                if (i > 0)
                {
                    Add(' ');
                }
                Add(v[i]);
            }
            Add("\" pattern=\"[0-9\\s]+\""); // numbers and spaces
            if (tip != null)
            {
                Add(" placeholder=\"");
                Add(tip);
                Add("\"");
            }

            if (max > 0)
            {
                Add(" maxlength=\"");
                Add(max);
                Add("\"");
            }

            if (min > 0)
            {
                Add(" minlength=\"");
                Add(min);
                Add("\"");
            }

            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            Add(">");

            return this;
        }


        public HtmlBuilder FILE(string label, string name, string v, string tip = null, bool @readonly = false, bool required = false, bool list = false)
        {
            LABEL(label);
            Add("<input class=\"uk-input\" type=\"file\" name=\"");
            Add(name);
            Add("\">");
            return this;
        }

        public HtmlBuilder TEL(string label, string name, string v, string tip = null, string pattern = null, sbyte max = 0, sbyte min = 0, bool @readonly = false, bool required = false)
        {
            LABEL(label);
            Add("<input class=\"uk-input\" type=\"tel\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddEsc(v);
            Add("\"");
            if (tip != null)
            {
                Add(" placeholder=\"");
                Add(tip);
                Add("\"");
            }

            if (pattern != null)
            {
                Add(" pattern=\"");
                AddEsc(pattern);
                Add("\"");
            }

            if (max > 0)
            {
                Add(" maxlength=\"");
                Add(max);
                Add("\"");
            }

            if (min > 0)
            {
                Add(" minlength=\"");
                Add(min);
                Add("\"");
            }

            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            Add(">");
            return this;
        }

        public HtmlBuilder URL(string label, string name, string v, string tip = null, string pattern = null, sbyte max = 0, sbyte min = 0, bool @readonly = false, bool required = false)
        {
            LABEL(label);
            Add("<input class=\"uk-input\" type=\"url\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddEsc(v);
            Add("\"");
            if (tip != null)
            {
                Add(" placeholder=\"");
                Add(tip);
                Add("\"");
            }

            if (pattern != null)
            {
                Add(" pattern=\"");
                AddEsc(pattern);
                Add("\"");
            }

            if (max > 0)
            {
                Add(" maxlength=\"");
                Add(max);
                Add("\"");
            }

            if (min > 0)
            {
                Add(" minlength=\"");
                Add(min);
                Add("\"");
            }

            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            Add(">");
            return this;
        }

        public HtmlBuilder SEARCH(string label, string name, string v, string tip = null, string pattern = null, sbyte max = 0, sbyte min = 0, bool required = false)
        {
            LABEL(label);
            Add("<div class=\"uk-inline");
            Add("\"><input type=\"search\" class=\"uk-input\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddEsc(v);
            Add("\"");

            if (tip != null)
            {
                Add(" placeholder=\"");
                Add(tip);
                Add("\"");
            }

            if (pattern != null)
            {
                Add(" pattern=\"");
                AddEsc(pattern);
                Add("\"");
            }

            if (max > 0)
            {
                Add(" maxlength=\"");
                Add(max);
                Add("\"");
                Add(" size=\"");
                Add(max);
                Add("\"");
            }

            if (min > 0)
            {
                Add(" minlength=\"");
                Add(min);
                Add("\"");
            }

            Add(">");
            Add("<a class=\"uk-form-icon uk-form-icon-flip\" href=\"#\" onclick=\"this.previousSibling.form.method = 'get'; this.previousSibling.form.submit();\" uk-icon=\"search\"></a>");
            Add("</div>");
            return this;
        }

        public HtmlBuilder PASSWORD(string label, string name, string v, string tip = null, string pattern = null, sbyte max = 0, sbyte min = 0, bool @readonly = false, bool required = false)
        {
            LABEL(label);
            Add("<input type=\"password\" class=\"uk-input\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddEsc(v);
            Add("\"");

            if (tip != null)
            {
                Add(" Help=\"");
                Add(tip);
                Add("\"");
            }

            if (pattern != null)
            {
                Add(" pattern=\"");
                AddEsc(pattern);
                Add("\"");
            }

            if (max > 0)
            {
                Add(" maxlength=\"");
                Add(max);
                Add("\"");
                Add(" size=\"");
                Add(max);
                Add("\"");
            }

            if (min > 0)
            {
                Add(" minlength=\"");
                Add(min);
                Add("\"");
            }

            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            Add(">");
            return this;
        }

        public HtmlBuilder DATE(string label, string name, DateTime val, DateTime max = default, DateTime min = default, bool @readonly = false, bool required = false, int step = 0)
        {
            LABEL(label);
            Add("<input type=\"date\" class=\"uk-input uk-width-1-1\" name=\"");
            Add(name);
            Add("\" value=\"");
            Add(val, 3, 0);
            Add("\"");

            if (max != default)
            {
                Add(" max=\"");
                Add(max);
                Add("\"");
            }

            if (min != default)
            {
                Add(" min=\"");
                Add(min);
                Add("\"");
            }

            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            if (step != 0)
            {
                Add(" step=\"");
                Add(step);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder DATETIME(string label, string name, DateTime val, DateTime max = default, DateTime min = default, bool @readonly = false, bool required = false, int step = 0)
        {
            LABEL(label);
            Add("<input type=\"datetime-local\" class=\"uk-input uk-width-1-1\" name=\"");
            Add(name);
            Add("\" value=\"");
            Add(val, 3, 0);
            Add("T08:00\"");

            if (max != default)
            {
                Add(" max=\"");
                Add(max);
                Add("\"");
            }

            if (min != default)
            {
                Add(" min=\"");
                Add(min);
                Add("\"");
            }

            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            if (step != 0)
            {
                Add(" step=\"");
                Add(step);
                Add("\"");
            }

            Add(">");
            return this;
        }

        public HtmlBuilder TIME()
        {
            T("</tbody>");
            return this;
        }

        public HtmlBuilder NUMBERPICK(string label, string name, short v, short max = default, short min = default, short step = 1, bool @readonly = false, bool required = false, string onchange = null, string css = null)
        {
            LABEL(label);
            Add("<div class=\"uk-inline");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }

            Add("\">");
            Add("<a class=\"uk-form-icon\" href=\"#\" uk-icon=\"icon: minus-circle; ratio: 1.5\" onclick=\"this.nextSibling.stepDown();this.nextSibling.onchange();\"></a>");
            Add("<input type=\"number\" class=\"uk-input\" name=\"");
            Add(name);
            Add("\" value=\"");
            Add(v);
            Add("\" min=\"");
            Add(min);
            Add("\" max=\"");
            Add(max);
            Add("\" step=\"");
            Add(step);
            Add("\"");
            if (onchange != null)
            {
                Add(" onchange=\"");
                Add(onchange);
                Add("\"");
            }

            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            Add(">");
            Add("<a class=\"uk-form-icon uk-form-icon-flip\" href=\"#\" uk-icon=\"icon: plus-circle; ratio: 1.5\" onclick=\"this.previousSibling.stepUp();this.previousSibling.onchange();\"></a>");
            Add("</div>");
            return this;
        }

        public HtmlBuilder NUMBER(string label, string name, int v, string tip = null, int max = 0, int min = 0, int step = 0, bool @readonly = false, bool required = false)
        {
            LABEL(label);
            Add("<input type=\"number\" class=\"uk-input\" name=\"");
            Add(name);
            Add("\" value=\"");
            Add(v);
            Add("\"");
            if (tip != null)
            {
                Add(" placeholder=\"");
                Add(tip);
                Add("\"");
            }

            Add(" min=\"");
            Add(min);
            Add("\"");
            if (max > 0)
            {
                Add(" max=\"");
                Add(max);
                Add("\"");
            }

            if (step > 0)
            {
                Add(" step=\"");
                Add(step);
                Add("\"");
            }

            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            Add(">");
            return this;
        }

        public HtmlBuilder NUMBER(string label, string name, decimal v, decimal max = decimal.MaxValue, decimal min = decimal.MinValue, decimal step = 0.00m, bool money = true, bool @readonly = false, bool required = false)
        {
            LABEL(label);
            Add("<input type=\"number\" class=\"uk-input\" name=\"");
            Add(name);
            Add("\" value=\"");
            Add(v, money);
            Add("\"");
            if (min != decimal.MinValue)
            {
                Add(" min=\"");
                Add(min);
                Add("\"");
            }

            if (max != decimal.MaxValue)
            {
                Add(" max=\"");
                Add(max);
                Add("\"");
            }

            Add(" step=\"");
            if (step > 0)
            {
                Add(step);
            }
            else
            {
                Add("any");
            }

            Add("\"");
            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            Add(">");
            return this;
        }

        public HtmlBuilder NUMBER(string label, string name, double v, double max = double.MaxValue, double min = double.MinValue, double step = 0, bool @readonly = false, bool required = false)
        {
            LABEL(label);
            Add("<input type=\"number\" class=\"uk-input\" name=\"");
            Add(name);
            Add("\" value=\"");
            Add(v);
            Add("\"");
            if (min > double.MinValue)
            {
                Add(" min=\"");
                Add(min);
                Add("\"");
            }

            if (max < double.MaxValue)
            {
                Add(" max=\"");
                Add(max);
                Add("\"");
            }

            Add(" step=\"");
            if (step > 0)
            {
                Add(step);
            }
            else
            {
                Add("any");
            }

            Add("\"");
            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            Add(">");
            return this;
        }

        public HtmlBuilder RANGE<V>(string label, string name, V v, V max, V min, V step, bool @readonly = false)
        {
            LABEL(label);
            Add("<input type=\"range\" class=\"uk-input\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddPrimitive(v);
            Add("\" max=\"");
            AddPrimitive(max);
            Add("\" min=\"");
            AddPrimitive(min);
            Add("\" step=\"");
            AddPrimitive(step);
            Add("\"");
            if (@readonly) Add(" readonly");
            Add(">");
            return this;
        }

        public HtmlBuilder COLOR(string label, string name, string v, bool @readonly = false, bool required = false)
        {
            LABEL(label);
            Add("<input type=\"color\" class=\"uk-input\" name=\"");
            Add(name);
            Add("\" value=\"");
            Add(v);
            Add("\"");
            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            Add(">");
            return this;
        }

        public HtmlBuilder CHECKBOX<V>(string label, string name, V v, bool check = false, string tip = null, bool required = false, bool disabled = false, string css = null)
        {
            LABEL(label);
            if (tip != null)
            {
                Add("<label class=\"uk-flex uk-flex-middle\">");
                if (css != null)
                {
                    Add(' ');
                    Add(css);
                }
            }
            if (tip == null)
            {
                Add("<div class=\"uk-input\">");
            }
            Add("<input type=\"checkbox\" class=\"uk-checkbox");
            if (tip == null && css != null)
            {
                Add(' ');
                Add(css);
            }
            Add("\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddPrimitive(v);
            Add("\"");
            if (check) Add(" checked=\"checked\"");
            if (required) Add("\" required=\"required\"");
            if (disabled) Add("\" disabled=\"disabled\"");
            Add(">");
            if (tip == null)
            {
                Add("</div>");
            }
            if (tip != null)
            {
                Add("&nbsp;");
                Add(tip); // caption following the checkbox
                Add("</label>");
            }

            return this;
        }


        public HtmlBuilder CHECKBOXSET(string name, string[] v, string[] opt, string legend = null, string css = null)
        {
            FIELDSUL_(legend, false, css);
            for (int i = 0; i < opt.Length; i++)
            {
                var e = opt[i];
                Add(" <label>");
                Add("<p class=\"uk-flex uk-input\"><input type=\"checkbox\" name=\"");
                Add(name);
                Add("\"");
                if (v != null && v.Contains(e))
                {
                    Add(" checked");
                }

                Add("></p>");
                Add(e);
                Add(" </label>");
            }

            _FIELDSUL();
            return this;
        }

        public HtmlBuilder RADIO<V>(string name, V v, string label = null, bool @checked = false, bool required = false, bool disabled = false, string tip = null)
        {
            Add("<label>");
            Add("<input type=\"radio\" class=\"uk-radio\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddPrimitive(v);
            Add("\"");
            if (required)
            {
                Add(" required");
            }

            if (@checked)
            {
                Add(" checked");
            }

            if (disabled)
            {
                Add(" disabled");
            }

            Add(">");
            Add(label);
            if (tip != null)
            {
                Add("&nbsp;&nbsp;");
                Add("<span class=\"uk-text-small\">");
                Add(tip);
                Add("</span>");
            }
            Add("</label>");
            return this;
        }

        public HtmlBuilder RADIO2<V>(string name, V v, string label = null, bool @checked = false, bool required = false, int idx = 0, int last = 0)
        {
            if (idx % 2 == 0)
            {
                Add("<li>");
            }

            Add("<label class=\"uk-width-1-2\">");
            Add("<input type=\"radio\" class=\"uk-radio\" name=\"");
            Add(name);
            Add("\" value=\"");
            AddPrimitive(v);
            Add("\"");
            if (required)
            {
                Add(" required");
            }

            if (@checked)
            {
                Add(" checked");
            }

            Add(">");
            Add(label);
            Add("</label>");
            if (idx == last || idx % 2 == 1)
            {
                Add("</li>");
            }

            return this;
        }

        public HtmlBuilder RADIOSET<K, V>(string name, K v, Map<K, V> opt = null, Predicate<V> filter = null, string legend = null, string css = null, bool required = false)
        {
            FIELDSUL_(legend, false, css);
            if (opt != null)
            {
                lock (opt)
                {
                    for (int i = 0; i < opt.Count; i++)
                    {
                        var e = opt.EntryAt(i);
                        if (filter != null && !filter(e.Value)) continue;
                        if (e.IsHead)
                        {
                            STATIC_(null);
                            Add(e.Value.ToString());
                            _STATIC();
                        }
                        else
                        {
                            Add("<li>");
                            Add("<label>");
                            Add("<input type=\"radio\" class=\"uk-radio\" name=\"");
                            Add(name);
                            Add("\" id=\"");
                            Add(name);
                            AddPrimitive(e.Key);
                            Add("\"");
                            Add("\" value=\"");
                            AddPrimitive(e.Key);
                            Add("\"");
                            if (e.Key.Equals(v)) Add(" checked");
                            if (required) Add(" required");
                            Add(">");
                            Add(e.Value.ToString());
                            Add("</label>");
                            Add("</li>");
                        }
                    }
                }
            }

            _FIELDSUL();
            return this;
        }

        public HtmlBuilder RADIOSET2<K, V>(string name, K v, Map<K, V> opt = null, string legend = null, string css = null, bool required = false, Func<K, V, bool> filter = null)
        {
            FIELDSUL_(legend, false, css);
            if (opt != null)
            {
                lock (opt)
                {
                    bool odd = true;
                    for (int i = 0; i < opt.Count; i++)
                    {
                        var e = opt.EntryAt(i);
                        if (filter != null && !filter(e.key, e.Value)) continue;
                        if (e.IsHead)
                        {
                            STATIC_(null);
                            Add(e.Value.ToString());
                            _STATIC();
                            odd = true;
                        }
                        else
                        {
                            if (odd)
                            {
                                Add("<li>");
                            }

                            Add("<label class=\"uk-width-expand\">");
                            Add("<input type=\"radio\" class=\"uk-radio\" name=\"");
                            Add(name);
                            Add("\" id=\"");
                            Add(name);
                            AddPrimitive(e.Key);
                            Add("\"");
                            Add("\" value=\"");
                            AddPrimitive(e.Key);
                            Add("\"");
                            if (e.Key.Equals(v)) Add(" checked");
                            if (required) Add(" required");
                            Add(">");
                            Add(e.Value.ToString());
                            Add("</label>");
                            if (!odd)
                            {
                                Add("</li>");
                            }

                            odd = !odd;
                        }
                    }
                }
            }

            _FIELDSUL();
            return this;
        }

        public HtmlBuilder RADIOSET(string name, string v, string[] opt, string legend = null, string css = null, bool required = false)
        {
            if (legend != null)
            {
                FIELDSUL_(legend, false, css);
            }

            for (int i = 0; i < opt.Length; i++)
            {
                var o = opt[i];
                if (i > 0) Add("&nbsp;&nbsp;");
                RADIO(name, o, o, o.Equals(v));
            }

            if (legend != null)
            {
                _FIELDSUL();
            }

            return this;
        }

        public HtmlBuilder TEXTAREA(string label, string name, string v, string tip = null, short max = 0, short min = 0, bool @readonly = false, bool required = false)
        {
            TEXTAREA_(label, name, tip, max, min, @readonly, required);
            Add(v);
            _TEXTAREA(label != null);
            return this;
        }

        public HtmlBuilder TEXTAREA(string label, string name, string[] v, string tip = null, short max = 0, short min = 0, bool @readonly = false, bool required = false)
        {
            TEXTAREA_(label, name, tip, max, min, @readonly, required);
            for (int i = 0; i < v?.Length; i++)
            {
                if (i > 0)
                {
                    Add(' ');
                }
                Add(v[i]);
            }
            _TEXTAREA(label != null);

            return this;
        }


        public HtmlBuilder TEXTAREA(string label, string name, JObj v, string tip = null, short max = 0, short min = 0, bool @readonly = false, bool required = false)
        {
            TEXTAREA_(label, name, tip, max, min, @readonly, required);
            var str = v?.ToString();
            Add(str);
            _TEXTAREA(label != null);
            return this;
        }

        public HtmlBuilder TEXTAREA(string label, string name, JArr v, string tip = null, short max = 0, short min = 0, bool @readonly = false, bool required = false)
        {
            TEXTAREA_(label, name, tip, max, min, @readonly, required);
            var str = v?.ToString();
            Add(str);
            _TEXTAREA(label != null);
            return this;
        }


        public HtmlBuilder TEXTAREA_(string label, string name, string tip = null, short max = 0, short min = 0, bool @readonly = false, bool required = false)
        {
            LABEL(label);
            Add("<textarea class=\"uk-textarea");
            Add("\" name=\"");
            Add(name);
            Add("\"");

            if (tip != null)
            {
                Add(" placeholder=\"");
                Add(tip);
                Add("\"");
            }

            if (max > 0)
            {
                Add(" maxlength=\"");
                Add(max);
                Add("\"");

                Add(" rows=\"");
                Add(max <= 30 ? 2 :
                    max <= 60 ? 3 :
                    max <= 100 ? 4 :
                    max <= 150 ? 5 :
                    max <= 200 ? 6 :
                    max <= 250 ? 7 :
                    max <= 300 ? 8 :
                    max <= 400 ? 10 :
                    12
                );
                Add("\"");
            }

            if (min > 0)
            {
                Add(" minlength=\"");
                Add(min);
                Add("\"");
            }

            if (@readonly) Add(" readonly");
            if (required) Add(" required");
            Add(">");
            return this;
        }

        public HtmlBuilder _TEXTAREA(bool label = true)
        {
            Add("</textarea>");
            return this;
        }

        public HtmlBuilder SELECT_<V>(V name, string cookie = null, string onfix = null, bool rtl = false, string onchange = null, string css = null)
        {
            Add("<select class=\"uk-select");

            if (rtl)
            {
                Add(" uk-select-right");
            }

            if (css != null)
            {
                Add(' ');
                Add(css);
            }
            Add("\" name=\"");
            AddPrimitive(name);

            if (cookie != null)
            {
                Add("\" cookie=\"");
                Add(cookie);
            }
            if (onfix != null)
            {
                Add("\" onfix=\"");
                Add(onfix);
            }

            if (onchange != null)
            {
                Add("\" onchange=\"");
                Add(onchange);
            }

            Add("\">");
            return this;
        }


        public HtmlBuilder SELECT_<V>(string label, V name, bool multiple = false, bool required = true, int size = 0, bool rtl = false, bool refresh = false, string css = null)
        {
            LABEL(label);
            Add("<select class=\"uk-select");
            if (rtl)
            {
                Add(" uk-select-right");
            }

            if (css != null)
            {
                Add(' ');
                Add(css);
            }

            Add("\" name=\"");
            AddPrimitive(name);
            Add("\"");
            if (multiple) Add(" multiple");
            if (required)
            {
                Add(" value=\"\" required");
            }
            if (size > 0)
            {
                Add(" size=\"");
                Add(size);
                Add("\"");
            }

            if (refresh)
            {
                Add(" onchange=\"formRefresh(this, event);\"");
            }

            Add(">");
            if (!required)
            {
                Add("<option style=\"display:none\" selected></option>");
            }
            return this;
        }

        public HtmlBuilder _SELECT()
        {
            Add("</select>");
            return this;
        }

        public HtmlBuilder OPTION<T>(T v, string caption = null, bool selected = false, bool enabled = true)
        {
            Add("<option value=\"");
            AddPrimitive(v);

            Add("\"");
            if (selected) Add(" selected");
            if (!enabled) Add(" disabled");
            Add(">");
            if (caption != null)
            {
                Add(caption);
            }
            else
            {
                AddPrimitive(v);
            }

            Add("</option>");
            return this;
        }

        public HtmlBuilder OPTION_<T>(T v, bool selected = false, bool enabled = true)
        {
            Add("<option value=\"");
            PutKey(v);
            Add("\"");
            if (selected) Add(" selected");
            if (!enabled) Add(" disabled");
            Add(">");
            return this;
        }

        public HtmlBuilder _OPTION()
        {
            Add("</option>");
            return this;
        }

        public HtmlBuilder OPTGROUP_<T>(T v)
        {
            Add("<optgroup label=\"");
            AddPrimitive(v);
            Add("\">");
            return this;
        }

        public HtmlBuilder _OPTGROUP()
        {
            Add("</optgroup>");
            return this;
        }

        public HtmlBuilder SELECT<K, V>(string label, string name, K v, Map<K, V> opts, Func<K, V, bool> filter = null, bool tip = false, bool required = false, sbyte size = 0, bool rtl = false, bool refresh = false)
        {
            SELECT_(label, name, false, required, size, rtl, refresh);
            if (opts != null)
            {
                bool grpopen = false;
                for (int i = 0; i < opts.Count; i++)
                {
                    var ety = opts.EntryAt(i);
                    if (filter != null && !filter(ety.key, ety.Value))
                    {
                        continue;
                    }

                    var strv = ety.Value.ToString();

                    if (ety.IsHead)
                    {
                        if (grpopen)
                        {
                            Add("</optgroup>");
                            grpopen = false;
                        }

                        Add("<optgroup label=\"");
                        AddPrimitive(strv);
                        Add("\">");
                        grpopen = true;
                    }
                    else
                    {
                        if (!required && !refresh && i == 0)
                        {
                            Add("<option value=\"\"></option>");
                        }
                        var key = ety.Key;
                        Add("<option value=\"");
                        AddPrimitive(key);
                        Add("\"");
                        if (key.Equals(v)) Add(" selected");
                        Add(">");
                        AddPrimitive(strv);
                        Add("</option>");
                    }
                }

                if (grpopen)
                {
                    Add("</optgroup>");
                    grpopen = false;
                }
            }
            _SELECT();
            return this;
        }

        public HtmlBuilder SELECT<K, V>(string label, string name, K[] vs, Map<K, V> opts, Func<K, V, bool> filter = null, Func<V, string> capt = null, bool required = true, sbyte size = 0)
        {
            SELECT_(label, name, true, required, size);
            if (opts != null)
            {
                for (int i = 0; i < opts.Count; i++)
                {
                    if (!required && i == 0)
                    {
                        Add("<option value=\"\"></option>");
                    }
                    var e = opts.EntryAt(i);
                    var key = e.Key;
                    var val = e.value;
                    if (filter != null && !filter(key, val)) continue;
                    Add("<option value=\"");
                    if (key is short shortk)
                    {
                        Add(shortk);
                    }
                    else if (key is int intk)
                    {
                        Add(intk);
                    }
                    else if (key is string strk)
                    {
                        Add(strk);
                    }

                    var strv = capt?.Invoke(val) ?? val.ToString();

                    Add("\"");
                    if (vs.Contains(key)) Add(" selected");
                    Add(">");
                    AddPrimitive(strv);
                    Add("</option>");
                }
            }
            _SELECT();
            return this;
        }

        public HtmlBuilder SELECT<V>(string label, string name, V v, V[] opts, bool required = true, sbyte size = 0)
        {
            SELECT_(label, name, false, required, size);
            if (opts != null)
            {
                for (int i = 0; i < opts.Length; i++)
                {
                    if (!required && i == 0)
                    {
                        Add("<option value=\"\"></option>");
                    }
                    var e = opts[i];
                    Add("<option value=\"");
                    AddPrimitive(e);
                    Add("\"");
                    if (e.Equals(v)) Add(" selected");
                    Add(">");
                    AddPrimitive(e);
                    Add("</option>");
                }
            }
            _SELECT();
            return this;
        }

        public HtmlBuilder SELECT(string label, string name, string[] vs, string[] opts, bool required = false, sbyte size = 0, bool refresh = false)
        {
            SELECT_(label, name, true, required, size, refresh);
            if (opts != null)
            {
                for (int i = 0; i < opts.Length; i++)
                {
                    if (!required && i == 0)
                    {
                        Add("<option value=\"\"></option>");
                    }
                    var e = opts[i];
                    Add("<option value=\"");
                    Add(e);
                    Add("\"");
                    if (vs.Contains(e)) Add(" selected");
                    Add(">");
                    Add(e);
                    Add("</option>");
                }
            }
            _SELECT();
            return this;
        }

        public HtmlBuilder DATALIST(string id, string[] opt)
        {
            Add("<datalist");
            Add(" id=\"datalist-");
            Add(id);
            Add("\">");
            for (int i = 0; i < opt?.Length; i++)
            {
                string v = opt[i];
                Add("<option value=\"");
                Add(v);
                Add("\">");
                Add(v);
                Add("</option>");
            }

            Add("</datalist>");
            return this;
        }

        public HtmlBuilder OUTPUT<V>(string name, V v, string cookie = null, string onfix = null, string css = null)
        {
            Add("<output");
            if (css != null)
            {
                Add(" class=\"");
                Add(css);
                Add("\"");
            }
            Add(" name=\"");
            Add(name);

            if (cookie != null)
            {
                Add("\" cookie=\"");
                Add(cookie);
            }
            if (onfix != null)
            {
                Add("\" onfix=\"");
                Add(onfix);
            }
            Add("\">");
            AddPrimitive(v);
            Add("</output>");
            return this;
        }

        public HtmlBuilder CNYOUTPUT(string name, decimal v, string cookie = null, string onfix = null, string css = null)
        {
            Add("<output class=\"rmb");
            if (css != null)
            {
                Add(' ');
                Add(css);
            }
            Add("\" name=\"");
            Add(name);

            if (cookie != null)
            {
                Add("\" cookie=\"");
                Add(cookie);
            }
            if (onfix != null)
            {
                Add("\" onfix=\"");
                Add(onfix);
            }
            Add("\">");

            Add(v, true);

            Add("</output>");

            return this;
        }

        public HtmlBuilder METER()
        {
            Add("<meter>");
            Add("</meter>");
            return this;
        }

        public HtmlBuilder CROP(string name, string caption, short width, short height)
        {
            Add("<a class=\"uk-button uk-button-default uk-margin-small-bottom\" onclick=\"document.getElementById(\'imginp\').click()\">");
            Add(caption);
            Add("</a>");
            Add("<div id=\"imgbnd\" style=\"height: ");
            Add(height + 16);
            Add("px\">");
            Add("<input type=\"file\" id=\"imginp\" style=\"display: none;\" name=\"");
            Add(name);
            Add("\" onchange=\"bind(this.parentNode, window.URL.createObjectURL(this.files[0]),");
            Add(width);
            Add(',');
            Add(height);
            Add(");\"></div>");
            return this;
        }
    }
}