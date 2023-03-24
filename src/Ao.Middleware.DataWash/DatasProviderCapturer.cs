using System;
using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public class DatasProviderCapturer<TKey, TValue> : IDatasProviderCapturer<TKey, TValue>
    {
        public DatasProviderCapturer(bool capture)
        {
            Capture = capture;
            captureDatasProvider = capture ? new DefaultDatasProvider<TKey, TValue>() : null;
        }

        protected readonly DefaultDatasProvider<TKey, TValue>? captureDatasProvider;

        public bool Capture { get; }

        public IDatasProvider<TKey, TValue>? CaptureDatasProvider => captureDatasProvider;

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
        public virtual IDataProvider<TKey, TValue>? AddCaptureDataProvider(IEnumerable<TKey> keys,IEnumerable<TValue> values)
        {
            var m = CastCaptureDataProvider(keys, values);
            return AddCaptureDataProvider(m);
        }
        public virtual IDataProvider<TKey, TValue>? AddCaptureDataProvider(IDataProvider<TKey, TValue> dataProvider)
        {
            if (captureDatasProvider==null)
            {
                return null;
            }
            captureDatasProvider.Add(dataProvider);
            return dataProvider;
        }

        public void Dispose()
        {
            captureDatasProvider?.Dispose(); 
            OnDispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDispose()
        {

        }
    }
}
