namespace IoC.Benchmark
{
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;

    internal class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddColumn(new TagColumn("Number of Instances", name => Instances.NumberOfInstances.TryGetValue(name, out var cnt) ? cnt.ToString(): string.Empty));
        }
    }
}