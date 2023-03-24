using Ao.Middleware.DataWash;
using CsvHelper;

namespace Ao.Middleware.Csv
{
    public class CsvDataProvider<TKey, TValue> : DatasProviderCapturer<TKey, TValue>, IDatasProvider<TKey, TValue>
    {
        public CsvDataProvider(ICsvDataConverter<TKey, TValue> dataConverter,IReader csvReader, INamedInfo? name, bool capture)
            : base(capture)
        {
            Name = name;
            CsvReader = csvReader;
            DataConverter = dataConverter;
        }

        public IReader CsvReader { get; }

        public INamedInfo? Name { get; }

        public ICsvDataConverter<TKey, TValue> DataConverter { get; }

        private IDataProvider<TKey, TValue>? currentDataProvider;
    
        private void DisposeCurrent()
        {
            if (currentDataProvider is IDisposable disposable)
            {
                disposable.Dispose(); 
                currentDataProvider = null;
            }
        }

        protected override void OnDispose()
        {
            DisposeCurrent();
            CsvReader.Dispose();
            GC.SuppressFinalize(this);
        }
        
        public IDataProvider<TKey, TValue>? Read()
        {
            if (CsvReader.Read())
            {
                if (CsvReader.HeaderRecord == null)
                {
                    if (CsvReader.ReadHeader())
                    {
                        CsvReader.Read();
                    }
                }
                if (captureDatasProvider == null)
                {
                    DisposeCurrent();
                }
                currentDataProvider = CastCaptureDataProvider(CsvReader.HeaderRecord!.Select(x => DataConverter.KeyConverter.Convert(x)),
                        Enumerable.Range(0, CsvReader.HeaderRecord.Length).Select(x => DataConverter.ValueConverter.Convert(CsvReader[x])));
                if (captureDatasProvider != null)
                {
                   return AddCaptureDataProvider(currentDataProvider);
                }
                return currentDataProvider;
            }
            return null;
        }

        public bool Reset()
        {
            DisposeCurrent();
            return false;
        }

        public Task LoadAsync()
        {
            while (Read() != null) ;
            return Task.CompletedTask;
        }
    }
}
