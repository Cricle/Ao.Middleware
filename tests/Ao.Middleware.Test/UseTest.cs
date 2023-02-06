namespace Ao.Middleware.Test
{
    [TestClass]
    public class UseTest
    {
        class Sum
        {
            public int A { get; set; }
        }
        [TestMethod]
        public async Task CanBreak()
        {
            var builder = new MiddlewareBuilder<Sum>();
            for (int i = 0; i < 100; i++)
            {
                builder.Use((x, next) =>
                {
                    if (x.A < 10)
                    {
                        x.A++;
                        return next(x);
                    }
                    return Task.CompletedTask;
                });
            }
            var handler = builder.Build();
            var s = new Sum();
            await handler(s);
            Assert.AreEqual(10, s.A);
        }
    }
}
