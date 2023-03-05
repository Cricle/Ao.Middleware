using System.Threading;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash
{
    public interface IWashUnit<TContext, TKey, TValue, TOutput>
        where TContext : IWashContext<TKey, TValue, TOutput>
    {
        Task WashAsync(TContext context, CancellationToken token = default);
    }
}
