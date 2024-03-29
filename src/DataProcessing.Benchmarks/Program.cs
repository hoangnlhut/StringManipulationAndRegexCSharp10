global using BenchmarkDotNet.Configs;
global using DataProcessing.Benchmarks;
global using BenchmarkDotNet.Attributes;

using System.Globalization;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Diagnosers;

var config = ManualConfig.Create(DefaultConfig.Instance);
config.SummaryStyle = new SummaryStyle(CultureInfo.CurrentCulture, true, BenchmarkDotNet.Columns.SizeUnit.B, null);
config.AddDiagnoser(MemoryDiagnoser.Default);

BenchmarkRunner.Run<SalesDataParsingBenchmarks>(config);