namespace DataProcessing;

internal static class DataProcessor
{
    public static async ValueTask ProcessAsync(ProcessingOptions options, CancellationToken cancellationToken = default)
    {
        if (options.OutputPath != string.Empty)
            Directory.CreateDirectory(options.OutputPath);

        if (options.ProcessSalesData)
            await SalesData.ProcessSalesDataSamplesAsync(options, cancellationToken);

        if (options.ProcessCustomerData)
            await CustomerData.ProcessCustomerDataSamplesAsync(options, cancellationToken);

        if (options.ProcessLogData)
            await LogData.ProcessLogDataSamplesAsync(options, cancellationToken);
    }
}
