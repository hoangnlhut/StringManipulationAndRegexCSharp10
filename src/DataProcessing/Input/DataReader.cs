using System.Runtime.CompilerServices;

namespace DataProcessing.Input;

internal sealed class DataReader
{
    public DataReader(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        Path = path;
    }

    public string Path { get; }

    public int RowsRead { get; private set; } = 0;

    public async IAsyncEnumerable<string> ReadRowsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
        using var reader = new StreamReader(stream, System.Text.Encoding.UTF8);

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var line = await reader.ReadLineAsync();
            
            if (line is null) 
                break;
            
            yield return line;
        }
    }

    public Task<string> ReadAllRowsAsync(CancellationToken cancellationToken = default) => File.ReadAllTextAsync(Path, cancellationToken);
}
