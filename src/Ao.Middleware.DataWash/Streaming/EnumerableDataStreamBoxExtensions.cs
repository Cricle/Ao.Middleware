using Ao.Middleware.DataWash.Streaming;
using System;
using System.Collections.Generic;
namespace Ao.Middleware.DataWash
{
    public static class EnumerableDataStreamBoxExtensions
    {
        public static IEnumerable<DataStreamBox<TKey, TValue>> EnumerableStreamBox<TKey, TValue>(this IDataProvider<TKey, TValue> dataProvider, INamedInfo? named)
        {
            foreach (var item in dataProvider)
            {
                yield return new DataStreamBox<TKey, TValue>(named, item.Key, item.Value);
            }
        }
        public static IEnumerable<DataStreamBox<TKey, TValue>> EnumerableStreamBox<TKey, TValue>(this IDataProviderCollection<TKey, TValue> datasProvider, Func<IDataProvider<TKey, TValue>, bool>? condition = null)
        {
            foreach (var item in datasProvider.DatasProviders)
            {
                foreach (var s in EnumerableStreamBox(item,condition))
                {
                    yield return s;
                }
            }
        }

        public static IEnumerable<DataStreamBox<TKey, TValue>> EnumerableStreamBox<TKey, TValue>(this IDatasProvider<TKey, TValue> datasProvider, Func<IDataProvider<TKey, TValue>, bool>? condition = null)
        {
            var v = datasProvider.Read();
            if (v == null)
            {
                yield break;
            }
            while (v != null)
            {
                if (condition != null && condition(v))
                {
                    foreach (var item in EnumerableStreamBox(v, v.Name))
                    {
                        yield return item;
                    }
                }
                v = datasProvider.Read();
            }
        }
    }
}
