namespace ThirdParty.Exporter;

public class SomeCleverExporter
{
    private readonly string _path;

    public SomeCleverExporter(string path) => _path = path;

    public async Task WriteDataAsync(string data, CancellationToken cancellationToken = default)
    {
        if (data is null)
            return;

        cancellationToken.ThrowIfCancellationRequested();

        // Imagine this handles writing to some more complex file type such as PDF!

        using StreamWriter outputFile = new(_path, false, System.Text.Encoding.UTF8);
        await outputFile.WriteAsync(data);
    }
}
