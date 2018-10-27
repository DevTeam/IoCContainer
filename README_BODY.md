### NuGet Packages

|     | binary packages | embedding packages |
| --- | --- | ---|
| ![IoC container](https://img.shields.io/badge/core-IoC%20container-orange.svg) | [![NuGet](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container) | [![NuGet](https://buildstats.info/nuget/IoC.Container.Source)](https://www.nuget.org/packages/IoC.Container.Source) |
| ![ASP.NET Core](https://img.shields.io/badge/feature-ASP.NET%20Core-orange.svg) | [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore)](https://www.nuget.org/packages/IoC.AspNetCore) | [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore.Source)](https://www.nuget.org/packages/IoC.AspNetCore.Source) |
| ![Interception](https://img.shields.io/badge/feature-Interception-orange.svg) | [![NuGet](https://buildstats.info/nuget/IoC.Interception)](https://www.nuget.org/packages/IoC.Interception) | [![NuGet](https://buildstats.info/nuget/IoC.Interception.Source)](https://www.nuget.org/packages/IoC.Interception.Source) |

_Embedding packages_ require C# 7.0 or higher.

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

    public override string ToString() { return "[" + Content + "]"; }
}

class ShroedingersCat : ICat
{
    public ShroedingersCat(State state) { State = state; }

    public State State { get; private set; }

    public override string ToString() { return State + " cat"; }
}
```

_**It is important to note that our abstraction and our implementation do not know anything about IoC containers**_

### Let's glue all together

At first add the package reference to [IoC.Container](https://www.nuget.org/packages/IoC.Container). It ships entirely as NuGet packages. Using NuGet packages allows you to optimize your application to include only the necessary dependencies.

- Package Manager

  ```
  Install-Package IoC.Container
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.Container
  ```

After that declare required dependencies in a dedicated class _Glue_.

```csharp
class Glue : IConfiguration
{
  public IEnumerable<IDisposable> Apply(IContainer container)
  {
    yield return container.Bind<IBox<TT>>().To<CardboardBox<TT>>();
    yield return container.Bind<ICat>().To<ShroedingersCat>();

    var rnd = new Random();
    yield return container.Bind<State>().To(_ => (State)rnd.Next(2));
  }
}
```

### Time to open boxes!

Build up _Program_ using _Glue_

```csharp
using (var container = Container.Create().Using<Glue>())
{
  container.BuildUp<Program>();
}
```

injecting a set of dependencies via _Program_ constructor (also it can be done via methods, properties or even fields)

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
  IBox<ICat>[] array,
  IList<IBox<ICat>> list,
  ISet<IBox<ICat>> set,
  IObservable<IBox<ICat>> observable,
  IBox<Lazy<Func<IEnumerable<IBox<ICat>>>>> complex,
  ThreadLocal<IBox<ICat>> threadLocal,
  ValueTask<IBox<ICat>> valueTask,
  (IBox<ICat> box, ICat cat, IBox<IBox<ICat>> bigBox) valueTuple) { ... }
```

### Under the hood

Actually each dependency is resolving by a strongly-typed block of statements like a operator `new` which is compiled from the coresponding expression tree to create or to get a required dependency instance with minimal performance and memory impact. Thus the calling of constructor looks like:

```csharp
new Program(new ShroedingersCat() , new CardboardBox<ShroedingersCat>(new ShroedingersCat()), ...);
```

This works the same way for any initializers like methods, properties or fields.

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

  // Create container
  var container = Container.Create().Using(new AspNetCoreFeature(services));

  // Configure container
  container.Using<Glue>();

  // Resolve IServiceProvider
  return container.Resolve<IServiceProvider>();
}
```

For more information see [this sample](Samples/AspNetCore).

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
using (var container = Container.Create().Using<InterceptionFeature>())
using (container.Bind<IService>().To<Service>())
using (container.Intercept<IService>(new MyInterceptor()))
{ }
```

## Class References

- [.NET 4.0](Docs/IoC_net40.md)
- [.NET 4.5](Docs/IoC_net45.md)
- [.NET Standard 1.0](Docs/IoC_netstandard1.0.md)
- [.NET Core 2.0](Docs/IoC_netcoreapp2.0.md)
- [UWP 10.0](Docs/IoC_uap10.0.md)

## Why this one?

The results of the [comparison tests](IoC.Comparison/ComparisonTests.cs) for some popular IoC containers like Castle Windsor, Autofac, Unity, Ninject ...

![Cat](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,status:SUCCESS/artifacts/content/REPORT.jpg)
