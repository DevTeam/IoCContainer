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
            // $group=01
            // $priority=02
            // $description=Resolve Tuple
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            using (container.Bind<INamedService>().To<NamedService>(
                ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name")))
            {
                // Resolve Tuple
                var tuple = container.Resolve<Tuple<IService, INamedService>>();

                tuple.Item1.ShouldBeOfType<Service>();
                tuple.Item2.ShouldBeOfType<NamedService>();
            }
            // }
        }
    }
}
