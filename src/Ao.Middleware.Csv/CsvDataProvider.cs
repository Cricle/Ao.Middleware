using Ao.Middleware.DataWash;
using CsvHelper;

namespace Ao.Middleware.Csv
{
    public class CsvDataProvider<TKey, TValue> : DatasProviderCapturer<TKey, TValue>, IDatasProvider<TKey, TValue>
    {
        public CsvDataProvider(IStringObjectDataConverter<TKey, TValue> dataConverter,IReader csvReader, INamedInfo? name, bool capture)
            : base(name,capture)
        {
            CsvReader = csvReader;
            DataConverter = dataConverter;
        }

        public IReader CsvReader { get; }

        public IStringObjectDataConverter<TKey, TValue> DataConverter { get; }          

        protected override void OnDispose()
        {
            CsvReader.Dispose();
        }
        
        protected override IDataProvider<TKey, TValue>? OnRead()
        {
            if (CsvReader.Read())
            {
                if (CsvReader.HeaderRecord == null&& CsvReader.ReadHeader())
                {
                    CsvReader.Read();
                }
                return CastCaptureDataProvider(CsvReader.HeaderRecord!.Select(x => DataConverter.KeyConverter.Convert(x)),
                        Enumerable.Range(0, CsvReader.HeaderRecord!.Length).Select(x => DataConverter.ValueConverter.Convert(CsvReader[x])));
            }
            return null;
        }
    }
}
