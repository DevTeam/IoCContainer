// ReSharper disable UnusedMember.Local
// ReSharper disable ClassNeverInstantiated.Global
namespace IoC.Benchmark
{
    using BenchmarkDotNet.Running;

    public class Program
    {
        static void Main(string[] args) => 
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
