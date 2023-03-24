using Ao.Middleware.DataWash;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Ao.Middleware.Csv
{
    public static class CsvWashContextExtensions
    {
        public static CsvDataProvider<TKey, TValue> AddCsv<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context,
            ICsvDataConverter<TKey, TValue> dataConverter, string filePath, INamedInfo? named, bool capture = false)
        {
            var textReader = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));
            context.Disposables.Add(textReader);
            var reader = new CsvReader(textReader, new CsvConfiguration(CultureInfo.CurrentCulture));
            context.Disposables.Add(reader);
            return AddCsv(context, dataConverter, reader, named, capture);
        }
        public static CsvDataProvider<TKey, TValue> AddCsv<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context,
            ICsvDataConverter<TKey, TValue> dataConverter,IReader reader, INamedInfo? named, bool capture = false)
        {
            var provider = new CsvDataProvider<TKey, TValue>(dataConverter, reader, named, capture);
            context.Inputs.DatasProviders.Add(provider);
            return provider;
        }
    }
}
