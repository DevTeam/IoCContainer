namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Lifetimes;
    using Shouldly;
    using Xunit;

    public class CustomLifetime
    {
        [Fact]
        // $visible=true
        // $tag=2 Lifetimes
        // $priority=10
        // $description=Custom lifetime
        // $header=Custom lifetimes allow implementing your own logic controlling every aspect of resolved instances.
        // {
        public void Run()
        {
            var serviceLifetime = new MyLifetime();

            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind using a custom lifetime
                .Bind<IService>().Lifetime(serviceLifetime).To<Service>()
                .Container;
            
            // Resolve instances
            var instance1 = container.Resolve<IService>();
            var instance2 = container.Resolve<IService>();

            // Check that instances were registered
            serviceLifetime.ToList().ShouldBe(new object[] {instance1, instance2}, true);
        }

        // Represents a custom lifetime that contains all created instances just for sample
        public class MyLifetime : TrackingLifetime, IEnumerable<object>
        {
            private readonly List<WeakReference> _instances = new List<WeakReference>();

            public MyLifetime() : base(TrackTypes.AfterCreation) { }

            // Creates the similar lifetime to use with generic types
            public override ILifetime CreateLifetime() => new MyLifetime();

            protected override object AfterCreation(object newInstance, IContainer container, object[] args)
            {
                var instance = base.AfterCreation(newInstance, container, args);
                lock (_instances)
                {
                    // Keeps a weak reference for the instance
                    _instances.Add(new WeakReference(instance));
                }
                
                return instance;
            }

            public IEnumerator<object> GetEnumerator()
            {
                IEnumerable<WeakReference> instances;
                lock (_instances)
                {
                    instances = _instances.ToArray();
                }

                return (
                    from weakReference in instances
                    let instance = weakReference.Target
                    where weakReference.IsAlive
                    select instance ).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public override void Dispose()
            {
                lock (_instances)
                {
                    _instances.Clear();
                }
            }
        }
        // }
    }
}
