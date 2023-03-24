using System;
using System.Collections.Generic;
using System.Threading;

namespace Ao.Middleware.DataWash
{
    public interface IWashContext<TKey, TValue, TOutput> : IReadOnlyDictionary<TKey, TValue>, IDisposable, IDataProviderCollection<TKey, TValue>, IColumnOutputCollection<TOutput>
    {
        CancellationToken Token { get; }

        IList<IDisposable> Disposables { get; }
    }
}
