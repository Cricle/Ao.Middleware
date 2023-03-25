using Collections.Pooled;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Ao.Middleware.DataWash
{
    public class WashContext<TKey> : WashContext<TKey, object?, object?>
    {
        public WashContext(DataProviderGroup<TKey, object?> inputs, DataProviderGroup<TKey, object?> outputs, CancellationToken token = default)
            : base(inputs, outputs, token)
        {
        }
        public WashContext(CancellationToken token = default)
            : base(token)
        {

        }

    }
    public class WashContext<TKey, TValue, TOutput> : IWashContext<TKey, TValue, TOutput>
    {
        public WashContext(DataProviderGroup<TKey, TValue> inputs, DataProviderGroup<TKey, TOutput> outputs, CancellationToken token = default)
        {
            disposables = new PooledList<IDisposable>();
            this.inputs = inputs;
            this.outputs = outputs;
            Token = token;
            disposables.Add(inputs);
            disposables.Add(outputs);
        }
        public WashContext(CancellationToken token = default)
            : this(new DataProviderGroup<TKey, TValue>(), new DataProviderGroup<TKey, TOutput>(), token)
        {
        }
        private readonly PooledList<IDisposable> disposables;
        private readonly DataProviderGroup<TKey, TOutput>? outputs;
        private readonly DataProviderGroup<TKey, TValue>? inputs;

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
