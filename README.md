<h2 align="center">
Ao.Middleware
</h2>
<h3 align="center">
A super lightweight middleware for any things.
</h3>

## How to use

### In async mode

```csharp
using System;

namespace Ao.Middleware.Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new MiddlewareBuilder<Sum>();//Define a middleware builder
            for (int i = 0; i < 10; i++)
            {
                builder.Use(x => x.Count++);//add middleware
            }
            var handler = builder.Build();//Build an pipeline
            var s = new Sum();//Define context
            handler(s).GetAwaiter().GetResult();//Execute
            Console.WriteLine(s.Count);//Result is 10
        }
    }
    public class Sum
    {
        public int Count { get; set; }
    }
}
```

### In sync mode

```csharp
using System;

namespace Ao.Middleware.Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new SyncMiddlewareBuilder<Sum>();//Define a middleware builder
            for (int i = 0; i < 10; i++)
            {
                builder.Use(x => x.Count++);//add middleware
            }
            var handler = builder.Build();//Build an pipeline
            var s = new Sum();//Define context
            handler(s)//Execute
            Console.WriteLine(s.Count);//Result is 10
        }
    }
    public class Sum
    {
        public int Count { get; set; }
    }
}
```

## Build status

[![.NET Build](https://github.com/Cricle/Ao.Middleware/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Cricle/Ao.Middleware/actions/workflows/dotnet.yml)

## Test convert

[![codecov](https://codecov.io/github/Cricle/Ao.Middleware/branch/main/graph/badge.svg?token=3HDRXLJNH2)](https://codecov.io/github/Cricle/Ao.Middleware)

## Nuget

![](https://img.shields.io/nuget/dt/Ao.Middleware)

## Benchmarks

``` ini

BenchmarkDotNet=v0.13.3, OS=Windows 11 (10.0.22000.1574/21H2)
AMD Ryzen 7 3700X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2


```
|      Method | MiddlewareCount |       Mean |    Error |   StdDev | Ratio | Allocated | Alloc Ratio |
|------------ |---------------- |-----------:|---------:|---------:|------:|----------:|------------:|
|     Execute |             100 | 2,756.4 ns | 11.60 ns | 10.85 ns |  1.00 |         - |          NA |
| ExecuteSync |             100 |   254.3 ns |  1.15 ns |  0.96 ns |  0.09 |         - |          NA |
|      Native |             100 |   487.5 ns |  0.98 ns |  0.86 ns |  0.18 |         - |          NA |
