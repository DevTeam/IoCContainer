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
  - Does not need additional dependencies

Supported platforms:
  - .NET 4.0+
  - .NET Core 1.0+
  - .NET Standard 1.0+

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
    public CardboardBox(T content) { Content = content; }

    public T Content { get; }

    public override string ToString() { return Content.ToString(); }
}

class ShroedingersCat : ICat
{
    public bool IsAlive => new Random().Next(2) == 1;

    public override string ToString() { return $"Is alive: {IsAlive}"; }
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

### Let's glue our abstraction and our implementation

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

## Class References

- [.NET 4.0](IoC/IoC_net40.md)
- [.NET 4.5](IoC/IoC_net45.md)
- [.NET Standard 1.0](IoC/IoC_netstandard1.0.md)
- [.NET Core 2.0](IoC/IoC_netcoreapp2.0.md)

## Why this one?

The results of the [comparison tests](IoC.Comparison/ComparisonTests.cs) for some popular IoC containers like Castle Windsor, Autofac, Unity, Ninject ...

![Cat](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,status:SUCCESS/artifacts/content/REPORT.jpg)
