namespace Ao.Middleware.DataWash
{
    public interface IDataConverter<in TInput, out TOutput>
    {
        TOutput Convert(TInput input);
    }
}
