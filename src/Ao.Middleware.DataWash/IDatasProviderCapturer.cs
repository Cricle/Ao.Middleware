using System;
using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public interface IDatasProviderCapturer<TKey, TValue> : IDisposable
    {
        bool Capture { get; }

        IDatasProvider<TKey, TValue>? CaptureDatasProvider { get; }
        
        IDataProvider<TKey, TValue>? CastCaptureDataProvider(IEnumerable<TKey> keys, IEnumerable<TValue> values);

        IDataProvider<TKey, TValue>? AddCaptureDataProvider(IDataProvider<TKey, TValue> dataProvider);
    }
}
