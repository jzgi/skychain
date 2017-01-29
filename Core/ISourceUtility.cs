﻿using System;
using NpgsqlTypes;

namespace Greatbone.Core
{
    public static class ISourceUtility
    {
        //
        // GET SPECIFIC
        //

        public static bool GetBool(this ISource src)
        {
            bool v = false;
            src.Get(null, ref v);
            return v;
        }

        public static short GetShort(this ISource src)
        {
            short v = 0;
            src.Get(null, ref v);
            return v;
        }

        public static int GetInt(this ISource src)
        {
            int v = 0;
            src.Get(null, ref v);
            return v;
        }

        public static long GetLong(this ISource src)
        {
            long v = 0;
            src.Get(null, ref v);
            return v;
        }

        public static decimal GetDecimal(this ISource src)
        {
            decimal v = 0;
            src.Get(null, ref v);
            return v;
        }

        public static DateTime GetDateTime(this ISource src)
        {
            DateTime v = default(DateTime);
            src.Get(null, ref v);
            return v;
        }

        public static NpgsqlPoint GetPoint(this ISource src)
        {
            NpgsqlPoint v = default(NpgsqlPoint);
            src.Get(null, ref v);
            return v;
        }

        public static char[] GetChars(this ISource src)
        {
            char[] v = null;
            src.Get(null, ref v);
            return v;
        }

        public static string GetString(this ISource src)
        {
            string v = null;
            src.Get(null, ref v);
            return v;
        }

        public static byte[] GetBytes(this ISource src)
        {
            byte[] v = null;
            src.Get(null, ref v);
            return v;
        }

        public static ArraySegment<byte> GetBytesSeg(this ISource src)
        {
            ArraySegment<byte> v;
            src.Get(null, ref v);
            return v;
        }

        public static D GetObject<D>(this ISource src, byte flags = 0) where D : IData, new()
        {
            D v = default(D);
            src.Get(null, ref v, flags);
            return v;
        }

        public static short[] GetShorts(this ISource src)
        {
            short[] v = null;
            src.Get(null, ref v);
            return v;
        }

        public static int[] GetInts(this ISource src)
        {
            int[] v = null;
            src.Get(null, ref v);
            return v;
        }

        public static long[] GetLongs(this ISource src)
        {
            long[] v = null;
            src.Get(null, ref v);
            return v;
        }


        public static string[] GetStrings(this ISource src)
        {
            string[] v = null;
            src.Get(null, ref v);
            return v;
        }

        public static D[] GetArray<D>(this ISource src, byte flags = 0) where D : IData, new()
        {
            D[] v = null;
            src.Get(null, ref v, flags);
            return v;
        }

        //
        // GET SPECIFIC BY NAME
        //

        public static bool GetBool(this ISource src, string name)
        {
            bool v = false;
            src.Get(name, ref v);
            return v;
        }

        public static short GetShort(this ISource src, string name)
        {
            short v = 0;
            src.Get(name, ref v);
            return v;
        }

        public static int GetInt(this ISource src, string name)
        {
            int v = 0;
            src.Get(name, ref v);
            return v;
        }

        public static long GetLong(this ISource src, string name)
        {
            long v = 0;
            src.Get(name, ref v);
            return v;
        }

        public static decimal GetDecimal(this ISource src, string name)
        {
            decimal v = 0;
            src.Get(name, ref v);
            return v;
        }

        public static DateTime GetDateTime(this ISource src, string name)
        {
            DateTime v = default(DateTime);
            src.Get(name, ref v);
            return v;
        }

        public static NpgsqlPoint GetPoint(this ISource src, string name)
        {
            NpgsqlPoint v = default(NpgsqlPoint);
            src.Get(name, ref v);
            return v;
        }

        public static char[] GetChars(this ISource src, string name)
        {
            char[] v = null;
            src.Get(name, ref v);
            return v;
        }

        public static string GetString(this ISource src, string name)
        {
            string v = null;
            src.Get(name, ref v);
            return v;
        }

        public static byte[] GetBytes(this ISource src, string name)
        {
            byte[] v = null;
            src.Get(name, ref v);
            return v;
        }

        public static ArraySegment<byte>? GetBytesSeg(this ISource src, string name)
        {
            ArraySegment<byte> v;
            src.Get(name, ref v);
            return v;
        }

        public static D GetObject<D>(this ISource src, string name) where D : IData, new()
        {
            D v = default(D);
            src.Get(name, ref v);
            return v;
        }

        public static short[] GetShorts(this ISource src, string name)
        {
            short[] v = null;
            src.Get(name, ref v);
            return v;
        }

        public static int[] GetInts(this ISource src, string name)
        {
            int[] v = null;
            src.Get(name, ref v);
            return v;
        }

        public static long[] GetLongs(this ISource src, string name)
        {
            long[] v = null;
            src.Get(name, ref v);
            return v;
        }

        public static string[] GetStrings(this ISource src, string name)
        {
            string[] v = null;
            src.Get(name, ref v);
            return v;
        }

        public static D[] GetArray<D>(this ISource src, string name) where D : IData, new()
        {
            D[] v = null;
            src.Get(name, ref v);
            return v;
        }
    }
}