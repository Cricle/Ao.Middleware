using System;
using System.Collections.Generic;
using System.Threading;

namespace Ao.Middleware.DataWash
{
    public interface IWashContext<TInput, TOutput> : IColumnOutputCollection<TOutput>, IDisposable
    {
        TInput Inputs { get; }

        CancellationToken Token { get; }

        IList<IDisposable> Disposables { get; }
    }
    public interface IWashContext<TKey, TValue, TOutput> : IWashContext<IDataProviderCollection<TKey, TValue>, IDataProviderCollection<TKey, TOutput>>
    {
    }
}
