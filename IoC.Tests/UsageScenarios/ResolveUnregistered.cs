// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Features;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ResolveUnregistered
    {
        [Fact]
        // $visible=true
        // $tag=injection
        // $priority=02
        // $description=Resolve Unregistered
        // $header=The feature _ResolveUnregisteredFeature_ allows you to resolve any concrete type from the container regardless of whether or not you specifically registered it.
        // {
        public void Run()
        {
            // Create and configure the container
            using var container = Container
                .Create()
                .Using<ResolveUnregisteredFeature>()
                .Bind<IDependency>().To<Dependency>()
                .Container;

            // Resolve an instance of unregistered type
            container.Resolve<Service>();
        }

        class Service
        {
            public Service(OtherService otherService, IDependency dependency) { }
        }

        class OtherService { }
        // }
    }
}
