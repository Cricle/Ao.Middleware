using System.Threading.Tasks;

namespace Ao.Middleware
{
    public interface IInvokable<TContext>
    {
        Task InvokeAsync(TContext context);
    }
}
