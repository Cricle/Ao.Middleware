using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
