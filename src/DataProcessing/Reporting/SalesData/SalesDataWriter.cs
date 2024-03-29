namespace DataProcessing.Reporting;

internal sealed class SalesDataWriter : DataWriter<IEnumerable<HistoricalSalesData>>
{
    public SalesDataWriter(ProcessingOptions options) : base(options)
    {
    }

    protected override Task WriteAsyncCore(
        string pathAndFileName, 
        IEnumerable<HistoricalSalesData> data, 
        CancellationToken cancellationToken = default)
    {
        // TODO - Implementation
        return Task.CompletedTask;
    }
}
