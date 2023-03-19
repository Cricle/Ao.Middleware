namespace Ao.Middleware
{
    internal class SyncDirectMiddleware<TContext> : ISyncInvokable<TContext>
    {
        private readonly SyncHandler<TContext> next;

        private readonly SyncHandler<TContext> worker;

        public SyncDirectMiddleware(SyncHandler<TContext> worker, SyncHandler<TContext> next)
        {
            this.worker = worker;
            this.next = next;
        }
        public void Invoke(TContext context)
        {
            worker(context);
            next(context);
        }
    }
}
