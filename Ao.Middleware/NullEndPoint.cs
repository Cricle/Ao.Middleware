using System.Threading.Tasks;

namespace Ao.Middleware
{
    public struct NullEndPoint<TContxt> : IMiddleware<TContxt>
    {
        public Task InvokeAsync(TContxt context, Handler<TContxt> next)
        {
            return ComplatedTasks.ComplatedTask;
        }
    }
}
