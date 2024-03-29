namespace DataProcessing;

internal class SalesData
{
    public static async Task ProcessSalesDataSamplesAsync(ProcessingOptions options, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var logger = options.LoggerFactory.CreateLogger<SalesData>();

        logger.LogInformation("Processing UK sales data");
        var ukSalesDataProcessor = new SalesDataProcessor(options);
        var ukData = await ukSalesDataProcessor.ProcessAsync("UK_SalesData.txt", cancellationToken);

        var salesDataInspector = new FailedSalesDataInspector(options);

        if (ukData.HasFailedRows)
            salesDataInspector.InspectAll(ukData.FailedRows);

        cancellationToken.ThrowIfCancellationRequested();

        logger.LogInformation("Processing US sales data");
        var usSalesDataProcessor = new SalesDataProcessor(options, new CultureInfo("en-US"));
        var usData = await usSalesDataProcessor.ProcessAsync("US_SalesData.txt", cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        logger.LogInformation("Processing Italy sales data");
        var italySalesDataProcessor = new SalesDataProcessor(options, new CultureInfo("it-IT"));
        var italyData = await italySalesDataProcessor.ProcessAsync("IT_SalesData.txt", cancellationToken);

        if (ukData.HasProcessedData)
        {
            // TODO
        }
    }
}
