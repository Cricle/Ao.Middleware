using System;

namespace Ao.Middleware.DataWash
{
    public interface IDatasProvider<TKey, TValue> : INamedObject, IDisposable
    {
        IDataProvider<TKey, TValue>? Read();

        bool Reset();
    }
}
