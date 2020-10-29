// ReSharper disable InconsistentNaming
namespace IoC.Benchmark
{
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;
    using Model;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    // ReSharper disable once UnusedType.Global
    public class Complex: BenchmarkBase
    {
        public override TActualContainer CreateContainer<TActualContainer, TAbstractContainer>()
        {
            var abstractContainer = new TAbstractContainer();
            var typeBuilder = TestTypeBuilder.Default;
            foreach (var type in typeBuilder.Types.Where(i => i != typeBuilder.RootType))
            {
                abstractContainer.Register(type, type);
            }

            abstractContainer.Register(typeof(IServiceRoot), typeBuilder.RootType);
            return abstractContainer.CreateActualContainer();
        }
    }
}