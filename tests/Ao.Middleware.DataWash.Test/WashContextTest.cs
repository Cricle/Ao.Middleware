namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class WashContextTest
    {
        [TestMethod]
        public void Init()
        {
            var ctx = new WashContext<string,object,object>();
            Assert.AreEqual(ctx, ctx.DataProviders);
            Assert.IsNotNull(ctx.Outputs);
            Assert.IsNotNull(ctx.MapData);
            ctx.Dispose();
        }
        [TestMethod]
        public void WithToken()
        {
            var source = new CancellationTokenSource();
            var ctx = new WashContext<string, object, object>(source.Token);
            Assert.AreEqual(source.Token, ctx.Token);
            ctx.Dispose();
            source.Dispose();
        }
    }
}
