using System.Threading.Tasks;

namespace Ao.Middleware
{
    public interface IInvokable<TContext>
    {
        Task InvokeAsync(MiddlewareContext<TContext> context);
    }
}
