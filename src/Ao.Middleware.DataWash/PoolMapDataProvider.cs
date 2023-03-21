using Collections.Pooled;

namespace Ao.Middleware.DataWash
{
    public class PoolMapDataProvider<TKey, TValue> : PooledDictionary<TKey, TValue>, IDataProvider<TKey, TValue>
    {
        public PoolMapDataProvider()
        {
        }

        public PoolMapDataProvider(INamedInfo? name)
        {
            Name = name;
        }

        public PoolMapDataProvider(int capacity, INamedInfo? name) 
            : base(capacity)
        {
            Name = name;
        }

        public INamedInfo? Name { get; }
    }
}
