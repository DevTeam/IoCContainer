# Simple, powerful and fast IoC container

[![NuGet Version and Downloads count](https://buildstats.info/nuget/IoC.Container)](https://www.nuget.org/packages/IoC.Container) [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

Key features:
  - One of the fastest
  - Produces minimal memory trafic
  - Auto-wiring without any changes in a design of code
  - Auto-wiring using a text metadata
  - Supports custom factories/lifetimes/containers
  - Reconfigurable on-the-fly
  - Supports concurrent and asynchronous resolving
  - Has no additional dependencies

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

### Add a [package reference](https://www.nuget.org/packages/IoC.Container)

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

See the [results](http://tcavs2015.cloudapp.net/httpAuth/app/rest/builds/buildType:DevTeam_IoCContainer_Build,status:SUCCESS/artifacts/content/REPORT.html) of the [comparison tests](https://github.com/DevTeam/IoCContainer/blob/master/IoC.Tests/ComparisonTests.cs) for some popular IoC containers like Castle Windsor, Autofac, Unity, Ninject ...

## Usage Scenarios

* [Resolve from Container](#resolve-from-container)
* [Generics](#generics)
* [Several Contracts](#several-contracts)
* [Asynchronous Resolve](#asynchronous-resolve)
* [Resolve All Possible](#resolve-all-possible)
* [Func](#func)
* [Singletone](#singletone)
* [Tags](#tags)
* [Factory](#factory)
* [Factory Method](#factory-method)
* [Dependency Tag](#dependency-tag)
* [Method Injection](#method-injection)
* [Property Injection](#property-injection)
* [Factory Method With Arguments](#factory-method-with-arguments)
* [Resolve Using Arguments](#resolve-using-arguments)
* [Auto dispose singletone during container's dispose](#auto-dispose-singletone-during-containers-dispose)
* [Configure Via Configuration class](#configure-via-configuration-class)
* [Configure Via Text](#configure-via-text)
* [Change configuration on-the-fly](#change-configuration-on-the-fly)
* [Custom Child Container](#custom-child-container)
* [Custom Lifetime](#custom-lifetime)
* [Replace Lifetime](#replace-lifetime)
* [Generator](#generator)
* [Instant Messenger](#instant-messenger)
* [Wrapper](#wrapper)
* [Cyclic Dependence](#cyclic-dependence)
* [Interfaces and classes for samples](#interfaces-and-classes-for-samples)

### Resolve from Container

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve the instance
    var instance = container.Get<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveFromContainer.cs)

### Generics

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind(typeof(IService<>)).To(typeof(Service<>)))
{
    // Resolve the generic instance
    var instance = container.Get<IService<int>>();

    instance.ShouldBeOfType<Service<int>>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveGeneric.cs)

### Several Contracts

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<Service, IService, IAnotherService>().To<Service>())
{
    // Resolve instances
    var instance1 = container.Get<IService>();
    var instance2 = container.Get<IAnotherService>();

    instance1.ShouldBeOfType<Service>();
    instance2.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveViaSeveralContracts.cs)

### Asynchronous Resolve

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve the instance asynchronously
    var instance = await container.AsyncGet<IService>(TaskScheduler.Default);

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveAsync.cs)

### Resolve All Possible

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Tag(1).To<Service>())
using (container.Bind<IService>().Tag(2).To<Service>())
using (container.Bind<IService>().Tag(3).To<Service>())
{
    // Resolve all possible instances
    var instances = container.Get<IEnumerable<IService>>().ToList();

    instances.Count.ShouldBe(3);
    instances.ForEach(instance => instance.ShouldBeOfType<Service>());
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveEnumerable.cs)

### Func

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().To<Service>())
{
    // Resolve Func
    var func = container.Get<Func<IService>>();
    // Get the instance
    var instance = func();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveViaFunc.cs)

### Singletone

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Lifetime(Lifetime.Singletone).To<Service>())
{
    // Resolve the instance twice
    var instance1 = container.Get<IService>();
    var instance2 = container.Get<IService>();

    instance1.ShouldBe(instance2);
}

// Other lifetimes are:
// Transient - A new instance each time
// Container - Singletone per container
// Resolve - Singletone per resolve
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveSingletone.cs)

### Tags

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<IService>().Tag(10).Tag().Tag("abc").To<Service>())
{
    // Resolve instances using tags
    var instance1 = container.Tag("abc").Get<IService>();
    var instance2 = container.Tag(10).Get<IService>();

    // Resolve the instance using the empty tag
    var instance3 = container.Get<IService>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveUsingTags.cs)

### Factory

``` CSharp
public void Run()
{
    // Create the container
    using (var container = Container.Create())
    // Configure the container
    using (container.Bind<IService>().To(new Factory()))
    {
        // Resolve the instance
        var instance = container.Get<IService>();
        instance.ShouldBeOfType<Service>();
    }
}

public class Factory: IFactory
{
    public object Create(ResolvingContext context)
    {
        return new Service(new Dependency());
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveViaFactory.cs)

### Factory Method

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IService>().To(() => new Service(new Dependency())))
// Each dependency cound be resolve also using a resolving context
// using (container.Bind<IService>().To(ctx => new Service(ctx.ResolvingContainer.Get<IDependency>())))
{
    // Resolve the instance
    var instance = container.Get<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveViaFactoryMethod.cs)

### Dependency Tag

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().Tag("MyDep").To<Dependency>())
using (container.Bind<IService>().To<Service>(Has.Ref("dependency", "MyDep")))
{
    // Resolve the instance
    var instance = container.Get<IService>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/DependencyTag.cs)

### Method Injection

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To<InitializingNamedService>(Has.Method("Initialize", Has.Arg("name", 0))))
{
    // Resolve the instance "alpha"
    var instance = container.Get<INamedService>("alpha");

    instance.ShouldBeOfType<InitializingNamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var func = container.Get<Func<string, INamedService>>();
    var otherInstance = func("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/MethodInjection.cs)

### Property Injection

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To<InitializingNamedService>(Has.Property("Name", 0)))
{
    // Resolve the instance "alpha"
    var instance = container.Get<INamedService>("alpha");

    instance.ShouldBeOfType<InitializingNamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var func = container.Get<Func<string, INamedService>>();
    var otherInstance = func("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/PropertyInjection.cs)

### Factory Method With Arguments

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To(ctx => new NamedService(ctx.ResolvingContainer.Get<IDependency>(), (string)ctx.Args[0])))
{
    // Resolve the instance "alpha"
    var instance = container.Get<INamedService>("alpha");

    instance.ShouldBeOfType<NamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var func = container.Get<Func<string, INamedService>>();
    var otherInstance = func("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveViaFactoryMethodWithArgs.cs)

### Resolve Using Arguments

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
using (container.Bind<INamedService>().To<NamedService>(Has.Arg("name", 0)))
{
    // Resolve the instance "alpha"
    var instance = container.Get<INamedService>("alpha");

    instance.ShouldBeOfType<NamedService>();
    instance.Name.ShouldBe("alpha");

    // Resolve the instance "beta"
    var func = container.Get<Func<string, INamedService>>();
    var otherInstance = func("beta");
    otherInstance.Name.ShouldBe("beta");
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ResolveWithArgs.cs)

### Auto dispose singletone during container's dispose

``` CSharp
var disposableService = new Mock<IDisposableService>();

// Create the container
using (var container = Container.Create())
{
    // Configure the container
    container.Bind<IService>().Lifetime(Lifetime.Singletone).To(() => disposableService.Object).ToSelf();

    // Resolve instances
    var instance1 = container.Get<IService>();
    var instance2 = container.Get<IService>();

    instance1.ShouldBe(instance2);
}

disposableService.Verify(i => i.Dispose(), Times.Once);
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/AutoDisposeSingletoneDuringContainersDispose.cs)

### Configure Via Configuration class

``` CSharp
public void Run()
{
    // Create the container and configure it
    using (var container = Container.Create().Using<Glue>())
    {
        // Resolve the instance
        var instance = container.Get<IService>();

        instance.ShouldBeOfType<Service>();
    }
}

public class Glue : IConfiguration
{
    public IEnumerable<IDisposable> Apply(IContainer container)
    {
        yield return container.Bind<IDependency>().To<Dependency>();
        yield return container.Bind<IService>().To<Service>();
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConfigureViaConfigurationClass.cs)

### Configure Via Text

``` CSharp
// Create the container and configure from the metadata string
using (var container = Container.Create().Using(
    "ref IoC.Tests;" +
    "using IoC.Tests.UsageScenarios;" +
    "Bind<IDependency>().To<Dependency>();" +
    "Bind<IService>().To<Service>();"))
{
    // Resolve the instance
    var instance = container.Get<IService>();

    instance.ShouldBeOfType<Service>();
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ConfigureViaText.cs)

### Change configuration on-the-fly

``` CSharp
// Create the container
using (var container = Container.Create())
// Configure the container
using (container.Bind<IDependency>().To<Dependency>())
{
    // Configure the container using the Transient lifetime
    using (container.Bind<IService>().To<Service>())
    {
        // Resolve instances
        var instance1 = container.Get<IService>();
        var instance2 = container.Get<IService>();

        instance1.ShouldNotBe(instance2);
    }

    // Reconfigure the container using the Singletone lifetime
    using (container.Bind<IService>().Lifetime(Lifetime.Singletone).To<Service>())
    {
        // Resolve the instance twice
        var instance1 = container.Get<IService>();
        var instance2 = container.Get<IService>();

        instance1.ShouldBe(instance2);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ChangeConfigurationOnTheFly.cs)

### Custom Child Container

``` CSharp
public void Run()
{
    // Create the container
    using (var container = Container.Create())
    // Configure current container to use a custom container's class to create a child container
    using (container.Bind<IContainer>().Tag(Scope.Child).To<MyContainer>())
    // Create our child container
    using (var childContainer = container.CreateChild("abc"))
    // Configure the child container
    using (childContainer.Bind<IDependency>().To<Dependency>())
    using (childContainer.Bind<IService>().To<Service>())
    {
        // Resolve the instance
        var instance = childContainer.Get<IService>();

        childContainer.ShouldBeOfType<MyContainer>();
        instance.ShouldBeOfType<Service>();
    }
}

public class MyContainer: IContainer
{
    public MyContainer(IContainer currentContainer)
    {
        Parent = currentContainer;
    }

    public IContainer Parent { get; }

    public bool TryRegister(IEnumerable<Key> keys, IFactory factory, ILifetime lifetime, out IDisposable registrationToken)
    {
        // Add your logic here or just ...
        return Parent.TryRegister(keys, factory, lifetime, out registrationToken);
    }

    public bool TryGetResolver(Key key, out IResolver resolver)
    {
        // Add your logic here or just ...
        return Parent.TryGetResolver(key, out resolver);
    }

    public void Dispose() { }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomChildContainer.cs)

### Custom Lifetime

``` CSharp
public void Run()
{
    // Create the container
    using (var container = Container.Create())
    // Configure the container
    using (container.Bind<IDependency>().To<Dependency>())
    using (container.Bind<IService>().Lifetime(new MyTransientLifetime()).To<Service>())
    {
        // Resolve the instance
        var instance = container.Get<IService>();

        instance.ShouldBeOfType<Service>();
    }
}

public class MyTransientLifetime : ILifetime
{
    public object GetOrCreate(ResolvingContext context, IFactory factory)
    {
        return factory.Create(context);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CustomLifetime.cs)

### Replace Lifetime

``` CSharp
public void Run()
{
    var counter = new Mock<ICounter>();

    // Create the container
    using (var container = Container.Create())
    using (container.Bind<ICounter>().To(() => counter.Object))
    // Replace the Singletone lifetime
    using (container.Bind<ILifetime>().Tag(Lifetime.Singletone).To<MySingletoneLifetime>(Has.Ref("baseSingletineLifetime", Lifetime.Singletone, Scope.Parent)))
    // Configure the container
    using (container.Bind<IDependency>().To<Dependency>())
    using (container.Bind<IService>().Lifetime(Lifetime.Singletone).To<Service>())
    {
        // Resolve the instance twice using the wrapped Singletine lifetime
        var instance1 = container.Get<IService>();
        var instance2 = container.Get<IService>();

        instance1.ShouldBe(instance2);
    }

    counter.Verify(i => i.Increment(), Times.Exactly(2));
}

public interface ICounter
{
    void Increment();
}

public class MySingletoneLifetime : ILifetime
{
    private readonly ILifetime _baseSingletoneLifetime;
    private readonly ICounter _counter;

    public MySingletoneLifetime(ILifetime baseSingletoneLifetime, ICounter counter)
    {
        _baseSingletoneLifetime = baseSingletoneLifetime;
        _counter = counter;
    }

    public object GetOrCreate(ResolvingContext context, IFactory factory)
    {
        _counter.Increment();
        return _baseSingletoneLifetime.GetOrCreate(context, factory);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/ReplaceLifetime.cs)

### Generator

``` CSharp
public void Run()
{
    // Create the container and configure it using configuration class
    using (var container = Container.Create().Using<Generators>())
    {
        // Generate numbers
        var sequential1 = container.Tag(GeneratorType.Sequential).Get<int>();
        var sequential2 = container.Tag(GeneratorType.Sequential).Get<int>();

        sequential2.ShouldBeGreaterThan(sequential1);
    }
}

public enum GeneratorType
{
    Sequential, Random
}

public class Generators: IConfiguration
{
    public IEnumerable<IDisposable> Apply(IContainer container)
    {
        var value = 0;
        // The "value++" operation is tread safe
        yield return container.Bind<int>().Tag(GeneratorType.Sequential).To(() => value++);

        var random = new Random();
        yield return container.Bind<int>().Tag(GeneratorType.Random).To(() => random.Next());
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Generator.cs)

### Instant Messenger

``` CSharp
public void Run()
{
    var observer = new Mock<IObserver<IMessage>>();

    // Initial message id
    var id = 33;

    // Create the container
    using (var container = Container.Create())
    // Configure the container
    using (container.Bind<int>().Tag("IdGenerator").To(() => id++))
    using (container.Bind(typeof(IInstantMessenger<>)).To(typeof(InstantMessenger<>)))
    using (container.Bind<IMessage>().To<Message>(Has.Arg("address", 0), Has.Arg("text", 1), Has.Ref("id", "IdGenerator")))
    {
        var instantMessenger = container.Get<IInstantMessenger<IMessage>>();
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

    public InstantMessenger(Func<string, string, T> createMessage)
    {
        _createMessage = createMessage;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        _observers.Add(observer);
        return Disposable.Create(() => _observers.Remove(observer));
    }

    public void SendMessage(string address, string text)
    {
        _observers.ForEach(observer => observer.OnNext(_createMessage(address, text)));
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/SimpleInstantMessenger.cs)

### Wrapper

``` CSharp
public void Run()
{
    var console = new Mock<IConsole>();

    // Create the base container
    using (var baseContainer = Container.Create("base"))
    // Configure the base container for base logger
    using (baseContainer.Bind<IConsole>().To(ctx => console.Object))
    using (baseContainer.Bind<ILogger>().To(typeof(Logger)))
    {
        // Configure some new container
        using (var childContainer = baseContainer.CreateChild("child"))
        // And add some console
        using (childContainer.Bind<IConsole>().To(ctx => console.Object))
        // And add logger's wrapper, specifing that resolving of the "logger" dependency should be done from the parent container
        using (childContainer.Bind<ILogger>().To(typeof(TimeLogger), Has.Ref("logger", Scope.Parent)))
        {
            var logger = childContainer.Get<ILogger>();

            // Log message
            logger.Log("Hello");
        }
    }

    // Check the console output
    console.Verify(i => i.WriteLine(It.IsRegex(".+: Hello")));
}

public interface IConsole
{
    void WriteLine(string test);
}

public interface ILogger
{
    void Log(string message);
}

public class Logger : ILogger
{
    private readonly IConsole _console;

    public Logger(IConsole console)
    {
        _console = console;
    }

    public void Log(string message)
    {
        _console.WriteLine(message);
    }
}

public class TimeLogger: ILogger
{
    private readonly ILogger _baseLogger;

    public TimeLogger(ILogger baseLogger)
    {
        _baseLogger = baseLogger;
    }

    public void Log(string message)
    {
        _baseLogger.Log(DateTimeOffset.Now + ": " + message);
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Wrapper.cs)

### Cyclic Dependence

``` CSharp
public void Run()
{
    var expectedException = new InvalidOperationException("error");
    var issueResolver = new Mock<IIssueResolver>();
    issueResolver.Setup(i => i.CyclicDependenceDetected(It.IsAny<ResolvingContext>(), It.IsAny<Type>(), 32)).Throws(expectedException);

    // Create the container
    using (var container = Container.Create())
    // Configure the container
    using (container.Bind<IIssueResolver>().To(ctx => issueResolver.Object))
    using (container.Bind<ILink>().To(typeof(Link), Has.Ref("link", 1)))
    using (container.Bind<ILink>().Tag(1).To(typeof(Link), Has.Ref("link", 2)))
    using (container.Bind<ILink>().Tag(2).To(typeof(Link), Has.Ref("link", 3)))
    using (container.Bind<ILink>().Tag(3).To(typeof(Link), Has.Ref("link", 1)))
    {
        try
        {
            // Resolve the first link
            container.Get<ILink>();
        }
        catch (InvalidOperationException actualException)
        {
            actualException.ShouldBe(expectedException);
        }
    }

    issueResolver.Verify(i => i.CyclicDependenceDetected(It.IsAny<ResolvingContext>(), It.IsAny<Type>(), 32));
}

public interface ILink
{
}

public class Link : ILink
{
    // ReSharper disable once UnusedParameter.Local
    public Link(ILink link)
    {
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/CyclicDependence.cs)

### Interfaces and classes for samples

``` CSharp
public interface IDependency { }

public class Dependency : IDependency { }

public interface IService { }

public interface IAnotherService { }

public interface IDisposableService : IService, IDisposable { }

public class Service : IService, IAnotherService
{
    public Service(IDependency dependency) { }
}

// Generic
public interface IService<T> { }

public class Service<T> : IService<T>
{
    public Service(IDependency dependency) { }
}

// Named
public interface INamedService
{
    string Name { get; }
}

public class NamedService : INamedService
{
    public NamedService(IDependency dependency, string name)
    {
        Name = name;
    }

    public string Name { get; }
}

// Property and Method injection
public class InitializingNamedService : INamedService
{
    public InitializingNamedService(IDependency dependency)
    {
    }

    public string Name { get; set; }

    public void Initialize(string name, IDependency otherDependency)
    {
        Name = name;
    }
}
```
[C#](https://raw.githubusercontent.com/DevTeam/IoCContainer/master/IoC.Tests/UsageScenarios/Models.cs)

## The build state

[<img src="http://tcavs2015.cloudapp.net/app/rest/builds/buildType:(id:DevTeam_IoCContainer_Build)/statusIcon"/>](http://tcavs2015.cloudapp.net/viewType.html?buildTypeId=DevTeam_IoCContainer_Build&guest=1)
