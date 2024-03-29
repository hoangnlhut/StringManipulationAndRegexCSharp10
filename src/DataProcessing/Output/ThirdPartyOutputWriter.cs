namespace DataProcessing;

internal sealed class ThirdPartyOutputWriter : IOutputWriter
{
    public Task WriteDataAsync(string data, string pathandFileName, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(pathandFileName);

        var exporter = new ThirdParty.Exporter.SomeCleverExporter(pathandFileName);
        return exporter.WriteDataAsync(data, cancellationToken);
    }
}
