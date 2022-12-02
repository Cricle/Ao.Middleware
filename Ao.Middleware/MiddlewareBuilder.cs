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

        public IList<Func<Handler<TContext>, Handler<TContext>>> Handlers { get; }

        public virtual Handler<TContext> Build()
        {
            var handler = CreateEndPoint();
            for (int i = Handlers.Count - 1; i >= 0; i--)
            {
                handler += Handlers[i](handler);
            }
            return handler;
        }

        public Handler<TContext> CreateEndPoint()
        {
            return (context) => new NullEndPoint<TContext>().InvokeAsync(context, EmptyHandler);
        }

        private readonly Handler<TContext> EmptyHandler = _ => ComplatedTasks.ComplatedTask;
    }
}
