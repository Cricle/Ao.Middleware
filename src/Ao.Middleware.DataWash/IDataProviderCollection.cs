using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public interface IDataProviderCollection<TKey, TValue>
    {
        IList<IDataProvider<TKey, TValue>> DataProviders { get; }

        IList<IDatasProvider<TKey, TValue>> DatasProviders { get;}
    }
}
