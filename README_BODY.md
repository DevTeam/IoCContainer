## [Schrödinger's cat](Samples/ShroedingersCat) shows how it works [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://dotnetfiddle.net/dRebQM)

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
    public ShroedingersCat(State state) => State = state;

    public State State { get; }
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
class Glue : IConfiguration
{
    public IEnumerable<IDisposable> Apply(IContainer container)
    {
        yield return container.Bind<IBox<TT>>().To<CardboardBox<TT>>();
        yield return container.Bind<ICat>().To<ShroedingersCat>();

        // Models a random subatomic event that may or may not occur.
        yield return container.Bind<Random>().As(Singleton).To<Random>();
        yield return container.Bind<State>().To(ctx => (State)ctx.Container.Resolve<Random>().Next(2));
    }
}
```

### Time to open boxes!

Build up _Program_ using _Glue_

```csharp
using (new Glue().BuildUp<Program>()) { }
```

IoC container injects all required dependencies via _Program_ constructor (also it can be done via methods, properties or even fields). Of course the same is true for dependencies of dependencies. It works for most of well-known .net data strucures automatically as well.

```csharp
public Program(
  ICat cat,
  IBox<ICat> box,
  IBox<IBox<ICat>> bigBox,
  Func<IBox<ICat>> func,
  Task<IBox<ICat>> task,
  Tuple<IBox<ICat>, ICat, IBox<IBox<ICat>>> tuple,
  Lazy<IBox<ICat>> lazy,
  IEnumerable<IBox<ICat>> enumerable,
  IAsyncEnumerable<IBox<ICat>> asyncEnumerable,
  IBox<ICat>[] array,
  IList<IBox<ICat>> list,
  ISet<IBox<ICat>> set,
  IObservable<IBox<ICat>> observable,
  IBox<Lazy<Func<IEnumerable<IBox<ICat>>>>> complex,
  ThreadLocal<IBox<ICat>> threadLocal,
  ValueTask<IBox<ICat>> valueTask,
  (IBox<ICat> box, ICat cat, IBox<IBox<ICat>> bigBox) valueTuple) { ... }
```

![Cat is alive](https://github.com/DevTeam/IoCContainer/blob/master/Docs/Images/cat-is-alive.png?raw=true)

### Under the hood

Actually each dependency is resolved by a strongly-typed block of statements like the operator `new` which is compiled from the coresponding expression tree to create or to get a required dependency instance with minimal impact on performance or memory consumtion. For instance, the calling of constructor `Program` looks like:

```csharp
new Program(new ShroedingersCat() , new CardboardBox<ShroedingersCat>(new ShroedingersCat()), ...);
```

This works the same way for any initializing methods, properties or fields.

_**The incredible performance and the memory traffic minimization make it possible to use all benefits of dependency injection everywhere and everytime without any compromises - just like a keyword `new`.**_

## NuGet packages

|     | binary packages | embedding packages * |
| --- | --- | ---|
| ![IoC container](https://img.shields.io/badge/core-IoC%20container-orange.svg) | [![NuGet](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container) | [![NuGet](https://buildstats.info/nuget/IoC.Container.Source)](https://www.nuget.org/packages/IoC.Container.Source) |
| ![ASP.NET Core](https://img.shields.io/badge/feature-ASP.NET%20Core-orange.svg) | [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore)](https://www.nuget.org/packages/IoC.AspNetCore) | [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore.Source)](https://www.nuget.org/packages/IoC.AspNetCore.Source) |
| ![Interception](https://img.shields.io/badge/feature-Interception-orange.svg) | [![NuGet](https://buildstats.info/nuget/IoC.Interception)](https://www.nuget.org/packages/IoC.Interception) | [![NuGet](https://buildstats.info/nuget/IoC.Interception.Source)](https://www.nuget.org/packages/IoC.Interception.Source) |

* _Embedding packages_ require C# 7.0 or higher.

## Class References

- [.NET 4.8](Docs/IoC_net48.md)
- [.NET Standard 2.0](Docs/IoC_netstandard2.0.md)
- [.NET Core 3.0](Docs/IoC_netcoreapp3.0.md)
- [UWP 10.0](Docs/IoC_uap10.0.md)

## ![ASP.NET Core](https://img.shields.io/badge/feature-ASP.NET%20Core-orange.svg)

### Add the [_NuGet_ package](https://www.nuget.org/packages/IoC.AspNetCore) reference

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

  // Create IoC container
  var container = Container.Create()
    // using .NET ASP Feature
    .Using(new AspNetCoreFeature(services))
    // using Glue
    .Using<Glue>();

  // Resolve IServiceProvider
  return container.Resolve<IServiceProvider>();
}
```

For more information please see [this sample](Samples/AspNetCore).

## ![Interception](https://img.shields.io/badge/feature-Interception-orange.svg)

### Add the [_NuGet_ package](https://www.nuget.org/packages/IoC.Interception) reference

- Package Manager

  ```
  Install-Package IoC.Interception
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.Interception
  ```

### Create the _IoC container_ using _InterceptionFeature_ and intercept all invocations to _Service_ by your _MyInterceptor_

```csharp
using var container = Container.Create().Using<InterceptionFeature>();
using (container.Bind<IService>().To<Service>())
using (container.Intercept<IService>(new MyInterceptor()))
{ }
```

## Why this one?

The results of the [comparison tests](IoC.Comparison/ComparisonTests.cs) for some popular IoC containers like Castle Windsor, Autofac, Unity, Ninject ...

![Cat](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,pinned:true,status:SUCCESS/artifacts/content/REPORT.jpg)
