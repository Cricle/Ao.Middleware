using System;

namespace Ao.Middleware.DataWash
{
    public interface IDataWriter
    {
        void Flush();
        void Write(bool value);
        void Write(char[] chars);
        void Write(char[] chars, int index, int count);
        void Write(decimal value);
        void Write(double value);
        void Write(float value);
        void Write(char ch);
        void Write(int value);
        void Write(long value);
        void Write(uint value);
        void Write(ulong value);
        void Write(string value);
#if !NETSTANDARD2_0
        void Write(ReadOnlySpan<char> chars);
#endif
    }
}