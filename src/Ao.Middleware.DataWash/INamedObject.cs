using System;

namespace Ao.Middleware.DataWash
{
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
