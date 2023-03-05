using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class WashBuilderTest
    {
        [TestMethod]
        public async Task WithDelegate()
        {
            var builder = new WashBuilder<WashContext<string>, string>();
            builder.WithDelegate(x =>
            {
                x.MapData["a1"] = 1;
            }).WithDelegate((x) =>
            {
                x.MapData["a1"] = (int)(x.MapData["a1"]) + 1;
                return Task.CompletedTask;
            }).WithDelegate((x, y) =>
            {
                x.MapData["a1"] = (int)(x.MapData["a1"]) + 1;
                return Task.CompletedTask;
            });
            var handler= builder.Build();
            using (var ctx=new WashContext<string>())
            {
                await handler(ctx);
                Assert.AreEqual(3, ctx.MapData["a1"]);
            }
        } 
        [TestMethod]
        public async Task Empty()
        {
            var builder = new WashBuilder<WashContext<string>, string>();
            await builder.Build()(null);
        }
    }
}
