﻿// ReSharper disable All
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;

    public interface IDependency
    {
        int Index { get; set; }
    }

    public class Dependency : IDependency
    {
        public int Index { get; set; }
    }

    public interface IService
    {
        IDependency Dependency { get; }

        string State { get; }
    }

    public interface IAnotherService { }

    public interface IDisposableService : IService, IDisposable
#if NET5_0_OR_GREATER || NETCOREAPP3_1
    , IAsyncDisposable
#endif
    {
    }

    public class Service : IService, IAnotherService, IDisposable
    {
        public int DisposeCount;

        public Service(IDependency dependency) => Dependency = dependency;

        public Service(IDependency dependency, string state)
        {
            Dependency = dependency;
            State = state;
        }

        public IDependency Dependency { get; }

        public string State { get; }

        public void Dispose() => DisposeCount++;
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

    public interface IListService<T>
        where T: IList<int>
    {
    }

    public class ListService<T> : IListService<T>
        where T : IList<int>
    {
    }
}
