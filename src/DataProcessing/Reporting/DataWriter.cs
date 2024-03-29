namespace DataProcessing.Reporting;

internal abstract class DataWriter<T>
{
    protected DataWriter(ProcessingOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        Options = options;
        SessionContext = options.SessionContext;
    }

    protected ProcessingOptions Options { get; }
    protected SessionContext SessionContext { get; }
    protected IEnumerable<IOutputWriter> OutputWriters => Options.OutputWriters;

    public Task WriteAsync(string fileName, T data, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("A valid filename is required.", nameof(fileName));

        ArgumentNullException.ThrowIfNull(data);

        return WriteAsyncCore(Path.Combine(Options.OutputPath, fileName), data, cancellationToken);
    }

    protected abstract Task WriteAsyncCore(string pathAndFileName, T source, CancellationToken cancellationToken = default);
}
