using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using IoC.Benchmark;

[assembly:Config(typeof(BenchmarkConfig))]
[assembly: Orderer(SummaryOrderPolicy.FastestToSlowest)]
[assembly: MarkdownExporterAttribute.GitHub]
[assembly: CategoriesColumn]
