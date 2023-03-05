namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class DelegateWashUnitTest
    {
        [TestMethod]
        public async Task InitWithAction()
        {
            var a = 0;
            await new DelegateWashUnit<WashContext<string>, string, object, object>(_ => a++).WashAsync(null);
        }
        [TestMethod]
        public async Task InitWithAsync()
        {
            var a = 0;
            await new DelegateWashUnit<WashContext<string>, string, object, object>(_ =>
            {
                a++;
                return Task.CompletedTask;
            }).WashAsync(null);
        }
        [TestMethod]
        public async Task InitWithTokenAsync()
        {
            var a = 0;
            await new DelegateWashUnit<WashContext<string>, string, object, object>((_, tk) =>
            {
                a++;
                return Task.CompletedTask;
            }).WashAsync(null);
        }
        [TestMethod]
        public async Task InitWithNull_ThrowException()
        {
            Func<WashContext<string>, CancellationToken, Task>? h = null;
            Assert.ThrowsException<ArgumentNullException>(() => new DelegateWashUnit<WashContext<string>, string, object, object>(h));
        }
    }
}
