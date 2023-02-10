using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Ao.Middleware
{
    public delegate Task Handler<TContext>(TContext context);
}
