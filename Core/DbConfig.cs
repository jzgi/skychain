﻿namespace Greatbone.Core
{
    ///
    /// DB configuration embedded in WebConfig.
    ///
    public class DbConfig : IData
    {
        public string host;

        public int port;

        public string database;

        public string username;

        public string password;

        // whether to create event-queue tables/indexes
        public bool queue;

        public void ReadData(IDataInput src, byte flags = 0)
        {
            src.Get(nameof(host), ref host);
            src.Get(nameof(port), ref port);
            src.Get(nameof(database), ref database);
            src.Get(nameof(username), ref username);
            src.Get(nameof(password), ref password);
            src.Get(nameof(queue), ref queue);
        }

        public void WriteData<R>(IDataOutput<R> snk, byte flags = 0) where R : IDataOutput<R>
        {
            snk.Put(nameof(host), host);
            snk.Put(nameof(port), port);
            snk.Put(nameof(database), database);
            snk.Put(nameof(username), username);
            snk.Put(nameof(password), password);
            snk.Put(nameof(queue), queue);
        }
    }
}