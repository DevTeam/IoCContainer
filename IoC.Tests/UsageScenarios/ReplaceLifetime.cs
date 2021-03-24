#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ReplaceLifetime
    {
        [Fact]
        // $visible=true
        // $tag=2 Lifetimes
        // $priority=10
        // $description=Replacement of Lifetime
        // $header=It is possible to replace default lifetimes on your own one. The sample below shows how to count the number of attempts to resolve [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instances.
        // {
        public void Run()
        {
            var counter = new Mock<ICounter>();

            using var container = Container
                .Create()
                .Bind<ICounter>().To(ctx => counter.Object)
                // Replace the Singleton lifetime with a custom lifetime
                .Bind<ILifetime>().Tag(Lifetime.Singleton).To<MySingletonLifetime>(
                    // Select the constructor
                    ctx => new MySingletonLifetime(
                        // Inject the singleton lifetime from the parent container for partially delegating logic
                        ctx.Container.Parent.Inject<ILifetime>(Lifetime.Singleton),
                        // Inject a counter to store the number of created instances
                        ctx.Container.Inject<ICounter>()))
                // Configure the container as usual
                .Bind<IDependency>().To<Dependency>()
                // Bind using the custom implementation of Singleton lifetime
                .Bind<IService>().As(Lifetime.Singleton).To<Service>()
                .Container;

            // Resolve the singleton twice using the custom lifetime
            var instance1 = container.Resolve<IService>();
            var instance2 = container.Resolve<IService>();

            // Check that instances are equal
            instance1.ShouldBe(instance2);

            // Check the number of created instances
            counter.Verify(i => i.Increment(), Times.Exactly(2));
        }

        // Represents the instance counter
        public interface ICounter
        {
            void Increment();
        }

        public class MySingletonLifetime : ILifetime
        {
            // Stores 'IncrementCounter' method info to the static field
            private static readonly MethodInfo IncrementCounterMethodInfo = typeof(MySingletonLifetime).GetTypeInfo().DeclaredMethods.Single(i => i.Name == nameof(IncrementCounter));

            private readonly ILifetime _baseSingletonLifetime;
            private readonly ICounter _counter;

            // Stores the base lifetime and the instance counter
            public MySingletonLifetime(ILifetime baseSingletonLifetime, ICounter counter)
            {
                _baseSingletonLifetime = baseSingletonLifetime;
                _counter = counter;
            }

            public Expression Build(IBuildContext context, Expression expression)
            {
                // Builds expression using base lifetime
                expression = _baseSingletonLifetime.Build(context, expression);

                // Defines `this` variable to store the reference to the current lifetime instance to call internal method 'IncrementCounter'
                var thisVar = Expression.Constant(this);

                // Creates a code block
                return Expression.Block(
                    // Adds the expression to call the method 'IncrementCounter' for the current lifetime instance
                    Expression.Call(thisVar, IncrementCounterMethodInfo),
                    // Returns the expression to create an instance
                    expression);
            }

            // Creates a similar lifetime to use with generic instances
            public ILifetime CreateLifetime() => new MySingletonLifetime(_baseSingletonLifetime.CreateLifetime(), _counter);

            // Select a container to resolve dependencies using the Singleton lifetime logic
            public IContainer SelectContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
                _baseSingletonLifetime.SelectContainer(registrationContainer, resolvingContainer);

            // Disposes the instance of the Singleton lifetime
            public void Dispose() => _baseSingletonLifetime.Dispose();

            // Just counts the number of requested instances
            internal void IncrementCounter() => _counter.Increment();
        }
        // }
    }
}
#endif