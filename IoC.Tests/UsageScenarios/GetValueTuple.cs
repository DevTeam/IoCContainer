#if !NET40 && !NETCOREAPP1_0
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class GetValueTuple
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=02
            // $description=Get ValueTuple
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            using (container.Bind<INamedService>().To<NamedService>(
                ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name")))
            {
                // Resolve ValueTuple
                var valueTuple = container.Get<(IService service, INamedService namedService)>();

                valueTuple.service.ShouldBeOfType<Service>();
                valueTuple.namedService.ShouldBeOfType<NamedService>();
            }
            // }
        }
    }
}
#endif