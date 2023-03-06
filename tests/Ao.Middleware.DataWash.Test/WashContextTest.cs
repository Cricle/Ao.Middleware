namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class WashContextTest
    {
        [TestMethod]
        public void Init()
        {
            var ctx = new WashContext<string>();
            Assert.AreEqual(ctx, ctx.DataProviders);
            Assert.IsNotNull(ctx.Outputs);
            Assert.IsNotNull(ctx.MapData);
            ctx.Dispose();
        }
        [TestMethod]
        public void WithToken()
        {
            var source = new CancellationTokenSource();
            var ctx = new WashContext<string>(source.Token);
            Assert.AreEqual(source.Token, ctx.Token);
            ctx.Dispose();
            source.Dispose();
        }
    }
}
