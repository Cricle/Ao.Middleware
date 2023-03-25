namespace Ao.Middleware.DataWash.Streaming
{
    public readonly record struct DataStreamBox<TKey, TValue>
    {
        public DataStreamBox(INamedInfo? named, TKey key, TValue value)
        {
            Named = named;
            Key = key;
            Value = value;
        }

        public INamedInfo? Named { get; }

        public TKey Key { get; }

        public TValue Value { get; }
    }
}
