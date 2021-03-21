// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Features;
    using Shouldly;
    using Xunit;

    public class ResolveFromArgs
    {
        [Fact]
        // $visible=true
        // $tag=5 Advanced
        // $priority=02
        // $description=Resolve Unbound form context arguments
        // $header=The feature _ResolveUnboundFeature_ allows you to resolve any instances from the context arguments regardless of whether or not you specifically bound it.
        // {
        public void Run()
        {
            using var container = Container
                .Create()
                .Using<ResolveUnboundFeature>()
                .Bind<IDependency>().To<Dependency>()
                .Container;

            var factory = container.Resolve<Func<string, int, Service<int>>>();
            var service = factory("Some name", 99);
            
            service.Name.ShouldBe("Some name");
            service.Id.ShouldBe(99);
        }

        class Service<T>
        {
            public Service(IDependency dependency, string name, T id)
            {
                Id = id;
                Name = name;
            }

            public T Id { get; }
            
            public string Name { get; }
        }
        // }
    }
}
