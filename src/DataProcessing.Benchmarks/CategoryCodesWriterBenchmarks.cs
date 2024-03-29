using System.Text;

namespace DataProcessing.Benchmarks;

public class CategoryCodesWriterBenchmarks
{
    private ICollection<string> _salesData = Array.Empty<string>();

    [Params(1, 2, 100, 1000)]
    public int Size { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var list = new List<string>(Size);

        for (int i = 0; i < Size; i++)
        {
            list.Add($"ENG{i:d3}");
        }

        _salesData = list;
    }

    [Benchmark]
    public void WithCapacity()
    {
        var capacity = _salesData.Count * (6 + Environment.NewLine.Length);

        var stringBuilder = new StringBuilder(capacity);

        foreach (var item in _salesData)
        {
            stringBuilder.AppendLine(item);
        }
    }

    [Benchmark]
    public void WithoutCapacity()
    {
        var stringBuilder = new StringBuilder();

        foreach (var item in _salesData)
        {
            stringBuilder.AppendLine(item);
        }
    }
}