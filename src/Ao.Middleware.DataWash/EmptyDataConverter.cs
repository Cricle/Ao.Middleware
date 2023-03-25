using System;

namespace Ao.Middleware.DataWash
{
    public class EmptyDataConverter<T>: IDataConverter<T,T>
    {
        public static readonly EmptyDataConverter<T> Instance = new EmptyDataConverter<T>();

        private EmptyDataConverter() { }
        public T Convert(T input)
        {
            return input;
        }

    }
}
