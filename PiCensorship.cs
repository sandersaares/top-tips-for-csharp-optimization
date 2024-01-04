using BenchmarkDotNet.Attributes;

namespace TopTips;

[MemoryDiagnoser]
//[EventPipeProfiler(BenchmarkDotNet.Diagnosers.EventPipeProfile.GcVerbose)]
public class PiCensorship
{
    [Benchmark(Baseline = true)]
    public async Task Typical()
    {
        await TypicalImplementation.WriteCensoredDigitsOfPiAsUtf8BytesAsync(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }

    [Benchmark]
    public async Task Optimized1()
    {
        await TypicalImplementation.WriteCensoredDigitsOfPiAsUtf8BytesOptimized1Async(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }

    [Benchmark]
    public async Task Optimized2()
    {
        await TypicalImplementation.WriteCensoredDigitsOfPiAsUtf8BytesOptimized2Async(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }

    [Benchmark]
    public async Task Optimized3()
    {
        await TypicalImplementation.WriteCensoredDigitsOfPiAsUtf8BytesOptimized3Async(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }

    [Benchmark]
    public async Task Optimized4()
    {
        await TypicalImplementation.WriteCensoredDigitsOfPiAsUtf8BytesOptimized4Async(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }

    [Benchmark]
    public async Task Optimized5()
    {
        await BetterImplementation.WriteCensoredDigitsOfPiAsUtf8BytesAsync(DigitsOfPi.π1000, Stream.Null, CancellationToken.None);
    }
}
