using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Ao.Middleware.DataWash.Test
{
    [TestClass]
    public class ColumnOutputWashContextConverterTest
    {
        [ExcludeFromCodeCoverage]
        class IntColumnOutputWashContextConverter<TKey> : ColumnOutputWashContextConverter<TKey, int, string>
        {
            protected override int Convert(string output)
            {
                return int.Parse(output);
            }
        }
        [ExcludeFromCodeCoverage]
        class NoDefaultColumnOutputWashContextConverter<TKey> : ColumnOutputWashContextConverter<TKey, object, object>
        {
            protected override object Convert(object output)
            {
                return output;
            }
        }
        [ExcludeFromCodeCoverage]
        class NullWashContext<TKey, TValue, TOutput> : DataProviderGroup<TKey, TValue>, IWashContext<TKey, TValue, IList<IColumnOutput<TKey, TValue>>>
        {
            public NullWashContext()
            {
                Outputs = new List<IColumnOutput<TKey, TValue>>();
            }

            public IList<IColumnOutput<TKey, TValue>> Outputs { get; }

            public CancellationToken Token { get; }
        }
        [TestMethod]
        public void ConvertWithOuputs()
        {
            var outputs = new List<IColumnOutput<string, object>>
            {
                new DefaultColumnOutput<string, object>("a1",1),
                new DefaultColumnOutput<string, object>("a2",2),
            };
            var res = ColumnOutputWashContextConverter<string, object>.Instance.Convert(outputs);
            Assert.IsInstanceOfType(res, typeof(WashContext<string, object, IList<IColumnOutput<string,object>>>));
            var wres = (WashContext<string, object,  IList<IColumnOutput<string,object>>>)res;
            Assert.AreEqual(2, wres.MapData.Count);
            Assert.AreEqual(1, wres.MapData["a1"]);
            Assert.AreEqual(2, wres.MapData["a2"]);
        }
        [TestMethod]
        public void ConvertWithOuputsCustomer()
        {
            var outputs = new List<IColumnOutput<int, string>>
            {
                new DefaultColumnOutput<int, string>(1,"2"),
                new DefaultColumnOutput<int, string>(2,"3"),
            };
            var res = new IntColumnOutputWashContextConverter<int>().Convert(outputs);
            Assert.IsInstanceOfType(res, typeof(WashContext<int, int, IList<IColumnOutput<int, string>>>));
            var wres = (WashContext<int, int, IList<IColumnOutput<int, string>>>)res;
            Assert.AreEqual(2, wres.MapData.Count);
            Assert.AreEqual(2, wres.MapData[1]);
            Assert.AreEqual(3, wres.MapData[2]);
        }
        [TestMethod]
        public void ConvertWithNoMapDataProvider()
        {
            var outputs = new List<IColumnOutput<string, object>>
            {
                new DefaultColumnOutput<string, object>("a1",1),
                new DefaultColumnOutput<string, object>("a2",2),
            };
            var res = new NoDefaultColumnOutputWashContextConverter<string>().Convert(outputs);
            Assert.AreEqual(1, res.DataProviders.Count);
            Assert.AreEqual(1, res.DataProviders[0]["a1"]);
            Assert.AreEqual(2, res.DataProviders[0]["a2"]);
        }
    }
}
