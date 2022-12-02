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
            Handlers = handlers;
        }

        public IList<Func<Handler<TContext>, Handler<TContext>>> Handlers { get; }

        public virtual Handler<TContext> Build()
        {
            var handler = CreateEndPoint();
            for (int i = Handlers.Count - 1; i >= 0; i--)
            {
                handler = Handlers[i](handler);
            }
            return handler;
        }

        public Handler<TContext> CreateEndPoint()
        {
            return _ => ComplatedTasks.ComplatedTask;
        }
    }
}
