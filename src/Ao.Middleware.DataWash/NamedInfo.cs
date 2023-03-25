using System;

namespace Ao.Middleware.DataWash
{
    public readonly struct NamedInfo : INamedInfo,IEquatable<NamedInfo>
    {
        public NamedInfo(string? name)
        {
            Name = name;
        }

        public string? Name { get; }

        public override bool Equals(object? obj)
        {
            if (obj is NamedInfo info)
            {
                return Equals(info);
            }
            return false;
        }

        public bool Equals(NamedInfo other)
        {
            return Name== other.Name;
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }

        public override string? ToString()
        {
            return Name;
        }
        public static bool operator ==(NamedInfo left, NamedInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NamedInfo left, NamedInfo right)
        {
            return !(left == right);
        }
        public static explicit operator NamedInfo(string? name)
        {
            return new NamedInfo(name);
        }
        public static explicit operator string?(NamedInfo info)
        {
            return info.Name;
        }
    }
}
