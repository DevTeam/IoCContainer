# Simple, powerful and fast Inversion of Control container for .NET

[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE) [<img src="http://tcavs2015.cloudapp.net/app/rest/builds/buildType:(id:DevTeam_IoCContainer_Build)/statusIcon"/>](http://tcavs2015.cloudapp.net/viewType.html?buildTypeId=DevTeam_IoCContainer_Build&guest=1)

## [Schrödinger's cat](Samples/ShroedingersCat) shows how it works [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://dotnetfiddle.net/YoDYA7)

### The reality is that

![Cat](https://github.com/DevTeam/IoCContainer/blob/master/Docs/Images/cat.jpg?raw=true)

### Let's create an abstraction

```csharp
interface IBox<out T> { T Content { get; } }

interface ICat { State State { get; } }

enum State { Alive, Dead }
```

### Here is our implementation

```csharp
class CardboardBox<T> : IBox<T>
{
    public CardboardBox(T content) => Content = content;

    public T Content { get; }
}

class ShroedingersCat : ICat
{
  // Represents the superposition of the states
  private readonly Lazy<State> _superposition;

  public ShroedingersCat(Lazy<State> superposition) => _superposition = superposition;

  // The decoherence of the superposition at the time of observation via an irreversible process
  public State State => _superposition.Value;

  public override string ToString() => $"{State} cat";
}
```

_**It is important to note that our abstraction and our implementation do not know anything about any IoC containers at all**_

### Let's glue all together

Just add the package reference to [IoC.Container](https://www.nuget.org/packages/IoC.Container). It ships entirely as NuGet packages.

_Using NuGet packages allows you to optimize your application to include only the necessary dependencies._

- Package Manager

  ```
  Install-Package IoC.Container
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.Container
  ```

Declare the required dependencies in a dedicated class _Glue_.

_You can do this anywhere in your code, but collecting this information in one place is often the best solution to maintain order._

```csharp
public class Glue : IConfiguration
{
  public IEnumerable<IToken> Apply(IMutableContainer container)
  {
    yield return container
      // Represents a cardboard box with any content
      .Bind<IBox<TT>>().To<CardboardBox<TT>>()
      // Represents schrodinger's cat
      .Bind<ICat>().To<ShroedingersCat>();

    // Models a random subatomic event that may or may not occur
    var indeterminacy = new Random();
    // Represents a quantum superposition of 2 states: Alive or Dead
    yield return container.Bind<State>().To(ctx => (State)indeterminacy.Next(2));
  }
}
```

### Time to open boxes!

```csharp
// Creates an Inversion of Control container
using var container = Container.Create().Using<Glue>();

// This is the Composition Root. It gets a cardboard box in the same way as the following expression:
// var box = new CardboardBox<ICat>(new ShroedingersCat(new Lazy<State>(() => (State)indeterminacy.Next(2))));
var box = container.Resolve<IBox<ICat>>();
// Checks the cat's state
WriteLine(box.Content);
```

Several aspects of the [Composition Root](https://blog.ploeh.dk/2011/07/28/CompositionRoot/):

- **As close to Init or Entry Point as possible:** It should be as close as possible to the application's entry point.
- **Single location for object construction:** A Composition Root is a (preferably) unique location in an application where modules are composed together.
- **The Composition Root is an application infrastructure component:** Only applications should have Composition Roots. Libraries and frameworks shouldn't.
- **A IoC Container should only be referenced from the Composition Root:** All other modules should have no reference to the container.
- **Predictable Dependency Graph:** It is better to have a pre-constructed, pre-discovered dependency graph.

Each dependency is resolved by a strongly-typed block of statements like the operator `new` which is compiled on the fly from the coresponding expression tree to create or to get a required dependency instance with minimal impact on performance or memory consumtion. For instance, the getting (or injecting) of a box looks like:

```csharp
var indeterminacy = new Random();
var box = new CardboardBox<ICat>(new ShroedingersCat(new Lazy<State>(() => (State)indeterminacy.Next(2))));
```

It allows you to take full advantage of dependency injection everywhere and every time without any compromises - in the same way as just a `new` keyword to create any instances.

## NuGet packages

|     | binary packages | source code packages ¹ |
| --- | --- | ---|
| IoC container | [![NuGet](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container) | [![NuGet](https://buildstats.info/nuget/IoC.Container.Source)](https://www.nuget.org/packages/IoC.Container.Source) |
| ASP.NET Core | [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore)](https://www.nuget.org/packages/IoC.AspNetCore) | [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore.Source)](https://www.nuget.org/packages/IoC.AspNetCore.Source) |
| Interception | [![NuGet](https://buildstats.info/nuget/IoC.Interception)](https://www.nuget.org/packages/IoC.Interception) | [![NuGet](https://buildstats.info/nuget/IoC.Interception.Source)](https://www.nuget.org/packages/IoC.Interception.Source) |

¹ _source code packages_ require C# 7.0 or higher

## ASP.NET Core

### Add the  [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore)](https://www.nuget.org/packages/IoC.AspNetCore) reference (or [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore.Source)](https://www.nuget.org/packages/IoC.AspNetCore.Source))

- Package Manager

  ```
  Install-Package IoC.AspNetCore
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.AspNetCore
  ```

### For ASP.NET Core 3+ create the _IoC container_ and use the service provider factory based on this container at [Main](Samples/WebApplication3/Program.cs)

```csharp
public static void Main(string[] args)
{
  using var container = Container
    // Creates an Inversion of Control container
    .Create()
    // using Glue
    .Using<Glue>();

  // Creates a host
  using var host = Host
    .CreateDefaultBuilder(args)
    // Adds a service provider for the Inversion of Control container
    .UseServiceProviderFactory(new ServiceProviderFactory(container))
    .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
    .Build();

  host.Run();
}

```

### For ASP.NET Core 2 create the _IoC container_ with feature _AspNetCoreFeature_ and configure it at [Startup](Samples/WebApplication2/Startup.cs)

```csharp
public IServiceProvider ConfigureServices(IServiceCollection services)
{
  services.AddMvc().AddControllersAsServices();

  return Container
    // Creates an Inversion of Control container
    .Create()
    // using .NET ASP Feature
    .Using(new AspNetCoreFeature(services))
    // using Glue
    .Using<Glue>()
    // Resolves IServiceProvider
    .Resolve<IServiceProvider>();
}
```

For more information please see [this sample](Samples/AspNetCore).

## Interception

### Add the [![NuGet](https://buildstats.info/nuget/IoC.Interception)](https://www.nuget.org/packages/IoC.Interception) reference (or [![NuGet](https://buildstats.info/nuget/IoC.Interception.Source)](https://www.nuget.org/packages/IoC.Interception.Source))

- Package Manager

  ```
  Install-Package IoC.Interception
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.Interception
  ```

### Add _InterceptionFeature_ to intercept calls to _IService_ by your own _MyInterceptor_

```csharp
using var container = Container
  // Creates an Inversion of Control container
  .Create()
  // Using the feature InterceptionFeature
  .Using<InterceptionFeature>()
  // Configures binds
  .Bind<IService>().To<Service>()
  // Configures interception for IService calls
  .Intercept<IService>(new MyInterceptor());

container.Resolve<IService>();

```

where _MyInterceptor_ could look like:

```csharp
class MyInterceptor : IInterceptor
{
  // Intercepts the invocations and appends some logic here
  public void Intercept(IInvocation invocation)
  {
    ..
  }
}
```

For details please see [this sample](IoC.Tests/UsageScenarios/Interception.cs).

## Why this one?

![Transient](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/BenchmarkDotNet.Artifacts/results/IoC.Benchmark.Transient-report.jpg)

![Singleton](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/BenchmarkDotNet.Artifacts/results/IoC.Benchmark.Singleton-report.jpg)

![Complex](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/BenchmarkDotNet.Artifacts/results/IoC.Benchmark.Complex-report.jpg)

- __Mean__ - arithmetic mean of root instances resolved per Nanosecond
- __Error__ - half of 99.9% confidence interval
- __StdDev__ - standard deviation of all measurements
- __Median__ - value separating the higher half of all measurements (50th percentile)
- __1 ns__ - 1 Nanosecond (0.000000001 sec)
### Supported Platforms

- .NET 4.0+
- [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/) 1.0+
- [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) 1.0+
- [UWP](https://docs.microsoft.com/en-us/windows/uwp/index) 10+

### Class References

- [.NET 4.8](Docs/IoC_net48.md)
- [.NET Standard 2.1](Docs/IoC_netstandard2.1.md)
- [.NET Core 3.1](Docs/IoC_netcoreapp3.1.md)
- [UWP 10.0](Docs/IoC_uap10.0.md)

### Easy Integration

- [ASP.NET Core](#aspnet-core)
- [Xamarin](https://github.com/DevTeam/IoCContainer/blob/master/Samples/XamarinXaml)
- [Windows Presentation Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WpfApp)
- [.NET core Windows Presentation Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WpfAppNetCore) 
- [Universal Windows Platform](https://github.com/DevTeam/IoCContainer/blob/master/Samples/UwpApp)
- [Windows Communication Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WcfServiceLibrary)
- [Entity Framework](https://github.com/DevTeam/IoCContainer/tree/master/Samples/EntityFrameworkCore)


## Usage Scenarios

- Powerful Injection
  - [Composition Root](#composition-root-)
  - [Resolve Func](#resolve-func-)
  - [Resolve Lazy](#resolve-lazy-)
  - [Resolve ThreadLocal](#resolve-threadlocal-)
  - [Resolve Tuple](#resolve-tuple-)
  - [Resolve ValueTuple](#resolve-valuetuple-)
  - [Method Injection](#method-injection-)
  - [Nullable Value Type Resolving](#nullable-value-type-resolving-)
  - [Property Injection](#property-injection-)
  - [Constructor Autowiring](#constructor-autowiring-)
  - [Containers Injection](#containers-injection-)
  - [Resolve instances as Array](#resolve-instances-as-array-)
  - [Resolve instances as ICollection](#resolve-instances-as-icollection-)
  - [Resolve instances as IEnumerable](#resolve-instances-as-ienumerable-)
  - [Resolve instances as IObservable source](#resolve-instances-as-iobservable-source-)
  - [Resolve Using Arguments](#resolve-using-arguments-)
  - [Resolve all appropriate instances as ISet](#resolve-all-appropriate-instances-as-iset-)
- Flexible Binding
  - [Autowiring](#autowiring-)
  - [Autowiring with initialization](#autowiring-with-initialization-)
  - [Configuration class](#configuration-class-)
  - [Generic Autowiring](#generic-autowiring-)
  - [Bindings](#bindings-)
  - [Change configuration on-the-fly](#change-configuration-on-the-fly-)
  - [Constant](#constant-)
  - [Generics](#generics-)
  - [Several Contracts](#several-contracts-)
  - [Tags](#tags-)
  - [Value](#value-)
  - [Dependency Tag](#dependency-tag-)
  - [Singleton lifetime](#singleton-lifetime-)
  - [Child Container](#child-container-)
  - [Container Singleton lifetime](#container-singleton-lifetime-)
  - [Scope Singleton lifetime](#scope-singleton-lifetime-)
  - [Thread Singleton Lifetime](#thread-singleton-lifetime-)
  - [Manual Wiring](#manual-wiring-)
  - [Struct](#struct-)
  - [Func](#func-)
  - [Func With Arguments](#func-with-arguments-)
  - [Auto dispose a singleton during owning container's dispose](#auto-dispose-a-singleton-during-owning-containers-dispose-)
  - [Default Parameters Injection](#default-parameters-injection-)
  - [Optional Injection](#optional-injection-)
  - [Configuration via a text metadata](#configuration-via-a-text-metadata-)
  - [Tracing](#tracing-)
  - [Validation](#validation-)
  - [Aspect Oriented](#aspect-oriented-)
- Asynchronous
  - [Asynchronous resolve](#asynchronous-resolve-)
  - [Asynchronous lightweight resolve](#asynchronous-lightweight-resolve-)
  - [Asynchronous construction](#asynchronous-construction-)
  - [Cancellation of asynchronous construction](#cancellation-of-asynchronous-construction-)
  - [Resolve instances via IAsyncEnumerable](#resolve-instances-via-iasyncenumerable-)
- Fully Customizable
  - [Custom Builder](#custom-builder-)
  - [Custom Child Container](#custom-child-container-)
  - [Custom Lifetime](#custom-lifetime-)
  - [Custom Tasks](#custom-tasks-)
  - [Interception](#interception-)
  - [Replace Lifetime](#replace-lifetime-)
- Other Samples
  - [Cyclic Dependency](#cyclic-dependency-)
  - [Generator](#generator-)
  - [Instant Messenger](#instant-messenger-)
  - [Wrapper](#wrapper-)

### Asynchronous resolve [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousResolve.cs)

Do you want to receive instances asynchronously? It's simple ...

``` CSharp
// Create the container and configure it
using var container = Container.Create()
    // Bind some dependency
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>().Container;

// Resolve an instance asynchronously
var instance = await container.Resolve<Task<IService>>();
```



### Asynchronous lightweight resolve [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousValueResolve.cs)

Asynchronously and economically. Why load a GC?

``` CSharp
// Create a container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind Service
    .Bind<IService>().To<Service>()
    .Container;

// Resolve an instance asynchronously via ValueTask
var instance = await container.Resolve<ValueTask<IService>>();
```



### Asynchronous construction [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousConstruction.cs)

It is easy to inject dependencies in asynchronous style.

``` CSharp
public async void Run()
{
    // Create the container and configure it
    using var container = Container.Create()
        // Bind some dependency
        .Bind<IDependency>().To<SomeDependency>()
        .Bind<Consumer>().To<Consumer>()
        .Container;

    // Resolve an instance asynchronously using TaskScheduler.Current
    var instance = await container.Resolve<Task<Consumer>>();

    // Check the instance's type
    instance.ShouldBeOfType<Consumer>();
}

public class SomeDependency: IDependency
{
    // Time-consuming logic constructor
    public SomeDependency() { }
}

public class Consumer
{
    public Consumer(Task<IDependency> dependency1, Task<IDependency> dependency2)
    {
        // Time-consuming logic
        var dep1 = dependency1.Result;
        var dep2 = dependency2.Result;
    }
}
```



### Cancellation of asynchronous construction [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousConstructionCancellation.cs)

It is possible to inject dependencies in asynchronous style and to cancel their creations using default _CancellationTokenSource_.

``` CSharp
public void Run()
{
    // Create a cancellation token source
    var cancellationTokenSource = new CancellationTokenSource();

    // Create the container and configure it
    using var container = Container.Create()
        // Bind cancellation token source
        .Bind<CancellationTokenSource>().To(ctx => cancellationTokenSource)
        // Bind cancellation token
        .Bind<CancellationToken>().To(ctx => ctx.Container.Inject<CancellationTokenSource>().Token)
        // Bind some dependency
        .Bind<IDependency>().To<SomeDependency>()
        .Bind<Consumer>().To<Consumer>()
        .Container;

    // Resolve an instance asynchronously
    var instanceTask = container.Resolve<Task<Consumer>>();

    // Cancel tasks
    cancellationTokenSource.Cancel();

    // Get an instance
    instanceTask.Result.ShouldBeOfType<Consumer>();
}

public class SomeDependency: IDependency
{
    // Time-consuming logic constructor with cancellation token
    public SomeDependency(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested) { }
    }
}

public class Consumer
{
    public Consumer(Task<IDependency> dependency1, Task<IDependency> dependency2)
    {
        // Time-consuming logic
        var dep1 = dependency1.Result;
        var dep2 = dependency2.Result;
    }
}
```



### Resolve instances via IAsyncEnumerable [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsyncEnumerables.cs)

It is easy to resolve an enumerator [IAsyncEnumerable<>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1) that provides asynchronous iteration over values of a type for every tags.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Using(CollectionFeature.Set)
    .Bind<IDependency>().To<Dependency>()
    // Bind to the default implementation
    .Bind<IService>().To<Service>()
    // Bind to the implementation #1
    .Bind<IService>().Tag(1).To<Service>()
    // Bind to the implementation #2
    .Bind<IService>().Tag(2).Tag("abc").To<Service>()
    // Bind to the implementation #3
    .Bind<IService>().Tag(3).To<Service>()
    .Container;

// Resolve all appropriate instances
var instances = container.Resolve<IAsyncEnumerable<IService>>();
var items = new List<IService>();
await foreach (var instance in instances) { items.Add(instance); }

// Check the number of resolved instances
items.Count.ShouldBe(4);

```



### Autowiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Autowiring.cs)

Auto-writing is most natural way to use containers. At first step we should create a container. At the second step we bind interfaces to their implementations. After that the container is ready to resolve dependencies.

``` CSharp
// Create the container and configure it, using full autowiring
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>()
    .Container;

// Resolve an instance of interface `IService`
var instance = container.Resolve<IService>();
```



### Autowiring with initialization [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AutoWiringWithInitialization.cs)

Sometimes instances required some actions before you give them to use - some methods of initialization or fields which should be defined. You can solve these things easy.

``` CSharp
// Create the container and configure it using full autowiring
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<INamedService>().To<InitializingNamedService>(
        // Configure the container to invoke method "Initialize" for every created instance of this type
        ctx => ctx.It.Initialize("initialized !!!", ctx.Container.Resolve<IDependency>()))
    .Container;

// Resolve an instance of interface `IService`
var instance = container.Resolve<INamedService>();

// Check the instance's type
instance.ShouldBeOfType<InitializingNamedService>();

// Check the initialization is ok
instance.Name.ShouldBe("initialized !!!");
```

:warning: It is not recommended because of it is a cause of hidden dependencies.

### Configuration class [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConfigurationClass.cs)

Configuration classes are used to dedicate a logic responsible for configuring containers.

``` CSharp
public void Run()
{
    // Create and configure the container
    using var container = Container.Create().Using<Glue>();
    // Resolve an instance
    var instance = container.Resolve<IService>();
}

ic class Glue : IConfiguration
{
public IEnumerable<IToken> Apply(IMutableContainer container)
{
    // Bind using full autowiring
    yield return container
        .Bind<IDependency>().To<Dependency>()
        .Bind<IService>().To<Service>();
}
}
```



### Generic Autowiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/GenericAutowiring.cs)

Auto-writing of generic types as simple as auto-writing of other types. Just use a generic parameters markers like _TT_, _TT1_, _TT2_ and etc. or TTI, TTI1, TTI2 ... for interfaces or TTS, TTS1, TTS2 ... for value types or other special markers like TTDisposable, TTDisposable1 and etc. TTList<>, TTDictionary<> ... or create your own generic parameters markers or bind open generic types.

``` CSharp
public void Run()
{
    // Create and configure the container using autowiring
    using var container = Container
        .Create()
        .Bind<IDependency>().To<Dependency>()
        // Bind using the predefined generic parameters marker TT (or TT1, TT2, TT3 ...)
        .Bind<IService<TT>>().To<Service<TT>>()
        // Bind using the predefined generic parameters marker TTList (or TTList1, TTList2 ...)
        // Of other cases there are TTComparable, TTComparable<in T>, TTEquatable<T>, TTEnumerable<out T>, TTDictionary<TKey, TValue> and etc.
        .Bind<IListService<TTList<int>>>().To<ListService<TTList<int>>>()
        // Bind using the custom generic parameters marker TCustom
        .Bind<IService<TTMy>>().Tag("custom marker").To<Service<TTMy>>()
        // Bind using the open generic type
        .Bind(typeof(IService<>)).Tag("open type").To(typeof(Service<>))
        .Container;

    // Resolve a generic instance
    var listService = container.Resolve<IListService<IList<int>>>();
    var instances = container.Resolve<ICollection<IService<int>>>();

    instances.Count.ShouldBe(3);
    // Check the instance's type
    foreach (var instance in instances)
    {
        instance.ShouldBeOfType<Service<int>>();
    }

    listService.ShouldBeOfType<ListService<IList<int>>>();
}

// Custom generic type marker using predefined attribute `GenericTypeArgument`
[GenericTypeArgument]
class TTMy { }
}
}
```



### Bindings [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Bindings.cs)

It is possible to bind any number of types.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind using several tags
    .Bind<IService>().Bind<IAnotherService>().Tag("abc").To<Service>()
    .Container;

// Resolve instances using tags
var instance1 = container.Resolve<IService>("abc".AsTag());
var instance2 = container.Resolve<IAnotherService>("abc".AsTag());

```



### Change configuration on-the-fly [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ChangeConfigurationOnTheFly.cs)



``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Container;

// Configure `IService` as Transient
using (container.Bind<IService>().To<Service>())
{
    // Resolve instances
    var instance1 = container.Resolve<IService>();
    var instance2 = container.Resolve<IService>();

    // Check that instances are not equal
    instance1.ShouldNotBe(instance2);
}

// Reconfigure `IService` as Singleton
using (container.Bind<IService>().As(Lifetime.Singleton).To<Service>())
{
    // Resolve the singleton twice
    var instance1 = container.Resolve<IService>();
    var instance2 = container.Resolve<IService>();

    // Check that instances are equal
    instance1.ShouldBe(instance2);
}

```



### Constant [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Constant.cs)

It's obvious here.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<int>().To(ctx => 10)
    .Container;
// Resolve an integer
var val = container.Resolve<int>();
// Check the value
val.ShouldBe(10);
```



### Generics [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Generics.cs)

Auto-writing of generic types via binding of open generic types or generic type markers are working the same way.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind open generic interface to open generic implementation
    .Bind(typeof(IService<>)).To(typeof(Service<>))
    // Or (it is working the same) just bind generic interface to generic implementation, using marker classes TT, TT1, TT2 and so on
    .Bind<IService<TT>>().Tag("just generic").To<Service<TT>>()
    .Container;

// Resolve a generic instance using "open generic" binding
var instance1 = container.Resolve<IService<int>>();

// Resolve a generic instance using "just generic" binding
var instance2 = container.Resolve<IService<string>>("just generic".AsTag());
```



### Several Contracts [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SeveralContracts.cs)

It is possible to bind several types to single implementation.

``` CSharp
// Create and configure the container, using full autowiring
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<Service, IService, IAnotherService>().To<Service>()
    .Container;

// Resolve instances
var instance1 = container.Resolve<IService>();
var instance2 = container.Resolve<IAnotherService>();
```



### Tags [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Tags.cs)

Tags are useful while binding to several implementations.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind using several tags
    .Bind<IService>().Tag(10).Tag().Tag("abc").To<Service>()
    .Container;

// Resolve instances using tags
var instance1 = container.Resolve<IService>("abc".AsTag());
var instance2 = container.Resolve<IService>(10.AsTag());

// Resolve the instance using the empty tag
var instance3 = container.Resolve<IService>();
```



### Value [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Value.cs)

In this case the specific type is binded to the manually created instance based on an expression tree. This dependency will be introduced as is, without any additional overhead like _lambda call_ or _type cast_.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IService>().To(ctx => new Service(new Dependency()))
    .Container;

// Resolve an instance
var instance = container.Resolve<IService>();
```



### Dependency Tag [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/DependencyTag.cs)

Use a _tag_ to inject specific dependency from several bindings of the same types.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().Tag("MyDep").To<Dependency>()
    // Configure autowiring and inject dependency tagged by "MyDep"
    .Bind<IService>().To<Service>(ctx => new Service(ctx.Container.Inject<IDependency>("MyDep")))
    .Container;

// Resolve an instance
var instance = container.Resolve<IService>();
```



### Singleton lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SingletonLifetime.cs)

[Singleton](https://en.wikipedia.org/wiki/Singleton_pattern) is a design pattern which stands for having only one instance of some class during the whole application lifetime. The main complaint about Singleton is that it contradicts the Dependency Injection principle and thus hinders testability. It essentially acts as a global constant, and it is hard to substitute it with a test when needed. The _Singleton lifetime_ is indispensable in this case.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Use the Singleton lifetime
    .Bind<IService>().As(Singleton).To<Service>()
    .Container;

// Resolve the singleton twice
var parentInstance1 = container.Resolve<IService>();
var parentInstance2 = container.Resolve<IService>();

// Check that instances from the parent container are equal
parentInstance1.ShouldBe(parentInstance2);

// Create a child container
using var childContainer = container.Create();
// Resolve the singleton twice
var childInstance1 = childContainer.Resolve<IService>();
var childInstance2 = childContainer.Resolve<IService>();

// Check that instances from the child container are equal
childInstance1.ShouldBe(childInstance2);

// Check that instances from different containers are equal
parentInstance1.ShouldBe(childInstance1);

```

The lifetime could be:
- _Transient_ - a new instance is creating each time (it's default lifetime)
- [_Singleton_](https://en.wikipedia.org/wiki/Singleton_pattern) - single instance
- _ContainerSingleton_ - singleton per container
- _ScopeSingleton_ - singleton per scope

### Child Container [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ChildContainer.cs)

Child containers allow to override or just to add bindings of a parent containers without any influence on a parent containers. This is most useful when there are some base parent container(s). And these containers are shared between several components which have its own child containers based on common parent container(s) and have some additional bindings.

``` CSharp
// Create the parent container
using var parentContainer = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind IService to Service
    .Bind<IService>().To<Service>()
    .Container;

using var childContainer = parentContainer
    .Create()
    // Override binding of IService to Service<int>
    .Bind<IService>().To<Service<int>>()
    .Container;

var instance1 = parentContainer.Resolve<IService>();
var instance2 = childContainer.Resolve<IService>();

childContainer.Parent.ShouldBe(parentContainer);
instance1.ShouldBeOfType<Service>();
instance2.ShouldBeOfType<Service<int>>();
```



### Container Singleton lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ContainerLifetime.cs)

Each container may have its own [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance for specific binding.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Use the Container Singleton lifetime
    .Bind<IService>().As(ContainerSingleton).To<Service>()
    .Container;

// Resolve the container singleton twice
var parentInstance1 = container.Resolve<IService>();
var parentInstance2 = container.Resolve<IService>();

// Check that instances from the parent container are equal
parentInstance1.ShouldBe(parentInstance2);

// Create a child container
using var childContainer = container.Create();
// Resolve the container singleton twice
var childInstance1 = childContainer.Resolve<IService>();
var childInstance2 = childContainer.Resolve<IService>();

// Check that instances from the child container are equal
childInstance1.ShouldBe(childInstance2);

// Check that instances from different containers are not equal
parentInstance1.ShouldNotBe(childInstance1);
```



### Scope Singleton lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ScopeSingletonLifetime.cs)

Each scope has its own [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance for specific binding. Scopes can be created, activated and deactivated. Scope can be injected like any other instance from container.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().As(ScopeSingleton).To<Dependency>()
    .Container;

// Use the Scope Singleton lifetime for instance
using (container.Bind<IService>().As(ScopeSingleton).To<Service>())
{
    // Resolve the default scope singleton twice
    var defaultScopeInstance1 = container.Resolve<IService>();
    var defaultScopeInstance2 = container.Resolve<IService>();

    // Check that instances from the default scope are equal
    defaultScopeInstance1.ShouldBe(defaultScopeInstance2);

    // Using the scope #1
    using var scope1 = container.CreateScope();
    using (scope1.Activate())
    {
        var scopeInstance1 = container.Resolve<IService>();
        var scopeInstance2 = container.Resolve<IService>();

        // Check that instances from the scope #1 are equal
        scopeInstance1.ShouldBe(scopeInstance2);

        // Check that instances from different scopes are not equal
        scopeInstance1.ShouldNotBe(defaultScopeInstance1);
    }

    // Default scope again
    var defaultScopeInstance3 = container.Resolve<IService>();

    // Check that instances from the default scope are equal
    defaultScopeInstance3.ShouldBe(defaultScopeInstance1);
}

// Reconfigure the container to check dependencies only
using (container.Bind<IService>().As(Transient).To<Service>())
{
    // Resolve transient instances
    var transientInstance1 = container.Resolve<IService>();
    var transientInstance2 = container.Resolve<IService>();

    // Check that transient instances are not equal
    transientInstance1.ShouldNotBe(transientInstance2);

    // Check that dependencies from the default scope are equal
    transientInstance1.Dependency.ShouldBe(transientInstance2.Dependency);

    // Using the scope #1
    using var scope2 = container.CreateScope();
    using (scope2.Activate())
    {
        // Resolve a transient instance in scope #2
        var transientInstance3 = container.Resolve<IService>();

        // Check that dependencies from different scopes are not equal
        transientInstance3.Dependency.ShouldNotBe(transientInstance1.Dependency);
    }
}

```



### Thread Singleton Lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ThreadSingletonLifetime.cs)

Sometimes it is useful to have a [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance per a thread (or more generally a singleton per something else). There is no special "lifetime" type in this framework to achieve this requirement, but it is quite easy create your own "lifetime" type for that using base type [_KeyBasedLifetime<>_](IoC/Lifetimes/KeyBasedLifetime.cs).

``` CSharp
public void Run()
{
    var finish = new ManualResetEvent(false);

    // Create and configure the container
    using var container = Container
        .Create()
        .Bind<IDependency>().To<Dependency>()
        // Bind an interface to an implementation using the singleton per a thread lifetime
        .Bind<IService>().Lifetime(new ThreadLifetime()).To<Service>()
        .Container;
    
    // Resolve the singleton twice
    var instance1 = container.Resolve<IService>();
    var instance2 = container.Resolve<IService>();
    IService instance3 = null;
    IService instance4 = null;

    var newThread = new Thread(() =>
    {
        instance3 = container.Resolve<IService>();
        instance4 = container.Resolve<IService>();
        finish.Set();
    });

    newThread.Start();
    finish.WaitOne();

    // Check that instances resolved in a main thread are equal
    instance1.ShouldBe(instance2);
    // Check that instance resolved in a new thread is not null
    instance3.ShouldNotBeNull();
    // Check that instances resolved in different threads are not equal
    instance1.ShouldNotBe(instance3);
    // Check that instances resolved in a new thread are equal
    instance4.ShouldBe(instance3);
}

// Represents the custom thead singleton lifetime based on the KeyBasedLifetime
public class ThreadLifetime : KeyBasedLifetime<int>
{
    // Creates a clone of the current lifetime (for the case with generic types)
    public override ILifetime Create() =>
        new ThreadLifetime();

    // Provides a key of an instance
    // If a key the same an instance is the same too
    protected override int CreateKey(IContainer container, object[] args) =>
        Thread.CurrentThread.ManagedThreadId;

    // Just returns created instance
    protected override T OnNewInstanceCreated<T>(T newInstance, int key, IContainer container, object[] args) =>
        newInstance;

    // Do nothing
    protected override void OnInstanceReleased(object releasedInstance, int key) { }
}
```



### Manual Wiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ManualWiring.cs)

In the case when the full control of creating an instance is required it is possible to do it in simple way without any performance impact.

``` CSharp
// Create and configure the container using manual wiring
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
    .Bind<INamedService>().To<InitializingNamedService>(
        // Select the constructor and inject the dependency into it
        ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
        // Configure the initializing method to invoke for the every created instance
        ctx => ctx.It.Initialize("some name", ctx.Container.Inject<IDependency>()))
    .Container;

// Resolve an instance
var instance = container.Resolve<INamedService>();

// Check the instance's type
instance.ShouldBeOfType<InitializingNamedService>();

// Check the injected dependency
instance.Name.ShouldBe("some name");

```

It's important to note that injection is possible by several ways in the sample above. **The first one** is an expressions like `ctx.Container.Inject<IDependency>()`. It uses the injection context `ctx` to access to the current (or other parents) container and method `Inject` to inject a dependency. But actually this method has no implementation, it ust a marker and it every such method wil be replaced by expression which creates dependency in place without any additional invocations. **Another one way** is to use an expressions like `ctx.Resolve<IDependency>()`. It will access a container each time to resolve a dependency. That is, each time it will look for the necessary binding in the container and call the method to create an instance of the dependency type. **In summary: wherever possible, use the first approach like `ctx.Container.Inject<IDependency>()`.**

### Struct [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Struct.cs)

Value types are fully supported avoiding any boxing/unboxing or cast operations, so the performance does not suffer!

``` CSharp
public void Run()
{
    // Create and configure the container
    using var container = Container
        .Create()
        .Bind<IDependency>().To<Dependency>()
        // Register the tracing builder
        .Bind<TracingBuilder, IBuilder>().As(Singleton).To<TracingBuilder>()
        // Register a struct
        .Bind<MyStruct>().To<MyStruct>()
        .Container;

    // Resolve an instance
    var instance = container.Resolve<MyStruct>();

    // Check the expression which was used to create an instances of MyStruct
    var expressions = container.Resolve<TracingBuilder>().Expressions;
    var structExpression = expressions[new Key(typeof(MyStruct))].ToString();
    // The actual code is "new MyStruct(new Dependency())"!
    structExpression.ShouldBe("new MyStruct(new Dependency())");
    // Obvious there are no any superfluous operations like a `boxing`, `unboxing` or `cast`,
    // just only what is really necessary to create an instance
}

public struct MyStruct
{
    public MyStruct(IDependency dependency) { }
}

// This builder saves expressions that used to create resolvers into a map
public class TracingBuilder : IBuilder
{
    public readonly IDictionary<Key, Expression> Expressions = new Dictionary<Key, Expression>();

    public Expression Build(IBuildContext context, Expression expression)
    {
        Expressions[context.Key] = expression;
        return expression;
    }
}
```



### Func [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Func.cs)

No comments. Everything is very simple!

``` CSharp
Func<IService> func = () => new Service(new Dependency());

// Create and configure the container
using var container = Container
    .Create()
    .Bind<IService>().To(ctx => func())
    .Container;

// Resolve an instance
var instance = container.Resolve<IService>();
```



### Func With Arguments [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/FuncWithArguments.cs)

It is easy to use Func<> with arguments and to pass these arguments to the created instances.

``` CSharp
Func<IDependency, string, INamedService> func = 
    (dependency, name) => new NamedService(dependency, name);

// Create and configure the container, using full autowiring
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind the constructor and inject argument[0] as the second parameter of type 'string'
    .Bind<INamedService>().To(ctx => func(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0]))
    .Container;

// Resolve the instance passing the string "alpha" into the array of arguments
var instance = container.Resolve<INamedService>("alpha");

// Check the instance's type
instance.ShouldBeOfType<NamedService>();

// Check that argument "alpha" was used during the construction of an instance
instance.Name.ShouldBe("alpha");

// Resolve a function to create instance
var getterFunc = container.Resolve<Func<string, INamedService>>();

// Run this function and pass the string "beta" as argument
var otherInstance = getterFunc("beta");

// Check that argument "beta" was used during constructing an instance
otherInstance.Name.ShouldBe("beta");
```



### Auto dispose a singleton during owning container's dispose [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AutoDisposeSingletonDuringContainersDispose.cs)

A [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance it's a very special instance. If it implements the _IDisposable_ (or IAsyncDisposable) interface the _Sigleton_ lifetime take care about disposing this instance after disposing of the owning container (where this type was registered) or if after the binding cancelation.

``` CSharp
var disposableService = new Mock<IDisposableService>();

// Create and configure the container
using (
    var container = Container
    .Create()
    .Bind<IService>().As(Lifetime.Singleton).To<IDisposableService>(ctx => disposableService.Object)
    .Container)
{
    var disposableInstance = container.Resolve<IService>();
}

// Check the singleton was disposed after the container was disposed
disposableService.Verify(i => i.Dispose(), Times.Once);
disposableService.Verify(i => i.DisposeAsync(), Times.Once);
```



### Default Parameters Injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/DefaultParamsInjection.cs)



``` CSharp
public void Run()
{
    // Create the container and configure it
    using var container = Container.Create()
        .Bind<IDependency>().To<Dependency>()
        .Bind<IService>().To<SomeService>()
        .Container;

    // Resolve an instance
    var instance = container.Resolve<IService>();

    // Check the optional dependency
    instance.State.ShouldBe("empty");
}

public class SomeService: IService
{
    // "state" dependency is not resolved here but it has the default value "empty"
    public SomeService(IDependency dependency, string state = "empty")
    {
        Dependency = dependency;
        State = state;
    }

    public IDependency Dependency { get; }

    public string State { get; }
}
```



### Optional Injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/OptionalInjection.cs)



``` CSharp
public void Run()
{
    // Create the container and configure it
    using var container = Container.Create()
        .Bind<IDependency>().To<Dependency>()
        .Bind<IService>().To<SomeService>(ctx => 
            new SomeService(
                ctx.Container.Inject<IDependency>(),
                // injects default(string) if the dependency cannot be resolved
                ctx.Container.TryInject<string>(),
                // injects default(int) if the dependency cannot be resolved
                ctx.Container.TryInject<int>(),
                // injects int?, it has no value if the dependency cannot be resolved
                ctx.Container.TryInjectValue<int>()))
        .Container;

    // Resolve an instance
    var instance = container.Resolve<IService>();

    // Check the optional dependency
    instance.State.ShouldBe("empty,True,False");
}

public class SomeService: IService
{
    // "state" dependency is not resolved here but will be null value because it was injected optional
    public SomeService(IDependency dependency, string state, int? val1, int? val2)
    {
        Dependency = dependency;
        State = state ?? $"empty,{val1.HasValue},{val2.HasValue}";
    }

    public IDependency Dependency { get; }

    public string State { get; }
}
```



### Configuration via a text metadata [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConfigurationText.cs)

The container's configuration could be parsed and applied from simple text metadata.

``` CSharp
// Create and configure the container from a metadata string
using var container = Container.Create().Apply(
    "ref IoC.Tests;" +
    "using IoC.Tests.UsageScenarios;" +
    "Bind<IDependency>().As(Singleton).To<Dependency>();" +
    "Bind<IService>().To<Service>();")
    .Container;
// Resolve an instance
var instance = container.Resolve<IService>();
```



### Tracing [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Tracing.cs)

Tracing allows to explore most aspects of container behavior: creating and removing child containers, adding and removing bindings, compiling instance factories.

``` CSharp
var traceMessages = new List<string>();

// This block to mark the scope for "using" statements
{
    // Create and configure the root container
    using var rootContainer = Container
        .Create("root")
        // Aggregate trace messages to the list 'traceMessages'
        .Trace(message => traceMessages.Add(message))
        .Container;

    // Create and configure the parent container
    using var parentContainer = rootContainer
        .Create("parent")
        .Bind<IDependency>().To<Dependency>(ctx => new Dependency())
        .Container;

    // Create and configure the child container
    using var childContainer = parentContainer
        .Create("child")
        .Bind<IService<TT>>().To<Service<TT>>()
        .Container;

    childContainer.Resolve<IService<int>>();
}
// Every containers were disposed here

traceMessages.Count.ShouldBe(8);
```



### Validation [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Validation.cs)

It is easy to validate any binding without actual creation of instances.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IService>().To<Service>()
    .Container;

// Try getting a resolver of the interface `IService`
// Also there is other method overload allowing to get the detailed error about issue why this instance cannot be resolved
var canBeResolved = container.TryGetResolver<IService>(out _);

// 'Service' has the mandatory dependency 'IDependency' in the constructor,
// which was not registered and that is why it cannot be resolved
canBeResolved.ShouldBeFalse();

// Add the required binding to fix the the issue above
container.Bind<IDependency>().To<Dependency>();

canBeResolved = container.TryGetResolver<IService>(out _);

// Everything is ok now
canBeResolved.ShouldBeTrue();

```



### Aspect Oriented [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AspectOrientedAutowiring.cs)

This framework has no special attributes to support aspect oriented autowiring because of a production code should not have references to these special attributes. But this code may contain these attributes by itself. And it is quite easy to use these attributes for aspect oriented autowiring, see the sample below.

``` CSharp
public void Run()
{
    var console = new Mock<IConsole>();

    // Creates an aspect oriented autowiring strategy based on some custom `DependencyAttribute`
    var autowiringStrategy = AutowiringStrategies.AspectOriented()
        .Type<TypeAttribute>(attribute => attribute.Type)
        .Order<OrderAttribute>(attribute => attribute.Order)
        .Tag<TagAttribute>(attribute => attribute.Tag);

    // Create the root container
    using (var rootContainer = Container.Create("root"))
    // Configure the child container
    {
        using var childContainer = rootContainer
            .Create("child")
            // Configure the child container by the custom aspect oriented autowiring strategy
            .Bind<IAutowiringStrategy>().To(ctx => autowiringStrategy)
            // Configure the child container
            .Bind<IConsole>().Tag("MyConsole").To(ctx => console.Object)
            .Bind<Clock>().To<Clock>()
            .Bind<string>().Tag("Prefix").To(ctx => "info")
            .Bind<ILogger>().To<Logger>()
            .Container;

        // Create a logger
        var logger = childContainer.Resolve<ILogger>();

        // Log the message
        logger.Log("Hello");
    }

    // Check the console output
    console.Verify(i => i.WriteLine(It.IsRegex(".+ - info: Hello")));
}

// Represents the dependency attribute to specify `type` for injection.
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
public class TypeAttribute : Attribute
{
    // The tag, which will be used during an injection
    [NotNull] public readonly Type Type;

    public TypeAttribute([NotNull] Type type) => Type = type;
}

// Represents the dependency attribute to specify `tag` for injection.
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
public class TagAttribute : Attribute
{
    // The tag, which will be used during an injection
    [NotNull] public readonly object Tag;

    public TagAttribute([NotNull] object tag) => Tag = tag;
}

// Represents the dependency attribute to specify `order` for injection.
[AttributeUsage(AttributeTargets.Method)]
public class OrderAttribute : Attribute
{
    // The order to be used to invoke a method
    public readonly int Order;

    public OrderAttribute(int order) => Order = order;
}

public interface IConsole { void WriteLine(string text); }

public interface IClock { DateTimeOffset Now { get; } }

public interface ILogger { void Log(string message); }

public class Logger : ILogger
{
    private readonly IConsole _console;
    private IClock _clock;

    // Constructor injection
    public Logger([Tag("MyConsole")] IConsole console) => _console = console;

    // Method injection
    [Order(1)]
    public void Initialize([Type(typeof(Clock))] IClock clock) => _clock = clock;

    // Property injection
    public string Prefix { get; [Tag("Prefix"), Order(2)] set; }

    // Adds current time and prefix before a message and writes it to console
    public void Log(string message) => _console?.WriteLine($"{_clock.Now} - {Prefix}: {message}");
}

public class Clock : IClock
{
    // "clockName" dependency is not resolved here but has default value
    public Clock([Type(typeof(string)), Tag("ClockName")] string clockName = "SPb") { }

    public DateTimeOffset Now => DateTimeOffset.Now;
}
```

Also you can specify your own aspect oriented autowiring by implementing the interface [_IAutowiringStrategy_](IoCContainer/blob/master/IoC/IAutowiringStrategy.cs).

### Custom Builder [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomBuilder.cs)

The sample below shows how to use this extension point [_IBuilder_](IoCContainer/blob/master/IoC/IBuilder.cs) to rewrite the expression tree of creation any instances to check constructor arguments on null. It is possible to create other own builders to make any manipulation on expression tree before they will be compiled into factories for the instances creation. Any logic any automation - checking arguments, logging, thread safety, authorization aspects and etc.

``` CSharp
public void Run()
{
    // Create and configure the container
    using var container = Container
        .Create()
        .Bind<IDependency>().To<Dependency>()
        .Bind<IService>().To<Service>(ctx => new Service(ctx.Container.Resolve<IDependency>(), ctx.Args[0] as string))
        // Register the custom builder
        .Bind<IBuilder>().To<NotNullGuardBuilder>()
        .Container;

    // Resolve an instance passing null to the "state" parameter
    Assert.Throws<ArgumentNullException>(() => container.Resolve<IService>(null as string));
}

// This custom builder adds the logic to check parameters of reference types injected via constructors on null
private class NotNullGuardBuilder : IBuilder
{
    public Expression Build(IBuildContext context, Expression expression) =>
        expression is NewExpression newExpression && newExpression.Arguments.Count != 0
            ? newExpression.Update(CheckedArgs(newExpression))
            : expression;

    private static IEnumerable<Expression> CheckedArgs(NewExpression newExpression) =>
        from arg in newExpression.Constructor.GetParameters().Select((info, index) => (info, expression: newExpression.Arguments[index]))
        let typeDescriptor = arg.info.ParameterType.Descriptor()
        select !typeDescriptor.IsValueType()
            // arg ?? throw new ArgumentNullException(nameof(arg), "The argument ...")
            ? Expression.Coalesce(
                arg.expression,
                // throws an exception when an argument is null
                Expression.Block(
                    Expression.Throw(Expression.Constant(new ArgumentNullException(arg.info.Name, $"The argument \"{arg.info.Name}\" is null while constructing the instance of type \"{newExpression.Type.Name}\"."))),
                    Expression.Default(typeDescriptor.Type)))
            : arg.expression;
}
```



### Custom Child Container [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomChildContainer.cs)

You may replace the default implementation of container by your own. I can’t imagine why, but it’s possible!

``` CSharp
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

    // Check the child container's type
    childContainer.ShouldBeOfType<MyContainer>();
}

// Sample of transparent container implementation
public class MyContainer: IMutableContainer
{
    // some implementation here
}
```



### Custom Lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomLifetime.cs)

Custom lifetimes allow to implement your own logic controlling every aspects of resolved instances. Also you could use the class [_KeyBasedLifetime<>_](IoC/Lifetimes/KeyBasedLifetime.cs) as a base for others.

``` CSharp
public void Run()
{
    // Create and configure the container
    using var container = Container
        .Create()
        .Bind<IDependency>().To<Dependency>()
        // Bind an interface to an implementation using the custom lifetime, based on the Singleton lifetime
        .Bind<IService>().Lifetime(new MyTransientLifetime()).To<Service>()
        .Container;
    
    // Resolve the singleton twice
    var instance1 = container.Resolve<IService>();
    var instance2 = container.Resolve<IService>();

    // Check that instances from the parent container are equal
    instance1.ShouldBe(instance2);
}

// Represents the custom lifetime based on the Singleton lifetime
public class MyTransientLifetime : ILifetime
{
    // Creates the instance of the Singleton lifetime
    private ILifetime _baseLifetime = new Lifetimes.SingletonLifetime();

    // Wraps the expression by the Singleton lifetime expression
    public Expression Build(IBuildContext context, Expression expression)
        => context.AddLifetime(expression, _baseLifetime);

    // Creates the similar lifetime to use with generic instances
    public ILifetime Create() => new MyTransientLifetime();

    // Select a container to resolve dependencies using the Singleton lifetime logic
    public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
        _baseLifetime.SelectResolvingContainer(registrationContainer, resolvingContainer);

    // Disposes the instance of the Singleton lifetime
    public void Dispose() => _baseLifetime.Dispose();
}
```



### Custom Tasks [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomTasks.cs)



``` CSharp
public async void Run()
{
    // Create the container and configure it
    using var container = Container.Create()
        .Using<CustomTasksFeature>()
        // Bind some dependency
        .Bind<IDependency>().To<SomeDependency>()
        .Bind<Consumer>().To<Consumer>()
        .Container;

    // Resolve an instance asynchronously
    var instance = await container.Resolve<Task<Consumer>>();

    // Check the instance's type
    instance.ShouldBeOfType<Consumer>();
}

internal class CustomTasksFeature: IConfiguration
{
    public IEnumerable<IToken> Apply(IMutableContainer container)
    {
        yield return container
            // Bind cancellation token source
            .Bind<CancellationTokenSource>().To<CancellationTokenSource>()
            // Bind the class responsible for tasks creation
            .Bind<TaskFactory<TT>>().As(Singleton).To<TaskFactory<TT>>()
            // Bind the task factory for any tags
            .Bind<Task<TT>>().Tag(Key.AnyTag).To(ctx => ctx.Container.Inject<TaskFactory<TT>>(ctx.Key.Tag).Create());
    }

    internal class TaskFactory<T>
    {
        private readonly Func<T> _factory;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public TaskFactory(Func<T> factory, CancellationTokenSource cancellationTokenSource)
        {
            _factory = factory;
            _cancellationTokenSource = cancellationTokenSource;
        }

        public Task<T> Create()
        {
            var task = new Task<T>(_factory, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
            task.Start(TaskScheduler.Default);
            return task;
        }
    }
}

public class SomeDependency: IDependency
{
    // Time-consuming logic constructor
    public SomeDependency() { }
}

public class Consumer
{
    public Consumer(Task<IDependency> dependency1, Task<IDependency> dependency2)
    {
        // Time-consuming logic
        var dep1 = dependency1.Result;
        var dep2 = dependency2.Result;
    }
}
```



### Interception [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Interception.cs)

The _Interception_ feature allows specify the set of bindings which will be used to produce instances wrapped by proxy objects. These proxy objects intercept any invocations to the created (or injected) instances and allows to add any logic around it: checking arguments, logging, thread safety, authorization aspects and etc.

``` CSharp
// To use this feature just add the NuGet package https://www.nuget.org/packages/IoC.Interception
// or https://www.nuget.org/packages/IoC.Interception.Source
public void Run()
{
    var methods = new List<string>();
    // Create and configure the container
    using var container = Container
        // Creates an Inversion of Control container
        .Create()
        // Using the feature InterceptionFeature
        .Using<InterceptionFeature>()
        // Configures binds
        .Bind<IDependency>().To<Dependency>()
        .Bind<IService>().To<Service>()
        // Configures interception for IService calls
        .Intercept<IService>(new MyInterceptor(methods))
        .Container;

    // Resolve an instance
    var instance = container.Resolve<IService>();

    // Invoke the getter "get_State"
    var state = instance.State;

    // Check invocations from the interceptor
    methods.ShouldContain("get_State");
}

// This interceptor just stores the name of called methods
public class MyInterceptor : IInterceptor
{
    private readonly ICollection<string> _methods;

    // Stores the collection of called method names
    public MyInterceptor(ICollection<string> methods) => _methods = methods;

    // Intercepts the invocations and appends the called method name to the collection
    public void Intercept(IInvocation invocation) => _methods.Add(invocation.Method.Name);
}
```



### Replace Lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ReplaceLifetime.cs)

Is it possible to replace default lifetimes by your own. The sample below shows how to count the number of attempts to resolve [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instances.

``` CSharp
public void Run()
{
    var counter = new Mock<ICounter>();

    // Create and configure the container
    using var container = Container
        .Create()
        .Bind<ICounter>().To(ctx => counter.Object)
        // Replace the Singleton lifetime
        .Bind<ILifetime>().Tag(Lifetime.Singleton).To<MySingletonLifetime>(
            // Select the constructor
            ctx => new MySingletonLifetime(
                // Inject the singleton lifetime from the parent container to use delegate logic
                ctx.Container.Parent.Inject<ILifetime>(Lifetime.Singleton),
                // Inject the counter to store the number of created instances
                ctx.Container.Inject<ICounter>()))
        // Configure the container as usual
        .Bind<IDependency>().To<Dependency>()
        // Bind using the custom implementation of Singleton lifetime
        .Bind<IService>().As(Lifetime.Singleton).To<Service>()
        .Container;

    // Resolve the singleton twice using the custom lifetime
    var instance1 = container.Resolve<IService>();
    var instance2 = container.Resolve<IService>();

    // Check that instances are equal
    instance1.ShouldBe(instance2);

    // Check the number of created instances
    counter.Verify(i => i.Increment(), Times.Exactly(2));
}

// Represents the instance counter
public interface ICounter
{
    void Increment();
}

public class MySingletonLifetime : ILifetime
{
    // Stores 'IncrementCounter' method info to the static field
    private static readonly MethodInfo IncrementCounterMethodInfo = typeof(MySingletonLifetime).GetTypeInfo().DeclaredMethods.Single(i => i.Name == nameof(IncrementCounter));

    private readonly ILifetime _baseSingletonLifetime;
    private readonly ICounter _counter;

    // Stores the base lifetime and the instance counter
    public MySingletonLifetime(ILifetime baseSingletonLifetime, ICounter counter)
    {
        _baseSingletonLifetime = baseSingletonLifetime;
        _counter = counter;
    }

    public Expression Build(IBuildContext context, Expression expression)
    {
        // Builds expression using base lifetime
        expression = _baseSingletonLifetime.Build(context, expression);

        // Defines `this` variable to store the reference to the current lifetime instance to call internal method 'IncrementCounter'
        var thisVar = Expression.Constant(this);

        // Creates the code block
        return Expression.Block(
            // Adds the expression to call the method 'IncrementCounter' for the current lifetime instance
            Expression.Call(thisVar, IncrementCounterMethodInfo),
            // Returns the expression to create an instance
            expression);
    }

    // Creates the similar lifetime to use with generic instances
    public ILifetime Create() => new MySingletonLifetime(_baseSingletonLifetime.Create(), _counter);

    // Select a container to resolve dependencies using the Singleton lifetime logic
    public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
        _baseSingletonLifetime.SelectResolvingContainer(registrationContainer, resolvingContainer);

    // Disposes the instance of the Singleton lifetime
    public void Dispose() => _baseSingletonLifetime.Dispose();

    // Just counts the number of calls
    internal void IncrementCounter() => _counter.Increment();
}
```



### Composition Root [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CompositionRoot.cs)



``` CSharp
public void Run()
{
    // Host runs a program
    Program.TestMain();
}

class Program
{
    // The application's entry point
    public static void TestMain()
    {
        // The Composition Root is an application infrastructure component
        // It should be as close as possible to the application's entry point
        using var compositionRoot = Container
            // Creates the IoC container: a IoC Container should only be referenced to build a Composition Root
            .Create()
            // Configures the container
            .Using<Configuration>()
            // Creates the composition root: single location for object construction
            .BuildUp<Program>();

        // Runs a logic
        compositionRoot.Instance.Run();
    }

    // Injects dependencies via a constructor
    internal Program(IService service)
    {
         // Saves dependencies as internal fields
    }

    private void Run()
    {
        // Implements a logic using dependencies
    }
}

// Represents the IoC container configuration
class Configuration: IConfiguration
{
    public IEnumerable<IToken> Apply(IMutableContainer container)
    {
        yield return container
            .Bind<IDependency>().To<Dependency>()
            .Bind<IService>().To<Service>();
    }
}
```



### Resolve Func [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveFunc.cs)

_Func_ dependency helps when a logic needs to inject some number of type instances on demand.

``` CSharp
// Create and configure the container
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>()
    .Container;

// Resolve function to create instances
var factory = container.Resolve<Func<IService>>();

// Resolve instances
var instance1 = factory();
var instance2 = factory();
```



### Resolve Lazy [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveLazy.cs)

_Lazy_ dependency helps when a logic needs to inject some _lazy proxy_ to get instance once on demand.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>()
    .Container;

// Resolve the instance of Lazy<IService>
var lazy = container.Resolve<Lazy<IService>>();

// Get the instance via Lazy
var instance = lazy.Value;
```



### Resolve ThreadLocal [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveThreadLocal.cs)



``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>()
    .Container;

// Resolve the instance of Lazy<IService>
var lazy = container.Resolve<ThreadLocal<IService>>();

// Get the instance via ThreadLocal
var instance = lazy.Value;
```



### Resolve Tuple [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveTuple.cs)

[Tuple](https://docs.microsoft.com/en-us/dotnet/api/system.tuple) has a specific number and sequence of elements which may be resolved from the box.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>()
    .Bind<INamedService>().To<NamedService>(ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name"))
    .Container;

// Resolve an instance of type Tuple<IService, INamedService>
var tuple = container.Resolve<Tuple<IService, INamedService>>();
```



### Resolve ValueTuple [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveValueTuple.cs)



``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>()
    .Bind<INamedService>().To<NamedService>(ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name"))
    .Container;
{
    // Resolve an instance of type (IService service, INamedService namedService)
    var valueTuple = container.Resolve<(IService service, INamedService namedService)>();
}

```



### Method Injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/MethodInjection.cs)

:warning: Please try using the constructor injection instead. The method injection is not recommended because of it is a cause of hidden dependencies.

``` CSharp
// Create and configure the container using full autowiring
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
    .Bind<INamedService>().To<InitializingNamedService>(
        // Select the constructor and inject the dependency
        ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
        // Configure the initializing method to invoke after the instance creation and inject the dependencies
        // First one is the value from arguments at index 0
        // Second one as is just dependency injection of type IDependency
        ctx => ctx.It.Initialize((string) ctx.Args[0], ctx.Container.Inject<IDependency>()))
    .Container;

// Resolve the instance using the argument "alpha"
var instance = container.Resolve<INamedService>("alpha");

// Check the instance's type
instance.ShouldBeOfType<InitializingNamedService>();

// Check the injected dependency
instance.Name.ShouldBe("alpha");

// Resolve a function to create an instance
var func = container.Resolve<Func<string, INamedService>>();

// Create an instance with the argument "beta"
var otherInstance = func("beta");

// Check the injected dependency
otherInstance.Name.ShouldBe("beta");

```



### Nullable Value Type Resolving [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/NullableValueTypeResolving.cs)



``` CSharp
// Create the container and configure it
using var container = Container.Create()
    .Bind<int>().Tag(1).To(ctx => 1)
    .Container;

// Resolve an instance
var val1 = container.Resolve<int?>(1.AsTag());
var val2 = container.Resolve<int?>(2.AsTag());
var val3 = container.Resolve<int?>();

// Check the optional dependency
val1.Value.ShouldBe(1);
val2.HasValue.ShouldBeFalse();
val3.HasValue.ShouldBeFalse();
```



### Property Injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/PropertyInjection.cs)

:warning: Please try using the constructor injection instead. The property injection is not recommended because of it is a cause of hidden dependencies.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
    .Bind<INamedService>().To<InitializingNamedService>(
        // Select the constructor and inject the dependency
        ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
        // Select the property to inject after the instance creation and inject the value from arguments at index 0
        ctx => ctx.Container.Inject(ctx.It.Name, (string) ctx.Args[0]))
    .Container;

// Resolve the instance using the argument "alpha"
var instance = container.Resolve<INamedService>("alpha");

// Check the instance's type
instance.ShouldBeOfType<InitializingNamedService>();

// Check the injected dependency
instance.Name.ShouldBe("alpha");

// Resolve a function to create an instance
var func = container.Resolve<Func<string, INamedService>>();

// Create an instance with the argument "beta"
var otherInstance = func("beta");

// Check the injected dependency
otherInstance.Name.ShouldBe("beta");

```



### Constructor Autowiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConstructorAutowiring.cs)



``` CSharp
// Create and configure the container, using full autowiring
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Configure via manual injection
    .Bind<IService>().To<Service>(
        // Select the constructor and inject arguments
        ctx => new Service(ctx.Container.Inject<IDependency>(), "some state"))
    .Container;
// Resolve an instance
var instance = container.Resolve<IService>();

// Check the injected constant
instance.State.ShouldBe("some state");
```



### Containers Injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ContainersInjection.cs)

:warning: Please avoid injecting containers in non-infrastructure code. Keep your general code in ignorance of a container.

``` CSharp
public void Run()
{
    // Create the parent container
    using var currentContainer = Container
        .Create("root")
        .Bind<MyClass>().To<MyClass>()
        .Container;

    var instance = currentContainer.Resolve<MyClass>();
    instance.CurrentContainer.ShouldBe(currentContainer);
    instance.ChildContainer1.Parent.ShouldBe(currentContainer);
    instance.ChildContainer2.Parent.ShouldBe(currentContainer);
    instance.NamedChildContainer.Parent.ShouldBe(currentContainer);
    instance.NamedChildContainer.ToString().ShouldBe("//root/Some name");
}

public class MyClass
{
    public MyClass(
        IContainer currentContainer,
        IMutableContainer newChildContainerFactory,
        Func<IMutableContainer> childContainerFactory,
        Func<string, IMutableContainer> nameChildContainerFactory)
    {
        CurrentContainer = currentContainer;
        ChildContainer1 = newChildContainerFactory;
        ChildContainer2 = childContainerFactory();
        NamedChildContainer = nameChildContainerFactory("Some name");
    }

    public IContainer CurrentContainer { get; }

    public IContainer ChildContainer1 { get; }

    public IContainer ChildContainer2 { get; }

    public IContainer NamedChildContainer { get; }
}
```



### Resolve instances as Array [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Array.cs)



``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind to the implementation #1
    .Bind<IService>().Tag(1).To<Service>()
    // Bind to the implementation #2
    .Bind<IService>().Tag(2).Tag("abc").To<Service>()
    // Bind to the implementation #3
    .Bind<IService>().Tag(3).To<Service>()
    .Container;

// Resolve all appropriate instances
var instances = container.Resolve<IService[]>();
```



### Resolve instances as ICollection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Collection.cs)



``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind to the implementation #1
    .Bind<IService>().Tag(1).To<Service>()
    // Bind to the implementation #2
    .Bind<IService>().Tag(2).Tag("abc").To<Service>()
    // Bind to the implementation #3
    .Bind<IService>().Tag(3).To<Service>()
    .Container;

// Resolve all appropriate instances
var instances = container.Resolve<ICollection<IService>>();

// Check the number of resolved instances
instances.Count.ShouldBe(3);
```



### Resolve instances as IEnumerable [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Enumerables.cs)



``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind to the implementation #1
    .Bind<IService>().Tag(1).To<Service>()
    // Bind to the implementation #2
    .Bind<IService>().Tag(2).Tag("abc").To<Service>()
    // Bind to the implementation #3
    .Bind<IService>().Tag(3).To<Service>()
    .Container;

// Resolve all appropriate instances
var instances = container.Resolve<IEnumerable<IService>>().ToList();

// Check the number of resolved instances
instances.Count.ShouldBe(3);
```



### Resolve instances as IObservable source [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Observable.cs)



``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind to the implementation #1
    .Bind<IService>().Tag(1).To<Service>()
    // Bind to the implementation #2
    .Bind<IService>().Tag(2).Tag("abc").To<Service>()
    // Bind to the implementation #3
    .Bind<IService>().Tag(3).To<Service>()
    .Container;

// Resolve the source for all appropriate instances
var instancesSource = container.Resolve<IObservable<IService>>();
```



### Resolve Using Arguments [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveWithArgs.cs)



``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
    .Bind<INamedService>().To<NamedService>(
        // Select the constructor and inject and inject the value from arguments at index 0
        ctx => new NamedService(ctx.Container.Inject<IDependency>(), (string) ctx.Args[0]))
    .Container;

// Resolve the instance using the argument "alpha"
var instance = container.Resolve<INamedService>("alpha");

// Check the instance's type
instance.ShouldBeOfType<NamedService>();

// Check the injected dependency
instance.Name.ShouldBe("alpha");

// Resolve a function to create an instance
var func = container.Resolve<Func<string, INamedService>>();

// Create an instance with the argument "beta"
var otherInstance = func("beta");

// Check the injected dependency
otherInstance.Name.ShouldBe("beta");

```



### Resolve all appropriate instances as ISet [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Set.cs)



``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind to the implementation #1
    .Bind<IService>().Tag(1).To<Service>()
    // Bind to the implementation #2
    .Bind<IService>().Tag(2).Tag("abc").To<Service>()
    // Bind to the implementation #3
    .Bind<IService>().Tag(3).To<Service>()
    .Container;

// Resolve all appropriate instances
var instances = container.Resolve<ISet<IService>>();

// Check the number of resolved instances
instances.Count.ShouldBe(3);
```



### Cyclic Dependency [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CyclicDependency.cs)



``` CSharp
public void Run()
{
    var expectedException = new InvalidOperationException("error");
    var foundCyclicDependency = new Mock<IFoundCyclicDependency>();
    // Throws the exception for reentrancy 128
    foundCyclicDependency.Setup(i => i.Resolve(It.Is<IBuildContext>(ctx => ctx.Depth == 128))).Throws(expectedException);

    // Create the container
    using var container = Container
        .Create()
        .Bind<IFoundCyclicDependency>().To(ctx => foundCyclicDependency.Object)
        // Configure the container, where 1,2,3 are tags to produce cyclic dependencies during a resolving
        .Bind<ILink>().To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1)))
        .Bind<ILink>().Tag(1).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(2)))
        .Bind<ILink>().Tag(2).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(3)))
        .Bind<ILink>().Tag(3).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1)))
        .Container;

    try
    {
        // Resolve the root instance
        container.Resolve<ILink>();
    }
    // Catch the exception about cyclic dependencies at a depth of 128
    catch (InvalidOperationException actualException)
    {
        // Check the exception
        actualException.ShouldBe(expectedException);
    }
}

public interface ILink
{
}

public class Link : ILink
{
    public Link(ILink link) { }
}
```



### Generator [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Generator.cs)



``` CSharp
public void Run()
{
    // Create and configure the container using a configuration class 'Generators'
    using var container = Container.Create().Using<Generators>();
    using (container.Bind<(int, int)>().To(
        // Use a function because of the expression trees have a limitation in syntax
        ctx => ValueTuple.Create(
            // The first one is of sequential number generator
            ctx.Container.Inject<int>(GeneratorType.Sequential),
            // The second one is of random number generator
            ctx.Container.Inject<int>(GeneratorType.Random))))
    {
        // Generate sequential numbers
        var sequential1 = container.Resolve<int>(GeneratorType.Sequential.AsTag());
        var sequential2 = container.Resolve<int>(GeneratorType.Sequential.AsTag());

        // Check numbers
        sequential2.ShouldBe(sequential1 + 1);

        // Generate a random number
        var random = container.Resolve<int>(GeneratorType.Random.AsTag());

        // Generate a tuple of numbers
        var setOfValues = container.Resolve<(int, int)>();

        // Check sequential numbers
        setOfValues.Item1.ShouldBe(sequential2 + 1);
    }
}

// Represents tags for generators
public enum GeneratorType
{
    Sequential, Random
}

// Represents IoC configuration
public class Generators: IConfiguration
{
    public IEnumerable<IToken> Apply(IMutableContainer container)
    {
        var value = 0;
        // Define function to get sequential integer value
        Func<int> generator = () => Interlocked.Increment(ref value);
        // Bind to this function using the corresponding tag 'Sequential'
        yield return container.Bind<int>().Tag(GeneratorType.Sequential).To(ctx => generator());

        var random = new Random();
        // Define function to get random integer value
        Func<int> randomizer = () => random.Next();
        // Bind to this function using the corresponding tag 'Random'
        yield return container.Bind<int>().Tag(GeneratorType.Random).To(ctx => randomizer());
    }
}
```



### Instant Messenger [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SimpleInstantMessenger.cs)



``` CSharp
public void Run()
{
    var observer = new Mock<IObserver<IMessage>>();

    // Create a container
    using var container = Container.Create().Using<InstantMessengerConfig>();

    // Resolve messenger
    var instantMessenger = container.Resolve<IInstantMessenger<IMessage>>();
    using var subscription = instantMessenger.Subscribe(observer.Object);

    // Send messages
    instantMessenger.SendMessage("Nik", "John", "Hello, John");
    instantMessenger.SendMessage("John", "Nik", "Hello, Nik!");

    // Verify messages
    observer.Verify(i => i.OnNext(It.Is<IMessage>(message => message.Id == 34 && message.Text == "Hello, John")));
    observer.Verify(i => i.OnNext(It.Is<IMessage>(message => message.Id == 35 && message.Text == "Hello, Nik!")));
}

public class InstantMessengerConfig: IConfiguration
{
    public IEnumerable<IToken> Apply(IMutableContainer container)
    {
        // Let's suppose that the initial message ID is 33
        var id = 33;

        yield return container
            // id generator
            .Bind<int>().To(ctx => Interlocked.Increment(ref id))
            // abstract messenger
            .Bind(typeof(IInstantMessenger<>)).To(typeof(InstantMessenger<>))
            // abstract subject
            .Bind<ISubject<TT>>().To<Subject<TT>>()
            // message factory
            .Bind<IMessageFactory<IMessage>>().To<Message>();
    }
}

public interface IInstantMessenger<out T>: IObservable<T>
{
    void SendMessage(string addressFrom, string addressTo, string text);
}

public interface IMessage
{
    int Id { get; }

    string AddressFrom { get; }

    string AddressTo { get; }

    string Text { get; }
}

public interface IMessageFactory<out T>
{
    T Create([NotNull] string addressFrom, [NotNull] string addressTo, [NotNull] string text);
}

public class Message: IMessage, IMessageFactory<IMessage>
{
    private readonly Func<int> _idFactory;

    // Injected constructor
    public Message(Func<int> idFactory) => _idFactory = idFactory;

    private Message(int id, [NotNull] string addressFrom, [NotNull] string addressTo, [NotNull] string text)
    {
        Id = id;
        AddressFrom = addressFrom ?? throw new ArgumentNullException(nameof(addressFrom));
        AddressTo = addressTo ?? throw new ArgumentNullException(nameof(addressTo));
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }

    public int Id { get; }

    public string AddressFrom { get; }

    public string AddressTo { get; }

    public string Text { get; }

    public IMessage Create(string addressFrom, string addressTo, string text) => new Message(_idFactory(), addressFrom, addressTo, text);
}

public class InstantMessenger<T> : IInstantMessenger<T>
{
    private readonly IMessageFactory<T> _messageFactory;
    private readonly ISubject<T> _messages;

    internal InstantMessenger(IMessageFactory<T> messageFactory, ISubject<T> subject)
    {
        _messageFactory = messageFactory;
        _messages = subject;
    }

    public IDisposable Subscribe(IObserver<T> observer) => _messages.Subscribe(observer);

    public void SendMessage(string addressFrom, string addressTo, string text) => _messages.OnNext(_messageFactory.Create(addressFrom, addressTo, text));
}
```



### Wrapper [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Wrapper.cs)



``` CSharp
public void Run()
{
    var console = new Mock<IConsole>();
    var clock = new Mock<IClock>();
    var now = new DateTimeOffset(2019, 9, 9, 12, 31, 34, TimeSpan.FromHours(3));
    clock.SetupGet(i => i.Now).Returns(now);

    // Create and configure the root container
    using var rootContainer = Container
        .Create("root")
        .Bind<IConsole>().To(ctx => console.Object)
        .Bind<ILogger>().To<Logger>()
        .Container;

    // Create and configure the child container
    using var childContainer = rootContainer
        .Create("Some child container")
        .Bind<IConsole>().To(ctx => console.Object)
        .Bind<IClock>().To(ctx => clock.Object)
        // Bind 'ILogger' to the instance creation, actually represented as an expression tree
        .Bind<ILogger>().To<TimeLogger>(
            // Inject the base logger from the parent container and the clock from the current container
            ctx => new TimeLogger(ctx.Container.Parent.Inject<ILogger>(), ctx.Container.Inject<IClock>()))
        .Container;

    // Create a logger
    var logger = childContainer.Resolve<ILogger>();

    // Log the message
    logger.Log("Hello");

    // Check the console output
    console.Verify(i => i.WriteLine($"{now}: Hello"));
}

public interface IConsole
{
    // Writes a text
    void WriteLine(string text);
}

public interface ILogger
{
    // Logs a message
    void Log(string message);
}

public interface IClock
{
    DateTimeOffset Now { get; }
}

public class Logger : ILogger
{
    private readonly IConsole _console;

    // Stores console to field
    public Logger(IConsole console) => _console = console;

    // Logs a message to console
    public void Log(string message) => _console.WriteLine(message);
}

public class TimeLogger: ILogger
{
    private readonly ILogger _baseLogger;
    private readonly IClock _clock;

    public TimeLogger(ILogger baseLogger, IClock clock)
    {
        _baseLogger = baseLogger;
        _clock = clock;
    }

    // Adds current time before a message and writes it to console
    public void Log(string message) => _baseLogger.Log($"{_clock.Now}: {message}");
}
```



