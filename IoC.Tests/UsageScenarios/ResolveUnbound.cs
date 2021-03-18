// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Features;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ResolveUnbound
    {
        [Fact]
        // $visible=true
        // $tag=5 Advanced
        // $priority=02
        // $description=Resolve Unbound
        // $header=The feature _ResolveUnboundFeature_ allows you to resolve any implementation type from the container regardless of whether or not you specifically bound it.
        // {
        public void Run()
        {
            using var container = Container
                .Create()
                .Using<ResolveUnboundFeature>()
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
