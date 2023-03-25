using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash
{
    public class RangeDatasProvider<TKey, TValue> : IDatasProvider<TKey, TValue>
    {
        public IEnumerable<IDataProvider<TKey, TValue>> Datas { get; }

        public INamedInfo? Name => throw new NotImplementedException();

        private IEnumerator<IDataProvider<TKey, TValue>>? dataEnu;

        public RangeDatasProvider(IEnumerable<IDataProvider<TKey, TValue>> datas)
        {
            Datas = datas ?? throw new ArgumentNullException(nameof(datas));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task LoadAsync()
        {
            return Task.CompletedTask;
        }

        public IDataProvider<TKey, TValue>? Read()
        {
            if (dataEnu==null)
            {
                dataEnu = Datas.GetEnumerator();
            }
            dataEnu.MoveNext();
            return dataEnu.Current;
        }

        public bool Reset()
        {
            dataEnu?.Dispose();
            dataEnu = null;
            return true;
        }
    }
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

        public Task LoadAsync()
        {
            while (Read() != null) ;
            return Task.CompletedTask;
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
