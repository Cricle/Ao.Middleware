namespace Ao.Middleware.DataWash
{
    public class StringObjectDataConverter : StringObjectDataConverter<string, object?>
    {
        public static readonly StringObjectDataConverter Instance = new StringObjectDataConverter();

        private StringObjectDataConverter()
            : base(EmptyDataConverter<string>.Instance, EmptyDataConverter<object?>.Instance, EmptyDataConverter<string>.Instance, EmptyDataConverter<object?>.Instance)
        {
        }
    }
    public class StringObjectDataConverter<TKey, TValue> : IStringObjectDataConverter<TKey, TValue>
    {
        public StringObjectDataConverter(IDataConverter<string, TKey> keyConverter, IDataConverter<object?, TValue> valueConverter, IDataConverter<TKey, string> stringConverter, IDataConverter<TValue, object?> objectConverter)
        {
            KeyConverter = keyConverter;
            ValueConverter = valueConverter;
            StringConverter = stringConverter;
            ObjectConverter = objectConverter;
        }

        public IDataConverter<string, TKey> KeyConverter { get; }

        public IDataConverter<object?, TValue> ValueConverter { get; }

        public IDataConverter<TKey, string> StringConverter { get; }

        public IDataConverter<TValue, object?> ObjectConverter { get; }
    }
}
