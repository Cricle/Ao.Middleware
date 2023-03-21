using System.Collections.Generic;

namespace Ao.Middleware.DataWash
{
    public class ColumnOutputWashContextConverter<TKey, TValue> : ColumnOutputWashContextConverter<TKey, TValue, TValue>
    {
        public static readonly ColumnOutputWashContextConverter<TKey, TValue> Instance = new ColumnOutputWashContextConverter<TKey, TValue>();

        private ColumnOutputWashContextConverter() { }

        protected override TValue Convert(TValue output)
        {
            return output;
        }
    }
    public abstract class ColumnOutputWashContextConverter<TKey, TValue,TOutputValue>: IColumnOutputConverter<TKey, IList<IColumnOutput<TKey,TOutputValue>>,IWashContext<TKey,TValue, IList<IColumnOutput<TKey, TOutputValue>>>>
    {
        public IWashContext<TKey, TValue, IList<IColumnOutput<TKey, TOutputValue>>> Convert(IList<IColumnOutput<TKey, TOutputValue>> outputs)
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
        protected virtual IWashContext<TKey, TValue, IList<IColumnOutput<TKey, TOutputValue>>> CreateContext()
        {
            return new WashContext<TKey, TValue, IList<IColumnOutput<TKey, TOutputValue>>>();
        }

        protected abstract TValue Convert(TOutputValue output);

    }
    public interface IColumnOutputConverter<TKey, TOutput, TReturn>
    {
        TReturn Convert(TOutput outputs);
    }
}
