using System;

namespace Ao.Middleware.DataWash
{
    public class AutoDataConverter<TInput, TOutput> : IDataConverter<TInput, TOutput>
    {
        public static readonly AutoDataConverter<TInput, TOutput> Instance = new AutoDataConverter<TInput, TOutput>();

        private static readonly TypeCode outputTypeCode = Type.GetTypeCode(typeof(TOutput));
        private static readonly Type inputActualType = typeof(TInput).IsGenericType && typeof(TInput).GetGenericTypeDefinition() == typeof(Nullable<>) ?
            typeof(TInput).GenericTypeArguments[0] : typeof(TInput);

        private static readonly bool outputIsObject = outputTypeCode == TypeCode.Object;

        private AutoDataConverter() { }

        public TOutput Convert(TInput input)
        {
            if (outputIsObject)
            {
                return (TOutput)(object)input!;
            }
            return (TOutput)System.Convert.ChangeType(input!, outputTypeCode);
        }
    }
}
