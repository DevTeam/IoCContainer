namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class DependencyTag
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=1 Basics
            // $priority=04
            // $description=Dependency tag
            // $header=Use a _tag_ to inject specific dependency from several bindings of the same types.
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().Tag("MyDep").To<Dependency>()
                // Configure autowiring and inject dependency tagged by "MyDep"
                .Bind<IService>().To<Service>(ctx => new Service(ctx.Container.Inject<IDependency>("MyDep")))
                .Container;

            // Resolve an instance
            var instance = container.Resolve<IService>();
            // }
            // Check the instance
            instance.ShouldBeOfType<Service>();
        }
    }
}
