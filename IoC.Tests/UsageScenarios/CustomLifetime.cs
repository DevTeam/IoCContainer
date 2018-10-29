namespace IoC.Tests.UsageScenarios
{
    using System.Linq.Expressions;
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
            // Create and configure the container
            using (var container = Container.Create())
            // Bind some dependency
            using (container.Bind<IDependency>().To<Dependency>())
            // Bind interface to implementation using the custom lifetime, based on the Singleton lifetime
            using (container.Bind<IService>().Lifetime(new MyTransientLifetime()).To<Service>())
            {
                // Resolve the singleton twice
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                // Check that instances from the parent container are equal
                instance1.ShouldBe(instance2);
            }
        }

        // Represents the custom lifetime based on the Singleton lifetime
        public class MyTransientLifetime : ILifetime
        {
            // Creates the instance of the Singleton lifetime
            private ILifetime _baseLifetime = new Lifetimes.SingletonLifetime();

            // Wraps the expression by the Singleton lifetime expression
            public Expression Build(Expression expression, IBuildContext buildContext)
                => buildContext.AddLifetime(expression, _baseLifetime);

            // Creates the similar lifetime to use with generic instances
            public ILifetime Create() => new MyTransientLifetime();

            // Select a container to resolve dependencies using the Singleton lifetime logic
            public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
                _baseLifetime.SelectResolvingContainer(registrationContainer, resolvingContainer);

            // Disposes the instance of the Singleton lifetime
            public void Dispose() => _baseLifetime.Dispose();
        }
        // }
    }
}
