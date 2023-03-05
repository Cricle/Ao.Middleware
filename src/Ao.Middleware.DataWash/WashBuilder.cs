using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash
{
    public delegate Task WashHandler<TContext, TKey, TValue, TOutput>(TContext context, CancellationToken token = default)
           where TContext : IWashContext<TKey, TValue, TOutput>;
    public class WashBuilder<TContext, TKey> : WashBuilder<TContext, TKey, object, object>
           where TContext : IWashContext<TKey, object, object>
    {
    }
    public class WashBuilder<TContext, TKey, TValue, TOutput> : List<IWashUnit<TContext, TKey, TValue, TOutput>>, IWashBuilder<TContext, TKey, TValue, TOutput>
           where TContext : IWashContext<TKey, TValue, TOutput>
    {
        public WashHandler<TContext, TKey, TValue, TOutput> Build()
        {
            if (Count == 0)
            {
                return EmptyWashAsync;
            }
            WashHandler<TContext, TKey, TValue, TOutput> first = this[0].WashAsync;
            var ret = first;
            for (int i = 1; i < Count; i++)
            {
                first += this[i].WashAsync;
            }
            return first;

        }

        private static Task EmptyWashAsync(TContext context, CancellationToken token = default)
        {
            return Task.CompletedTask;
        }
    }
}
