namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;
    using static Lifetime;

    public class ScopeSingletonLifetime
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=03
            // $description=Scope Singleton lifetime
            // {
            // Create and configure the container
            using (var container = Container.Create())
            // Use the Scope Singleton lifetime for dependency
            using (container.Bind<IDependency>().As(ScopeSingleton).To<Dependency>())
            {
                // Use the Scope Singleton lifetime for instance
                using (container.Bind<IService>().As(ScopeSingleton).To<Service>())
                {
                    // Resolve the default scope singleton twice
                    var defaultScopeInstance1 = container.Resolve<IService>();
                    var defaultScopeInstance2 = container.Resolve<IService>();

                    // Check that instances from the default scope are equal
                    defaultScopeInstance1.ShouldBe(defaultScopeInstance2);

                    // Using the scope "a"
                    using (new Scope("a").Begin())
                    {
                        var scopeInstance1 = container.Resolve<IService>();
                        var scopeInstance2 = container.Resolve<IService>();

                        // Check that instances from the scope "a" are equal
                        scopeInstance1.ShouldBe(scopeInstance2);

                        // Check that instances from different scopes are not equal
                        scopeInstance1.ShouldNotBe(defaultScopeInstance1);
                    }

                    // Default scope again
                    var defaultScopeInstance3 = container.Resolve<IService>();

                    // Check that instances from the default scope are equal
                    defaultScopeInstance3.ShouldBe(defaultScopeInstance1);
                }

                // Reconfigure the container to check dependencies only
                using (container.Bind<IService>().As(Transient).To<Service>())
                {
                    // Resolve transient instances
                    var transientInstance1 = container.Resolve<IService>();
                    var transientInstance2 = container.Resolve<IService>();

                    // Check that transient instances are not equal
                    transientInstance1.ShouldNotBe(transientInstance2);

                    // Check that dependencies from the default scope are equal
                    transientInstance1.Dependency.ShouldBe(transientInstance2.Dependency);

                    // Using the scope "a"
                    using (new Scope("a").Begin())
                    {
                        // Resolve a transient instance in scope "a"
                        var transientInstance3 = container.Resolve<IService>();

                        // Check that dependencies from different scopes are not equal
                        transientInstance3.Dependency.ShouldNotBe(transientInstance1.Dependency);
                    }
                }
            }
            // }
        }
    }
}
