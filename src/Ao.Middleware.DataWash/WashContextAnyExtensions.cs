using System.Linq;

namespace Ao.Middleware.DataWash
{
    public static class WashContextAnyExtensions
    {
        public static TValue? GetInputOrDefault<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, INamedInfo? name, TKey key)
        {
            if (TryGetInput(context, name, key, out var val))
            {
                return val;
            }
            return default;
        }
        public static bool TryGetInput<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, INamedInfo? name, TKey key, out TValue? value)
        {
            if (name == null)
            {
                return context.Inputs.MapData.TryGetValue(key, out value);
            }
            var provider = context.Inputs.DataProviders.FirstOrDefault(x => Equals(x.Name, name));
            if (provider == null)
            {
                value = default;
                return false;
            }
            return provider.TryGetValue(key, out value);
        }
        public static void AddOutput<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, TKey key, TOutput output)
        {
            context.Outputs.MapData.Add(key, output);
        }
    }
}
