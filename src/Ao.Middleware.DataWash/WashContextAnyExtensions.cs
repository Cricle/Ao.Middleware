using System.Linq;

namespace Ao.Middleware.DataWash
{
    public static class WashContextAnyExtensions
    {
        public static TValue? GetOrDefault<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, string? name, TKey key)
        {
            if (TryGet(context,name,key,out var val))
            {
                return val;
            }
            return default;
        }
        public static bool TryGet<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, string? name,TKey key,out TValue? value)
        {
            if (name == null && context is IWithMapDataProviderWashContext<TKey, TValue> wc)
            {
                return wc.MapData.TryGetValue(key, out value);
            }
            var provider = context.DataProviders.FirstOrDefault(x => x.Name == name);
            if (provider==null)
            {
                value=default;
                return false;
            }
            return provider.TryGetValue(key, out value);
        }
        public static void AddOutput<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, IColumnOutput<TKey, TOutput> output)
        {
            context.Outputs.Add(output);
        }
        public static void AddOutput<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, TKey key, TOutput output)
        {
            AddOutput(context, new DefaultColumnOutput<TKey, TOutput>(key, output));
        }
    }
}
