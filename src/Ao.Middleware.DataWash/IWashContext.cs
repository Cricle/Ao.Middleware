using System;
using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public interface IWashContext<TKey, TValue, TOutput> : IReadOnlyDictionary<TKey, TValue>, IDisposable
    {
        IList<IDataProvider<TKey, TValue>> DataProviders { get; }

        IList<IColumnOutput<TKey, TOutput>> Outputs { get; }
    }
}
