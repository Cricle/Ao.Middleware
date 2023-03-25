using Ao.Middleware.DataWash;
using System.Diagnostics;
using Ao.Middleware.Csv;
using CsvHelper;
using System.Globalization;
using System.Text;

namespace Ao.Middleware.Samples.Flows
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new SyncMiddlewareBuilder<IWashContext<string,object?,object?>>();
            builder.UseCsv(CsvDataConverter.Instance,
                    Path.Combine(AppContext.BaseDirectory, "Res", "a.csv"),
                    new NamedInfo("csv"))
                .Use(x => x.Outputs.DatasProviders.Add(x.Inputs.DatasProviders[0]));

            var handler = builder.Build();
            var gc = GC.GetTotalMemory(true);
            var sw = Stopwatch.GetTimestamp();
            for (int i = 0; i < 1; i++)
            {
                using (var ctx = new WashContext<string,object?,object?>())
                {
                    handler(ctx);
                    var s = new StringBuilder();
                    var wt = new CsvWriter(new StringWriter(s), CultureInfo.CurrentCulture);
                    var q = new CsvDataWriter<string, object?>(CsvDataConverter.Instance, wt);
                    q.WriteAsync(ctx.Outputs.DatasProviders[0]).GetAwaiter().GetResult();
                    Console.WriteLine(s);
                }
            }
            Console.WriteLine(new TimeSpan(Stopwatch.GetTimestamp() - sw));
            Console.WriteLine($"{(GC.GetTotalMemory(false) - gc) / 1024 / 1024.0}Mb");
        }
    }
}