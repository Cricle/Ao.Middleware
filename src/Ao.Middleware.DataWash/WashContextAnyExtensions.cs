using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ao.Middleware.DataWash
{
    public static class WashContextAnyExtensions
    {
        public static TValue? GetOrDefault<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, INamedInfo? name, TKey key)
        {
            if (TryGet(context, name, key, out var val))
            {
                return val;
            }
            return default;
        }
        public static bool TryGet<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, INamedInfo? name, TKey key, out TValue? value)
        {
            if (name == null && context is IWithMapDataProviderWashContext<TKey, TValue> wc)
            {
                return wc.MapData.TryGetValue(key, out value);
            }
            var provider = context.DataProviders.FirstOrDefault(x => Equals(x.Name,name));
            if (provider == null)
            {
                value = default;
                return false;
            }
            return provider.TryGetValue(key, out value);
        }
        public static void AddOutput<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, IColumnOutput<TKey, TValue> output)
            where TOutput:IList<IColumnOutput<TKey,TValue>>
        {
            context.Outputs.Add(output);
        }
        public static void AddOutput<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, TKey key, TValue output)
                  where TOutput : IList<IColumnOutput<TKey, TValue>>
        {
            AddOutput(context, new DefaultColumnOutput<TKey, TValue>(key, output));
        }
    }
}
