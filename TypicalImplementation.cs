using System.Buffers;
using System.Text;

namespace TopTips;

internal static class TypicalImplementation
{
    /// <summary>
    /// Censors any digit of pi that is smaller than the previous digit.
    /// </summary>
    /// <returns>Count of censored digits.</returns>
    public static async Task<int> WriteCensoredDigitsOfPiAsUtf8BytesAsync(string π, Stream output, CancellationToken cancel)
    {
        ArgumentNullException.ThrowIfNull(π);
        ArgumentNullException.ThrowIfNull(output);

        var components = π.Split('.');

        if (components.Length != 2)
            throw new ArgumentException("π must be in the form of '3.14159...'", nameof(π));

        // The 3. is always the same, so we can just write it out.
        var prefix = components[0];

        // The long tail is what we actually censor.
        var suffix = components[1];

        // Business logic for consecutive suffix numbers:
        // * if the number gets bigger, we allow it.
        // * if the number is equal, we allow it.
        // * if the number gets smaller, we censor it.
        // * the first number is allowed.
        // e.g. 3.14*59*6**589*9**38*6*6**3...

        // We return how many numbers we censored.
        var censoredNumberCount = 0;

        // We keep track of the previous number as a character and just ASCII-compare each one.
        // Start with zero (lowest value) to ensure the first number is allowed without special cases in algorithm.
        char previous = '0';

        var censoredSuffixChars = suffix.Select(c =>
        {
            var isSmallerThanPrevious = c < previous;
            previous = c;

            // If the number is smaller than the previous, we censor it.
            if (isSmallerThanPrevious)
            {
                censoredNumberCount++;
                return '*';
            }

            return c;
        }).ToArray();

        var result = $"{prefix}.{new string(censoredSuffixChars)}";
        await output.WriteAsync(Encoding.UTF8.GetBytes(result), cancel);

        return censoredNumberCount;
    }

    #region Annotated
    /// <summary>
    /// Censors any digit of pi that is smaller than the previous digit.
    /// </summary>
    /// <returns>Count of censored digits.</returns>
    public static async Task<int> WriteCensoredDigitsOfPiAsUtf8BytesAsyncAnnotated(string π, Stream output, CancellationToken cancel)
    {
        // Allocates a Task<int> to return to the caller.

        ArgumentNullException.ThrowIfNull(π);
        ArgumentNullException.ThrowIfNull(output);

        // Allocates a string[2]
        // Allocates a string for components[0]
        // Allocates a string for components[1]
        var components = π.Split('.');

        if (components.Length != 2)
            throw new ArgumentException("π must be in the form of '3.14159...'", nameof(π));

        // The 3. is always the same, so we can just write it out.
        var prefix = components[0];

        // The long tail is what we actually censor.
        var suffix = components[1];

        // Business logic for consecutive suffix numbers:
        // * if the number gets bigger, we allow it.
        // * if the number is equal, we allow it.
        // * if the number gets smaller, we censor it.
        // * the first number is allowed.
        // e.g. 3.14*59*6**589*9**38*6*6**3...

        // We return how many numbers we censored.
        var censoredNumberCount = 0;

        // We keep track of the previous number as a character and just ASCII-compare each one.
        // Start with zero (lowest value) to ensure the first number is allowed without special cases in algorithm.
        char previous = '0';

        // Allocates a char[] for censoredChars.
        // Allocates a closure to capture 'censoredNumberCount' and 'previous' variables.
        // Allocates a delegate for the lambda.
        // Allocates a char[][] for LINQ reasons.
        // Allocates an iterator for LINQ reasons.
        // Allocates a char-enumerator to enumerate the chars in the string.
        var censoredSuffixChars = suffix.Select(c =>
        {
            var isSmallerThanPrevious = c < previous;
            previous = c;

            // If the number is smaller than the previous, we censor it.
            if (isSmallerThanPrevious)
            {
                censoredNumberCount++;
                return '*';
            }

            return c;
        }).ToArray();

        // Allocates a temporary string for 'censoredChars' stringification.
        // Allocates a string for 'result'.
        var result = $"{prefix}.{new string(censoredSuffixChars)}";

        // Allocates a byte[] for 'result' bytes.
        // Potentially allocates a state machine box if the write is performed asynchronously.
        await output.WriteAsync(Encoding.UTF8.GetBytes(result), cancel);

        return censoredNumberCount;
    }
    #endregion

    #region Optimization 1
    /// <summary>
    /// Censors any digit of pi that is smaller than the previous digit.
    /// </summary>
    /// <returns>Count of censored digits.</returns>
    public static async Task<int> WriteCensoredDigitsOfPiAsUtf8BytesOptimized1Async(string π, Stream output, CancellationToken cancel)
    {
        ArgumentNullException.ThrowIfNull(π);
        ArgumentNullException.ThrowIfNull(output);

        // OPTIMIZATION: Just validate the prefix without extracting it.
        if (!π.StartsWith("3.", StringComparison.Ordinal))
            throw new ArgumentException("π must start with '3.'", nameof(π));

        // Business logic for consecutive suffix numbers:
        // * if the number gets bigger, we allow it.
        // * if the number is equal, we allow it.
        // * if the number gets smaller, we censor it.
        // * the first number is allowed.
        // e.g. 3.14*59*6**589*9**38*6*6**3...

        // We return how many numbers we censored.
        var censoredNumberCount = 0;

        // We keep track of the previous number as a character and just ASCII-compare each one.
        // Start with zero (lowest value) to ensure the first number is allowed without special cases in algorithm.
        char previous = '0';

        // OPTIMIZATION: Operate on the chars from the original string, just skipping the "3." at the start.
        var censoredSuffixChars = π.Skip(2).Select(c =>
        {
            var isSmallerThanPrevious = c < previous;
            previous = c;

            // If the number is smaller than the previous, we censor it.
            if (isSmallerThanPrevious)
            {
                censoredNumberCount++;
                return '*';
            }

            return c;
        }).ToArray();

        // OPTIMIZATION: Just write the "3." prefix as a constant value.
        var result = $"3.{new string(censoredSuffixChars)}";
        await output.WriteAsync(Encoding.UTF8.GetBytes(result), cancel);

        return censoredNumberCount;
    }
    #endregion

