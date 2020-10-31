namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Tuple
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=3 BCL types
            // $priority=01
            // $description=Tuple
            // $header=[Tuple](https://docs.microsoft.com/en-us/dotnet/api/system.tuple) has a specific number and sequence of elements which may be resolved at the same time.
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                .Bind<INamedService>().To<NamedService>(ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name"))
                .Container;

            // Resolve an instance of type Tuple<IService, INamedService>
            var tuple = container.Resolve<Tuple<IService, INamedService>>();
            // }
            // Check items
            tuple.Item1.ShouldBeOfType<Service>();
            tuple.Item2.ShouldBeOfType<NamedService>();
        }
    }
}
