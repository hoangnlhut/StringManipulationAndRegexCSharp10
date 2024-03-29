namespace DataProcessing;

internal class CustomerData
{
    public static async Task ProcessCustomerDataSamplesAsync(ProcessingOptions options, CancellationToken cancellationToken = default)
    {
        var logger = options.LoggerFactory.CreateLogger<CustomerData>();

        logger.LogInformation("Processing DK customer data");

        var customerDataProcessor = new CustomerDataProcessor(options);
        var customerData = await customerDataProcessor.ProcessAsync("DK_CustomerData.txt", cancellationToken);

        if (customerData.PriorityCustomers.Any())
        {
            // TODO
        }
    }
}