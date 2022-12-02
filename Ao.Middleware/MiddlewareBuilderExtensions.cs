using System;

namespace Ao.Middleware
{
    public static class MiddlewareBuilderExtensions
    {
        public static void Use<TContext>(this IMiddlewareBuilder<TContext> builder, Handler<TContext> action)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            builder.Use((next) => new Handler<TContext>(new DirectMiddleware<TContext>(action, next).InvokeAsync));
        }

        public static IMiddlewareBuilder<TContext> Use<TContext>(this IMiddlewareBuilder<TContext> builder, Func<Handler<TContext>, Handler<TContext>> func)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            builder.Handlers.Add(func);
            return builder;
        }

        public static void Use<TContext>(this IMiddlewareBuilder<TContext> builder, IMiddleware<TContext> middleware)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            builder.Use((next) => (context) => middleware.InvokeAsync(context, next));
        }

        public static void Use<TContext>(this IMiddlewareBuilder<TContext> builder, Func<TContext, IMiddleware<TContext>> middlewareTypeGetter)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middlewareTypeGetter is null)
            {
                throw new ArgumentNullException(nameof(middlewareTypeGetter));
            }

            builder.Use((context) => middlewareTypeGetter(context));
        }
    }
}
