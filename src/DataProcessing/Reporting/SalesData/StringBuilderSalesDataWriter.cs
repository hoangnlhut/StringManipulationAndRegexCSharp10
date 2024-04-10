using System.Text;

namespace DataProcessing.Reporting;

internal sealed class StringBuilderSalesDataWriter : DataWriter<IEnumerable<HistoricalSalesData>>
{
    public StringBuilderSalesDataWriter(ProcessingOptions options) : base(options)
    {
    }

    protected override async Task WriteAsyncCore(
        string pathAndFileName, 
        IEnumerable<HistoricalSalesData> data, 
        CancellationToken cancellationToken = default)
    {
        var stringBuilder = new StringBuilder();

        foreach (var item in data)
        {
            item.ProduceRow(Options.ApplicationCulture, stringBuilder);
        }

        foreach (var writer in OutputWriters)
        {
            await writer.WriteDataAsync(stringBuilder.ToString(), pathAndFileName, cancellationToken);
        }
    }
}
