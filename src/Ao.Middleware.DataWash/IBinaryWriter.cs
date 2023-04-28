using System;

namespace Ao.Middleware.DataWash
{
    public interface IBinaryDataWriter: IDataWriter
    {
        void Write(byte value);
        void Write(byte[] buffer);
        void Write(byte[] buffer, int index, int count);
#if !NETSTANDARD2_0
        void Write(Half value);
#endif
        void Write(ReadOnlySpan<byte> buffer);
        void Write(sbyte value);
        void Write(short value);
        void Write(ushort value);
    }
}