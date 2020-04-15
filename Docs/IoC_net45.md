<a name='assembly'></a>
# IoC

## Contents

- [AspMvcActionAttribute](#T-IoC-AspMvcActionAttribute 'IoC.AspMvcActionAttribute')
- [AspMvcActionSelectorAttribute](#T-IoC-AspMvcActionSelectorAttribute 'IoC.AspMvcActionSelectorAttribute')
- [AspMvcAreaAttribute](#T-IoC-AspMvcAreaAttribute 'IoC.AspMvcAreaAttribute')
- [AspMvcControllerAttribute](#T-IoC-AspMvcControllerAttribute 'IoC.AspMvcControllerAttribute')
- [AspMvcDisplayTemplateAttribute](#T-IoC-AspMvcDisplayTemplateAttribute 'IoC.AspMvcDisplayTemplateAttribute')
- [AspMvcEditorTemplateAttribute](#T-IoC-AspMvcEditorTemplateAttribute 'IoC.AspMvcEditorTemplateAttribute')
- [AspMvcMasterAttribute](#T-IoC-AspMvcMasterAttribute 'IoC.AspMvcMasterAttribute')
- [AspMvcModelTypeAttribute](#T-IoC-AspMvcModelTypeAttribute 'IoC.AspMvcModelTypeAttribute')
- [AspMvcPartialViewAttribute](#T-IoC-AspMvcPartialViewAttribute 'IoC.AspMvcPartialViewAttribute')
- [AspMvcSuppressViewErrorAttribute](#T-IoC-AspMvcSuppressViewErrorAttribute 'IoC.AspMvcSuppressViewErrorAttribute')
- [AspMvcTemplateAttribute](#T-IoC-AspMvcTemplateAttribute 'IoC.AspMvcTemplateAttribute')
- [AspMvcViewAttribute](#T-IoC-AspMvcViewAttribute 'IoC.AspMvcViewAttribute')
- [AspMvcViewComponentAttribute](#T-IoC-AspMvcViewComponentAttribute 'IoC.AspMvcViewComponentAttribute')
- [AspMvcViewComponentViewAttribute](#T-IoC-AspMvcViewComponentViewAttribute 'IoC.AspMvcViewComponentViewAttribute')
- [AspectOrientedAutowiringStrategy](#T-IoC-Core-AspectOrientedAutowiringStrategy 'IoC.Core.AspectOrientedAutowiringStrategy')
  - [TryResolveConstructor()](#M-IoC-Core-AspectOrientedAutowiringStrategy-TryResolveConstructor-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-ConstructorInfo}},IoC-IMethod{System-Reflection-ConstructorInfo}@- 'IoC.Core.AspectOrientedAutowiringStrategy.TryResolveConstructor(System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.ConstructorInfo}},IoC.IMethod{System.Reflection.ConstructorInfo}@)')
  - [TryResolveInitializers()](#M-IoC-Core-AspectOrientedAutowiringStrategy-TryResolveInitializers-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}},System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}}@- 'IoC.Core.AspectOrientedAutowiringStrategy.TryResolveInitializers(System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.MethodInfo}},System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.MethodInfo}}@)')
  - [TryResolveType()](#M-IoC-Core-AspectOrientedAutowiringStrategy-TryResolveType-System-Type,System-Type,System-Type@- 'IoC.Core.AspectOrientedAutowiringStrategy.TryResolveType(System.Type,System.Type,System.Type@)')
- [AspectOrientedMetadata](#T-IoC-Core-AspectOrientedMetadata 'IoC.Core.AspectOrientedMetadata')
  - [TryResolveConstructor()](#M-IoC-Core-AspectOrientedMetadata-TryResolveConstructor-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-ConstructorInfo}},IoC-IMethod{System-Reflection-ConstructorInfo}@- 'IoC.Core.AspectOrientedMetadata.TryResolveConstructor(System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.ConstructorInfo}},IoC.IMethod{System.Reflection.ConstructorInfo}@)')
  - [TryResolveInitializers()](#M-IoC-Core-AspectOrientedMetadata-TryResolveInitializers-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}},System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}}@- 'IoC.Core.AspectOrientedMetadata.TryResolveInitializers(System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.MethodInfo}},System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.MethodInfo}}@)')
  - [TryResolveType()](#M-IoC-Core-AspectOrientedMetadata-TryResolveType-System-Type,System-Type,System-Type@- 'IoC.Core.AspectOrientedMetadata.TryResolveType(System.Type,System.Type,System.Type@)')
- [AssertionConditionAttribute](#T-IoC-AssertionConditionAttribute 'IoC.AssertionConditionAttribute')
- [AssertionConditionType](#T-IoC-AssertionConditionType 'IoC.AssertionConditionType')
  - [IS_FALSE](#F-IoC-AssertionConditionType-IS_FALSE 'IoC.AssertionConditionType.IS_FALSE')
  - [IS_NOT_NULL](#F-IoC-AssertionConditionType-IS_NOT_NULL 'IoC.AssertionConditionType.IS_NOT_NULL')
  - [IS_NULL](#F-IoC-AssertionConditionType-IS_NULL 'IoC.AssertionConditionType.IS_NULL')
  - [IS_TRUE](#F-IoC-AssertionConditionType-IS_TRUE 'IoC.AssertionConditionType.IS_TRUE')
- [AssertionMethodAttribute](#T-IoC-AssertionMethodAttribute 'IoC.AssertionMethodAttribute')
- [AutowiringStrategies](#T-IoC-AutowiringStrategies 'IoC.AutowiringStrategies')
  - [AspectOriented()](#M-IoC-AutowiringStrategies-AspectOriented 'IoC.AutowiringStrategies.AspectOriented')
  - [Order\`\`1(strategy,orderSelector)](#M-IoC-AutowiringStrategies-Order``1-IoC-IAutowiringStrategy,System-Func{``0,System-IComparable}- 'IoC.AutowiringStrategies.Order``1(IoC.IAutowiringStrategy,System.Func{``0,System.IComparable})')
  - [Tag\`\`1(strategy,tagSelector)](#M-IoC-AutowiringStrategies-Tag``1-IoC-IAutowiringStrategy,System-Func{``0,System-Object}- 'IoC.AutowiringStrategies.Tag``1(IoC.IAutowiringStrategy,System.Func{``0,System.Object})')
  - [Type\`\`1(strategy,typeSelector)](#M-IoC-AutowiringStrategies-Type``1-IoC-IAutowiringStrategy,System-Func{``0,System-Type}- 'IoC.AutowiringStrategies.Type``1(IoC.IAutowiringStrategy,System.Func{``0,System.Type})')
- [BaseTypeRequiredAttribute](#T-IoC-BaseTypeRequiredAttribute 'IoC.BaseTypeRequiredAttribute')
- [BuildContext](#T-IoC-Core-BuildContext 'IoC.Core.BuildContext')
- [CanBeNullAttribute](#T-IoC-CanBeNullAttribute 'IoC.CanBeNullAttribute')
- [CannotApplyEqualityOperatorAttribute](#T-IoC-CannotApplyEqualityOperatorAttribute 'IoC.CannotApplyEqualityOperatorAttribute')
- [CollectionAccessAttribute](#T-IoC-CollectionAccessAttribute 'IoC.CollectionAccessAttribute')
- [CollectionAccessType](#T-IoC-CollectionAccessType 'IoC.CollectionAccessType')
  - [ModifyExistingContent](#F-IoC-CollectionAccessType-ModifyExistingContent 'IoC.CollectionAccessType.ModifyExistingContent')
  - [None](#F-IoC-CollectionAccessType-None 'IoC.CollectionAccessType.None')
  - [Read](#F-IoC-CollectionAccessType-Read 'IoC.CollectionAccessType.Read')
  - [UpdatedContent](#F-IoC-CollectionAccessType-UpdatedContent 'IoC.CollectionAccessType.UpdatedContent')
- [CollectionFeature](#T-IoC-Features-CollectionFeature 'IoC.Features.CollectionFeature')
  - [Default](#F-IoC-Features-CollectionFeature-Default 'IoC.Features.CollectionFeature.Default')
  - [Apply()](#M-IoC-Features-CollectionFeature-Apply-IoC-IMutableContainer- 'IoC.Features.CollectionFeature.Apply(IoC.IMutableContainer)')
- [ConfigurationFeature](#T-IoC-Features-ConfigurationFeature 'IoC.Features.ConfigurationFeature')
  - [Default](#F-IoC-Features-ConfigurationFeature-Default 'IoC.Features.ConfigurationFeature.Default')
  - [Apply()](#M-IoC-Features-ConfigurationFeature-Apply-IoC-IMutableContainer- 'IoC.Features.ConfigurationFeature.Apply(IoC.IMutableContainer)')
- [Container](#T-IoC-Container 'IoC.Container')
  - [Parent](#P-IoC-Container-Parent 'IoC.Container.Parent')
  - [Create(name,configurations)](#M-IoC-Container-Create-System-String,System-Collections-Generic-IEnumerable{IoC-IConfiguration}- 'IoC.Container.Create(System.String,System.Collections.Generic.IEnumerable{IoC.IConfiguration})')
  - [Dispose()](#M-IoC-Container-Dispose 'IoC.Container.Dispose')
  - [GetEnumerator()](#M-IoC-Container-GetEnumerator 'IoC.Container.GetEnumerator')
  - [RegisterResource()](#M-IoC-Container-RegisterResource-System-IDisposable- 'IoC.Container.RegisterResource(System.IDisposable)')
  - [Subscribe()](#M-IoC-Container-Subscribe-System-IObserver{IoC-ContainerEvent}- 'IoC.Container.Subscribe(System.IObserver{IoC.ContainerEvent})')
  - [ToString()](#M-IoC-Container-ToString 'IoC.Container.ToString')
  - [TryGetDependency()](#M-IoC-Container-TryGetDependency-IoC-Key,IoC-IDependency@,IoC-ILifetime@- 'IoC.Container.TryGetDependency(IoC.Key,IoC.IDependency@,IoC.ILifetime@)')
  - [TryGetResolver\`\`1()](#M-IoC-Container-TryGetResolver``1-System-Type,System-Object,IoC-Resolver{``0}@,System-Exception@,IoC-IContainer- 'IoC.Container.TryGetResolver``1(System.Type,System.Object,IoC.Resolver{``0}@,System.Exception@,IoC.IContainer)')
  - [TryRegisterDependency()](#M-IoC-Container-TryRegisterDependency-System-Collections-Generic-IEnumerable{IoC-Key},IoC-IDependency,IoC-ILifetime,IoC-IToken@- 'IoC.Container.TryRegisterDependency(System.Collections.Generic.IEnumerable{IoC.Key},IoC.IDependency,IoC.ILifetime,IoC.IToken@)')
  - [UnregisterResource()](#M-IoC-Container-UnregisterResource-System-IDisposable- 'IoC.Container.UnregisterResource(System.IDisposable)')
- [ContainerEvent](#T-IoC-ContainerEvent 'IoC.ContainerEvent')
  - [#ctor(container,eventType)](#M-IoC-ContainerEvent-#ctor-IoC-IContainer,IoC-EventType- 'IoC.ContainerEvent.#ctor(IoC.IContainer,IoC.EventType)')
  - [Container](#F-IoC-ContainerEvent-Container 'IoC.ContainerEvent.Container')
  - [Dependency](#F-IoC-ContainerEvent-Dependency 'IoC.ContainerEvent.Dependency')
  - [Error](#F-IoC-ContainerEvent-Error 'IoC.ContainerEvent.Error')
  - [EventType](#F-IoC-ContainerEvent-EventType 'IoC.ContainerEvent.EventType')
  - [IsSuccess](#F-IoC-ContainerEvent-IsSuccess 'IoC.ContainerEvent.IsSuccess')
  - [Keys](#F-IoC-ContainerEvent-Keys 'IoC.ContainerEvent.Keys')
  - [Lifetime](#F-IoC-ContainerEvent-Lifetime 'IoC.ContainerEvent.Lifetime')
  - [ResolverExpression](#F-IoC-ContainerEvent-ResolverExpression 'IoC.ContainerEvent.ResolverExpression')
- [ContainerSingletonLifetime](#T-IoC-Lifetimes-ContainerSingletonLifetime 'IoC.Lifetimes.ContainerSingletonLifetime')
  - [Create()](#M-IoC-Lifetimes-ContainerSingletonLifetime-Create 'IoC.Lifetimes.ContainerSingletonLifetime.Create')
  - [CreateKey()](#M-IoC-Lifetimes-ContainerSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]- 'IoC.Lifetimes.ContainerSingletonLifetime.CreateKey(IoC.IContainer,System.Object[])')
  - [OnInstanceReleased()](#M-IoC-Lifetimes-ContainerSingletonLifetime-OnInstanceReleased-System-Object,IoC-IContainer- 'IoC.Lifetimes.ContainerSingletonLifetime.OnInstanceReleased(System.Object,IoC.IContainer)')
  - [OnNewInstanceCreated\`\`1()](#M-IoC-Lifetimes-ContainerSingletonLifetime-OnNewInstanceCreated``1-``0,IoC-IContainer,IoC-IContainer,System-Object[]- 'IoC.Lifetimes.ContainerSingletonLifetime.OnNewInstanceCreated``1(``0,IoC.IContainer,IoC.IContainer,System.Object[])')
  - [ToString()](#M-IoC-Lifetimes-ContainerSingletonLifetime-ToString 'IoC.Lifetimes.ContainerSingletonLifetime.ToString')
- [Context](#T-IoC-Context 'IoC.Context')
  - [Args](#F-IoC-Context-Args 'IoC.Context.Args')
  - [Container](#F-IoC-Context-Container 'IoC.Context.Container')
  - [Key](#F-IoC-Context-Key 'IoC.Context.Key')
- [Context\`1](#T-IoC-Context`1 'IoC.Context`1')
  - [It](#F-IoC-Context`1-It 'IoC.Context`1.It')
- [ContractAnnotationAttribute](#T-IoC-ContractAnnotationAttribute 'IoC.ContractAnnotationAttribute')
- [CoreFeature](#T-IoC-Features-CoreFeature 'IoC.Features.CoreFeature')
  - [Default](#F-IoC-Features-CoreFeature-Default 'IoC.Features.CoreFeature.Default')
  - [Apply()](#M-IoC-Features-CoreFeature-Apply-IoC-IMutableContainer- 'IoC.Features.CoreFeature.Apply(IoC.IMutableContainer)')
- [DependencyDescription](#T-IoC-Issues-DependencyDescription 'IoC.Issues.DependencyDescription')
  - [#ctor(dependency,lifetime)](#M-IoC-Issues-DependencyDescription-#ctor-IoC-IDependency,IoC-ILifetime- 'IoC.Issues.DependencyDescription.#ctor(IoC.IDependency,IoC.ILifetime)')
  - [Dependency](#F-IoC-Issues-DependencyDescription-Dependency 'IoC.Issues.DependencyDescription.Dependency')
  - [Lifetime](#F-IoC-Issues-DependencyDescription-Lifetime 'IoC.Issues.DependencyDescription.Lifetime')
- [DependencyEntry](#T-IoC-Core-DependencyEntry 'IoC.Core.DependencyEntry')
  - [ResolverParameters](#F-IoC-Core-DependencyEntry-ResolverParameters 'IoC.Core.DependencyEntry.ResolverParameters')
- [EventType](#T-IoC-EventType 'IoC.EventType')
  - [CreateContainer](#F-IoC-EventType-CreateContainer 'IoC.EventType.CreateContainer')
  - [DisposeContainer](#F-IoC-EventType-DisposeContainer 'IoC.EventType.DisposeContainer')
  - [RegisterDependency](#F-IoC-EventType-RegisterDependency 'IoC.EventType.RegisterDependency')
  - [ResolverCompilation](#F-IoC-EventType-ResolverCompilation 'IoC.EventType.ResolverCompilation')
  - [UnregisterDependency](#F-IoC-EventType-UnregisterDependency 'IoC.EventType.UnregisterDependency')
- [FluentBind](#T-IoC-FluentBind 'IoC.FluentBind')
  - [AnyTag\`\`1(binding)](#M-IoC-FluentBind-AnyTag``1-IoC-IBinding{``0}- 'IoC.FluentBind.AnyTag``1(IoC.IBinding{``0})')
  - [As\`\`1(binding,lifetime)](#M-IoC-FluentBind-As``1-IoC-IBinding{``0},IoC-Lifetime- 'IoC.FluentBind.As``1(IoC.IBinding{``0},IoC.Lifetime)')
  - [Autowiring\`\`1(binding,autowiringStrategy)](#M-IoC-FluentBind-Autowiring``1-IoC-IBinding{``0},IoC-IAutowiringStrategy- 'IoC.FluentBind.Autowiring``1(IoC.IBinding{``0},IoC.IAutowiringStrategy)')
  - [Bind(container,types)](#M-IoC-FluentBind-Bind-IoC-IMutableContainer,System-Type[]- 'IoC.FluentBind.Bind(IoC.IMutableContainer,System.Type[])')
  - [Bind(token,types)](#M-IoC-FluentBind-Bind-IoC-IToken,System-Type[]- 'IoC.FluentBind.Bind(IoC.IToken,System.Type[])')
  - [Bind\`\`1(container)](#M-IoC-FluentBind-Bind``1-IoC-IMutableContainer- 'IoC.FluentBind.Bind``1(IoC.IMutableContainer)')
  - [Bind\`\`1(token)](#M-IoC-FluentBind-Bind``1-IoC-IToken- 'IoC.FluentBind.Bind``1(IoC.IToken)')
  - [Bind\`\`1(binding)](#M-IoC-FluentBind-Bind``1-IoC-IBinding- 'IoC.FluentBind.Bind``1(IoC.IBinding)')
  - [Bind\`\`10(container)](#M-IoC-FluentBind-Bind``10-IoC-IMutableContainer- 'IoC.FluentBind.Bind``10(IoC.IMutableContainer)')
  - [Bind\`\`10(binding)](#M-IoC-FluentBind-Bind``10-IoC-IBinding- 'IoC.FluentBind.Bind``10(IoC.IBinding)')
  - [Bind\`\`10(token)](#M-IoC-FluentBind-Bind``10-IoC-IToken- 'IoC.FluentBind.Bind``10(IoC.IToken)')
  - [Bind\`\`11(container)](#M-IoC-FluentBind-Bind``11-IoC-IMutableContainer- 'IoC.FluentBind.Bind``11(IoC.IMutableContainer)')
  - [Bind\`\`11(binding)](#M-IoC-FluentBind-Bind``11-IoC-IBinding- 'IoC.FluentBind.Bind``11(IoC.IBinding)')
  - [Bind\`\`11(token)](#M-IoC-FluentBind-Bind``11-IoC-IToken- 'IoC.FluentBind.Bind``11(IoC.IToken)')
  - [Bind\`\`12(container)](#M-IoC-FluentBind-Bind``12-IoC-IMutableContainer- 'IoC.FluentBind.Bind``12(IoC.IMutableContainer)')
  - [Bind\`\`12(binding)](#M-IoC-FluentBind-Bind``12-IoC-IBinding- 'IoC.FluentBind.Bind``12(IoC.IBinding)')
  - [Bind\`\`12(token)](#M-IoC-FluentBind-Bind``12-IoC-IToken- 'IoC.FluentBind.Bind``12(IoC.IToken)')
  - [Bind\`\`13(container)](#M-IoC-FluentBind-Bind``13-IoC-IMutableContainer- 'IoC.FluentBind.Bind``13(IoC.IMutableContainer)')
  - [Bind\`\`13(binding)](#M-IoC-FluentBind-Bind``13-IoC-IBinding- 'IoC.FluentBind.Bind``13(IoC.IBinding)')
  - [Bind\`\`13(token)](#M-IoC-FluentBind-Bind``13-IoC-IToken- 'IoC.FluentBind.Bind``13(IoC.IToken)')
  - [Bind\`\`14(container)](#M-IoC-FluentBind-Bind``14-IoC-IMutableContainer- 'IoC.FluentBind.Bind``14(IoC.IMutableContainer)')
  - [Bind\`\`14(binding)](#M-IoC-FluentBind-Bind``14-IoC-IBinding- 'IoC.FluentBind.Bind``14(IoC.IBinding)')
  - [Bind\`\`14(token)](#M-IoC-FluentBind-Bind``14-IoC-IToken- 'IoC.FluentBind.Bind``14(IoC.IToken)')
  - [Bind\`\`15(container)](#M-IoC-FluentBind-Bind``15-IoC-IMutableContainer- 'IoC.FluentBind.Bind``15(IoC.IMutableContainer)')
  - [Bind\`\`15(binding)](#M-IoC-FluentBind-Bind``15-IoC-IBinding- 'IoC.FluentBind.Bind``15(IoC.IBinding)')
  - [Bind\`\`15(token)](#M-IoC-FluentBind-Bind``15-IoC-IToken- 'IoC.FluentBind.Bind``15(IoC.IToken)')
  - [Bind\`\`16(container)](#M-IoC-FluentBind-Bind``16-IoC-IMutableContainer- 'IoC.FluentBind.Bind``16(IoC.IMutableContainer)')
  - [Bind\`\`16(binding)](#M-IoC-FluentBind-Bind``16-IoC-IBinding- 'IoC.FluentBind.Bind``16(IoC.IBinding)')
  - [Bind\`\`16(token)](#M-IoC-FluentBind-Bind``16-IoC-IToken- 'IoC.FluentBind.Bind``16(IoC.IToken)')
  - [Bind\`\`17(container)](#M-IoC-FluentBind-Bind``17-IoC-IMutableContainer- 'IoC.FluentBind.Bind``17(IoC.IMutableContainer)')
  - [Bind\`\`17(binding)](#M-IoC-FluentBind-Bind``17-IoC-IBinding- 'IoC.FluentBind.Bind``17(IoC.IBinding)')
  - [Bind\`\`17(token)](#M-IoC-FluentBind-Bind``17-IoC-IToken- 'IoC.FluentBind.Bind``17(IoC.IToken)')
  - [Bind\`\`18(container)](#M-IoC-FluentBind-Bind``18-IoC-IMutableContainer- 'IoC.FluentBind.Bind``18(IoC.IMutableContainer)')
  - [Bind\`\`18(binding)](#M-IoC-FluentBind-Bind``18-IoC-IBinding- 'IoC.FluentBind.Bind``18(IoC.IBinding)')
  - [Bind\`\`18(token)](#M-IoC-FluentBind-Bind``18-IoC-IToken- 'IoC.FluentBind.Bind``18(IoC.IToken)')
  - [Bind\`\`19(container)](#M-IoC-FluentBind-Bind``19-IoC-IMutableContainer- 'IoC.FluentBind.Bind``19(IoC.IMutableContainer)')
  - [Bind\`\`19(binding)](#M-IoC-FluentBind-Bind``19-IoC-IBinding- 'IoC.FluentBind.Bind``19(IoC.IBinding)')
  - [Bind\`\`19(token)](#M-IoC-FluentBind-Bind``19-IoC-IToken- 'IoC.FluentBind.Bind``19(IoC.IToken)')
  - [Bind\`\`2(container)](#M-IoC-FluentBind-Bind``2-IoC-IMutableContainer- 'IoC.FluentBind.Bind``2(IoC.IMutableContainer)')
  - [Bind\`\`2(binding)](#M-IoC-FluentBind-Bind``2-IoC-IBinding- 'IoC.FluentBind.Bind``2(IoC.IBinding)')
  - [Bind\`\`2(token)](#M-IoC-FluentBind-Bind``2-IoC-IToken- 'IoC.FluentBind.Bind``2(IoC.IToken)')
  - [Bind\`\`20(container)](#M-IoC-FluentBind-Bind``20-IoC-IMutableContainer- 'IoC.FluentBind.Bind``20(IoC.IMutableContainer)')
  - [Bind\`\`20(binding)](#M-IoC-FluentBind-Bind``20-IoC-IBinding- 'IoC.FluentBind.Bind``20(IoC.IBinding)')
  - [Bind\`\`20(token)](#M-IoC-FluentBind-Bind``20-IoC-IToken- 'IoC.FluentBind.Bind``20(IoC.IToken)')
  - [Bind\`\`21(container)](#M-IoC-FluentBind-Bind``21-IoC-IMutableContainer- 'IoC.FluentBind.Bind``21(IoC.IMutableContainer)')
  - [Bind\`\`21(binding)](#M-IoC-FluentBind-Bind``21-IoC-IBinding- 'IoC.FluentBind.Bind``21(IoC.IBinding)')
  - [Bind\`\`21(token)](#M-IoC-FluentBind-Bind``21-IoC-IToken- 'IoC.FluentBind.Bind``21(IoC.IToken)')
  - [Bind\`\`22(container)](#M-IoC-FluentBind-Bind``22-IoC-IMutableContainer- 'IoC.FluentBind.Bind``22(IoC.IMutableContainer)')
  - [Bind\`\`22(binding)](#M-IoC-FluentBind-Bind``22-IoC-IBinding- 'IoC.FluentBind.Bind``22(IoC.IBinding)')
  - [Bind\`\`22(token)](#M-IoC-FluentBind-Bind``22-IoC-IToken- 'IoC.FluentBind.Bind``22(IoC.IToken)')
  - [Bind\`\`23(container)](#M-IoC-FluentBind-Bind``23-IoC-IMutableContainer- 'IoC.FluentBind.Bind``23(IoC.IMutableContainer)')
  - [Bind\`\`23(binding)](#M-IoC-FluentBind-Bind``23-IoC-IBinding- 'IoC.FluentBind.Bind``23(IoC.IBinding)')
  - [Bind\`\`23(token)](#M-IoC-FluentBind-Bind``23-IoC-IToken- 'IoC.FluentBind.Bind``23(IoC.IToken)')
  - [Bind\`\`24(container)](#M-IoC-FluentBind-Bind``24-IoC-IMutableContainer- 'IoC.FluentBind.Bind``24(IoC.IMutableContainer)')
  - [Bind\`\`24(binding)](#M-IoC-FluentBind-Bind``24-IoC-IBinding- 'IoC.FluentBind.Bind``24(IoC.IBinding)')
  - [Bind\`\`24(token)](#M-IoC-FluentBind-Bind``24-IoC-IToken- 'IoC.FluentBind.Bind``24(IoC.IToken)')
  - [Bind\`\`25(container)](#M-IoC-FluentBind-Bind``25-IoC-IMutableContainer- 'IoC.FluentBind.Bind``25(IoC.IMutableContainer)')
  - [Bind\`\`25(binding)](#M-IoC-FluentBind-Bind``25-IoC-IBinding- 'IoC.FluentBind.Bind``25(IoC.IBinding)')
  - [Bind\`\`25(token)](#M-IoC-FluentBind-Bind``25-IoC-IToken- 'IoC.FluentBind.Bind``25(IoC.IToken)')
  - [Bind\`\`26(container)](#M-IoC-FluentBind-Bind``26-IoC-IMutableContainer- 'IoC.FluentBind.Bind``26(IoC.IMutableContainer)')
  - [Bind\`\`26(binding)](#M-IoC-FluentBind-Bind``26-IoC-IBinding- 'IoC.FluentBind.Bind``26(IoC.IBinding)')
  - [Bind\`\`26(token)](#M-IoC-FluentBind-Bind``26-IoC-IToken- 'IoC.FluentBind.Bind``26(IoC.IToken)')
  - [Bind\`\`27(container)](#M-IoC-FluentBind-Bind``27-IoC-IMutableContainer- 'IoC.FluentBind.Bind``27(IoC.IMutableContainer)')
  - [Bind\`\`27(binding)](#M-IoC-FluentBind-Bind``27-IoC-IBinding- 'IoC.FluentBind.Bind``27(IoC.IBinding)')
  - [Bind\`\`27(token)](#M-IoC-FluentBind-Bind``27-IoC-IToken- 'IoC.FluentBind.Bind``27(IoC.IToken)')
  - [Bind\`\`28(container)](#M-IoC-FluentBind-Bind``28-IoC-IMutableContainer- 'IoC.FluentBind.Bind``28(IoC.IMutableContainer)')
  - [Bind\`\`28(binding)](#M-IoC-FluentBind-Bind``28-IoC-IBinding- 'IoC.FluentBind.Bind``28(IoC.IBinding)')
  - [Bind\`\`28(token)](#M-IoC-FluentBind-Bind``28-IoC-IToken- 'IoC.FluentBind.Bind``28(IoC.IToken)')
  - [Bind\`\`29(container)](#M-IoC-FluentBind-Bind``29-IoC-IMutableContainer- 'IoC.FluentBind.Bind``29(IoC.IMutableContainer)')
  - [Bind\`\`29(binding)](#M-IoC-FluentBind-Bind``29-IoC-IBinding- 'IoC.FluentBind.Bind``29(IoC.IBinding)')
  - [Bind\`\`29(token)](#M-IoC-FluentBind-Bind``29-IoC-IToken- 'IoC.FluentBind.Bind``29(IoC.IToken)')
  - [Bind\`\`3(container)](#M-IoC-FluentBind-Bind``3-IoC-IMutableContainer- 'IoC.FluentBind.Bind``3(IoC.IMutableContainer)')
  - [Bind\`\`3(binding)](#M-IoC-FluentBind-Bind``3-IoC-IBinding- 'IoC.FluentBind.Bind``3(IoC.IBinding)')
  - [Bind\`\`3(token)](#M-IoC-FluentBind-Bind``3-IoC-IToken- 'IoC.FluentBind.Bind``3(IoC.IToken)')
  - [Bind\`\`30(container)](#M-IoC-FluentBind-Bind``30-IoC-IMutableContainer- 'IoC.FluentBind.Bind``30(IoC.IMutableContainer)')
  - [Bind\`\`30(binding)](#M-IoC-FluentBind-Bind``30-IoC-IBinding- 'IoC.FluentBind.Bind``30(IoC.IBinding)')
  - [Bind\`\`30(token)](#M-IoC-FluentBind-Bind``30-IoC-IToken- 'IoC.FluentBind.Bind``30(IoC.IToken)')
  - [Bind\`\`31(container)](#M-IoC-FluentBind-Bind``31-IoC-IMutableContainer- 'IoC.FluentBind.Bind``31(IoC.IMutableContainer)')
  - [Bind\`\`31(binding)](#M-IoC-FluentBind-Bind``31-IoC-IBinding- 'IoC.FluentBind.Bind``31(IoC.IBinding)')
  - [Bind\`\`31(token)](#M-IoC-FluentBind-Bind``31-IoC-IToken- 'IoC.FluentBind.Bind``31(IoC.IToken)')
  - [Bind\`\`32(container)](#M-IoC-FluentBind-Bind``32-IoC-IMutableContainer- 'IoC.FluentBind.Bind``32(IoC.IMutableContainer)')
  - [Bind\`\`32(binding)](#M-IoC-FluentBind-Bind``32-IoC-IBinding- 'IoC.FluentBind.Bind``32(IoC.IBinding)')
  - [Bind\`\`32(token)](#M-IoC-FluentBind-Bind``32-IoC-IToken- 'IoC.FluentBind.Bind``32(IoC.IToken)')
  - [Bind\`\`33(container)](#M-IoC-FluentBind-Bind``33-IoC-IMutableContainer- 'IoC.FluentBind.Bind``33(IoC.IMutableContainer)')
  - [Bind\`\`33(binding)](#M-IoC-FluentBind-Bind``33-IoC-IBinding- 'IoC.FluentBind.Bind``33(IoC.IBinding)')
  - [Bind\`\`33(token)](#M-IoC-FluentBind-Bind``33-IoC-IToken- 'IoC.FluentBind.Bind``33(IoC.IToken)')
  - [Bind\`\`4(container)](#M-IoC-FluentBind-Bind``4-IoC-IMutableContainer- 'IoC.FluentBind.Bind``4(IoC.IMutableContainer)')
  - [Bind\`\`4(binding)](#M-IoC-FluentBind-Bind``4-IoC-IBinding- 'IoC.FluentBind.Bind``4(IoC.IBinding)')
  - [Bind\`\`4(token)](#M-IoC-FluentBind-Bind``4-IoC-IToken- 'IoC.FluentBind.Bind``4(IoC.IToken)')
  - [Bind\`\`5(container)](#M-IoC-FluentBind-Bind``5-IoC-IMutableContainer- 'IoC.FluentBind.Bind``5(IoC.IMutableContainer)')
  - [Bind\`\`5(binding)](#M-IoC-FluentBind-Bind``5-IoC-IBinding- 'IoC.FluentBind.Bind``5(IoC.IBinding)')
  - [Bind\`\`5(token)](#M-IoC-FluentBind-Bind``5-IoC-IToken- 'IoC.FluentBind.Bind``5(IoC.IToken)')
  - [Bind\`\`6(container)](#M-IoC-FluentBind-Bind``6-IoC-IMutableContainer- 'IoC.FluentBind.Bind``6(IoC.IMutableContainer)')
  - [Bind\`\`6(binding)](#M-IoC-FluentBind-Bind``6-IoC-IBinding- 'IoC.FluentBind.Bind``6(IoC.IBinding)')
  - [Bind\`\`6(token)](#M-IoC-FluentBind-Bind``6-IoC-IToken- 'IoC.FluentBind.Bind``6(IoC.IToken)')
  - [Bind\`\`7(container)](#M-IoC-FluentBind-Bind``7-IoC-IMutableContainer- 'IoC.FluentBind.Bind``7(IoC.IMutableContainer)')
  - [Bind\`\`7(binding)](#M-IoC-FluentBind-Bind``7-IoC-IBinding- 'IoC.FluentBind.Bind``7(IoC.IBinding)')
  - [Bind\`\`7(token)](#M-IoC-FluentBind-Bind``7-IoC-IToken- 'IoC.FluentBind.Bind``7(IoC.IToken)')
  - [Bind\`\`8(container)](#M-IoC-FluentBind-Bind``8-IoC-IMutableContainer- 'IoC.FluentBind.Bind``8(IoC.IMutableContainer)')
  - [Bind\`\`8(binding)](#M-IoC-FluentBind-Bind``8-IoC-IBinding- 'IoC.FluentBind.Bind``8(IoC.IBinding)')
  - [Bind\`\`8(token)](#M-IoC-FluentBind-Bind``8-IoC-IToken- 'IoC.FluentBind.Bind``8(IoC.IToken)')
  - [Bind\`\`9(container)](#M-IoC-FluentBind-Bind``9-IoC-IMutableContainer- 'IoC.FluentBind.Bind``9(IoC.IMutableContainer)')
  - [Bind\`\`9(binding)](#M-IoC-FluentBind-Bind``9-IoC-IBinding- 'IoC.FluentBind.Bind``9(IoC.IBinding)')
  - [Bind\`\`9(token)](#M-IoC-FluentBind-Bind``9-IoC-IToken- 'IoC.FluentBind.Bind``9(IoC.IToken)')
  - [Lifetime\`\`1(binding,lifetime)](#M-IoC-FluentBind-Lifetime``1-IoC-IBinding{``0},IoC-ILifetime- 'IoC.FluentBind.Lifetime``1(IoC.IBinding{``0},IoC.ILifetime)')
  - [Tag\`\`1(binding,tagValue)](#M-IoC-FluentBind-Tag``1-IoC-IBinding{``0},System-Object- 'IoC.FluentBind.Tag``1(IoC.IBinding{``0},System.Object)')
  - [To(binding,type,statements)](#M-IoC-FluentBind-To-IoC-IBinding{System-Object},System-Type,System-Linq-Expressions-Expression{System-Action{IoC-Context{System-Object}}}[]- 'IoC.FluentBind.To(IoC.IBinding{System.Object},System.Type,System.Linq.Expressions.Expression{System.Action{IoC.Context{System.Object}}}[])')
  - [To\`\`1(binding,statements)](#M-IoC-FluentBind-To``1-IoC-IBinding{``0},System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentBind.To``1(IoC.IBinding{``0},System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [To\`\`1(binding,factory,statements)](#M-IoC-FluentBind-To``1-IoC-IBinding{``0},System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentBind.To``1(IoC.IBinding{``0},System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
- [FluentConfiguration](#T-IoC-FluentConfiguration 'IoC.FluentConfiguration')
  - [Apply(container,configurationText)](#M-IoC-FluentConfiguration-Apply-IoC-IMutableContainer,System-String[]- 'IoC.FluentConfiguration.Apply(IoC.IMutableContainer,System.String[])')
  - [Apply(token,configurationText)](#M-IoC-FluentConfiguration-Apply-IoC-IToken,System-String[]- 'IoC.FluentConfiguration.Apply(IoC.IToken,System.String[])')
  - [Apply(container,configurationStreams)](#M-IoC-FluentConfiguration-Apply-IoC-IMutableContainer,System-IO-Stream[]- 'IoC.FluentConfiguration.Apply(IoC.IMutableContainer,System.IO.Stream[])')
  - [Apply(token,configurationStreams)](#M-IoC-FluentConfiguration-Apply-IoC-IToken,System-IO-Stream[]- 'IoC.FluentConfiguration.Apply(IoC.IToken,System.IO.Stream[])')
  - [Apply(container,configurationReaders)](#M-IoC-FluentConfiguration-Apply-IoC-IMutableContainer,System-IO-TextReader[]- 'IoC.FluentConfiguration.Apply(IoC.IMutableContainer,System.IO.TextReader[])')
  - [Apply(token,configurationReaders)](#M-IoC-FluentConfiguration-Apply-IoC-IToken,System-IO-TextReader[]- 'IoC.FluentConfiguration.Apply(IoC.IToken,System.IO.TextReader[])')
  - [Apply(container,configurations)](#M-IoC-FluentConfiguration-Apply-IoC-IMutableContainer,System-Collections-Generic-IEnumerable{IoC-IConfiguration}- 'IoC.FluentConfiguration.Apply(IoC.IMutableContainer,System.Collections.Generic.IEnumerable{IoC.IConfiguration})')
  - [Apply(token,configurations)](#M-IoC-FluentConfiguration-Apply-IoC-IToken,System-Collections-Generic-IEnumerable{IoC-IConfiguration}- 'IoC.FluentConfiguration.Apply(IoC.IToken,System.Collections.Generic.IEnumerable{IoC.IConfiguration})')
  - [Apply(container,configurations)](#M-IoC-FluentConfiguration-Apply-IoC-IMutableContainer,IoC-IConfiguration[]- 'IoC.FluentConfiguration.Apply(IoC.IMutableContainer,IoC.IConfiguration[])')
  - [Apply(token,configurations)](#M-IoC-FluentConfiguration-Apply-IoC-IToken,IoC-IConfiguration[]- 'IoC.FluentConfiguration.Apply(IoC.IToken,IoC.IConfiguration[])')
  - [Apply\`\`1(container)](#M-IoC-FluentConfiguration-Apply``1-IoC-IMutableContainer- 'IoC.FluentConfiguration.Apply``1(IoC.IMutableContainer)')
  - [Apply\`\`1(token)](#M-IoC-FluentConfiguration-Apply``1-IoC-IToken- 'IoC.FluentConfiguration.Apply``1(IoC.IToken)')
  - [AsTokenOf(disposableToken,container)](#M-IoC-FluentConfiguration-AsTokenOf-System-IDisposable,IoC-IMutableContainer- 'IoC.FluentConfiguration.AsTokenOf(System.IDisposable,IoC.IMutableContainer)')
  - [Create(configurationFactory)](#M-IoC-FluentConfiguration-Create-System-Func{IoC-IContainer,IoC-IToken}- 'IoC.FluentConfiguration.Create(System.Func{IoC.IContainer,IoC.IToken})')
  - [Using(container,configurations)](#M-IoC-FluentConfiguration-Using-IoC-IMutableContainer,IoC-IConfiguration[]- 'IoC.FluentConfiguration.Using(IoC.IMutableContainer,IoC.IConfiguration[])')
  - [Using(token,configurations)](#M-IoC-FluentConfiguration-Using-IoC-IToken,IoC-IConfiguration[]- 'IoC.FluentConfiguration.Using(IoC.IToken,IoC.IConfiguration[])')
  - [Using\`\`1(container)](#M-IoC-FluentConfiguration-Using``1-IoC-IMutableContainer- 'IoC.FluentConfiguration.Using``1(IoC.IMutableContainer)')
  - [Using\`\`1(token)](#M-IoC-FluentConfiguration-Using``1-IoC-IToken- 'IoC.FluentConfiguration.Using``1(IoC.IToken)')
- [FluentContainer](#T-IoC-FluentContainer 'IoC.FluentContainer')
  - [BuildUp\`\`1(configuration,args)](#M-IoC-FluentContainer-BuildUp``1-IoC-IConfiguration,System-Object[]- 'IoC.FluentContainer.BuildUp``1(IoC.IConfiguration,System.Object[])')
  - [BuildUp\`\`1(token,args)](#M-IoC-FluentContainer-BuildUp``1-IoC-IToken,System-Object[]- 'IoC.FluentContainer.BuildUp``1(IoC.IToken,System.Object[])')
  - [BuildUp\`\`1(container,args)](#M-IoC-FluentContainer-BuildUp``1-IoC-IMutableContainer,System-Object[]- 'IoC.FluentContainer.BuildUp``1(IoC.IMutableContainer,System.Object[])')
  - [Create(parentContainer,name)](#M-IoC-FluentContainer-Create-IoC-IContainer,System-String- 'IoC.FluentContainer.Create(IoC.IContainer,System.String)')
  - [Create(token,name)](#M-IoC-FluentContainer-Create-IoC-IToken,System-String- 'IoC.FluentContainer.Create(IoC.IToken,System.String)')
- [FluentGetResolver](#T-IoC-FluentGetResolver 'IoC.FluentGetResolver')
  - [AsTag(tagValue)](#M-IoC-FluentGetResolver-AsTag-System-Object- 'IoC.FluentGetResolver.AsTag(System.Object)')
  - [GetResolver\`\`1(type,tag,container)](#M-IoC-FluentGetResolver-GetResolver``1-IoC-IContainer,System-Type,IoC-Tag- 'IoC.FluentGetResolver.GetResolver``1(IoC.IContainer,System.Type,IoC.Tag)')
  - [GetResolver\`\`1(tag,container)](#M-IoC-FluentGetResolver-GetResolver``1-IoC-IContainer,IoC-Tag- 'IoC.FluentGetResolver.GetResolver``1(IoC.IContainer,IoC.Tag)')
  - [GetResolver\`\`1(type,container)](#M-IoC-FluentGetResolver-GetResolver``1-IoC-IContainer,System-Type- 'IoC.FluentGetResolver.GetResolver``1(IoC.IContainer,System.Type)')
  - [GetResolver\`\`1(container)](#M-IoC-FluentGetResolver-GetResolver``1-IoC-IContainer- 'IoC.FluentGetResolver.GetResolver``1(IoC.IContainer)')
  - [TryGetResolver\`\`1(type,tag,container,resolver)](#M-IoC-FluentGetResolver-TryGetResolver``1-IoC-IContainer,System-Type,IoC-Tag,IoC-Resolver{``0}@- 'IoC.FluentGetResolver.TryGetResolver``1(IoC.IContainer,System.Type,IoC.Tag,IoC.Resolver{``0}@)')
  - [TryGetResolver\`\`1(tag,container,resolver)](#M-IoC-FluentGetResolver-TryGetResolver``1-IoC-IContainer,IoC-Tag,IoC-Resolver{``0}@- 'IoC.FluentGetResolver.TryGetResolver``1(IoC.IContainer,IoC.Tag,IoC.Resolver{``0}@)')
  - [TryGetResolver\`\`1(type,container,resolver)](#M-IoC-FluentGetResolver-TryGetResolver``1-IoC-IContainer,System-Type,IoC-Resolver{``0}@- 'IoC.FluentGetResolver.TryGetResolver``1(IoC.IContainer,System.Type,IoC.Resolver{``0}@)')
  - [TryGetResolver\`\`1(container,resolver)](#M-IoC-FluentGetResolver-TryGetResolver``1-IoC-IContainer,IoC-Resolver{``0}@- 'IoC.FluentGetResolver.TryGetResolver``1(IoC.IContainer,IoC.Resolver{``0}@)')
- [FluentNativeResolve](#T-IoC-FluentNativeResolve 'IoC.FluentNativeResolve')
  - [Resolve\`\`1(container)](#M-IoC-FluentNativeResolve-Resolve``1-IoC-Container- 'IoC.FluentNativeResolve.Resolve``1(IoC.Container)')
  - [Resolve\`\`1(container,tag)](#M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,IoC-Tag- 'IoC.FluentNativeResolve.Resolve``1(IoC.Container,IoC.Tag)')
  - [Resolve\`\`1(container,args)](#M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,System-Object[]- 'IoC.FluentNativeResolve.Resolve``1(IoC.Container,System.Object[])')
  - [Resolve\`\`1(container,tag,args)](#M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,IoC-Tag,System-Object[]- 'IoC.FluentNativeResolve.Resolve``1(IoC.Container,IoC.Tag,System.Object[])')
  - [Resolve\`\`1(container,type)](#M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,System-Type- 'IoC.FluentNativeResolve.Resolve``1(IoC.Container,System.Type)')
  - [Resolve\`\`1(container,type,tag)](#M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,System-Type,IoC-Tag- 'IoC.FluentNativeResolve.Resolve``1(IoC.Container,System.Type,IoC.Tag)')
  - [Resolve\`\`1(container,type,args)](#M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,System-Type,System-Object[]- 'IoC.FluentNativeResolve.Resolve``1(IoC.Container,System.Type,System.Object[])')
  - [Resolve\`\`1(container,type,tag,args)](#M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,System-Type,IoC-Tag,System-Object[]- 'IoC.FluentNativeResolve.Resolve``1(IoC.Container,System.Type,IoC.Tag,System.Object[])')
- [FluentRegister](#T-IoC-Core-FluentRegister 'IoC.Core.FluentRegister')
  - [Register(container,types,dependency,lifetime,tags)](#M-IoC-Core-FluentRegister-Register-IoC-IMutableContainer,System-Collections-Generic-IEnumerable{System-Type},IoC-IDependency,IoC-ILifetime,System-Object[]- 'IoC.Core.FluentRegister.Register(IoC.IMutableContainer,System.Collections.Generic.IEnumerable{System.Type},IoC.IDependency,IoC.ILifetime,System.Object[])')
  - [Register\`\`1(container,lifetime,tags)](#M-IoC-Core-FluentRegister-Register``1-IoC-IMutableContainer,IoC-ILifetime,System-Object[]- 'IoC.Core.FluentRegister.Register``1(IoC.IMutableContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`1(container,factory,lifetime,tags,statements)](#M-IoC-Core-FluentRegister-Register``1-IoC-IMutableContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.Core.FluentRegister.Register``1(IoC.IMutableContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`2(container,lifetime,tags)](#M-IoC-Core-FluentRegister-Register``2-IoC-IMutableContainer,IoC-ILifetime,System-Object[]- 'IoC.Core.FluentRegister.Register``2(IoC.IMutableContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`2(container,factory,lifetime,tags,statements)](#M-IoC-Core-FluentRegister-Register``2-IoC-IMutableContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.Core.FluentRegister.Register``2(IoC.IMutableContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`3(container,lifetime,tags)](#M-IoC-Core-FluentRegister-Register``3-IoC-IMutableContainer,IoC-ILifetime,System-Object[]- 'IoC.Core.FluentRegister.Register``3(IoC.IMutableContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`3(container,factory,lifetime,tags,statements)](#M-IoC-Core-FluentRegister-Register``3-IoC-IMutableContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.Core.FluentRegister.Register``3(IoC.IMutableContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`4(container,lifetime,tags)](#M-IoC-Core-FluentRegister-Register``4-IoC-IMutableContainer,IoC-ILifetime,System-Object[]- 'IoC.Core.FluentRegister.Register``4(IoC.IMutableContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`4(container,factory,lifetime,tags,statements)](#M-IoC-Core-FluentRegister-Register``4-IoC-IMutableContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.Core.FluentRegister.Register``4(IoC.IMutableContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
- [FluentResolve](#T-IoC-FluentResolve 'IoC.FluentResolve')
  - [Resolve\`\`1(container,args)](#M-IoC-FluentResolve-Resolve``1-IoC-IContainer,System-Object[]- 'IoC.FluentResolve.Resolve``1(IoC.IContainer,System.Object[])')
  - [Resolve\`\`1(container,tag,args)](#M-IoC-FluentResolve-Resolve``1-IoC-IContainer,IoC-Tag,System-Object[]- 'IoC.FluentResolve.Resolve``1(IoC.IContainer,IoC.Tag,System.Object[])')
  - [Resolve\`\`1(container,type,args)](#M-IoC-FluentResolve-Resolve``1-IoC-IContainer,System-Type,System-Object[]- 'IoC.FluentResolve.Resolve``1(IoC.IContainer,System.Type,System.Object[])')
  - [Resolve\`\`1(container,type,tag,args)](#M-IoC-FluentResolve-Resolve``1-IoC-IContainer,System-Type,IoC-Tag,System-Object[]- 'IoC.FluentResolve.Resolve``1(IoC.IContainer,System.Type,IoC.Tag,System.Object[])')
- [FluentScope](#T-IoC-FluentScope 'IoC.FluentScope')
  - [CreateScope(container)](#M-IoC-FluentScope-CreateScope-IoC-IContainer- 'IoC.FluentScope.CreateScope(IoC.IContainer)')
- [FluentTrace](#T-IoC-FluentTrace 'IoC.FluentTrace')
  - [ToTraceSource(container)](#M-IoC-FluentTrace-ToTraceSource-IoC-IContainer- 'IoC.FluentTrace.ToTraceSource(IoC.IContainer)')
  - [Trace(container,onTraceMessage)](#M-IoC-FluentTrace-Trace-IoC-IMutableContainer,System-Action{System-String}- 'IoC.FluentTrace.Trace(IoC.IMutableContainer,System.Action{System.String})')
  - [Trace(token,onTraceMessage)](#M-IoC-FluentTrace-Trace-IoC-IToken,System-Action{System-String}- 'IoC.FluentTrace.Trace(IoC.IToken,System.Action{System.String})')
  - [Trace(container)](#M-IoC-FluentTrace-Trace-IoC-IMutableContainer- 'IoC.FluentTrace.Trace(IoC.IMutableContainer)')
  - [Trace(token)](#M-IoC-FluentTrace-Trace-IoC-IToken- 'IoC.FluentTrace.Trace(IoC.IToken)')
- [FuncFeature](#T-IoC-Features-FuncFeature 'IoC.Features.FuncFeature')
  - [Default](#F-IoC-Features-FuncFeature-Default 'IoC.Features.FuncFeature.Default')
  - [Light](#F-IoC-Features-FuncFeature-Light 'IoC.Features.FuncFeature.Light')
  - [Apply()](#M-IoC-Features-FuncFeature-Apply-IoC-IMutableContainer- 'IoC.Features.FuncFeature.Apply(IoC.IMutableContainer)')
- [GenericTypeArgumentAttribute](#T-IoC-GenericTypeArgumentAttribute 'IoC.GenericTypeArgumentAttribute')
- [IArray](#T-IoC-Core-IArray 'IoC.Core.IArray')
- [IAutowiringStrategy](#T-IoC-IAutowiringStrategy 'IoC.IAutowiringStrategy')
  - [TryResolveConstructor(constructors,constructor)](#M-IoC-IAutowiringStrategy-TryResolveConstructor-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-ConstructorInfo}},IoC-IMethod{System-Reflection-ConstructorInfo}@- 'IoC.IAutowiringStrategy.TryResolveConstructor(System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.ConstructorInfo}},IoC.IMethod{System.Reflection.ConstructorInfo}@)')
  - [TryResolveInitializers(methods,initializers)](#M-IoC-IAutowiringStrategy-TryResolveInitializers-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}},System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}}@- 'IoC.IAutowiringStrategy.TryResolveInitializers(System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.MethodInfo}},System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.MethodInfo}}@)')
  - [TryResolveType(registeredType,resolvingType,instanceType)](#M-IoC-IAutowiringStrategy-TryResolveType-System-Type,System-Type,System-Type@- 'IoC.IAutowiringStrategy.TryResolveType(System.Type,System.Type,System.Type@)')
- [IBinding](#T-IoC-IBinding 'IoC.IBinding')
  - [AutowiringStrategy](#P-IoC-IBinding-AutowiringStrategy 'IoC.IBinding.AutowiringStrategy')
  - [Container](#P-IoC-IBinding-Container 'IoC.IBinding.Container')
  - [Lifetime](#P-IoC-IBinding-Lifetime 'IoC.IBinding.Lifetime')
  - [Tags](#P-IoC-IBinding-Tags 'IoC.IBinding.Tags')
  - [Tokens](#P-IoC-IBinding-Tokens 'IoC.IBinding.Tokens')
  - [Types](#P-IoC-IBinding-Types 'IoC.IBinding.Types')
- [IBinding\`1](#T-IoC-IBinding`1 'IoC.IBinding`1')
- [IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext')
  - [AutowiringStrategy](#P-IoC-IBuildContext-AutowiringStrategy 'IoC.IBuildContext.AutowiringStrategy')
  - [Container](#P-IoC-IBuildContext-Container 'IoC.IBuildContext.Container')
  - [Depth](#P-IoC-IBuildContext-Depth 'IoC.IBuildContext.Depth')
  - [Key](#P-IoC-IBuildContext-Key 'IoC.IBuildContext.Key')
  - [Parent](#P-IoC-IBuildContext-Parent 'IoC.IBuildContext.Parent')
  - [AddLifetime(baseExpression,lifetime)](#M-IoC-IBuildContext-AddLifetime-System-Linq-Expressions-Expression,IoC-ILifetime- 'IoC.IBuildContext.AddLifetime(System.Linq.Expressions.Expression,IoC.ILifetime)')
  - [BindTypes(originalType,targetType)](#M-IoC-IBuildContext-BindTypes-System-Type,System-Type- 'IoC.IBuildContext.BindTypes(System.Type,System.Type)')
  - [CreateChild(key,container)](#M-IoC-IBuildContext-CreateChild-IoC-Key,IoC-IContainer- 'IoC.IBuildContext.CreateChild(IoC.Key,IoC.IContainer)')
  - [GetDependencyExpression(defaultExpression)](#M-IoC-IBuildContext-GetDependencyExpression-System-Linq-Expressions-Expression- 'IoC.IBuildContext.GetDependencyExpression(System.Linq.Expressions.Expression)')
  - [InjectDependencies(baseExpression,instanceExpression)](#M-IoC-IBuildContext-InjectDependencies-System-Linq-Expressions-Expression,System-Linq-Expressions-ParameterExpression- 'IoC.IBuildContext.InjectDependencies(System.Linq.Expressions.Expression,System.Linq.Expressions.ParameterExpression)')
  - [ReplaceTypes(baseExpression)](#M-IoC-IBuildContext-ReplaceTypes-System-Linq-Expressions-Expression- 'IoC.IBuildContext.ReplaceTypes(System.Linq.Expressions.Expression)')
  - [TryReplaceType(originalType,targetType)](#M-IoC-IBuildContext-TryReplaceType-System-Type,System-Type@- 'IoC.IBuildContext.TryReplaceType(System.Type,System.Type@)')
- [IBuilder](#T-IoC-IBuilder 'IoC.IBuilder')
  - [Build(context,bodyExpression)](#M-IoC-IBuilder-Build-IoC-IBuildContext,System-Linq-Expressions-Expression- 'IoC.IBuilder.Build(IoC.IBuildContext,System.Linq.Expressions.Expression)')
- [ICannotBuildExpression](#T-IoC-Issues-ICannotBuildExpression 'IoC.Issues.ICannotBuildExpression')
  - [Resolve(buildContext,dependency,lifetime,error)](#M-IoC-Issues-ICannotBuildExpression-Resolve-IoC-IBuildContext,IoC-IDependency,IoC-ILifetime,System-Exception- 'IoC.Issues.ICannotBuildExpression.Resolve(IoC.IBuildContext,IoC.IDependency,IoC.ILifetime,System.Exception)')
- [ICannotGetResolver](#T-IoC-Issues-ICannotGetResolver 'IoC.Issues.ICannotGetResolver')
  - [Resolve\`\`1(container,key,error)](#M-IoC-Issues-ICannotGetResolver-Resolve``1-IoC-IContainer,IoC-Key,System-Exception- 'IoC.Issues.ICannotGetResolver.Resolve``1(IoC.IContainer,IoC.Key,System.Exception)')
- [ICannotParseLifetime](#T-IoC-Issues-ICannotParseLifetime 'IoC.Issues.ICannotParseLifetime')
  - [Resolve(statementText,statementLineNumber,statementPosition,lifetimeName)](#M-IoC-Issues-ICannotParseLifetime-Resolve-System-String,System-Int32,System-Int32,System-String- 'IoC.Issues.ICannotParseLifetime.Resolve(System.String,System.Int32,System.Int32,System.String)')
- [ICannotParseTag](#T-IoC-Issues-ICannotParseTag 'IoC.Issues.ICannotParseTag')
  - [Resolve(statementText,statementLineNumber,statementPosition,tag)](#M-IoC-Issues-ICannotParseTag-Resolve-System-String,System-Int32,System-Int32,System-String- 'IoC.Issues.ICannotParseTag.Resolve(System.String,System.Int32,System.Int32,System.String)')
- [ICannotParseType](#T-IoC-Issues-ICannotParseType 'IoC.Issues.ICannotParseType')
  - [Resolve(statementText,statementLineNumber,statementPosition,typeName)](#M-IoC-Issues-ICannotParseType-Resolve-System-String,System-Int32,System-Int32,System-String- 'IoC.Issues.ICannotParseType.Resolve(System.String,System.Int32,System.Int32,System.String)')
- [ICannotRegister](#T-IoC-Issues-ICannotRegister 'IoC.Issues.ICannotRegister')
  - [Resolve(container,keys)](#M-IoC-Issues-ICannotRegister-Resolve-IoC-IContainer,IoC-Key[]- 'IoC.Issues.ICannotRegister.Resolve(IoC.IContainer,IoC.Key[])')
- [ICannotResolveConstructor](#T-IoC-Issues-ICannotResolveConstructor 'IoC.Issues.ICannotResolveConstructor')
  - [Resolve(buildContext,constructors)](#M-IoC-Issues-ICannotResolveConstructor-Resolve-IoC-IBuildContext,System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-ConstructorInfo}}- 'IoC.Issues.ICannotResolveConstructor.Resolve(IoC.IBuildContext,System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.ConstructorInfo}})')
- [ICannotResolveDependency](#T-IoC-Issues-ICannotResolveDependency 'IoC.Issues.ICannotResolveDependency')
  - [Resolve(buildContext)](#M-IoC-Issues-ICannotResolveDependency-Resolve-IoC-IBuildContext- 'IoC.Issues.ICannotResolveDependency.Resolve(IoC.IBuildContext)')
- [ICannotResolveGenericTypeArgument](#T-IoC-Issues-ICannotResolveGenericTypeArgument 'IoC.Issues.ICannotResolveGenericTypeArgument')
  - [Resolve(buildContext,type,genericTypeArgPosition,genericTypeArg)](#M-IoC-Issues-ICannotResolveGenericTypeArgument-Resolve-IoC-IBuildContext,System-Type,System-Int32,System-Type- 'IoC.Issues.ICannotResolveGenericTypeArgument.Resolve(IoC.IBuildContext,System.Type,System.Int32,System.Type)')
- [ICannotResolveType](#T-IoC-Issues-ICannotResolveType 'IoC.Issues.ICannotResolveType')
  - [Resolve(buildContext,registeredType,resolvingType)](#M-IoC-Issues-ICannotResolveType-Resolve-IoC-IBuildContext,System-Type,System-Type- 'IoC.Issues.ICannotResolveType.Resolve(IoC.IBuildContext,System.Type,System.Type)')
- [ICompiler](#T-IoC-ICompiler 'IoC.ICompiler')
  - [TryCompile(context,expression,resolver)](#M-IoC-ICompiler-TryCompile-IoC-IBuildContext,System-Linq-Expressions-LambdaExpression,System-Delegate@- 'IoC.ICompiler.TryCompile(IoC.IBuildContext,System.Linq.Expressions.LambdaExpression,System.Delegate@)')
- [ICompositionRoot\`1](#T-IoC-ICompositionRoot`1 'IoC.ICompositionRoot`1')
  - [Instance](#P-IoC-ICompositionRoot`1-Instance 'IoC.ICompositionRoot`1.Instance')
- [IConfiguration](#T-IoC-IConfiguration 'IoC.IConfiguration')
  - [Apply(container)](#M-IoC-IConfiguration-Apply-IoC-IMutableContainer- 'IoC.IConfiguration.Apply(IoC.IMutableContainer)')
- [IContainer](#T-IoC-IContainer 'IoC.IContainer')
  - [Parent](#P-IoC-IContainer-Parent 'IoC.IContainer.Parent')
  - [TryGetDependency(key,dependency,lifetime)](#M-IoC-IContainer-TryGetDependency-IoC-Key,IoC-IDependency@,IoC-ILifetime@- 'IoC.IContainer.TryGetDependency(IoC.Key,IoC.IDependency@,IoC.ILifetime@)')
  - [TryGetResolver\`\`1(type,tag,resolver,error,resolvingContainer)](#M-IoC-IContainer-TryGetResolver``1-System-Type,System-Object,IoC-Resolver{``0}@,System-Exception@,IoC-IContainer- 'IoC.IContainer.TryGetResolver``1(System.Type,System.Object,IoC.Resolver{``0}@,System.Exception@,IoC.IContainer)')
- [IDependency](#T-IoC-IDependency 'IoC.IDependency')
  - [TryBuildExpression(buildContext,lifetime,baseExpression,error)](#M-IoC-IDependency-TryBuildExpression-IoC-IBuildContext,IoC-ILifetime,System-Linq-Expressions-Expression@,System-Exception@- 'IoC.IDependency.TryBuildExpression(IoC.IBuildContext,IoC.ILifetime,System.Linq.Expressions.Expression@,System.Exception@)')
- [IExpressionBuilder\`1](#T-IoC-Core-IExpressionBuilder`1 'IoC.Core.IExpressionBuilder`1')
  - [Build(bodyExpression,buildContext,context)](#M-IoC-Core-IExpressionBuilder`1-Build-System-Linq-Expressions-Expression,IoC-IBuildContext,`0- 'IoC.Core.IExpressionBuilder`1.Build(System.Linq.Expressions.Expression,IoC.IBuildContext,`0)')
- [IFoundCyclicDependency](#T-IoC-Issues-IFoundCyclicDependency 'IoC.Issues.IFoundCyclicDependency')
  - [Resolve(buildContext)](#M-IoC-Issues-IFoundCyclicDependency-Resolve-IoC-IBuildContext- 'IoC.Issues.IFoundCyclicDependency.Resolve(IoC.IBuildContext)')
- [ILifetime](#T-IoC-ILifetime 'IoC.ILifetime')
  - [Create()](#M-IoC-ILifetime-Create 'IoC.ILifetime.Create')
  - [SelectResolvingContainer(registrationContainer,resolvingContainer)](#M-IoC-ILifetime-SelectResolvingContainer-IoC-IContainer,IoC-IContainer- 'IoC.ILifetime.SelectResolvingContainer(IoC.IContainer,IoC.IContainer)')
- [IMethod\`1](#T-IoC-IMethod`1 'IoC.IMethod`1')
  - [Info](#P-IoC-IMethod`1-Info 'IoC.IMethod`1.Info')
  - [GetParametersExpressions()](#M-IoC-IMethod`1-GetParametersExpressions-IoC-IBuildContext- 'IoC.IMethod`1.GetParametersExpressions(IoC.IBuildContext)')
  - [SetDependency(parameterPosition,dependencyType,dependencyTag)](#M-IoC-IMethod`1-SetDependency-System-Int32,System-Type,System-Object- 'IoC.IMethod`1.SetDependency(System.Int32,System.Type,System.Object)')
  - [SetExpression(parameterPosition,parameterExpression)](#M-IoC-IMethod`1-SetExpression-System-Int32,System-Linq-Expressions-Expression- 'IoC.IMethod`1.SetExpression(System.Int32,System.Linq.Expressions.Expression)')
- [IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer')
  - [TryRegisterDependency(keys,dependency,lifetime,dependencyToken)](#M-IoC-IMutableContainer-TryRegisterDependency-System-Collections-Generic-IEnumerable{IoC-Key},IoC-IDependency,IoC-ILifetime,IoC-IToken@- 'IoC.IMutableContainer.TryRegisterDependency(System.Collections.Generic.IEnumerable{IoC.Key},IoC.IDependency,IoC.ILifetime,IoC.IToken@)')
- [IResourceRegistry](#T-IoC-IResourceRegistry 'IoC.IResourceRegistry')
  - [RegisterResource(resource)](#M-IoC-IResourceRegistry-RegisterResource-System-IDisposable- 'IoC.IResourceRegistry.RegisterResource(System.IDisposable)')
  - [UnregisterResource(resource)](#M-IoC-IResourceRegistry-UnregisterResource-System-IDisposable- 'IoC.IResourceRegistry.UnregisterResource(System.IDisposable)')
- [IScope](#T-IoC-IScope 'IoC.IScope')
  - [Activate()](#M-IoC-IScope-Activate 'IoC.IScope.Activate')
- [IToken](#T-IoC-IToken 'IoC.IToken')
  - [Container](#P-IoC-IToken-Container 'IoC.IToken.Container')
- [ImplicitNotNullAttribute](#T-IoC-ImplicitNotNullAttribute 'IoC.ImplicitNotNullAttribute')
- [ImplicitUseKindFlags](#T-IoC-ImplicitUseKindFlags 'IoC.ImplicitUseKindFlags')
  - [Access](#F-IoC-ImplicitUseKindFlags-Access 'IoC.ImplicitUseKindFlags.Access')
  - [Assign](#F-IoC-ImplicitUseKindFlags-Assign 'IoC.ImplicitUseKindFlags.Assign')
  - [InstantiatedNoFixedConstructorSignature](#F-IoC-ImplicitUseKindFlags-InstantiatedNoFixedConstructorSignature 'IoC.ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature')
  - [InstantiatedWithFixedConstructorSignature](#F-IoC-ImplicitUseKindFlags-InstantiatedWithFixedConstructorSignature 'IoC.ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature')
- [ImplicitUseTargetFlags](#T-IoC-ImplicitUseTargetFlags 'IoC.ImplicitUseTargetFlags')
  - [Members](#F-IoC-ImplicitUseTargetFlags-Members 'IoC.ImplicitUseTargetFlags.Members')
  - [WithMembers](#F-IoC-ImplicitUseTargetFlags-WithMembers 'IoC.ImplicitUseTargetFlags.WithMembers')
- [Injections](#T-IoC-Injections 'IoC.Injections')
  - [Inject(container,type)](#M-IoC-Injections-Inject-IoC-IContainer,System-Type- 'IoC.Injections.Inject(IoC.IContainer,System.Type)')
  - [Inject(container,type,tag)](#M-IoC-Injections-Inject-IoC-IContainer,System-Type,System-Object- 'IoC.Injections.Inject(IoC.IContainer,System.Type,System.Object)')
  - [Inject\`\`1(container)](#M-IoC-Injections-Inject``1-IoC-IContainer- 'IoC.Injections.Inject``1(IoC.IContainer)')
  - [Inject\`\`1(container,tag)](#M-IoC-Injections-Inject``1-IoC-IContainer,System-Object- 'IoC.Injections.Inject``1(IoC.IContainer,System.Object)')
  - [Inject\`\`1(container,destination,source)](#M-IoC-Injections-Inject``1-IoC-IContainer,``0,``0- 'IoC.Injections.Inject``1(IoC.IContainer,``0,``0)')
  - [TryInject(container,type)](#M-IoC-Injections-TryInject-IoC-IContainer,System-Type- 'IoC.Injections.TryInject(IoC.IContainer,System.Type)')
  - [TryInject(container,type,tag)](#M-IoC-Injections-TryInject-IoC-IContainer,System-Type,System-Object- 'IoC.Injections.TryInject(IoC.IContainer,System.Type,System.Object)')
  - [TryInject\`\`1(container)](#M-IoC-Injections-TryInject``1-IoC-IContainer- 'IoC.Injections.TryInject``1(IoC.IContainer)')
  - [TryInject\`\`1(container,tag)](#M-IoC-Injections-TryInject``1-IoC-IContainer,System-Object- 'IoC.Injections.TryInject``1(IoC.IContainer,System.Object)')
- [InstantHandleAttribute](#T-IoC-InstantHandleAttribute 'IoC.InstantHandleAttribute')
- [InvokerParameterNameAttribute](#T-IoC-InvokerParameterNameAttribute 'IoC.InvokerParameterNameAttribute')
- [ItemCanBeNullAttribute](#T-IoC-ItemCanBeNullAttribute 'IoC.ItemCanBeNullAttribute')
- [ItemNotNullAttribute](#T-IoC-ItemNotNullAttribute 'IoC.ItemNotNullAttribute')
- [Key](#T-IoC-Key 'IoC.Key')
  - [#ctor(type,tag)](#M-IoC-Key-#ctor-System-Type,System-Object- 'IoC.Key.#ctor(System.Type,System.Object)')
  - [AnyTag](#F-IoC-Key-AnyTag 'IoC.Key.AnyTag')
  - [Tag](#F-IoC-Key-Tag 'IoC.Key.Tag')
  - [Type](#F-IoC-Key-Type 'IoC.Key.Type')
  - [Equals()](#M-IoC-Key-Equals-System-Object- 'IoC.Key.Equals(System.Object)')
  - [Equals()](#M-IoC-Key-Equals-IoC-Key- 'IoC.Key.Equals(IoC.Key)')
  - [GetHashCode()](#M-IoC-Key-GetHashCode 'IoC.Key.GetHashCode')
  - [ToString()](#M-IoC-Key-ToString 'IoC.Key.ToString')
- [KeyBasedLifetime\`1](#T-IoC-Lifetimes-KeyBasedLifetime`1 'IoC.Lifetimes.KeyBasedLifetime`1')
  - [Build()](#M-IoC-Lifetimes-KeyBasedLifetime`1-Build-IoC-IBuildContext,System-Linq-Expressions-Expression- 'IoC.Lifetimes.KeyBasedLifetime`1.Build(IoC.IBuildContext,System.Linq.Expressions.Expression)')
  - [Create()](#M-IoC-Lifetimes-KeyBasedLifetime`1-Create 'IoC.Lifetimes.KeyBasedLifetime`1.Create')
  - [CreateKey(container,args)](#M-IoC-Lifetimes-KeyBasedLifetime`1-CreateKey-IoC-IContainer,System-Object[]- 'IoC.Lifetimes.KeyBasedLifetime`1.CreateKey(IoC.IContainer,System.Object[])')
  - [Dispose()](#M-IoC-Lifetimes-KeyBasedLifetime`1-Dispose 'IoC.Lifetimes.KeyBasedLifetime`1.Dispose')
  - [OnInstanceReleased(releasedInstance,key)](#M-IoC-Lifetimes-KeyBasedLifetime`1-OnInstanceReleased-System-Object,`0- 'IoC.Lifetimes.KeyBasedLifetime`1.OnInstanceReleased(System.Object,`0)')
  - [OnNewInstanceCreated\`\`1(newInstance,key,container,args)](#M-IoC-Lifetimes-KeyBasedLifetime`1-OnNewInstanceCreated``1-``0,`0,IoC-IContainer,System-Object[]- 'IoC.Lifetimes.KeyBasedLifetime`1.OnNewInstanceCreated``1(``0,`0,IoC.IContainer,System.Object[])')
  - [SelectResolvingContainer()](#M-IoC-Lifetimes-KeyBasedLifetime`1-SelectResolvingContainer-IoC-IContainer,IoC-IContainer- 'IoC.Lifetimes.KeyBasedLifetime`1.SelectResolvingContainer(IoC.IContainer,IoC.IContainer)')
- [LazyFeature](#T-IoC-Features-LazyFeature 'IoC.Features.LazyFeature')
  - [Default](#F-IoC-Features-LazyFeature-Default 'IoC.Features.LazyFeature.Default')
  - [Apply()](#M-IoC-Features-LazyFeature-Apply-IoC-IMutableContainer- 'IoC.Features.LazyFeature.Apply(IoC.IMutableContainer)')
- [Lifetime](#T-IoC-Lifetime 'IoC.Lifetime')
  - [ContainerSingleton](#F-IoC-Lifetime-ContainerSingleton 'IoC.Lifetime.ContainerSingleton')
  - [ScopeSingleton](#F-IoC-Lifetime-ScopeSingleton 'IoC.Lifetime.ScopeSingleton')
  - [Singleton](#F-IoC-Lifetime-Singleton 'IoC.Lifetime.Singleton')
  - [Transient](#F-IoC-Lifetime-Transient 'IoC.Lifetime.Transient')
- [LinqTunnelAttribute](#T-IoC-LinqTunnelAttribute 'IoC.LinqTunnelAttribute')
- [LocalizationRequiredAttribute](#T-IoC-LocalizationRequiredAttribute 'IoC.LocalizationRequiredAttribute')
- [MacroAttribute](#T-IoC-MacroAttribute 'IoC.MacroAttribute')
  - [Editable](#P-IoC-MacroAttribute-Editable 'IoC.MacroAttribute.Editable')
  - [Expression](#P-IoC-MacroAttribute-Expression 'IoC.MacroAttribute.Expression')
  - [Target](#P-IoC-MacroAttribute-Target 'IoC.MacroAttribute.Target')
- [MeansImplicitUseAttribute](#T-IoC-MeansImplicitUseAttribute 'IoC.MeansImplicitUseAttribute')
- [MustUseReturnValueAttribute](#T-IoC-MustUseReturnValueAttribute 'IoC.MustUseReturnValueAttribute')
- [NoEnumerationAttribute](#T-IoC-NoEnumerationAttribute 'IoC.NoEnumerationAttribute')
- [NoReorder](#T-IoC-NoReorder 'IoC.NoReorder')
- [NotNullAttribute](#T-IoC-NotNullAttribute 'IoC.NotNullAttribute')
- [NotifyPropertyChangedInvocatorAttribute](#T-IoC-NotifyPropertyChangedInvocatorAttribute 'IoC.NotifyPropertyChangedInvocatorAttribute')
- [PathReferenceAttribute](#T-IoC-PathReferenceAttribute 'IoC.PathReferenceAttribute')
- [ProvidesContextAttribute](#T-IoC-ProvidesContextAttribute 'IoC.ProvidesContextAttribute')
- [PublicAPIAttribute](#T-IoC-PublicAPIAttribute 'IoC.PublicAPIAttribute')
- [PureAttribute](#T-IoC-PureAttribute 'IoC.PureAttribute')
- [RazorSectionAttribute](#T-IoC-RazorSectionAttribute 'IoC.RazorSectionAttribute')
- [RegexPatternAttribute](#T-IoC-RegexPatternAttribute 'IoC.RegexPatternAttribute')
- [Resolver\`1](#T-IoC-Resolver`1 'IoC.Resolver`1')
- [ScopeSingletonLifetime](#T-IoC-Lifetimes-ScopeSingletonLifetime 'IoC.Lifetimes.ScopeSingletonLifetime')
  - [Create()](#M-IoC-Lifetimes-ScopeSingletonLifetime-Create 'IoC.Lifetimes.ScopeSingletonLifetime.Create')
  - [CreateKey()](#M-IoC-Lifetimes-ScopeSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]- 'IoC.Lifetimes.ScopeSingletonLifetime.CreateKey(IoC.IContainer,System.Object[])')
  - [OnInstanceReleased()](#M-IoC-Lifetimes-ScopeSingletonLifetime-OnInstanceReleased-System-Object,IoC-IScope- 'IoC.Lifetimes.ScopeSingletonLifetime.OnInstanceReleased(System.Object,IoC.IScope)')
  - [OnNewInstanceCreated\`\`1()](#M-IoC-Lifetimes-ScopeSingletonLifetime-OnNewInstanceCreated``1-``0,IoC-IScope,IoC-IContainer,System-Object[]- 'IoC.Lifetimes.ScopeSingletonLifetime.OnNewInstanceCreated``1(``0,IoC.IScope,IoC.IContainer,System.Object[])')
  - [ToString()](#M-IoC-Lifetimes-ScopeSingletonLifetime-ToString 'IoC.Lifetimes.ScopeSingletonLifetime.ToString')
- [Set](#T-IoC-Features-Set 'IoC.Features.Set')
  - [Core](#F-IoC-Features-Set-Core 'IoC.Features.Set.Core')
  - [Default](#F-IoC-Features-Set-Default 'IoC.Features.Set.Default')
  - [Light](#F-IoC-Features-Set-Light 'IoC.Features.Set.Light')
- [SingletonLifetime](#T-IoC-Lifetimes-SingletonLifetime 'IoC.Lifetimes.SingletonLifetime')
  - [Build()](#M-IoC-Lifetimes-SingletonLifetime-Build-IoC-IBuildContext,System-Linq-Expressions-Expression- 'IoC.Lifetimes.SingletonLifetime.Build(IoC.IBuildContext,System.Linq.Expressions.Expression)')
  - [Create()](#M-IoC-Lifetimes-SingletonLifetime-Create 'IoC.Lifetimes.SingletonLifetime.Create')
  - [Dispose()](#M-IoC-Lifetimes-SingletonLifetime-Dispose 'IoC.Lifetimes.SingletonLifetime.Dispose')
  - [SelectResolvingContainer()](#M-IoC-Lifetimes-SingletonLifetime-SelectResolvingContainer-IoC-IContainer,IoC-IContainer- 'IoC.Lifetimes.SingletonLifetime.SelectResolvingContainer(IoC.IContainer,IoC.IContainer)')
  - [ToString()](#M-IoC-Lifetimes-SingletonLifetime-ToString 'IoC.Lifetimes.SingletonLifetime.ToString')
- [SourceTemplateAttribute](#T-IoC-SourceTemplateAttribute 'IoC.SourceTemplateAttribute')
- [StringFormatMethodAttribute](#T-IoC-StringFormatMethodAttribute 'IoC.StringFormatMethodAttribute')
  - [#ctor(formatParameterName)](#M-IoC-StringFormatMethodAttribute-#ctor-System-String- 'IoC.StringFormatMethodAttribute.#ctor(System.String)')
- [TT](#T-IoC-TT 'IoC.TT')
- [TT1](#T-IoC-TT1 'IoC.TT1')
- [TT10](#T-IoC-TT10 'IoC.TT10')
- [TT11](#T-IoC-TT11 'IoC.TT11')
- [TT12](#T-IoC-TT12 'IoC.TT12')
- [TT13](#T-IoC-TT13 'IoC.TT13')
- [TT14](#T-IoC-TT14 'IoC.TT14')
- [TT15](#T-IoC-TT15 'IoC.TT15')
- [TT16](#T-IoC-TT16 'IoC.TT16')
- [TT17](#T-IoC-TT17 'IoC.TT17')
- [TT18](#T-IoC-TT18 'IoC.TT18')
- [TT19](#T-IoC-TT19 'IoC.TT19')
- [TT2](#T-IoC-TT2 'IoC.TT2')
- [TT20](#T-IoC-TT20 'IoC.TT20')
- [TT21](#T-IoC-TT21 'IoC.TT21')
- [TT22](#T-IoC-TT22 'IoC.TT22')
- [TT23](#T-IoC-TT23 'IoC.TT23')
- [TT24](#T-IoC-TT24 'IoC.TT24')
- [TT25](#T-IoC-TT25 'IoC.TT25')
- [TT26](#T-IoC-TT26 'IoC.TT26')
- [TT27](#T-IoC-TT27 'IoC.TT27')
- [TT28](#T-IoC-TT28 'IoC.TT28')
- [TT29](#T-IoC-TT29 'IoC.TT29')
- [TT3](#T-IoC-TT3 'IoC.TT3')
- [TT30](#T-IoC-TT30 'IoC.TT30')
- [TT31](#T-IoC-TT31 'IoC.TT31')
- [TT32](#T-IoC-TT32 'IoC.TT32')
- [TT4](#T-IoC-TT4 'IoC.TT4')
- [TT5](#T-IoC-TT5 'IoC.TT5')
- [TT6](#T-IoC-TT6 'IoC.TT6')
- [TT7](#T-IoC-TT7 'IoC.TT7')
- [TT8](#T-IoC-TT8 'IoC.TT8')
- [TT9](#T-IoC-TT9 'IoC.TT9')
- [TTCollection1\`1](#T-IoC-TTCollection1`1 'IoC.TTCollection1`1')
- [TTCollection2\`1](#T-IoC-TTCollection2`1 'IoC.TTCollection2`1')
- [TTCollection3\`1](#T-IoC-TTCollection3`1 'IoC.TTCollection3`1')
- [TTCollection4\`1](#T-IoC-TTCollection4`1 'IoC.TTCollection4`1')
- [TTCollection5\`1](#T-IoC-TTCollection5`1 'IoC.TTCollection5`1')
- [TTCollection6\`1](#T-IoC-TTCollection6`1 'IoC.TTCollection6`1')
- [TTCollection7\`1](#T-IoC-TTCollection7`1 'IoC.TTCollection7`1')
- [TTCollection8\`1](#T-IoC-TTCollection8`1 'IoC.TTCollection8`1')
- [TTCollection\`1](#T-IoC-TTCollection`1 'IoC.TTCollection`1')
- [TTComparable](#T-IoC-TTComparable 'IoC.TTComparable')
- [TTComparable1](#T-IoC-TTComparable1 'IoC.TTComparable1')
- [TTComparable1\`1](#T-IoC-TTComparable1`1 'IoC.TTComparable1`1')
- [TTComparable2](#T-IoC-TTComparable2 'IoC.TTComparable2')
- [TTComparable2\`1](#T-IoC-TTComparable2`1 'IoC.TTComparable2`1')
- [TTComparable3](#T-IoC-TTComparable3 'IoC.TTComparable3')
- [TTComparable3\`1](#T-IoC-TTComparable3`1 'IoC.TTComparable3`1')
- [TTComparable4](#T-IoC-TTComparable4 'IoC.TTComparable4')
- [TTComparable4\`1](#T-IoC-TTComparable4`1 'IoC.TTComparable4`1')
- [TTComparable5](#T-IoC-TTComparable5 'IoC.TTComparable5')
- [TTComparable5\`1](#T-IoC-TTComparable5`1 'IoC.TTComparable5`1')
- [TTComparable6](#T-IoC-TTComparable6 'IoC.TTComparable6')
- [TTComparable6\`1](#T-IoC-TTComparable6`1 'IoC.TTComparable6`1')
- [TTComparable7](#T-IoC-TTComparable7 'IoC.TTComparable7')
- [TTComparable7\`1](#T-IoC-TTComparable7`1 'IoC.TTComparable7`1')
- [TTComparable8](#T-IoC-TTComparable8 'IoC.TTComparable8')
- [TTComparable8\`1](#T-IoC-TTComparable8`1 'IoC.TTComparable8`1')
- [TTComparable\`1](#T-IoC-TTComparable`1 'IoC.TTComparable`1')
- [TTComparer1\`1](#T-IoC-TTComparer1`1 'IoC.TTComparer1`1')
- [TTComparer2\`1](#T-IoC-TTComparer2`1 'IoC.TTComparer2`1')
- [TTComparer3\`1](#T-IoC-TTComparer3`1 'IoC.TTComparer3`1')
- [TTComparer4\`1](#T-IoC-TTComparer4`1 'IoC.TTComparer4`1')
- [TTComparer5\`1](#T-IoC-TTComparer5`1 'IoC.TTComparer5`1')
- [TTComparer6\`1](#T-IoC-TTComparer6`1 'IoC.TTComparer6`1')
- [TTComparer7\`1](#T-IoC-TTComparer7`1 'IoC.TTComparer7`1')
- [TTComparer8\`1](#T-IoC-TTComparer8`1 'IoC.TTComparer8`1')
- [TTComparer\`1](#T-IoC-TTComparer`1 'IoC.TTComparer`1')
- [TTDictionary1\`2](#T-IoC-TTDictionary1`2 'IoC.TTDictionary1`2')
- [TTDictionary2\`2](#T-IoC-TTDictionary2`2 'IoC.TTDictionary2`2')
- [TTDictionary3\`2](#T-IoC-TTDictionary3`2 'IoC.TTDictionary3`2')
- [TTDictionary4\`2](#T-IoC-TTDictionary4`2 'IoC.TTDictionary4`2')
- [TTDictionary5\`2](#T-IoC-TTDictionary5`2 'IoC.TTDictionary5`2')
- [TTDictionary6\`2](#T-IoC-TTDictionary6`2 'IoC.TTDictionary6`2')
- [TTDictionary7\`2](#T-IoC-TTDictionary7`2 'IoC.TTDictionary7`2')
- [TTDictionary8\`2](#T-IoC-TTDictionary8`2 'IoC.TTDictionary8`2')
- [TTDictionary\`2](#T-IoC-TTDictionary`2 'IoC.TTDictionary`2')
- [TTDisposable](#T-IoC-TTDisposable 'IoC.TTDisposable')
- [TTDisposable1](#T-IoC-TTDisposable1 'IoC.TTDisposable1')
- [TTDisposable2](#T-IoC-TTDisposable2 'IoC.TTDisposable2')
- [TTDisposable3](#T-IoC-TTDisposable3 'IoC.TTDisposable3')
- [TTDisposable4](#T-IoC-TTDisposable4 'IoC.TTDisposable4')
- [TTDisposable5](#T-IoC-TTDisposable5 'IoC.TTDisposable5')
- [TTDisposable6](#T-IoC-TTDisposable6 'IoC.TTDisposable6')
- [TTDisposable7](#T-IoC-TTDisposable7 'IoC.TTDisposable7')
- [TTDisposable8](#T-IoC-TTDisposable8 'IoC.TTDisposable8')
- [TTEnumerable1\`1](#T-IoC-TTEnumerable1`1 'IoC.TTEnumerable1`1')
- [TTEnumerable2\`1](#T-IoC-TTEnumerable2`1 'IoC.TTEnumerable2`1')
- [TTEnumerable3\`1](#T-IoC-TTEnumerable3`1 'IoC.TTEnumerable3`1')
- [TTEnumerable4\`1](#T-IoC-TTEnumerable4`1 'IoC.TTEnumerable4`1')
- [TTEnumerable5\`1](#T-IoC-TTEnumerable5`1 'IoC.TTEnumerable5`1')
- [TTEnumerable6\`1](#T-IoC-TTEnumerable6`1 'IoC.TTEnumerable6`1')
- [TTEnumerable7\`1](#T-IoC-TTEnumerable7`1 'IoC.TTEnumerable7`1')
- [TTEnumerable8\`1](#T-IoC-TTEnumerable8`1 'IoC.TTEnumerable8`1')
- [TTEnumerable\`1](#T-IoC-TTEnumerable`1 'IoC.TTEnumerable`1')
- [TTEnumerator1\`1](#T-IoC-TTEnumerator1`1 'IoC.TTEnumerator1`1')
- [TTEnumerator2\`1](#T-IoC-TTEnumerator2`1 'IoC.TTEnumerator2`1')
- [TTEnumerator3\`1](#T-IoC-TTEnumerator3`1 'IoC.TTEnumerator3`1')
- [TTEnumerator4\`1](#T-IoC-TTEnumerator4`1 'IoC.TTEnumerator4`1')
- [TTEnumerator5\`1](#T-IoC-TTEnumerator5`1 'IoC.TTEnumerator5`1')
- [TTEnumerator6\`1](#T-IoC-TTEnumerator6`1 'IoC.TTEnumerator6`1')
- [TTEnumerator7\`1](#T-IoC-TTEnumerator7`1 'IoC.TTEnumerator7`1')
- [TTEnumerator8\`1](#T-IoC-TTEnumerator8`1 'IoC.TTEnumerator8`1')
- [TTEnumerator\`1](#T-IoC-TTEnumerator`1 'IoC.TTEnumerator`1')
- [TTEqualityComparer1\`1](#T-IoC-TTEqualityComparer1`1 'IoC.TTEqualityComparer1`1')
- [TTEqualityComparer2\`1](#T-IoC-TTEqualityComparer2`1 'IoC.TTEqualityComparer2`1')
- [TTEqualityComparer3\`1](#T-IoC-TTEqualityComparer3`1 'IoC.TTEqualityComparer3`1')
- [TTEqualityComparer4\`1](#T-IoC-TTEqualityComparer4`1 'IoC.TTEqualityComparer4`1')
- [TTEqualityComparer5\`1](#T-IoC-TTEqualityComparer5`1 'IoC.TTEqualityComparer5`1')
- [TTEqualityComparer6\`1](#T-IoC-TTEqualityComparer6`1 'IoC.TTEqualityComparer6`1')
- [TTEqualityComparer7\`1](#T-IoC-TTEqualityComparer7`1 'IoC.TTEqualityComparer7`1')
- [TTEqualityComparer8\`1](#T-IoC-TTEqualityComparer8`1 'IoC.TTEqualityComparer8`1')
- [TTEqualityComparer\`1](#T-IoC-TTEqualityComparer`1 'IoC.TTEqualityComparer`1')
- [TTEquatable1\`1](#T-IoC-TTEquatable1`1 'IoC.TTEquatable1`1')
- [TTEquatable2\`1](#T-IoC-TTEquatable2`1 'IoC.TTEquatable2`1')
- [TTEquatable3\`1](#T-IoC-TTEquatable3`1 'IoC.TTEquatable3`1')
- [TTEquatable4\`1](#T-IoC-TTEquatable4`1 'IoC.TTEquatable4`1')
- [TTEquatable5\`1](#T-IoC-TTEquatable5`1 'IoC.TTEquatable5`1')
- [TTEquatable6\`1](#T-IoC-TTEquatable6`1 'IoC.TTEquatable6`1')
- [TTEquatable7\`1](#T-IoC-TTEquatable7`1 'IoC.TTEquatable7`1')
- [TTEquatable8\`1](#T-IoC-TTEquatable8`1 'IoC.TTEquatable8`1')
- [TTEquatable\`1](#T-IoC-TTEquatable`1 'IoC.TTEquatable`1')
- [TTI](#T-IoC-TTI 'IoC.TTI')
- [TTI1](#T-IoC-TTI1 'IoC.TTI1')
- [TTI10](#T-IoC-TTI10 'IoC.TTI10')
- [TTI11](#T-IoC-TTI11 'IoC.TTI11')
- [TTI12](#T-IoC-TTI12 'IoC.TTI12')
- [TTI13](#T-IoC-TTI13 'IoC.TTI13')
- [TTI14](#T-IoC-TTI14 'IoC.TTI14')
- [TTI15](#T-IoC-TTI15 'IoC.TTI15')
- [TTI16](#T-IoC-TTI16 'IoC.TTI16')
- [TTI17](#T-IoC-TTI17 'IoC.TTI17')
- [TTI18](#T-IoC-TTI18 'IoC.TTI18')
- [TTI19](#T-IoC-TTI19 'IoC.TTI19')
- [TTI2](#T-IoC-TTI2 'IoC.TTI2')
- [TTI20](#T-IoC-TTI20 'IoC.TTI20')
- [TTI21](#T-IoC-TTI21 'IoC.TTI21')
- [TTI22](#T-IoC-TTI22 'IoC.TTI22')
- [TTI23](#T-IoC-TTI23 'IoC.TTI23')
- [TTI24](#T-IoC-TTI24 'IoC.TTI24')
- [TTI25](#T-IoC-TTI25 'IoC.TTI25')
- [TTI26](#T-IoC-TTI26 'IoC.TTI26')
- [TTI27](#T-IoC-TTI27 'IoC.TTI27')
- [TTI28](#T-IoC-TTI28 'IoC.TTI28')
- [TTI29](#T-IoC-TTI29 'IoC.TTI29')
- [TTI3](#T-IoC-TTI3 'IoC.TTI3')
- [TTI30](#T-IoC-TTI30 'IoC.TTI30')
- [TTI31](#T-IoC-TTI31 'IoC.TTI31')
- [TTI32](#T-IoC-TTI32 'IoC.TTI32')
- [TTI4](#T-IoC-TTI4 'IoC.TTI4')
- [TTI5](#T-IoC-TTI5 'IoC.TTI5')
- [TTI6](#T-IoC-TTI6 'IoC.TTI6')
- [TTI7](#T-IoC-TTI7 'IoC.TTI7')
- [TTI8](#T-IoC-TTI8 'IoC.TTI8')
- [TTI9](#T-IoC-TTI9 'IoC.TTI9')
- [TTList1\`1](#T-IoC-TTList1`1 'IoC.TTList1`1')
- [TTList2\`1](#T-IoC-TTList2`1 'IoC.TTList2`1')
- [TTList3\`1](#T-IoC-TTList3`1 'IoC.TTList3`1')
- [TTList4\`1](#T-IoC-TTList4`1 'IoC.TTList4`1')
- [TTList5\`1](#T-IoC-TTList5`1 'IoC.TTList5`1')
- [TTList6\`1](#T-IoC-TTList6`1 'IoC.TTList6`1')
- [TTList7\`1](#T-IoC-TTList7`1 'IoC.TTList7`1')
- [TTList8\`1](#T-IoC-TTList8`1 'IoC.TTList8`1')
- [TTList\`1](#T-IoC-TTList`1 'IoC.TTList`1')
- [TTObservable1\`1](#T-IoC-TTObservable1`1 'IoC.TTObservable1`1')
- [TTObservable2\`1](#T-IoC-TTObservable2`1 'IoC.TTObservable2`1')
- [TTObservable3\`1](#T-IoC-TTObservable3`1 'IoC.TTObservable3`1')
- [TTObservable4\`1](#T-IoC-TTObservable4`1 'IoC.TTObservable4`1')
- [TTObservable5\`1](#T-IoC-TTObservable5`1 'IoC.TTObservable5`1')
- [TTObservable6\`1](#T-IoC-TTObservable6`1 'IoC.TTObservable6`1')
- [TTObservable7\`1](#T-IoC-TTObservable7`1 'IoC.TTObservable7`1')
- [TTObservable8\`1](#T-IoC-TTObservable8`1 'IoC.TTObservable8`1')
- [TTObservable\`1](#T-IoC-TTObservable`1 'IoC.TTObservable`1')
- [TTObserver1\`1](#T-IoC-TTObserver1`1 'IoC.TTObserver1`1')
- [TTObserver2\`1](#T-IoC-TTObserver2`1 'IoC.TTObserver2`1')
- [TTObserver3\`1](#T-IoC-TTObserver3`1 'IoC.TTObserver3`1')
- [TTObserver4\`1](#T-IoC-TTObserver4`1 'IoC.TTObserver4`1')
- [TTObserver5\`1](#T-IoC-TTObserver5`1 'IoC.TTObserver5`1')
- [TTObserver6\`1](#T-IoC-TTObserver6`1 'IoC.TTObserver6`1')
- [TTObserver7\`1](#T-IoC-TTObserver7`1 'IoC.TTObserver7`1')
- [TTObserver8\`1](#T-IoC-TTObserver8`1 'IoC.TTObserver8`1')
- [TTObserver\`1](#T-IoC-TTObserver`1 'IoC.TTObserver`1')
- [TTS](#T-IoC-TTS 'IoC.TTS')
- [TTS1](#T-IoC-TTS1 'IoC.TTS1')
- [TTS10](#T-IoC-TTS10 'IoC.TTS10')
- [TTS11](#T-IoC-TTS11 'IoC.TTS11')
- [TTS12](#T-IoC-TTS12 'IoC.TTS12')
- [TTS13](#T-IoC-TTS13 'IoC.TTS13')
- [TTS14](#T-IoC-TTS14 'IoC.TTS14')
- [TTS15](#T-IoC-TTS15 'IoC.TTS15')
- [TTS16](#T-IoC-TTS16 'IoC.TTS16')
- [TTS17](#T-IoC-TTS17 'IoC.TTS17')
- [TTS18](#T-IoC-TTS18 'IoC.TTS18')
- [TTS19](#T-IoC-TTS19 'IoC.TTS19')
- [TTS2](#T-IoC-TTS2 'IoC.TTS2')
- [TTS20](#T-IoC-TTS20 'IoC.TTS20')
- [TTS21](#T-IoC-TTS21 'IoC.TTS21')
- [TTS22](#T-IoC-TTS22 'IoC.TTS22')
- [TTS23](#T-IoC-TTS23 'IoC.TTS23')
- [TTS24](#T-IoC-TTS24 'IoC.TTS24')
- [TTS25](#T-IoC-TTS25 'IoC.TTS25')
- [TTS26](#T-IoC-TTS26 'IoC.TTS26')
- [TTS27](#T-IoC-TTS27 'IoC.TTS27')
- [TTS28](#T-IoC-TTS28 'IoC.TTS28')
- [TTS29](#T-IoC-TTS29 'IoC.TTS29')
- [TTS3](#T-IoC-TTS3 'IoC.TTS3')
- [TTS30](#T-IoC-TTS30 'IoC.TTS30')
- [TTS31](#T-IoC-TTS31 'IoC.TTS31')
- [TTS32](#T-IoC-TTS32 'IoC.TTS32')
- [TTS4](#T-IoC-TTS4 'IoC.TTS4')
- [TTS5](#T-IoC-TTS5 'IoC.TTS5')
- [TTS6](#T-IoC-TTS6 'IoC.TTS6')
- [TTS7](#T-IoC-TTS7 'IoC.TTS7')
- [TTS8](#T-IoC-TTS8 'IoC.TTS8')
- [TTS9](#T-IoC-TTS9 'IoC.TTS9')
- [TTSet1\`1](#T-IoC-TTSet1`1 'IoC.TTSet1`1')
- [TTSet2\`1](#T-IoC-TTSet2`1 'IoC.TTSet2`1')
- [TTSet3\`1](#T-IoC-TTSet3`1 'IoC.TTSet3`1')
- [TTSet4\`1](#T-IoC-TTSet4`1 'IoC.TTSet4`1')
- [TTSet5\`1](#T-IoC-TTSet5`1 'IoC.TTSet5`1')
- [TTSet6\`1](#T-IoC-TTSet6`1 'IoC.TTSet6`1')
- [TTSet7\`1](#T-IoC-TTSet7`1 'IoC.TTSet7`1')
- [TTSet8\`1](#T-IoC-TTSet8`1 'IoC.TTSet8`1')
- [TTSet\`1](#T-IoC-TTSet`1 'IoC.TTSet`1')
- [Tag](#T-IoC-Tag 'IoC.Tag')
  - [ToString()](#M-IoC-Tag-ToString 'IoC.Tag.ToString')
- [TaskFeature](#T-IoC-Features-TaskFeature 'IoC.Features.TaskFeature')
  - [Default](#F-IoC-Features-TaskFeature-Default 'IoC.Features.TaskFeature.Default')
  - [Tag](#F-IoC-Features-TaskFeature-Tag 'IoC.Features.TaskFeature.Tag')
  - [Apply()](#M-IoC-Features-TaskFeature-Apply-IoC-IMutableContainer- 'IoC.Features.TaskFeature.Apply(IoC.IMutableContainer)')
- [TerminatesProgramAttribute](#T-IoC-TerminatesProgramAttribute 'IoC.TerminatesProgramAttribute')
- [TraceEvent](#T-IoC-TraceEvent 'IoC.TraceEvent')
  - [#ctor(containerEvent,message)](#M-IoC-TraceEvent-#ctor-IoC-ContainerEvent,System-String- 'IoC.TraceEvent.#ctor(IoC.ContainerEvent,System.String)')
  - [ContainerEvent](#F-IoC-TraceEvent-ContainerEvent 'IoC.TraceEvent.ContainerEvent')
  - [Message](#F-IoC-TraceEvent-Message 'IoC.TraceEvent.Message')
- [TupleFeature](#T-IoC-Features-TupleFeature 'IoC.Features.TupleFeature')
  - [Default](#F-IoC-Features-TupleFeature-Default 'IoC.Features.TupleFeature.Default')
  - [Light](#F-IoC-Features-TupleFeature-Light 'IoC.Features.TupleFeature.Light')
  - [Apply()](#M-IoC-Features-TupleFeature-Apply-IoC-IMutableContainer- 'IoC.Features.TupleFeature.Apply(IoC.IMutableContainer)')
- [UsedImplicitlyAttribute](#T-IoC-UsedImplicitlyAttribute 'IoC.UsedImplicitlyAttribute')
- [ValueProviderAttribute](#T-IoC-ValueProviderAttribute 'IoC.ValueProviderAttribute')
- [WellknownExpressions](#T-IoC-WellknownExpressions 'IoC.WellknownExpressions')
  - [ArgsParameter](#F-IoC-WellknownExpressions-ArgsParameter 'IoC.WellknownExpressions.ArgsParameter')
  - [ContainerParameter](#F-IoC-WellknownExpressions-ContainerParameter 'IoC.WellknownExpressions.ContainerParameter')
- [XamlItemBindingOfItemsControlAttribute](#T-IoC-XamlItemBindingOfItemsControlAttribute 'IoC.XamlItemBindingOfItemsControlAttribute')
- [XamlItemsControlAttribute](#T-IoC-XamlItemsControlAttribute 'IoC.XamlItemsControlAttribute')

<a name='T-IoC-AspMvcActionAttribute'></a>
## AspMvcActionAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
is an MVC action. If applied to a method, the MVC action name is calculated
implicitly from the context. Use this attribute for custom wrappers similar to
`System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)`.

<a name='T-IoC-AspMvcActionSelectorAttribute'></a>
## AspMvcActionSelectorAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. When applied to a parameter of an attribute,
indicates that this parameter is an MVC action name.

##### Example

```
[ActionName("Foo")]
public ActionResult Login(string returnUrl) {
  ViewBag.ReturnUrl = Url.Action("Foo"); // OK
  return RedirectToAction("Bar"); // Error: Cannot resolve action
}
```

<a name='T-IoC-AspMvcAreaAttribute'></a>
## AspMvcAreaAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC area.
Use this attribute for custom wrappers similar to
`System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)`.

<a name='T-IoC-AspMvcControllerAttribute'></a>
## AspMvcControllerAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is
an MVC controller. If applied to a method, the MVC controller name is calculated
implicitly from the context. Use this attribute for custom wrappers similar to
`System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String, String)`.

<a name='T-IoC-AspMvcDisplayTemplateAttribute'></a>
## AspMvcDisplayTemplateAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC display template.
Use this attribute for custom wrappers similar to 
`System.Web.Mvc.Html.DisplayExtensions.DisplayForModel(HtmlHelper, String)`.

<a name='T-IoC-AspMvcEditorTemplateAttribute'></a>
## AspMvcEditorTemplateAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC editor template.
Use this attribute for custom wrappers similar to
`System.Web.Mvc.Html.EditorExtensions.EditorForModel(HtmlHelper, String)`.

<a name='T-IoC-AspMvcMasterAttribute'></a>
## AspMvcMasterAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC Master. Use this attribute
for custom wrappers similar to `System.Web.Mvc.Controller.View(String, String)`.

<a name='T-IoC-AspMvcModelTypeAttribute'></a>
## AspMvcModelTypeAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC model type. Use this attribute
for custom wrappers similar to `System.Web.Mvc.Controller.View(String, Object)`.

<a name='T-IoC-AspMvcPartialViewAttribute'></a>
## AspMvcPartialViewAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC
partial view. If applied to a method, the MVC partial view name is calculated implicitly
from the context. Use this attribute for custom wrappers similar to
`System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(HtmlHelper, String)`.

<a name='T-IoC-AspMvcSuppressViewErrorAttribute'></a>
## AspMvcSuppressViewErrorAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Allows disabling inspections for MVC views within a class or a method.

<a name='T-IoC-AspMvcTemplateAttribute'></a>
## AspMvcTemplateAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC template.
Use this attribute for custom wrappers similar to
`System.ComponentModel.DataAnnotations.UIHintAttribute(System.String)`.

<a name='T-IoC-AspMvcViewAttribute'></a>
## AspMvcViewAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
is an MVC view component. If applied to a method, the MVC view name is calculated implicitly
from the context. Use this attribute for custom wrappers similar to
`System.Web.Mvc.Controller.View(Object)`.

<a name='T-IoC-AspMvcViewComponentAttribute'></a>
## AspMvcViewComponentAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
is an MVC view component name.

<a name='T-IoC-AspMvcViewComponentViewAttribute'></a>
## AspMvcViewComponentViewAttribute `type`

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
is an MVC view component view. If applied to a method, the MVC view component view name is default.

<a name='T-IoC-Core-AspectOrientedAutowiringStrategy'></a>
## AspectOrientedAutowiringStrategy `type`

##### Namespace

IoC.Core

<a name='M-IoC-Core-AspectOrientedAutowiringStrategy-TryResolveConstructor-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-ConstructorInfo}},IoC-IMethod{System-Reflection-ConstructorInfo}@-'></a>
### TryResolveConstructor() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Core-AspectOrientedAutowiringStrategy-TryResolveInitializers-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}},System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}}@-'></a>
### TryResolveInitializers() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Core-AspectOrientedAutowiringStrategy-TryResolveType-System-Type,System-Type,System-Type@-'></a>
### TryResolveType() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Core-AspectOrientedMetadata'></a>
## AspectOrientedMetadata `type`

##### Namespace

IoC.Core

##### Summary

Metadata for aspect oriented autowiring strategy.

<a name='M-IoC-Core-AspectOrientedMetadata-TryResolveConstructor-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-ConstructorInfo}},IoC-IMethod{System-Reflection-ConstructorInfo}@-'></a>
### TryResolveConstructor() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Core-AspectOrientedMetadata-TryResolveInitializers-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}},System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}}@-'></a>
### TryResolveInitializers() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Core-AspectOrientedMetadata-TryResolveType-System-Type,System-Type,System-Type@-'></a>
### TryResolveType() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-AssertionConditionAttribute'></a>
## AssertionConditionAttribute `type`

##### Namespace

IoC

##### Summary

Indicates the condition parameter of the assertion method. The method itself should be
marked by [AssertionMethodAttribute](#T-IoC-AssertionMethodAttribute 'IoC.AssertionMethodAttribute') attribute. The mandatory argument of
the attribute is the assertion type.

<a name='T-IoC-AssertionConditionType'></a>
## AssertionConditionType `type`

##### Namespace

IoC

##### Summary

Specifies assertion type. If the assertion method argument satisfies the condition,
then the execution continues. Otherwise, execution is assumed to be halted.

<a name='F-IoC-AssertionConditionType-IS_FALSE'></a>
### IS_FALSE `constants`

##### Summary

Marked parameter should be evaluated to false.

<a name='F-IoC-AssertionConditionType-IS_NOT_NULL'></a>
### IS_NOT_NULL `constants`

##### Summary

Marked parameter should be evaluated to not null value.

<a name='F-IoC-AssertionConditionType-IS_NULL'></a>
### IS_NULL `constants`

##### Summary

Marked parameter should be evaluated to null value.

<a name='F-IoC-AssertionConditionType-IS_TRUE'></a>
### IS_TRUE `constants`

##### Summary

Marked parameter should be evaluated to true.

<a name='T-IoC-AssertionMethodAttribute'></a>
## AssertionMethodAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that the marked method is assertion method, i.e. it halts control flow if
one of the conditions is satisfied. To set the condition, mark one of the parameters with 
[AssertionConditionAttribute](#T-IoC-AssertionConditionAttribute 'IoC.AssertionConditionAttribute') attribute.

<a name='T-IoC-AutowiringStrategies'></a>
## AutowiringStrategies `type`

##### Namespace

IoC

##### Summary

Provides autowiring strategies.

<a name='M-IoC-AutowiringStrategies-AspectOriented'></a>
### AspectOriented() `method`

##### Summary

Create an aspect oriented autowiring strategy.

##### Returns

The instance of aspect oriented autowiring strategy.

##### Parameters

This method has no parameters.

<a name='M-IoC-AutowiringStrategies-Order``1-IoC-IAutowiringStrategy,System-Func{``0,System-IComparable}-'></a>
### Order\`\`1(strategy,orderSelector) `method`

##### Summary

Specify an order selector for an aspect oriented autowiring strategy.

##### Returns

The instance of aspect oriented autowiring strategy.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| strategy | [IoC.IAutowiringStrategy](#T-IoC-IAutowiringStrategy 'IoC.IAutowiringStrategy') | The base aspect oriented autowiring strategy. |
| orderSelector | [System.Func{\`\`0,System.IComparable}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,System.IComparable}') | The type selector. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TOrderAttribute | The order metadata attribute. |

<a name='M-IoC-AutowiringStrategies-Tag``1-IoC-IAutowiringStrategy,System-Func{``0,System-Object}-'></a>
### Tag\`\`1(strategy,tagSelector) `method`

##### Summary

Specify a tag selector for an aspect oriented autowiring strategy.

##### Returns

The instance of aspect oriented autowiring strategy.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| strategy | [IoC.IAutowiringStrategy](#T-IoC-IAutowiringStrategy 'IoC.IAutowiringStrategy') | The base aspect oriented autowiring strategy. |
| tagSelector | [System.Func{\`\`0,System.Object}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,System.Object}') | The tag selector. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TTagAttribute | The tag metadata attribute. |

<a name='M-IoC-AutowiringStrategies-Type``1-IoC-IAutowiringStrategy,System-Func{``0,System-Type}-'></a>
### Type\`\`1(strategy,typeSelector) `method`

##### Summary

Specify a type selector for an aspect oriented autowiring strategy.

##### Returns

The instance of aspect oriented autowiring strategy.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| strategy | [IoC.IAutowiringStrategy](#T-IoC-IAutowiringStrategy 'IoC.IAutowiringStrategy') | The base aspect oriented autowiring strategy. |
| typeSelector | [System.Func{\`\`0,System.Type}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,System.Type}') | The type selector. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TTypeAttribute | The type metadata attribute. |

<a name='T-IoC-BaseTypeRequiredAttribute'></a>
## BaseTypeRequiredAttribute `type`

##### Namespace

IoC

##### Summary

When applied to a target attribute, specifies a requirement for any type marked
with the target attribute to implement or inherit specific type or types.

##### Example

```
[BaseTypeRequired(typeof(IComponent)] // Specify requirement
class ComponentAttribute : Attribute { }
[Component] // ComponentAttribute requires implementing IComponent interface
class MyComponent : IComponent { }
```

<a name='T-IoC-Core-BuildContext'></a>
## BuildContext `type`

##### Namespace

IoC.Core

##### Summary

Represents build context.

<a name='T-IoC-CanBeNullAttribute'></a>
## CanBeNullAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that the value of the marked element could be `null` sometimes,
so the check for `null` is necessary before its usage.

##### Example

```
[CanBeNull] object Test() =&gt; null;
void UseTest() {
  var p = Test();
  var s = p.ToString(); // Warning: Possible 'System.NullReferenceException'
}
```

<a name='T-IoC-CannotApplyEqualityOperatorAttribute'></a>
## CannotApplyEqualityOperatorAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that the value of the marked type (or its derivatives)
cannot be compared using '==' or '!=' operators and `Equals()`
should be used instead. However, using '==' or '!=' for comparison
with `null` is always permitted.

##### Example

```
[CannotApplyEqualityOperator]
class NoEquality { }
class UsesNoEquality {
  void Test() {
    var ca1 = new NoEquality();
    var ca2 = new NoEquality();
    if (ca1 != null) { // OK
      bool condition = ca1 == ca2; // Warning
    }
  }
}
```

<a name='T-IoC-CollectionAccessAttribute'></a>
## CollectionAccessAttribute `type`

##### Namespace

IoC

##### Summary

Indicates how method, constructor invocation or property access
over collection type affects content of the collection.

<a name='T-IoC-CollectionAccessType'></a>
## CollectionAccessType `type`

##### Namespace

IoC

<a name='F-IoC-CollectionAccessType-ModifyExistingContent'></a>
### ModifyExistingContent `constants`

##### Summary

Method can change content of the collection but does not add new elements.

<a name='F-IoC-CollectionAccessType-None'></a>
### None `constants`

##### Summary

Method does not use or modify content of the collection.

<a name='F-IoC-CollectionAccessType-Read'></a>
### Read `constants`

##### Summary

Method only reads content of the collection but does not modify it.

<a name='F-IoC-CollectionAccessType-UpdatedContent'></a>
### UpdatedContent `constants`

##### Summary

Method can add new elements to the collection.

<a name='T-IoC-Features-CollectionFeature'></a>
## CollectionFeature `type`

##### Namespace

IoC.Features

##### Summary

Allows to resolve enumeration of all instances related to corresponding bindings.

<a name='F-IoC-Features-CollectionFeature-Default'></a>
### Default `constants`

<a name='M-IoC-Features-CollectionFeature-Apply-IoC-IMutableContainer-'></a>
### Apply() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Features-ConfigurationFeature'></a>
## ConfigurationFeature `type`

##### Namespace

IoC.Features

##### Summary

Allows to configure via a text metadata.

<a name='F-IoC-Features-ConfigurationFeature-Default'></a>
### Default `constants`

##### Summary

The default instance.

<a name='M-IoC-Features-ConfigurationFeature-Apply-IoC-IMutableContainer-'></a>
### Apply() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Container'></a>
## Container `type`

##### Namespace

IoC

##### Summary

The base IoC container implementation.

<a name='P-IoC-Container-Parent'></a>
### Parent `property`

##### Summary

*Inherit from parent.*

<a name='M-IoC-Container-Create-System-String,System-Collections-Generic-IEnumerable{IoC-IConfiguration}-'></a>
### Create(name,configurations) `method`

##### Summary

Creates a root container with default features.

##### Returns

The root container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The optional name of the container. |
| configurations | [System.Collections.Generic.IEnumerable{IoC.IConfiguration}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{IoC.IConfiguration}') |  |

<a name='M-IoC-Container-Dispose'></a>
### Dispose() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-GetEnumerator'></a>
### GetEnumerator() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-RegisterResource-System-IDisposable-'></a>
### RegisterResource() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-Subscribe-System-IObserver{IoC-ContainerEvent}-'></a>
### Subscribe() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-TryGetDependency-IoC-Key,IoC-IDependency@,IoC-ILifetime@-'></a>
### TryGetDependency() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-TryGetResolver``1-System-Type,System-Object,IoC-Resolver{``0}@,System-Exception@,IoC-IContainer-'></a>
### TryGetResolver\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-TryRegisterDependency-System-Collections-Generic-IEnumerable{IoC-Key},IoC-IDependency,IoC-ILifetime,IoC-IToken@-'></a>
### TryRegisterDependency() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-UnregisterResource-System-IDisposable-'></a>
### UnregisterResource() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-ContainerEvent'></a>
## ContainerEvent `type`

##### Namespace

IoC

##### Summary

Provides information about changes in the container.

<a name='M-IoC-ContainerEvent-#ctor-IoC-IContainer,IoC-EventType-'></a>
### #ctor(container,eventType) `constructor`

##### Summary

Creates new instance of container event.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The origin container. |
| eventType | [IoC.EventType](#T-IoC-EventType 'IoC.EventType') | The event type. |

<a name='F-IoC-ContainerEvent-Container'></a>
### Container `constants`

##### Summary

The target container.

<a name='F-IoC-ContainerEvent-Dependency'></a>
### Dependency `constants`

##### Summary

Related dependency.

<a name='F-IoC-ContainerEvent-Error'></a>
### Error `constants`

##### Summary

Error during operation.

<a name='F-IoC-ContainerEvent-EventType'></a>
### EventType `constants`

##### Summary

The type of event.

<a name='F-IoC-ContainerEvent-IsSuccess'></a>
### IsSuccess `constants`

##### Summary

True if it is success.

<a name='F-IoC-ContainerEvent-Keys'></a>
### Keys `constants`

##### Summary

The changed keys.

<a name='F-IoC-ContainerEvent-Lifetime'></a>
### Lifetime `constants`

##### Summary

Related lifetime.

<a name='F-IoC-ContainerEvent-ResolverExpression'></a>
### ResolverExpression `constants`

##### Summary

Related lifetime.

<a name='T-IoC-Lifetimes-ContainerSingletonLifetime'></a>
## ContainerSingletonLifetime `type`

##### Namespace

IoC.Lifetimes

##### Summary

For a singleton instance per container.

<a name='M-IoC-Lifetimes-ContainerSingletonLifetime-Create'></a>
### Create() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ContainerSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]-'></a>
### CreateKey() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ContainerSingletonLifetime-OnInstanceReleased-System-Object,IoC-IContainer-'></a>
### OnInstanceReleased() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ContainerSingletonLifetime-OnNewInstanceCreated``1-``0,IoC-IContainer,IoC-IContainer,System-Object[]-'></a>
### OnNewInstanceCreated\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ContainerSingletonLifetime-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Context'></a>
## Context `type`

##### Namespace

IoC

##### Summary

Represents the resolving context.

<a name='F-IoC-Context-Args'></a>
### Args `constants`

##### Summary

The optional resolving arguments.

<a name='F-IoC-Context-Container'></a>
### Container `constants`

##### Summary

The resolving container.

<a name='F-IoC-Context-Key'></a>
### Key `constants`

##### Summary

The resolving key.

<a name='T-IoC-Context`1'></a>
## Context\`1 `type`

##### Namespace

IoC

##### Summary

Represents the initializing context.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='F-IoC-Context`1-It'></a>
### It `constants`

##### Summary

The resolved instance.

<a name='T-IoC-ContractAnnotationAttribute'></a>
## ContractAnnotationAttribute `type`

##### Namespace

IoC

##### Summary

Describes dependency between method input and output.

<a name='T-IoC-Features-CoreFeature'></a>
## CoreFeature `type`

##### Namespace

IoC.Features

##### Summary

Adds the set of core features like lifetimes and containers.

<a name='F-IoC-Features-CoreFeature-Default'></a>
### Default `constants`

<a name='M-IoC-Features-CoreFeature-Apply-IoC-IMutableContainer-'></a>
### Apply() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Issues-DependencyDescription'></a>
## DependencyDescription `type`

##### Namespace

IoC.Issues

##### Summary

Represents the dependency.

<a name='M-IoC-Issues-DependencyDescription-#ctor-IoC-IDependency,IoC-ILifetime-'></a>
### #ctor(dependency,lifetime) `constructor`

##### Summary

Creates a new instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dependency | [IoC.IDependency](#T-IoC-IDependency 'IoC.IDependency') | The resolved dependency. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The lifetime to use |

<a name='F-IoC-Issues-DependencyDescription-Dependency'></a>
### Dependency `constants`

##### Summary

The resolved dependency.

<a name='F-IoC-Issues-DependencyDescription-Lifetime'></a>
### Lifetime `constants`

##### Summary

The lifetime to use.

<a name='T-IoC-Core-DependencyEntry'></a>
## DependencyEntry `type`

##### Namespace

IoC.Core

<a name='F-IoC-Core-DependencyEntry-ResolverParameters'></a>
### ResolverParameters `constants`

##### Summary

All resolvers parameters.

<a name='T-IoC-EventType'></a>
## EventType `type`

##### Namespace

IoC

##### Summary

Container event types.

<a name='F-IoC-EventType-CreateContainer'></a>
### CreateContainer `constants`

##### Summary

On container creation.

<a name='F-IoC-EventType-DisposeContainer'></a>
### DisposeContainer `constants`

##### Summary

On container disposing.

<a name='F-IoC-EventType-RegisterDependency'></a>
### RegisterDependency `constants`

##### Summary

On dependency registration.

<a name='F-IoC-EventType-ResolverCompilation'></a>
### ResolverCompilation `constants`

##### Summary

On resolver compilation.

<a name='F-IoC-EventType-UnregisterDependency'></a>
### UnregisterDependency `constants`

##### Summary

On dependency unregistration.

<a name='T-IoC-FluentBind'></a>
## FluentBind `type`

##### Namespace

IoC

##### Summary

Represents extensions to add bindings to the container.

<a name='M-IoC-FluentBind-AnyTag``1-IoC-IBinding{``0}-'></a>
### AnyTag\`\`1(binding) `method`

##### Summary

Marks the binding to be used for any tags.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding{\`\`0}](#T-IoC-IBinding{``0} 'IoC.IBinding{``0}') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentBind-As``1-IoC-IBinding{``0},IoC-Lifetime-'></a>
### As\`\`1(binding,lifetime) `method`

##### Summary

Assigns well-known lifetime to the binding.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding{\`\`0}](#T-IoC-IBinding{``0} 'IoC.IBinding{``0}') |  |
| lifetime | [IoC.Lifetime](#T-IoC-Lifetime 'IoC.Lifetime') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentBind-Autowiring``1-IoC-IBinding{``0},IoC-IAutowiringStrategy-'></a>
### Autowiring\`\`1(binding,autowiringStrategy) `method`

##### Summary

Assigns the autowiring strategy to the binding.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding{\`\`0}](#T-IoC-IBinding{``0} 'IoC.IBinding{``0}') |  |
| autowiringStrategy | [IoC.IAutowiringStrategy](#T-IoC-IAutowiringStrategy 'IoC.IAutowiringStrategy') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentBind-Bind-IoC-IMutableContainer,System-Type[]-'></a>
### Bind(container,types) `method`

##### Summary

Binds types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| types | [System.Type[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type[] 'System.Type[]') |  |

<a name='M-IoC-FluentBind-Bind-IoC-IToken,System-Type[]-'></a>
### Bind(token,types) `method`

##### Summary

Binds types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The container binding token. |
| types | [System.Type[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type[] 'System.Type[]') |  |

<a name='M-IoC-FluentBind-Bind``1-IoC-IMutableContainer-'></a>
### Bind\`\`1(container) `method`

##### Summary

Binds the type.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The contract type. |

<a name='M-IoC-FluentBind-Bind``1-IoC-IToken-'></a>
### Bind\`\`1(token) `method`

##### Summary

Binds the type.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The container binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The contract type. |

<a name='M-IoC-FluentBind-Bind``1-IoC-IBinding-'></a>
### Bind\`\`1(binding) `method`

##### Summary

Binds the type.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The contract type. |

<a name='M-IoC-FluentBind-Bind``10-IoC-IMutableContainer-'></a>
### Bind\`\`10(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |

<a name='M-IoC-FluentBind-Bind``10-IoC-IBinding-'></a>
### Bind\`\`10(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |

<a name='M-IoC-FluentBind-Bind``10-IoC-IToken-'></a>
### Bind\`\`10(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |

<a name='M-IoC-FluentBind-Bind``11-IoC-IMutableContainer-'></a>
### Bind\`\`11(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |

<a name='M-IoC-FluentBind-Bind``11-IoC-IBinding-'></a>
### Bind\`\`11(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |

<a name='M-IoC-FluentBind-Bind``11-IoC-IToken-'></a>
### Bind\`\`11(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |

<a name='M-IoC-FluentBind-Bind``12-IoC-IMutableContainer-'></a>
### Bind\`\`12(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |

<a name='M-IoC-FluentBind-Bind``12-IoC-IBinding-'></a>
### Bind\`\`12(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |

<a name='M-IoC-FluentBind-Bind``12-IoC-IToken-'></a>
### Bind\`\`12(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |

<a name='M-IoC-FluentBind-Bind``13-IoC-IMutableContainer-'></a>
### Bind\`\`13(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |

<a name='M-IoC-FluentBind-Bind``13-IoC-IBinding-'></a>
### Bind\`\`13(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |

<a name='M-IoC-FluentBind-Bind``13-IoC-IToken-'></a>
### Bind\`\`13(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |

<a name='M-IoC-FluentBind-Bind``14-IoC-IMutableContainer-'></a>
### Bind\`\`14(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |

<a name='M-IoC-FluentBind-Bind``14-IoC-IBinding-'></a>
### Bind\`\`14(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |

<a name='M-IoC-FluentBind-Bind``14-IoC-IToken-'></a>
### Bind\`\`14(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |

<a name='M-IoC-FluentBind-Bind``15-IoC-IMutableContainer-'></a>
### Bind\`\`15(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |

<a name='M-IoC-FluentBind-Bind``15-IoC-IBinding-'></a>
### Bind\`\`15(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |

<a name='M-IoC-FluentBind-Bind``15-IoC-IToken-'></a>
### Bind\`\`15(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |

<a name='M-IoC-FluentBind-Bind``16-IoC-IMutableContainer-'></a>
### Bind\`\`16(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |

<a name='M-IoC-FluentBind-Bind``16-IoC-IBinding-'></a>
### Bind\`\`16(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |

<a name='M-IoC-FluentBind-Bind``16-IoC-IToken-'></a>
### Bind\`\`16(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |

<a name='M-IoC-FluentBind-Bind``17-IoC-IMutableContainer-'></a>
### Bind\`\`17(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |

<a name='M-IoC-FluentBind-Bind``17-IoC-IBinding-'></a>
### Bind\`\`17(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |

<a name='M-IoC-FluentBind-Bind``17-IoC-IToken-'></a>
### Bind\`\`17(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |

<a name='M-IoC-FluentBind-Bind``18-IoC-IMutableContainer-'></a>
### Bind\`\`18(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |

<a name='M-IoC-FluentBind-Bind``18-IoC-IBinding-'></a>
### Bind\`\`18(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |

<a name='M-IoC-FluentBind-Bind``18-IoC-IToken-'></a>
### Bind\`\`18(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |

<a name='M-IoC-FluentBind-Bind``19-IoC-IMutableContainer-'></a>
### Bind\`\`19(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |

<a name='M-IoC-FluentBind-Bind``19-IoC-IBinding-'></a>
### Bind\`\`19(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |

<a name='M-IoC-FluentBind-Bind``19-IoC-IToken-'></a>
### Bind\`\`19(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |

<a name='M-IoC-FluentBind-Bind``2-IoC-IMutableContainer-'></a>
### Bind\`\`2(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |

<a name='M-IoC-FluentBind-Bind``2-IoC-IBinding-'></a>
### Bind\`\`2(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |

<a name='M-IoC-FluentBind-Bind``2-IoC-IToken-'></a>
### Bind\`\`2(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |

<a name='M-IoC-FluentBind-Bind``20-IoC-IMutableContainer-'></a>
### Bind\`\`20(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |

<a name='M-IoC-FluentBind-Bind``20-IoC-IBinding-'></a>
### Bind\`\`20(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |

<a name='M-IoC-FluentBind-Bind``20-IoC-IToken-'></a>
### Bind\`\`20(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |

<a name='M-IoC-FluentBind-Bind``21-IoC-IMutableContainer-'></a>
### Bind\`\`21(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |

<a name='M-IoC-FluentBind-Bind``21-IoC-IBinding-'></a>
### Bind\`\`21(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |

<a name='M-IoC-FluentBind-Bind``21-IoC-IToken-'></a>
### Bind\`\`21(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |

<a name='M-IoC-FluentBind-Bind``22-IoC-IMutableContainer-'></a>
### Bind\`\`22(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |

<a name='M-IoC-FluentBind-Bind``22-IoC-IBinding-'></a>
### Bind\`\`22(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |

<a name='M-IoC-FluentBind-Bind``22-IoC-IToken-'></a>
### Bind\`\`22(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |

<a name='M-IoC-FluentBind-Bind``23-IoC-IMutableContainer-'></a>
### Bind\`\`23(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |

<a name='M-IoC-FluentBind-Bind``23-IoC-IBinding-'></a>
### Bind\`\`23(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |

<a name='M-IoC-FluentBind-Bind``23-IoC-IToken-'></a>
### Bind\`\`23(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |

<a name='M-IoC-FluentBind-Bind``24-IoC-IMutableContainer-'></a>
### Bind\`\`24(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |

<a name='M-IoC-FluentBind-Bind``24-IoC-IBinding-'></a>
### Bind\`\`24(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |

<a name='M-IoC-FluentBind-Bind``24-IoC-IToken-'></a>
### Bind\`\`24(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |

<a name='M-IoC-FluentBind-Bind``25-IoC-IMutableContainer-'></a>
### Bind\`\`25(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |

<a name='M-IoC-FluentBind-Bind``25-IoC-IBinding-'></a>
### Bind\`\`25(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |

<a name='M-IoC-FluentBind-Bind``25-IoC-IToken-'></a>
### Bind\`\`25(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |

<a name='M-IoC-FluentBind-Bind``26-IoC-IMutableContainer-'></a>
### Bind\`\`26(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |

<a name='M-IoC-FluentBind-Bind``26-IoC-IBinding-'></a>
### Bind\`\`26(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |

<a name='M-IoC-FluentBind-Bind``26-IoC-IToken-'></a>
### Bind\`\`26(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |

<a name='M-IoC-FluentBind-Bind``27-IoC-IMutableContainer-'></a>
### Bind\`\`27(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |

<a name='M-IoC-FluentBind-Bind``27-IoC-IBinding-'></a>
### Bind\`\`27(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |

<a name='M-IoC-FluentBind-Bind``27-IoC-IToken-'></a>
### Bind\`\`27(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |

<a name='M-IoC-FluentBind-Bind``28-IoC-IMutableContainer-'></a>
### Bind\`\`28(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |

<a name='M-IoC-FluentBind-Bind``28-IoC-IBinding-'></a>
### Bind\`\`28(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |

<a name='M-IoC-FluentBind-Bind``28-IoC-IToken-'></a>
### Bind\`\`28(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |

<a name='M-IoC-FluentBind-Bind``29-IoC-IMutableContainer-'></a>
### Bind\`\`29(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |

<a name='M-IoC-FluentBind-Bind``29-IoC-IBinding-'></a>
### Bind\`\`29(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |

<a name='M-IoC-FluentBind-Bind``29-IoC-IToken-'></a>
### Bind\`\`29(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |

<a name='M-IoC-FluentBind-Bind``3-IoC-IMutableContainer-'></a>
### Bind\`\`3(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |

<a name='M-IoC-FluentBind-Bind``3-IoC-IBinding-'></a>
### Bind\`\`3(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |

<a name='M-IoC-FluentBind-Bind``3-IoC-IToken-'></a>
### Bind\`\`3(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |

<a name='M-IoC-FluentBind-Bind``30-IoC-IMutableContainer-'></a>
### Bind\`\`30(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |

<a name='M-IoC-FluentBind-Bind``30-IoC-IBinding-'></a>
### Bind\`\`30(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |

<a name='M-IoC-FluentBind-Bind``30-IoC-IToken-'></a>
### Bind\`\`30(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |

<a name='M-IoC-FluentBind-Bind``31-IoC-IMutableContainer-'></a>
### Bind\`\`31(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |
| T30 | The contract type #30. |

<a name='M-IoC-FluentBind-Bind``31-IoC-IBinding-'></a>
### Bind\`\`31(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |
| T30 | The contract type #30. |

<a name='M-IoC-FluentBind-Bind``31-IoC-IToken-'></a>
### Bind\`\`31(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |
| T30 | The contract type #30. |

<a name='M-IoC-FluentBind-Bind``32-IoC-IMutableContainer-'></a>
### Bind\`\`32(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |
| T30 | The contract type #30. |
| T31 | The contract type #31. |

<a name='M-IoC-FluentBind-Bind``32-IoC-IBinding-'></a>
### Bind\`\`32(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |
| T30 | The contract type #30. |
| T31 | The contract type #31. |

<a name='M-IoC-FluentBind-Bind``32-IoC-IToken-'></a>
### Bind\`\`32(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |
| T30 | The contract type #30. |
| T31 | The contract type #31. |

<a name='M-IoC-FluentBind-Bind``33-IoC-IMutableContainer-'></a>
### Bind\`\`33(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |
| T30 | The contract type #30. |
| T31 | The contract type #31. |
| T32 | The contract type #32. |

<a name='M-IoC-FluentBind-Bind``33-IoC-IBinding-'></a>
### Bind\`\`33(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |
| T30 | The contract type #30. |
| T31 | The contract type #31. |
| T32 | The contract type #32. |

<a name='M-IoC-FluentBind-Bind``33-IoC-IToken-'></a>
### Bind\`\`33(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |
| T9 | The contract type #9. |
| T10 | The contract type #10. |
| T11 | The contract type #11. |
| T12 | The contract type #12. |
| T13 | The contract type #13. |
| T14 | The contract type #14. |
| T15 | The contract type #15. |
| T16 | The contract type #16. |
| T17 | The contract type #17. |
| T18 | The contract type #18. |
| T19 | The contract type #19. |
| T20 | The contract type #20. |
| T21 | The contract type #21. |
| T22 | The contract type #22. |
| T23 | The contract type #23. |
| T24 | The contract type #24. |
| T25 | The contract type #25. |
| T26 | The contract type #26. |
| T27 | The contract type #27. |
| T28 | The contract type #28. |
| T29 | The contract type #29. |
| T30 | The contract type #30. |
| T31 | The contract type #31. |
| T32 | The contract type #32. |

<a name='M-IoC-FluentBind-Bind``4-IoC-IMutableContainer-'></a>
### Bind\`\`4(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |

<a name='M-IoC-FluentBind-Bind``4-IoC-IBinding-'></a>
### Bind\`\`4(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |

<a name='M-IoC-FluentBind-Bind``4-IoC-IToken-'></a>
### Bind\`\`4(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |

<a name='M-IoC-FluentBind-Bind``5-IoC-IMutableContainer-'></a>
### Bind\`\`5(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |

<a name='M-IoC-FluentBind-Bind``5-IoC-IBinding-'></a>
### Bind\`\`5(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |

<a name='M-IoC-FluentBind-Bind``5-IoC-IToken-'></a>
### Bind\`\`5(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |

<a name='M-IoC-FluentBind-Bind``6-IoC-IMutableContainer-'></a>
### Bind\`\`6(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |

<a name='M-IoC-FluentBind-Bind``6-IoC-IBinding-'></a>
### Bind\`\`6(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |

<a name='M-IoC-FluentBind-Bind``6-IoC-IToken-'></a>
### Bind\`\`6(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |

<a name='M-IoC-FluentBind-Bind``7-IoC-IMutableContainer-'></a>
### Bind\`\`7(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |

<a name='M-IoC-FluentBind-Bind``7-IoC-IBinding-'></a>
### Bind\`\`7(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |

<a name='M-IoC-FluentBind-Bind``7-IoC-IToken-'></a>
### Bind\`\`7(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |

<a name='M-IoC-FluentBind-Bind``8-IoC-IMutableContainer-'></a>
### Bind\`\`8(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |

<a name='M-IoC-FluentBind-Bind``8-IoC-IBinding-'></a>
### Bind\`\`8(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |

<a name='M-IoC-FluentBind-Bind``8-IoC-IToken-'></a>
### Bind\`\`8(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |

<a name='M-IoC-FluentBind-Bind``9-IoC-IMutableContainer-'></a>
### Bind\`\`9(container) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |

<a name='M-IoC-FluentBind-Bind``9-IoC-IBinding-'></a>
### Bind\`\`9(binding) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding](#T-IoC-IBinding 'IoC.IBinding') | The target binding. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |

<a name='M-IoC-FluentBind-Bind``9-IoC-IToken-'></a>
### Bind\`\`9(token) `method`

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The binding token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |
| T6 | The contract type #6. |
| T7 | The contract type #7. |
| T8 | The contract type #8. |

<a name='M-IoC-FluentBind-Lifetime``1-IoC-IBinding{``0},IoC-ILifetime-'></a>
### Lifetime\`\`1(binding,lifetime) `method`

##### Summary

Assigns the lifetime to the binding.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding{\`\`0}](#T-IoC-IBinding{``0} 'IoC.IBinding{``0}') |  |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentBind-Tag``1-IoC-IBinding{``0},System-Object-'></a>
### Tag\`\`1(binding,tagValue) `method`

##### Summary

Marks the binding by the tag. Is it possible to use multiple times.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding{\`\`0}](#T-IoC-IBinding{``0} 'IoC.IBinding{``0}') |  |
| tagValue | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentBind-To-IoC-IBinding{System-Object},System-Type,System-Linq-Expressions-Expression{System-Action{IoC-Context{System-Object}}}[]-'></a>
### To(binding,type,statements) `method`

##### Summary

Registers autowiring binding.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding{System.Object}](#T-IoC-IBinding{System-Object} 'IoC.IBinding{System.Object}') | The binding token. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The instance type. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{System.Object}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{System.Object}}}[]') | The set of expressions to initialize an instance. |

<a name='M-IoC-FluentBind-To``1-IoC-IBinding{``0},System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### To\`\`1(binding,statements) `method`

##### Summary

Registers autowiring binding.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding{\`\`0}](#T-IoC-IBinding{``0} 'IoC.IBinding{``0}') | The binding token. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentBind-To``1-IoC-IBinding{``0},System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### To\`\`1(binding,factory,statements) `method`

##### Summary

Registers autowiring binding.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding{\`\`0}](#T-IoC-IBinding{``0} 'IoC.IBinding{``0}') | The binding token. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='T-IoC-FluentConfiguration'></a>
## FluentConfiguration `type`

##### Namespace

IoC

##### Summary

Represents extensions to configure a container.

<a name='M-IoC-FluentConfiguration-Apply-IoC-IMutableContainer,System-String[]-'></a>
### Apply(container,configurationText) `method`

##### Summary

Applies text configurations for the target container.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| configurationText | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The text configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IToken,System-String[]-'></a>
### Apply(token,configurationText) `method`

##### Summary

Applies text configurations for the target container.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The target container token. |
| configurationText | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The text configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IMutableContainer,System-IO-Stream[]-'></a>
### Apply(container,configurationStreams) `method`

##### Summary

Applies text configurations from streams for the target container.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| configurationStreams | [System.IO.Stream[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream[] 'System.IO.Stream[]') | The set of streams with text configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IToken,System-IO-Stream[]-'></a>
### Apply(token,configurationStreams) `method`

##### Summary

Applies text configurations from streams for the target container.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The target container token. |
| configurationStreams | [System.IO.Stream[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream[] 'System.IO.Stream[]') | The set of streams with text configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IMutableContainer,System-IO-TextReader[]-'></a>
### Apply(container,configurationReaders) `method`

##### Summary

Applies text configurations from text readers for the target container.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| configurationReaders | [System.IO.TextReader[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.TextReader[] 'System.IO.TextReader[]') | The set of text readers with text configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IToken,System-IO-TextReader[]-'></a>
### Apply(token,configurationReaders) `method`

##### Summary

Applies text configurations from text readers for the target container.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The target container token. |
| configurationReaders | [System.IO.TextReader[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.TextReader[] 'System.IO.TextReader[]') | The set of text readers with text configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IMutableContainer,System-Collections-Generic-IEnumerable{IoC-IConfiguration}-'></a>
### Apply(container,configurations) `method`

##### Summary

Applies configurations for the target container.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| configurations | [System.Collections.Generic.IEnumerable{IoC.IConfiguration}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{IoC.IConfiguration}') | The configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IToken,System-Collections-Generic-IEnumerable{IoC-IConfiguration}-'></a>
### Apply(token,configurations) `method`

##### Summary

Applies configurations for the target container.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The target container token. |
| configurations | [System.Collections.Generic.IEnumerable{IoC.IConfiguration}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{IoC.IConfiguration}') | The configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IMutableContainer,IoC-IConfiguration[]-'></a>
### Apply(container,configurations) `method`

##### Summary

Applies configurations for the target container.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| configurations | [IoC.IConfiguration[]](#T-IoC-IConfiguration[] 'IoC.IConfiguration[]') | The configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IToken,IoC-IConfiguration[]-'></a>
### Apply(token,configurations) `method`

##### Summary

Applies configurations for the target container.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The target container token. |
| configurations | [IoC.IConfiguration[]](#T-IoC-IConfiguration[] 'IoC.IConfiguration[]') | The configurations. |

<a name='M-IoC-FluentConfiguration-Apply``1-IoC-IMutableContainer-'></a>
### Apply\`\`1(container) `method`

##### Summary

Applies a configuration for the target container.

##### Returns

The target container token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of configuration. |

<a name='M-IoC-FluentConfiguration-Apply``1-IoC-IToken-'></a>
### Apply\`\`1(token) `method`

##### Summary

Applies a configuration for the target container.

##### Returns

The target container token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The target container token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of configuration. |

<a name='M-IoC-FluentConfiguration-AsTokenOf-System-IDisposable,IoC-IMutableContainer-'></a>
### AsTokenOf(disposableToken,container) `method`

##### Summary

Converts a disposable resource to the container's token.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| disposableToken | [System.IDisposable](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IDisposable 'System.IDisposable') | A disposable resource. |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

<a name='M-IoC-FluentConfiguration-Create-System-Func{IoC-IContainer,IoC-IToken}-'></a>
### Create(configurationFactory) `method`

##### Summary

Creates configuration from factory.

##### Returns

The configuration instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationFactory | [System.Func{IoC.IContainer,IoC.IToken}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{IoC.IContainer,IoC.IToken}') | The configuration factory. |

<a name='M-IoC-FluentConfiguration-Using-IoC-IMutableContainer,IoC-IConfiguration[]-'></a>
### Using(container,configurations) `method`

##### Summary

Applies configurations for the target container.

##### Returns

The target container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| configurations | [IoC.IConfiguration[]](#T-IoC-IConfiguration[] 'IoC.IConfiguration[]') | The configurations. |

<a name='M-IoC-FluentConfiguration-Using-IoC-IToken,IoC-IConfiguration[]-'></a>
### Using(token,configurations) `method`

##### Summary

Applies configurations for the target container.

##### Returns

The target container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The target container token. |
| configurations | [IoC.IConfiguration[]](#T-IoC-IConfiguration[] 'IoC.IConfiguration[]') | The configurations. |

<a name='M-IoC-FluentConfiguration-Using``1-IoC-IMutableContainer-'></a>
### Using\`\`1(container) `method`

##### Summary

Uses a configuration for the target container.

##### Returns

The target container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of configuration. |

<a name='M-IoC-FluentConfiguration-Using``1-IoC-IToken-'></a>
### Using\`\`1(token) `method`

##### Summary

Uses a configuration for the target container.

##### Returns

The target container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The target container token. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of configuration. |

<a name='T-IoC-FluentContainer'></a>
## FluentContainer `type`

##### Namespace

IoC

##### Summary

Extension methods for IoC containers and configurations.

<a name='M-IoC-FluentContainer-BuildUp``1-IoC-IConfiguration,System-Object[]-'></a>
### BuildUp\`\`1(configuration,args) `method`

##### Summary

Buildups an instance which was not registered in container. Can be used as entry point of DI.

##### Returns

The disposable instance holder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configuration | [IoC.IConfiguration](#T-IoC-IConfiguration 'IoC.IConfiguration') | The configurations. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TInstance | The instance type. |

<a name='M-IoC-FluentContainer-BuildUp``1-IoC-IToken,System-Object[]-'></a>
### BuildUp\`\`1(token,args) `method`

##### Summary

Buildups an instance.
Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.

##### Returns

The disposable instance holder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The target container token. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TInstance | The instance type. |

<a name='M-IoC-FluentContainer-BuildUp``1-IoC-IMutableContainer,System-Object[]-'></a>
### BuildUp\`\`1(container,args) `method`

##### Summary

Buildups an instance.
Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.

##### Returns

The disposable instance holder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TInstance | The instance type. |

<a name='M-IoC-FluentContainer-Create-IoC-IContainer,System-String-'></a>
### Create(parentContainer,name) `method`

##### Summary

Creates child container.

##### Returns

The child container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parentContainer | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The parent container. |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of child container. |

<a name='M-IoC-FluentContainer-Create-IoC-IToken,System-String-'></a>
### Create(token,name) `method`

##### Summary

Creates child container.

##### Returns

The child container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The parent container token. |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of child container. |

<a name='T-IoC-FluentGetResolver'></a>
## FluentGetResolver `type`

##### Namespace

IoC

##### Summary

Represents extensions to get a resolver from the container.

<a name='M-IoC-FluentGetResolver-AsTag-System-Object-'></a>
### AsTag(tagValue) `method`

##### Summary

Creates tag.

##### Returns

The tag.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tagValue | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The tag value. |

<a name='M-IoC-FluentGetResolver-GetResolver``1-IoC-IContainer,System-Type,IoC-Tag-'></a>
### GetResolver\`\`1(type,tag,container) `method`

##### Summary

Gets the resolver.

##### Returns

The resolver.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target type. |
| tag | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The tag of binding. |
| container | [IoC.Tag](#T-IoC-Tag 'IoC.Tag') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The resolver type. |

<a name='M-IoC-FluentGetResolver-GetResolver``1-IoC-IContainer,IoC-Tag-'></a>
### GetResolver\`\`1(tag,container) `method`

##### Summary

Gets the resolver.

##### Returns

The resolver.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The tag of binding. |
| container | [IoC.Tag](#T-IoC-Tag 'IoC.Tag') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentGetResolver-GetResolver``1-IoC-IContainer,System-Type-'></a>
### GetResolver\`\`1(type,container) `method`

##### Summary

Gets the resolver.

##### Returns

The resolver.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target type. |
| container | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The resolver type. |

<a name='M-IoC-FluentGetResolver-GetResolver``1-IoC-IContainer-'></a>
### GetResolver\`\`1(container) `method`

##### Summary

Gets the resolver.

##### Returns

The resolver.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentGetResolver-TryGetResolver``1-IoC-IContainer,System-Type,IoC-Tag,IoC-Resolver{``0}@-'></a>
### TryGetResolver\`\`1(type,tag,container,resolver) `method`

##### Summary

Tries getting the resolver.

##### Returns

True if success.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target type. |
| tag | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The tag of binding. |
| container | [IoC.Tag](#T-IoC-Tag 'IoC.Tag') |  |
| resolver | [IoC.Resolver{\`\`0}@](#T-IoC-Resolver{``0}@ 'IoC.Resolver{``0}@') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The resolver type. |

<a name='M-IoC-FluentGetResolver-TryGetResolver``1-IoC-IContainer,IoC-Tag,IoC-Resolver{``0}@-'></a>
### TryGetResolver\`\`1(tag,container,resolver) `method`

##### Summary

Tries getting the resolver.

##### Returns

True if success.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The tag of binding. |
| container | [IoC.Tag](#T-IoC-Tag 'IoC.Tag') | The target container. |
| resolver | [IoC.Resolver{\`\`0}@](#T-IoC-Resolver{``0}@ 'IoC.Resolver{``0}@') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentGetResolver-TryGetResolver``1-IoC-IContainer,System-Type,IoC-Resolver{``0}@-'></a>
### TryGetResolver\`\`1(type,container,resolver) `method`

##### Summary

Tries getting the resolver.

##### Returns

True if success.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target type. |
| container | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The target container. |
| resolver | [IoC.Resolver{\`\`0}@](#T-IoC-Resolver{``0}@ 'IoC.Resolver{``0}@') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentGetResolver-TryGetResolver``1-IoC-IContainer,IoC-Resolver{``0}@-'></a>
### TryGetResolver\`\`1(container,resolver) `method`

##### Summary

Tries getting the resolver.

##### Returns

True if success.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| resolver | [IoC.Resolver{\`\`0}@](#T-IoC-Resolver{``0}@ 'IoC.Resolver{``0}@') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='T-IoC-FluentNativeResolve'></a>
## FluentNativeResolve `type`

##### Namespace

IoC

##### Summary

Represents extensions to resolve from the native container.

<a name='M-IoC-FluentNativeResolve-Resolve``1-IoC-Container-'></a>
### Resolve\`\`1(container) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.Container](#T-IoC-Container 'IoC.Container') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,IoC-Tag-'></a>
### Resolve\`\`1(container,tag) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.Container](#T-IoC-Container 'IoC.Container') | The target container. |
| tag | [IoC.Tag](#T-IoC-Tag 'IoC.Tag') | The tag. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,System-Object[]-'></a>
### Resolve\`\`1(container,args) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.Container](#T-IoC-Container 'IoC.Container') | The target container. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,IoC-Tag,System-Object[]-'></a>
### Resolve\`\`1(container,tag,args) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.Container](#T-IoC-Container 'IoC.Container') | The target container. |
| tag | [IoC.Tag](#T-IoC-Tag 'IoC.Tag') | The tag. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,System-Type-'></a>
### Resolve\`\`1(container,type) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.Container](#T-IoC-Container 'IoC.Container') | The target container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The resolving instance type. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,System-Type,IoC-Tag-'></a>
### Resolve\`\`1(container,type,tag) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.Container](#T-IoC-Container 'IoC.Container') | The target container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The resolving instance type. |
| tag | [IoC.Tag](#T-IoC-Tag 'IoC.Tag') | The tag. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,System-Type,System-Object[]-'></a>
### Resolve\`\`1(container,type,args) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.Container](#T-IoC-Container 'IoC.Container') | The target container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The resolving instance type. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentNativeResolve-Resolve``1-IoC-Container,System-Type,IoC-Tag,System-Object[]-'></a>
### Resolve\`\`1(container,type,tag,args) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.Container](#T-IoC-Container 'IoC.Container') | The target container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The resolving instance type. |
| tag | [IoC.Tag](#T-IoC-Tag 'IoC.Tag') | The tag. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='T-IoC-Core-FluentRegister'></a>
## FluentRegister `type`

##### Namespace

IoC.Core

##### Summary

Represents extensions to register a dependency in the container.

<a name='M-IoC-Core-FluentRegister-Register-IoC-IMutableContainer,System-Collections-Generic-IEnumerable{System-Type},IoC-IDependency,IoC-ILifetime,System-Object[]-'></a>
### Register(container,types,dependency,lifetime,tags) `method`

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| types | [System.Collections.Generic.IEnumerable{System.Type}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Type}') | The set of types. |
| dependency | [IoC.IDependency](#T-IoC-IDependency 'IoC.IDependency') | The dependency. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

<a name='M-IoC-Core-FluentRegister-Register``1-IoC-IMutableContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`1(container,lifetime,tags) `method`

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowiring type. |

<a name='M-IoC-Core-FluentRegister-Register``1-IoC-IMutableContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`1(container,factory,lifetime,tags,statements) `method`

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |

<a name='M-IoC-Core-FluentRegister-Register``2-IoC-IMutableContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`2(container,lifetime,tags) `method`

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowiring type. |
| T1 | The contract type #1. |

<a name='M-IoC-Core-FluentRegister-Register``2-IoC-IMutableContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`2(container,factory,lifetime,tags,statements) `method`

##### Summary

Registers a binding.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The contract type #1. |

<a name='M-IoC-Core-FluentRegister-Register``3-IoC-IMutableContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`3(container,lifetime,tags) `method`

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowiring type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |

<a name='M-IoC-Core-FluentRegister-Register``3-IoC-IMutableContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`3(container,factory,lifetime,tags,statements) `method`

##### Summary

Registers a binding.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |

<a name='M-IoC-Core-FluentRegister-Register``4-IoC-IMutableContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`4(container,lifetime,tags) `method`

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowiring type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |

<a name='M-IoC-Core-FluentRegister-Register``4-IoC-IMutableContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`4(container,factory,lifetime,tags,statements) `method`

##### Summary

Registers a binding.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |

<a name='T-IoC-FluentResolve'></a>
## FluentResolve `type`

##### Namespace

IoC

##### Summary

Represents extensions to resolve from the container.

<a name='M-IoC-FluentResolve-Resolve``1-IoC-IContainer,System-Object[]-'></a>
### Resolve\`\`1(container,args) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentResolve-Resolve``1-IoC-IContainer,IoC-Tag,System-Object[]-'></a>
### Resolve\`\`1(container,tag,args) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| tag | [IoC.Tag](#T-IoC-Tag 'IoC.Tag') | The tag. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentResolve-Resolve``1-IoC-IContainer,System-Type,System-Object[]-'></a>
### Resolve\`\`1(container,type,args) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The resolving instance type. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentResolve-Resolve``1-IoC-IContainer,System-Type,IoC-Tag,System-Object[]-'></a>
### Resolve\`\`1(container,type,tag,args) `method`

##### Summary

Resolves an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The resolving instance type. |
| tag | [IoC.Tag](#T-IoC-Tag 'IoC.Tag') | The tag. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='T-IoC-FluentScope'></a>
## FluentScope `type`

##### Namespace

IoC

##### Summary

Represents extensions dealing with scopes.

<a name='M-IoC-FluentScope-CreateScope-IoC-IContainer-'></a>
### CreateScope(container) `method`

##### Summary

Creates a new resolving scope. Can be used with `ScopeSingleton`.

##### Returns

Tne new scope instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | A container to resolve a scope. |

<a name='T-IoC-FluentTrace'></a>
## FluentTrace `type`

##### Namespace

IoC

##### Summary

Represents extensions to trace the container.

<a name='M-IoC-FluentTrace-ToTraceSource-IoC-IContainer-'></a>
### ToTraceSource(container) `method`

##### Summary

Gets a container trace source.

##### Returns

The race source.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container to trace. |

<a name='M-IoC-FluentTrace-Trace-IoC-IMutableContainer,System-Action{System-String}-'></a>
### Trace(container,onTraceMessage) `method`

##### Summary

Traces container actions through a handler.

##### Returns

The trace token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container to trace. |
| onTraceMessage | [System.Action{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.String}') | The trace handler. |

<a name='M-IoC-FluentTrace-Trace-IoC-IToken,System-Action{System-String}-'></a>
### Trace(token,onTraceMessage) `method`

##### Summary

Traces container actions through a handler.

##### Returns

The trace token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The token of target container to trace. |
| onTraceMessage | [System.Action{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.String}') | The trace handler. |

<a name='M-IoC-FluentTrace-Trace-IoC-IMutableContainer-'></a>
### Trace(container) `method`

##### Summary

Traces container actions through a `System.Diagnostics.Trace`.

##### Returns

The trace token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target container to trace. |

<a name='M-IoC-FluentTrace-Trace-IoC-IToken-'></a>
### Trace(token) `method`

##### Summary

Traces container actions through a `System.Diagnostics.Trace`.

##### Returns

The trace token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [IoC.IToken](#T-IoC-IToken 'IoC.IToken') | The token of target container to trace. |

<a name='T-IoC-Features-FuncFeature'></a>
## FuncFeature `type`

##### Namespace

IoC.Features

##### Summary

Allows to resolve Functions.

<a name='F-IoC-Features-FuncFeature-Default'></a>
### Default `constants`

<a name='F-IoC-Features-FuncFeature-Light'></a>
### Light `constants`

<a name='M-IoC-Features-FuncFeature-Apply-IoC-IMutableContainer-'></a>
### Apply() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-GenericTypeArgumentAttribute'></a>
## GenericTypeArgumentAttribute `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker.

<a name='T-IoC-Core-IArray'></a>
## IArray `type`

##### Namespace

IoC.Core

##### Summary

Marker interface for arrays.

<a name='T-IoC-IAutowiringStrategy'></a>
## IAutowiringStrategy `type`

##### Namespace

IoC

##### Summary

Represents an abstraction for autowiring strategy.

<a name='M-IoC-IAutowiringStrategy-TryResolveConstructor-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-ConstructorInfo}},IoC-IMethod{System-Reflection-ConstructorInfo}@-'></a>
### TryResolveConstructor(constructors,constructor) `method`

##### Summary

Resolves a constructor from a set of available constructors.

##### Returns

True if the constructor was resolved.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| constructors | [System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.ConstructorInfo}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.ConstructorInfo}}') | The set of available constructors. |
| constructor | [IoC.IMethod{System.Reflection.ConstructorInfo}@](#T-IoC-IMethod{System-Reflection-ConstructorInfo}@ 'IoC.IMethod{System.Reflection.ConstructorInfo}@') | The resolved constructor. |

<a name='M-IoC-IAutowiringStrategy-TryResolveInitializers-System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}},System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-MethodInfo}}@-'></a>
### TryResolveInitializers(methods,initializers) `method`

##### Summary

Resolves initializing methods from a set of available methods/setters in the specific order which will be used to invoke them.

##### Returns

True if initializing methods were resolved.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| methods | [System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.MethodInfo}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.MethodInfo}}') | The set of available methods. |
| initializers | [System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.MethodInfo}}@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.MethodInfo}}@') | The set of initializing methods in the appropriate order. |

<a name='M-IoC-IAutowiringStrategy-TryResolveType-System-Type,System-Type,System-Type@-'></a>
### TryResolveType(registeredType,resolvingType,instanceType) `method`

##### Summary

Resolves type to create an instance.

##### Returns

True if the type was resolved.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| registeredType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | Registered type. |
| resolvingType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | Resolving type. |
| instanceType | [System.Type@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type@ 'System.Type@') | The type to create an instance. |

<a name='T-IoC-IBinding'></a>
## IBinding `type`

##### Namespace

IoC

##### Summary

The an abstract containers binding.

<a name='P-IoC-IBinding-AutowiringStrategy'></a>
### AutowiringStrategy `property`

##### Summary

The autowiring strategy or null by default.

<a name='P-IoC-IBinding-Container'></a>
### Container `property`

##### Summary

The target container to configure.

<a name='P-IoC-IBinding-Lifetime'></a>
### Lifetime `property`

##### Summary

The lifetime instance or null by default.

<a name='P-IoC-IBinding-Tags'></a>
### Tags `property`

##### Summary

The tags to mark this binding.

<a name='P-IoC-IBinding-Tokens'></a>
### Tokens `property`

##### Summary

Binding tokens.

<a name='P-IoC-IBinding-Types'></a>
### Types `property`

##### Summary

The contract type to bind.

<a name='T-IoC-IBinding`1'></a>
## IBinding\`1 `type`

##### Namespace

IoC

##### Summary

The containers binding.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='T-IoC-IBuildContext'></a>
## IBuildContext `type`

##### Namespace

IoC

##### Summary

Represents an abstract build context.

<a name='P-IoC-IBuildContext-AutowiringStrategy'></a>
### AutowiringStrategy `property`

##### Summary

The current autowiring strategy.

<a name='P-IoC-IBuildContext-Container'></a>
### Container `property`

##### Summary

The target container.

<a name='P-IoC-IBuildContext-Depth'></a>
### Depth `property`

##### Summary

The depth of current context in the build tree.

<a name='P-IoC-IBuildContext-Key'></a>
### Key `property`

##### Summary

The target key to build resolver.

<a name='P-IoC-IBuildContext-Parent'></a>
### Parent `property`

##### Summary

The parent of the current build context.

<a name='M-IoC-IBuildContext-AddLifetime-System-Linq-Expressions-Expression,IoC-ILifetime-'></a>
### AddLifetime(baseExpression,lifetime) `method`

##### Summary

Prepares base expression adding the appropriate lifetime.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| baseExpression | [System.Linq.Expressions.Expression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression') | The base expression. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |

<a name='M-IoC-IBuildContext-BindTypes-System-Type,System-Type-'></a>
### BindTypes(originalType,targetType) `method`

##### Summary

Binds a raw type to a target type.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| originalType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The registered type. |
| targetType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The target type. |

<a name='M-IoC-IBuildContext-CreateChild-IoC-Key,IoC-IContainer-'></a>
### CreateChild(key,container) `method`

##### Summary

Creates a child build context.

##### Returns

The new build context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [IoC.Key](#T-IoC-Key 'IoC.Key') | The key |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The container. |

<a name='M-IoC-IBuildContext-GetDependencyExpression-System-Linq-Expressions-Expression-'></a>
### GetDependencyExpression(defaultExpression) `method`

##### Summary

Gets the dependency expression.

##### Returns

The dependency expression.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| defaultExpression | [System.Linq.Expressions.Expression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression') | The default expression. |

<a name='M-IoC-IBuildContext-InjectDependencies-System-Linq-Expressions-Expression,System-Linq-Expressions-ParameterExpression-'></a>
### InjectDependencies(baseExpression,instanceExpression) `method`

##### Summary

Prepares base expression injecting appropriate dependencies.

##### Returns

The resulting expression.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| baseExpression | [System.Linq.Expressions.Expression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression') | The base expression. |
| instanceExpression | [System.Linq.Expressions.ParameterExpression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.ParameterExpression 'System.Linq.Expressions.ParameterExpression') | The instance expression. |

<a name='M-IoC-IBuildContext-ReplaceTypes-System-Linq-Expressions-Expression-'></a>
### ReplaceTypes(baseExpression) `method`

##### Summary

Prepares base expression replacing generic types' markers by related types.

##### Returns

The resulting expression.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| baseExpression | [System.Linq.Expressions.Expression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression') | The base expression. |

<a name='M-IoC-IBuildContext-TryReplaceType-System-Type,System-Type@-'></a>
### TryReplaceType(originalType,targetType) `method`

##### Summary

Tries to replace generic types' markers by related types.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| originalType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The target raw type. |
| targetType | [System.Type@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type@ 'System.Type@') | The replacing type. |

<a name='T-IoC-IBuilder'></a>
## IBuilder `type`

##### Namespace

IoC

##### Summary

Represents an abstract builder for an instance.

<a name='M-IoC-IBuilder-Build-IoC-IBuildContext,System-Linq-Expressions-Expression-'></a>
### Build(context,bodyExpression) `method`

##### Summary

Builds the expression based on a build context.

##### Returns

The new expression.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [IoC.IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext') | Current build context. |
| bodyExpression | [System.Linq.Expressions.Expression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression') | The expression body to build an instance resolver. |

<a name='T-IoC-Issues-ICannotBuildExpression'></a>
## ICannotBuildExpression `type`

##### Namespace

IoC.Issues

##### Summary

Resolves the scenario when cannot build expression.

<a name='M-IoC-Issues-ICannotBuildExpression-Resolve-IoC-IBuildContext,IoC-IDependency,IoC-ILifetime,System-Exception-'></a>
### Resolve(buildContext,dependency,lifetime,error) `method`

##### Summary

Resolves the scenario when cannot build expression.

##### Returns

The resulting expression.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buildContext | [IoC.IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext') | The build context. |
| dependency | [IoC.IDependency](#T-IoC-IDependency 'IoC.IDependency') | The dependency. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The lifetime. |
| error | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The error. |

<a name='T-IoC-Issues-ICannotGetResolver'></a>
## ICannotGetResolver `type`

##### Namespace

IoC.Issues

##### Summary

Resolves the scenario when cannot get a resolver.

<a name='M-IoC-Issues-ICannotGetResolver-Resolve``1-IoC-IContainer,IoC-Key,System-Exception-'></a>
### Resolve\`\`1(container,key,error) `method`

##### Summary

Resolves the scenario when cannot get a resolver.

##### Returns

The resolver.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| key | [IoC.Key](#T-IoC-Key 'IoC.Key') | The resolving key. |
| error | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The error. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='T-IoC-Issues-ICannotParseLifetime'></a>
## ICannotParseLifetime `type`

##### Namespace

IoC.Issues

##### Summary

Resolves the scenario when cannot parse a lifetime from a text.

<a name='M-IoC-Issues-ICannotParseLifetime-Resolve-System-String,System-Int32,System-Int32,System-String-'></a>
### Resolve(statementText,statementLineNumber,statementPosition,lifetimeName) `method`

##### Summary

Resolves the scenario when cannot parse a lifetime from a text.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| statementText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The statement containing a lifetime metadata. |
| statementLineNumber | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The line number in the source data. |
| statementPosition | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The position at the line of the source data. |
| lifetimeName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text with a lifetime metadata. |

<a name='T-IoC-Issues-ICannotParseTag'></a>
## ICannotParseTag `type`

##### Namespace

IoC.Issues

##### Summary

Resolves the scenario when cannot parse a tag from a text.

<a name='M-IoC-Issues-ICannotParseTag-Resolve-System-String,System-Int32,System-Int32,System-String-'></a>
### Resolve(statementText,statementLineNumber,statementPosition,tag) `method`

##### Summary

Resolves the scenario when cannot parse a tag from a text.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| statementText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The statement containing a tag metadata. |
| statementLineNumber | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The line number in the source data. |
| statementPosition | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The position at the line of the source data. |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text with a tag metadata. |

<a name='T-IoC-Issues-ICannotParseType'></a>
## ICannotParseType `type`

##### Namespace

IoC.Issues

##### Summary

Resolves the scenario when cannot parse a type from a text.

<a name='M-IoC-Issues-ICannotParseType-Resolve-System-String,System-Int32,System-Int32,System-String-'></a>
### Resolve(statementText,statementLineNumber,statementPosition,typeName) `method`

##### Summary

Resolves the scenario when cannot parse a type from a text.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| statementText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The statement containing a type metadata. |
| statementLineNumber | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The line number in the source data. |
| statementPosition | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The position at the line of the source data. |
| typeName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text with a type metadata. |

<a name='T-IoC-Issues-ICannotRegister'></a>
## ICannotRegister `type`

##### Namespace

IoC.Issues

##### Summary

Resolves the scenario when a new binding cannot be registered.

<a name='M-IoC-Issues-ICannotRegister-Resolve-IoC-IContainer,IoC-Key[]-'></a>
### Resolve(container,keys) `method`

##### Summary

Resolves the scenario when a new binding cannot be registered.

##### Returns

The dependency token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| keys | [IoC.Key[]](#T-IoC-Key[] 'IoC.Key[]') | The set of binding keys. |

<a name='T-IoC-Issues-ICannotResolveConstructor'></a>
## ICannotResolveConstructor `type`

##### Namespace

IoC.Issues

##### Summary

Resolves the scenario when cannot resolve a constructor.

<a name='M-IoC-Issues-ICannotResolveConstructor-Resolve-IoC-IBuildContext,System-Collections-Generic-IEnumerable{IoC-IMethod{System-Reflection-ConstructorInfo}}-'></a>
### Resolve(buildContext,constructors) `method`

##### Summary

Resolves the scenario when cannot resolve a constructor.

##### Returns

The constructor.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buildContext | [IoC.IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext') | The build context. |
| constructors | [System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.ConstructorInfo}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{IoC.IMethod{System.Reflection.ConstructorInfo}}') | Available constructors. |

<a name='T-IoC-Issues-ICannotResolveDependency'></a>
## ICannotResolveDependency `type`

##### Namespace

IoC.Issues

##### Summary

Resolves issue with unknown dependency.

<a name='M-IoC-Issues-ICannotResolveDependency-Resolve-IoC-IBuildContext-'></a>
### Resolve(buildContext) `method`

##### Summary

Resolves the scenario when the dependency was not found.

##### Returns

The pair of the dependency and of the lifetime.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buildContext | [IoC.IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext') | The build context. |

<a name='T-IoC-Issues-ICannotResolveGenericTypeArgument'></a>
## ICannotResolveGenericTypeArgument `type`

##### Namespace

IoC.Issues

##### Summary

Resolves the scenario when cannot resolve the generic type argument of an instance type.

<a name='M-IoC-Issues-ICannotResolveGenericTypeArgument-Resolve-IoC-IBuildContext,System-Type,System-Int32,System-Type-'></a>
### Resolve(buildContext,type,genericTypeArgPosition,genericTypeArg) `method`

##### Summary

Resolves the generic type argument of an instance type.

##### Returns

The resoled generic type argument.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buildContext | [IoC.IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext') | The build context. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | Registered type. |
| genericTypeArgPosition | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The generic type argument position in the registered type. |
| genericTypeArg | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The generic type argument in the registered type. |

<a name='T-IoC-Issues-ICannotResolveType'></a>
## ICannotResolveType `type`

##### Namespace

IoC.Issues

##### Summary

Resolves the scenario when cannot resolve the instance type.

<a name='M-IoC-Issues-ICannotResolveType-Resolve-IoC-IBuildContext,System-Type,System-Type-'></a>
### Resolve(buildContext,registeredType,resolvingType) `method`

##### Summary

Resolves the scenario when cannot resolve the instance type.

##### Returns

The type to create an instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buildContext | [IoC.IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext') | The build context. |
| registeredType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | Registered type. |
| resolvingType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | Resolving type. |

<a name='T-IoC-ICompiler'></a>
## ICompiler `type`

##### Namespace

IoC

##### Summary

Represents an abstract expression compiler.

<a name='M-IoC-ICompiler-TryCompile-IoC-IBuildContext,System-Linq-Expressions-LambdaExpression,System-Delegate@-'></a>
### TryCompile(context,expression,resolver) `method`

##### Summary

Compiles an expression to a delegate.

##### Returns

True if success.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [IoC.IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext') | Current context for building. |
| expression | [System.Linq.Expressions.LambdaExpression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.LambdaExpression 'System.Linq.Expressions.LambdaExpression') | The lambda expression to compile. |
| resolver | [System.Delegate@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate@ 'System.Delegate@') | The compiled resolver delegate. |

<a name='T-IoC-ICompositionRoot`1'></a>
## ICompositionRoot\`1 `type`

##### Namespace

IoC

##### Summary

Represents an abstract composition root.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TInstance |  |

<a name='P-IoC-ICompositionRoot`1-Instance'></a>
### Instance `property`

##### Summary

The composition root instance.

<a name='T-IoC-IConfiguration'></a>
## IConfiguration `type`

##### Namespace

IoC

##### Summary

Represents an abstract containers configuration.

<a name='M-IoC-IConfiguration-Apply-IoC-IMutableContainer-'></a>
### Apply(container) `method`

##### Summary

Applies a configuration to the target mutable container.

##### Returns

The enumeration of configuration tokens which allows to cancel that changes.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IMutableContainer](#T-IoC-IMutableContainer 'IoC.IMutableContainer') | The target mutable container to configure. |

<a name='T-IoC-IContainer'></a>
## IContainer `type`

##### Namespace

IoC

##### Summary

Represents an abstract Inversion of Control container.

<a name='P-IoC-IContainer-Parent'></a>
### Parent `property`

##### Summary

Provides a parent container or `null` if it does not have a parent.

<a name='M-IoC-IContainer-TryGetDependency-IoC-Key,IoC-IDependency@,IoC-ILifetime@-'></a>
### TryGetDependency(key,dependency,lifetime) `method`

##### Summary

Provides a dependency and a lifetime for the registered key.

##### Returns

True if successful.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [IoC.Key](#T-IoC-Key 'IoC.Key') | The key to get a dependency. |
| dependency | [IoC.IDependency@](#T-IoC-IDependency@ 'IoC.IDependency@') | The dependency. |
| lifetime | [IoC.ILifetime@](#T-IoC-ILifetime@ 'IoC.ILifetime@') | The lifetime. |

<a name='M-IoC-IContainer-TryGetResolver``1-System-Type,System-Object,IoC-Resolver{``0}@,System-Exception@,IoC-IContainer-'></a>
### TryGetResolver\`\`1(type,tag,resolver,error,resolvingContainer) `method`

##### Summary

Provides a resolver for a specific type and tag or error if something goes wrong.

##### Returns

`True` if successful and a resolver was provided.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The binding type. |
| tag | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The binding tag or null if there is no tag. |
| resolver | [IoC.Resolver{\`\`0}@](#T-IoC-Resolver{``0}@ 'IoC.Resolver{``0}@') | The resolver to get an instance. |
| error | [System.Exception@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception@ 'System.Exception@') | Error that occurs when resolving. |
| resolvingContainer | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container and null if the resolving container is the current container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of instance producing by the resolver. |

<a name='T-IoC-IDependency'></a>
## IDependency `type`

##### Namespace

IoC

##### Summary

Represents an abstract IoC dependency.

<a name='M-IoC-IDependency-TryBuildExpression-IoC-IBuildContext,IoC-ILifetime,System-Linq-Expressions-Expression@,System-Exception@-'></a>
### TryBuildExpression(buildContext,lifetime,baseExpression,error) `method`

##### Summary

Builds an expression for dependency based on the current build context and specified lifetime.

##### Returns

`True` if successful and an expression was provided.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buildContext | [IoC.IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext') | The build context. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| baseExpression | [System.Linq.Expressions.Expression@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression@ 'System.Linq.Expressions.Expression@') | The resulting expression for the current dependency. |
| error | [System.Exception@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception@ 'System.Exception@') | The error if something goes wrong. |

<a name='T-IoC-Core-IExpressionBuilder`1'></a>
## IExpressionBuilder\`1 `type`

##### Namespace

IoC.Core

##### Summary

Allows to build expression for lifetimes.

<a name='M-IoC-Core-IExpressionBuilder`1-Build-System-Linq-Expressions-Expression,IoC-IBuildContext,`0-'></a>
### Build(bodyExpression,buildContext,context) `method`

##### Summary

Builds the expression.

##### Returns

The new expression.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| bodyExpression | [System.Linq.Expressions.Expression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression') | The expression body to get an instance. |
| buildContext | [IoC.IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext') | The build context. |
| context | [\`0](#T-`0 '`0') | The expression build context. |

<a name='T-IoC-Issues-IFoundCyclicDependency'></a>
## IFoundCyclicDependency `type`

##### Namespace

IoC.Issues

##### Summary

Resolves the scenario when a cyclic dependency was detected.

<a name='M-IoC-Issues-IFoundCyclicDependency-Resolve-IoC-IBuildContext-'></a>
### Resolve(buildContext) `method`

##### Summary

Resolves the scenario when a cyclic dependency was detected.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| buildContext | [IoC.IBuildContext](#T-IoC-IBuildContext 'IoC.IBuildContext') | The build context. |

<a name='T-IoC-ILifetime'></a>
## ILifetime `type`

##### Namespace

IoC

##### Summary

Represents an abstraction of container lifetime.

<a name='M-IoC-ILifetime-Create'></a>
### Create() `method`

##### Summary

Creates the similar lifetime to use with generic instances.

##### Returns

The new lifetime instance.

##### Parameters

This method has no parameters.

<a name='M-IoC-ILifetime-SelectResolvingContainer-IoC-IContainer,IoC-IContainer-'></a>
### SelectResolvingContainer(registrationContainer,resolvingContainer) `method`

##### Summary

Provides a container to resolve dependencies.

##### Returns

The selected container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| registrationContainer | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The container where a dependency was registered. |
| resolvingContainer | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The container which is used to resolve an instance. |

<a name='T-IoC-IMethod`1'></a>
## IMethod\`1 `type`

##### Namespace

IoC

##### Summary

Represents an abstraction for autowiring method.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TMethodInfo | The type of method info. |

<a name='P-IoC-IMethod`1-Info'></a>
### Info `property`

##### Summary

The methods information.

<a name='M-IoC-IMethod`1-GetParametersExpressions-IoC-IBuildContext-'></a>
### GetParametersExpressions() `method`

##### Summary

Provides a set of parameters expressions.

##### Returns

Parameters' expressions

##### Parameters

This method has no parameters.

<a name='M-IoC-IMethod`1-SetDependency-System-Int32,System-Type,System-Object-'></a>
### SetDependency(parameterPosition,dependencyType,dependencyTag) `method`

##### Summary

Specifies the dependency type and tag for method parameter at the position.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parameterPosition | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The parameter position. |
| dependencyType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The dependency type. |
| dependencyTag | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The optional dependency tag value. |

<a name='M-IoC-IMethod`1-SetExpression-System-Int32,System-Linq-Expressions-Expression-'></a>
### SetExpression(parameterPosition,parameterExpression) `method`

##### Summary

Specifies the expression of method parameter at the position.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parameterPosition | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The parameter position. |
| parameterExpression | [System.Linq.Expressions.Expression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression') | The parameter expression. |

<a name='T-IoC-IMutableContainer'></a>
## IMutableContainer `type`

##### Namespace

IoC

##### Summary

Represents an abstract of configurable Inversion of Control container.

<a name='M-IoC-IMutableContainer-TryRegisterDependency-System-Collections-Generic-IEnumerable{IoC-Key},IoC-IDependency,IoC-ILifetime,IoC-IToken@-'></a>
### TryRegisterDependency(keys,dependency,lifetime,dependencyToken) `method`

##### Summary

Registers the dependency and the lifetime for the specified dependency key.

##### Returns

`True` if is registered successfully.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keys | [System.Collections.Generic.IEnumerable{IoC.Key}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{IoC.Key}') | The set of keys to register. |
| dependency | [IoC.IDependency](#T-IoC-IDependency 'IoC.IDependency') | The dependency. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The lifetime. |
| dependencyToken | [IoC.IToken@](#T-IoC-IToken@ 'IoC.IToken@') | The dependency token to unregister this dependency key. |

<a name='T-IoC-IResourceRegistry'></a>
## IResourceRegistry `type`

##### Namespace

IoC

##### Summary

Represents an abstraction of the resource registry.

<a name='M-IoC-IResourceRegistry-RegisterResource-System-IDisposable-'></a>
### RegisterResource(resource) `method`

##### Summary

Registers a resource to the registry.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resource | [System.IDisposable](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IDisposable 'System.IDisposable') | The target resource. |

<a name='M-IoC-IResourceRegistry-UnregisterResource-System-IDisposable-'></a>
### UnregisterResource(resource) `method`

##### Summary

Unregisters a resource from the registry.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resource | [System.IDisposable](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IDisposable 'System.IDisposable') | The target resource. |

<a name='T-IoC-IScope'></a>
## IScope `type`

##### Namespace

IoC

##### Summary

Represents an abstraction of a scope which is used with `Lifetime.ScopeSingleton`.

<a name='M-IoC-IScope-Activate'></a>
### Activate() `method`

##### Summary

Activate the scope.

##### Returns

The token to deactivate the activated scope.

##### Parameters

This method has no parameters.

<a name='T-IoC-IToken'></a>
## IToken `type`

##### Namespace

IoC

##### Summary

Represents an abstraction of a binding token.

<a name='P-IoC-IToken-Container'></a>
### Container `property`

##### Summary

The configurable container owning the registered binding.

<a name='T-IoC-ImplicitNotNullAttribute'></a>
## ImplicitNotNullAttribute `type`

##### Namespace

IoC

##### Summary

Implicitly apply [NotNull]/[ItemNotNull] annotation to all the of type members and parameters
in particular scope where this annotation is used (type declaration or whole assembly).

<a name='T-IoC-ImplicitUseKindFlags'></a>
## ImplicitUseKindFlags `type`

##### Namespace

IoC

<a name='F-IoC-ImplicitUseKindFlags-Access'></a>
### Access `constants`

##### Summary

Only entity marked with attribute considered used.

<a name='F-IoC-ImplicitUseKindFlags-Assign'></a>
### Assign `constants`

##### Summary

Indicates implicit assignment to a member.

<a name='F-IoC-ImplicitUseKindFlags-InstantiatedNoFixedConstructorSignature'></a>
### InstantiatedNoFixedConstructorSignature `constants`

##### Summary

Indicates implicit instantiation of a type.

<a name='F-IoC-ImplicitUseKindFlags-InstantiatedWithFixedConstructorSignature'></a>
### InstantiatedWithFixedConstructorSignature `constants`

##### Summary

Indicates implicit instantiation of a type with fixed constructor signature.
That means any unused constructor parameters won't be reported as such.

<a name='T-IoC-ImplicitUseTargetFlags'></a>
## ImplicitUseTargetFlags `type`

##### Namespace

IoC

##### Summary

Specify what is considered used implicitly when marked
with [MeansImplicitUseAttribute](#T-IoC-MeansImplicitUseAttribute 'IoC.MeansImplicitUseAttribute') or [UsedImplicitlyAttribute](#T-IoC-UsedImplicitlyAttribute 'IoC.UsedImplicitlyAttribute').

<a name='F-IoC-ImplicitUseTargetFlags-Members'></a>
### Members `constants`

##### Summary

Members of entity marked with attribute are considered used.

<a name='F-IoC-ImplicitUseTargetFlags-WithMembers'></a>
### WithMembers `constants`

##### Summary

Entity marked with attribute and all its members considered used.

<a name='T-IoC-Injections'></a>
## Injections `type`

##### Namespace

IoC

##### Summary

A set of injection markers.

<a name='M-IoC-Injections-Inject-IoC-IContainer,System-Type-'></a>
### Inject(container,type) `method`

##### Summary

Injects a dependency. Just the injection marker.

##### Returns

The injected instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of dependency. |

<a name='M-IoC-Injections-Inject-IoC-IContainer,System-Type,System-Object-'></a>
### Inject(container,type,tag) `method`

##### Summary

Injects a dependency. Just the injection marker.

##### Returns

The injected instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of dependency. |
| tag | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The tag of dependency. |

<a name='M-IoC-Injections-Inject``1-IoC-IContainer-'></a>
### Inject\`\`1(container) `method`

##### Summary

Injects a dependency. Just the injection marker.

##### Returns

The injected instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of dependency. |

<a name='M-IoC-Injections-Inject``1-IoC-IContainer,System-Object-'></a>
### Inject\`\`1(container,tag) `method`

##### Summary

Injects a dependency. Just the injection marker.

##### Returns

The injected instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| tag | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The tag of dependency. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of dependency. |

<a name='M-IoC-Injections-Inject``1-IoC-IContainer,``0,``0-'></a>
### Inject\`\`1(container,destination,source) `method`

##### Summary

Injects a dependency. Just the injection marker.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| destination | [\`\`0](#T-``0 '``0') | The destination member for injection. |
| source | [\`\`0](#T-``0 '``0') | The source of injection. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-IoC-Injections-TryInject-IoC-IContainer,System-Type-'></a>
### TryInject(container,type) `method`

##### Summary

Try to inject a dependency. Just the injection marker.

##### Returns

The injected instance or `default(T)`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of dependency. |

<a name='M-IoC-Injections-TryInject-IoC-IContainer,System-Type,System-Object-'></a>
### TryInject(container,type,tag) `method`

##### Summary

Try to inject a dependency. Just the injection marker.

##### Returns

The injected instance or `default(T)`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of dependency. |
| tag | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The tag of dependency. |

<a name='M-IoC-Injections-TryInject``1-IoC-IContainer-'></a>
### TryInject\`\`1(container) `method`

##### Summary

Try to inject a dependency. Just the injection marker.

##### Returns

The injected instance or `default(T)`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of dependency. |

<a name='M-IoC-Injections-TryInject``1-IoC-IContainer,System-Object-'></a>
### TryInject\`\`1(container,tag) `method`

##### Summary

Try to inject a dependency. Just the injection marker.

##### Returns

The injected instance or `default(T)`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| tag | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The tag of dependency. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of dependency. |

<a name='T-IoC-InstantHandleAttribute'></a>
## InstantHandleAttribute `type`

##### Namespace

IoC

##### Summary

Tells code analysis engine if the parameter is completely handled when the invoked method is on stack.
If the parameter is a delegate, indicates that delegate is executed while the method is executed.
If the parameter is an enumerable, indicates that it is enumerated while the method is executed.

<a name='T-IoC-InvokerParameterNameAttribute'></a>
## InvokerParameterNameAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that the function argument should be string literal and match one
of the parameters of the caller function. For example, ReSharper annotates
the parameter of [ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException').

##### Example

```
void Foo(string param) {
  if (param == null)
    throw new ArgumentNullException("par"); // Warning: Cannot resolve symbol
}
```

<a name='T-IoC-ItemCanBeNullAttribute'></a>
## ItemCanBeNullAttribute `type`

##### Namespace

IoC

##### Summary

Can be appplied to symbols of types derived from IEnumerable as well as to symbols of Task
and Lazy classes to indicate that the value of a collection item, of the Task.Result property
or of the Lazy.Value property can be null.

<a name='T-IoC-ItemNotNullAttribute'></a>
## ItemNotNullAttribute `type`

##### Namespace

IoC

##### Summary

Can be appplied to symbols of types derived from IEnumerable as well as to symbols of Task
and Lazy classes to indicate that the value of a collection item, of the Task.Result property
or of the Lazy.Value property can never be null.

<a name='T-IoC-Key'></a>
## Key `type`

##### Namespace

IoC

##### Summary

Represents a dependency key.

<a name='M-IoC-Key-#ctor-System-Type,System-Object-'></a>
### #ctor(type,tag) `constructor`

##### Summary

Creates the instance of Key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') |  |
| tag | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='F-IoC-Key-AnyTag'></a>
### AnyTag `constants`

##### Summary

The marker object for any tag.

<a name='F-IoC-Key-Tag'></a>
### Tag `constants`

##### Summary

The tag.

<a name='F-IoC-Key-Type'></a>
### Type `constants`

##### Summary

The type.

<a name='M-IoC-Key-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Key-Equals-IoC-Key-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Key-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Key-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Lifetimes-KeyBasedLifetime`1'></a>
## KeyBasedLifetime\`1 `type`

##### Namespace

IoC.Lifetimes

##### Summary

Represents the abstraction for singleton based lifetimes.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TKey | The key type. |

<a name='M-IoC-Lifetimes-KeyBasedLifetime`1-Build-IoC-IBuildContext,System-Linq-Expressions-Expression-'></a>
### Build() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-KeyBasedLifetime`1-Create'></a>
### Create() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-KeyBasedLifetime`1-CreateKey-IoC-IContainer,System-Object[]-'></a>
### CreateKey(container,args) `method`

##### Summary

Creates key for singleton.

##### Returns

The created key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The arguments. |

<a name='M-IoC-Lifetimes-KeyBasedLifetime`1-Dispose'></a>
### Dispose() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-KeyBasedLifetime`1-OnInstanceReleased-System-Object,`0-'></a>
### OnInstanceReleased(releasedInstance,key) `method`

##### Summary

Is invoked on the instance was released.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| releasedInstance | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The released instance. |
| key | [\`0](#T-`0 '`0') | The instance key. |

<a name='M-IoC-Lifetimes-KeyBasedLifetime`1-OnNewInstanceCreated``1-``0,`0,IoC-IContainer,System-Object[]-'></a>
### OnNewInstanceCreated\`\`1(newInstance,key,container,args) `method`

##### Summary

Is invoked on the new instance creation.

##### Returns

The created instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newInstance | [\`\`0](#T-``0 '``0') | The new instance. |
| key | [\`0](#T-`0 '`0') | The instance key. |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | Optional arguments. |

<a name='M-IoC-Lifetimes-KeyBasedLifetime`1-SelectResolvingContainer-IoC-IContainer,IoC-IContainer-'></a>
### SelectResolvingContainer() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Features-LazyFeature'></a>
## LazyFeature `type`

##### Namespace

IoC.Features

##### Summary

Allows to resolve Lazy.

<a name='F-IoC-Features-LazyFeature-Default'></a>
### Default `constants`

<a name='M-IoC-Features-LazyFeature-Apply-IoC-IMutableContainer-'></a>
### Apply() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Lifetime'></a>
## Lifetime `type`

##### Namespace

IoC

##### Summary

A set of well-known lifetimes.

<a name='F-IoC-Lifetime-ContainerSingleton'></a>
### ContainerSingleton `constants`

##### Summary

For a singleton instance per container.

<a name='F-IoC-Lifetime-ScopeSingleton'></a>
### ScopeSingleton `constants`

##### Summary

For a singleton instance per scope.

<a name='F-IoC-Lifetime-Singleton'></a>
### Singleton `constants`

##### Summary

For a singleton instance.

<a name='F-IoC-Lifetime-Transient'></a>
### Transient `constants`

##### Summary

For a new instance each time (default).

<a name='T-IoC-LinqTunnelAttribute'></a>
## LinqTunnelAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that method is pure LINQ method, with postponed enumeration (like Enumerables.Select,
.Where). This annotation allows inference of [InstantHandle] annotation for parameters
of delegate type by analyzing LINQ method chains.

<a name='T-IoC-LocalizationRequiredAttribute'></a>
## LocalizationRequiredAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that marked element should be localized or not.

##### Example

```
[LocalizationRequiredAttribute(true)]
class Foo {
  string str = "my string"; // Warning: Localizable string
}
```

<a name='T-IoC-MacroAttribute'></a>
## MacroAttribute `type`

##### Namespace

IoC

##### Summary

Allows specifying a macro for a parameter of a [SourceTemplateAttribute](#T-IoC-SourceTemplateAttribute 'IoC.SourceTemplateAttribute').

##### Example

Applying the attribute on a source template method:

```
[SourceTemplate, Macro(Target = "item", Expression = "suggestVariableName()")]
public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; collection) {
  foreach (var item in collection) {
    //$ $END$
  }
}
```

Applying the attribute on a template method parameter:

```
[SourceTemplate]
public static void something(this Entity x, [Macro(Expression = "guid()", Editable = -1)] string newguid) {
  /*$ var $x$Id = "$newguid$" + x.ToString();
  x.DoSomething($x$Id); */
}
```

##### Remarks

You can apply the attribute on the whole method or on any of its additional parameters. The macro expression
is defined in the [Expression](#P-IoC-MacroAttribute-Expression 'IoC.MacroAttribute.Expression') property. When applied on a method, the target
template parameter is defined in the [Target](#P-IoC-MacroAttribute-Target 'IoC.MacroAttribute.Target') property. To apply the macro silently
for the parameter, set the [Editable](#P-IoC-MacroAttribute-Editable 'IoC.MacroAttribute.Editable') property value = -1.

<a name='P-IoC-MacroAttribute-Editable'></a>
### Editable `property`

##### Summary

Allows specifying which occurrence of the target parameter becomes editable when the template is deployed.

##### Remarks

If the target parameter is used several times in the template, only one occurrence becomes editable;
other occurrences are changed synchronously. To specify the zero-based index of the editable occurrence,
use values >= 0. To make the parameter non-editable when the template is expanded, use -1.

<a name='P-IoC-MacroAttribute-Expression'></a>
### Expression `property`

##### Summary

Allows specifying a macro that will be executed for a [SourceTemplateAttribute](#T-IoC-SourceTemplateAttribute 'IoC.SourceTemplateAttribute')
parameter when the template is expanded.

<a name='P-IoC-MacroAttribute-Target'></a>
### Target `property`

##### Summary

Identifies the target parameter of a [SourceTemplateAttribute](#T-IoC-SourceTemplateAttribute 'IoC.SourceTemplateAttribute') if the
[MacroAttribute](#T-IoC-MacroAttribute 'IoC.MacroAttribute') is applied on a template method.

<a name='T-IoC-MeansImplicitUseAttribute'></a>
## MeansImplicitUseAttribute `type`

##### Namespace

IoC

##### Summary

Should be used on attributes and causes ReSharper to not mark symbols marked with such attributes
as unused (as well as by other usage inspections)

<a name='T-IoC-MustUseReturnValueAttribute'></a>
## MustUseReturnValueAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that the return value of method invocation must be used.

<a name='T-IoC-NoEnumerationAttribute'></a>
## NoEnumerationAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that IEnumerable, passed as parameter, is not enumerated.

<a name='T-IoC-NoReorder'></a>
## NoReorder `type`

##### Namespace

IoC

##### Summary

Prevents the Member Reordering feature from tossing members of the marked class.

##### Remarks

The attribute must be mentioned in your member reordering patterns

<a name='T-IoC-NotNullAttribute'></a>
## NotNullAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that the value of the marked element could never be `null`.

##### Example

```
[NotNull] object Foo() {
  return null; // Warning: Possible 'null' assignment
}
```

<a name='T-IoC-NotifyPropertyChangedInvocatorAttribute'></a>
## NotifyPropertyChangedInvocatorAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that the method is contained in a type that implements
 `System.ComponentModel.INotifyPropertyChanged` interface and this method
 is used to notify that some property value changed.

##### Example

```
 public class Foo : INotifyPropertyChanged {
   public event PropertyChangedEventHandler PropertyChanged;
 
   [NotifyPropertyChangedInvocator]
   protected virtual void NotifyChanged(string propertyName) { ... }
   string _name;
 
   public string Name {
     get { return _name; }
     set { _name = value; NotifyChanged("LastName"); /* Warning */ }
   }
 }
 
```

Examples of generated notifications:

##### Remarks

The method should be non-static and conform to one of the supported signatures:

<a name='T-IoC-PathReferenceAttribute'></a>
## PathReferenceAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that a parameter is a path to a file or a folder within a web project.
Path can be relative or absolute, starting from web root (~).

<a name='T-IoC-ProvidesContextAttribute'></a>
## ProvidesContextAttribute `type`

##### Namespace

IoC

##### Summary

Indicates the type member or parameter of some type, that should be used instead of all other ways
to get the value that type. This annotation is useful when you have some "context" value evaluated
and stored somewhere, meaning that all other ways to get this value must be consolidated with existing one.

##### Example

```
class Foo {
  [ProvidesContext] IBarService _barService = ...;
  void ProcessNode(INode node) {
    DoSomething(node, node.GetGlobalServices().Bar);
    //              ^ Warning: use value of '_barService' field
  }
}
```

<a name='T-IoC-PublicAPIAttribute'></a>
## PublicAPIAttribute `type`

##### Namespace

IoC

##### Summary

This attribute is intended to mark publicly available API
which should not be removed and so is treated as used.

<a name='T-IoC-PureAttribute'></a>
## PureAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that a method does not make any observable state changes.
The same as `System.Diagnostics.Contracts.PureAttribute`.

##### Example

```
[Pure] int Multiply(int x, int y) =&gt; x * y;
void M() {
  Multiply(123, 42); // Waring: Return value of pure method is not used
}
```

<a name='T-IoC-RazorSectionAttribute'></a>
## RazorSectionAttribute `type`

##### Namespace

IoC

##### Summary

Razor attribute. Indicates that a parameter or a method is a Razor section.
Use this attribute for custom wrappers similar to 
`System.Web.WebPages.WebPageBase.RenderSection(String)`.

<a name='T-IoC-RegexPatternAttribute'></a>
## RegexPatternAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that parameter is regular expression pattern.

<a name='T-IoC-Resolver`1'></a>
## Resolver\`1 `type`

##### Namespace

IoC

##### Summary

Represents an abstraction of instance resolver.

##### Returns

The resolved instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [T:IoC.Resolver\`1](#T-T-IoC-Resolver`1 'T:IoC.Resolver`1') | The resolving container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of resolving instance. |

<a name='T-IoC-Lifetimes-ScopeSingletonLifetime'></a>
## ScopeSingletonLifetime `type`

##### Namespace

IoC.Lifetimes

##### Summary

For a singleton instance per scope.

<a name='M-IoC-Lifetimes-ScopeSingletonLifetime-Create'></a>
### Create() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ScopeSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]-'></a>
### CreateKey() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ScopeSingletonLifetime-OnInstanceReleased-System-Object,IoC-IScope-'></a>
### OnInstanceReleased() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ScopeSingletonLifetime-OnNewInstanceCreated``1-``0,IoC-IScope,IoC-IContainer,System-Object[]-'></a>
### OnNewInstanceCreated\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ScopeSingletonLifetime-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Features-Set'></a>
## Set `type`

##### Namespace

IoC.Features

##### Summary

Represents a feature sets.

<a name='F-IoC-Features-Set-Core'></a>
### Core `constants`

##### Summary

Core features.

<a name='F-IoC-Features-Set-Default'></a>
### Default `constants`

##### Summary

Default features.

<a name='F-IoC-Features-Set-Light'></a>
### Light `constants`

##### Summary

The light set of features.

<a name='T-IoC-Lifetimes-SingletonLifetime'></a>
## SingletonLifetime `type`

##### Namespace

IoC.Lifetimes

##### Summary

For a singleton instance.

<a name='M-IoC-Lifetimes-SingletonLifetime-Build-IoC-IBuildContext,System-Linq-Expressions-Expression-'></a>
### Build() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-SingletonLifetime-Create'></a>
### Create() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-SingletonLifetime-Dispose'></a>
### Dispose() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-SingletonLifetime-SelectResolvingContainer-IoC-IContainer,IoC-IContainer-'></a>
### SelectResolvingContainer() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-SingletonLifetime-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-SourceTemplateAttribute'></a>
## SourceTemplateAttribute `type`

##### Namespace

IoC

##### Summary

An extension method marked with this attribute is processed by ReSharper code completion
as a 'Source Template'. When extension method is completed over some expression, it's source code
is automatically expanded like a template at call site.

##### Example

In this example, the 'forEach' method is a source template available over all values
of enumerable types, producing ordinary C# 'foreach' statement and placing caret inside block:

```
[SourceTemplate]
public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; xs) {
  foreach (var x in xs) {
     //$ $END$
  }
}
```

##### Remarks

Template method body can contain valid source code and/or special comments starting with '$'.
Text inside these comments is added as source code when the template is applied. Template parameters
can be used either as additional method parameters or as identifiers wrapped in two '$' signs.
Use the [MacroAttribute](#T-IoC-MacroAttribute 'IoC.MacroAttribute') attribute to specify macros for parameters.

<a name='T-IoC-StringFormatMethodAttribute'></a>
## StringFormatMethodAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that the marked method builds string by format pattern and (optional) arguments.
Parameter, which contains format string, should be given in constructor. The format string
should be in [Format](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Format 'System.String.Format(System.IFormatProvider,System.String,System.Object[])')-like form.

##### Example

```
[StringFormatMethod("message")]
void ShowError(string message, params object[] args) { /* do something */ }
void Foo() {
  ShowError("Failed: {0}"); // Warning: Non-existing argument in format string
}
```

<a name='M-IoC-StringFormatMethodAttribute-#ctor-System-String-'></a>
### #ctor(formatParameterName) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| formatParameterName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Specifies which parameter of an annotated method should be treated as format-string |

<a name='T-IoC-TT'></a>
## TT `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT1'></a>
## TT1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT10'></a>
## TT10 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT11'></a>
## TT11 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT12'></a>
## TT12 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT13'></a>
## TT13 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT14'></a>
## TT14 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT15'></a>
## TT15 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT16'></a>
## TT16 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT17'></a>
## TT17 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT18'></a>
## TT18 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT19'></a>
## TT19 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT2'></a>
## TT2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT20'></a>
## TT20 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT21'></a>
## TT21 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT22'></a>
## TT22 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT23'></a>
## TT23 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT24'></a>
## TT24 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT25'></a>
## TT25 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT26'></a>
## TT26 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT27'></a>
## TT27 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT28'></a>
## TT28 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT29'></a>
## TT29 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT3'></a>
## TT3 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT30'></a>
## TT30 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT31'></a>
## TT31 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT32'></a>
## TT32 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT4'></a>
## TT4 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT5'></a>
## TT5 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT6'></a>
## TT6 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT7'></a>
## TT7 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT8'></a>
## TT8 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TT9'></a>
## TT9 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a reference type.

<a name='T-IoC-TTCollection1`1'></a>
## TTCollection1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ICollection[T]`.

<a name='T-IoC-TTCollection2`1'></a>
## TTCollection2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ICollection[T]`.

<a name='T-IoC-TTCollection3`1'></a>
## TTCollection3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ICollection[T]`.

<a name='T-IoC-TTCollection4`1'></a>
## TTCollection4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ICollection[T]`.

<a name='T-IoC-TTCollection5`1'></a>
## TTCollection5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ICollection[T]`.

<a name='T-IoC-TTCollection6`1'></a>
## TTCollection6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ICollection[T]`.

<a name='T-IoC-TTCollection7`1'></a>
## TTCollection7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ICollection[T]`.

<a name='T-IoC-TTCollection8`1'></a>
## TTCollection8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ICollection[T]`.

<a name='T-IoC-TTCollection`1'></a>
## TTCollection\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ICollection[T]`.

<a name='T-IoC-TTComparable'></a>
## TTComparable `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable`.

<a name='T-IoC-TTComparable1'></a>
## TTComparable1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable`.

<a name='T-IoC-TTComparable1`1'></a>
## TTComparable1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable[T]`.

<a name='T-IoC-TTComparable2'></a>
## TTComparable2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable`.

<a name='T-IoC-TTComparable2`1'></a>
## TTComparable2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable[T]`.

<a name='T-IoC-TTComparable3'></a>
## TTComparable3 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable`.

<a name='T-IoC-TTComparable3`1'></a>
## TTComparable3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable[T]`.

<a name='T-IoC-TTComparable4'></a>
## TTComparable4 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable`.

<a name='T-IoC-TTComparable4`1'></a>
## TTComparable4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable[T]`.

<a name='T-IoC-TTComparable5'></a>
## TTComparable5 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable`.

<a name='T-IoC-TTComparable5`1'></a>
## TTComparable5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable[T]`.

<a name='T-IoC-TTComparable6'></a>
## TTComparable6 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable`.

<a name='T-IoC-TTComparable6`1'></a>
## TTComparable6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable[T]`.

<a name='T-IoC-TTComparable7'></a>
## TTComparable7 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable`.

<a name='T-IoC-TTComparable7`1'></a>
## TTComparable7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable[T]`.

<a name='T-IoC-TTComparable8'></a>
## TTComparable8 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable`.

<a name='T-IoC-TTComparable8`1'></a>
## TTComparable8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable[T]`.

<a name='T-IoC-TTComparable`1'></a>
## TTComparable\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IComparable[T]`.

<a name='T-IoC-TTComparer1`1'></a>
## TTComparer1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IComparer[T]`.

<a name='T-IoC-TTComparer2`1'></a>
## TTComparer2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IComparer[T]`.

<a name='T-IoC-TTComparer3`1'></a>
## TTComparer3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IComparer[T]`.

<a name='T-IoC-TTComparer4`1'></a>
## TTComparer4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IComparer[T]`.

<a name='T-IoC-TTComparer5`1'></a>
## TTComparer5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IComparer[T]`.

<a name='T-IoC-TTComparer6`1'></a>
## TTComparer6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IComparer[T]`.

<a name='T-IoC-TTComparer7`1'></a>
## TTComparer7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IComparer[T]`.

<a name='T-IoC-TTComparer8`1'></a>
## TTComparer8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IComparer[T]`.

<a name='T-IoC-TTComparer`1'></a>
## TTComparer\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IComparer[T]`.

<a name='T-IoC-TTDictionary1`2'></a>
## TTDictionary1\`2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IDictionary[TKey, TValue]`.

<a name='T-IoC-TTDictionary2`2'></a>
## TTDictionary2\`2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IDictionary[TKey, TValue]`.

<a name='T-IoC-TTDictionary3`2'></a>
## TTDictionary3\`2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IDictionary[TKey, TValue]`.

<a name='T-IoC-TTDictionary4`2'></a>
## TTDictionary4\`2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IDictionary[TKey, TValue]`.

<a name='T-IoC-TTDictionary5`2'></a>
## TTDictionary5\`2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IDictionary[TKey, TValue]`.

<a name='T-IoC-TTDictionary6`2'></a>
## TTDictionary6\`2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IDictionary[TKey, TValue]`.

<a name='T-IoC-TTDictionary7`2'></a>
## TTDictionary7\`2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IDictionary[TKey, TValue]`.

<a name='T-IoC-TTDictionary8`2'></a>
## TTDictionary8\`2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IDictionary[TKey, TValue]`.

<a name='T-IoC-TTDictionary`2'></a>
## TTDictionary\`2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IDictionary[TKey, TValue]`.

<a name='T-IoC-TTDisposable'></a>
## TTDisposable `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IDisposable`.

<a name='T-IoC-TTDisposable1'></a>
## TTDisposable1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IDisposable`.

<a name='T-IoC-TTDisposable2'></a>
## TTDisposable2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IDisposable`.

<a name='T-IoC-TTDisposable3'></a>
## TTDisposable3 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IDisposable`.

<a name='T-IoC-TTDisposable4'></a>
## TTDisposable4 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IDisposable`.

<a name='T-IoC-TTDisposable5'></a>
## TTDisposable5 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IDisposable`.

<a name='T-IoC-TTDisposable6'></a>
## TTDisposable6 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IDisposable`.

<a name='T-IoC-TTDisposable7'></a>
## TTDisposable7 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IDisposable`.

<a name='T-IoC-TTDisposable8'></a>
## TTDisposable8 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IDisposable`.

<a name='T-IoC-TTEnumerable1`1'></a>
## TTEnumerable1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerable[T]`.

<a name='T-IoC-TTEnumerable2`1'></a>
## TTEnumerable2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerable[T]`.

<a name='T-IoC-TTEnumerable3`1'></a>
## TTEnumerable3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerable[T]`.

<a name='T-IoC-TTEnumerable4`1'></a>
## TTEnumerable4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerable[T]`.

<a name='T-IoC-TTEnumerable5`1'></a>
## TTEnumerable5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerable[T]`.

<a name='T-IoC-TTEnumerable6`1'></a>
## TTEnumerable6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerable[T]`.

<a name='T-IoC-TTEnumerable7`1'></a>
## TTEnumerable7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerable[T]`.

<a name='T-IoC-TTEnumerable8`1'></a>
## TTEnumerable8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerable[T]`.

<a name='T-IoC-TTEnumerable`1'></a>
## TTEnumerable\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerable[T]`.

<a name='T-IoC-TTEnumerator1`1'></a>
## TTEnumerator1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerator[T]`.

<a name='T-IoC-TTEnumerator2`1'></a>
## TTEnumerator2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerator[T]`.

<a name='T-IoC-TTEnumerator3`1'></a>
## TTEnumerator3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerator[T]`.

<a name='T-IoC-TTEnumerator4`1'></a>
## TTEnumerator4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerator[T]`.

<a name='T-IoC-TTEnumerator5`1'></a>
## TTEnumerator5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerator[T]`.

<a name='T-IoC-TTEnumerator6`1'></a>
## TTEnumerator6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerator[T]`.

<a name='T-IoC-TTEnumerator7`1'></a>
## TTEnumerator7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerator[T]`.

<a name='T-IoC-TTEnumerator8`1'></a>
## TTEnumerator8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerator[T]`.

<a name='T-IoC-TTEnumerator`1'></a>
## TTEnumerator\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEnumerator[T]`.

<a name='T-IoC-TTEqualityComparer1`1'></a>
## TTEqualityComparer1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEqualityComparer[T]`.

<a name='T-IoC-TTEqualityComparer2`1'></a>
## TTEqualityComparer2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEqualityComparer[T]`.

<a name='T-IoC-TTEqualityComparer3`1'></a>
## TTEqualityComparer3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEqualityComparer[T]`.

<a name='T-IoC-TTEqualityComparer4`1'></a>
## TTEqualityComparer4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEqualityComparer[T]`.

<a name='T-IoC-TTEqualityComparer5`1'></a>
## TTEqualityComparer5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEqualityComparer[T]`.

<a name='T-IoC-TTEqualityComparer6`1'></a>
## TTEqualityComparer6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEqualityComparer[T]`.

<a name='T-IoC-TTEqualityComparer7`1'></a>
## TTEqualityComparer7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEqualityComparer[T]`.

<a name='T-IoC-TTEqualityComparer8`1'></a>
## TTEqualityComparer8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEqualityComparer[T]`.

<a name='T-IoC-TTEqualityComparer`1'></a>
## TTEqualityComparer\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IEqualityComparer[T]`.

<a name='T-IoC-TTEquatable1`1'></a>
## TTEquatable1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IEquatable[T]`.

<a name='T-IoC-TTEquatable2`1'></a>
## TTEquatable2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IEquatable[T]`.

<a name='T-IoC-TTEquatable3`1'></a>
## TTEquatable3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IEquatable[T]`.

<a name='T-IoC-TTEquatable4`1'></a>
## TTEquatable4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IEquatable[T]`.

<a name='T-IoC-TTEquatable5`1'></a>
## TTEquatable5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IEquatable[T]`.

<a name='T-IoC-TTEquatable6`1'></a>
## TTEquatable6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IEquatable[T]`.

<a name='T-IoC-TTEquatable7`1'></a>
## TTEquatable7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IEquatable[T]`.

<a name='T-IoC-TTEquatable8`1'></a>
## TTEquatable8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IEquatable[T]`.

<a name='T-IoC-TTEquatable`1'></a>
## TTEquatable\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IEquatable[T]`.

<a name='T-IoC-TTI'></a>
## TTI `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI1'></a>
## TTI1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI10'></a>
## TTI10 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI11'></a>
## TTI11 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI12'></a>
## TTI12 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI13'></a>
## TTI13 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI14'></a>
## TTI14 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI15'></a>
## TTI15 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI16'></a>
## TTI16 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI17'></a>
## TTI17 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI18'></a>
## TTI18 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI19'></a>
## TTI19 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI2'></a>
## TTI2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI20'></a>
## TTI20 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI21'></a>
## TTI21 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI22'></a>
## TTI22 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI23'></a>
## TTI23 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI24'></a>
## TTI24 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI25'></a>
## TTI25 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI26'></a>
## TTI26 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI27'></a>
## TTI27 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI28'></a>
## TTI28 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI29'></a>
## TTI29 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI3'></a>
## TTI3 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI30'></a>
## TTI30 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI31'></a>
## TTI31 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI32'></a>
## TTI32 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI4'></a>
## TTI4 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI5'></a>
## TTI5 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI6'></a>
## TTI6 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI7'></a>
## TTI7 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI8'></a>
## TTI8 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTI9'></a>
## TTI9 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for an interface.

<a name='T-IoC-TTList1`1'></a>
## TTList1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IList[T]`.

<a name='T-IoC-TTList2`1'></a>
## TTList2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IList[T]`.

<a name='T-IoC-TTList3`1'></a>
## TTList3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IList[T]`.

<a name='T-IoC-TTList4`1'></a>
## TTList4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IList[T]`.

<a name='T-IoC-TTList5`1'></a>
## TTList5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IList[T]`.

<a name='T-IoC-TTList6`1'></a>
## TTList6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IList[T]`.

<a name='T-IoC-TTList7`1'></a>
## TTList7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IList[T]`.

<a name='T-IoC-TTList8`1'></a>
## TTList8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IList[T]`.

<a name='T-IoC-TTList`1'></a>
## TTList\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.IList[T]`.

<a name='T-IoC-TTObservable1`1'></a>
## TTObservable1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObservable[T]`.

<a name='T-IoC-TTObservable2`1'></a>
## TTObservable2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObservable[T]`.

<a name='T-IoC-TTObservable3`1'></a>
## TTObservable3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObservable[T]`.

<a name='T-IoC-TTObservable4`1'></a>
## TTObservable4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObservable[T]`.

<a name='T-IoC-TTObservable5`1'></a>
## TTObservable5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObservable[T]`.

<a name='T-IoC-TTObservable6`1'></a>
## TTObservable6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObservable[T]`.

<a name='T-IoC-TTObservable7`1'></a>
## TTObservable7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObservable[T]`.

<a name='T-IoC-TTObservable8`1'></a>
## TTObservable8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObservable[T]`.

<a name='T-IoC-TTObservable`1'></a>
## TTObservable\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObservable[T]`.

<a name='T-IoC-TTObserver1`1'></a>
## TTObserver1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObserver[T]`.

<a name='T-IoC-TTObserver2`1'></a>
## TTObserver2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObserver[T]`.

<a name='T-IoC-TTObserver3`1'></a>
## TTObserver3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObserver[T]`.

<a name='T-IoC-TTObserver4`1'></a>
## TTObserver4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObserver[T]`.

<a name='T-IoC-TTObserver5`1'></a>
## TTObserver5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObserver[T]`.

<a name='T-IoC-TTObserver6`1'></a>
## TTObserver6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObserver[T]`.

<a name='T-IoC-TTObserver7`1'></a>
## TTObserver7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObserver[T]`.

<a name='T-IoC-TTObserver8`1'></a>
## TTObserver8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObserver[T]`.

<a name='T-IoC-TTObserver`1'></a>
## TTObserver\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.IObserver[T]`.

<a name='T-IoC-TTS'></a>
## TTS `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS1'></a>
## TTS1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS10'></a>
## TTS10 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS11'></a>
## TTS11 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS12'></a>
## TTS12 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS13'></a>
## TTS13 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS14'></a>
## TTS14 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS15'></a>
## TTS15 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS16'></a>
## TTS16 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS17'></a>
## TTS17 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS18'></a>
## TTS18 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS19'></a>
## TTS19 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS2'></a>
## TTS2 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS20'></a>
## TTS20 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS21'></a>
## TTS21 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS22'></a>
## TTS22 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS23'></a>
## TTS23 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS24'></a>
## TTS24 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS25'></a>
## TTS25 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS26'></a>
## TTS26 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS27'></a>
## TTS27 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS28'></a>
## TTS28 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS29'></a>
## TTS29 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS3'></a>
## TTS3 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS30'></a>
## TTS30 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS31'></a>
## TTS31 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS32'></a>
## TTS32 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS4'></a>
## TTS4 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS5'></a>
## TTS5 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS6'></a>
## TTS6 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS7'></a>
## TTS7 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS8'></a>
## TTS8 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTS9'></a>
## TTS9 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for a value type.

<a name='T-IoC-TTSet1`1'></a>
## TTSet1\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ISet[T]`.

<a name='T-IoC-TTSet2`1'></a>
## TTSet2\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ISet[T]`.

<a name='T-IoC-TTSet3`1'></a>
## TTSet3\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ISet[T]`.

<a name='T-IoC-TTSet4`1'></a>
## TTSet4\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ISet[T]`.

<a name='T-IoC-TTSet5`1'></a>
## TTSet5\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ISet[T]`.

<a name='T-IoC-TTSet6`1'></a>
## TTSet6\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ISet[T]`.

<a name='T-IoC-TTSet7`1'></a>
## TTSet7\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ISet[T]`.

<a name='T-IoC-TTSet8`1'></a>
## TTSet8\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ISet[T]`.

<a name='T-IoC-TTSet`1'></a>
## TTSet\`1 `type`

##### Namespace

IoC

##### Summary

Represents the generic type arguments marker for `System.Collections.Generic.ISet[T]`.

<a name='T-IoC-Tag'></a>
## Tag `type`

##### Namespace

IoC

##### Summary

Represents a tag holder.

<a name='M-IoC-Tag-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Features-TaskFeature'></a>
## TaskFeature `type`

##### Namespace

IoC.Features

##### Summary

Allows to resolve Tasks.

<a name='F-IoC-Features-TaskFeature-Default'></a>
### Default `constants`

<a name='F-IoC-Features-TaskFeature-Tag'></a>
### Tag `constants`

##### Summary

TaskFeature default tag

<a name='M-IoC-Features-TaskFeature-Apply-IoC-IMutableContainer-'></a>
### Apply() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-TerminatesProgramAttribute'></a>
## TerminatesProgramAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that the marked method unconditionally terminates control flow execution.
For example, it could unconditionally throw exception.

<a name='T-IoC-TraceEvent'></a>
## TraceEvent `type`

##### Namespace

IoC

##### Summary

Represents a container trace event.

<a name='M-IoC-TraceEvent-#ctor-IoC-ContainerEvent,System-String-'></a>
### #ctor(containerEvent,message) `constructor`

##### Summary

Creates new instance of a trace event.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| containerEvent | [IoC.ContainerEvent](#T-IoC-ContainerEvent 'IoC.ContainerEvent') | The original container event. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The trace message. |

<a name='F-IoC-TraceEvent-ContainerEvent'></a>
### ContainerEvent `constants`

##### Summary

The original container event.

<a name='F-IoC-TraceEvent-Message'></a>
### Message `constants`

##### Summary

The trace message.

<a name='T-IoC-Features-TupleFeature'></a>
## TupleFeature `type`

##### Namespace

IoC.Features

##### Summary

Allows to resolve Tuples.

<a name='F-IoC-Features-TupleFeature-Default'></a>
### Default `constants`

<a name='F-IoC-Features-TupleFeature-Light'></a>
### Light `constants`

<a name='M-IoC-Features-TupleFeature-Apply-IoC-IMutableContainer-'></a>
### Apply() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-UsedImplicitlyAttribute'></a>
## UsedImplicitlyAttribute `type`

##### Namespace

IoC

##### Summary

Indicates that the marked symbol is used implicitly (e.g. via reflection, in external library),
so this symbol will not be marked as unused (as well as by other usage inspections).

<a name='T-IoC-ValueProviderAttribute'></a>
## ValueProviderAttribute `type`

##### Namespace

IoC

##### Summary

For a parameter that is expected to be one of the limited set of values.
Specify fields of which type should be used as values for this parameter.

<a name='T-IoC-WellknownExpressions'></a>
## WellknownExpressions `type`

##### Namespace

IoC

##### Summary

The list of well-known expressions.

<a name='F-IoC-WellknownExpressions-ArgsParameter'></a>
### ArgsParameter `constants`

##### Summary

The args parameters.

<a name='F-IoC-WellknownExpressions-ContainerParameter'></a>
### ContainerParameter `constants`

##### Summary

The container parameter.

<a name='T-IoC-XamlItemBindingOfItemsControlAttribute'></a>
## XamlItemBindingOfItemsControlAttribute `type`

##### Namespace

IoC

##### Summary

XAML attribute. Indicates the property of some `BindingBase`-derived type, that
is used to bind some item of `ItemsControl`-derived type. This annotation will
enable the `DataContext` type resolve for XAML bindings for such properties.

##### Remarks

Property should have the tree ancestor of the `ItemsControl` type or
marked with the [XamlItemsControlAttribute](#T-IoC-XamlItemsControlAttribute 'IoC.XamlItemsControlAttribute') attribute.

<a name='T-IoC-XamlItemsControlAttribute'></a>
## XamlItemsControlAttribute `type`

##### Namespace

IoC

##### Summary

XAML attribute. Indicates the type that has `ItemsSource` property and should be treated
as `ItemsControl`-derived type, to enable inner items `DataContext` type resolve.
