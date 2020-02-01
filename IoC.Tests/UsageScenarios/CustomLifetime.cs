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
        // $header=Custom lifetimes allow to implement your own logic controlling every aspects of resolved instances. Also you could use the class [_KeyBasedLifetime<>_](IoCContainer/blob/master/IoC/Lifetimes/KeyBasedLifetime.cs) as a base for others.
        // {
        public void Run()
        {
            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind an interface to an implementation using the custom lifetime, based on the Singleton lifetime
                .Bind<IService>().Lifetime(new MyTransientLifetime()).To<Service>()
                .Container;
            
            // Resolve the singleton twice
            var instance1 = container.Resolve<IService>();
            var instance2 = container.Resolve<IService>();

            // Check that instances from the parent container are equal
            instance1.ShouldBe(instance2);
        }

        // Represents the custom lifetime based on the Singleton lifetime
        public class MyTransientLifetime : ILifetime
        {
            // Creates the instance of the Singleton lifetime
            private ILifetime _baseLifetime = new Lifetimes.SingletonLifetime();

            // Wraps the expression by the Singleton lifetime expression
            public Expression Build(IBuildContext context, Expression expression)
                => context.AddLifetime(expression, _baseLifetime);

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
