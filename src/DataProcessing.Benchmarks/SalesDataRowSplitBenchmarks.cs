using System.Text.RegularExpressions;

namespace DataProcessing.Benchmarks;

public class SalesDataRowSplitBenchmarks
{
    public readonly List<string[]> Results = new(SampleData.Rows.Length);

    [Benchmark]
    public void StringSplit()
    {
        Results.Clear();
        foreach (var row in SampleData.Rows)
        {
            var parts = row.Split('|');
            Results.Add(parts);
        }
    }

    [Benchmark]
    public void RegexSplit()
    {
        Results.Clear();
        foreach (var row in SampleData.Rows)
        {
            var parts = Regex.Split(row, @"\|");
            Results.Add(parts);
        }
    }
}
