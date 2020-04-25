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

### Graph of 27 transient instances

![Transient](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/IoC.Benchmark.Transient-report.jpg)

### Graph of 20 transient instances and 1 singleton instance

![Singleton](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/IoC.Benchmark.Singleton-report.jpg)

### Graph of 364 transient instances of unique type

![Complex](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/IoC.Benchmark.Complex-report.jpg)

- __new__ - _Method_ when the graph of objects was constructed by operators _new_ only
- __Mean__ - arithmetic mean of the root instances resolved per nanosecond
- __Error__ - half of 99.9% confidence interval
- __StdDev__ - standard deviation of all measurements
- __Median__ - value separating the higher half of all measurements (50th percentile)
- __1 ns__ - 1 Nanosecond (0.000000001 sec)

_[BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) was used to measure and analyze these results._

