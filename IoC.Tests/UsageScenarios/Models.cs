// ReSharper disable All
namespace IoC.Tests.UsageScenarios
{
    using System;

    public interface IDependency { }

    public class Dependency : IDependency { }

    public interface IService
    {
        IDependency Dependency { get; }

        string State { get; }
    }

    public interface IAnotherService { }

    public interface IDisposableService : IService, IDisposable
#if NETCOREAPP3_1
    , IAsyncDisposable
#endif
    {
    }

    public class Service : IService, IAnotherService
    {
        public Service(IDependency dependency) => Dependency = dependency;

        public Service(IDependency dependency, string state)
        {
            Dependency = dependency;
            State = state;
        }

        public IDependency Dependency { get; }

        public string State { get; }
    }

    // Generic
    public interface IService<T>: IService { }

    public class Service<T> : IService<T>
    {
        public Service(IDependency dependency) { }

        public IDependency Dependency { get; }

        public string State { get; }
    }

    // Named
    public interface INamedService
    {
        string Name { get; }
    }

    public class NamedService : INamedService
    {
        public NamedService(IDependency dependency, string name) => Name = name;

        public string Name { get; }
    }

    // Property and Method injection
    public class InitializingNamedService : INamedService
    {
        public InitializingNamedService(IDependency dependency)
        {
        }

        public string Name { get; set; }

        public void Initialize(string name, IDependency otherDependency) => Name = name;
    }
}
