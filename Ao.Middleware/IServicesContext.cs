using System;

namespace Ao.Middleware
{
    public interface IServicesContext
    {
        IServiceProvider ServiceProvider { get; }
    }
}
