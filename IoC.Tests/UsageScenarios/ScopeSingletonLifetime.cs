namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ScopeSingletonLifetime
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=08
            // $priority=00
            // $description=Scope Singleton lifetime
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().As(Lifetime.ScopeSingleton).To<Dependency>())
            {
                using (container.Bind<IService>().As(Lifetime.ScopeSingleton).To<Service>())
                {
                    // Default resolving scope
                    var instance1 = container.Get<IService>();
                    var instance2 = container.Get<IService>();
                    instance1.ShouldBe(instance2);

                    // Resolving in scope "1"
                    using (new Scope("1"))
                    {
                        var instance3 = container.Get<IService>();
                        var instance4 = container.Get<IService>();

                        instance3.ShouldBe(instance4);
                        instance3.ShouldNotBe(instance1);
                    }

                    // Default resolving scope again
                    var instance5 = container.Get<IService>();
                    instance5.ShouldBe(instance1);
                }

                // Reconfigure to check dependencies only
                using (container.Bind<IService>().As(Lifetime.Transient).To<Service>())
                {
                    // Default resolving scope
                    var instance1 = container.Get<IService>();
                    var instance2 = container.Get<IService>();
                    instance1.Dependency.ShouldBe(instance2.Dependency);

                    // Resolving in scope "1"
                    using (new Scope("1"))
                    {
                        var instance3 = container.Get<IService>();
                        instance3.Dependency.ShouldNotBe(instance1.Dependency);
                    }
                }
            }
            // }
        }
    }
}
