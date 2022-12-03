using System.Threading.Tasks;

namespace Ao.Middleware
{
    internal readonly struct DirectMiddleware<TContext> : IInvokable<TContext>
    {
        private readonly Handler<TContext> next;

        private readonly Handler<TContext> worker;

        public DirectMiddleware(Handler<TContext> worker, Handler<TContext> next)
        {
            this.worker = worker;
            this.next = next;
        }

        public async Task InvokeAsync(TContext context)
        {
            await worker(context);
            await next(context);
        }

    }
}
