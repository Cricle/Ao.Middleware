using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Middleware.Test
{
    [TestClass]
    public class MiddlewareBuilderTest
    {
        [TestMethod]
        public void Given_Null_Init_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MiddlewareBuilder<object>(null));
        }
        [TestMethod]
        public void InitWithOther()
        {
            var builder1= new MiddlewareBuilder<object>();
            var builder2= new MiddlewareBuilder<object>(builder1.Handlers);
            Assert.AreEqual(builder1.Handlers, builder2.Handlers);
        }
    }
}
