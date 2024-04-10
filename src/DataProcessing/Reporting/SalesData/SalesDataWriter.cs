namespace DataProcessing.Reporting;

internal sealed class SalesDataWriter : DataWriter<IEnumerable<HistoricalSalesData>>
{
    public SalesDataWriter(ProcessingOptions options) : base(options)
    {
    }

    protected override async Task WriteAsyncCore(
        string pathAndFileName, 
        IEnumerable<HistoricalSalesData> data, 
        CancellationToken cancellationToken = default)
    {
         var formattedOutput = string.Empty;

        foreach (var item in data)
        {
            var row = string.Join(',', item.GetFormattedComponents(Options.ApplicationCulture));
            formattedOutput += string.Concat(row, Environment.NewLine);
        }

        foreach (var writer in OutputWriters)
        {
            await writer.WriteDataAsync(formattedOutput, pathAndFileName, cancellationToken);
        }
    }
}
