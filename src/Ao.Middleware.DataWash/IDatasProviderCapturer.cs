using System;

namespace Ao.Middleware.DataWash
{
    public interface IDatasProviderCapturer<TKey, TValue> : IDisposable
    {
        bool Capture { get; }

        IDatasProvider<TKey, TValue>? CaptureDatasProvider { get; }

        bool AddCaptureDataProvider(IDataProvider<TKey, TValue> dataProvider);
    }
}
