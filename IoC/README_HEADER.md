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
}

class ShroedingersCat : ICat
{
  public bool IsAlive => new Random().Next(2) == 1;
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
  var box1 = container.Get<IBox<ICat>>();
  Console.WriteLine("#1 is alive: " + box1.Content.IsAlive);

  // Func
  var box2 = container.Get<Func<IBox<ICat>>>();
  Console.WriteLine("#2 is alive: " + box2().Content.IsAlive);

  // Task
  var box3 = await container.Get<Task<IBox<ICat>>>();
  Console.WriteLine("#3 is alive: " + box3.Content.IsAlive);

  // Tuple
  var box4 = container.Get<Tuple<IBox<ICat>, ICat>>();
  Console.WriteLine("#4 is alive: " + box4.Item1.Content.IsAlive + ", " + box4.Item2.IsAlive);

  // Lazy
  var box5 = container.Get<Lazy<IBox<ICat>>>();
  Console.WriteLine("#5 is alive: " + box5.Value.Content.IsAlive);

  // Enumerable
  var boxes6 = container.Get<IEnumerable<IBox<ICat>>>();
  Console.WriteLine("#6 is alive: " + boxes6.Single().Content.IsAlive);

  // List
  var boxes7 = container.Get<IList<IBox<ICat>>>();
  Console.WriteLine("#7 is alive: " + boxes7[0].Content.IsAlive);
}
```

### Under the hood

Actually these getters are represented just as set of operators `new` that allow to create required instances.

```csharp
var box = new CardboardBox<ShroedingersCat>(new ShroedingersCat());
```

There is only one difference - this getter are wrapped to compiled lambda function and the each call of these lambdas spends some minimal time in the operator `call`, but in actual scenarios it is not required to make these lambdas each time to create an instance.
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

The results of the [comparison tests](IoC.Tests/ComparisonTests.cs) for some popular IoC containers like Castle Windsor, Autofac, Unity, Ninject ...

![Cat](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,status:SUCCESS/artifacts/content/REPORT.jpg)