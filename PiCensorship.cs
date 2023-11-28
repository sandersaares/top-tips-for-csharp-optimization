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

    [Benchmark]
    public Task<int> Optimized1()
    {
        return TypicalImplementation.WriteCensoredDigitsOfPiAsUtf8BytesOptimized1Async(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }

    [Benchmark]
    public Task<int> Optimized2()
    {
        return TypicalImplementation.WriteCensoredDigitsOfPiAsUtf8BytesOptimized2Async(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }

    [Benchmark]
    public Task<int> Optimized3()
    {
        return TypicalImplementation.WriteCensoredDigitsOfPiAsUtf8BytesOptimized3Async(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }

    [Benchmark]
    public Task<int> Optimized4()
    {
        return TypicalImplementation.WriteCensoredDigitsOfPiAsUtf8BytesOptimized4Async(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }

    [Benchmark]
    public ValueTask<int> Optimized5()
    {
        return BetterImplementation.WriteCensoredDigitsOfPiAsUtf8BytesAsync(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }
}
