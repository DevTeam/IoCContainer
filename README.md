# Simple, powerful and fast IoC container

[![NuGet Version and Downloads count](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container) [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

Key features:
  - One of the fastest, almost as fast as operators `new`
  - Produces minimal memory trafic
  - Powerful auto-wiring
    - Checks the auto-wiring configuration at the compile time
    - Allows to not change a code design to use IoC
    - Clear generic types' mapping
    - The simple text metadata is supported
  - Fully extensible and supports custom containers/lifetimes
  - Reconfigurable on-the-fly
  - Supports concurrent and asynchronous resolving

Supported platforms:
  - .NET 4.5+
  - .NET Core 1.0+
  - .NET Standard 1.1+

## [Schrödinger's cat](https://github.com/DevTeam/IoCContainer/tree/master/Samples/ShroedingersCat) shows how it works

### The reality is that

![Cat](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/Docs/Images/cat.jpg)

### Let's create an abstraction

```csharp
interface IBox<out T> { T Content { get; } }

interface ICat { bool IsAlive { get; } }
```

### Here is our implementation

```csharp
class CardboardBox<T> : IBox<T>
{
  public CardboardBox(T content) { Content = content; }

  public T Content { get; }
}

class ShroedingersCat : ICat
{
  public bool IsAlive => new Random().Next(2) == 1;
}
```

_**It is important to note that our abstraction and our implementation do not know anything about IoC containers**_

### Add a [package reference](https://www.nuget.org/packages/IoC.Container)

- Package Manager

  ```
  Install-Package IoC.Container
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.Container
  ```
  
- Packet CLI

  ```
  paket add IoC.Container
  ```

### Let's glue our abstraction and our implementation

```csharp
class Glue : IConfiguration
{
  public IEnumerable<IDisposable> Apply(IContainer container)
  {
    yield return container.Bind(typeof(IBox<>)).To(typeof(CardboardBox<>));
    yield return container.Bind<ICat>().To<ShroedingersCat>();
  }
}
```

### Just configure the container and check it works as expected

```csharp
using (var container = Container.Create().Using<Glue>())
{
  var box1 = container.Get<IBox<ICat>>();
  Console.WriteLine("#1 is alive: " + box1.Content.IsAlive);

  // Func way
  var box2 = container.FuncGet<IBox<ICat>>();
  Console.WriteLine("#2 is alive: " + box2().Content.IsAlive);

  // Async way
  var box3 = await container.AsyncGet<IBox<ICat>>(TaskScheduler.Default);
  Console.WriteLine("#3 is alive: " + box3.Content.IsAlive);
}
```

## Why this one?

The results of the [comparison tests](https://github.com/DevTeam/IoCContainer/blob/master/IoC.Tests/ComparisonTests.cs) for some popular IoC containers like Castle Windsor, Autofac, Unity, Ninject ...

![Cat](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_Build,status:SUCCESS/artifacts/content/REPORT.jpg)

## Usage Scenarios

* [Several contracts](#several-contracts)
* [Asynchronous get](#asynchronous-get)
* [Constant](#constant)
* [Dependency Tag](#dependency-tag)
* [Func](#func)
* [Func get](#func-get)
* [Generic Auto-wiring](#generic-auto-wiring)
* [Generics](#generics)
* [Tags](#tags)
* [Auto-wiring](#auto-wiring)
* [Method Injection](#method-injection)
* [Property Injection](#property-injection)
* [Singletone lifetime](#singletone-lifetime)
* [Constructor Auto-wiring](#constructor-auto-wiring)
* [Flexible Auto-wiring](#flexible-auto-wiring)
* [Resolve all possible items as IEnumerable<>](#resolve-all-possible-items-as-ienumerable<>)
* [Func With Arguments](#func-with-arguments)
* [Resolve Using Arguments](#resolve-using-arguments)
* [Auto dispose singletone during container's dispose](#auto-dispose-singletone-during-containers-dispose)
* [Configuration class](#configuration-class)
* [Configuration text](#configuration-text)
* [Change configuration on-the-fly](#change-configuration-on-the-fly)
* [Custom Child Container](#custom-child-container)
* [Custom Lifetime](#custom-lifetime)
* [Replace Lifetime](#replace-lifetime)
* [Scope lifetime](#scope-lifetime)
* [Wrapper](#wrapper)
* [Generator](#generator)
* [Cyclic Dependence](#cyclic-dependence)
* [Instant Messenger](#instant-messenger)
* [Interfaces and classes for samples](#interfaces-and-classes-for-samples)

### Several contracts

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<Service, IService, IAnotherService>().To<Service>())
{
    // Resolve instances
    var instance1 = container.Get<IService>();
    var instance2 = container.Get<IAnotherService>();

    instance1.ShouldBeOfType<Service>();
    instance2.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SeveralContracts.cs)

### Asynchronous get

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve the instance asynchronously
    var instance = await container.AsyncGet<IService>(TaskScheduler.Default);

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsyncGet.cs)

### Constant

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IService>().To(ctx => new Service(new Dependency())))
{
    // Resolve the instance
    var instance = container.Get<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Constant.cs)

### Dependency Tag

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
// Mark binding by tag "MyDep"
using (container.Bind<IDependency>().Tag("MyDep").To<Dependency>())
// Configure auto-wiring and use dependency with tag "MyDep"
using (container.Bind<IService>().To<Service>(
    ctx => new Service(ctx.Container.Inject<IDependency>("MyDep"))))
{
    // Resolve the instance
    var instance = container.Get<IService>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/DependencyTag.cs)

### Func

``` CSharp
Func<IService> func = () => new Service(new Dependency());
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IService>().To(ctx => func()))
{
    // Resolve the instance
    var instance = container.Get<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Func.cs)

### Func get

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve Func
    var func = container.Get<Func<IService>>();
    // Get the instance
    var instance = func();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/FuncGet.cs)

### Generic Auto-wiring

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
// Configure auto-wiring
using (container.Bind<IService<TT>>().To<Service<TT>>(
    // Configure the constructor to use
    ctx => new Service<TT>(ctx.Container.Inject<IDependency>())))
{
    // Resolve the generic instance
    var instance = container.Get<IService<int>>();

    instance.ShouldBeOfType<Service<int>>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/GenericAutowiring.cs)

### Generics

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind(typeof(IService<>)).To(typeof(Service<>)))
{
    // Resolve the generic instance
    var instance = container.Get<IService<int>>();

    instance.ShouldBeOfType<Service<int>>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Generics.cs)

### Tags

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Tag(10).Tag().Tag("abc").To<Service>())
{
    // Resolve instances using tags
    var instance1 = container.Tag("abc").Get<IService>();
    var instance2 = container.Tag(10).Get<IService>();

    // Resolve the instance using the empty tag
    var instance3 = container.Get<IService>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Tags.cs)

### Auto-wiring

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve the instance
    var instance = container.Get<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Autowiring.cs)

### Method Injection

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To<InitializingNamedService>(
    // Configure the constructor to use
    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
    // Configure the method to initialize
    ctx => ctx.It.Initialize((string)ctx.Args[0], ctx.Container.Inject<IDependency>())))
{
    // Resolve the instance "alpha"
    var instance = container.Get<INamedService>("alpha");

    instance.ShouldBeOfType<InitializingNamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var func = container.Get<Func<string, INamedService>>();
    var otherInstance = func("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/MethodInjection.cs)

### Property Injection

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To<InitializingNamedService>(
    // Configure the constructor to use
    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
    // Configure the property to initialize
    ctx => ctx.Container.Inject(ctx.It.Name, (string)ctx.Args[0])))
{
    // Resolve the instance "alpha"
    var instance = container.Get<INamedService>("alpha");

    instance.ShouldBeOfType<InitializingNamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var func = container.Get<Func<string, INamedService>>();
    var otherInstance = func("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/PropertyInjection.cs)

### Singletone lifetime

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Lifetime(Lifetime.Singletone).To<Service>())
{
    // Resolve the instance twice
    var instance1 = container.Get<IService>();
    var instance2 = container.Get<IService>();

    instance1.ShouldBe(instance2);
}

// Other lifetimes are:
// Transient - A new instance each time
// Container - Singletone per container
// Scope - Singletone per scope
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SingletoneLifetime.cs)

### Constructor Auto-wiring

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
// Configure auto-wiring
using (container.Bind<IService>().To<Service>(
    // Configure the constructor to use
    ctx => new Service(ctx.Container.Inject<IDependency>(), "some state")))
{
    // Resolve the instance
    var instance = container.Get<IService>();

    instance.ShouldBeOfType<Service>();
    instance.State.ShouldBe("some state");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConstructorAutowiring.cs)

### Flexible Auto-wiring

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
// Configure auto-wiring
using (container.Bind<INamedService>().To<InitializingNamedService>(
    // Configure the constructor to use
    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
    // Configure the method to initialize
    ctx => ctx.It.Initialize("some name", ctx.Container.Inject<IDependency>())))
{
    // Resolve the instance
    var instance = container.Get<INamedService>();

    instance.ShouldBeOfType<InitializingNamedService>();
    instance.Name.ShouldBe("some name");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/FlexibleAutowiring.cs)

### Resolve all possible items as IEnumerable<>

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Tag(1).To<Service>())
using (container.Bind<IService>().Tag(2).To<Service>())
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve all possible instances
    var instances = container.Get<IEnumerable<IService>>().ToList();

    instances.Count.ShouldBe(3);
    instances.ForEach(instance => instance.ShouldBeOfType<Service>());
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Enumerables.cs)

### Func With Arguments

``` CSharp
// Create the container
Func<IDependency, string, INamedService> func = 
    (dependency, name) => new NamedService(dependency, name);

using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
// Configure auto-wiring for constructor and use element from index as a second argument
using (container.Bind<INamedService>().To(
    ctx => func(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0])))
{
    // Resolve the instance "alpha"
    var instance = container.Get<INamedService>("alpha");

    instance.ShouldBeOfType<NamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var getterFunc = container.Get<Func<string, INamedService>>();
    var otherInstance = getterFunc("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/FuncWithArguments.cs)

### Resolve Using Arguments

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To<NamedService>(
    // Configure the constructor to use
    ctx => new NamedService(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0])))
{
    // Resolve the instance "alpha"
    var instance = container.Get<INamedService>("alpha");

    instance.ShouldBeOfType<NamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var func = container.Get<Func<string, INamedService>>();
    var otherInstance = func("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveWithArgs.cs)

### Auto dispose singletone during container's dispose

``` CSharp
var disposableService = new Mock<IDisposableService>();

// Create the container
using (var container = Container.Create())
{
    // Configure the container
    container.Bind<IService>().Lifetime(Lifetime.Singletone)
        .To<IDisposableService>(ctx => disposableService.Object).ToSelf();

    // Resolve instances
    var instance1 = container.Get<IService>();
    var instance2 = container.Get<IService>();

    instance1.ShouldBe(instance2);
}

disposableService.Verify(i => i.Dispose(), Times.Once);
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AutoDisposeSingletoneDuringContainersDispose.cs)

### Configuration class

``` CSharp
public void Run()
{
    // Create the container and configure it
    using (var container = Container.Create().Using<Glue>())
    {
        // Resolve the instance
        var instance = container.Get<IService>();

        instance.ShouldBeOfType<Service>();
    }
}

public class Glue : IConfiguration
{
    public IEnumerable<IDisposable> Apply(IContainer container)
    {
        // Use full auto-wiring
        yield return container.Bind<IDependency>().To<Dependency>();
        yield return container.Bind<IService>().To<Service>();
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConfigurationClass.cs)

### Configuration text

``` CSharp
// Create the container and configure from the metadata string
using (var container = Container.Create().Using(
    "ref IoC.Tests;" +
    "using IoC.Tests.UsageScenarios;" +
    "Bind<IDependency>().To<Dependency>();" +
    "Bind<IService>().To<Service>();"))
{
    // Resolve the instance
    var instance = container.Get<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConfigurationText.cs)

### Change configuration on-the-fly

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
{
    // Configure the container using the Transient lifetime
    using (container.Bind<IService>().To<Service>())
    {
        // Resolve instances
        var instance1 = container.Get<IService>();
        var instance2 = container.Get<IService>();

        instance1.ShouldNotBe(instance2);
    }

    // Reconfigure the container using the Singletone lifetime
    using (container.Bind<IService>().Lifetime(Lifetime.Singletone).To<Service>())
    {
        // Resolve the instance twice
        var instance1 = container.Get<IService>();
        var instance2 = container.Get<IService>();

        instance1.ShouldBe(instance2);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ChangeConfigurationOnTheFly.cs)

### Custom Child Container

``` CSharp
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

[SuppressMessage("ReSharper", "PossibleNullReferenceException")]
public class MyContainer: IContainer
{
    public MyContainer(IContainer currentContainer)
    {
        Parent = currentContainer;
    }

    public IContainer Parent { get; }

    public bool TryRegister(IEnumerable<Key> keys, IoC.IDependency dependency, ILifetime lifetime, out IDisposable registrationToken)
    {
        return Parent.TryRegister(keys, dependency, lifetime, out registrationToken);
    }

    public bool TryGetDependency(Key key, out IoC.IDependency dependency, out ILifetime lifetime)
    {
        return Parent.TryGetDependency(key, out dependency, out lifetime);
    }

    public bool TryGetResolver<T>(Key key, out Resolver<T> resolver, IContainer container = null)
    {
        return Parent.TryGetResolver(key, out resolver, container);
    }

    public bool TryGet(Type type, object tag, out object instance, params object[] args)
    {
        return Parent.TryGet(type, tag, out instance, args);
    }

    public bool TryGet<T>(object tag, out T instance, params object[] args)
    {
        return Parent.TryGet(tag, out instance, args);
    }

    public void Dispose() { }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Key> GetEnumerator()
    {
        return Parent.GetEnumerator();
    }

    public IDisposable Subscribe(IObserver<ContainerEvent> observer)
    {
        return Parent.Subscribe(observer);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomChildContainer.cs)

### Custom Lifetime

``` CSharp
public void Run()
{
    // Create the container
    using (var container = Container.Create())
    // Configure the container
    using (container.Bind<IDependency>().To<Dependency>())
    using (container.Bind<IService>().Lifetime(new MyTransientLifetime()).To<Service>())
    {
        // Resolve the instance
        var instance = container.Get<IService>();

        instance.ShouldBeOfType<Service>();
    }
}

public class MyTransientLifetime : ILifetime
{
    public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
    {
        return resolver(container, args);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomLifetime.cs)

### Replace Lifetime

``` CSharp
public void Run()
{
    var counter = new Mock<ICounter>();

    // Create the container
    using (var container = Container.Create())
    using (container.Bind<ICounter>().To(ctx => counter.Object))
    // Replace the Singletone lifetime
    using (container.Bind<ILifetime>().Tag(Lifetime.Singletone).To<MySingletoneLifetime>(
            // Configure the constructor to use
            ctx => new MySingletoneLifetime(
                // Inject the singletone lifetime from the parent container to use a base logic
                ctx.Container.Parent.Inject<ILifetime>(Lifetime.Singletone),
                // Inject a counter
                ctx.Container.Inject<ICounter>())))
    // Configure the container
    using (container.Bind<IDependency>().To<Dependency>())
    // Custom Singletone lifetime is using
    using (container.Bind<IService>().Lifetime(Lifetime.Singletone).To<Service>())
    {
        // Resolve the instance twice using the wrapped Singletine lifetime
        var instance1 = container.Get<IService>();
        var instance2 = container.Get<IService>();

        instance1.ShouldBe(instance2);
    }

    counter.Verify(i => i.Increment(), Times.Exactly(2));
}

public interface ICounter
{
    void Increment();
}

public class MySingletoneLifetime : ILifetime
{
    private readonly ILifetime _baseSingletoneLifetime;
    private readonly ICounter _counter;

    public MySingletoneLifetime(ILifetime baseSingletoneLifetime, ICounter counter)
    {
        _baseSingletoneLifetime = baseSingletoneLifetime;
        _counter = counter;
    }

    public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
    {
        // Just counting the number of calls
        _counter.Increment();
        return _baseSingletoneLifetime.GetOrCreate(container, args, resolver);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ReplaceLifetime.cs)

### Scope lifetime

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().Lifetime(Lifetime.Scope).To<Dependency>())
{
    using (container.Bind<IService>().Lifetime(Lifetime.Scope).To<Service>())
    {
        // Default resolving scope
        var instance1 = container.Get<IService>();
        var instance2 = container.Get<IService>();
        instance1.ShouldBe(instance2);

        // Resolving in scope "1"
        using (new ResolvingScope("1"))
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
    using (container.Bind<IService>().Lifetime(Lifetime.Transient).To<Service>())
    {
        // Default resolving scope
        var instance1 = container.Get<IService>();
        var instance2 = container.Get<IService>();
        instance1.Dependency.ShouldBe(instance2.Dependency);

        // Resolving in scope "1"
        using (new ResolvingScope("1"))
        {
            var instance3 = container.Get<IService>();
            instance3.Dependency.ShouldNotBe(instance1.Dependency);
        }
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ScopeLifetime.cs)

### Wrapper

``` CSharp
public void Run()
{
    var console = new Mock<IConsole>();

    // Create the base container
    using (var baseContainer = Container.Create("base"))
    // Configure the base container for base logger
    using (baseContainer.Bind<IConsole>().To(ctx => console.Object))
    using (baseContainer.Bind<ILogger>().To<Logger>())
    {
        // Configure some new container
        using (var childContainer = baseContainer.CreateChild("child"))
        // And add some console
        using (childContainer.Bind<IConsole>().To(ctx => console.Object))
        using (childContainer.Bind<ILogger>().To<TimeLogger>(
            // Inject the logger from the parent container to wrapp
            ctx => new TimeLogger(ctx.Container.Parent.Inject<ILogger>())))
        {
            var logger = childContainer.Get<ILogger>();

            // Log message
            logger.Log("Hello");
        }
    }

    // Check the console output
    console.Verify(i => i.WriteLine(It.IsRegex(".+: Hello")));
}

public interface IConsole
{
    void WriteLine(string test);
}

public interface ILogger
{
    void Log(string message);
}

public class Logger : ILogger
{
    private readonly IConsole _console;

    public Logger(IConsole console)
    {
        _console = console;
    }

    public void Log(string message)
    {
        _console.WriteLine(message);
    }
}

public class TimeLogger: ILogger
{
    private readonly ILogger _baseLogger;

    public TimeLogger(ILogger baseLogger)
    {
        _baseLogger = baseLogger;
    }

    public void Log(string message)
    {
        _baseLogger.Log(DateTimeOffset.Now + ": " + message);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Wrapper.cs)

### Generator

``` CSharp
public void Run()
{
    Func<int, int, (int, int)> valueGetter = (sequential, random) => (sequential, random);

    // Create the container and configure it using configuration class
    using (var container = Container.Create().Using<Generators>())
    // Configure binding to get a set of numbers as (int, int).
    // Inject dependency of sequential number to the first item
    // Inject dependency of random number to the second item
    using (container.Bind<(int, int)>().To(
        ctx => valueGetter(
            ctx.Container.Inject<int>(GeneratorType.Sequential),
            ctx.Container.Inject<int>(GeneratorType.Random))))
    {
        // Generate sequential numbers
        var sequential1 = container.Tag(GeneratorType.Sequential).Get<int>();
        var sequential2 = container.Tag(GeneratorType.Sequential).Get<int>();

        sequential2.ShouldBe(sequential1 + 1);

        // Generate a random number
        var random = container.Tag(GeneratorType.Random).Get<int>();

        // Generate a set of numbers
        var setOfValues = container.Get<(int, int)>();

        setOfValues.Item1.ShouldBe(sequential2 + 1);
    }
}

public enum GeneratorType
{
    Sequential, Random
}

public class Generators: IConfiguration
{
    public IEnumerable<IDisposable> Apply(IContainer container)
    {
        var value = 0;
        // Define function to get sequential integer value
        Func<int> generator = () => Interlocked.Increment(ref value);
        yield return container.Bind<int>().Tag(GeneratorType.Sequential).To(ctx => generator());

        var random = new Random();
        // Define function to get random integer value
        Func<int> randomizer = () => random.Next();
        yield return container.Bind<int>().Tag(GeneratorType.Random).To(ctx => randomizer());
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Generator.cs)

### Cyclic Dependence

``` CSharp
public void Run()
{
    var expectedException = new InvalidOperationException("error");
    var issueResolver = new Mock<IIssueResolver>();
    issueResolver.Setup(i => i.CyclicDependenceDetected(It.IsAny<Key>(), 128)).Throws(expectedException);

    // Create the container
    using (var container = Container.Create())
    // Configure the container. 1,2,3 are tags of binding
    using (container.Bind<IIssueResolver>().To(ctx => issueResolver.Object))
    using (container.Bind<ILink>().To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1))))
    using (container.Bind<ILink>().Tag(1).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(2))))
    using (container.Bind<ILink>().Tag(2).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(3))))
    using (container.Bind<ILink>().Tag(3).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1))))
    {
        try
        {
            // Resolve the first link
            container.Get<ILink>();
        }
        catch (InvalidOperationException actualException)
        {
            actualException.ShouldBe(expectedException);
        }
    }

    issueResolver.Verify(i => i.CyclicDependenceDetected(It.IsAny<Key>(), 128));
}

public interface ILink
{
}

public class Link : ILink
{
    // ReSharper disable once UnusedParameter.Local
    public Link(ILink link)
    {
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CyclicDependence.cs)

### Instant Messenger

``` CSharp
public void Run()
{
    var observer = new Mock<IObserver<IMessage>>();

    // Initial message id
    var id = 33;
    Func<int> generator = () => id++;

    // Create the container
    using (var container = Container.Create())
    // Configure the container
    using (container.Bind<int>().Tag("IdGenerator").To(ctx => generator()))
    using (container.Bind(typeof(IInstantMessenger<>)).To(typeof(InstantMessenger<>)))
    using (container.Bind<IMessage>().To<Message>(ctx => new Message(ctx.Container.Inject<int>("IdGenerator"), (string)ctx.Args[0], (string)ctx.Args[1])))
    {
        var instantMessenger = container.Get<IInstantMessenger<IMessage>>();
        using (instantMessenger.Subscribe(observer.Object))
        {
            for (var i = 0; i < 10; i++)
            {
                instantMessenger.SendMessage("John", "Hello");
            }
        }
    }

    observer.Verify(i => i.OnNext(It.Is<IMessage>(message => message.Id >= 33 && message.Address == "John" && message.Text == "Hello")), Times.Exactly(10));
}

public interface IInstantMessenger<out T>: IObservable<T>
{
    void SendMessage(string address, string text);
}

public interface IMessage
{
    int Id { get; }

    string Address { get; }

    string Text { get; }
}

public class Message: IMessage
{
    public Message(int id, [NotNull] string address, [NotNull] string text)
    {
        Id = id;
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }

    public int Id { get; }

    public string Address { get; }

    public string Text { get; }
}

public class InstantMessenger<T> : IInstantMessenger<T>
{
    private readonly Func<string, string, T> _createMessage;
    private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

    public InstantMessenger(Func<string, string, T> createMessage)
    {
        _createMessage = createMessage;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        _observers.Add(observer);
        return Disposable.Create(() => _observers.Remove(observer));
    }

    public void SendMessage(string address, string text)
    {
        _observers.ForEach(observer => observer.OnNext(_createMessage(address, text)));
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SimpleInstantMessenger.cs)

### Interfaces and classes for samples

``` CSharp
public interface IDependency { }

public class Dependency : IDependency { }

public interface IService
{
    IDependency Dependency { get; }

    string State { get; }
}

public interface IAnotherService { }

public interface IDisposableService : IService, IDisposable
{
}

public class Service : IService, IAnotherService
{
    public Service(IDependency dependency)
    {
        Dependency = dependency;
    }

    public Service(IDependency dependency, string state)
    {
        Dependency = dependency;
        State = state;
    }

    public IDependency Dependency { get; }

    public string State { get; }
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
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Models.cs)

## The build state

[<img src="http://tcavs2015.cloudapp.net/app/rest/builds/buildType:(id:DevTeam_IoCContainer_Build)/statusIcon"/>](http://tcavs2015.cloudapp.net/viewType.html?buildTypeId=DevTeam_IoCContainer_Build&guest=1)
