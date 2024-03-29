namespace DataProcessing;

internal class LogData
{
    public static async Task ProcessLogDataSamplesAsync(ProcessingOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(options);

        var logger = options.LoggerFactory.CreateLogger<LogData>();

        logger.LogInformation("Processing log data");

        var logDataProcessor = new InsecureClientsProxyLogProcessor(options);
        var insercureClients = await logDataProcessor.ProcessAsync("ServerLog.txt", cancellationToken);

        if (insercureClients.Any())
        {
            // TODO
        }
    }
}
