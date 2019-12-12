// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class GenericAutowiring
    {
        [Fact]
        // $visible=true
        // $tag=binding
        // $priority=00
        // $description=Generic Autowiring
        // $header=Auto-writing of generic types as simple as auto-writing of other types. Just use a generic parameters markers like _TT_, _TT1_, _TT2_ and etc., create your own generic parameters markers or bind open generic types.
        // {
        public void Run()
        {
            // Create and configure the container using autowiring
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind using the predefined generic parameters marker TT (or TT1, TT2, TT3 ...)
                .Bind<IService<TT>>().To<Service<TT>>()
                // Bind using the custom generic parameters marker TCustom
                .Bind<IService<TTMy>>().Tag("custom marker").To<Service<TTMy>>()
                // Bind using the open generic type
                .Bind(typeof(IService<>)).Tag("open type").To(typeof(Service<>))
                .Container;

            // Resolve a generic instance
            var instances = container.Resolve<ICollection<IService<int>>>();

            instances.Count.ShouldBe(3);
            // Check the instance's type
            foreach (var instance in instances)
            {
                instance.ShouldBeOfType<Service<int>>();
            }
        }

        // Custom generic type marker using predefined attribute `GenericTypeArgument`
        [GenericTypeArgument]
        class TTMy { }
    }
}
