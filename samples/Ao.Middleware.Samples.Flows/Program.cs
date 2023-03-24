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
            var builder = new SyncMiddlewareBuilder<IWashContext<string,object?,object?>>();
            builder.UseCsv(CsvDataConverter.Instance,
                    Path.Combine(AppContext.BaseDirectory, "Res", "a.csv"),
                    new NamedInfo("csv"));

            var handler = builder.Build();
            var gc = GC.GetTotalMemory(true);
            var sw = Stopwatch.GetTimestamp();
            for (int i = 0; i < 100_000; i++)
            {
                using (var ctx = new WashContext<string,object?,object?>())
                {
                    handler(ctx);
                }
            }
            Console.WriteLine(new TimeSpan(Stopwatch.GetTimestamp() - sw));
            Console.WriteLine($"{(GC.GetTotalMemory(false) - gc) / 1024 / 1024.0}Mb");
        }
    }
}