using BenchmarkDotNet.Attributes;

namespace Ao.Middleware.Benchmark.Runs
{
    [MemoryDiagnoser]
    public class Executes
    {
        private Handler<object> handler;
        private SyncHandler<object> syncHandler;
        private Func<object, Task> native;
        private object obj;

        [Params(100)]
        public int MiddlewareCount { get; set; }
        private int a,b,c;
        [GlobalSetup]
        public void Setup()
        {
            var builder = new MiddlewareBuilder<object>();
            for (int i = 0; i < MiddlewareCount; i++)
            {
                builder.Use(_ =>
                {
                    a++;
                    return Task.CompletedTask;
                });
            }
            handler = builder.Build();
            var syncBuilder = new SyncMiddlewareBuilder<object>();
            for (int i = 0; i < MiddlewareCount; i++)
            {
                syncBuilder.Use(new SyncHandler<object>(_ => { b++; }));
            }
            syncHandler = syncBuilder.Build();
            native = _ => Task.CompletedTask;
            for (int i = 0; i < MiddlewareCount - 1; i++)
            {
                native += _ =>
                {
                    c++;
                    return Task.CompletedTask;
                };
            }
            obj = new object();
        }

        [Benchmark(Baseline = true)]
        public async Task Execute()
        {
            await handler(obj);
        }
        [Benchmark]
        public void ExecuteSync()
        {
            syncHandler(obj);
        }
        [Benchmark]
        public async Task Native()
        {
            await native(obj);
        }
    }
}
