using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public interface IDataProvider<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        string? Name { get; }
    }
}
