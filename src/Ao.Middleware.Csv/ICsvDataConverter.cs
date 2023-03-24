using Ao.Middleware.DataWash;

namespace Ao.Middleware.Csv
{
    public interface ICsvDataConverter<TKey,TValue>
    {
        IDataConverter<string, TKey> KeyConverter { get; }

        IDataConverter<object?, TValue> ValueConverter { get; }

    }
}
