namespace Ao.Middleware.DataWash
{
    public interface IStringObjectDataConverter<TKey, TValue>
    {
        IDataConverter<string, TKey> KeyConverter { get; }

        IDataConverter<object?, TValue> ValueConverter { get; }

        IDataConverter<TKey, string> StringConverter { get; }

        IDataConverter<TValue, object?> ObjectConverter { get; }
    }
}
