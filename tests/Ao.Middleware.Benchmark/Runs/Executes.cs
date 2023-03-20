using BenchmarkDotNet.Attributes;

namespace Ao.Middleware.Benchmark.Runs
{
    [MemoryDiagnoser]
    public class Executes
    {
        private Handler<byte> handler;
        private SyncHandler<byte> syncHandler;
        private Func<byte, Task> native;
        private byte obj;

        [Params(100)]
        public int MiddlewareCount { get; set; }
        private int a, b, c;
        [GlobalSetup]
        public void Setup()
        {
            var builder = new MiddlewareBuilder<byte>();
            for (int i = 0; i < MiddlewareCount; i++)
            {
                builder.Use(_ =>
                {
                    a++;
                    return Task.CompletedTask;
                });
            }
            handler = builder.Build();
            var syncBuilder = new SyncMiddlewareBuilder<byte>();
            for (int i = 0; i < MiddlewareCount; i++)
            {
                syncBuilder.Use(new SyncHandler<byte>(_ => { b++; }));
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
            obj = 1;
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
