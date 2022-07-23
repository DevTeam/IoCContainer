// ReSharper disable UnusedMember.Local
// ReSharper disable ClassNeverInstantiated.Global
namespace IoC.Benchmark
{
    using System;
    using System.Linq;
    using BenchmarkDotNet.Running;

    public class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 1)
            {
                var name = args[0].ToLowerInvariant();
                var benchmarkType = (
                    from type in typeof(Program).Assembly.GetTypes()
                    where type.Name.ToLowerInvariant() == name
                    where typeof(BenchmarkBase).IsAssignableFrom(type)
                    select type).FirstOrDefault();

                if (benchmarkType == null)
                {
                    Console.Error.WriteLine($"Cannot find the benchmark \"{args[0]}\".");
                    return 1;
                }

                var benchmark = (BenchmarkBase)Activator.CreateInstance(benchmarkType);
                if (benchmark == null)
                {
                    Console.Error.WriteLine($"Cannot create the benchmark \"{args[0]}\".");
                    return 1;
                }

                benchmark.Setup();
                Console.WriteLine($"Running benchmark \"{args[0]}\".");
                //benchmark.ThisByCR();
                return 0;
            }

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            return 0;
        }
    }
}
