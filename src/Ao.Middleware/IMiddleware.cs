using System.Threading.Tasks;

namespace Ao.Middleware
{
    public interface IMiddleware<TContext>
    {
        Task InvokeAsync(TContext context, Handler<TContext> next);
    }
}
