namespace DataProcessing.Reporting;

internal class SalesDataSummaryReportWriter : DataWriter<IEnumerable<HistoricalSalesData>>
{
    public SalesDataSummaryReportWriter(ProcessingOptions options) : base(options)
    {
    }

    protected override async Task WriteAsyncCore(
        string pathAndFileName, 
        IEnumerable<HistoricalSalesData> data, 
        CancellationToken cancellationToken = default)
    {
        var formattedOutput = ProduceReportString(data, cancellationToken);

        foreach (var writer in OutputWriters)
        {
            await writer.WriteDataAsync(formattedOutput, pathAndFileName, cancellationToken);
        }
    }

    private string ProduceReportString(
        IEnumerable<HistoricalSalesData> salesData, 
        CancellationToken cancellationToken = default)
    {
        // TODO - Implementation
        return string.Empty;
    }
}
