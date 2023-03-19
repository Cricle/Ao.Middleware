using System.Threading.Tasks;

namespace Ao.Middleware
{
    public interface ISyncMiddleware<TContext>
    {
        void Invoke(TContext context, SyncHandler<TContext> next);
    }
    public interface IMiddleware<TContext>
    {
        Task InvokeAsync(TContext context, Handler<TContext> next);
    }
}
