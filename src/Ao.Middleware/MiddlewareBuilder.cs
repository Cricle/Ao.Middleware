using System;
using System.Collections.Generic;

namespace Ao.Middleware
{
    public class MiddlewareBuilder<TContext> : IMiddlewareBuilder<TContext>
    {
        public MiddlewareBuilder()
        {
            Handlers = new List<Func<Handler<TContext>, Handler<TContext>>>();
        }

        public MiddlewareBuilder(IList<Func<Handler<TContext>, Handler<TContext>>> handlers)
        {
            Handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        }

        public IList<Func<Handler<TContext>, Handler<TContext>>> Handlers { get; }
    }
}