    #region Optimization 2
    /// <summary>
    /// Censors any digit of pi that is smaller than the previous digit.
    /// </summary>
    /// <returns>Count of censored digits.</returns>
    public static async Task<int> WriteCensoredDigitsOfPiAsUtf8BytesOptimized2Async(string π, Stream output, CancellationToken cancel)
    {
        ArgumentNullException.ThrowIfNull(π);
        ArgumentNullException.ThrowIfNull(output);

        if (!π.StartsWith("3.", StringComparison.Ordinal))
            throw new ArgumentException("π must start with '3.'", nameof(π));

        // Business logic for consecutive suffix numbers:
        // * if the number gets bigger, we allow it.
        // * if the number is equal, we allow it.
        // * if the number gets smaller, we censor it.
        // * the first number is allowed.
        // e.g. 3.14*59*6**589*9**38*6*6**3...

        // We return how many numbers we censored.
        var censoredNumberCount = 0;

        // We keep track of the previous number as a character and just ASCII-compare each one.
        // Start with zero (lowest value) to ensure the first number is allowed without special cases in algorithm.
        char previous = '0';

        // OPTIMIZATION: Do not use LINQ, iterate the chars directly to construct the censored suffix.
        var censoredSuffixChars = new char[π.Length - 2];

        for (var i = 0; i < censoredSuffixChars.Length; i++)
        {
            var c = π[i + 2];

            var isSmallerThanPrevious = c < previous;
            previous = c;

            // If the number is smaller than the previous, we censor it.
            if (isSmallerThanPrevious)
            {
                censoredNumberCount++;
                censoredSuffixChars[i] = '*';
            }
            else
            {
                censoredSuffixChars[i] = c;
            }
        }

        var result = $"3.{new string(censoredSuffixChars)}";
        await output.WriteAsync(Encoding.UTF8.GetBytes(result), cancel);

        return censoredNumberCount;
    }
    #endregion

    #region Optimization 3
    /// <summary>
    /// Censors any digit of pi that is smaller than the previous digit.
    /// </summary>
    /// <returns>Count of censored digits.</returns>
    public static async Task<int> WriteCensoredDigitsOfPiAsUtf8BytesOptimized3Async(string π, Stream output, CancellationToken cancel)
    {
        ArgumentNullException.ThrowIfNull(π);
        ArgumentNullException.ThrowIfNull(output);

        if (!π.StartsWith("3.", StringComparison.Ordinal))
            throw new ArgumentException("π must start with '3.'", nameof(π));

        // Business logic for consecutive suffix numbers:
        // * if the number gets bigger, we allow it.
        // * if the number is equal, we allow it.
        // * if the number gets smaller, we censor it.
        // * the first number is allowed.
        // e.g. 3.14*59*6**589*9**38*6*6**3...

        // We return how many numbers we censored.
        var censoredNumberCount = 0;

        // We keep track of the previous number as a character and just ASCII-compare each one.
        // Start with zero (lowest value) to ensure the first number is allowed without special cases in algorithm.
        char previous = '0';

        var censoredSuffixChars = new char[π.Length - 2];

        for (var i = 0; i < censoredSuffixChars.Length; i++)
        {
            var c = π[i + 2];

            var isSmallerThanPrevious = c < previous;
            previous = c;

            // If the number is smaller than the previous, we censor it.
            if (isSmallerThanPrevious)
            {
                censoredNumberCount++;
                censoredSuffixChars[i] = '*';
            }
            else
            {
                censoredSuffixChars[i] = c;
            }
        }

        var result = $"3.{new string(censoredSuffixChars)}";

        // OPTIMIZATION: Do not allocate memory when encoding the string to UTF-8 bytes.
        var desiredBufferLength = Encoding.UTF8.GetMaxByteCount(result.Length);
        var buffer = ArrayPool<byte>.Shared.Rent(desiredBufferLength);

        try
        {
            var usedBufferBytes = Encoding.UTF8.GetBytes(result, buffer);

            await output.WriteAsync(buffer.AsMemory(..usedBufferBytes), cancel);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }

        return censoredNumberCount;
    }
    #endregion

    #region Optimization 4
    /// <summary>
    /// Censors any digit of pi that is smaller than the previous digit.
    /// </summary>
    /// <returns>Count of censored digits.</returns>
    public static async Task<int> WriteCensoredDigitsOfPiAsUtf8BytesOptimized4Async(string π, Stream output, CancellationToken cancel)
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
            var suffix = buffer.AsMemory(2..usedBufferBytes));

            var censoredNumberCount = CensorSuffixOptimized4(suffix);

            var censored = buffer.AsMemory(0..usedBufferBytes);

            await output.WriteAsync(censored, cancel);

            return censoredNumberCount;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    private static int CensorSuffixOptimized4(Memory<byte> suffix)
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
    #endregion

    #region Optimization 5
    // See BetterImplementation.cs
    #endregion
}
