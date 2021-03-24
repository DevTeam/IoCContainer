namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "RedundantTypeArgumentsOfMethod")]
    public class SeveralContracts
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=1 Basics
            // $priority=02
            // $description=Several contracts
            // $header=It is possible to bind several types to a single implementation.
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<Service, IService, IAnotherService>().To<Service>()
                .Container;

            // Resolve instances
            var instance1 = container.Resolve<IService>();
            var instance2 = container.Resolve<IAnotherService>();
            // }
            // Check instances
            instance1.ShouldBeOfType<Service>();
            instance2.ShouldBeOfType<Service>();
        }
    }
}
