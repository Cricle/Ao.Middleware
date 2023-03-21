namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class PoolMapDataProviderTest
    {
        [TestMethod]
        public void Names()
        {
            var provider = new PoolMapDataProvider<string, object>(new NamedInfo("aaa"));
            Assert.AreEqual(new NamedInfo("aaa"), provider.Name);
            provider = new PoolMapDataProvider<string, object>();
            Assert.IsNull(provider.Name);
        }
    }
}
