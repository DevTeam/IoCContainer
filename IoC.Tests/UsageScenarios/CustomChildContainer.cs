// ReSharper disable PossibleNullReferenceException
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class CustomChildContainer
    {
        [Fact]
        // $visible=true
        // $tag=customization
        // $priority=00
        // $description=Custom Child Container
        // {
        public void Run()
        {
            // Create and configure the root container
            using (var container = Container.Create())
            using (container.Bind<IService>().To<Service>())
            // Configure the root container to use a custom container during creating a child container
            using (container.Bind<IContainer>().Tag(WellknownContainers.NewChild).To<MyContainer>())
            // Create and configure the custom child container
            using (var childContainer = container.CreateChild())
            using (childContainer.Bind<IDependency>().To<Dependency>())
            {
                // Resolve an instance
                var instance = childContainer.Resolve<IService>();

                // Check the child container's type
                childContainer.ShouldBeOfType<MyContainer>();

                // Check the instance's type
                instance.ShouldBeOfType<Service>();
            }
        }

        // Sample of transparent container implementation
        public class MyContainer: IContainer
        {
            // Stores the parent container to delegate all logic
            public MyContainer(IContainer currentContainer) => Parent = currentContainer;

            public IContainer Parent { get; }

            // Registers dependencies
            public bool TryRegisterDependency(IEnumerable<Key> keys, IoC.IDependency dependency, ILifetime lifetime, out IDisposable dependencyToken) 
                => Parent.TryRegisterDependency(keys, dependency, lifetime, out dependencyToken);

            // Gets registered dependencies and lifetimes
            public bool TryGetDependency(Key key, out IoC.IDependency dependency, out ILifetime lifetime)
                => Parent.TryGetDependency(key, out dependency, out lifetime);

            // Tries to get a resolver
            public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, out Exception error, IContainer resolvingContainer = null)
                => Parent.TryGetResolver(type, tag, out resolver, out error, resolvingContainer);

            // Stores a token
            public void RegisterResource(IDisposable resource) { }

            // Releases a token
            public void UnregisterResource(IDisposable resource) { }

            public void Dispose() { }

            // Creates the registered keys' enumerator
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            // Creates the registered keys' strong-typed enumerator
            public IEnumerator<IEnumerable<Key>> GetEnumerator() => Parent.GetEnumerator();

            // Subscribes an observer to receive container events
            public IDisposable Subscribe(IObserver<ContainerEvent> observer) => Parent.Subscribe(observer);
        }
        // }
    }
}
