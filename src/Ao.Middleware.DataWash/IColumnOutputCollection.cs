using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public interface IColumnOutputCollection<TKey, TOutput>
    {
        IList<IColumnOutput<TKey, TOutput>> Outputs { get; }
    }
}
