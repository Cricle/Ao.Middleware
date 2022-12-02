using System;
using System.Collections.Generic;

namespace Ao.Middleware
{
    public interface IMiddlewareBuilder<TContext>
    {
        IList<Func<Handler<TContext>, Handler<TContext>>> Handlers { get; }

        Handler<TContext> Build();

        Handler<TContext> CreateEndPoint();
    }
}
