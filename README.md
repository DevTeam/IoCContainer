# Simple, powerful and fast IoC container

[![NuGet Version and Downloads count](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container) [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

Supported platforms:
  - .NET 4.0+
  - .NET Core 1.0+
  - .NET Standard 1.0+

## [Schrödinger's cat](https://github.com/DevTeam/IoCContainer/tree/master/Samples/ShroedingersCat) shows how it works

### The reality is that

![Cat](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/Docs/Images/cat.jpg)

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

### Add a [![NuGet Version and Downloads count](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container) reference to the IoC.Container library by one of these ways:

- Package Manager

  ```
  Install-Package IoC.Container
  ```
  
- .NET CLI
  
  ```
  dotnet add package IoC.Container
  ```
  
- Packet CLI

  ```
  paket add IoC.Container
  ```

### Let's glue our abstraction and our implementation

```csharp
class Glue : IConfiguration
{
  public IEnumerable<IDisposable> Apply(IContainer container)
  {
    yield return container.Bind(typeof(IBox<>)).To(typeof(CardboardBox<>));
    yield return container.Bind<ICat>().To<ShroedingersCat>();
  }
}
```

### Just configure the container and check it works as expected

```csharp
using (var container = Container.Create().Using<Glue>())
{
  // Directly getting
  var box1 = container.Get<IBox<ICat>>();
  Console.WriteLine("#1 is alive: " + box1.Content.IsAlive);

  // Func way
  var box2 = container.FuncGet<IBox<ICat>>();
  Console.WriteLine("#2 is alive: " + box2().Content.IsAlive);

  // Async way
  var box3 = await container.AsyncGet<IBox<ICat>>(TaskScheduler.Default);
  Console.WriteLine("#3 is alive: " + box3.Content.IsAlive);
}
```

## Why this one?

See the [results](http://tcavs2015.cloudapp.net/httpAuth/app/rest/builds/buildType:DevTeam_IoCContainer_Build,status:SUCCESS/artifacts/content/reports.zip) of the [comparison tests](https://github.com/DevTeam/IoCContainer/blob/master/IoC.Tests/ComparisonTests.cs) for some popular IoC containers like Castle Windsor, Autofac, Unity, Ninject ...

## The build state

[<img src="http://tcavs2015.cloudapp.net/app/rest/builds/buildType:(id:DevTeam_IoCContainer_Build)/statusIcon"/>](http://tcavs2015.cloudapp.net/viewType.html?buildTypeId=DevTeam_IoCContainer_Build&guest=1)
