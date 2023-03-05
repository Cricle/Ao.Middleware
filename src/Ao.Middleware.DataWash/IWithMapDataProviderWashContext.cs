using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public interface IWithMapDataProviderWashContext<TKey, TValue>
    {
        IDictionary<TKey, TValue> MapData { get; }
    }
}
