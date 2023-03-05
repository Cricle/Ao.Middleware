using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class PoolMapDataProviderTest
    {
        [TestMethod]
        public void Names()
        {
            var provider = new PoolMapDataProvider<string, object>("aaa");
            Assert.AreEqual("aaa", provider.Name);
            provider = new PoolMapDataProvider<string, object>();
            Assert.IsNull(provider.Name);
        }
    }
}
