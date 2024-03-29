using Microsoft.Extensions.Logging.Abstractions;

namespace DataProcessing.Benchmarks;

public class FailedSalesDataInspectorBenchmarks
{
    // This row is valid, which avoids the logger alloc and tests the
    // inspector all the way through.
    private const string Row = "a|b|c|d|e|f|SAL123:Test";

    private static readonly FailedSalesDataInspector Inspector = 
        new(new ProcessingOptions(NullLoggerFactory.Instance));

    [Benchmark]
    public void InspectRow() => Inspector.Inspect(Row);
}
