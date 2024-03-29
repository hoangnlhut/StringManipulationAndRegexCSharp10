namespace DataProcessing;

internal static class DataProcessor
{
    public static async ValueTask ProcessAsync(ProcessingOptions options, CancellationToken cancellationToken = default)
    {
        if (options.OutputPath != string.Empty)
            Directory.CreateDirectory(options.OutputPath);

        if (options.ProcessSalesData)
            await SalesData.ProcessSalesDataSamplesAsync(options, cancellationToken);
    }
}
