using System.Text;

namespace DataProcessing.Reporting;

internal class CustomerDataUniqueCountriesWriter : DataWriter<IEnumerable<HistoricalCustomerData>>
{
    private readonly StringComparer _stringComparer;

    public CustomerDataUniqueCountriesWriter(ProcessingOptions options, CultureInfo cultureInfo) : base(options)
    {
        // 2nd params is ignore case
        _stringComparer =  StringComparer.Create(cultureInfo, true);
    }

    public CustomerDataUniqueCountriesWriter(ProcessingOptions options) : this(options, options.ApplicationCulture)
    {
    }

    protected override async Task WriteAsyncCore(
        string pathAndFileName, 
        IEnumerable<HistoricalCustomerData> data, 
        CancellationToken cancellationToken = default)
    {
        var countries = new SortedSet<string>(data.Select(x => x.Country), _stringComparer);

        var stringBuilder = new StringBuilder();
        foreach (var country in countries) 
        { 
            //stringBuilder.Append(country).Append(Environment.NewLine); equal to below statement
            stringBuilder.AppendLine(country);
        }

        foreach (var writer in OutputWriters)
        {
            await writer.WriteDataAsync(stringBuilder.ToString(), pathAndFileName, cancellationToken);
        }
    }
}
