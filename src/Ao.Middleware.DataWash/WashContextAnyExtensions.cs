using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

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
        public static IEnumerable<IDataProvider<TKey,TValue>> GetInputProvider<TKey, TValue>(this IList<IDatasProvider<TKey, TValue>> context, INamedInfo? name, TKey? key)
        {
            var query = context.AsEnumerable();
            if (name!=null)
            {
                query = query.Where(x => x.Name == name);
            }
            var ret = query.SelectMany(x => x);
            if (key!=null)
            {
                return ret;
            }
            return ret.Where(x => x.ContainsKey(key!));
        }
        public static bool TryGet<TKey, TValue>(this IList<IDatasProvider<TKey, TValue>> context, INamedInfo? name, TKey key, out TValue? value)
        {
            var query = GetInputProvider(context, name, key).FirstOrDefault();
            if (query != null)
            {
                return query.TryGetValue(key, out value);
            }
            value = default;
            return false;
        }
        public static bool TryGetInput<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, INamedInfo? name, TKey key, out TValue? value)
        {
            return TryGet(context.Inputs.DatasProviders, name, key, out value);
        }
        public static bool AddOutput<TKey, TValue>(this IList<IDatasProvider<TKey, TValue>> context,INamedInfo? name, TKey key, TValue output)
        {
            name ??= DefaultNamedInfo.Instance;
            var query = GetInputProvider(context, name, key).FirstOrDefault();
            if (query != null)
            {
                query[key] = output;
                return true;
            }
        }
        public static void AddOutput<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, TKey key, TOutput output)
        {
            context.Outputs.MapData.Add(key, output);
        }
    }
}
