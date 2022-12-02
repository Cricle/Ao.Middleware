using System.Threading.Tasks;

namespace Ao.Middleware
{
    public readonly struct NullEndPoint<TContxt> : IMiddleware<TContxt>
    {
        public static readonly NullEndPoint<TContxt> Default = new NullEndPoint<TContxt>();

        public Task InvokeAsync(TContxt context, Handler<TContxt> next)
        {
            return ComplatedTasks.ComplatedTask;
        }
    }
}
