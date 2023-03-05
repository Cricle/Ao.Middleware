using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class DataProviderGroupTest
    {
        private DataProviderGroup<string,object> Datas()
        {
           return new DataProviderGroup<string, object>
            {
                new PoolMapDataProvider<string, object>
                {
                    ["a1"]=1
                },
                new PoolMapDataProvider<string, object>
                {
                    ["a2"]=2
                },
            };
        }
        [TestMethod]
        public void Name()
        {
            var group = new DataProviderGroup<string, object>("aaa");
            Assert.AreEqual("aaa", group.Name);
        }
        [TestMethod]
        public void Keys()
        {
            var group = Datas();
            var keys = group.Keys;
            Assert.AreEqual(2, keys.Count());
            Assert.AreEqual("a1", keys.ElementAt(0));
            Assert.AreEqual("a2", keys.ElementAt(1));
        }
        [TestMethod]
        public void Values()
        {
            var group = Datas();
            var keys = group.Values;
            Assert.AreEqual(2, keys.Count());
            Assert.AreEqual(1, keys.ElementAt(0));
            Assert.AreEqual(2, keys.ElementAt(1));
        }
        [TestMethod]
        public void ThisGet()
        {
            var group = Datas();
            Assert.AreEqual(1, group["a1"]);
        }
        [TestMethod]
        public void ThisGet_NotFound()
        {
            var group = Datas();
            Assert.ThrowsException<KeyNotFoundException>(() => group["aa"]);
        }
        [TestMethod]
        public void ContainsKey()
        {
            var group = Datas();
            Assert.IsTrue(group.ContainsKey("a1"));
            Assert.IsFalse(group.ContainsKey("a11"));
        }
        [TestMethod]
        public void TryGetValue()
        {
            var group = Datas();
            var ok = group.TryGetValue("a1", out var v);
            Assert.IsTrue(ok);
            Assert.AreEqual(1, v);

            ok = group.TryGetValue("a11", out v);
            Assert.IsFalse(ok);
            Assert.IsNull(v);
        }
        [TestMethod]
        public void Enumerable()
        {
            var group = ((IEnumerable<KeyValuePair<string, object>>)Datas()).GetEnumerator();
            Assert.IsTrue(group.MoveNext());
            Assert.AreEqual("a1", group.Current.Key);
            Assert.AreEqual(1, group.Current.Value);
            Assert.IsTrue(group.MoveNext());
            Assert.AreEqual("a2", group.Current.Key);
            Assert.AreEqual(2, group.Current.Value);
            Assert.IsFalse(group.MoveNext());
        }
    }
}
