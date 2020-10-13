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

  // Decoherence of the superposition at the time of observation via an irreversible process
  public State State => _superposition.Value;

  public override string ToString() => $"{State} cat";
}
```

_It is important to note that our abstraction and our implementation do not know anything about any IoC containers at all._

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

Declare the required dependencies in a dedicated class *__Glue__*. It is possible do this anywhere in your code, but putting this information in one place is often the better solution and helps keep your code more organized.

Below is the concept of mutable containers (_IMutableContainer_). Any binding is not irreversible, thus the owner of a binding [can cancel this binding](#change-configuration-on-the-fly-) using the related binding token (_IToken_).

```csharp
public class Glue : IConfiguration
{
  public IEnumerable<IToken> Apply(IMutableContainer container)
  {
    // Returns single token for 2 bindings
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

_Defining generic type arguments using special marker types like [*__TT__*](#generic-autowiring-) in the sample above is one of the distinguishing features of this library. So there is an easy way to bind complex generic types with nested generic types and with any type constraints._

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

There is a place where we create our object graphs and it is better to concentrate this creation into a single area of your application. This place is called the [*__Composition Root__*]((https://blog.ploeh.dk/2011/07/28/CompositionRoot/)).

#### Few aspects of the Composition Root

The Composition Root is the single place in your application where the composition of the object graphs for your application take place, using the IoC container, but we can delay the creation of some instances or create a set of instances by injecting instance factories like *__Func&lt;T&gt;__* instead of the instances themselves.

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

It allows you to take full advantage of dependency injection everywhere and every time without any compromises in the same way as just a *__new__* keyword to create any instances.

## NuGet packages

|     | binary packages | source code packages ¹ |
| --- | --- | ---|
| Container | [![NuGet](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container) | [![NuGet](https://buildstats.info/nuget/IoC.Container.Source)](https://www.nuget.org/packages/IoC.Container.Source) |
| ASP.NET | [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore)](https://www.nuget.org/packages/IoC.AspNetCore) | [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore.Source)](https://www.nuget.org/packages/IoC.AspNetCore.Source) |
| Interception | [![NuGet](https://buildstats.info/nuget/IoC.Interception)](https://www.nuget.org/packages/IoC.Interception) | [![NuGet](https://buildstats.info/nuget/IoC.Interception.Source)](https://www.nuget.org/packages/IoC.Interception.Source) |

¹ _source code packages_ require C# 7.0 or higher

## ASP.NET Core

- Package Manager

  ```
  Install-Package IoC.AspNetCore
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.AspNetCore
  ```

For __ASP.NET Core 3+__ create the _IoC container_ and use the service provider factory based on this container at [Main](Samples/WebApplication3/Program.cs)

```csharp
public static void Main(string[] args)
{
  using var container = Container
    // Creates an Inversion of Control container
    .Create()
    .Using<ClockConfiguration>();

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

For details please see [this sample](Samples/WebApplication3).

For __ASP.NET Core 2__ create the _IoC container_ with feature _AspNetCoreFeature_ and configure it at [Startup](Samples/WebApplication2/Startup.cs)

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

For details please see [this sample](Samples/WebApplication2).

## Interception

- Package Manager

  ```
  Install-Package IoC.Interception
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.Interception
  ```

Add _InterceptionFeature_ to intercept calls to _IService_ by your own _MyInterceptor_

```csharp
using var container = Container
  // Using the feature InterceptionFeature
  .Using<InterceptionFeature>()
  .Bind<IService>().To<Service>()
  // Intercepts any invocations to any instances resolved via IoC container
  .Intercept(key => true, new MyInterceptor())

container.Resolve<IService>();

```

where _MyInterceptor_ looks like:

```csharp
class MyInterceptor : IInterceptor
{
  // Intercepts invocations and appends some logic around
  public void Intercept(IInvocation invocation)
  {
    ...
    invocation.Proceed();
    ...
  }
}
```

For details please see [this sample](IoC.Tests/UsageScenarios/Interception.cs).

## Why this one?

### Graph of 27 transient instances

![Transient](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/IoC.Benchmark.Transient-report.jpg)

### Graph of 20 transient instances and 1 singleton instance

![Singleton](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/IoC.Benchmark.Singleton-report.jpg)

### Graph of 364 transient instances of unique type

![Complex](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/IoC.Benchmark.Complex-report.jpg)

### Graph of 22 transient instances, including 3 Func to create 4 instances each time

![Func](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/IoC.Benchmark.Func-report.jpg)

### Graph of 22 transient instances, including 3 arrays of 4 instances in each

![Array](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/IoC.Benchmark.Array-report.jpg)

### Graph of 22 transient instances, including 3 enumerable of 4 instances in each

![Enum](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/IoC.Benchmark.Enum-report.jpg)

- __new__ - _Method_ when the graph of objects was constructed by operators _new_ only
- __Mean__ - arithmetic mean of the root instances resolved per nanosecond
- __Error__ - half of 99.9% confidence interval
- __StdDev__ - standard deviation of all measurements
- __Median__ - value separating the higher half of all measurements (50th percentile)
- __1 ns__ - 1 Nanosecond (0.000000001 sec)

_[BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) was used to measure and analyze these results._

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

- Basic
  - [Composition Root](#composition-root-)
  - [Autowiring](#autowiring-)
  - [Bindings](#bindings-)
  - [Constant dependency](#constant-dependency-)
  - [Generics](#generics-)
  - [Wrapper](#wrapper-)
  - [Tags](#tags-)
  - [Several contracts](#several-contracts-)
  - [Autowiring with initialization](#autowiring-with-initialization-)
  - [Child container](#child-container-)
  - [Container Singleton lifetime](#container-singleton-lifetime-)
  - [Expression binding](#expression-binding-)
  - [Method injection](#method-injection-)
  - [Nullable value type](#nullable-value-type-)
  - [Property injection](#property-injection-)
  - [Scope Singleton lifetime](#scope-singleton-lifetime-)
  - [Singleton lifetime](#singleton-lifetime-)
  - [Dependency tag](#dependency-tag-)
  - [Func](#func-)
  - [Manual wiring](#manual-wiring-)
  - [Array](#array-)
  - [AsyncEnumerable](#asyncenumerable-)
  - [ValueTask](#valuetask-)
  - [Collection](#collection-)
  - [Enumerable](#enumerable-)
  - [Func dependency](#func-dependency-)
  - [Func with arguments](#func-with-arguments-)
  - [Lazy](#lazy-)
  - [Observable](#observable-)
  - [Set](#set-)
  - [Struct](#struct-)
  - [ThreadLocal](#threadlocal-)
  - [Tuple](#tuple-)
  - [ValueTuple](#valuetuple-)
  - [Configuration](#configuration-)
  - [Injection of default parameters](#injection-of-default-parameters-)
  - [Generic autowiring](#generic-autowiring-)
  - [Optional injection](#optional-injection-)
  - [Resolve using arguments](#resolve-using-arguments-)
  - [Auto Disposing](#auto-disposing-)
  - [Aspect Oriented](#aspect-oriented-)
- Advanced
  - [Change configuration on-the-fly](#change-configuration-on-the-fly-)
  - [Resolve unbound implementations](#resolve-unbound-implementations-)
  - [Thread Singleton lifetime](#thread-singleton-lifetime-)
  - [Constructor choice](#constructor-choice-)
  - [Containers injection](#containers-injection-)
  - [Asynchronous construction](#asynchronous-construction-)
  - [Override a task scheduler](#override-a-task-scheduler-)
  - [Tracing](#tracing-)
  - [Check a binding](#check-a-binding-)
  - [Check for possible resolving](#check-for-possible-resolving-)
  - [Cancellation of asynchronous construction](#cancellation-of-asynchronous-construction-)
  - [Custom builder](#custom-builder-)
  - [Custom child container](#custom-child-container-)
  - [Custom lifetime](#custom-lifetime-)
  - [Interception](#interception-)
  - [Custom autowiring strategy](#custom-autowiring-strategy-)
  - [Replace a lifetime](#replace-a-lifetime-)
- Samples
  - [Cyclic dependency](#cyclic-dependency-)
  - [Plugins](#plugins-)
  - [Generator sample](#generator-sample-)
  - [Wrapper sample](#wrapper-sample-)
  - [Instant Messenger sample](#instant-messenger-sample-)

### Change configuration on-the-fly [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ChangeConfigurationOnTheFly.cs)



``` CSharp
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



### Resolve unbound implementations [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveUnregisteredImplementations.cs)

The feature _ResolveUnboundFeature_ allows you to resolve any implementation type from the container regardless of whether or not you specifically bound it.

``` CSharp
public void Run()
{
    using var container = Container
        .Create()
        .Using<ResolveUnboundFeature>()
        .Bind<IDependency>().To<Dependency>()
        .Container;

    // Resolve an instance of unregistered type
    container.Resolve<Service<int>>();
}

class Service<T>
{
    public Service(OtherService<T> otherService, IDependency dependency) { }
}

class OtherService<T>
{
    public OtherService(T value) { }
}
```



### Thread Singleton lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ThreadSingletonLifetime.cs)

Sometimes it is useful to have a [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance per a thread (or more generally a singleton per something else). There is no special "lifetime" type in this framework to achieve this requirement, but it is quite easy create your own "lifetime" type for that using base type [_KeyBasedLifetime<>_](IoC/Lifetimes/KeyBasedLifetime.cs).

``` CSharp
public void Run()
{
    var finish = new ManualResetEvent(false);

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
public class ThreadLifetime : KeyBasedLifetime<int, object>
{
    // Creates a clone of the current lifetime (for the case with generic types)
    public override ILifetime Create() =>
        new ThreadLifetime();

    // Provides a key of an instance
    // If a key the same an instance is the same too
    protected override int CreateKey(IContainer container, object[] args) =>
        Thread.CurrentThread.ManagedThreadId;

    // Just returns created instance
    protected override object OnNewInstanceCreated(object newInstance, int key, IContainer container, object[] args) =>
        newInstance;

    // Do nothing
    protected override void OnInstanceReleased(object releasedInstance, int key) { }
}
```



### Constructor choice [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConstructorChoice.cs)



``` CSharp
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>(
        // Select the constructor and inject required dependencies
        ctx => new Service(ctx.Container.Inject<IDependency>(), "some state"))
    .Container;

var instance = container.Resolve<IService>();

// Check the injected constant
instance.State.ShouldBe("some state");
```



### Containers injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ContainersInjection.cs)

:warning: Please avoid injecting containers in non-infrastructure code. Keep your general code in ignorance of a container.

``` CSharp
public void Run()
{
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
        IMutableContainer newChildContainer,
        Func<IMutableContainer> childContainerFactory,
        Func<string, IMutableContainer> nameChildContainerFactory)
    {
        CurrentContainer = currentContainer;
        ChildContainer1 = newChildContainer;
        ChildContainer2 = childContainerFactory();
        NamedChildContainer = nameChildContainerFactory("Some name");
    }

    public IContainer CurrentContainer { get; }

    public IContainer ChildContainer1 { get; }

    public IContainer ChildContainer2 { get; }

    public IContainer NamedChildContainer { get; }
}
```



### Asynchronous construction [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousConstruction.cs)

It is easy to inject dependencies in asynchronous style.

``` CSharp
public async void Run()
{
    using var container = Container.Create()
        // Bind some dependency
        .Bind<IDependency>().To<SomeDependency>()
        .Bind<Consumer>().To<Consumer>()
        .Container;

    // Resolve an instance asynchronously using the default task scheduler _TaskScheduler.Current_
    var instance = await container.Resolve<Task<Consumer>>();

    // Check the instance
    instance.ShouldBeOfType<Consumer>();
}

public class SomeDependency: IDependency
{
    // Time-consuming logic constructor
    public SomeDependency() { }

    public int Index { get; set; }
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



### Override a task scheduler [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/TaskSchedulerOverride.cs)

_TaskScheduler.Current_ is used by default for an asynchronous construction, but it is easy to override it, binding abstract class _TaskScheduler_ to required implementation in a IoC container.

``` CSharp
using var container = Container.Create()
    // Bind some dependency
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>()
    // Override _TaskScheduler if it is required, TaskScheduler.Current will be used by default
    .Bind<TaskScheduler>().To(ctx => TaskScheduler.Default)
    .Container;

// Resolve an instance asynchronously
var instance = await container.Resolve<Task<IService>>();
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
        .Trace(e => traceMessages.Add(e.Message))
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



### Check a binding [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ValidateBinding.cs)

It is easy to validate that binding is already exists.

``` CSharp
using var container = Container.Create();

var isBound = container.IsBound<IService>();
// _IService_ is not bound yet
isBound.ShouldBeFalse();

container.Bind<IService>().To<Service>();
// _IService_ is already bound
isBound = container.IsBound<IService>();

isBound.ShouldBeTrue();
```



### Check for possible resolving [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ValidateResolving.cs)

It is easy to validate without the actual resolving.

``` CSharp
// Create and configure the container
using var container = Container
    .Create()
    .Bind<IService>().To<Service>()
    .Container;

var canResolve = container.CanResolve<IService>();

// _Service_ has the mandatory dependency _IDependency_ in the constructor,
// which was not registered and that is why _IService_ cannot be resolved
canResolve.ShouldBeFalse();

// Add the required binding for _Service_
container.Bind<IDependency>().To<Dependency>();

// Now it is possible to resolve _IService_
canResolve = container.CanResolve<IService>();

// Everything is ok now
canResolve.ShouldBeTrue();
```



### Cancellation of asynchronous construction [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousConstructionCancellation.cs)

It is possible to inject dependencies in asynchronous style and to cancel their creations using default _CancellationTokenSource_.

``` CSharp
public void Run()
{
    // Create a cancellation token source
    var cancellationTokenSource = new CancellationTokenSource();

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

    public int Index { get; set; }
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



### Custom builder [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomBuilder.cs)

The sample below shows how to use this extension point [_IBuilder_](IoCContainer/blob/master/IoC/IBuilder.cs) to rewrite the expression tree of creation any instances to check constructor arguments on null. It is possible to create other own builders to make any manipulation on expression tree before they will be compiled into factories for the instances creation. Any logic any automation - checking arguments, logging, thread safety, authorization aspects and etc.

``` CSharp
public void Run()
{
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



### Custom child container [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomChildContainer.cs)

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



### Custom lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomLifetime.cs)

Custom lifetimes allow to implement your own logic controlling every aspects of resolved instances. Also you could use the class [_KeyBasedLifetime<>_](IoC/Lifetimes/KeyBasedLifetime.cs) as a base for others.

``` CSharp
public void Run()
{
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
    public Expression Build(IBuildContext context, Expression expression) =>
        _baseLifetime.Build(context, expression);

    // Creates the similar lifetime to use with generic instances
    public ILifetime Create() => new MyTransientLifetime();

    // Select a container to resolve dependencies using the Singleton lifetime logic
    public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
        _baseLifetime.SelectResolvingContainer(registrationContainer, resolvingContainer);

    // Disposes the instance of the Singleton lifetime
    public void Dispose() => _baseLifetime.Dispose();
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
    using var container = Container
        // Creates the Inversion of Control container
        .Create()
        // Using the feature InterceptionFeature
        .Using<InterceptionFeature>()
        // Configures binds
        .Bind<IDependency>().To<Dependency>()
        .Bind<IService>().To<Service>()
        // Intercepts any invocations
        .Intercept(key => true, new MyInterceptor(methods))
        .Container;

    // Resolve an instance
    var instance = container.Resolve<IService>();

    // Invoke the getter "get_State"
    var state = instance.State;
    instance.Dependency.Index = 1;

    // Check invocations from the interceptor
    methods.ShouldContain("get_State");
    methods.ShouldContain("set_Index");
}

// This interceptor just stores the name of called methods
public class MyInterceptor : IInterceptor
{
    private readonly ICollection<string> _methods;

    // Stores the collection of called method names
    public MyInterceptor(ICollection<string> methods) => _methods = methods;

    // Intercepts the invocations and appends the called method name to the collection
    public void Intercept(IInvocation invocation)
    {
        _methods.Add(invocation.Method.Name);
        invocation.Proceed();
    }
}
```



### Custom autowiring strategy [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/OverrideAutowiringStrategy.cs)



``` CSharp
public void Run()
{
    using var container = Container
        .Create()
        .Bind<IDependency>().To<Dependency>()
        // Additionally NamedService requires name in the constructor
        .Bind<INamedService>().To<NamedService>()
        // Overrides the previous autowiring strategy for the current and children containers
        .Bind<IAutowiringStrategy>().To<CustomAutowiringStrategy>()
        .Container;

    var service = container.Resolve<INamedService>();

    service.Name.ShouldBe("default name");
}

class CustomAutowiringStrategy : IAutowiringStrategy
{
    private readonly IAutowiringStrategy _baseStrategy;

    public CustomAutowiringStrategy(IContainer container, IAutowiringStrategy baseStrategy) =>
        // Saves the previous autowiring strategy
        _baseStrategy = baseStrategy;

    public bool TryResolveType(IContainer container, Type registeredType, Type resolvingType, out Type instanceType) =>
        // Just uses a logic from the previous autowiring strategy as is
        _baseStrategy.TryResolveType(container, registeredType, resolvingType, out instanceType);

    // Overrides a logic to inject the constant "default name" to every constructors parameters named "name" of type String
    public bool TryResolveConstructor(IContainer container, IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
    {
        if (!_baseStrategy.TryResolveConstructor(container, constructors, out constructor))
        {
            return false;
        }

        var selectedConstructor = constructor;
        selectedConstructor.Info.GetParameters()
            // Filters constructor parameters
            .Where(p => p.Name == "name" && p.ParameterType == typeof(string)).ToList()
            // Overrides every parameters expression by the constant "default name"
            .ForEach(p => selectedConstructor.SetExpression(p.Position, Expression.Constant("default name")));

        return true;
    }

    public bool TryResolveInitializers(IContainer container, IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        // Just uses a logic from the previous autowiring strategy as is
        => _baseStrategy.TryResolveInitializers(container, methods, out initializers);
}
```



### Replace a lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ReplaceLifetime.cs)

Is it possible to replace default lifetimes by your own. The sample below shows how to count the number of attempts to resolve [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instances.

``` CSharp
public void Run()
{
    var counter = new Mock<ICounter>();

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



### Bindings [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Bindings.cs)

It is possible to bind any number of types.

``` CSharp
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



### Constant dependency [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConstantDependency.cs)

It's obvious here.

``` CSharp
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

Autowriting of generic types via binding of open generic types or generic type markers are working the same way.

``` CSharp
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



### Wrapper [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SimpleWrapper.cs)



``` CSharp
public void Run()
{
    // Create and configure a parent container
    using var parentContainer = Container
        .Create()
        // Binds a service to wrap
        .Bind<IService>().To<Service>()
        .Container;

    // Create and configure a child container
    using var childContainer = parentContainer
        .Create()
        // Binds wrapper, injecting the base IService from the parent container via constructor
        .Bind<IService>().To<WrapperForService>()
        .Container;

    var service = childContainer.Resolve<IService>();

    service.Value.ShouldBe("Wrapper abc");
}

public interface IService
{
    public string Value { get; }
}

public class Service: IService
{
    public string Value => "abc";
}

public class WrapperForService : IService
{
    private readonly IService _wrapping;

    public WrapperForService(IService wrapping) => _wrapping = wrapping;

    public string Value => $"Wrapper {_wrapping.Value}";
}
```



### Tags [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Tags.cs)

Tags are useful while binding to several implementations.

``` CSharp
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



### Several contracts [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SeveralContracts.cs)

It is possible to bind several types to single implementation.

``` CSharp
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<Service, IService, IAnotherService>().To<Service>()
    .Container;

// Resolve instances
var instance1 = container.Resolve<IService>();
var instance2 = container.Resolve<IAnotherService>();
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

// Check the instance
instance.ShouldBeOfType<InitializingNamedService>();

// Check the initialization is ok
instance.Name.ShouldBe("initialized !!!");
```

:warning: It is not recommended because of it is a cause of hidden dependencies.

### Child container [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ChildContainer.cs)

Child containers allow to override or just to add bindings of a parent containers without any influence on a parent containers. This is most useful when there are some base parent container(s). And these containers are shared between several components which have its own child containers based on common parent container(s) and have some additional bindings.

``` CSharp
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



### Expression binding [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ExpressionBinding.cs)

In this case the specific type is bound to an expression tree. This dependency will be introduced as is, without any additional overhead like _lambda call_ or _type cast_.

``` CSharp
using var container = Container
    .Create()
    .Bind<IService>().To(ctx => new Service(new Dependency()))
    .Container;

// Resolve an instance
var instance = container.Resolve<IService>();
```



### Method injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/MethodInjection.cs)

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



### Nullable value type [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/NullableValueType.cs)



``` CSharp
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



### Property injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/PropertyInjection.cs)

:warning: Please try using the constructor injection instead. The property injection is not recommended because of it is a cause of hidden dependencies.

``` CSharp
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
    .Bind<INamedService>().To<InitializingNamedService>(
        // Select the constructor and inject the dependency
        ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
        // Select the property to inject after the instance creation and inject the value from arguments at index 0
        ctx => ctx.Container.Assign(ctx.It.Name, (string) ctx.Args[0]))
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



### Scope Singleton lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ScopeSingletonLifetime.cs)

Each scope has its own [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance for specific binding. Scopes can be created, activated and deactivated. Scope can be injected like any other instance from container.

``` CSharp
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



### Singleton lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SingletonLifetime.cs)

[Singleton](https://en.wikipedia.org/wiki/Singleton_pattern) is a design pattern which stands for having only one instance of some class during the whole application lifetime. The main complaint about Singleton is that it contradicts the Dependency Injection principle and thus hinders testability. It essentially acts as a global constant, and it is hard to substitute it with a test when needed. The _Singleton lifetime_ is indispensable in this case.

``` CSharp
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

### Dependency tag [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/DependencyTag.cs)

Use a _tag_ to inject specific dependency from several bindings of the same types.

``` CSharp
using var container = Container
    .Create()
    .Bind<IDependency>().Tag("MyDep").To<Dependency>()
    // Configure autowiring and inject dependency tagged by "MyDep"
    .Bind<IService>().To<Service>(ctx => new Service(ctx.Container.Inject<IDependency>("MyDep")))
    .Container;

// Resolve an instance
var instance = container.Resolve<IService>();
```



### Func [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Func.cs)

_Func_ dependency helps when a logic needs to inject some number of type instances on demand.

``` CSharp
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



### Manual wiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ManualWiring.cs)

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

// Check the instance
instance.ShouldBeOfType<InitializingNamedService>();

// Check the injected dependency
instance.Name.ShouldBe("some name");
```

It's important to note that injection is possible by several ways in the sample above. **The first one** is an expressions like `ctx.Container.Inject<IDependency>()`. It uses the injection context `ctx` to access to the current (or other parents) container and method `Inject` to inject a dependency. But actually this method has no implementation, it ust a marker and it every such method wil be replaced by expression which creates dependency in place without any additional invocations. **Another one way** is to use an expressions like `ctx.Resolve<IDependency>()`. It will access a container each time to resolve a dependency. That is, each time it will look for the necessary binding in the container and call the method to create an instance of the dependency type. **In summary: wherever possible, use the first approach like `ctx.Container.Inject<IDependency>()`.**

### Array [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Array.cs)

To resolve all possible instances of any tags of the specific type as an _array_ just use the injection _T[]_

``` CSharp
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



### AsyncEnumerable [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsyncEnumerables.cs)

It is easy to resolve an enumerator [IAsyncEnumerable<>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1) that provides asynchronous iteration over values of a type for every tags.

``` CSharp
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



### ValueTask [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousValueResolve.cs)

In this scenario ValueTask is just wrapping a resolved instance.

``` CSharp
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    // Bind Service
    .Bind<IService>().To<Service>()
    .Container;

// Resolve an instance asynchronously via ValueTask
var instance = await container.Resolve<ValueTask<IService>>();
```



### Collection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Collection.cs)

To resolve all possible instances of any tags of the specific type as a _collection_ just use the injection _ICollection<T>_

``` CSharp
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



### Enumerable [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Enumerables.cs)

To resolve all possible instances of any tags of the specific type as an _enumerable_ just use the injection _IEnumerable<T>_

``` CSharp
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



### Func dependency [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/FuncDependency.cs)

No comments. Everything is very simple!

``` CSharp
Func<IService> func = () => new Service(new Dependency());

using var container = Container
    .Create()
    .Bind<IService>().To(ctx => func())
    .Container;

var instance = container.Resolve<IService>();
```



### Func with arguments [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/FuncWithArguments.cs)

It is easy to use Func<T> with arguments and to pass these arguments to the created instances.

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



### Lazy [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Lazy.cs)

_Lazy_ dependency helps when a logic needs to inject Lazy<T> to get instance once on demand.

``` CSharp
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



### Observable [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Observable.cs)

To resolve all possible instances of any tags of the specific type as an _observable_ just use the injection _IObservable<T>_

``` CSharp
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



### Set [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Set.cs)

To resolve all possible instances of any tags of the specific type as a _set_ just use the injection _ISet<T>_

``` CSharp
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



### Struct [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Struct.cs)

Value types are fully supported avoiding any boxing/unboxing or cast operations, so the performance does not suffer!

``` CSharp
public void Run()
{
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



### ThreadLocal [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ThreadLocal.cs)



``` CSharp
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>()
    .Container;

// Resolve the instance of ThreadLocal<IService>
var threadLocal = container.Resolve<ThreadLocal<IService>>();

// Get the instance via ThreadLocal
var instance = threadLocal.Value;
```



### Tuple [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Tuple.cs)

[Tuple](https://docs.microsoft.com/en-us/dotnet/api/system.tuple) has a specific number and sequence of elements which may be resolved at the same time.

``` CSharp
using var container = Container
    .Create()
    .Bind<IDependency>().To<Dependency>()
    .Bind<IService>().To<Service>()
    .Bind<INamedService>().To<NamedService>(ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name"))
    .Container;

// Resolve an instance of type Tuple<IService, INamedService>
var tuple = container.Resolve<Tuple<IService, INamedService>>();
```



### ValueTuple [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ValueTuple.cs)



``` CSharp
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



### Configuration [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Configuration.cs)

Configuration classes are used to dedicate a logic responsible for configuring containers.

``` CSharp
public void Run()
{
    using var container = Container
        .Create()
        .Using<Glue>();

    var instance = container.Resolve<IService>();
}

public class Glue : IConfiguration
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



### Injection of default parameters [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/DefaultParamsInjection.cs)



``` CSharp
public void Run()
{
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



### Generic autowiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/GenericAutowiring.cs)

Autowiring of generic types as simple as autowiring of other simple types. Just use a generic parameters markers like _TT_, _TT1_, _TT2_ and etc. or TTI, TTI1, TTI2 ... for interfaces or TTS, TTS1, TTS2 ... for value types or other special markers like TTDisposable, TTDisposable1 and etc. TTList<>, TTDictionary<> ... or create your own generic parameters markers or bind open generic types.

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
```



### Optional injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/OptionalInjection.cs)



``` CSharp
public void Run()
{
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



### Resolve using arguments [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveWithArgs.cs)



``` CSharp
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



### Auto Disposing [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AutoDisposing.cs)

A [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance it's a very special instance. If it implements the _IDisposable_ (or IAsyncDisposable) interface the _Sigleton_ lifetime take care about disposing this instance after disposing of the owning container (where this type was registered) or if after the binding cancelation.

``` CSharp
var disposableService = new Mock<IDisposableService>();

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



### Aspect Oriented [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AspectOriented.cs)

This framework has no special attributes to support aspect oriented autowiring because of a production code should not have references to these special attributes. But this code may contain these attributes by itself. And it is quite easy to use these attributes for aspect oriented autowiring, see the sample below.

``` CSharp
public void Run()
{
    var console = new Mock<IConsole>();

    // Creates an aspect oriented autowiring strategy based the custom attribute `DependencyAttribute`
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

### Cyclic dependency [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CyclicDependency.cs)

By default, a circular dependency is detected after the 256th recursive resolution. This behaviour may be changed by overriding the interface _IFoundCyclicDependency_.

``` CSharp
public void Run()
{
    var expectedException = new InvalidOperationException("error");
    var foundCyclicDependency = new Mock<IFoundCyclicDependency>();
    // Throws the exception for reentrancy 128
    foundCyclicDependency.Setup(i => i.Resolve(It.Is<IBuildContext>(ctx => ctx.Depth == 128))).Throws(expectedException);

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

public interface ILink { }

public class Link : ILink
{
    public Link(ILink link) { }
}
```



### Plugins [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Plugins.cs)



``` CSharp
public void Run()
{
    // Given
    var pluginTypes = new[] { typeof(Plugin1), typeof(Plugin2), typeof(Plugin3) };

    using var container = Container.Create();
    foreach (var pluginType in pluginTypes)
    {
        // Should ensure uniqueness of plugin
        var uniquePluginId = pluginType;

        // Bind several opened types by a tag which should ensure uniqueness of binding
        container.Bind(typeof(IPlugin)).Tag(uniquePluginId).To(pluginType);
    }

    // When

    // Resolve instances
    var plugins = container.Resolve<IEnumerable<IPlugin>>();

    // This also works when you cannot use a generic type like IEnumerable<IPlugin>
    // var plugins = container.Resolve<IEnumerable<object>>(typeof(IEnumerable<>).MakeGenericType(typeof(IPlugin)));

    // Then
    var resolvedPluginTypes = plugins.Select(i => i.GetType()).ToList();

    resolvedPluginTypes.Count.ShouldBe(3);

    // We cannot rely on order here
    resolvedPluginTypes.Contains(typeof(Plugin1)).ShouldBeTrue();
    resolvedPluginTypes.Contains(typeof(Plugin2)).ShouldBeTrue();
    resolvedPluginTypes.Contains(typeof(Plugin3)).ShouldBeTrue();
}

interface IPlugin { }

class Plugin1 : IPlugin { }

class Plugin2 : IPlugin { }

class Plugin3 : IPlugin { }
```



### Generator sample [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Generator.cs)



``` CSharp
public void Run()
{
    // Create and configure the container using a configuration class 'Generators'
    using var container = Container.Create().Using<Generators>();
    using (container.Bind<(int, int)>().To(
        // Use a function because of the expression trees have a limitation in syntax
        ctx => System.ValueTuple.Create(
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



### Wrapper sample [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Wrapper.cs)



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
        .Create("child")
        .Bind<IClock>().To(ctx => clock.Object)
        // Bind 'ILogger' to the instance creation, actually represented as an expression tree
        // injecting the base logger from the parent container "root" and the clock from the current container "child"
        .Bind<ILogger>().To<TimeLogger>()
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

    // Adds current time as a message prefix and writes it to the console
    public void Log(string message) => _baseLogger.Log($"{_clock.Now}: {message}");
}
```



### Instant Messenger sample [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SimpleInstantMessenger.cs)



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



