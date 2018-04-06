# Simple, powerful and fast IoC container

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

Package| Linku
---|:---:
IoC.Container|[![NuGet Version and Downloads count](https://buildstats.info/nuget/IoC.Container?includePreReleases=true)](https://www.nuget.org/packages/IoC.Container)
IoC.AspNetCore|[![NuGet Version and Downloads count](https://buildstats.info/nuget/IoC.AspNetCore?includePreReleases=true)](https://www.nuget.org/packages/IoC.AspNetCore)

IoC.Container provides the following benefits:
  - One of the [fastest](#why-this-one), almost as fast as operators `new`
  - Produces minimal memory trafic
  - Powerful auto-wiring
    - Checks the auto-wiring configuration at the compile time
    - Allows to not change a code design to use IoC
    - [Clear generic types' mapping](#generic-auto-wiring)
    - [The simple text metadata is supported](#configuration-via-a-text-metadata)
    - Flexible autowiring configuration (as example a [custom Aspect Oriented Autowiring](#aspect-oriented-autowiring))
  - Fully extensible and supports [custom containers](#custom-child-container)/[lifetimes](#custom-lifetime)
  - [Reconfigurable on-the-fly](#change-configuration-on-the-fly)
  - Supports [concurrent and asynchronous resolving](#asynchronous-resolve)
  - Does not need additional dependencies
  - Supports [ASP.NET Core](#aspnet-core)
  - Supports [Windows Presentation Foundation](Samples/WpfApp/README.md)
  - Supports [Universal Windows Platform](Samples/UwpApp/README.md)
  - Supports [Windows Communication Foundation](Samples/WcfServiceLibrary/README.md)

Supported platforms:
  - .NET 4.0+
  - .NET Core 1.0+
  - .NET Standard 1.0+
  - [UWP 10+](https://docs.microsoft.com/en-us/windows/uwp/index)

## [Schrödinger's cat](Samples/ShroedingersCat) shows how it works

### The reality is that

![Cat](Docs/Images/cat.jpg)

### Let's create an abstraction

```csharp
interface IBox<out T> { T Content { get; } }

interface ICat { bool IsAlive { get; } }
```

### Here is our implementation

```csharp
class CardboardBox<T> : IBox<T>
{
    public CardboardBox(T content) => Content = content;

    public T Content { get; }

    public override string ToString() => Content.ToString();
}

class ShroedingersCat : ICat
{
    public bool IsAlive => new Random().Next(2) == 1;

    public override string ToString() => $"Is alive: {IsAlive}";
}
```

_**It is important to note that our abstraction and our implementation do not know anything about IoC containers**_

### Add the [package reference](https://www.nuget.org/packages/IoC.Container)

IoC.Container ships entirely as NuGet packages. Using NuGet packages allows you to optimize your app to include only the necessary dependencies.

- Package Manager

  ```
  Install-Package IoC.Container
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.Container
  ```

### Let's glue all together

```csharp
class Glue : IConfiguration
{
  public IEnumerable<IDisposable> Apply(IContainer container)
  {
    yield return container.Bind<IBox<TT>>().To<CardboardBox<TT>>();
    yield return container.Bind<ICat>().To<ShroedingersCat>();
  }
}
```

### Just configure the container and check it works as expected

```csharp
using (var container = Container.Create().Using<Glue>())
{
    var box = container.Resolve<IBox<ICat>>();
    Console.WriteLine(box);

    // Func
    var func = container.Resolve<Func<IBox<ICat>>>();
    Console.WriteLine(func());

    // Async
    box = await container.Resolve<Task<IBox<ICat>>>();
    Console.WriteLine(box);

    // Async value
    box = await container.Resolve<ValueTask<IBox<ICat>>>();
    Console.WriteLine(box);

    // Tuple<,>
    var tuple = container.Resolve<Tuple<IBox<ICat>, ICat>>();
    Console.WriteLine(tuple.Item1 + ", " + tuple.Item2);

    // ValueTuple(,,)
    var valueTuple = container.Resolve<(IBox<ICat> box, ICat cat, IBox<ICat> anotherBox)>();
    Console.WriteLine(valueTuple.box + ", " + valueTuple.cat + ", " + valueTuple.anotherBox);

    // Lazy
    var lazy = container.Resolve<Lazy<IBox<ICat>>>();
    Console.WriteLine(lazy.Value);

    // Enumerable
    var enumerable = container.Resolve<IEnumerable<IBox<ICat>>>();
    Console.WriteLine(enumerable.Single());

    // List
    var list = container.Resolve<IList<IBox<ICat>>>();
    Console.WriteLine(list[0]);
}
```

### Under the hood

Actually these resolvers are represented just as set of operators `new` that allow to create (or get) required instances.

```csharp
var box = new CardboardBox<ShroedingersCat>(new ShroedingersCat());
```

There is only one difference - these resolvers are wrapped to compiled lambda functions and the each call of these lambdas spends some minimal time in the operator `call`, but in actual scenarios it is not required to make these lambdas each time to create an instance.
When some dependencies are injected to an instance they are injected without any lambdas at all but just as a minimal set of instruction to create these dependencies:

```csharp
new ShroedingersCat()
```

Thus this IoC container makes the minimal impact in terms of perfomrance and of memory trafic on a creation of instances of classes and might be used everywhere and everytime in accordance with the [SOLID principles](https://en.wikipedia.org/wiki/SOLID_\(object-oriented_design\)).

## [ASP.NET Core](https://github.com/aspnet/Home)

### Add the [package reference](IoC.AspNetCore)

- Package Manager

  ```
  Install-Package IoC.AspNetCore
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.AspNetCore
  ```

### Change IoC container and configure it at [Startup](Samples/AspNetCore/WebApplication/Startup.cs)

```csharp
public IServiceProvider ConfigureServices(IServiceCollection services)
{
  services.AddMvc().AddControllersAsServices();

  // Create container
  var container = Container.Create().Using(new AspNetCoreFeature(services));

  // Configure container
  container.Using<Glue>();

  // Resolve IServiceProvider
  return container.Resolve<IServiceProvider>();
}
```

For more information see [this sample](Samples/AspNetCore).

## Class References

- [.NET 4.0](Docs/IoC_net40.md)
- [.NET 4.5](Docs/IoC_net45.md)
- [.NET Standard 1.0](Docs/IoC_netstandard1.0.md)
- [.NET Core 2.0](Docs/IoC_netcoreapp2.0.md)
- [UWP 10.0](Docs/IoC_uap10.0.md)

## Why this one?

The results of the [comparison tests](IoC.Comparison/ComparisonTests.cs) for some popular IoC containers like Castle Windsor, Autofac, Unity, Ninject ...

![Cat](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,status:SUCCESS/artifacts/content/REPORT.jpg)

## Usage Scenarios

* [Several Contracts](#several-contracts)
* [Asynchronous resolve](#asynchronous-resolve)
* [Asynchronous lightweight resolve](#asynchronous-lightweight-resolve)
* [Constant](#constant)
* [Dependency Tag](#dependency-tag)
* [Func](#func)
* [Generic Auto-wiring](#generic-auto-wiring)
* [Generics](#generics)
* [Resolve Func](#resolve-func)
* [Resolve Lazy](#resolve-lazy)
* [Resolve Tuple](#resolve-tuple)
* [Resolve ValueTuple](#resolve-valuetuple)
* [Tags](#tags)
* [Auto-wiring](#auto-wiring)
* [Child Container](#child-container)
* [Method Injection](#method-injection)
* [Property Injection](#property-injection)
* [Singleton lifetime](#singleton-lifetime)
* [Constructor Auto-wiring](#constructor-auto-wiring)
* [Manual Auto-wiring](#manual-auto-wiring)
* [Resolve all appropriate instances as ICollection](#resolve-all-appropriate-instances-as-icollection)
* [Resolve all appropriate instances as IEnumerable](#resolve-all-appropriate-instances-as-ienumerable)
* [Func With Arguments](#func-with-arguments)
* [Resolve all appropriate instances as IObservable source](#resolve-all-appropriate-instances-as-iobservable-source)
* [Resolve Using Arguments](#resolve-using-arguments)
* [Resolve all appropriate instances as ISet](#resolve-all-appropriate-instances-as-iset)
* [Auto dispose singleton during container's dispose](#auto-dispose-singleton-during-containers-dispose)
* [Configuration class](#configuration-class)
* [Configuration via a text metadata](#configuration-via-a-text-metadata)
* [Change configuration on-the-fly](#change-configuration-on-the-fly)
* [Aspect Oriented Autowiring](#aspect-oriented-autowiring)
* [Custom Child Container](#custom-child-container)
* [Custom Lifetime](#custom-lifetime)
* [Replace Lifetime](#replace-lifetime)
* [Scope Singleton lifetime](#scope-singleton-lifetime)
* [Wrapper](#wrapper)
* [Generator](#generator)
* [Cyclic Dependence](#cyclic-dependence)
* [Instant Messenger](#instant-messenger)
* [Samples Model](#samples-model)

### Several Contracts

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container, using full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<Service, IService, IAnotherService>().To<Service>())
{
    // Resolve instances
    var instance1 = container.Resolve<IService>();
    var instance2 = container.Resolve<IAnotherService>();

    instance1.ShouldBeOfType<Service>();
    instance2.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SeveralContracts.cs)

### Asynchronous resolve

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve an instance asynchronously
    var instance = await container.Resolve<Task<IService>>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousResolve.cs)

### Asynchronous lightweight resolve

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve an instance asynchronously via ValueTask
    var instance = await container.Resolve<ValueTask<IService>>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousValueResolve.cs)

### Constant

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IService>().To(ctx => new Service(new Dependency())))
{
    // Resolve an instance
    var instance = container.Resolve<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Constant.cs)

### Dependency Tag

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
// Mark binding by tag "MyDep"
using (container.Bind<IDependency>().Tag("MyDep").To<Dependency>())
// Configure auto-wiring and inject dependency by tag "MyDep"
using (container.Bind<IService>().To<Service>(
    ctx => new Service(ctx.Container.Inject<IDependency>("MyDep"))))
{
    // Resolve an instance
    var instance = container.Resolve<IService>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/DependencyTag.cs)

### Func

``` CSharp
Func<IService> func = () => new Service(new Dependency());
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IService>().To(ctx => func()))
{
    // Resolve an instance
    var instance = container.Resolve<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Func.cs)

### Generic Auto-wiring

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
// Configure auto-wiring
using (container.Bind<IService<TT>>().To<Service<TT>>(
    // Select the constructor
    ctx => new Service<TT>(ctx.Container.Inject<IDependency>())))
{
    // Resolve a generic instance
    var instance = container.Resolve<IService<int>>();

    instance.ShouldBeOfType<Service<int>>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/GenericAutowiring.cs)

### Generics

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind(typeof(IService<>)).To(typeof(Service<>)))
{
    // Resolve a generic instance
    var instance = container.Resolve<IService<int>>();

    instance.ShouldBeOfType<Service<int>>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Generics.cs)

### Resolve Func

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve Func
    var func = container.Resolve<Func<IService>>();
    // Get the instance via Func
    var instance = func();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveFunc.cs)

### Resolve Lazy

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve Lazy
    var lazy = container.Resolve<Lazy<IService>>();
    // Get the instance via Lazy
    var instance = lazy.Value;

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveLazy.cs)

### Resolve Tuple

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
using (container.Bind<INamedService>().To<NamedService>(
    ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name")))
{
    // Resolve Tuple
    var tuple = container.Resolve<Tuple<IService, INamedService>>();

    tuple.Item1.ShouldBeOfType<Service>();
    tuple.Item2.ShouldBeOfType<NamedService>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveTuple.cs)

### Resolve ValueTuple

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
using (container.Bind<INamedService>().To<NamedService>(
    ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name")))
{
    // Resolve ValueTuple
    var valueTuple = container.Resolve<(IService service, INamedService namedService)>();

    valueTuple.service.ShouldBeOfType<Service>();
    valueTuple.namedService.ShouldBeOfType<NamedService>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveValueTuple.cs)

### Tags

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Tag(10).Tag().Tag("abc").To<Service>())
{
    // Resolve instances using tags
    var instance1 = container.Resolve<IService>("abc".AsTag());
    var instance2 = container.Resolve<IService>(10.AsTag());

    // Resolve the instance using the empty tag
    var instance3 = container.Resolve<IService>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Tags.cs)

### Auto-wiring

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve an instance
    var instance = container.Resolve<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Autowiring.cs)

### Child Container

``` CSharp
// Create a parent container
using (var parentContainer = Container.Create())
// Create a child container
using (var childContainer = parentContainer.CreateChild())
{
    childContainer.Parent.ShouldBe(parentContainer);
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ChildContainer.cs)

### Method Injection

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To<InitializingNamedService>(
    // Select the constructor
    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
    // Select the method and inject its parameters
    // First as arguments[0]
    // Second as just dependency of type IDependency
    ctx => ctx.It.Initialize((string)ctx.Args[0], ctx.Container.Inject<IDependency>())))
{
    // Resolve the instance "alpha"
    var instance = container.Resolve<INamedService>("alpha");

    instance.ShouldBeOfType<InitializingNamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var func = container.Resolve<Func<string, INamedService>>();
    var otherInstance = func("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/MethodInjection.cs)

### Property Injection

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To<InitializingNamedService>(
    // Select the constructor to use
    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
    // Select the property to inject
    // And inject arguments[0]
    ctx => ctx.Container.Inject(ctx.It.Name, (string)ctx.Args[0])))
{
    // Resolve the instance "alpha"
    var instance = container.Resolve<INamedService>("alpha");

    instance.ShouldBeOfType<InitializingNamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var func = container.Resolve<Func<string, INamedService>>();
    var otherInstance = func("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/PropertyInjection.cs)

### Singleton lifetime

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().As(Lifetime.Singleton).To<Service>())
{
    // Resolve one instance twice
    var instance1 = container.Resolve<IService>();
    var instance2 = container.Resolve<IService>();

    instance1.ShouldBe(instance2);
}

// Other lifetimes:
// Transient - A new instance each time (default)
// ContainerSingleton - Singleton per container
// ScopeSingleton - Singleton per scope
// ThreadSingleton - Singleton per thread for NET 4.0+, .NET Core 1.0+, .NET Standard 2.0+
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SingletonLifetime.cs)

### Constructor Auto-wiring

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure a container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
// Configure via auto-wiring
using (container.Bind<IService>().To<Service>(
    // Select the constructor and specify its arguments
    ctx => new Service(ctx.Container.Inject<IDependency>(), "some state")))
{
    // Resolve an instance
    var instance = container.Resolve<IService>();

    instance.ShouldBeOfType<Service>();
    instance.State.ShouldBe("some state");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConstructorAutowiring.cs)

### Manual Auto-wiring

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
// Use full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
// Configure auto-wiring
using (container.Bind<INamedService>().To<InitializingNamedService>(
    // Select the constructor and inject its parameters
    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
    // Configure the method to invoke after the inctance's creation
    ctx => ctx.It.Initialize("some name", ctx.Container.Inject<IDependency>())))
{
    // Resolve an instance
    var instance = container.Resolve<INamedService>();

    instance.ShouldBeOfType<InitializingNamedService>();
    instance.Name.ShouldBe("some name");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ManualAutowiring.cs)

### Resolve all appropriate instances as ICollection

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Tag(1).To<Service>())
using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve all appropriate instances
    var instances = container.Resolve<ICollection<IService>>();

    instances.Count.ShouldBe(3);
    foreach (var instance in instances)
    {
        instance.ShouldBeOfType<Service>();
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Collection.cs)

### Resolve all appropriate instances as IEnumerable

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Tag(1).To<Service>())
using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve all appropriate instances
    var instances = container.Resolve<IEnumerable<IService>>().ToList();

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
// Configure a container, using full auto-wiring
using (container.Bind<IDependency>().To<Dependency>())
// Select the constructor and inject argument[0] as the second parameter of type 'string'
using (container.Bind<INamedService>().To(
    ctx => func(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0])))
{
    // Resolve the instance "alpha" passing the array of arguments
    var instance = container.Resolve<INamedService>("alpha");

    instance.ShouldBeOfType<NamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var getterFunc = container.Resolve<Func<string, INamedService>>();
    var otherInstance = getterFunc("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/FuncWithArguments.cs)

### Resolve all appropriate instances as IObservable source

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Tag(1).To<Service>())
using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve source for all appropriate instances
    var instancesSource = container.Resolve<IObservable<IService>>();

    var observer = new Mock<IObserver<IService>>();
    using (instancesSource.Subscribe(observer.Object))
    {
        observer.Verify(o => o.OnNext(It.IsAny<IService>()), Times.Exactly(3));
        observer.Verify(o => o.OnCompleted(), Times.Once);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Observable.cs)

### Resolve Using Arguments

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To<NamedService>(
    // Select the constructor and inject its parameters
    ctx => new NamedService(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0])))
{
    // Resolve the instance "alpha"
    var instance = container.Resolve<INamedService>("alpha");

    instance.ShouldBeOfType<NamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var func = container.Resolve<Func<string, INamedService>>();
    var otherInstance = func("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveWithArgs.cs)

### Resolve all appropriate instances as ISet

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Tag(1).To<Service>())
using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve all appropriate instances
    var instances = container.Resolve<ISet<IService>>();

    instances.Count.ShouldBe(3);
    foreach (var instance in instances)
    {
        instance.ShouldBeOfType<Service>();
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Set.cs)

### Auto dispose singleton during container's dispose

``` CSharp
var disposableService = new Mock<IDisposableService>();

// Create a container
using (var container = Container.Create())
{
    // Configure the container
    container.Bind<IService>().As(Lifetime.Singleton).To<IDisposableService>(ctx => disposableService.Object).ToSelf();

    // Resolve instances
    var instance1 = container.Resolve<IService>();
    var instance2 = container.Resolve<IService>();

    instance1.ShouldBe(instance2);
}

disposableService.Verify(i => i.Dispose(), Times.Once);
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AutoDisposeSingletonDuringContainersDispose.cs)

### Configuration class

``` CSharp
public void Run()
{
    // Create a container and configure it
    using (var container = Container.Create().Using<Glue>())
    {
        // Resolve an instance
        var instance = container.Resolve<IService>();

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

### Configuration via a text metadata

``` CSharp
// Create a container and configure it from the metadata string
using (var container = Container.Create().Using(
    "ref IoC.Tests;" +
    "using IoC.Tests.UsageScenarios;" +
    "Bind<IDependency>().To<Dependency>();" +
    "Bind<IService>().To<Service>();"))
{
    // Resolve an instance
    var instance = container.Resolve<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConfigurationText.cs)

### Change configuration on-the-fly

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
{
    // Configure the container using the Transient (default) lifetime
    using (container.Bind<IService>().To<Service>())
    {
        // Resolve instances
        var instance1 = container.Resolve<IService>();
        var instance2 = container.Resolve<IService>();

        instance1.ShouldNotBe(instance2);
    }

    // Reconfigure the container using the Singleton lifetime
    using (container.Bind<IService>().As(Lifetime.Singleton).To<Service>())
    {
        // Resolve the instance twice
        var instance1 = container.Resolve<IService>();
        var instance2 = container.Resolve<IService>();

        instance1.ShouldBe(instance2);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ChangeConfigurationOnTheFly.cs)

### Aspect Oriented Autowiring

``` CSharp
public void Run()
{
    var console = new Mock<IConsole>();

    // Create a base container
    using (var baseContainer = Container.Create("base"))
    // Configure some child container
    using (var childContainer = baseContainer.CreateChild("child"))
    // Configure the child container by custom aspect oriented autowring strategy
    using (childContainer.Bind<IAutowiringStrategy>().To<AspectOrientedAutowiringStrategy>())
    // Configure the container
    using (childContainer.Bind<IConsole>().To(ctx => console.Object))
    using (childContainer.Bind<IClock>().Tag("MyClock").To<Clock>())
    using (childContainer.Bind<string>().Tag("Prefix").To(ctx => "info"))
    using (childContainer.Bind<ILogger>().To<Logger>())
    {
        var logger = childContainer.Resolve<ILogger>();

        // Log message
        logger.Log("Hello");
    }

    // Check the console output
    console.Verify(i => i.WriteLine(It.IsRegex(".+ - info: Hello")));
}

/// <summary>
/// Attribute for constructor or methods that is ready for autowiring
/// </summary>
[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
public class AutowiringAttribute : Attribute
{
    public AutowiringAttribute(int order = 0, object defaultTag = null)
    {
        Order = order;
        DefaultTag = defaultTag;
    }

    public int Order { get; }

    public object DefaultTag { get; }
}

/// <summary>
/// Parameters` attribute to define a tag value
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public class TagAttribute: Attribute
{
    public TagAttribute([CanBeNull] object tag) => Tag = tag;

    [CanBeNull] public object Tag { get; }
}

private class AspectOrientedAutowiringStrategy : IAutowiringStrategy
{
    public IMethod<ConstructorInfo> SelectConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors) => (
            from ctor in constructors
            let autowringAttribute = ctor.Info.GetCustomAttribute<AutowiringAttribute>()
            // filters constructors containing the attribute Autowiring
            where ctor != null
            // sorts constructors by autowringAttribute.Order
            orderby autowringAttribute.Order
            select ctor)
        // Get a first appropriate constructor
        .First();

    public IEnumerable<IMethod<MethodInfo>> GetInitializers(IEnumerable<IMethod<MethodInfo>> methods) =>
        from method in methods
        let autowringAttribute = method.Info.GetCustomAttribute<AutowiringAttribute>()
        // filters methods/property setters containing the attribute Autowiring
        where autowringAttribute != null
        // sorts methods/property setters by autowringAttribute.Order
        orderby autowringAttribute.Order
        where (
            from parameter in method.Info.GetParameters()
            let injectAttribute = parameter.GetCustomAttribute<TagAttribute>()
            // filters parameters containing a custom tag value to make a dependency injection
            where injectAttribute?.Tag != null || autowringAttribute.DefaultTag != null
            // redefines the default dependency
            select method.TryInjectDependency(parameter.Position, parameter.ParameterType, injectAttribute?.Tag ?? autowringAttribute.DefaultTag))
         // checks that all custom injection were successfully
        .All(isInjected => isInjected)
        select method;
}

public interface IConsole
{
    void WriteLine(string test);
}

public interface IClock
{
    DateTimeOffset Now { get; }
}

public class Clock : IClock
{
    [Autowiring] public Clock() { }

    public DateTimeOffset Now => DateTimeOffset.Now;
}

public interface ILogger
{
    void Log(string message);
}

public class Logger : ILogger
{
    private readonly IConsole _console;
    private IClock _clock;

    // Constructor injection
    [Autowiring]
    public Logger(IConsole console) => _console = console;

    // Method injection
    [Autowiring(1)]
    public void Initialize([Tag("MyClock")] IClock clock) => _clock = clock;

    // Property injection
    public string Prefix { get; [Autowiring(2, "Prefix")] set; }

    public void Log(string message) => _console?.WriteLine($"{_clock.Now} - {Prefix}: {message}");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AspectOrientedAutowiring.cs)

### Custom Child Container

``` CSharp
public void Run()
{
    // Create a root container
    using (var container = Container.Create())
    // Configure the root container to use a custom container as a child container
    using (container.Bind<IContainer>().Tag(WellknownContainers.Child).To<MyContainer>())
    // Create the custom child container
    using (var childContainer = container.CreateChild("abc"))
    // Configure our container
    using (childContainer.Bind<IDependency>().To<Dependency>())
    using (childContainer.Bind<IService>().To<Service>())
    {
        // Resolve an instance
        var instance = childContainer.Resolve<IService>();

        childContainer.ShouldBeOfType<MyContainer>();
        instance.ShouldBeOfType<Service>();
    }
}

[SuppressMessage("ReSharper", "PossibleNullReferenceException")]
public class MyContainer: IContainer
{
    public MyContainer(IContainer currentContainer) => Parent = currentContainer;

    public IContainer Parent { get; }

    public bool TryRegister(IEnumerable<Key> keys, IoC.IDependency dependency, ILifetime lifetime, out IDisposable registrationToken) 
        => Parent.TryRegister(keys, dependency, lifetime, out registrationToken);

    public bool TryGetDependency(Key key, out IoC.IDependency dependency, out ILifetime lifetime)
        => Parent.TryGetDependency(key, out dependency, out lifetime);

    public bool TryGetResolver<T>(Type type, out Resolver<T> resolver, IContainer container = null)
        => Parent.TryGetResolver(type, out resolver, container);

    public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, IContainer container = null)
        => Parent.TryGetResolver(type, tag, out resolver, container);

    public void Dispose() { }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public IEnumerator<IEnumerable<Key>> GetEnumerator() => Parent.GetEnumerator();

    public IDisposable Subscribe(IObserver<ContainerEvent> observer) => Parent.Subscribe(observer);
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomChildContainer.cs)

### Custom Lifetime

``` CSharp
public void Run()
{
    // Create a container
    using (var container = Container.Create())
    // Configure the container
    using (container.Bind<IDependency>().To<Dependency>())
    using (container.Bind<IService>().Lifetime(new MyTransientLifetime()).To<Service>())
    {
        // Resolve an instance
        var instance = container.Resolve<IService>();

        instance.ShouldBeOfType<Service>();
    }
}

public class MyTransientLifetime : ILifetime
{
    public Expression Build(Expression expression, IBuildContext buildContext, object state = default(object))
        => expression;

    public ILifetime Clone() => new MyTransientLifetime();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomLifetime.cs)

### Replace Lifetime

``` CSharp
public void Run()
{
    var counter = new Mock<ICounter>();

    // Create a container
    using (var container = Container.Create())
    using (container.Bind<ICounter>().To(ctx => counter.Object))
    // Replace the Singleton lifetime
    using (container.Bind<ILifetime>().Tag(Lifetime.Singleton).To<MySingletonLifetime>(
            // Select the constructor
            ctx => new MySingletonLifetime(
                // Inject the singleton lifetime from the parent container to use its logic
                ctx.Container.Parent.Inject<ILifetime>(Lifetime.Singleton),
                // Inject a counter
                ctx.Container.Inject<ICounter>())))
    // Configure the container
    using (container.Bind<IDependency>().To<Dependency>())
    // Use the custom implementation of Singleton lifetime
    using (container.Bind<IService>().As(Lifetime.Singleton).To<Service>())
    {
        // Resolve one instance twice using the custom Singletine lifetime
        var instance1 = container.Resolve<IService>();
        var instance2 = container.Resolve<IService>();

        instance1.ShouldBe(instance2);
    }

    counter.Verify(i => i.Increment(), Times.Exactly(2));
}

public interface ICounter
{
    void Increment();
}

public class MySingletonLifetime : ILifetime
{
    private static readonly MethodInfo IncrementCounterMethodInfo = typeof(MySingletonLifetime).GetTypeInfo().DeclaredMethods.Single(i => i.Name == nameof(IncrementCounter));
    private readonly ILifetime _baseSingletonLifetime;
    private readonly ICounter _counter;

    public MySingletonLifetime(ILifetime baseSingletonLifetime, ICounter counter)
    {
        _baseSingletonLifetime = baseSingletonLifetime;
        _counter = counter;
    }

    public Expression Build(Expression expression, IBuildContext buildContext, object state = default(object))
    {
        // Build expression using base lifetime
        expression = _baseSingletonLifetime.Build(expression, buildContext, state);

        // Define `this` variable
        var thisVar = buildContext.AppendValue(this);

        return Expression.Block(
            // Adds statement this.IncrementCounter()
            Expression.Call(thisVar, IncrementCounterMethodInfo),
            expression);
    }

    public ILifetime Clone() => new MySingletonLifetime(_baseSingletonLifetime.Clone(), _counter);

    // Just counting the number of calls
    internal void IncrementCounter() => _counter.Increment();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ReplaceLifetime.cs)

### Scope Singleton lifetime

``` CSharp
// Create a container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().As(Lifetime.ScopeSingleton).To<Dependency>())
{
    using (container.Bind<IService>().As(Lifetime.ScopeSingleton).To<Service>())
    {
        // Default scope
        var instance1 = container.Resolve<IService>();
        var instance2 = container.Resolve<IService>();
        instance1.ShouldBe(instance2);

        // Scope "1"
        using (var scope = new Scope("1"))
        using (scope.Begin())
        {
            var instance3 = container.Resolve<IService>();
            var instance4 = container.Resolve<IService>();

            instance3.ShouldBe(instance4);
            instance3.ShouldNotBe(instance1);
        }

        // Default scope again
        var instance5 = container.Resolve<IService>();
        instance5.ShouldBe(instance1);
    }

    // Reconfigure to check dependencies only
    using (container.Bind<IService>().As(Lifetime.Transient).To<Service>())
    {
        // Default scope
        var instance1 = container.Resolve<IService>();
        var instance2 = container.Resolve<IService>();
        instance1.Dependency.ShouldBe(instance2.Dependency);

        // Scope "1"
        using (var scope = new Scope("1"))
        using (scope.Begin())
        {
            var instance3 = container.Resolve<IService>();
            instance3.Dependency.ShouldNotBe(instance1.Dependency);
        }
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ScopeSingletonLifetime.cs)

### Wrapper

``` CSharp
public void Run()
{
    var console = new Mock<IConsole>();

    // Create a base container
    using (var baseContainer = Container.Create("base"))
    // Configure it for base logger
    using (baseContainer.Bind<IConsole>().To(ctx => console.Object))
    using (baseContainer.Bind<ILogger>().To<Logger>())
    {
        // Configure some child container
        using (var childContainer = baseContainer.CreateChild("child"))
        // Configure console
        using (childContainer.Bind<IConsole>().To(ctx => console.Object))
        using (childContainer.Bind<ILogger>().To<TimeLogger>(
            // Inject the logger from the parent container to our new logger
            ctx => new TimeLogger(ctx.Container.Parent.Inject<ILogger>())))
        {
            var logger = childContainer.Resolve<ILogger>();

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

    public Logger(IConsole console) => _console = console;

    public void Log(string message) => _console.WriteLine(message);
}

public class TimeLogger: ILogger
{
    private readonly ILogger _baseLogger;

    public TimeLogger(ILogger baseLogger) => _baseLogger = baseLogger;

    // Adds current time before a message.
    public void Log(string message) => _baseLogger.Log(DateTimeOffset.Now + ": " + message);
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Wrapper.cs)

### Generator

``` CSharp
public void Run()
{
    Func<int, int, (int, int)> valueGetter = (sequential, random) => (sequential, random);

    // Create a container and configure it using a configuration class
    using (var container = Container.Create().Using<Generators>())
    // Configure a binding to inject 2 number from different generators to result (int, int) of the Func.
    // Inject the dependency of sequential number to the first element
    // Inject the dependency of random number to the second element
    using (container.Bind<(int, int)>().To(
        ctx => valueGetter(
            ctx.Container.Inject<int>(GeneratorType.Sequential),
            ctx.Container.Inject<int>(GeneratorType.Random))))
    {
        // Generate sequential numbers
        var sequential1 = container.Resolve<int>(GeneratorType.Sequential.AsTag());
        var sequential2 = container.Resolve<int>(GeneratorType.Sequential.AsTag());

        sequential2.ShouldBe(sequential1 + 1);

        // Generate a random number
        var random = container.Resolve<int>(GeneratorType.Random.AsTag());

        // Generate a set of numbers
        var setOfValues = container.Resolve<(int, int)>();

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

    // Create a container
    using (var container = Container.Create())
    // Configure the container: 1,2,3 are tags to produce cyclic dependencies
    using (container.Bind<IIssueResolver>().To(ctx => issueResolver.Object))
    using (container.Bind<ILink>().To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1))))
    using (container.Bind<ILink>().Tag(1).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(2))))
    using (container.Bind<ILink>().Tag(2).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(3))))
    using (container.Bind<ILink>().Tag(3).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1))))
    {
        try
        {
            // Resolve the first link
            container.Resolve<ILink>();
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
    public Link(ILink link) { }
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

    // Create a container
    using (var container = Container.Create())
    // Configure the container
    using (container.Bind<int>().Tag("IdGenerator").To(ctx => generator()))
    using (container.Bind(typeof(IInstantMessenger<>)).To(typeof(InstantMessenger<>)))
    using (container.Bind<IMessage>().To<Message>(ctx => new Message(ctx.Container.Inject<int>("IdGenerator"), (string)ctx.Args[0], (string)ctx.Args[1])))
    {
        var instantMessenger = container.Resolve<IInstantMessenger<IMessage>>();
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

    public InstantMessenger(Func<string, string, T> createMessage) => _createMessage = createMessage;

    public IDisposable Subscribe(IObserver<T> observer)
    {
        _observers.Add(observer);
        return Disposable.Create(() => _observers.Remove(observer));
    }

    public void SendMessage(string address, string text)
        => _observers.ForEach(observer => observer.OnNext(_createMessage(address, text)));
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SimpleInstantMessenger.cs)

### Samples Model

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
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Models.cs)

## The build state

[<img src="http://tcavs2015.cloudapp.net/app/rest/builds/buildType:(id:DevTeam_IoCContainer_Build)/statusIcon"/>](http://tcavs2015.cloudapp.net/viewType.html?buildTypeId=DevTeam_IoCContainer_Build&guest=1)
