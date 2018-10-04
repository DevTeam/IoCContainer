## IoC.Container provides the following benefits and features

### [Flexible Binding](https://github.com/DevTeam/IoCContainer#binding)

  - [Auto-wiring](https://github.com/DevTeam/IoCContainer#auto-wiring)
  - [Compile-time verification](https://github.com/DevTeam/IoCContainer#manual-auto-wiring)
  - [Generic types bindings](https://github.com/DevTeam/IoCContainer#generics) with [simple types mapping](https://github.com/DevTeam/IoCContainer#generic-auto-wiring)
  - [Named/tagged dependencies](https://github.com/DevTeam/IoCContainer#tags)
  - [Containers hierarchy](https://github.com/DevTeam/IoCContainer#child-container)
  - [Bindings via text metadata](https://github.com/DevTeam/IoCContainer#configuration-via-a-text-metadata)
  - [Customizable aspect oriented autowiring](https://github.com/DevTeam/IoCContainer#aspect-oriented-autowiring)
  - Easy expandable set of lifetimes
    - [Singleton](https://github.com/DevTeam/IoCContainer#singleton-lifetime) with [auto-disposing](https://github.com/DevTeam/IoCContainer#auto-dispose-singleton-during-containers-dispose)
    - [Singleton per container](https://github.com/DevTeam/IoCContainer#container-singleton-lifetime)
    - [Singleton per scope](https://github.com/DevTeam/IoCContainer#scope-singleton-lifetime)
  - Binding to
    - [Several Contracts](https://github.com/DevTeam/IoCContainer#several-contracts)
    - [Constant](https://github.com/DevTeam/IoCContainer#constant), [factory](https://github.com/DevTeam/IoCContainer#func), [factory with arguments](https://github.com/DevTeam/IoCContainer#func-with-arguments)
  - Supports [validation](https://github.com/DevTeam/IoCContainer#validation)

### [Powerful Injection](https://github.com/DevTeam/IoCContainer#injection)

  - [Сonstructors injection](https://github.com/DevTeam/IoCContainer#constructor-auto-wiring), [methods injection](https://github.com/DevTeam/IoCContainer#method-injection) and [properties injection](https://github.com/DevTeam/IoCContainer#property-injection)
  - Injection of [Func](https://github.com/DevTeam/IoCContainer#resolve-func), [Lazy](https://github.com/DevTeam/IoCContainer#resolve-lazy), [Tuple](https://github.com/DevTeam/IoCContainer#resolve-tuple) and [ValueTuple](https://github.com/DevTeam/IoCContainer#resolve-valuetuple)
  - Injection of [IEnumerable](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-ienumerable), [ICollection](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-icollection), [ISet](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-iset) or even via [IObservable](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-iobservable-source)
  - Detailed errors information

### [Incredible Performance](https://github.com/DevTeam/IoCContainer#why-this-one)

  - One of the fastest, almost as fast as operator `new`
  - Uses [expression trees](https://docs.microsoft.com/en-us/dotnet/csharp/expression-trees) to produce the [effective injection code](https://github.com/DevTeam/IoCContainer#struct) without any superfluous operations like a `boxing`, `unboxing` or `cast`
  - Minimizes the memory traffic

### [Fully Customizable](https://github.com/DevTeam/IoCContainer#customization)

  - [Custom containers](https://github.com/DevTeam/IoCContainer#custom-child-container)
  - [Custom lifetimes](https://github.com/DevTeam/IoCContainer#custom-lifetime)
  - [Replacing predefined lifetimes](https://github.com/DevTeam/IoCContainer#replace-lifetime)
  - [Custom builders](https://github.com/DevTeam/IoCContainer#custom-builder)
  - [Interceptors](https://github.com/DevTeam/IoCContainer#interception)

### [Multithreading-Ready](https://github.com/DevTeam/IoCContainer#multithreading)

  - Thread-safe
  - [Asynchronous resolving](https://github.com/DevTeam/IoCContainer#asynchronous-resolve)
  - [Lightweight asynchronous resolving](https://github.com/DevTeam/IoCContainer#asynchronous-lightweight-resolve)

### [Design Aspects](https://github.com/DevTeam/IoCContainer#design)

  - Allows not to change the design of own code to follow [Inversion of Control](https://martinfowler.com/articles/injection.html) pattern
  - Aggregates features into dedicated [classes](https://github.com/DevTeam/IoCContainer#configuration-class)
  - [Modifiable on-the-fly](https://github.com/DevTeam/IoCContainer#change-configuration-on-the-fly)
  - Has no any additional dependencies
  - Supports the embedding directly to your code by [embedding-in-code packages](https://github.com/DevTeam/IoCContainer#nuget-packages)

### Easy Integration

  - [ASP.NET Core](https://github.com/DevTeam/IoCContainer#aspnet-core)
  - [Windows Presentation Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WpfApp)
  - [Universal Windows Platform](https://github.com/DevTeam/IoCContainer/blob/master/Samples/UwpApp)
  - [Windows Communication Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WcfServiceLibrary)
  - [Entity Framework Core](https://github.com/DevTeam/IoCContainer/tree/master/Samples/EntityFrameworkCore)

### Supported Platforms

  - .NET 4.0+
  - [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/) 1.0+
  - [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) 1.0+
  - [UWP](https://docs.microsoft.com/en-us/windows/uwp/index) 10+

