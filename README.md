[<img src="http://tcavs2015.cloudapp.net/app/rest/builds/buildType:(id:DevTeam_IoCContainer_Build)/statusIcon"/>](http://tcavs2015.cloudapp.net/viewType.html?buildTypeId=DevTeam_IoCContainer_Build&guest=1) [<img src="https://www.nuget.org/Content/Logos/nugetlogo.png" height="18">](https://github.com/DevTeam/IoCContainer/wiki/NuGet-packages)

# Simple, powerful and fast Inversion of Control container for .NET

There are many different implementations of [Inversion of Control](https://github.com/DevTeam/IoCContainer/wiki/Inversion-of-Control). Why it would be preferable to use this implementation? It has many outstanding [features](https://github.com/DevTeam/IoCContainer/wiki/Features) to make your code more efficient. See [Wiki](https://github.com/DevTeam/IoCContainer/wiki) for details.

Supported platforms:
  - .NET 4.5+
  - .NET Core 1.0+
  - .NET Standard 1.0+

![Comparison test](https://github.com/DevTeam/IoCContainer/blob/master/Docs/Images/speed.png) [Comparison test](https://github.com/DevTeam/IoCContainer/blob/master/IoC.Tests/ComparisonTests.cs) of some IoC containers in the synthetic test (creating a graph from 2 transient and singleton objects in the serie of 100k iterations) has the following [result](http://tcavs2015.cloudapp.net/httpAuth/app/rest/builds/buildType:DevTeam_IoCContainer_Build,status:SUCCESS/artifacts/content/reports/Comparison.zip).

[Shroedingers Cat](https://github.com/DevTeam/IoCContainer/tree/master/Samples/ShroedingersCat) shows how it works:

```csharp
using (var container = Container.Create().Using(new Glue()))
{
  // Directly getting
  var box1 = container.Get<IBox<ICat>>();
  Console.WriteLine($"#1 is alive: {box1.Content.IsAlive}");

  // Func way
  var box2 = container.FuncGet<IBox<ICat>>();
  Console.WriteLine($"#2 is alive: {box2().Content.IsAlive}");

  // Async way
  var box3 = await container.StartGet<IBox<ICat>>(TaskScheduler.Default);
  Console.WriteLine($"#3 is alive: {box3.Content.IsAlive}");
  return box3.Content.IsAlive;
}
```

**Abstraction**:
```csharp
interface IBox<out T> { T Content { get; } }

interface ICat { int IsAlive { get; } }
```

**Implementation**:

![Cat](https://github.com/DevTeam/IoCContainer/blob/master/Docs/Images/cat.jpg)

```csharp
class CardboardBox<T> : IBox<T>
{
  public CardboardBox(T content) { Content = content; }

  public T Content { get; }
}

class ShroedingersCat : ICat
{
  public int IsAlive => new Random().Next(2);
}
```

Let's glue it together:
```csharp
class Glue : IConfiguration
{
  public IEnumerable<IDisposable> Apply(IContainer container)
  {
    yield return container.Pair(typeof(IBox<>), typeof(CardboardBox<>));
    yield return container.Pair<ICat, ShroedingersCat>();
  }
}
```