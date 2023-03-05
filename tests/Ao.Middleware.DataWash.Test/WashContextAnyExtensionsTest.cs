namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class WashContextAnyExtensionsTest
    {
        [TestMethod]
        public void AddOutput()
        {
            var ctx = new WashContext<int>();
            ctx.AddOutput(1, 1);
            Assert.AreEqual(1, ctx.Outputs.Count);
            Assert.AreEqual(1, ctx.Outputs[0].Key);
            Assert.AreEqual(1, ctx.Outputs[0].Output);
            Assert.IsInstanceOfType(ctx.Outputs[0], typeof(DefaultColumnOutput<int, object>));
        }
    }
}
