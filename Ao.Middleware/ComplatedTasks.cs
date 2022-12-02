using System.Threading.Tasks;

namespace Ao.Middleware
{
    internal static class ComplatedTasks
    {
        public static readonly Task ComplatedTask = Task.FromResult(false);
    }
}
