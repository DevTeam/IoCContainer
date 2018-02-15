namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class CustomLifetime
    {
        [Fact]
        // $visible=true
        // $group=08
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
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
        }

        public class MyTransientLifetime : ILifetime
        {
            public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
            {
                return resolver(container, args);
            }

            public ILifetime Clone()
            {
                return new MyTransientLifetime();
            }
        }
        // }
    }
}
