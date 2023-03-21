namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class WashContextAnyExtensionsTest
    {
        [TestMethod]
        public void GetOrDefault()
        {
            var ctx = new WashContext<int>();
            ctx.MapData[1] = 123;
            Assert.IsNull(ctx.GetOrDefault(new NamedInfo("aaa"), 1));
            Assert.AreEqual(123, ctx.GetOrDefault(null, 1));
        }
        [TestMethod]
        public void TryGet_Nothing()
        {
            var ctx = new WashContext<int>();
            var res = ctx.TryGet(new NamedInfo("aaa"), 1, out var val);
            Assert.IsFalse(res);
            Assert.IsNull(val);
        }
        [TestMethod]
        public void TryGet_Named()
        {
            var ctx = new WashContext<int>();
            ctx.DataProviders.Add(new PoolMapDataProvider<int, object>(new NamedInfo("aaa"))
            {
                [1] = 123
            });
            var res = ctx.TryGet(new NamedInfo("aaa"), 1, out var val);
            Assert.IsTrue(res);
            Assert.AreEqual(123, val);
        }
        [TestMethod]
        public void TryGet_NoNamed()
        {
            var ctx = new WashContext<int>();
            ctx.MapData[1] = 123;
            var res = ctx.TryGet(null, 1, out var val);
            Assert.IsTrue(res);
            Assert.AreEqual(123, val);
        }
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
