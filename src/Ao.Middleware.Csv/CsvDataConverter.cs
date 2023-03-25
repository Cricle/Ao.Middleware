using Ao.Middleware.DataWash;

namespace Ao.Middleware.Csv
{
    public class CsvDataConverter : CsvDataConverter<string, object?>
    {
        public static readonly CsvDataConverter Instance = new CsvDataConverter();

        private CsvDataConverter() 
            : base(EmptyDataConverter<string>.Instance, EmptyDataConverter<object?>.Instance, EmptyDataConverter<string>.Instance, EmptyDataConverter<object?>.Instance)
        {
        }
    }
    public class CsvDataConverter<TKey, TValue> : ICsvDataConverter<TKey, TValue>
    {
        public CsvDataConverter(IDataConverter<string, TKey> keyConverter, IDataConverter<object?, TValue> valueConverter, IDataConverter<TKey, string> stringConverter, IDataConverter<TValue, object?> objectConverter)
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
