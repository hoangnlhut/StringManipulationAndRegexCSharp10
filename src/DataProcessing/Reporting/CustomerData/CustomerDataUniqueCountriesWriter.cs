namespace DataProcessing.Reporting;

internal class CustomerDataUniqueCountriesWriter : DataWriter<IEnumerable<HistoricalCustomerData>>
{
    public CustomerDataUniqueCountriesWriter(ProcessingOptions options, CultureInfo cultureInfo) : base(options)
    {
    }

    public CustomerDataUniqueCountriesWriter(ProcessingOptions options) : this(options, options.ApplicationCulture)
    {
    }

    protected override Task WriteAsyncCore(
        string pathAndFileName, 
        IEnumerable<HistoricalCustomerData> data, 
        CancellationToken cancellationToken = default)
    {
        // TODO - Implementation
        return Task.CompletedTask;
    }
}
