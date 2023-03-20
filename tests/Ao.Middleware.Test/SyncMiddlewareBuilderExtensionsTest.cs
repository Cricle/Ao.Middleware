using System.Diagnostics.CodeAnalysis;

namespace Ao.Middleware.Test
{
    [TestClass]
    public class SyncSyncMiddlewareBuilderExtensionsTest
    {
        [ExcludeFromCodeCoverage]
        class NullMiddleware<TContext> : ISyncMiddleware<TContext>
        {
            public void Invoke(TContext context, SyncHandler<TContext> next)
            {
                throw new NotImplementedException();
            }
        }
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var builder = new SyncMiddlewareBuilder<int>();

            Assert.ThrowsException<ArgumentNullException>(() => SyncMiddlewareBuilderExtensions.Use(null, (SyncHandler<int> _) => { }));
            Assert.ThrowsException<ArgumentNullException>(() => SyncMiddlewareBuilderExtensions.Use(null, new NullMiddleware<int>()));
            Assert.ThrowsException<ArgumentNullException>(() => SyncMiddlewareBuilderExtensions.Use(null, (Func<SyncHandler<int>, SyncHandler<int>>)(_ => _)));
            Assert.ThrowsException<ArgumentNullException>(() => SyncMiddlewareBuilderExtensions.Use(null, (Action<int, SyncHandler<int>>)((_, __) => { })));
            Assert.ThrowsException<ArgumentNullException>(() => SyncMiddlewareBuilderExtensions.Use(null, (Func<int, ISyncMiddleware<int>>)(_ => new NullMiddleware<int>())));

            Assert.ThrowsException<ArgumentNullException>(() => SyncMiddlewareBuilderExtensions.Use(builder, (SyncHandler<int>?)null));
            Assert.ThrowsException<ArgumentNullException>(() => SyncMiddlewareBuilderExtensions.Use(builder, (NullMiddleware<int>?)null));
            Assert.ThrowsException<ArgumentNullException>(() => SyncMiddlewareBuilderExtensions.Use(builder, (Func<SyncHandler<int>, SyncHandler<int>>?)null));
            Assert.ThrowsException<ArgumentNullException>(() => SyncMiddlewareBuilderExtensions.Use(builder, (Action<int, SyncHandler<int>>?)null));
            Assert.ThrowsException<ArgumentNullException>(() => SyncMiddlewareBuilderExtensions.Use(builder, (Func<int, ISyncMiddleware<int>>?)null));
        }
        class Sum
        {
            public int Count { get; set; }
        }

        private static int Run(Action<SyncMiddlewareBuilder<Sum>> action, int count)
        {
            var builder = new SyncMiddlewareBuilder<Sum>();
            for (int i = 0; i < count; i++)
            {
                action(builder);
            }
            var handler = builder.Build();
            var ctx = new Sum();
            handler(ctx);
            return ctx.Count;
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void UseByAction(int count)
        {
            Assert.AreEqual(count, Run(x => x.Use(d => d.Count++), count));
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void UseByHandler(int count)
        {
            Assert.AreEqual(count, Run(x => x.Use(new SyncHandler<Sum>(s =>
            {
                s.Count++;
            })), count));
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void UseByAsyncLambda(int count)
        {
            Assert.AreEqual(count, Run(x => x.Use(new Action<Sum, SyncHandler<Sum>>((s, next) =>
            {
                s.Count++;
                next(s);
            })), count));
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void UseByRaw(int count)
        {
            Assert.AreEqual(count, Run(x => x.Use(new Func<SyncHandler<Sum>, SyncHandler<Sum>>((origin) =>
            {
                return new SyncHandler<Sum>((ctx) =>
                {
                    ctx.Count++;
                    origin(ctx);
                });
            })), count));
        }
        class AddOneMiddleware : ISyncMiddleware<Sum>
        {
            public void Invoke(Sum context, SyncHandler<Sum> next)
            {
                context.Count++;
                next(context);
            }
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void UseByMiddleware(int count)
        {
            Assert.AreEqual(count, Run(x => x.Use(new AddOneMiddleware()), count));
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void UseByMiddlewareFunc(int count)
        {
            Assert.AreEqual(count, Run(x => x.Use(_ => new AddOneMiddleware()), count));
        }
    }
}
