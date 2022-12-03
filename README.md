<h2 align="center">
Ao.Middleware
</h2>
<h3 align="center">
A super lightweight middleware for any things.
</h3>

## How to use

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

## Build status

[![.NET Build](https://github.com/Cricle/Ao.Middleware/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Cricle/Ao.Middleware/actions/workflows/dotnet.yml)

## Test convert

[![codecov](https://codecov.io/github/Cricle/Ao.Middleware/branch/main/graph/badge.svg?token=3HDRXLJNH2)](https://codecov.io/github/Cricle/Ao.Middleware)

## Nuget

![](https://img.shields.io/nuget/dt/Ao.Middleware)
