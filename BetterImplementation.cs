using System.Buffers;
using System.Text;

namespace TopTips;

internal static class BetterImplementation
{
    /// <summary>
    /// Censors any digit of pi that is smaller than the previous digit.
    /// </summary>
    /// <returns>Count of censored digits.</returns>
    public static async ValueTask<int> WriteCensoredDigitsOfPiAsUtf8BytesAsync(string π, Stream output, CancellationToken cancel)
    {
        ArgumentNullException.ThrowIfNull(π);
        ArgumentNullException.ThrowIfNull(output);

        if (!π.StartsWith("3.", StringComparison.Ordinal))
            throw new ArgumentException("π must start with '3.'", nameof(π));

        // Immediately convert everything to UTF-8 bytes to avoid string operations.
        var desiredBufferLength = Encoding.UTF8.GetMaxByteCount(π.Length);
        var buffer = ArrayPool<byte>.Shared.Rent(desiredBufferLength);

        try
        {
            var usedBufferBytes = Encoding.UTF8.GetBytes(π, buffer);

            // Skip the first 2 bytes because they are the "3." prefix.
            // The rest of the used buffer is the suffix.
            var suffix = buffer.AsMemory(2..(usedBufferBytes - 2));

            var censoredNumberCount = CensorSuffix(suffix);

            var censored = buffer.AsMemory(0..usedBufferBytes);

            await output.WriteAsync(censored, cancel);

            return censoredNumberCount;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    private static int CensorSuffix(Memory<byte> suffix)
    {
        // Business logic for consecutive suffix numbers:
        // * if the number gets bigger, we allow it.
        // * if the number is equal, we allow it.
        // * if the number gets smaller, we censor it.
        // * the first number is allowed.
        // e.g. 3.14*59*6**589*9**38*6*6**3...

        // We return how many numbers we censored.
        var censoredNumberCount = 0;

        // We keep track of the previous number as a character and just integer-compare each one.
        // Start with zero (lowest value) to ensure the first number is allowed without special cases in algorithm.
        byte previous = (byte)'0';

        var span = suffix.Span;

        for (var i = 0; i < span.Length; i++)
        {
            var c = span[i];
            var isSmallerThanPrevious = c < previous;
            previous = c;

            if (isSmallerThanPrevious)
            {
                censoredNumberCount++;
                span[i] = (byte)'*';
            }
        }

        return censoredNumberCount;
    }
}
