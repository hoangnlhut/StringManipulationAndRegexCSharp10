namespace DataProcessing.Reporting;

internal sealed class SalesCategoryCodesWriter : DataWriter<IEnumerable<HistoricalSalesData>>
{
    public SalesCategoryCodesWriter(ProcessingOptions options) : base(options)
    {
    }

    protected override Task WriteAsyncCore(
        string pathAndFileName, 
        IEnumerable<HistoricalSalesData> salesData, 
        CancellationToken cancellationToken = default)
    {
        // TODO - Implementation
        return Task.CompletedTask;
    }
}
