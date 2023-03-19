using System;
using System.Runtime.CompilerServices;

namespace Ao.Middleware
{
    public static class SyncMiddlewareBuilderExtensions
    {

        public static SyncHandler<TContext> Build<TContext>(this ISyncMiddlewareBuilder<TContext> builder)
        {
            SyncHandler<TContext> handler = EndPoint;
            for (int i = builder.Handlers.Count - 1; i >= 0; i--)
            {
                handler = builder.Handlers[i](handler);
            }
            return handler;
        }
        [MethodImpl((MethodImplOptions)256)]
        private static void EndPoint<TContext>(TContext context)
        {
        }

        public static ISyncMiddlewareBuilder<TContext> Use<TContext>(this ISyncMiddlewareBuilder<TContext> builder, SyncHandler<TContext> action)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            builder.Use((next) => new SyncDirectMiddleware<TContext>(action, next).Invoke);
            return builder;
        }
        public static ISyncMiddlewareBuilder<TContext> Use<TContext>(this ISyncMiddlewareBuilder<TContext> builder, Action<TContext, SyncHandler<TContext>> func)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            return builder.Use((next) => new SyncHandler<TContext>((ctx) => func(ctx, next)));
        }
        public static ISyncMiddlewareBuilder<TContext> Use<TContext>(this ISyncMiddlewareBuilder<TContext> builder, Func<SyncHandler<TContext>, SyncHandler<TContext>> func)
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

        public static ISyncMiddlewareBuilder<TContext> Use<TContext>(this ISyncMiddlewareBuilder<TContext> builder, ISyncMiddleware<TContext> middleware)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            builder.Use((next) => (context) => middleware.Invoke(context, next));
            return builder;
        }

        public static ISyncMiddlewareBuilder<TContext> Use<TContext>(this ISyncMiddlewareBuilder<TContext> builder, Func<TContext, ISyncMiddleware<TContext>> middlewareTypeGetter)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middlewareTypeGetter is null)
            {
                throw new ArgumentNullException(nameof(middlewareTypeGetter));
            }
            builder.Use((SyncHandler<TContext> handler) => new SyncHandler<TContext>(ctx => middlewareTypeGetter(ctx).Invoke(ctx, handler)));
            return builder;
        }
    }
}
