using Collections.Pooled;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Ao.Middleware.DataWash
{
    public class WashContext<TKey, TValue, TOutput> : IWashContext<TKey, TValue, TOutput>
    {
        public WashContext(CancellationToken token = default)
        {
            disposables = new PooledList<IDisposable>();
            outputs = new DataProviderGroup<TKey, TOutput>();
            inputs = new DataProviderGroup<TKey, TValue>();
            Token = token;
            disposables.Add(inputs);
            disposables.Add(outputs);
        }
        private readonly PooledList<IDisposable> disposables;
        private readonly DataProviderGroup<TKey, TOutput> outputs;
        private readonly DataProviderGroup<TKey, TValue> inputs;

        public CancellationToken Token { get; }

        public IList<IDisposable> Disposables => disposables;

        public IDataProviderCollection<TKey, TValue> Inputs => inputs;

        public IDataProviderCollection<TKey, TOutput> Outputs => outputs;

        public void Dispose()
        {
            foreach (var item in disposables)
            {
                item.Dispose();
            }
            disposables.Dispose();
            OnDispose();
            GC.SuppressFinalize(this);
        }
        protected virtual void OnDispose()
        {

        }
    }
}
