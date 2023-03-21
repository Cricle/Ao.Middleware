using System.Data;

namespace Ao.Middleware.DataWash
{
    public interface IRecordReaderProvider<TKey, TValue> : IDatasProvider<TKey,TValue>
    {
        IDataReader Reader { get; }
    }
}
