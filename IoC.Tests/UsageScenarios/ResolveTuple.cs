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
            // {
            // Create and configure the container
            using var container = Container.Create();
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            using (container.Bind<INamedService>().To<NamedService>(
                ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name")))
            {
                // Resolve an instance of type Tuple<IService, INamedService>
                var tuple = container.Resolve<Tuple<IService, INamedService>>();
                // }
                // Check the items types
                tuple.Item1.ShouldBeOfType<Service>();
                tuple.Item2.ShouldBeOfType<NamedService>();
                // {
            }

            // }
        }
    }
}
