﻿namespace IoC.Tests.UsageScenarios
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
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().Lifetime(new MyTransientLifetime()).To<Service>())
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
        }

        public class MyTransientLifetime : ILifetime
        {
            public object GetOrCreate(ResolvingContext context, IFactory factory)
            {
                return factory.Create(context);
            }
        }
        // }
    }
}
