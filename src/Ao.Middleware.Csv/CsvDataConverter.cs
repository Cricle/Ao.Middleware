using Ao.Middleware.DataWash;

namespace Ao.Middleware.Csv
{
    public class CsvDataConverter : CsvDataConverter<string, object>
    {
        public static readonly CsvDataConverter Instance = new CsvDataConverter();

        private CsvDataConverter() 
            : base(EmptyDataConverter<string>.Instance, EmptyDataConverter<object>.Instance)
        {
        }
    }
    public class CsvDataConverter<TKey, TValue> : ICsvDataConverter<TKey, TValue>
    {
        public CsvDataConverter(IDataConverter<string, TKey> keyConverter, IDataConverter<object, TValue> valueConverter)
        {
            KeyConverter = keyConverter ?? throw new ArgumentNullException(nameof(keyConverter));
            ValueConverter = valueConverter ?? throw new ArgumentNullException(nameof(valueConverter));
        }

        public IDataConverter<string, TKey> KeyConverter { get; }

        public IDataConverter<object, TValue> ValueConverter { get; }
    }
}
