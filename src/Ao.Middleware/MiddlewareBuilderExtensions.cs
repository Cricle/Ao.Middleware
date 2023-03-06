using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Ao.Middleware
{
    public static class MiddlewareBuilderExtensions
    {
        public static Handler<TContext> Build<TContext>(this IMiddlewareBuilder<TContext> builder)
        {
            Handler<TContext> handler = EndPointAsync;
            for (int i = builder.Handlers.Count - 1; i >= 0; i--)
            {
                handler = builder.Handlers[i](handler);
            }
            return handler;
        }
        [MethodImpl((MethodImplOptions)256)]
        private static Task EndPointAsync<TContext>(TContext context)
        {
            return ComplatedTasks.ComplatedTask;
        }
        public static IMiddlewareBuilder<TContext> Use<TContext>(this IMiddlewareBuilder<TContext> builder, Action<TContext> action)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            builder.Use((next) => new Handler<TContext>(new DirectMiddleware<TContext>(ctx =>
            {
                action(ctx);
                return ComplatedTasks.ComplatedTask;
            }, next).InvokeAsync));
            return builder;
        }
        public static IMiddlewareBuilder<TContext> Use<TContext>(this IMiddlewareBuilder<TContext> builder, Handler<TContext> action)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            builder.Use((next) => new DirectMiddleware<TContext>(action, next).InvokeAsync);
            return builder;
        }
        public static IMiddlewareBuilder<TContext> Use<TContext>(this IMiddlewareBuilder<TContext> builder, Func<TContext, Handler<TContext>, Task> func)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return builder.Use((next) => new Handler<TContext>((ctx) => func(ctx, next)));
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

        public static IMiddlewareBuilder<TContext> Use<TContext>(this IMiddlewareBuilder<TContext> builder, IMiddleware<TContext> middleware)
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
            return builder;
        }

        public static IMiddlewareBuilder<TContext> Use<TContext>(this IMiddlewareBuilder<TContext> builder, Func<TContext, IMiddleware<TContext>> middlewareTypeGetter)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middlewareTypeGetter is null)
            {
                throw new ArgumentNullException(nameof(middlewareTypeGetter));
            }
            builder.Use((Handler<TContext> handler) => new Handler<TContext>(ctx => middlewareTypeGetter(ctx).InvokeAsync(ctx, handler)));
            return builder;
        }
    }
}
