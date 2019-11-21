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
  public IEnumerable<IToken> Apply(IContainer container)
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

// Gets a cardboard box in the same way as the following expression:
// var box = new CardboardBox<ICat>(new ShroedingersCat(new Lazy<State>(() => (State)indeterminacy.Next(2))));
var box = container.Resolve<IBox<ICat>>();
// Checks the cat's state
WriteLine(box.Content);
```

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

## Class References

- [.NET 4.8](Docs/IoC_net48.md)
- [.NET Standard 2.0](Docs/IoC_netstandard2.0.md)
- [.NET Core 3.0](Docs/IoC_netcoreapp3.0.md)
- [UWP 10.0](Docs/IoC_uap10.0.md)

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

### Create the _IoC container_ with feature _AspNetCoreFeature_ and configure it at [Startup](Samples/AspNetCore/WebApplication/Startup.cs)

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

The results of the [comparison tests](IoC.Comparison/ComparisonTests.cs) for some popular IoC containers like Castle Windsor, Autofac, Unity, Ninject ...

![Cat](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/REPORT.jpg)
