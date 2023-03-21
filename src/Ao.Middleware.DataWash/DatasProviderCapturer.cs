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

        public IDatasProvider<TKey, TValue>? CaptureDatasProvider { get; }

        public virtual bool AddCaptureDataProvider(IEnumerable<TKey> keys,IEnumerable<TValue> values)
        {
            var m=new PoolMapDataProvider<TKey, TValue>();
            using (var enuKey=keys.GetEnumerator())
            using(var enuValue=values.GetEnumerator())
            {
                while (enuKey.MoveNext()&&enuValue.MoveNext())
                {
                    m[enuKey.Current] = enuValue.Current;
                }
            }
            return AddCaptureDataProvider(m);
        }
        public virtual bool AddCaptureDataProvider(IDataProvider<TKey, TValue> dataProvider)
        {
            if (captureDatasProvider==null)
            {
                return false;
            }
            captureDatasProvider.Add(dataProvider);
            return true;
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
