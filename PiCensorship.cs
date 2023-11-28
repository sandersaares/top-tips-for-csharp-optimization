using BenchmarkDotNet.Attributes;

namespace TopTips;

[MemoryDiagnoser]
[EventPipeProfiler(BenchmarkDotNet.Diagnosers.EventPipeProfile.GcVerbose)]
public class PiCensorship
{
    [Benchmark(Baseline = true)]
    public Task<int> Typical()
    {
        return TypicalImplementation.WriteCensoredDigitsOfPiAsUtf8BytesAsync(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }

    #region Better
    [Benchmark]
    public ValueTask<int> Better()
    {
        return BetterImplementation.WriteCensoredDigitsOfPiAsUtf8BytesAsync(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }
    #endregion
}
