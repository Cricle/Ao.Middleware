using Ao.Middleware.Benchmark.Runs;
using BenchmarkDotNet.Running;

namespace Ao.Middleware.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Executes>();
        }
    }
}