# Simple, powerful and fast Inversion of Control container for .NET

[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE) [<img src="http://tcavs2015.cloudapp.net/app/rest/builds/buildType:(id:DevTeam_IoCContainer_Build)/statusIcon"/>](http://tcavs2015.cloudapp.net/viewType.html?buildTypeId=DevTeam_IoCContainer_Build&guest=1)

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

|     | binary packages | source code packages * |
| --- | --- | ---|
| ![IoC container](https://img.shields.io/badge/core-IoC%20container-orange.svg) | [![NuGet](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container) | [![NuGet](https://buildstats.info/nuget/IoC.Container.Source)](https://www.nuget.org/packages/IoC.Container.Source) |
| ![ASP.NET Core](https://img.shields.io/badge/feature-ASP.NET%20Core-orange.svg) | [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore)](https://www.nuget.org/packages/IoC.AspNetCore) | [![NuGet](https://buildstats.info/nuget/IoC.AspNetCore.Source)](https://www.nuget.org/packages/IoC.AspNetCore.Source) |
| ![Interception](https://img.shields.io/badge/feature-Interception-orange.svg) | [![NuGet](https://buildstats.info/nuget/IoC.Interception)](https://www.nuget.org/packages/IoC.Interception) | [![NuGet](https://buildstats.info/nuget/IoC.Interception.Source)](https://www.nuget.org/packages/IoC.Interception.Source) |

* _source code packages_ require C# 7.0 or higher.

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
### Supported Platforms

  - .NET 4.0+
  - [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/) 1.0+
  - [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) 1.0+
  - [UWP](https://docs.microsoft.com/en-us/windows/uwp/index) 10+

### Easy Integration

  - [ASP.NET Core](#aspnet-core)
  - [Xamarin](https://github.com/DevTeam/IoCContainer/blob/master/Samples/XamarinXaml)
  - [Windows Presentation Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WpfApp)
  - [.NET core Windows Presentation Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WpfAppNetCore) 
  - [Universal Windows Platform](https://github.com/DevTeam/IoCContainer/blob/master/Samples/UwpApp)
  - [Windows Communication Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WcfServiceLibrary)
  - [Entity Framework](https://github.com/DevTeam/IoCContainer/tree/master/Samples/EntityFrameworkCore)


## Usage Scenarios

- Powerful Injection
  - [Resolve Func](#resolve-func-)
  - [Resolve Lazy](#resolve-lazy-)
  - [Resolve ThreadLocal](#resolve-threadlocal-)
  - [Resolve Tuple](#resolve-tuple-)
  - [Resolve ValueTuple](#resolve-valuetuple-)
  - [Method Injection](#method-injection-)
  - [Property Injection](#property-injection-)
  - [Constructor Autowiring](#constructor-autowiring-)
  - [Resolve all appropriate instances as Array](#resolve-all-appropriate-instances-as-array-)
  - [Resolve all appropriate instances as IAsyncEnumerable](#resolve-all-appropriate-instances-as-iasyncenumerable-)
  - [Resolve all appropriate instances as ICollection](#resolve-all-appropriate-instances-as-icollection-)
  - [Resolve all appropriate instances as IEnumerable](#resolve-all-appropriate-instances-as-ienumerable-)
  - [Resolve all appropriate instances as IObservable source](#resolve-all-appropriate-instances-as-iobservable-source-)
  - [Resolve Using Arguments](#resolve-using-arguments-)
  - [Resolve all appropriate instances as ISet](#resolve-all-appropriate-instances-as-iset-)
- Flexible Binding
  - [Autowiring](#autowiring-)
  - [Autowiring with initialization](#autowiring-with-initialization-)
  - [Generic Autowiring](#generic-autowiring-)
  - [Constant](#constant-)
  - [Generics](#generics-)
  - [Several Contracts](#several-contracts-)
  - [Tags](#tags-)
  - [Value](#value-)
  - [Dependency Tag](#dependency-tag-)
  - [Func](#func-)
  - [Singleton lifetime](#singleton-lifetime-)
  - [Child Container](#child-container-)
  - [Container Singleton lifetime](#container-singleton-lifetime-)
  - [Scope Singleton lifetime](#scope-singleton-lifetime-)
  - [Manual Autowiring](#manual-autowiring-)
  - [Struct](#struct-)
  - [Func With Arguments](#func-with-arguments-)
  - [Auto dispose singleton during container's dispose](#auto-dispose-singleton-during-containers-dispose-)
  - [Configuration via a text metadata](#configuration-via-a-text-metadata-)
  - [Validation](#validation-)
  - [Aspect Oriented Autowiring](#aspect-oriented-autowiring-)
- Multithreading-Ready
  - [Asynchronous resolve](#asynchronous-resolve-)
  - [Asynchronous lightweight resolve](#asynchronous-lightweight-resolve-)
- Design Aspects
  - [Configuration class](#configuration-class-)
  - [Change configuration on-the-fly](#change-configuration-on-the-fly-)
- Fully Customizable
  - [Custom Builder](#custom-builder-)
  - [Custom Child Container](#custom-child-container-)
  - [Custom Lifetime](#custom-lifetime-)
  - [Interception](#interception-)
  - [Replace Lifetime](#replace-lifetime-)
- Other Samples
  - [Cyclic Dependency](#cyclic-dependency-)
  - [Generator](#generator-)
  - [Instant Messenger](#instant-messenger-)
  - [Wrapper](#wrapper-)

### Autowiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Autowiring.cs)

Auto-writing is most natural way to use containers. At first step we should create a container. At the second step we bind interfaces to their implementations. After that the container is ready to resolve dependencies.

``` CSharp
// Create the container and configure it, using full autowiring
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve an instance of interface `IService`
    var instance = container.Resolve<IService>();
}

```



### Autowiring with initialization [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AutoWiringWithInitialization.cs)

Auto-writing allows to perform some initializations.

``` CSharp
// Create the container and configure it, using full autowiring
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To<InitializingNamedService>(ctx => ctx.It.Initialize("text", ctx.Container.Resolve<IDependency>())))
{
    // Resolve an instance of interface `IService`
    var instance = container.Resolve<INamedService>();
}

```



### Generic Autowiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/GenericAutowiring.cs)

Auto-writing of generic types as simple as auto-writing of other types. Just use a generic parameters markers like _TT_, _TT1_ and etc. or bind open generic types.

``` CSharp
// Create and configure the container using autowiring
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind to the instance creation, actually represented as an expression tree
using (container.Bind<IService<TT>>().To<Service<TT>>())
    // or the same: using (container.Bind(typeof(IService<>)).To(typeof(Service<>)))
{
    // Resolve a generic instance
    var instance = container.Resolve<IService<int>>();
}

```



### Constant [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Constant.cs)

It's obvious here.

``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<int>().To(ctx => 10))
{
    // Resolve an integer
    var val = container.Resolve<int>();

    // Check the value
    val.ShouldBe(10);
}

```



### Generics [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Generics.cs)

Auto-writing of generic types via binding of open generic types or generic type markers are working the same way.

``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind open generic interface to open generic implementation
using (container.Bind(typeof(IService<>)).To(typeof(Service<>)))
    // Or (it is working the same) just bind generic interface to generic implementation, using marker classes TT, TT1, TT2 and so on
using (container.Bind<IService<TT>>().Tag("just generic").To<Service<TT>>())
{
    // Resolve a generic instance using "open generic" binding
    var instance1 = container.Resolve<IService<int>>();

    // Resolve a generic instance using "just generic" binding
    var instance2 = container.Resolve<IService<string>>("just generic".AsTag());
}

```



### Several Contracts [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SeveralContracts.cs)

It is possible to bind several types to single implementation.

``` CSharp
// Create and configure the container, using full autowiring
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<Service, IService, IAnotherService>().To<Service>())
{
    // Resolve instances
    var instance1 = container.Resolve<IService>();
    var instance2 = container.Resolve<IAnotherService>();
}

```



### Tags [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Tags.cs)

Tags are useful while binding to several implementations.

``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind using several tags
using (container.Bind<IService>().Tag(10).Tag().Tag("abc").To<Service>())
{
    // Resolve instances using tags
    var instance1 = container.Resolve<IService>("abc".AsTag());
    var instance2 = container.Resolve<IService>(10.AsTag());

    // Resolve the instance using the empty tag
    var instance3 = container.Resolve<IService>();
}

```



### Value [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Value.cs)

In this case the specific type is binded to the manually created instance based on an expression tree. This dependency will be introduced as is, without any additional overhead like _lambda call_ or _type cast_.

``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IService>().To(ctx => new Service(new Dependency())))
{
    // Resolve an instance
    var instance = container.Resolve<IService>();
}

```



### Dependency Tag [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/DependencyTag.cs)

Use a _tag_ to inject specific dependency from several bindings of the same types.

``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().Tag("MyDep").To<Dependency>())
    // Configure autowiring and inject dependency tagged by "MyDep"
using (container.Bind<IService>().To<Service>(
    ctx => new Service(ctx.Container.Inject<IDependency>("MyDep"))))
{
    // Resolve an instance
    var instance = container.Resolve<IService>();
}

```



### Func [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Func.cs)

_Func_ dependency helps when a logic requires to inject some number of type's instances on demand.

``` CSharp
Func<IService> func = () => new Service(new Dependency());

// Create and configure the container
using var container = Container.Create();
using (container.Bind<IService>().To(ctx => func()))
{
    // Resolve an instance
    var instance = container.Resolve<IService>();
}
```



### Singleton lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SingletonLifetime.cs)

Singleton is a design pattern which stands for having only one instance of some class during the whole application lifetime. The main complaint about Singleton is that it contradicts the Dependency Injection principle and thus hinders testability. It essentially acts as a global constant, and it is hard to substitute it with a test when needed. The _Singleton lifetime_ is indispensable in this case.

``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Use the Singleton lifetime
using (container.Bind<IService>().As(Singleton).To<Service>())
{
    // Resolve the singleton twice
    var parentInstance1 = container.Resolve<IService>();
    var parentInstance2 = container.Resolve<IService>();

    // Check that instances from the parent container are equal
    parentInstance1.ShouldBe(parentInstance2);

    // Create a child container
    using var childContainer = container.Create();
    // Resolve the singleton twice
    var childInstance1 = childContainer.Resolve<IService>();
    var childInstance2 = childContainer.Resolve<IService>();

    // Check that instances from the child container are equal
    childInstance1.ShouldBe(childInstance2);

    // Check that instances from different containers are equal
    parentInstance1.ShouldBe(childInstance1);
}

```

The lifetime could be:
- _Singleton_ - single instance
- _ContainerSingleton_ - singleton per container
- _ScopeSingleton_ - singleton per scope

_Transient_ - is default lifetime and a new instance is creating each time

### Child Container [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ChildContainer.cs)



``` CSharp
// Create the parent container
using var parentContainer = Container.Create();
using var childContainer = parentContainer.Create();
childContainer.Parent.ShouldBe(parentContainer);

```



### Container Singleton lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ContainerLifetime.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Use the Container Singleton lifetime
using (container.Bind<IService>().As(ContainerSingleton).To<Service>())
{
    // Resolve the container singleton twice
    var parentInstance1 = container.Resolve<IService>();
    var parentInstance2 = container.Resolve<IService>();

    // Check that instances from the parent container are equal
    parentInstance1.ShouldBe(parentInstance2);

    // Create a child container
    using var childContainer = container.Create();
    // Resolve the container singleton twice
    var childInstance1 = childContainer.Resolve<IService>();
    var childInstance2 = childContainer.Resolve<IService>();

    // Check that instances from the child container are equal
    childInstance1.ShouldBe(childInstance2);

    // Check that instances from different containers are not equal
    parentInstance1.ShouldNotBe(childInstance1);
}

```



### Scope Singleton lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ScopeSingletonLifetime.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().As(ScopeSingleton).To<Dependency>())
{
    // Use the Scope Singleton lifetime for instance
    using (container.Bind<IService>().As(ScopeSingleton).To<Service>())
    {
        // Resolve the default scope singleton twice
        var defaultScopeInstance1 = container.Resolve<IService>();
        var defaultScopeInstance2 = container.Resolve<IService>();

        // Check that instances from the default scope are equal
        defaultScopeInstance1.ShouldBe(defaultScopeInstance2);

        // Using the scope "a"
        using (new Scope("a").Begin())
        {
            var scopeInstance1 = container.Resolve<IService>();
            var scopeInstance2 = container.Resolve<IService>();

            // Check that instances from the scope "a" are equal
            scopeInstance1.ShouldBe(scopeInstance2);

            // Check that instances from different scopes are not equal
            scopeInstance1.ShouldNotBe(defaultScopeInstance1);
        }

        // Default scope again
        var defaultScopeInstance3 = container.Resolve<IService>();

        // Check that instances from the default scope are equal
        defaultScopeInstance3.ShouldBe(defaultScopeInstance1);
    }

    // Reconfigure the container to check dependencies only
    using (container.Bind<IService>().As(Transient).To<Service>())
    {
        // Resolve transient instances
        var transientInstance1 = container.Resolve<IService>();
        var transientInstance2 = container.Resolve<IService>();

        // Check that transient instances are not equal
        transientInstance1.ShouldNotBe(transientInstance2);

        // Check that dependencies from the default scope are equal
        transientInstance1.Dependency.ShouldBe(transientInstance2.Dependency);

        // Using the scope "a"
        using (new Scope("a").Begin())
        {
            // Resolve a transient instance in scope "a"
            var transientInstance3 = container.Resolve<IService>();

            // Check that dependencies from different scopes are not equal
            transientInstance3.Dependency.ShouldNotBe(transientInstance1.Dependency);
        }
    }
}

```



### Manual Autowiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ManualAutowiring.cs)



``` CSharp
// Create and configure the container using full autowiring
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
using (container.Bind<INamedService>().To<InitializingNamedService>(
    // Select the constructor and inject the dependency
    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
    // Configure the initializing method to invoke after the instance creation and inject the dependencies
    ctx => ctx.It.Initialize("some name", ctx.Container.Inject<IDependency>())))
{
    // Resolve an instance
    var instance = container.Resolve<INamedService>();

    // Check the instance's type
    instance.ShouldBeOfType<InitializingNamedService>();

    // Check the injected dependency
    instance.Name.ShouldBe("some name");
}

```



### Struct [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Struct.cs)



``` CSharp
public void Run()
{
    // Create and configure the container
    using var container = Container.Create();
    using (container.Bind<IDependency>().To<Dependency>())
        // Register the tracing builder
    using (container.Bind<TracingBuilder, IBuilder>().As(Singleton).To<TracingBuilder>())
        // Register a struct
    using (container.Bind<MyStruct>().To<MyStruct>())
    {
        // Resolve an instance
        var instance = container.Resolve<MyStruct>();

        // Check the expression which was used to create an instances of MyStruct
        var expressions = container.Resolve<TracingBuilder>().Expressions;
        var structExpression = expressions[new Key(typeof(MyStruct))].ToString();
        structExpression.ShouldBe("new MyStruct(new Dependency())");
        // Obvious there are no any superfluous operations like a `boxing`, `unboxing` or `cast`,
        // just only what is really necessary to create an instance
    }
}

public struct MyStruct
{
    public MyStruct(IDependency dependency) { }
}

// This builder saves expressions that used to create resolvers into a map
public class TracingBuilder : IBuilder
{
    public readonly IDictionary<Key, Expression> Expressions = new Dictionary<Key, Expression>();

    public Expression Build(Expression expression, IBuildContext buildContext)
    {
        Expressions[buildContext.Key] = expression;
        return expression;
    }
}
```



### Func With Arguments [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/FuncWithArguments.cs)



``` CSharp
Func<IDependency, string, INamedService> func = 
    (dependency, name) => new NamedService(dependency, name);

// Create and configure the container, using full autowiring
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind, selecting the constructor and inject argument[0] as the second parameter of type 'string'
using (container.Bind<INamedService>().To(
    ctx => func(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0])))
{
    // Resolve the instance, putting the string "alpha" into the array of arguments
    var instance = container.Resolve<INamedService>("alpha");

    // Check the instance's type
    instance.ShouldBeOfType<NamedService>();

    // Check that argument "alpha" was used during constructing an instance
    instance.Name.ShouldBe("alpha");

    // Resolve the function to create instance
    var getterFunc = container.Resolve<Func<string, INamedService>>();

    // Run this function and put the string "beta" as argument
    var otherInstance = getterFunc("beta");

    // Check that argument "beta" was used during constructing an instance
    otherInstance.Name.ShouldBe("beta");
}
```



### Auto dispose singleton during container's dispose [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AutoDisposeSingletonDuringContainersDispose.cs)



``` CSharp
var disposableService = new Mock<IDisposableService>();

// Create and configure the container
using (var container = Container.Create()
    .Bind<IService>().As(Lifetime.Singleton).To<IDisposableService>(ctx => disposableService.Object).ToSelf())
{
    // Resolve singleton instance twice
    var instance1 = container.Resolve<IService>();
    var instance2 = container.Resolve<IService>();

    // Check that instances are equal
    instance1.ShouldBe(instance2);
}

// Check the singleton was disposed after the container was disposed
disposableService.Verify(i => i.Dispose(), Times.Once);
disposableService.Verify(i => i.DisposeAsync(), Times.Once);
```



### Configuration via a text metadata [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConfigurationText.cs)



``` CSharp
// Create and configure the container from a metadata string
using var container = Container.Create().Using(
    "ref IoC.Tests;" +
    "using IoC.Tests.UsageScenarios;" +
    "Bind<IDependency>().As(Singleton).To<Dependency>();" +
    "Bind<IService>().To<Service>();");
// Resolve an instance
var instance = container.Resolve<IService>();
```



### Validation [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Validation.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IService>().To<Service>())
{
    // Try getting a resolver of the interface `IService`
    var canBeResolved = container.TryGetResolver<IService>(out _);

    // 'Service' has the mandatory dependency 'IDependency' in the constructor,
    // which was not registered and cannot be resolved
    canBeResolved.ShouldBeFalse();
}

```



### Aspect Oriented Autowiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AspectOrientedAutowiring.cs)



``` CSharp
public void Run()
{
    var console = new Mock<IConsole>();

    // Creates an aspect oriented autowiring strategy based on some custom `DependencyAttribute`
    var autowiringStrategy = AutowiringStrategies.AspectOriented()
        .Type<DependencyAttribute>(attribute => attribute.Type)
        .Order<DependencyAttribute>(attribute => attribute.Order)
        .Tag<DependencyAttribute>(attribute => attribute.Tag);

    // Create the root container
    using (var rootContainer = Container.Create("root"))
    // Configure the child container
    using (var childContainer = rootContainer.Create("child"))
    // Configure the child container by the custom aspect oriented autowiring strategy
    using (childContainer.Bind<IAutowiringStrategy>().To(ctx => autowiringStrategy))
    // Configure the child container
    using (childContainer.Bind<IConsole>().Tag("MyConsole").To(ctx => console.Object))
    using (childContainer.Bind<Clock>().To<Clock>())
    using (childContainer.Bind<string>().Tag("Prefix").To(ctx => "info"))
    using (childContainer.Bind<ILogger>().To<Logger>())
    {
        // Create a logger
        var logger = childContainer.Resolve<ILogger>();

        // Log the message
        logger.Log("Hello");
    }

    // Check the console output
    console.Verify(i => i.WriteLine(It.IsRegex(".+ - info: Hello")));
}

// Represents the dependency attribute to specify `tag`, 'order' or `type` for injection.
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
public class DependencyAttribute : Attribute
{
    // The tag, which will be used during an injection
    [CanBeNull] public readonly object Tag;

    // The order to be used to invoke a method
    public readonly int Order;

    // The tag, which will be used during an injection
    [CanBeNull] public readonly Type Type;

    public DependencyAttribute([CanBeNull] object tag = null, int order = 0, [CanBeNull] Type type = null)
    {
        Tag = tag;
        Order = order;
        Type = type;
    }            
}               

public interface IConsole
{
    // Writes a text
    void WriteLine(string text);
}

public interface IClock
{
    // Returns current time
    DateTimeOffset Now { get; }
}

public class Clock : IClock
{            
    public DateTimeOffset Now => DateTimeOffset.Now;
}

public interface ILogger
{
    // Logs a message
    void Log(string message);
}

public class Logger : ILogger
{
    private readonly IConsole _console;
    private IClock _clock;

    public Logger([Dependency(tag: "MyConsole")] IConsole console) => _console = console;

    // Method injection
    [Dependency(order: 1)]
    public void Initialize([Dependency(type: typeof(Clock))] IClock clock) => _clock = clock;

    // Property injection
    public string Prefix { get; [Dependency(tag: "Prefix", order: 2)] set; }

    // Adds current time and prefix before a message and writes it to console
    public void Log(string message) => _console?.WriteLine($"{_clock.Now} - {Prefix}: {message}");
}
```

Also you can specify your own aspect oriented autowiring by implementing the interface [_IAutowiringStrategy_](IoCContainer/blob/master/IoC/IAutowiringStrategy.cs).

### Custom Builder [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomBuilder.cs)



``` CSharp
public void Run()
{
    // Create and configure the container
    using var container = Container.Create();
    using (container.Bind<IDependency>().To<Dependency>())
    using (container.Bind<IService>().To<Service>(ctx => new Service(ctx.Container.Resolve<IDependency>(), ctx.Args[0] as string)))
    // Register the custom builder
    using (container.Bind<IBuilder>().To<NotNullGuardBuilder>())
    {
        // Resolve an instance passing null to the "state" parameter
        Assert.Throws<ArgumentNullException>(() => container.Resolve<IService>(null as string));
    }
}

// This custom builder adds the logic to check parameters of reference types injected via constructors on null
private class NotNullGuardBuilder : IBuilder
{
    public Expression Build(Expression expression, IBuildContext buildContext) =>
        expression is NewExpression newExpression && newExpression.Arguments.Count != 0
            ? newExpression.Update(CheckedArgs(newExpression))
            : expression;

    private static IEnumerable<Expression> CheckedArgs(NewExpression newExpression) =>
        from arg in newExpression.Constructor.GetParameters().Select((info, index) => (info, expression: newExpression.Arguments[index]))
        let typeDescriptor = arg.info.ParameterType.Descriptor()
        select !typeDescriptor.IsValueType()
            // arg ?? throw new ArgumentNullException(nameof(arg), "The argument ...")
            ? Expression.Coalesce(
                arg.expression,
                // throws an exception when an argument is null
                Expression.Block(
                    Expression.Throw(Expression.Constant(new ArgumentNullException(arg.info.Name, $"The argument \"{arg.info.Name}\" is null while constructing the instance of type \"{newExpression.Type.Name}\"."))),
                    Expression.Default(typeDescriptor.Type)))
            : arg.expression;
}
```



### Custom Child Container [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomChildContainer.cs)



``` CSharp
public void Run()
{
    // Create and configure the root container
    using var container = Container.Create();
    using (container.Bind<IService>().To<Service>())
    // Configure the root container to use a custom container during creating a child container
    using (container.Bind<IContainer>().Tag(WellknownContainers.NewChild).To<MyContainer>()) 
    // Create and configure the custom child container
    using (var childContainer = container.Create())
    // Bind some dependency
    using (childContainer.Bind<IDependency>().To<Dependency>())
    {
        // Resolve an instance
        var instance = childContainer.Resolve<IService>();

        // Check the child container's type
        childContainer.ShouldBeOfType<MyContainer>();
    }
}

// Sample of transparent container implementation
public class MyContainer: IContainer
{
    // some implementation here
}
```



### Custom Lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomLifetime.cs)



``` CSharp
public void Run()
{
    // Create and configure the container
    using var container = Container.Create();
    using (container.Bind<IDependency>().To<Dependency>())
        // Bind interface to implementation using the custom lifetime, based on the Singleton lifetime
    using (container.Bind<IService>().Lifetime(new MyTransientLifetime()).To<Service>())
    {
        // Resolve the singleton twice
        var instance1 = container.Resolve<IService>();
        var instance2 = container.Resolve<IService>();

        // Check that instances from the parent container are equal
        instance1.ShouldBe(instance2);
    }
}

// Represents the custom lifetime based on the Singleton lifetime
public class MyTransientLifetime : ILifetime
{
    // Creates the instance of the Singleton lifetime
    private ILifetime _baseLifetime = new Lifetimes.SingletonLifetime();

    // Wraps the expression by the Singleton lifetime expression
    public Expression Build(Expression expression, IBuildContext buildContext)
        => buildContext.AddLifetime(expression, _baseLifetime);

    // Creates the similar lifetime to use with generic instances
    public ILifetime Create() => new MyTransientLifetime();

    // Select a container to resolve dependencies using the Singleton lifetime logic
    public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
        _baseLifetime.SelectResolvingContainer(registrationContainer, resolvingContainer);

    // Disposes the instance of the Singleton lifetime
    public void Dispose() => _baseLifetime.Dispose();
}
```



### Interception [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Interception.cs)



``` CSharp
// To use this feature just add the NuGet package https://www.nuget.org/packages/IoC.Interception
// or https://www.nuget.org/packages/IoC.Interception.Source
public void Run()
{
    var methods = new List<string>();
    // Create and configure the container
    using var container = Container.Create().Using<InterceptionFeature>();
    using (container.Bind<IDependency>().To<Dependency>())
    using (container.Bind<IService>().To<Service>())
        // Configure the interception by 'MyInterceptor'
    using (container.Intercept<IService>(new MyInterceptor(methods)))
    {
        // Resolve an instance
        var instance = container.Resolve<IService>();

        // Invoke the getter "get_State"
        var state = instance.State;

        // Check invocations from the interceptor
        methods.ShouldContain("get_State");
    }
}

// This interceptor just stores the name of called methods
public class MyInterceptor : IInterceptor
{
    private readonly ICollection<string> _methods;

    // Stores the collection of called method names
    public MyInterceptor(ICollection<string> methods) => _methods = methods;

    // Intercepts the invocations and append the called method name to the collection
    public void Intercept(IInvocation invocation) => _methods.Add(invocation.Method.Name);
}
```



### Replace Lifetime [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ReplaceLifetime.cs)



``` CSharp
public void Run()
{
    var counter = new Mock<ICounter>();

    // Create and configure the container
    using (var container = Container.Create())
    using (container.Bind<ICounter>().To(ctx => counter.Object))
    // Replace the Singleton lifetime
    using (container.Bind<ILifetime>().Tag(Lifetime.Singleton).To<MySingletonLifetime>(
            // Select the constructor
            ctx => new MySingletonLifetime(
                // Inject the singleton lifetime from the parent container to use delegate logic
                ctx.Container.Parent.Inject<ILifetime>(Lifetime.Singleton),
                // Inject the counter to store the number of created instances
                ctx.Container.Inject<ICounter>())))
    // Configure the container as usual
    using (container.Bind<IDependency>().To<Dependency>())
    // Bind using the custom implementation of Singleton lifetime
    using (container.Bind<IService>().As(Lifetime.Singleton).To<Service>())
    {
        // Resolve the singleton twice using the custom lifetime
        var instance1 = container.Resolve<IService>();
        var instance2 = container.Resolve<IService>();

        // Check that instances are equal
        instance1.ShouldBe(instance2);
    }

    // Check the number of created instances
    counter.Verify(i => i.Increment(), Times.Exactly(2));
}

// Represents the instance counter
public interface ICounter
{
    void Increment();
}

public class MySingletonLifetime : ILifetime
{
    // Stores 'IncrementCounter' method info to the static field
    private static readonly MethodInfo IncrementCounterMethodInfo = typeof(MySingletonLifetime).GetTypeInfo().DeclaredMethods.Single(i => i.Name == nameof(IncrementCounter));

    private readonly ILifetime _baseSingletonLifetime;
    private readonly ICounter _counter;

    // Stores the base lifetime and the instance counter
    public MySingletonLifetime(ILifetime baseSingletonLifetime, ICounter counter)
    {
        _baseSingletonLifetime = baseSingletonLifetime;
        _counter = counter;
    }

    public Expression Build(Expression expression, IBuildContext buildContext)
    {
        // Builds expression using base lifetime
        expression = _baseSingletonLifetime.Build(expression, buildContext);

        // Defines `this` variable to store the reference to the current lifetime instance to call internal method 'IncrementCounter'
        var thisVar = Expression.Constant(this);

        // Creates the code block
        return Expression.Block(
            // Adds the expression to call the method 'IncrementCounter' for the current lifetime instance
            Expression.Call(thisVar, IncrementCounterMethodInfo),
            // Returns the expression to create an instance
            expression);
    }

    // Creates the similar lifetime to use with generic instances
    public ILifetime Create() => new MySingletonLifetime(_baseSingletonLifetime.Create(), _counter);

    // Select a container to resolve dependencies using the Singleton lifetime logic
    public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
        _baseSingletonLifetime.SelectResolvingContainer(registrationContainer, resolvingContainer);

    // Disposes the instance of the Singleton lifetime
    public void Dispose() => _baseSingletonLifetime.Dispose();

    // Just counts the number of calls
    internal void IncrementCounter() => _counter.Increment();
}
```



### Configuration class [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConfigurationClass.cs)



``` CSharp
public void Run()
{
    // Create and configure the container
    using var container = Container.Create().Using<Glue>();
    // Resolve an instance
    var instance = container.Resolve<IService>();
}

ic class Glue : IConfiguration
{
public IEnumerable<IDisposable> Apply(IContainer container)
{
    // Bind using full autowiring
    yield return container.Bind<IDependency>().To<Dependency>();
    yield return container.Bind<IService>().To<Service>();
}
}
```



### Change configuration on-the-fly [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ChangeConfigurationOnTheFly.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
{
    // Configure `IService` as Transient
    using (container.Bind<IService>().To<Service>())
    {
        // Resolve instances
        var instance1 = container.Resolve<IService>();
        var instance2 = container.Resolve<IService>();

        // Check that instances are not equal
        instance1.ShouldNotBe(instance2);
    }

    // Reconfigure `IService` as Singleton
    using (container.Bind<IService>().As(Lifetime.Singleton).To<Service>())
    {
        // Resolve the singleton twice
        var instance1 = container.Resolve<IService>();
        var instance2 = container.Resolve<IService>();

        // Check that instances are equal
        instance1.ShouldBe(instance2);
    }
}

```



### Resolve Func [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveFunc.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve function to get instances
    var func = container.Resolve<Func<IService>>();
    
    // Get an instance
    var instance = func();
}

```



### Resolve Lazy [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveLazy.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve the instance of Lazy<IService>
    var lazy = container.Resolve<Lazy<IService>>();

    // Get the instance via Lazy
    var instance = lazy.Value;
}

```



### Resolve ThreadLocal [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveThreadLocal.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve the instance of Lazy<IService>
    var lazy = container.Resolve<ThreadLocal<IService>>();

    // Get the instance via ThreadLocal
    var instance = lazy.Value;
}

```



### Resolve Tuple [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveTuple.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
using (container.Bind<INamedService>().To<NamedService>(
    ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name")))
{
    // Resolve an instance of type Tuple<IService, INamedService>
    var tuple = container.Resolve<Tuple<IService, INamedService>>();
}

```



### Resolve ValueTuple [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveValueTuple.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
using (container.Bind<INamedService>().To<NamedService>(
    ctx => new NamedService(ctx.Container.Inject<IDependency>(), "some name")))
{
    // Resolve an instance of type (IService service, INamedService namedService)
    var valueTuple = container.Resolve<(IService service, INamedService namedService)>();
}

```



### Method Injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/MethodInjection.cs)



``` CSharp
// Create and configure the container using full autowiring
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
using (container.Bind<INamedService>().To<InitializingNamedService>(
    // Select the constructor and inject the dependency
    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
    // Configure the initializing method to invoke after the instance creation and inject the dependencies
    // First one is the value from arguments at index 0
    // Second one as is just dependency injection of type IDependency
    ctx => ctx.It.Initialize((string)ctx.Args[0], ctx.Container.Inject<IDependency>())))
{
    // Resolve the instance using the argument "alpha"
    var instance = container.Resolve<INamedService>("alpha");

    // Check the instance's type
    instance.ShouldBeOfType<InitializingNamedService>();

    // Check the injected dependency
    instance.Name.ShouldBe("alpha");

    // Resolve a function to create an instance
    var func = container.Resolve<Func<string, INamedService>>();

    // Create an instance with the argument "beta"
    var otherInstance = func("beta");

    // Check the injected dependency
    otherInstance.Name.ShouldBe("beta");
}

```



### Property Injection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/PropertyInjection.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
using (container.Bind<INamedService>().To<InitializingNamedService>(
    // Select the constructor and inject the dependency
    ctx => new InitializingNamedService(ctx.Container.Inject<IDependency>()),
    // Select the property to inject after the instance creation and inject the value from arguments at index 0
    ctx => ctx.Container.Inject(ctx.It.Name, (string)ctx.Args[0])))
{
    // Resolve the instance using the argument "alpha"
    var instance = container.Resolve<INamedService>("alpha");

    // Check the instance's type
    instance.ShouldBeOfType<InitializingNamedService>();

    // Check the injected dependency
    instance.Name.ShouldBe("alpha");

    // Resolve a function to create an instance
    var func = container.Resolve<Func<string, INamedService>>();

    // Create an instance with the argument "beta"
    var otherInstance = func("beta");

    // Check the injected dependency
    otherInstance.Name.ShouldBe("beta");
}

```



### Constructor Autowiring [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConstructorAutowiring.cs)



``` CSharp
// Create and configure the container, using full autowiring
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Configure via manual injection
using (container.Bind<IService>().To<Service>(
    // Select the constructor and inject arguments
    ctx => new Service(ctx.Container.Inject<IDependency>(), "some state")))
{
    // Resolve an instance
    var instance = container.Resolve<IService>();
// Check the injected constant
instance.State.ShouldBe("some state");
}

```



### Resolve all appropriate instances as Array [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Array.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind to the implementation #1
using (container.Bind<IService>().Tag(1).To<Service>())
    // Bind to the implementation #2
using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
    // Bind to the implementation #3
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve all appropriate instances
    var instances = container.Resolve<IService[]>();
}

```



### Resolve all appropriate instances as IAsyncEnumerable [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsyncEnumerables.cs)



``` CSharp
// Create and configure the container
using var container = Container.CreateCore().Using(CollectionFeature.Default);
using (container.Bind<IDependency>().To<Dependency>())
    // Bind to the implementation #1
using (container.Bind<IService>().Tag(1).To<Service>())
    // Bind to the implementation #2
using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
    // Bind to the implementation #3
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve all appropriate instances
    var instances = container.Resolve<IAsyncEnumerable<IService>>();
    var items = new List<IService>();
    await foreach (var instance in instances) { items.Add(instance); }
    
    // Check the number of resolved instances
    items.Count.ShouldBe(3);

}

```



### Resolve all appropriate instances as ICollection [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Collection.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind to the implementation #1
using (container.Bind<IService>().Tag(1).To<Service>())
    // Bind to the implementation #2
using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
    // Bind to the implementation #3
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve all appropriate instances
    var instances = container.Resolve<ICollection<IService>>();

    // Check the number of resolved instances
    instances.Count.ShouldBe(3);
}

```



### Resolve all appropriate instances as IEnumerable [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Enumerables.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind to the implementation #1
using (container.Bind<IService>().Tag(1).To<Service>())
    // Bind to the implementation #2
using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
    // Bind to the implementation #3
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve all appropriate instances
    var instances = container.Resolve<IEnumerable<IService>>().ToList();

    // Check the number of resolved instances
    instances.Count.ShouldBe(3);
}

```



### Resolve all appropriate instances as IObservable source [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Observable.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind to the implementation #1
using (container.Bind<IService>().Tag(1).To<Service>())
    // Bind to the implementation #2
using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
    // Bind to the implementation #3
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve the source for all appropriate instances
    var instancesSource = container.Resolve<IObservable<IService>>();
}

```



### Resolve Using Arguments [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveWithArgs.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind 'INamedService' to the instance creation and initialization, actually represented as an expression tree
using (container.Bind<INamedService>().To<NamedService>(
    // Select the constructor and inject and inject the value from arguments at index 0
    ctx => new NamedService(ctx.Container.Inject<IDependency>(), (string)ctx.Args[0])))
{
    // Resolve the instance using the argument "alpha"
    var instance = container.Resolve<INamedService>("alpha");

    // Check the instance's type
    instance.ShouldBeOfType<NamedService>();

    // Check the injected dependency
    instance.Name.ShouldBe("alpha");

    // Resolve a function to create an instance
    var func = container.Resolve<Func<string, INamedService>>();

    // Create an instance with the argument "beta"
    var otherInstance = func("beta");

    // Check the injected dependency
    otherInstance.Name.ShouldBe("beta");
}

```



### Resolve all appropriate instances as ISet [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Set.cs)



``` CSharp
// Create and configure the container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind to the implementation #1
using (container.Bind<IService>().Tag(1).To<Service>())
    // Bind to the implementation #2
using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
    // Bind to the implementation #3
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve all appropriate instances
    var instances = container.Resolve<ISet<IService>>();

    // Check the number of resolved instances
    instances.Count.ShouldBe(3);
}

```



### Asynchronous resolve [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousResolve.cs)



``` CSharp
// Create the container and configure it
using var container = Container.Create()
    // Bind some dependency
    .Bind<IDependency>().To<Dependency>().ToSelf()
    .Bind<IService>().To<Service>().ToSelf();
// Resolve an instance asynchronously
var instance = await container.Resolve<Task<IService>>();
```



### Asynchronous lightweight resolve [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AsynchronousValueResolve.cs)



``` CSharp
// Create a container
using var container = Container.Create();
using (container.Bind<IDependency>().To<Dependency>())
    // Bind Service
using (container.Bind<IService>().To<Service>())
{
    // Resolve an instance asynchronously via ValueTask
    var instance = await container.Resolve<ValueTask<IService>>();
}

```



### Cyclic Dependency [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CyclicDependency.cs)



``` CSharp
public void Run()
{
    var expectedException = new InvalidOperationException("error");
    var foundCyclicDependency = new Mock<IFoundCyclicDependency>();
    // Throws the exception for reentrancy 128
    foundCyclicDependency.Setup(i => i.Resolve(It.IsAny<Key>(), 128)).Throws(expectedException);

    // Create the container
    using var container = Container.Create();
    using (container.Bind<IFoundCyclicDependency>().To(ctx => foundCyclicDependency.Object))
    // Configure the container, where 1,2,3 are tags to produce cyclic dependencies during a resolving
    using (container.Bind<ILink>().To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1))))
    using (container.Bind<ILink>().Tag(1).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(2))))
    using (container.Bind<ILink>().Tag(2).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(3))))
    using (container.Bind<ILink>().Tag(3).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1))))
    {
        try
        {
            // Resolve the root instance
            container.Resolve<ILink>();
        }
        // Catch the exception about cyclic dependencies at a depth of 128
        catch (InvalidOperationException actualException)
        {
            // Check the exception
            actualException.ShouldBe(expectedException);
        }
    }
}

public interface ILink
{
}

public class Link : ILink
{
    public Link(ILink link) { }
}
```



### Generator [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Generator.cs)



``` CSharp
public void Run()
{
    Func<int, int, (int, int)> valueGetter = (sequential, random) => (sequential, random);

    // Create and configure the container using a configuration class 'Generators'
    using var container = Container.Create().Using<Generators>();
    using (container.Bind<(int, int)>().To(
        // Use a function because of the expression trees have a limitation in syntax
        ctx => valueGetter(
            // The first one is of sequential number generator
            ctx.Container.Inject<int>(GeneratorType.Sequential),
            // The second one is of random number generator
            ctx.Container.Inject<int>(GeneratorType.Random))))
    {
        // Generate sequential numbers
        var sequential1 = container.Resolve<int>(GeneratorType.Sequential.AsTag());
        var sequential2 = container.Resolve<int>(GeneratorType.Sequential.AsTag());

        // Check numbers
        sequential2.ShouldBe(sequential1 + 1);

        // Generate a random number
        var random = container.Resolve<int>(GeneratorType.Random.AsTag());

        // Generate a tuple of numbers
        var setOfValues = container.Resolve<(int, int)>();

        // Check sequential numbers
        setOfValues.Item1.ShouldBe(sequential2 + 1);
    }
}

// Represents tags for generators
public enum GeneratorType
{
    Sequential, Random
}

// Represents IoC configuration
public class Generators: IConfiguration
{
    public IEnumerable<IDisposable> Apply(IContainer container)
    {
        var value = 0;
        // Define function to get sequential integer value
        Func<int> generator = () => Interlocked.Increment(ref value);
        // Bind to this function using the corresponding tag 'Sequential'
        yield return container.Bind<int>().Tag(GeneratorType.Sequential).To(ctx => generator());

        var random = new Random();
        // Define function to get random integer value
        Func<int> randomizer = () => random.Next();
        // Bind to this function using the corresponding tag 'Random'
        yield return container.Bind<int>().Tag(GeneratorType.Random).To(ctx => randomizer());
    }
}
```



### Instant Messenger [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SimpleInstantMessenger.cs)



``` CSharp
public void Run()
{
    var observer = new Mock<IObserver<IMessage>>();

    // Initial message id
    var id = 33;
    Func<int> generator = () => id++;

    // Create a container
    using (var container = Container.Create())
    // Configure the container
    using (container.Bind<int>().Tag("IdGenerator").To(ctx => generator()))
    using (container.Bind(typeof(IInstantMessenger<>)).To(typeof(InstantMessenger<>)))
    using (container.Bind<IMessage>().To<Message>(ctx => new Message(ctx.Container.Inject<int>("IdGenerator"), (string)ctx.Args[0], (string)ctx.Args[1])))
    {
        var instantMessenger = container.Resolve<IInstantMessenger<IMessage>>();
        using (instantMessenger.Subscribe(observer.Object))
        {
            for (var i = 0; i < 10; i++)
            {
                instantMessenger.SendMessage("John", "Hello");
            }
        }
    }

    observer.Verify(i => i.OnNext(It.Is<IMessage>(message => message.Id >= 33 && message.Address == "John" && message.Text == "Hello")), Times.Exactly(10));
}

public interface IInstantMessenger<out T>: IObservable<T>
{
    void SendMessage(string address, string text);
}

public interface IMessage
{
    int Id { get; }

    string Address { get; }

    string Text { get; }
}

public class Message: IMessage
{
    public Message(int id, [NotNull] string address, [NotNull] string text)
    {
        Id = id;
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }

    public int Id { get; }

    public string Address { get; }

    public string Text { get; }
}

public class InstantMessenger<T> : IInstantMessenger<T>
{
    private readonly Func<string, string, T> _createMessage;
    private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

    public InstantMessenger(Func<string, string, T> createMessage) => _createMessage = createMessage;

    public IDisposable Subscribe(IObserver<T> observer)
    {
        _observers.Add(observer);
        return Disposable.Create(() => _observers.Remove(observer));
    }

    public void SendMessage(string address, string text)
        => _observers.ForEach(observer => observer.OnNext(_createMessage(address, text)));
}
```



### Wrapper [![CSharp](https://img.shields.io/badge/C%23-code-blue.svg)](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Wrapper.cs)



``` CSharp
public void Run()
{
    var console = new Mock<IConsole>();
    var clock = new Mock<IClock>();
    var now = new DateTimeOffset(2019, 9, 9, 12, 31, 34, TimeSpan.FromHours(3));
    clock.SetupGet(i => i.Now).Returns(now);

    // Create and configure the root container
    using (var rootContainer = Container.Create("root"))
    using (rootContainer.Bind<IConsole>().To(ctx => console.Object))
    using (rootContainer.Bind<ILogger>().To<Logger>())
    {
        // Create and configure the child container
        using var childContainer = rootContainer.Create("Some child container");
        using (childContainer.Bind<IConsole>().To(ctx => console.Object))
        using (childContainer.Bind<IClock>().To(ctx => clock.Object))
        // Bind 'ILogger' to the instance creation, actually represented as an expression tree
        using (childContainer.Bind<ILogger>().To<TimeLogger>(
            // Inject the base logger from the parent container and the clock from the current container
            ctx => new TimeLogger(ctx.Container.Parent.Inject<ILogger>(), ctx.Container.Inject<IClock>())))
        {
            // Create a logger
            var logger = childContainer.Resolve<ILogger>();

            // Log the message
            logger.Log("Hello");
        }
    }

    // Check the console output
    console.Verify(i => i.WriteLine($"{now}: Hello"));
}

public interface IConsole
{
    // Writes a text
    void WriteLine(string text);
}

public interface ILogger
{
    // Logs a message
    void Log(string message);
}

public interface IClock
{
    DateTimeOffset Now { get; }
}

public class Logger : ILogger
{
    private readonly IConsole _console;

    // Stores console to field
    public Logger(IConsole console) => _console = console;

    // Logs a message to console
    public void Log(string message) => _console.WriteLine(message);
}

public class TimeLogger: ILogger
{
    private readonly ILogger _baseLogger;
    private readonly IClock _clock;

    public TimeLogger(ILogger baseLogger, IClock clock)
    {
        _baseLogger = baseLogger;
        _clock = clock;
    }

    // Adds current time before a message and writes it to console
    public void Log(string message) => _baseLogger.Log($"{_clock.Now}: {message}");
}
```



