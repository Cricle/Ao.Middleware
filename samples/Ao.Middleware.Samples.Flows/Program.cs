using Ao.Middleware.DataWash;
using System.Diagnostics;
using Ao.Middleware.Csv;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Ao.Middleware.Samples.Flows
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new SyncMiddlewareBuilder<WashContext<string>>();
            builder.Use(x =>
            {
                x.MapData["1"] = 123;
                var textReader = new StreamReader(File.OpenRead(Path.Combine(AppContext.BaseDirectory, "Res", "a.csv")));
                x.Disposables.Add(textReader);
                var reader =new CsvReader(textReader, new CsvConfiguration(CultureInfo.CurrentCulture));
                x.Disposables.Add(reader);
                var provider=x.AddCsv(CsvDataConverter.Instance,
                    reader,
                    new NamedInfo("csv")
                    ,true);
                provider.LoadAsync().GetAwaiter().GetResult();
            }).Use(x =>
            {
                x.AddOutput("1", 1);
            });

            var handler = builder.Build();
            var gc = GC.GetTotalMemory(true);
            var sw = Stopwatch.GetTimestamp();
            for (int i = 0; i < 10000; i++)
            {
                using (var ctx = new WashContext<string>())
                {
                    handler(ctx);
                }
            }
            Console.WriteLine(new TimeSpan(Stopwatch.GetTimestamp() - sw));
            Console.WriteLine($"{(GC.GetTotalMemory(false) - gc) / 1024 / 1024.0}Mb");
        }
    }
}