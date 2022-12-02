using System.Threading.Tasks;

namespace Ao.Middleware
{
    public interface IMiddleware<TContext>
    {
        Task InvokeAsync(MiddlewareContext<TContext> context, Handler<TContext> next);
    }
}
