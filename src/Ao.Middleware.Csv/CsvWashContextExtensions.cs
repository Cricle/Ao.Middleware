using Ao.Middleware.DataWash;
using CsvHelper;

namespace Ao.Middleware.Csv
{
    public static class CsvWashContextExtensions
    {
        public static CsvDataProvider<TKey, TValue> AddCsv<TKey, TValue, TOutput>(this IWashContext<TKey, TValue, TOutput> context,
            ICsvDataConverter<TKey, TValue> dataConverter,IReader reader, INamedInfo? named, bool capture = false)
        {
            var provider = new CsvDataProvider<TKey, TValue>(dataConverter, reader, named, capture);
            context.DatasProviders.Add(provider);
            return provider;
        }
    }
}
