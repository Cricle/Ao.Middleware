using System;
using System.Collections.Generic;

namespace Ao.Middleware
{
    public class SyncMiddlewareBuilder<TContext> : ISyncMiddlewareBuilder<TContext>
    {
        public SyncMiddlewareBuilder()
        {
            Handlers = new List<Func<SyncHandler<TContext>, SyncHandler<TContext>>>();
        }

        public SyncMiddlewareBuilder(IList<Func<SyncHandler<TContext>, SyncHandler<TContext>>> handlers)
        {
            Handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        }

        public IList<Func<SyncHandler<TContext>, SyncHandler<TContext>>> Handlers { get; }
    }
}
