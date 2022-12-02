using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.Middleware
{
    public class MiddlewareBuilder<TContext> : IMiddlewareBuilder<TContext>
    {
        public MiddlewareBuilder()
        {
            this.Handlers = new List<Func<Handler<TContext>, Handler<TContext>>>();
        }

        public IList<Func<Handler<TContext>, Handler<TContext>>> Handlers { get; }

        public virtual Handler<TContext> Build()
        {
            Handler<TContext> handler = CreateEndPoint();
            foreach (var func in this.Handlers.Reverse())
            {
                handler = func(handler);
            }
            return handler;
        }

        public Handler<TContext> CreateEndPoint()
        {
            return (MiddlewareContext<TContext> context) => new NullEndPoint<TContext>().InvokeAsync(context, EmptyHandler);
        }

        private readonly Handler<TContext> EmptyHandler = (MiddlewareContext<TContext> _) => ComplatedTasks.ComplatedTask;
    }
}
