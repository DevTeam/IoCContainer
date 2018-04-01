# Simple, powerful and fast IoC container

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

Package| Link
---|:---:
IoC.Container|[![NuGet Version and Downloads count](https://buildstats.info/nuget/IoC.Container?includePreReleases=true)](https://www.nuget.org/packages/IoC.Container)
IoC.AspNetCore|[![NuGet Version and Downloads count](https://buildstats.info/nuget/IoC.AspNetCore?includePreReleases=true)](https://www.nuget.org/packages/IoC.AspNetCore)

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
  - Supports [Windows Presentation Foundation](#wpf)

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

IoC.Container ships entirely as NuGet packages. Using NuGet packages allows you to optimize your app to include only the necessary dependencies.

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

# [WPF](https://docs.microsoft.com/en-us/dotnet/framework/wpf/index)

### Create the IoC container, like [here](Samples/Windows/WpfApp/App.xaml.cs)

```csharp
public partial class App
{
    internal IContainer Container;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Container = IoC.Container.Create().Using<Configuration>();
        Container.Resolve<IMainWindowView>().Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Container.Dispose();
        base.OnExit(e);
    }
}
```

Where _Configuration_ is the [configuration](Samples/Windows/WpfApp/Configuration.cs) of IoC container.

### Implement a data provider class, for instance like [DataProvider](Samples/Windows/WpfApp/DataProvider.cs)

```csharp
public class DataProvider: ObjectDataProvider
{
    // Design Time Container
    private static readonly Lazy<IContainer> ContainerDesignTime 
        = new Lazy<IContainer>(() => Container.Create().Using<ConfigurationDesignTime>());

    public object Tag { get; set; }

    protected override void BeginQuery()
    {
        base.BeginQuery();
        if (Application.Current is App app)
            OnQueryFinished(app.Container.Resolve<object>(ObjectType, Tag));
        else 
            OnQueryFinished(ContainerDesignTime.Value.Resolve<object>(ObjectType, Tag));
    }
}
```

Where _ConfigurationDesignTime_ is the [desing time configuration](Samples/Windows/WpfApp/ConfigurationDesignTime.cs) of IoC container.

### Use it in XAML do bind view models like [here](Samples/Windows/WpfApp/Views/MainWindow.xaml)

```xml
<Window x:Class="WpfApp.Views.MainWindow" x:ClassModifier="internal"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:WpfApp"
        xmlns:vewModels="clr-namespace:WpfApp.VewModels">
    <Window.Resources>
        <local:DataProvider x:Key="ClockViewModel" ObjectType="{x:Type vewModels:IClockViewModel}" />
    </Window.Resources>
    <Grid DataContext="{StaticResource ClockViewModel}">
        <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
            <TextBlock Text="{Binding Date}" FontSize="64" />
            <TextBlock Text="{Binding Time}" FontSize="64" />
        </StackPanel>
    </Grid>
</Window>
```

For more information see [this sample](Samples/Windows/WpfApp).

## Class References

- [.NET 4.0](Docs/IoC_net40.md)
- [.NET 4.5](Docs/IoC_net45.md)
- [.NET Standard 1.0](Docs/IoC_netstandard1.0.md)
- [.NET Core 2.0](Docs/IoC_netcoreapp2.0.md)

## Why this one?

The results of the [comparison tests](IoC.Comparison/ComparisonTests.cs) for some popular IoC containers like Castle Windsor, Autofac, Unity, Ninject ...

![Cat](http://tcavs2015.cloudapp.net/guestAuth/app/rest/builds/buildType:DevTeam_IoCContainer_CreateReports,status:SUCCESS/artifacts/content/REPORT.jpg)
