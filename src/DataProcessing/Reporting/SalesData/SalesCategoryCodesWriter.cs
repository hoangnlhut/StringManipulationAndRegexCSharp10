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
        //using sorting LINQ
        var sortedCodes = salesData.
            Select(s => s.Category.Code)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(s => s, StringComparer.OrdinalIgnoreCase);

        // TODO - Implementation


        return Task.CompletedTask;
    }
}
