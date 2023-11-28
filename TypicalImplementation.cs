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

        var censoredChars = suffix.Select(c =>
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

        var result = $"{prefix}.{new string(censoredChars)}";
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
        var censoredChars = suffix.Select(c =>
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
        // Allocates a string for 'prefix'.
        var result = $"{prefix}.{new string(censoredChars)}";

        // Allocates a byte[] for 'result' bytes.
        // Potentially allocates a state machine box if the write is performed asynchronously.
        await output.WriteAsync(Encoding.UTF8.GetBytes(result), cancel);

        return censoredNumberCount;
    }
    #endregion
}
