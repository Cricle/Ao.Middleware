using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public class ColumnOutputWashContextConverter<TKey, TSame> : ColumnOutputWashContextConverter<TKey, TSame, TSame>
    {
        public static readonly ColumnOutputWashContextConverter<TKey, TSame> Instance = new ColumnOutputWashContextConverter<TKey, TSame>();

        private ColumnOutputWashContextConverter()
        {
        }
        protected override IWashContext<TKey, TSame, TSame> CreateContext()
        {
            return new WashContext<TKey, TSame, TSame>();
        }
        protected override TSame Convert(TSame output)
        {
            return output;
        }
    }
    public abstract class ColumnOutputWashContextConverter<TKey, TValue, TOutput> : IColumnOutputConverter<TKey, TOutput, IWashContext<TKey, TValue, TOutput>>
    {
        public IWashContext<TKey, TValue, TOutput> Convert(IReadOnlyList<IColumnOutput<TKey, TOutput>> outputs)
        {
            var ctx = CreateContext();
            if (ctx is IWithMapDataProviderWashContext<TKey, TValue> mapDataCtx)
            {
                foreach (var item in outputs)
                {
                    mapDataCtx.MapData[item.Key] = Convert(item.Output);
                }
            }
            else
            {
                var provider = new PoolMapDataProvider<TKey, TValue>();
                foreach (var item in outputs)
                {
                    provider[item.Key] = Convert(item.Output);
                }
                ctx.DataProviders.Add(provider);
            }
            return ctx;
        }

        protected virtual IWashContext<TKey, TValue, TOutput> CreateContext()
        {
            return new WashContext<TKey, TValue, TOutput>();
        }

        protected abstract TValue Convert(TOutput output);
    }
    public interface IColumnOutputConverter<TKey, TOutput, TReturn>
    {
        TReturn Convert(IReadOnlyList<IColumnOutput<TKey, TOutput>> outputs);
    }
}
