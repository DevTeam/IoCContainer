namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ChangeConfigurationOnTheFly
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=01
            // $description=Change configuration on-the-fly
            // {
            using var container = Container.Create();
            var dependencyBindingToken = container.Bind<IDependency>().To<Dependency>();

            // Configure `IService` as Transient
            using (container.Bind<IService>().To<Service>())
            {
                // Check binding state for IService
                container.IsBound<IService>().ShouldBeTrue();
                container.CanResolve<IService>().ShouldBeTrue();

                // Resolve instances
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                // Check that instances are not equal
                instance1.ShouldNotBe(instance2);
            }

            // Check binding state for IService
            container.IsBound<IService>().ShouldBeFalse();
            container.CanResolve<IService>().ShouldBeFalse();

            // Reconfigure `IService` as Singleton
            using (container.Bind<IService>().As(Lifetime.Singleton).To<Service>())
            {
                // Resolve the singleton twice
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                // Check that instances are equal
                instance1.ShouldBe(instance2);

                // Unbind IDependency
                dependencyBindingToken.Dispose();

                // Check binding state for IService
                container.IsBound<IService>().ShouldBeTrue();
                container.CanResolve<IService>().ShouldBeFalse();
            }

            container.IsBound<IService>().ShouldBeFalse();
            container.CanResolve<IService>().ShouldBeFalse();
            // }
        }
    }
}
