using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash
{
    public abstract class DatasProviderCapturer<TKey, TValue> : IDatasProviderCapturer<TKey, TValue>, IDatasProvider<TKey, TValue>
    {
        public DatasProviderCapturer(INamedInfo? name,bool capture)
        {
            Name = name;
            Capture = capture;
            captureDatasProvider = capture ? new DataProviderGroup<TKey, TValue>() : null;
        }

        protected readonly DataProviderGroup<TKey, TValue>? captureDatasProvider;
        protected IDatasProvider<TKey, TValue>? current;

        public bool Capture { get; }

        public IDatasProvider<TKey, TValue>? CaptureDatasProvider => captureDatasProvider;

        public INamedInfo? Name { get; }

        protected void DisposeCurrent()
        {
            if (current is IDisposable disposable)
            {
                disposable.Dispose();
                current = null;
            }
        }
        public virtual IDataProvider<TKey, TValue>? CastCaptureDataProvider(IEnumerable<TKey> keys, IEnumerable<TValue> values)
        {
            var m = new PoolMapDataProvider<TKey, TValue>();
            using (var enuKey = keys.GetEnumerator())
            using (var enuValue = values.GetEnumerator())
            {
                while (enuKey.MoveNext() && enuValue.MoveNext())
                {
                    m[enuKey.Current] = enuValue.Current;
                }
            }
            return m;
        }
        public virtual IDataProvider<TKey, TValue>? AddCaptureDataProvider(IEnumerable<TKey> keys, IEnumerable<TValue> values)
        {
            var m = CastCaptureDataProvider(keys, values);
            return AddCaptureDataProvider(m);
        }
        public virtual IDataProvider<TKey, TValue>? AddCaptureDataProvider(IDataProvider<TKey, TValue> dataProvider)
        {
            if (captureDatasProvider == null)
            {
                return null;
            }
            captureDatasProvider.Add(dataProvider);
            return dataProvider;
        }

        public void Dispose()
        {
            captureDatasProvider?.Dispose();
            DisposeCurrent();
            OnDispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDispose()
        {

        }

        public virtual Task LoadAsync()
        {
            while (Read() != null) ;
            return Task.CompletedTask;
        }

        public IDataProvider<TKey, TValue>? Read()
        {
            if (captureDatasProvider == null)
            {
                DisposeCurrent();
            }
            var res = OnRead();
            if (res != null&& captureDatasProvider != null)
            {
                return AddCaptureDataProvider(res);
            }
            return res;
        }

        protected abstract IDataProvider<TKey, TValue>? OnRead();

        public virtual bool Reset()
        {
            DisposeCurrent();
            return true;
        }
    }
}
