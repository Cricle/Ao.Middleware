﻿using Collections.Pooled;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.Middleware.DataWash
{
    public class DataProviderGroup<TKey, TValue> : PooledList<IDataProvider<TKey, TValue>>, IDataProvider<TKey, TValue>, IDataProviderCollection<TKey, TValue>
    {
        public DataProviderGroup()
        {
        }

        public DataProviderGroup(INamedInfo? name)
        {
            Name = name;
        }

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

        public IList<IDataProvider<TKey, TValue>> DataProviders => this;

        public virtual IList<IDatasProvider<TKey, TValue>> DatasProviders => Array.Empty<IDatasProvider<TKey, TValue>>();

        protected virtual IEnumerable<IDatasProvider<TKey, TValue>> GetDatasProviders()
        {
            yield break;
        }

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
        }
    }
}
