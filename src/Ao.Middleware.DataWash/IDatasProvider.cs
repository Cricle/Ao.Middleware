using System;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash
{
    public interface IDatasProvider<TKey, TValue> : INamedObject, IDisposable
    {
        Task LoadAsync();

        IDataProvider<TKey, TValue>? Read();

        bool Reset();
    }
}
