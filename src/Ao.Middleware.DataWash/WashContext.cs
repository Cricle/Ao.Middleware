using Collections.Pooled;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Ao.Middleware.DataWash
{
    public class WashContext<TKey> : WashContext<TKey, object, IList<IColumnOutput<TKey, object>>>
    {
        private readonly PooledList<IColumnOutput<TKey, object>> outputs;
        public WashContext(CancellationToken token = default)
            : base(token)
        {
            Outputs= outputs = new PooledList<IColumnOutput<TKey, object>>();
        }
        protected override void OnDispose()
        {
            outputs.Dispose();
        }
    }
    public class WashContext<TKey, TValue, TOutput> : DataProviderGroup<TKey, TValue>, IWashContext<TKey, TValue, TOutput>, IWithMapDataProviderWashContext<TKey, TValue>
    {
        public WashContext(CancellationToken token = default)
        {
            mapData = new PoolMapDataProvider<TKey, TValue>();
            Token = token;
            Add(mapData);
        }

        private readonly PoolMapDataProvider<TKey, TValue> mapData;

        public TOutput? Outputs { get; protected set; }

        public IDictionary<TKey, TValue> MapData => mapData;

        public CancellationToken Token { get; }

        public new void Dispose()
        {
            base.Dispose();
            mapData.Dispose();
            OnDispose();
            GC.SuppressFinalize(this);
        }
        protected virtual void OnDispose()
        {

        }
    }
}
