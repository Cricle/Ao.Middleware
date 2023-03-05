using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public interface IWashBuilder<TContext, TKey, TValue, TOutput>: IList<IWashUnit<TContext, TKey, TValue, TOutput>>
           where TContext : IWashContext<TKey, TValue, TOutput>
    {

    }
}
