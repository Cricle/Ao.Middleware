using System.Threading.Tasks;

namespace Ao.Middleware
{
    public delegate Task Handler<TContext>(TContext context);

    public delegate void SyncHandler<TContext>(TContext context);
}
