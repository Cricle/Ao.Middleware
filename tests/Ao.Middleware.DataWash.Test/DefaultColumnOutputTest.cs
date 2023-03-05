namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class DefaultColumnOutputTest
    {
        [TestMethod]
        public void EqualsHashCodeString()
        {
            var a = new DefaultColumnOutput<int, int>(0, 1);
            var b = new DefaultColumnOutput<int, int>(0, 1);
            var c = new DefaultColumnOutput<int, int>(1, 1);

            Assert.IsTrue(a == b);
            Assert.IsTrue(a != c);

            Assert.IsTrue(a.Equals((object)b));
            Assert.IsFalse(a.Equals((object)c));
            Assert.IsFalse(a.Equals((object?)null));
            Assert.IsFalse(a.Equals((IColumnOutput<int, int>?)null));

            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), c.GetHashCode());

            Assert.AreEqual(a.ToString(), b.ToString());
            Assert.AreNotEqual(a.ToString(), c.ToString());
        }
    }
}
