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
                var provider=x.AddCsv(CsvDataConverter.Instance,
                    Path.Combine(AppContext.BaseDirectory, "Res", "a.csv"),
                    new NamedInfo("csv")
                    ,true);
                provider.LoadAsync().GetAwaiter().GetResult();
                x.Outputs.DatasProviders.Add(provider);
            }).Use(x =>
            {
                //x.AddOutput("1", 1);
            });

            var handler = builder.Build();
            var gc = GC.GetTotalMemory(true);
            var sw = Stopwatch.GetTimestamp();
            for (int i = 0; i < 1000; i++)
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