namespace IoC.Benchmark
{
    using System.Collections.Generic;
    using BenchmarkDotNet.Attributes;
    using Features;

    [Config(typeof(BenchmarkConfig))]
    [BenchmarkCategory("instances")]
    public class Instances
    {
        private const int Count = 10000000;
        private static readonly IMutableContainer Container = IoC.Container.Create(CoreFeature.Set);
        internal static readonly IDictionary<string, int> NumberOfInstances = new Dictionary<string, int>();
        private static readonly Resolver<object> ResolverCase1 = CreateResolver(nameof(Case1), 1, 0);
        private static readonly Resolver<object> ResolverCase2 = CreateResolver(nameof(Case2), 2, 2);
        private static readonly Resolver<object> ResolverCase3 = CreateResolver(nameof(Case3), 2, 4);
        private static readonly Resolver<object> ResolverCase4 = CreateResolver(nameof(Case4), 3, 3);
        private static readonly Resolver<object> ResolverCase5 = CreateResolver(nameof(Case5), 3, 4);
        private static readonly Resolver<object> ResolverCase6 = CreateResolver(nameof(Case6), 5, 3);

        [Benchmark(OperationsPerInvoke = Count)]
        public void Case1()
        {
            for (var i = 0; i < Count; i++)
            {
                ResolverCase1(Configs.IoCContainerComplex, Configs.EmptyArgs);
            }
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public void Case2()
        {
            for (var i = 0; i < Count; i++)
            {
                ResolverCase2(Configs.IoCContainerComplex, Configs.EmptyArgs);
            }
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public void Case3()
        {
            for (var i = 0; i < Count; i++)
            {
                ResolverCase3(Configs.IoCContainerComplex, Configs.EmptyArgs);
            }
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public void Case4()
        {
            for (var i = 0; i < Count; i++)
            {
                ResolverCase4(Configs.IoCContainerComplex, Configs.EmptyArgs);
            }
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public void Case5()
        {
            for (var i = 0; i < Count; i++)
            {
                ResolverCase5(Configs.IoCContainerComplex, Configs.EmptyArgs);
            }
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public void Case6()
        {
            for (var i = 0; i < Count; i++)
            {
                ResolverCase6(Configs.IoCContainerComplex, Configs.EmptyArgs);
            }
        }

        private static Resolver<object> CreateResolver(string name, int levelsCount, int dependenciesCount)
        {
            var typeBuilder = new TestTypeBuilder(levelsCount, dependenciesCount);
            foreach (var type in typeBuilder.Types)
            {
                Container.Bind(type).To(type);
            }

            NumberOfInstances[name] = typeBuilder.Count;
            return Container.GetResolver<object>(typeBuilder.RootType);
        }
    }
}
