using System.Globalization;
using System.Text.RegularExpressions;

namespace DataProcessing.Benchmarks;

public class SalesDataParsingBenchmarks
{
    private static readonly CultureInfo CultureInfo = new("en-GB");

    private Regex? _compiledRegex;

    [GlobalSetup]
    public void Setup()
    {
        var pattern =
            @"^(?<productName>.+)\|" +
            @"(?<quantity>\d+)\|" +
            //@$"((?<symbol>{_cultureInfo.NumberFormat.CurrencySymbol})?(?<amount>\d[.\d,]*))\|" +
            @"((?<symbol>" +
            CultureInfo.NumberFormat.CurrencySymbol +
            ")?(?<amount>(?:[0-9]{0,3}(?:" +
            Regex.Escape(CultureInfo.NumberFormat.CurrencyGroupSeparator) +
            "[0-9]{3})*(?:" +
            Regex.Escape(CultureInfo.NumberFormat.CurrencyDecimalSeparator) +
            @"[0-9]{0,2})?)))\|" +
            @"(?<tax>\d+)\|(?<date>.{6,})\|(?<productCode>.+\s*?[:-]\s*.+)\|(?<categoryCode>[a-zA-Z]{3}[0-3]{3}):(?<categoryDesc>.*)$";

        _compiledRegex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled, TimeSpan.FromSeconds(10)); // DEMO M6 - Adding Timespan
    }

    internal readonly List<HistoricalSalesData> Results = new(SampleData.Rows.Length);

    [Benchmark]
    public void WithoutRegex()
    {
        Results.Clear();
        foreach (var row in SampleData.Rows)
        {
            var rowParts = row.Split('|');
            if (HistoricalSalesData.TryCreateFromHistoricalData(rowParts, CultureInfo, out var result))
            {
                Results.Add(result);
            }
        }
    }

    [Benchmark]
    public void WithRegex()
    {
        Results.Clear();
        foreach (var row in SampleData.Rows)
        {
            if (HistoricalSalesData.TryCreateFromRow(row, _compiledRegex!, CultureInfo, out var result))
            {
                Results.Add(result);
            }
        }
    }
}
