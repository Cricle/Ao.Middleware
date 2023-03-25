using Ao.Middleware.DataWash;
using CsvHelper;

namespace Ao.Middleware.Csv
{
    public class CsvDataWriter<TKey, TValue>
    {
        public CsvDataWriter(ICsvDataConverter<TKey, TValue> dataConverter, IWriter writer)
        {
            DataConverter = dataConverter;
            Writer = writer;
        }

        public ICsvDataConverter<TKey, TValue> DataConverter { get; }

        public IWriter Writer { get; }

        public bool WriteHead { get; set; } = true;

        public virtual TKey[] GetHeader(IDataProvider<TKey, TValue> datas)
        {
            return datas.Keys.ToArray();
        }

        public Task WriteAsync(IDatasProvider<TKey, TValue> datas, CancellationToken token = default)
        {
            var val = datas.Read();
            if (val==null)
            {
                return Task.CompletedTask;
            }
            var head = GetHeader(val);
            if (WriteHead)
            {
                foreach (var item in head)
                {
                    Writer.WriteField(item);
                }
                Writer.NextRecord();
                token.ThrowIfCancellationRequested();
            }
            while (val != null)
            {
                token.ThrowIfCancellationRequested();
                foreach (var item in head)
                {
                    if (val.TryGetValue(item,out var v))
                    {
                        Writer.WriteField(v);
                    }
                }
                Writer.NextRecord();
                val = datas.Read();
            }
            return Task.CompletedTask;
        }
    }
}
