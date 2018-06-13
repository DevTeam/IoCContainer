# Simple, powerful and fast IoC container

[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

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

NuGet packages:
  - IoC.Container
    - [![NuGet](https://buildstats.info/nuget/IoC.Container?includePreReleases=true)](https://www.nuget.org/packages/IoC.Container)
  - IoC.Container.Source, embedded-in-code
    - [![NuGet](https://buildstats.info/nuget/IoC.Container.Source?includePreReleases=true)](https://www.nuget.org/packages/IoC.Container.Source)
  - IoC.AspNetCore
    - [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore?includePreReleases=true)](https://www.nuget.org/packages/IoC.AspNetCore)
  - IoC.AspNetCore.Source, embedded-in-code
    - [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore.Source?includePreReleases=true)](https://www.nuget.org/packages/IoC.AspNetCore.Source)

Embedded-in-code packages requires C# 7.0 or higher

Supported platforms:
  - .NET 4.0+
  - [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/) 1.0+
  - [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) 1.0+
  - [UWP](https://docs.microsoft.com/en-us/windows/uwp/index) 10+

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

IoC.Container ships entirely as NuGet packages. Using NuGet packages allows you to optimize your application to include only the necessary dependencies.

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

Actually these resolvers are represented just as a set of operators `new` which allow to create (or to get) required instances.

```csharp
var box = new CardboardBox<ShroedingersCat>(new ShroedingersCat());
```

There is only one difference - these resolvers are wrapped to compiled lambda-functions and an each call of these lambda-functions spends some minimal time in the operator `call`, but in actual scenarios it is not required to make these calls each time to create instances.
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
