using Ao.Middleware.DataWash;
using CsvHelper;

namespace Ao.Middleware.Csv
{
    public class CsvDataProvider : CsvDataProvider<string, object>
    {
        public CsvDataProvider(IReader csvReader, INamedInfo? name, bool capture) : base(csvReader, name, capture)
        {
        }

        public override string ToKey(string key)
        {
            return key;
        }

        public override object ToValue(string? value)
        {
            return value;
        }
    }
    public abstract class CsvDataProvider<TKey, TValue> : DatasProviderCapturer<TKey, TValue>, IDatasProvider<TKey, TValue>
    {
        public CsvDataProvider(IReader csvReader, INamedInfo? name, bool capture)
            : base(capture)
        {
            Name = name;
            CsvReader = csvReader;
        }

        public IReader CsvReader { get; }

        public INamedInfo? Name { get; }

        protected override void OnDispose()
        {
            CsvReader.Dispose();
            GC.SuppressFinalize(this);
        }

        public IDataProvider<TKey, TValue>? Read()
        {
            if (CsvReader.Read())
            {
                if (captureDatasProvider != null)
                {
                    AddCaptureDataProvider(
                        CsvReader.HeaderRecord.Select(x => ToKey(x)),
                        Enumerable.Range(0, CsvReader.ColumnCount).Select(x => ToValue(CsvReader[x])));
                }
            }
            return null;
        }

        public bool Reset()
        {
            return false;
        }

        public abstract TKey ToKey(string key);

        public abstract TValue ToValue(string? value);
    }
}
