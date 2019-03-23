## IoC.Container provides the following benefits and features

### [Flexible Bindings](https://github.com/DevTeam/IoCContainer#binding)

  - [Autowiring](https://github.com/DevTeam/IoCContainer#autowiring-)
  - [Aspect oriented autowiring](https://github.com/DevTeam/IoCContainer#aspect-oriented-autowiring-)
  - [Generic types bindings](https://github.com/DevTeam/IoCContainer#generics-) and [easy generic types mapping](https://github.com/DevTeam/IoCContainer#generic-autowiring-)
  - [Named/tagged dependencies](https://github.com/DevTeam/IoCContainer#tags-)
  - [Containers hierarchy](https://github.com/DevTeam/IoCContainer#child-container-)
  - [Compile-time verification](https://github.com/DevTeam/IoCContainer#manual-autowiring-)
  - [Bindings via text metadata](https://github.com/DevTeam/IoCContainer#configuration-via-a-text-metadata-)  
  - Easy extensible set of lifetimes
    - [Singleton](https://github.com/DevTeam/IoCContainer#singleton-lifetime-) with [auto-disposing](https://github.com/DevTeam/IoCContainer#auto-dispose-singleton-during-containers-dispose-)
    - [Singleton per container](https://github.com/DevTeam/IoCContainer#container-singleton-lifetime-)
    - [Singleton per scope](https://github.com/DevTeam/IoCContainer#scope-singleton-lifetime-)
  - Binding to
    - [Several Contracts](https://github.com/DevTeam/IoCContainer#several-contracts-)
    - [Constant](https://github.com/DevTeam/IoCContainer#constant-), [factory](https://github.com/DevTeam/IoCContainer#func-), [factory with arguments](https://github.com/DevTeam/IoCContainer#func-with-arguments-)
  - Supports [validation](https://github.com/DevTeam/IoCContainer#validation)

### [Powerful dependency injection](https://github.com/DevTeam/IoCContainer#injection)

  - Through
    - [Сonstructors](https://github.com/DevTeam/IoCContainer#constructor-autowiring-)
	- [Methods](https://github.com/DevTeam/IoCContainer#method-injection-)
	- [Properties](https://github.com/DevTeam/IoCContainer#property-injection)
	- Fields
  - Injection of composed dependencies
    - [Func](https://github.com/DevTeam/IoCContainer#resolve-func-)
	- [Lazy](https://github.com/DevTeam/IoCContainer#resolve-lazy-)
	- [ThreadLocal](https://github.com/DevTeam/IoCContainer#resolve-threadlocal-)
	- [Tuple](https://github.com/DevTeam/IoCContainer#resolve-tuple-)
	- [ValueTuple](https://github.com/DevTeam/IoCContainer#resolve-valuetuple-)
    - [IAsyncEnumerable](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-iasyncenumerable-)
	- [IEnumerable](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-ienumerable-)
	- [Array](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-array-)
	- [ICollection](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-icollection-)
	- [ISet](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-iset-)
	- [IObservable](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-iobservable-source-)
  - Detailed diagnostics information

### [Incredible Performance](https://github.com/DevTeam/IoCContainer#why-this-one)

  - One of the fastest, almost as fast as operator `new`
  - Uses [expression trees](https://docs.microsoft.com/en-us/dotnet/csharp/expression-trees) to produce the [effective injection code](https://github.com/DevTeam/IoCContainer#struct-) without any superfluous operations like a `boxing`, `unboxing` or `cast`
  - Minimizes memory traffic

### [Fully Customizable](https://github.com/DevTeam/IoCContainer#customization)

  - [Custom containers](https://github.com/DevTeam/IoCContainer#custom-child-container-)
  - [Custom lifetimes](https://github.com/DevTeam/IoCContainer#custom-lifetime-)
  - [Replacing predefined lifetimes](https://github.com/DevTeam/IoCContainer#replace-lifetime-)
  - [Custom builders](https://github.com/DevTeam/IoCContainer#custom-builder-)
  - [Invocations' interceptors](https://github.com/DevTeam/IoCContainer#interception-)

### [Asynchronous](https://github.com/DevTeam/IoCContainer#multithreading)
  
  - [Asynchronous resolving](https://github.com/DevTeam/IoCContainer#asynchronous-resolve-)
  - [Lightweight asynchronous resolving](https://github.com/DevTeam/IoCContainer#asynchronous-lightweight-resolve-)
  - [Asynchronous enumerations](https://github.com/DevTeam/IoCContainer#resolve-all-appropriate-instances-as-iasyncenumerable-)	

### [Design Aspects](https://github.com/DevTeam/IoCContainer#design)

  - Allows to preserve an original design of code and to minimize the impact of the [Inversion of Control](https://martinfowler.com/articles/injection.html) approach
  - Aggregates features into dedicated [configuration classes](https://github.com/DevTeam/IoCContainer#configuration-class-)
  - [Modifiable on-the-fly](https://github.com/DevTeam/IoCContainer#change-configuration-on-the-fly-)
  - Free from external dependencies like other frameworks or assemblies
  - [Embedding packages](https://github.com/DevTeam/IoCContainer#nuget-packages) allow to use all these features without referencing on additional assemblies

### Easy Integration

  - [ASP.NET Core](https://github.com/DevTeam/IoCContainer#aspnet-core)
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

