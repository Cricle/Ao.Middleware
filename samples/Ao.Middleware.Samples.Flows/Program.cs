using Ao.Middleware.DataWash;
using System.Diagnostics;

namespace Ao.Middleware.Samples.Flows
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new WashBuilder<WashContext<int>, int>();
            builder.WithDelegate(x =>
            {
                x.MapData[1] = 123;
            }).WithDelegate(x =>
            {
                x.AddOutput(1, x.MapData[1]);
            });

            var handler = builder.Build();
            var gc = GC.GetTotalMemory(true);
            var sw = Stopwatch.GetTimestamp();
            for (int i = 0; i < 1_000_000; i++)
            {
                using (var ctx = new WashContext<int>())
                {
                    await handler(ctx);
                }
            }
            Console.WriteLine(new TimeSpan(Stopwatch.GetTimestamp() - sw));
            Console.WriteLine($"{(GC.GetTotalMemory(false) - gc) / 1024 / 1024.0}Mb");
        }
    }
}