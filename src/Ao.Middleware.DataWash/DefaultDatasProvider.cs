using System;
using System.Collections;
using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public class RangeDatasProvider<TKey, TValue> : IDatasProvider<TKey, TValue>
    {
        public RangeDatasProvider(IEnumerable<IDataProvider<TKey, TValue>> datas)
            :this(datas,null)
        {
        }
        public RangeDatasProvider(IEnumerable<IDataProvider<TKey, TValue>> datas, INamedInfo? name)
        {
            Datas = datas;
            Name = name;
        }

        public IEnumerable<IDataProvider<TKey, TValue>> Datas { get; }

        public INamedInfo? Name { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerator<IDataProvider<TKey, TValue>> GetEnumerator()
        {
            return Datas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
