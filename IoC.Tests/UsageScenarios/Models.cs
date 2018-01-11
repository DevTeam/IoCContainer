﻿// ReSharper disable All
namespace IoC.Tests.UsageScenarios
{
    using System;

    // $visible=true
    // $group=99
    // $priority=00
    // $description=Interfaces and classes for samples
    // {
    public interface IDependency { }

    public class Dependency : IDependency { }

    public interface IService { }

    public interface IAnotherService { }

    public interface IDisposableService : IService, IDisposable { }

    public class Service : IService, IAnotherService
    {
        public Service(IDependency dependency) { }
    }

    // Generic
    public interface IService<T> { }

    public class Service<T> : IService<T>
    {
        public Service(IDependency dependency) { }
    }

    // Named
    public interface INamedService
    {
        string Name { get; }
    }

    public class NamedService : INamedService
    {
        public NamedService(IDependency dependency, string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    // Property and Method injection
    public class InitializingNamedService : INamedService
    {
        public InitializingNamedService(IDependency dependency)
        {
        }

        public string Name { get; set; }

        public void Initialize(string name, IDependency otherDependency)
        {
            Name = name;
        }
    }
    // }
}
