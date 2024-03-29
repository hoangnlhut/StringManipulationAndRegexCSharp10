namespace DataProcessing.Reporting;

internal sealed class StringBuilderSalesDataWriter : DataWriter<IEnumerable<HistoricalSalesData>>
{
    public StringBuilderSalesDataWriter(ProcessingOptions options) : base(options)
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
