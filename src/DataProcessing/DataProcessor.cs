namespace DataProcessing;

internal static class DataProcessor
{
    public static ValueTask ProcessAsync(ProcessingOptions options, CancellationToken cancellationToken = default)
    {
        if (options.OutputPath != string.Empty)
            Directory.CreateDirectory(options.OutputPath);

        return ValueTask.CompletedTask;
    }
}
