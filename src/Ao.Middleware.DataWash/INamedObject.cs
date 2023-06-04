using System;

namespace Ao.Middleware.DataWash
{
    public class DefaultNamedInfo: INamedInfo
    {
        public static readonly DefaultNamedInfo Instance = new DefaultNamedInfo();

        private DefaultNamedInfo() { }

        public override bool Equals(object? obj)
        {
            return obj == Instance;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "Default";
        }
    }
    public interface INamedInfo
    {
        int GetHashCode();

        bool Equals(object other);

        string? ToString();
    }
    public interface INamedObject
    {
        INamedInfo? Name { get; }
    }
}
