using System.Text.RegularExpressions;

namespace DataProcessing.Benchmarks;

public class CompiledRegexBenchmarks
{
    private const string Row = "[AA] [54a96b1b-77e3-43d4-8a9b-4ad969690217] [Italien] [Some Company J]";

    private Regex? _compiledRegex;

    [GlobalSetup]
    public void Setup() => _compiledRegex = new Regex(@"\[(?<data>.*?)\]", 
        RegexOptions.Compiled, TimeSpan.FromSeconds(1));

    private MatchCollection? _result = null;

    [Benchmark]
    public void NonCompiled() => _result = Regex.Matches(Row, @"\[(?<data>.*?)\]", 
        RegexOptions.None, TimeSpan.FromSeconds(1));

    [Benchmark]
    public void Compiled() => _result = _compiledRegex!.Matches(Row);
}
