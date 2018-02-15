namespace IoC.Tests.UsageScenarios
{
    using Moq;
    using Shouldly;
    using Xunit;

    public class ReplaceLifetime
    {
        [Fact]
        // $visible=true
        // $group=08
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
                var instance1 = container.Get<IService>();
                var instance2 = container.Get<IService>();

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
            private readonly ILifetime _baseSingletonLifetime;
            private readonly ICounter _counter;

            public MySingletonLifetime(ILifetime baseSingletonLifetime, ICounter counter)
            {
                _baseSingletonLifetime = baseSingletonLifetime;
                _counter = counter;
            }

            public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
            {
                // Just counting the number of calls
                _counter.Increment();
                return _baseSingletonLifetime.GetOrCreate(container, args, resolver);
            }

            public ILifetime Clone()
            {
                return new MySingletonLifetime(_baseSingletonLifetime.Clone(), _counter);
            }
        }
        // }
    }
}
