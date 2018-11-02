## IoC.Container provides the following benefits and features

### [Flexible Binding](#binding)

  - [Auto-wiring](#auto-wiring-)
  - [Compile-time verification](#manual-auto-wiring-)
  - [Generic types bindings](#generics-) with [simple types mapping-](#generic-auto-wiring-)
  - [Named/tagged dependencies](#tags-)
  - [Containers hierarchy](#child-container-)
  - [Bindings via text metadata](#configuration-via-a-text-metadata-)
  - [Customizable aspect oriented autowiring](#aspect-oriented-auto-wiring-)
  - Easy extensible set of lifetimes
    - [Singleton](#singleton-lifetime-) with [auto-disposing](#auto-dispose-singleton-during-containers-dispose-)
    - [Singleton per container](#container-singleton-lifetime-)
    - [Singleton per scope](#scope-singleton-lifetime-)
  - Binding to
    - [Several Contracts](#several-contracts-)
    - [Constant](#constant-), [factory](#func-), [factory with arguments](#func-with-arguments-)
  - Supports [validation](#validation)

### [Powerful Injection](#injection)

  - [Сonstructors injection](#constructor-auto-wiring-), [methods injection](#method-injection-) and [properties injection-](#property-injection)
  - Injection of [Func](#resolve-func-), [Lazy](#resolve-lazy-), [ThreadLocal](#resolve-threadlocal-), [Tuple](#resolve-tuple-) and [ValueTuple](#resolve-valuetuple-)
  - Injection of [IEnumerable](#resolve-all-appropriate-instances-as-ienumerable-), [Array](#resolve-all-appropriate-instances-as-array-), [ICollection](#resolve-all-appropriate-instances-as-icollection-), [ISet](#resolve-all-appropriate-instances-as-iset-) or even via [IObservable](#resolve-all-appropriate-instances-as-iobservable-source-)
  - Detailed errors information

### [Incredible Performance](#why-this-one)

  - One of the fastest, almost as fast as operator `new`
  - Uses [expression trees](https://docs.microsoft.com/en-us/dotnet/csharp/expression-trees) to produce the [effective injection code](#struct-) without any superfluous operations like a `boxing`, `unboxing` or `cast`
  - Minimizes the memory traffic

### [Fully Customizable](#customization)

  - [Custom containers](#custom-child-container-)
  - [Custom lifetimes](#custom-lifetime-)
  - [Replacing predefined lifetimes](#replace-lifetime-)
  - [Custom builders](#custom-builder-)
  - [Interceptors](#interception-)

### [Multithreading-Ready](#multithreading)

  - Thread-safe
  - [Asynchronous resolving](#asynchronous-resolve-)
  - [Lightweight asynchronous resolving](#asynchronous-lightweight-resolve-)

### [Design Aspects](#design)

  - Allows not to change the design of own code to follow [Inversion of Control](https://martinfowler.com/articles/injection.html) pattern
  - Aggregates features into dedicated [classes](#configuration-class-)
  - [Modifiable on-the-fly](#change-configuration-on-the-fly-)
  - Has no any additional dependencies
  - [Embedding packages](#nuget-packages)

### Easy Integration

  - [ASP.NET Core](#aspnet-core)
  - [Windows Presentation Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WpfApp)
  - [Universal Windows Platform](https://github.com/DevTeam/IoCContainer/blob/master/Samples/UwpApp)
  - [Windows Communication Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WcfServiceLibrary)
  - [Entity Framework Core](https://github.com/DevTeam/IoCContainer/tree/master/Samples/EntityFrameworkCore)

### Supported Platforms

  - .NET 4.0+
  - [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/) 1.0+
  - [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) 1.0+
  - [UWP](https://docs.microsoft.com/en-us/windows/uwp/index) 10+

