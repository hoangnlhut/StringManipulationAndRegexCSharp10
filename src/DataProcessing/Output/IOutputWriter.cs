namespace DataProcessing;

internal interface IOutputWriter
{
    public Task WriteDataAsync(string data, string pathandFileName, CancellationToken cancellationToken = default);
}
