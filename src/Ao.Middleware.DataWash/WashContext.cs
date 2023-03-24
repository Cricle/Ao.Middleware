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
            Outputs = outputs = new PooledList<IColumnOutput<TKey, object>>();
        }
        protected override void OnDispose()
        {
            outputs.Dispose();
        }
    }
    public class WashContext<TKey, TValue, TOutput> : DataProviderGroup<TKey, TValue>, IWashContext<TKey, TValue, TOutput>, IWithMapDataProviderWashContext<TKey, TValue>
    {
#pragma warning disable CS8618
        public WashContext(CancellationToken token = default)
#pragma warning restore CS8618
        {
            mapData = new PoolMapDataProvider<TKey, TValue>();
            datasProviders = new PooledList<IDatasProvider<TKey, TValue>>();
            disposables = new PooledList<IDisposable>();
            Token = token;
            Add(mapData);
        }
        private readonly PooledList<IDatasProvider<TKey, TValue>> datasProviders;
        private readonly PoolMapDataProvider<TKey, TValue> mapData;
        private readonly PooledList<IDisposable> disposables;

        public virtual TOutput Outputs { get; protected set; }

        public IDictionary<TKey, TValue> MapData => mapData;

        public override IList<IDatasProvider<TKey, TValue>> DatasProviders => datasProviders;

        public CancellationToken Token { get; }

        public IList<IDisposable> Disposables => disposables;

        public new void Dispose()
        {
            base.Dispose();
            foreach (var item in disposables)
            {
                item.Dispose();
            }
            mapData.Dispose();
            datasProviders.Dispose();
            disposables.Dispose();
            OnDispose();
            GC.SuppressFinalize(this);
        }
        protected virtual void OnDispose()
        {

        }
    }
}
