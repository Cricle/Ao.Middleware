using System;

namespace Ao.Middleware.DataWash
{
    public class DefaultDatasProvider<TKey, TValue> :DataProviderGroup<TKey,TValue>, IDatasProvider<TKey, TValue>
    {
        private int visitPos;

        public DefaultDatasProvider()
        {
            Reset();
        }

        public DefaultDatasProvider(INamedInfo? name)
            :base(name)
        {
            Reset();
        }

        public int VisitPos
        {
            get
            {
                return visitPos;
            }
            set
            {
                if (visitPos < 0 || visitPos > Count)
                {
                    throw new IndexOutOfRangeException($"Must min than {Count}");
                }
                visitPos = value - 1;
            }
        }

        public IDataProvider<TKey, TValue>? Read()
        {
            if (visitPos>Count)
            {
                return null;
            }
            visitPos++;
            return this[visitPos];
        }

        public bool Reset()
        {
            visitPos = -1;
            return true;
        }
    }
}
