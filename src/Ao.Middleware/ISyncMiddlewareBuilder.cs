using System;
using System.Collections.Generic;

namespace Ao.Middleware
{
    public interface ISyncMiddlewareBuilder<TContext>
    {
        IList<Func<SyncHandler<TContext>, SyncHandler<TContext>>> Handlers { get; }
    }
}
