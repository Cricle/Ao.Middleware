using System.Threading.Tasks;

namespace Ao.Middleware
{
    public class NullEndPoint<TContxt> : IMiddleware<TContxt>
    {
        public Task InvokeAsync(MiddlewareContext<TContxt> context, Handler<TContxt> next)
        {
            return ComplatedTasks.ComplatedTask;
        }
    }
}
