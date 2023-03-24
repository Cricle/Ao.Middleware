using Collections.Pooled;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Ao.Middleware.DataWash
{
    public class WashContext<TKey, TValue, TOutput> : DataProviderGroup<TKey, TValue>, IWashContext<TKey, TValue, TOutput>
    {
#pragma warning disable CS8618
        public WashContext(CancellationToken token = default)
#pragma warning restore CS8618
        {
            disposables = new PooledList<IDisposable>();
            outputs = new DataProviderGroup<TKey, TOutput>();
            Token = token;
        }
        private readonly PooledList<IDisposable> disposables;
        private readonly DataProviderGroup<TKey, TOutput> outputs;

        public CancellationToken Token { get; }

        public IList<IDisposable> Disposables => disposables;

        public IDataProviderCollection<TKey, TValue> Inputs => this;

        public IDataProviderCollection<TKey, TOutput> Outputs => outputs;

        public new void Dispose()
        {
            base.Dispose();
            foreach (var item in disposables)
            {
                item.Dispose();
            }
            outputs.Dispose();
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
