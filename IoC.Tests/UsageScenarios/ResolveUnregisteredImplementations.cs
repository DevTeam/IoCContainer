// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Features;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ResolveUnregisteredImplementations
    {
        [Fact]
        // $visible=true
        // $tag=injection
        // $priority=02
        // $description=Resolve Unregistered Implementations
        // $header=The feature _ResolveUnregisteredImplementationsFeature_ allows you to resolve any implementation type from the container regardless of whether or not you specifically registered it.
        // {
        public void Run()
        {
            // Create and configure the container
            using var container = Container
                .Create()
                .Using<ResolveUnregisteredImplementationsFeature>()
                .Bind<IDependency>().To<Dependency>()
                .Container;

            // Resolve an instance of unregistered type
            container.Resolve<Service<int>>();
        }

        class Service<T>
        {
            public Service(OtherService<T> otherService, IDependency dependency) { }
        }

        class OtherService<T>
        {
            public OtherService(T value) { }
        }
        // }
    }
}
