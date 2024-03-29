namespace DataProcessing;

internal abstract class Processor<T>
{
    public Processor(ProcessingOptions processingOptions)
    {
        ArgumentNullException.ThrowIfNull(processingOptions);

        Options = processingOptions;

        // Path to the directory holding the source data
        BaseInputPath = Path.Combine(Directory.GetCurrentDirectory(), processingOptions.InputDirectory);
    }

    protected ProcessingOptions Options { get; init; }
    protected string BaseInputPath { get; init; }

    public abstract Task<T> ProcessAsync(string filename, CancellationToken cancellationToken = default);
}
