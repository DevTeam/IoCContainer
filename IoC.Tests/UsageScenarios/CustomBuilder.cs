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
            // Create and configure the container
            using (var container = Container.Create())
            // Bind some dependency
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            // Register the custom builder
            using (container.Bind<IBuilder>().To<MyBuilder>())
            {
                // Resolve instances
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                // Check that instances are equal
                instance1.ShouldBe(instance2);
            }
        }

        // This custom builder just adds the Singleton lifetime for any instances
        public class MyBuilder : IBuilder
        {
            public Expression Build(Expression expression, IBuildContext buildContext) =>
                // Add the Singleton lifetime for any instances
                buildContext.AddLifetime(expression, new Lifetimes.SingletonLifetime());
        }
        // }
    }
}
