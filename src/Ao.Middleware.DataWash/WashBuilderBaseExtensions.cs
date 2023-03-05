using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash
{
    public static class WashBuilderBaseExtensions
    {
        public static IWashBuilder<TContext, TKey, TValue, TOutput> With<TContext, TKey, TValue, TOutput>(
            this IWashBuilder<TContext, TKey, TValue, TOutput> builder,
            IWashUnit<TContext, TKey, TValue, TOutput> unit)
             where TContext : IWashContext<TKey, TValue, TOutput>
        {
            builder.Add(unit);
            return builder;
        }
        public static IWashBuilder<TContext, TKey, TValue, TOutput> WithDelegate<TContext, TKey, TValue, TOutput>(
            this IWashBuilder<TContext, TKey, TValue, TOutput> builder,
            Action<TContext> action)
             where TContext : IWashContext<TKey, TValue, TOutput>
        {
            return builder.With(new DelegateWashUnit<TContext, TKey, TValue, TOutput>(action));
        }
        public static IWashBuilder<TContext, TKey, TValue, TOutput> WithDelegate<TContext, TKey, TValue, TOutput>(
            this IWashBuilder<TContext, TKey, TValue, TOutput> builder,
            Func<TContext, Task> func)
             where TContext : IWashContext<TKey, TValue, TOutput>
        {
            return builder.With(new DelegateWashUnit<TContext, TKey, TValue, TOutput>(func));
        }
        public static IWashBuilder<TContext, TKey, TValue, TOutput> WithDelegate<TContext, TKey, TValue, TOutput>(
            this IWashBuilder<TContext, TKey, TValue, TOutput> builder,
            Func<TContext, CancellationToken, Task> func)
             where TContext : IWashContext<TKey, TValue, TOutput>
        {
            return builder.With(new DelegateWashUnit<TContext, TKey, TValue, TOutput>(func));
        }
    }
}
