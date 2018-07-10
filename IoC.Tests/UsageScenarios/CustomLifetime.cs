namespace IoC.Tests.UsageScenarios
{
    using System.Linq.Expressions;
    using Extensibility;
    using Shouldly;
    using Xunit;

    public class CustomLifetime
    {
        [Fact]
        // $visible=true
        // $tag=customization
        // $priority=00
        // $description=Custom Lifetime
        // {
        public void Run()
        {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().Lifetime(new MyTransientLifetime()).To<Service>())
            {
                // Resolve an instance
                var instance = container.Resolve<IService>();

                instance.ShouldBeOfType<Service>();
            }
        }

        public class MyTransientLifetime : ILifetime
        {
            public Expression Build(Expression expression, IBuildContext buildContext, object state = default(object))
                => expression;

            public ILifetime Clone() => new MyTransientLifetime();
        }
        // }
    }
}
