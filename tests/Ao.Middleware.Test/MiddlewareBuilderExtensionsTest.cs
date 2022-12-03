using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Ao.Middleware.Test
{
    [TestClass]
    public class MiddlewareBuilderExtensionsTest
    {
        class NullMiddleware<TContext> : IMiddleware<TContext>
        {
            public Task InvokeAsync(TContext context, Handler<TContext> next)
            {
                throw new NotImplementedException();
            }
        }
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var builder = new MiddlewareBuilder<int>();

            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(null, (Handler<int> _) => Task.CompletedTask));
            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(null, new NullMiddleware<int>()));
            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(null, (Func<Handler<int>, Handler<int>>)(_ => _)));
            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(null, (Func<int, Handler<int>, Task>)((_, __) => Task.CompletedTask)));
            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(null, (Func<int, IMiddleware<int>>)(_ => new NullMiddleware<int>())));
            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(null, (Action<int>)(_ => { })));

            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(builder, (Handler<int>?)null));
            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(builder, (NullMiddleware<int>?)null));
            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(builder, (Func<Handler<int>, Handler<int>>?)null));
            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(builder, (Func<int, Handler<int>, Task>?)null));
            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(builder, (Func<int, IMiddleware<int>>?)null));
            Assert.ThrowsException<ArgumentNullException>(() => MiddlewareBuilderExtensions.Use(builder, (Action<int>?)null));
        }
        class Sum
        {
            public int Count { get; set; }
        }

        private static async Task<int> Run(Action<MiddlewareBuilder<Sum>> action,int count)
        {
            var builder = new MiddlewareBuilder<Sum>();
            for (int i = 0; i < count; i++)
            {
                action(builder);
            }
            var handler = builder.Build();
            var ctx = new Sum();
            await handler(ctx);
            return ctx.Count;
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public async Task UseByAction(int count)
        {
            Assert.AreEqual(count, await Run(x => x.Use(d => d.Count++), count));
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public async Task UseByHandler(int count)
        {
            Assert.AreEqual(count, await Run(x => x.Use(new Handler<Sum>(s =>
            {
                s.Count++;
                return Task.CompletedTask;
            })), count));
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public async Task UseByAsyncLambda(int count)
        {
            Assert.AreEqual(count, await Run(x => x.Use(new Func<Sum, Handler<Sum>, Task>((s,next) =>
            {
                s.Count++;
                return next(s);
            })), count));
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public async Task UseByRaw(int count)
        {
            Assert.AreEqual(count, await Run(x => x.Use(new Func<Handler<Sum>, Handler<Sum>>((origin) =>
            {
                return new Handler<Sum>((ctx) =>
                {
                    ctx.Count++;
                    return origin(ctx);
                });
            })), count));
        }
        class AddOneMiddleware : IMiddleware<Sum>
        {
            public Task InvokeAsync(Sum context, Handler<Sum> next)
            {
                context.Count++;
                return next(context);
            }
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public async Task UseByMiddleware(int count)
        {
            Assert.AreEqual(count, await Run(x => x.Use(new AddOneMiddleware()), count));
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public async Task UseByMiddlewareFunc(int count)
        {
            Assert.AreEqual(count, await Run(x => x.Use(_ => new AddOneMiddleware()), count));
        }
    }
}
