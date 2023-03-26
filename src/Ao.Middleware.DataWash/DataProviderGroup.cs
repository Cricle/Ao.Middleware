using Collections.Pooled;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.Middleware.DataWash
{
    public class DataProviderGroup<TKey, TValue> : PooledList<IDataProvider<TKey, TValue>>, IDataProvider<TKey, TValue>, IDataProviderCollection<TKey, TValue>, IDatasProvider<TKey,TValue>, IDisposable
    {
        public DataProviderGroup()
            : this(null)
        {
        }

        public DataProviderGroup(INamedInfo? name)
        {
            Name = name;
            datasProviders = new PooledList<IDatasProvider<TKey, TValue>>();
        }
        protected readonly PooledList<IDatasProvider<TKey, TValue>> datasProviders;

        public TValue this[TKey key]
        {
            get
            {
                if (TryGetValue(key, out var val))
                {
                    return val;
                }
                throw new KeyNotFoundException(key?.ToString());
            }
        }

        public virtual INamedInfo? Name { get; }

        public IEnumerable<TKey> Keys =>
            Enumerable.SelectMany<IDataProvider<TKey, TValue>, TKey>(this, x => x.Keys);

        public IEnumerable<TValue> Values =>
            Enumerable.SelectMany<IDataProvider<TKey, TValue>, TValue>(this, x => x.Values);

        public IList<IDatasProvider<TKey, TValue>> DatasProviders => datasProviders;

        public bool ContainsKey(TKey key)
        {
            return Enumerable.Any<IDataProvider<TKey, TValue>>(this, x => x.ContainsKey(key));
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            foreach (var item in this)
            {
                if (item.TryGetValue(key, out value))
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

        public new void Dispose()
        {
            foreach (var item in this)
            {
                if (item is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            foreach (var item in datasProviders)
            {
                item.Dispose();
            }
            datasProviders.Dispose(); 
        }
    }
}
