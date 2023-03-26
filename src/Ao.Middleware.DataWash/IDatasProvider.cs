using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash
{
    public interface IDatasProvider<TKey, TValue> : IEnumerable<IDataProvider<TKey, TValue>>,INamedObject, IDisposable
    {
    }
}
