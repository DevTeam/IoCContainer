namespace IoC.Tests.UsageScenarios
{
    using System.Linq.Expressions;
    using Shouldly;
    using Xunit;

    public class CustomBuilder
    {
        [Fact]
        // $visible=true
        // $tag=customization
        // $priority=00
        // $description=Custom Builder
        // {
        public void Run()
        {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            // Add custom builder
            using (container.Bind<IBuilder>().To<MyBuilder>())
            {
                // Resolve an instance
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                instance1.ShouldBeOfType<Service>();
                instance1.ShouldBe(instance2);
            }
        }

        // This custom builder just adds the Singleton lifetime for any instances
        // Also it can be used, for instance, to create proxies
        public class MyBuilder : IBuilder
        {
            public Expression Build(Expression expression, IBuildContext buildContext)
            {
                return buildContext.AppendLifetime(expression, new Lifetimes.SingletonLifetime());
            }
        }
        // }
    }
}
