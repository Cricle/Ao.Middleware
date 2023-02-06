using BenchmarkDotNet.Attributes;

namespace Ao.Middleware.Benchmark.Runs
{
    [MemoryDiagnoser]
    public class Executes
    {
        private Handler<object> handler;
        private Func<object, Task> native;
        private object obj;

        [Params(100)]
        public int MiddlewareCount { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var builder = new MiddlewareBuilder<object>();
            for (int i = 0; i < MiddlewareCount; i++)
            {
                builder.Use(_ => Task.CompletedTask);
            }
            handler = builder.Build();
            native = _ => Task.CompletedTask;
            for (int i = 0; i < MiddlewareCount - 1; i++)
            {
                native += _ => Task.CompletedTask;
            }
            obj = new object();
        }

        [Benchmark(Baseline = true)]
        public async Task Execute()
        {
            await handler(obj);
        }
        [Benchmark]
        public async Task Native()
        {
            await native(obj);
        }
    }
}
