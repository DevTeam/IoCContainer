#if !NET40 && !NETCOREAPP1_0
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ValueTuples
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=3 BCL types
            // $priority=01
            // $description=Value Tuples
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                .Bind<INamedService>().To<NamedService>(ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name"))
                .Container;
            {
                // Resolve an instance of type (IService service, INamedService namedService)
                var valueTuple = container.Resolve<(IService service, INamedService namedService)>();
                // }
                // Check types
                valueTuple.service.ShouldBeOfType<Service>();
                valueTuple.namedService.ShouldBeOfType<NamedService>();
                // {
            }

            // }
        }
    }
}
#endif