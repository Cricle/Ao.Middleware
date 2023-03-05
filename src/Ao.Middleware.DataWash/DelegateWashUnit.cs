using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash
{
    public class DelegateWashUnit<TContext, TKey, TValue, TOutput> : IWashUnit<TContext, TKey, TValue, TOutput>
        where TContext : IWashContext<TKey, TValue, TOutput>
    {
        public DelegateWashUnit(Action<TContext> action)
            : this((ctx, tk) =>
            {
                action(ctx);
                return Task.CompletedTask;
            })
        {
        }
        public DelegateWashUnit(Func<TContext, Task> func)
            : this((ctx, tk) => func(ctx))
        {
        }
        public DelegateWashUnit(Func<TContext, CancellationToken, Task> func)
        {
            Func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public Func<TContext, CancellationToken, Task> Func { get; }

        public Task WashAsync(TContext context, CancellationToken token = default)
        {
            return Func(context, token);
        }
    }
}
