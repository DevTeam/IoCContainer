﻿// ReSharper disable PossibleNullReferenceException
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
        // $tag=5 Advanced
        // $priority=10
        // $description=Custom child container
        // $header=You may replace the default implementation of the container with your own. I can't imagine why it should be done, but it’s possible!
        // {
        public void Run()
        {
            // Create and configure the root container
            using var container = Container
                .Create()
                .Bind<IService>().To<Service>()
                // Configure the root container to use a custom container as a child container
                .Bind<IMutableContainer>().To<MyContainer>()
                .Container;

            // Create and configure the custom child container
            using var childContainer = container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Container;

            // Resolve an instance
            var instance = childContainer.Resolve<IService>();

            // Check the child container type
            childContainer.ShouldBeOfType<MyContainer>();
            // }
            // Check the instance
            instance.ShouldBeOfType<Service>();
        // {
        }

        // Sample of transparent container implementation
        public class MyContainer: IMutableContainer
        {
            // Some implementation here
            // }
            // Stores the parent container to delegate all logic
            public MyContainer(IContainer current) => Parent = current;

            public IContainer Parent { get; }

            // Registers dependencies
            public bool TryRegisterDependency(IEnumerable<Key> keys, IoC.IDependency dependency, ILifetime lifetime, out IToken dependencyToken)
            {
                if(Parent is IMutableContainer mutableContainer)
                {
                    return mutableContainer.TryRegisterDependency(keys, dependency, lifetime, out dependencyToken);
                }

                dependencyToken = default;
                return false;
            }

            // Gets registered dependencies and lifetimes
            public bool TryGetDependency(Key key, out IoC.IDependency dependency, out ILifetime lifetime)
                => Parent.TryGetDependency(key, out dependency, out lifetime);

            // Tries getting a resolver
            public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, out Exception error, IContainer resolvingContainer = null)
                => Parent.TryGetResolver(type, tag, out resolver, out error, resolvingContainer);

            // Stores a token
            public void RegisterResource(IDisposable resource) => Parent.RegisterResource(resource);

            // Releases a token
            public bool UnregisterResource(IDisposable resource) => Parent.UnregisterResource(resource);

            public void Dispose() => (Parent as IMutableContainer)?.Dispose();

            // Creates a registered keys' enumerator
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            // Creates a registered keys' strong-typed enumerator
            public IEnumerator<IEnumerable<Key>> GetEnumerator() => Parent.GetEnumerator();

            // Subscribes an observer to receive container events
            public IDisposable Subscribe(IObserver<ContainerEvent> observer) => Parent.Subscribe(observer);
        // {
        }
        // }
    }
}
