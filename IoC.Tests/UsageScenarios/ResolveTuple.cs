namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ResolveTuple
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=injection
            // $priority=02
            // $description=Resolve Tuple
            // $header=[Tuple](https://docs.microsoft.com/en-us/dotnet/api/system.tuple) has a specific number and sequence of elements which may be resolved from the box.
            // {
            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                .Bind<INamedService>().To<NamedService>(ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name"))
                .Container;

            // Resolve an instance of type Tuple<IService, INamedService>
            var tuple = container.Resolve<Tuple<IService, INamedService>>();
            // }
            // Check the items types
            tuple.Item1.ShouldBeOfType<Service>();
            tuple.Item2.ShouldBeOfType<NamedService>();
        }
    }
}
