namespace Ao.Middleware.DataWash
{
    public interface IColumnOutputCollection<TOutput>
    {
        TOutput Outputs { get; }
    }
}
