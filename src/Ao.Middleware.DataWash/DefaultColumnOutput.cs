using System;

namespace Ao.Middleware.DataWash
{
    public readonly struct DefaultColumnOutput<TKey, TOutput> : IColumnOutput<TKey, TOutput>
    {
        public DefaultColumnOutput(TKey key, TOutput output)
        {
            Key = key;
            Output = output;
        }

        public TKey Key { get; }

        public TOutput Output { get; }

        public bool Equals(IColumnOutput<TKey, TOutput>? other)
        {
            if (other==null)
            {
                return false;
            }
            return Equals(other.Key, Key)&&
                Equals(other.Output,Output);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Output);
        }
        public override bool Equals(object? obj)
        {
            if (obj is DefaultColumnOutput<TKey,TOutput> output)
            {
                return Equals(output);
            }
            return false;
        }

        public static bool operator ==(DefaultColumnOutput<TKey, TOutput> left, DefaultColumnOutput<TKey, TOutput> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DefaultColumnOutput<TKey, TOutput> left, DefaultColumnOutput<TKey, TOutput> right)
        {
            return !(left == right);
        }
        public override string ToString()
        {
            return $"{{{Key} = {Output}}}";
        }
    }
}
