using System.Threading.Tasks;
using System.Threading;

namespace Ao.Middleware.DataWash
{
    public interface IDataWriter<TKey, TValue>
    {
        Task WriteAsync(IDatasProvider<TKey, TValue> datas, CancellationToken token = default);
    }
}
