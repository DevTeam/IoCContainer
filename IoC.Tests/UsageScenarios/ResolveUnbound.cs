// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using Xunit;

    public class ResolveUnbound
    {
        [Fact]
        // $visible=true
        // $tag=1 Basics
        // $priority=02
        // $description=Resolve Unbound
        // {
        public void Run()
        {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Container;

            // Resolve an instance of unregistered type
            container.Resolve<Service<int>>();
        }

        class Service<T>
        {
            public Service(OtherService<T> otherService, IDependency dependency) { }
        }

        class OtherService<T>  { public OtherService(T value) { } }
        // }
    }
}
