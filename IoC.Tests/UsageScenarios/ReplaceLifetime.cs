#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Extensibility;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ReplaceLifetime
    {
        [Fact]
        // $visible=true
        // $tag=customization
        // $priority=00
        // $description=Replace Lifetime
        // {
        public void Run()
        {
            var counter = new Mock<ICounter>();

            // Create a container
            using (var container = Container.Create())
            using (container.Bind<ICounter>().To(ctx => counter.Object))
            // Replace the Singleton lifetime
            using (container.Bind<ILifetime>().Tag(Lifetime.Singleton).To<MySingletonLifetime>(
                    // Select the constructor
                    ctx => new MySingletonLifetime(
                        // Inject the singleton lifetime from the parent container to use its logic
                        ctx.Container.Parent.Inject<ILifetime>(Lifetime.Singleton),
                        // Inject a counter
                        ctx.Container.Inject<ICounter>())))
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            // Use the custom implementation of Singleton lifetime
            using (container.Bind<IService>().As(Lifetime.Singleton).To<Service>())
            {
                // Resolve one instance twice using the custom Singletine lifetime
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                instance1.ShouldBe(instance2);
            }

            counter.Verify(i => i.Increment(), Times.Exactly(2));
        }

        public interface ICounter
        {
            void Increment();
        }

        public class MySingletonLifetime : ILifetime
        {
            private static readonly MethodInfo IncrementCounterMethodInfo = typeof(MySingletonLifetime).GetTypeInfo().DeclaredMethods.Single(i => i.Name == nameof(IncrementCounter));
            private readonly ILifetime _baseSingletonLifetime;
            private readonly ICounter _counter;

            public MySingletonLifetime(ILifetime baseSingletonLifetime, ICounter counter)
            {
                _baseSingletonLifetime = baseSingletonLifetime;
                _counter = counter;
            }

            public Expression Build(Expression expression, IBuildContext buildContext, object state = default(object))
            {
                // Build expression using base lifetime
                expression = _baseSingletonLifetime.Build(expression, buildContext, state);

                // Define `this` variable
                var thisVar = buildContext.AppendValue(this);

                return Expression.Block(
                    // Adds statement this.IncrementCounter()
                    Expression.Call(thisVar, IncrementCounterMethodInfo),
                    expression);
            }

            public ILifetime Clone() => new MySingletonLifetime(_baseSingletonLifetime.Clone(), _counter);

            // Just counting the number of calls
            internal void IncrementCounter() => _counter.Increment();
        }
        // }
    }
}
#endif