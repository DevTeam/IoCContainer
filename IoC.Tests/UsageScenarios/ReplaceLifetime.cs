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

            // Create the container
            using (var container = Container.Create())
            using (container.Bind<ICounter>().To(ctx => counter.Object))
            // Replace the Singletone lifetime
            using (container.Bind<ILifetime>().Tag(Lifetime.Singletone).To<MySingletoneLifetime>(
                    // Configure the constructor to use
                    ctx => new MySingletoneLifetime(
                        // Inject the singletone lifetime from the parent container to use a base logic
                        ctx.Container.Parent.Inject<ILifetime>(Lifetime.Singletone),
                        // Inject a counter
                        ctx.Container.Inject<ICounter>())))
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            // Custom Singletone lifetime is using
            using (container.Bind<IService>().Lifetime(Lifetime.Singletone).To<Service>())
            {
                // Resolve the instance twice using the wrapped Singletine lifetime
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

        public class MySingletoneLifetime : ILifetime
        {
            private readonly ILifetime _baseSingletoneLifetime;
            private readonly ICounter _counter;

            public MySingletoneLifetime(ILifetime baseSingletoneLifetime, ICounter counter)
            {
                _baseSingletoneLifetime = baseSingletoneLifetime;
                _counter = counter;
            }

            public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
            {
                // Just counting the number of calls
                _counter.Increment();
                return _baseSingletoneLifetime.GetOrCreate(container, args, resolver);
            }

            public ILifetime Clone()
            {
                return new MySingletoneLifetime(_baseSingletoneLifetime.Clone(), _counter);
            }
        }
        // }
    }
}
