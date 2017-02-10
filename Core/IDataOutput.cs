﻿using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace Greatbone.Core
{
    ///
    /// Represents a sink for data output.
    ///
    public interface IDataOutput<out R> where R : IDataOutput<R>
    {
        R PutNull(string name);

        R PutRaw(string name, string raw);

        R Put(string name, bool v);

        R Put(string name, short v);

        R Put(string name, int v);

        R Put(string name, long v);

        R Put(string name, double v);

        R Put(string name, decimal v);

        R Put(string name, JNumber v);

        R Put(string name, DateTime v);

        R Put(string name, NpgsqlPoint v);

        R Put(string name, char[] v);

        R Put(string name, string v, Ui<short>? ctl = null);

        R Put(string name, byte[] v);

        R Put(string name, ArraySegment<byte> v);

        R Put(string name, IDataInput v);

        R Put(string name, short[] v);

        R Put(string name, int[] v);

        R Put(string name, long[] v);

        R Put(string name, string[] v);

        R Put(string name, Dictionary<string, string> v);

        R Put(string name, IData v, ushort proj = 0);

        R Put<D>(string name, D[] v, ushort proj = 0) where D : IData;

        R Put<D>(string name, List<D> v, ushort proj = 0) where D : IData;
    }
}