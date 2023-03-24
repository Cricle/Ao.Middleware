namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class WashContextAnyExtensionsTest
    {
        [TestMethod]
        public void GetOrDefault()
        {
            var ctx = new WashContext<int, object, object>();
            ctx.MapData[1] = 123;
            Assert.IsNull(ctx.GetInputOrDefault(new NamedInfo("aaa"), 1));
            Assert.AreEqual(123, ctx.GetInputOrDefault(null, 1));
        }
        [TestMethod]
        public void TryGet_Nothing()
        {
            var ctx = new WashContext<int, object, object>();
            var res = ctx.TryGetInput(new NamedInfo("aaa"), 1, out var val);
            Assert.IsFalse(res);
            Assert.IsNull(val);
        }
        [TestMethod]
        public void TryGet_Named()
        {
            var ctx = new WashContext<int, object, object>();
            ctx.DataProviders.Add(new PoolMapDataProvider<int, object>(new NamedInfo("aaa"))
            {
                [1] = 123
            });
            var res = ctx.TryGetInput(new NamedInfo("aaa"), 1, out var val);
            Assert.IsTrue(res);
            Assert.AreEqual(123, val);
        }
        [TestMethod]
        public void TryGet_NoNamed()
        {
            var ctx = new WashContext<int,object,object>();
            ctx.MapData[1] = 123;
            var res = ctx.TryGetInput(null, 1, out var val);
            Assert.IsTrue(res);
            Assert.AreEqual(123, val);
        }
        [TestMethod]
        public void AddOutput()
        {
            var ctx = new WashContext<int, object, object>();
            ctx.AddOutput(1, 1);
            Assert.AreEqual(1, ctx.Outputs.MapData.Count);
            Assert.AreEqual(1, ctx.Outputs.MapData[1]);
        }
    }
}
