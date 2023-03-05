using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Ao.Middleware.DataWash
{
    public class DataProviderGroup<TKey, TValue> : List<IDataProvider<TKey, TValue>>, IDataProvider<TKey, TValue>
    {
        public DataProviderGroup()
        {
        }

        public DataProviderGroup(IEnumerable<IDataProvider<TKey, TValue>> collection) : base(collection)
        {
        }

        public TValue this[TKey key]
        {
            get
            {
                if (TryGetValue(key,out var val))
                {
                    return val;
                }
                throw new KeyNotFoundException(key?.ToString());
            }
        }

        public virtual string? Name { get; }

        public IEnumerable<TKey> Keys =>
            Enumerable.SelectMany<IDataProvider<TKey, TValue>,TKey>(this, x => x.Keys);

        public IEnumerable<TValue> Values =>
            Enumerable.SelectMany<IDataProvider<TKey, TValue>, TValue>(this, x => x.Values);

        public bool ContainsKey(TKey key)
        {
            return Enumerable.Any<IDataProvider<TKey, TValue>>(this,x=>x.ContainsKey(key));
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            foreach (var item in this)
            {
                if (item.TryGetValue(key,out value))
                {
                    return true;
                }
            }
            value = default;
            return false;
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
           return Enumerable.SelectMany<IDataProvider<TKey, TValue>, KeyValuePair<TKey, TValue>>(this, x => x).GetEnumerator();
        }
    }
}
