using System;

namespace Ao.Middleware
{
    public static class MiddlewareBuilderExtensions
    {
        public static void Use<TContext>(this IMiddlewareBuilder<TContext> builder, Handler<TContext> action)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            builder.Use((Handler<TContext> next) => new Handler<TContext>(new DirectMiddleware<TContext>(action, next).InvokeAsync));
        }

        public static IMiddlewareBuilder<TContext> Use<TContext>(this IMiddlewareBuilder<TContext> builder, Func<Handler<TContext>, Handler<TContext>> func)
        {
            builder.Handlers.Add(func);
            return builder;
        }

        public static void Use<TContext>(this IMiddlewareBuilder<TContext> builder, IMiddleware<TContext> middleware)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            if (middleware == null)
            {
                throw new ArgumentNullException("middleware");
            }
            builder.Use((Handler<TContext> next) => (MiddlewareContext<TContext> context) => middleware.InvokeAsync(context, next));
        }

        public static void Use<TContext, TMiddleware>(this IMiddlewareBuilder<TContext> builder) where TMiddleware : class, IMiddleware<TContext>, new()
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            TMiddleware middleware = Activator.CreateInstance<TMiddleware>();
            builder.Use((Handler<TContext> next) => (MiddlewareContext<TContext> context) => middleware.InvokeAsync(context, next));
        }

        public static void Use<TContext>(this IMiddlewareBuilder<TContext> builder, Func<TContext, IMiddleware<TContext>> middlewareTypeGetter)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            if (middlewareTypeGetter == null)
            {
                throw new ArgumentNullException("middlewareTypeGetter");
            }
            builder.Use((TContext context) => middlewareTypeGetter(context));
        }
    }
}
