﻿using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public interface IDataProviderCollection<TKey, TValue>
    {
        IDictionary<TKey, TValue> MapData { get; }

        IList<IDataProvider<TKey, TValue>> DataProviders { get; }

        IList<IDatasProvider<TKey, TValue>> DatasProviders { get; }
    }
}
