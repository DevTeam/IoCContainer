namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class Bindings
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=basic
            // $priority=01
            // $description=Bindings
            // $header=It is possible to bind any number of types.
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind using several tags
                .Bind<IService>().Bind<IAnotherService>().Tag("abc").To<Service>()
                .Container;

            // Resolve instances using tags
            var instance1 = container.Resolve<IService>("abc".AsTag());
            var instance2 = container.Resolve<IAnotherService>("abc".AsTag());

            // }
            // Check the instances
            instance1.ShouldBeOfType<Service>();
            instance2.ShouldBeOfType<Service>();
        }
    }
}
