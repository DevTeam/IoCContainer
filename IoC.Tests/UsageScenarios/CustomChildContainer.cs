namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class CustomChildContainer
    {
        [Fact]
        // $visible=true
        // $group=08
        // $priority=00
        // $description=Custom Child Container
        // {
        public void Run()
        {
            // Create the container
            using (var container = Container.Create())
            // Configure current container to use a custom container's class to create a child container
            using (container.Bind<IContainer>().Tag(Scope.Child).To<MyContainer>())
            // Create our child container
            using (var childContainer = container.CreateChild("abc"))
            // Configure the child container
            using (childContainer.Bind<IDependency>().To<Dependency>())
            using (childContainer.Bind<IService>().To<Service>())
            {
                // Resolve the instance
                var instance = childContainer.Get<IService>();

                childContainer.ShouldBeOfType<MyContainer>();
                instance.ShouldBeOfType<Service>();
            }
        }

        public class MyContainer: IContainer
        {
            public MyContainer(IContainer currentContainer)
            {
                Parent = currentContainer;
            }

            public IContainer Parent { get; }

            public bool TryRegister(IEnumerable<Key> keys, IFactory factory, ILifetime lifetime, out IDisposable registrationToken)
            {
                // Add your logic here or just ...
                return Parent.TryRegister(keys, factory, lifetime, out registrationToken);
            }

            public bool TryGetResolver(Key key, out IResolver resolver)
            {
                // Add your logic here or just ...
                return Parent.TryGetResolver(key, out resolver);
            }

            public void Dispose() { }
        }
        // }
    }
}
