using Collections.Pooled;
using System;
using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public class WashContext<TKey> : WashContext<TKey, object, object>
    {
        public WashContext()
        {
        }

        public WashContext(IEnumerable<IDataProvider<TKey, object>> collection) : base(collection)
        {
        }
    }
    public class WashContext<TKey, TValue, TOutput> : DataProviderGroup<TKey, TValue>, IWashContext<TKey, TValue, TOutput>, IWithMapDataProviderWashContext<TKey, TValue>
    {
        public WashContext()
        {
            outputs = new PooledList<IColumnOutput<TKey, TOutput>>();
            mapData = new PoolMapDataProvider<TKey, TValue>();
            Add(mapData);
        }

        public WashContext(IEnumerable<IDataProvider<TKey, TValue>> collection)
            : base(collection)
        {
            outputs = new PooledList<IColumnOutput<TKey, TOutput>>();
            mapData = new PoolMapDataProvider<TKey, TValue>();
            Add(mapData);
        }

        private readonly PooledList<IColumnOutput<TKey, TOutput>> outputs;
        private readonly PoolMapDataProvider<TKey, TValue> mapData;

        public IList<IDataProvider<TKey, TValue>> DataProviders => this;

        public IList<IColumnOutput<TKey, TOutput>> Outputs => outputs;

        public IDictionary<TKey, TValue> MapData => mapData;

        public void Dispose()
        {
            outputs.Dispose();
            mapData.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
