using System.Text;

namespace DataProcessing.Reporting;

internal class InsecureHostsReportWriter : DataWriter<InsecureHostsByIPAddressDictionary>
{
    private const int LineSeparatorLength = 30;

    public InsecureHostsReportWriter(ProcessingOptions options) : base(options)
    {
    }

    protected override async Task WriteAsyncCore(
        string pathAndFileName, 
        InsecureHostsByIPAddressDictionary data, 
        CancellationToken cancellationToken = default)
    {
        var sb = new StringBuilder();

        foreach (var (ip, hostnames) in data)
        {
            // TODO
        }

        foreach (var writer in OutputWriters)
        {
            await writer.WriteDataAsync(sb.ToString(), pathAndFileName, cancellationToken);
        }
    }
}
