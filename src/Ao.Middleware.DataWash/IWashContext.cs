using System;
using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public static class WashContextAnyExtensions
    {
        public static void AddOutput<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context,IColumnOutput<TKey,TOutput> output)
        {
            context.Outputs.Add(output);
        }
        public static void AddOutput<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context, TKey key,TOutput output)
        {
            AddOutput(context, new DefaultColumnOutput<TKey, TOutput>(key, output));
        }
    }
    public interface IWashContext<TKey, TValue, TOutput> : IReadOnlyDictionary<TKey, TValue>, IDisposable
    {
        IList<IDataProvider<TKey, TValue>> DataProviders { get; }

        IList<IColumnOutput<TKey, TOutput>> Outputs { get; }
    }
}
