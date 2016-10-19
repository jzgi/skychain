using System;

namespace Greatbone.Core
{

    ///
    /// <summary>
    /// A helper used to generate SQL commands.
    /// </summary>
    ///
    public class DbSql : Text, ISink<DbSql>
    {

        const int InitialCapacity = 1024;

        public const int

            X_SEL = 0x800000,

            X_UPD = 0x400000,

            X_INS = 0x200000;

        // contexts
        const sbyte ValueList = 0, ColumnList = 1, ParameterList = 2, SetList = 3;

        // the putting context
        internal sbyte ctx;

        // used when generating a list
        internal int ordinal;


        public DbSql(string str) : base(InitialCapacity)
        {
            Add(str);
        }

        internal void Clear()
        {
            count = 0;
            ctx = 0;
            ordinal = 0;
        }

        public DbSql _(string str)
        {
            Add(' ');
            Add(str);

            return this;
        }

        public DbSql setlst<T>(T obj, uint x = 0) where T : IPersist
        {
            ctx = SetList;
            ordinal = 1;
            obj.Save(this, x);
            return this;
        }

        public DbSql columnlst<T>(T obj, uint x = 0) where T : IPersist
        {
            ctx = ColumnList;
            ordinal = 1;
            obj.Save(this, x);
            return this;
        }

        public DbSql paramlst<T>(T obj, uint x = 0) where T : IPersist
        {
            ctx = ParameterList;
            ordinal = 1;
            obj.Save(this, x);
            return this;
        }

        public DbSql valuelst<T>(T obj, uint x = 0) where T : IPersist
        {
            ctx = ValueList;
            ordinal = 1;
            obj.Save(this, x);
            return this;
        }

        public DbSql _<T>(T obj, uint x = 0) where T : IPersist
        {
            Add(" (");
            columnlst(obj, x);
            Add(")");
            return this;
        }

        public DbSql _VALUES_<T>(T obj, uint x = 0) where T : IPersist
        {
            Add(" VALUES (");
            valuelst(obj, x);
            Add(")");
            return this;
        }

        public DbSql _SET_<T>(T obj, uint x = 0) where T : IPersist
        {
            Add(" SET ");
            setlst(obj, x);
            return this;
        }

        void Build(string name)
        {
            if (ordinal > 1) Add(", ");

            switch (ctx)
            {
                case ColumnList: Add(name); break;
                case ParameterList: Add("@"); Add(name); break;
                case SetList: Add(name); Add("=@"); Add(name); break;
            }

            ordinal++;
        }

        public DbSql Put(string name, bool v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                Add(v ? "TRUE" : "FALSE");
            }
            return this;
        }

        public DbSql Put(string name, short v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                Add(v);
            }
            return this;
        }

        public DbSql Put(string name, int v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                Add(v);
            }
            return this;
        }

        public DbSql Put(string name, long v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                Add(v);
            }
            return this;
        }

        public DbSql Put(string name, Number v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                Add(v);
            }
            return this;
        }

        public DbSql Put(string name, DateTime v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                Add(v);
            }
            return this;
        }

        public DbSql Put(string name, decimal v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                Add(v);
            }
            return this;
        }

        public DbSql Put(string name, string v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                Add('\''); Add(v); Add('\'');
            }
            return this;
        }

        public DbSql Put(string name, char[] v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                Add('\''); Add(v); Add('\'');
            }
            return this;
        }

        public DbSql Put(string name, byte[] v)
        {
            Build(name);
            return this;
        }

        public DbSql Put(string name, ArraySegment<byte> v)
        {
            Build(name);
            return this;
        }

        public DbSql Put<V>(string name, V v, uint x = 0) where V : IPersist
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                if (v == null)
                {
                    Add("NULL");
                }
                else
                {
                }
            }
            return this;
        }

        public DbSql Put(string name, JArr v)
        {
            Build(name);
            return this;
        }

        public DbSql Put(string name, JObj v)
        {
            Build(name);
            return this;
        }

        public DbSql Put(string name, short[] v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                if (v == null)
                {
                    Add("NULL");
                }
                else
                {
                    Add("ARRAY[");
                    for (int i = 0; i < v.Length; i++)
                    {
                        if (i > 0) Add(',');
                        Add(v[i]);
                    }
                    Add(']');
                    if (v.Length == 0)
                    {
                        Add("::smallint[]");
                    }
                }
            }
            return this;
        }

        public DbSql Put(string name, int[] v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                if (v == null)
                {
                    Add("NULL");
                }
                else
                {
                    Add("ARRAY[");
                    for (int i = 0; i < v.Length; i++)
                    {
                        if (i > 0) Add(',');
                        Add(v[i]);
                    }
                    Add(']');
                    if (v.Length == 0)
                    {
                        Add("::integer[]");
                    }
                }
            }
            return this;
        }

        public DbSql Put(string name, string[] v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                if (v == null)
                {
                    Add("NULL");
                }
                else
                {
                    Add("ARRAY[");
                    for (int i = 0; i < v.Length; i++)
                    {
                        if (i > 0) Add(',');
                        Add('\'');
                        Add(v[i]);
                        Add('\'');
                    }
                    Add(']');
                    if (v.Length == 0)
                    {
                        Add("::varchar[]");
                    }
                }
            }
            return this;
        }

        public DbSql Put(string name, long[] v)
        {
            if (name != null)
            {
                Build(name);
            }
            else
            {
                if (v == null)
                {
                    Add("NULL");
                }
                else
                {
                    Add("ARRAY[");
                    for (int i = 0; i < v.Length; i++)
                    {
                        if (i > 0) Add(',');
                        Add(v[i]);
                    }
                    Add(']');
                    if (v.Length == 0)
                    {
                        Add("::bigint[]");
                    }
                }
            }
            return this;
        }

        public DbSql Put<V>(string name, V[] v, uint x = 0) where V : IPersist
        {
            Build(name);
            return this;
        }

        public DbSql PutNull(string name)
        {
            Build(name);
            return this;
        }

    }

}