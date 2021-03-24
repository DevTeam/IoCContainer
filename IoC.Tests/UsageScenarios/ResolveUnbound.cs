// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ResolveUnbound
    {
        [Fact]
        // $visible=true
        // $tag=1 Basics
        // $priority=02
        // $description=Resolve Unbound
        // $header=By default, all instances of non-abstract or value types are ready to resolve and inject as dependencies.
        // $footer=In the case when context arguments contain instances of suitable types and a container has no appropriate bindings context arguments will be used for resolving and injections.
        // {
        public void Run()
        {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Container;

            // Resolve an instance of unregistered type
            var instance = container.Resolve<Service<int>>(99);
            instance.OtherService.Value.ShouldBe(99);
            instance.OtherService.Count.ShouldBe(10);
        }

        class Service<T>
        {
            public Service(OtherService<T> otherService, IDependency dependency)
            {
                OtherService = otherService;
            }

            public OtherService<T> OtherService { get; }
        }

        class OtherService<T>
        {
            public OtherService(T value, int count = 10)
            {
                Value = value;
                Count = count;
            }

            public T Value { get; }

            public long Count { get; }
        }
        // }
    }
}
