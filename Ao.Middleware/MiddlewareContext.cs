using System;

namespace Ao.Middleware
{
    public class MiddlewareContext<TContext>
    {
        public MiddlewareContext(IServiceProvider serviceProvider, TContext context)
        {
            this.ServiceProvider = serviceProvider;
            this.Context = context;
        }

        public IServiceProvider ServiceProvider { get; }

        public TContext Context { get; }
    }
}
