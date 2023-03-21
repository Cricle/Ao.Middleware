using System;
using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public interface IColumnOutput<TKey, TOutput> : IEquatable<IColumnOutput<TKey, TOutput>>
    {
        TKey Key { get; }

        TOutput Output { get; }
    }
}
