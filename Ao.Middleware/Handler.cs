using System.Threading.Tasks;

namespace Ao.Middleware
{
    public delegate Task Handler<TContext>(MiddlewareContext<TContext> context);
}
