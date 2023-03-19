namespace Ao.Middleware.Test
{
    [TestClass]
    public class SyncMiddlewareBuilderTest
    {

        [TestMethod]
        public void Sync_Given_Null_Init_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new SyncMiddlewareBuilder<object>(null));
        }
        [TestMethod]
        public void SyncInitWithOther()
        {
            var builder1 = new SyncMiddlewareBuilder<object>();
            var builder2 = new SyncMiddlewareBuilder<object>(builder1.Handlers);
            Assert.AreEqual(builder1.Handlers, builder2.Handlers);
        }
    }
}
