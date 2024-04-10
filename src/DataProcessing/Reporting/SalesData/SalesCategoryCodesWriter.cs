using System.Text;

namespace DataProcessing.Reporting;

internal sealed class SalesCategoryCodesWriter : DataWriter<IEnumerable<HistoricalSalesData>>
{
    public SalesCategoryCodesWriter(ProcessingOptions options) : base(options)
    {
    }

    protected override async Task WriteAsyncCore(
        string pathAndFileName, 
        IEnumerable<HistoricalSalesData> salesData, 
        CancellationToken cancellationToken = default)
    {
        //using sorting LINQ
        var sortedCodes = salesData.
            Select(s => s.Category.Code)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(s => s, StringComparer.OrdinalIgnoreCase).ToArray();

        var capacity = sortedCodes.Length * (6 + Environment.NewLine.Length);
        var stringBuilder = new StringBuilder(capacity);

        foreach (var item in sortedCodes)
        {
            stringBuilder.AppendLine(item);
        }

        foreach (var writer in OutputWriters)
        {
            await writer.WriteDataAsync(stringBuilder.ToString(), pathAndFileName, cancellationToken);
        }

    }
}
