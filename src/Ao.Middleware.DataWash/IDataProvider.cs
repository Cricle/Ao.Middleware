using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ao.Middleware.DataWash
{
    public interface IDataProvider<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, INamedObject
    {
    }
}
