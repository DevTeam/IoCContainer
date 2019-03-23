## IoC.Container provides the following benefits and features

### [Flexible Bindings](#binding)

  - [Autowiring](#autowiring-)
  - [Aspect oriented autowiring](#aspect-oriented-autowiring-)
  - [Generic types bindings](#generics-) and [easy generic types mapping](#generic-autowiring-)
  - [Named/tagged dependencies](#tags-)
  - [Containers hierarchy](#child-container-)
  - [Compile-time verification](#manual-autowiring-)
  - [Bindings via text metadata](#configuration-via-a-text-metadata-)  
  - Easy extensible set of lifetimes
    - [Singleton](#singleton-lifetime-) with [auto-disposing](#auto-dispose-singleton-during-containers-dispose-)
    - [Singleton per container](#container-singleton-lifetime-)
    - [Singleton per scope](#scope-singleton-lifetime-)
  - Binding to
    - [Several Contracts](#several-contracts-)
    - [Constant](#constant-), [factory](#func-), [factory with arguments](#func-with-arguments-)
  - Supports [validation](#validation)

### [Powerful dependency injection](#injection)

  - Through
    - [Сonstructors](#constructor-autowiring-)
	- [Methods](#method-injection-)
	- [Properties](#property-injection)
	- Fields
  - Injection of composed dependencies
    - [Func](#resolve-func-)
	- [Lazy](#resolve-lazy-)
	- [ThreadLocal](#resolve-threadlocal-)
	- [Tuple](#resolve-tuple-)
	- [ValueTuple](#resolve-valuetuple-)
    - [IAsyncEnumerable](#resolve-all-appropriate-instances-as-iasyncenumerable-)
	- [IEnumerable](#resolve-all-appropriate-instances-as-ienumerable-)
	- [Array](#resolve-all-appropriate-instances-as-array-)
	- [ICollection](#resolve-all-appropriate-instances-as-icollection-)
	- [ISet](#resolve-all-appropriate-instances-as-iset-)
	- [IObservable](#resolve-all-appropriate-instances-as-iobservable-source-)
  - Detailed diagnostics information

### [Incredible Performance](#why-this-one)

  - One of the fastest, almost as fast as operator `new`
  - Uses [expression trees](https://docs.microsoft.com/en-us/dotnet/csharp/expression-trees) to produce the [effective injection code](#struct-) without any superfluous operations like a `boxing`, `unboxing` or `cast`
  - Minimizes memory traffic

### [Fully Customizable](#customization)

  - [Custom containers](#custom-child-container-)
  - [Custom lifetimes](#custom-lifetime-)
  - [Replacing predefined lifetimes](#replace-lifetime-)
  - [Custom builders](#custom-builder-)
  - [Invocations' interceptors](#interception-)

### [Asynchronous](#multithreading)
  
  - [Asynchronous resolving](#asynchronous-resolve-)
  - [Lightweight asynchronous resolving](#asynchronous-lightweight-resolve-)
  - [Asynchronous enumerations](#resolve-all-appropriate-instances-as-iasyncenumerable-)	

### [Design Aspects](#design)

  - Allows to preserve an original design of code and to minimize the impact of the [Inversion of Control](https://martinfowler.com/articles/injection.html) approach
  - Aggregates features into dedicated [configuration classes](#configuration-class-)
  - [Modifiable on-the-fly](#change-configuration-on-the-fly-)
  - Free from external dependencies like other frameworks or assemblies
  - [Embedding packages](#nuget-packages) allow to use all these features without referencing on additional assemblies

### Easy Integration

  - [ASP.NET Core](#aspnet-core)
  - [Xamarin](https://github.com/DevTeam/IoCContainer/blob/master/Samples/XamarinXaml)
  - [Windows Presentation Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WpfApp)
  - [.NET core Windows Presentation Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WpfAppNetCore) 
  - [Universal Windows Platform](https://github.com/DevTeam/IoCContainer/blob/master/Samples/UwpApp)
  - [Windows Communication Foundation](https://github.com/DevTeam/IoCContainer/blob/master/Samples/WcfServiceLibrary)
  - [Entity Framework](https://github.com/DevTeam/IoCContainer/tree/master/Samples/EntityFrameworkCore)

### Supported Platforms

  - .NET 4.0+
  - [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/) 1.0+
  - [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) 1.0+
  - [UWP](https://docs.microsoft.com/en-us/windows/uwp/index) 10+

