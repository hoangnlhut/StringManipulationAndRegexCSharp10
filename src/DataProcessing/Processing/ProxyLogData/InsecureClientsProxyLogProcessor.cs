namespace DataProcessing;

internal class InsecureClientsProxyLogProcessor : Processor<InsecureHostsByIPAddressDictionary>
{
    // language=regex
    private const string RegexPattern = @"^\[WARN\].+?Z\s(?<ip>(?:\d{1,3}\.){3}\d{1,3}).+?(?:http:\/\/)(?<hostname>[^:\/]+)[^\s]* Insecure scheme detected.*$";

    public InsecureClientsProxyLogProcessor(ProcessingOptions processingOptions) : base(processingOptions)
    {
    }

    public override async Task<InsecureHostsByIPAddressDictionary> ProcessAsync(string filename, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filename);

        var dataReader = new DataReader(Path.Combine(BaseInputPath, filename));
        var logData = await dataReader.ReadAllRowsAsync(cancellationToken);

        // TODO - Match

        var insecureHostsByIp = new InsecureHostsByIPAddressDictionary();

        // TODO - Process Matches

        return insecureHostsByIp;
    }
}
