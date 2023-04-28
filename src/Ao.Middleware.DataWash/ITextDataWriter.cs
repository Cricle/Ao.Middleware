using System;
using System.IO;
#if NET7_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
#if !NETSTANDARD2_0
using System.Threading;
#endif
using System.Text;
using System.Threading.Tasks;

namespace Ao.Middleware.DataWash
{
    public interface ITextDataWriter: IDataWriter
    {
        Encoding Encoding { get; }

        IFormatProvider FormatProvider { get; }

        string NewLine { get; set; }

        Task FlushAsync();
        void Write(object? value);
        void Write(
#if NET7_0_OR_GREATER
            [StringSyntax(StringSyntaxAttribute.CompositeFormat)] 
#endif
        string format, object? arg0);
        void Write(
#if NET7_0_OR_GREATER
            [StringSyntax(StringSyntaxAttribute.CompositeFormat)] 
#endif
        string format, object? arg0, object? arg1);
        void Write(
#if NET7_0_OR_GREATER
            [StringSyntax(StringSyntaxAttribute.CompositeFormat)] 
#endif
        string format, object? arg0, object? arg1, object? arg2);
        void Write(
#if NET7_0_OR_GREATER
            [StringSyntax(StringSyntaxAttribute.CompositeFormat)] 
#endif
        string format, params object?[] arg);
        Task WriteAsync(char value);
        Task WriteAsync(char[] buffer, int index, int count);
        Task WriteAsync(char[]? buffer);
        Task WriteAsync(string? value);
        void WriteLine();
        Task WriteLineAsync();
#if !NETSTANDARD2_0
        void Write(ReadOnlySpan<char> buffer);
        void Write(StringBuilder? value);
        Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default);
        Task WriteAsync(StringBuilder? value, CancellationToken cancellationToken = default);
#endif
    }
}