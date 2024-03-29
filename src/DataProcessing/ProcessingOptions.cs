namespace DataProcessing;

internal sealed class ProcessingOptions
{
    private readonly List<IOutputWriter> _outputWriters = new();

    public ProcessingOptions(ILoggerFactory loggerFactory) 
        : this(new CultureInfo("en-GB"), new SessionContext(string.Empty, string.Empty, string.Empty), loggerFactory) { }

    public ProcessingOptions(CultureInfo applicationCulture, SessionContext sessionContext, ILoggerFactory loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(applicationCulture);
        ArgumentNullException.ThrowIfNull(sessionContext);
        ArgumentNullException.ThrowIfNull(loggerFactory);

        ApplicationCulture = applicationCulture;
        SessionContext = sessionContext;
        LoggerFactory = loggerFactory;
    }

    public string OutputPath { get; init; } = string.Empty;
    public string InputDirectory { get; init; } = "_SourceData";

    public bool ProcessSalesData { get; init; } = true;
    public bool ProcessCustomerData { get; init; } = true;
    public bool ProcessLogData { get; init; } = true;

    public CultureInfo ApplicationCulture { get; }
    public SessionContext SessionContext { get; }
    public ILoggerFactory LoggerFactory { get; }

    public IReadOnlyCollection<IOutputWriter> OutputWriters => _outputWriters;

    public ProcessingOptions AddOutputWriter(IOutputWriter outputWriter)
    {
        _outputWriters.Add(outputWriter);
        return this;
    }
}