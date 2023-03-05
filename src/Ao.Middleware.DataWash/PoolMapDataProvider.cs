using Collections.Pooled;

namespace Ao.Middleware.DataWash
{
    public class PoolMapDataProvider<TKey, TValue> : PooledDictionary<TKey, TValue>, IDataProvider<TKey, TValue>
    {
        public PoolMapDataProvider()
        {
        }

        public PoolMapDataProvider(string? name)
        {
            Name = name;
        }

        public string? Name { get; }
    }
}
