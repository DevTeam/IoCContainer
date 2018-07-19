## NuGet Packages

  - IoC.Container
    - [![NuGet](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container)
  - IoC.Container.Source, embedded-in-code
    - [![NuGet](https://buildstats.info/nuget/IoC.Container.Source)](https://www.nuget.org/packages/IoC.Container.Source)
  - IoC.AspNetCore
    - [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore)](https://www.nuget.org/packages/IoC.AspNetCore)
  - IoC.AspNetCore.Source, embedded-in-code
    - [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore.Source)](https://www.nuget.org/packages/IoC.AspNetCore.Source)

Embedding-in-code packages require C# 7.0 or higher

## [Schrödinger's cat](Samples/ShroedingersCat) shows how it works

### The reality is that

![Cat](https://github.com/DevTeam/IoCContainer/blob/master/Docs/Images/cat.jpg?raw=true)

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

You could use a dedicated class(es) to declare dependencies.

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

### Time to open boxes!

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

Let's take a note that the real "Inversion of Control container" scenario is not supposing using any resolving methods, except a single root resolving (just imagine here that boxes equal to root instances in the sample above). It is better when all dependencies are injected automatically via constructors, initializing methods, properties or even fields.

### Under the hood

Actually each instance is resolved by strongly-typed function which is represented just as a graph of operators `new` compiled from expression trees which allow to create (or to get) required instances with minimal performance and memory impact. Thus the code like

```csharp
var box = container.Resolve<IBox<ICat>>();
```

will be compiled as invocation of the following strongly-typed function `Resolve()` excluding any boxing/unboxing statements:

```csharp
CardboardBox<ShroedingersCat> Resolve()
{
  new CardboardBox<ShroedingersCat>(new ShroedingersCat());
}
```

This single function contains a full set of operators `new` related constructors of all dependencies. This is also true for any initializers like methods, properties or fields.

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
