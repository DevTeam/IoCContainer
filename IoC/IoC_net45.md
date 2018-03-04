<a name='contents'></a>
# Contents [#](#contents 'Go To Here')

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
- [AssertionConditionAttribute](#T-IoC-AssertionConditionAttribute 'IoC.AssertionConditionAttribute')
- [AssertionConditionType](#T-IoC-AssertionConditionType 'IoC.AssertionConditionType')
  - [IS_FALSE](#F-IoC-AssertionConditionType-IS_FALSE 'IoC.AssertionConditionType.IS_FALSE')
  - [IS_NOT_NULL](#F-IoC-AssertionConditionType-IS_NOT_NULL 'IoC.AssertionConditionType.IS_NOT_NULL')
  - [IS_NULL](#F-IoC-AssertionConditionType-IS_NULL 'IoC.AssertionConditionType.IS_NULL')
  - [IS_TRUE](#F-IoC-AssertionConditionType-IS_TRUE 'IoC.AssertionConditionType.IS_TRUE')
- [AssertionMethodAttribute](#T-IoC-AssertionMethodAttribute 'IoC.AssertionMethodAttribute')
- [BaseTypeRequiredAttribute](#T-IoC-BaseTypeRequiredAttribute 'IoC.BaseTypeRequiredAttribute')
- [CanBeNullAttribute](#T-IoC-CanBeNullAttribute 'IoC.CanBeNullAttribute')
- [CannotApplyEqualityOperatorAttribute](#T-IoC-CannotApplyEqualityOperatorAttribute 'IoC.CannotApplyEqualityOperatorAttribute')
- [CollectionAccessAttribute](#T-IoC-CollectionAccessAttribute 'IoC.CollectionAccessAttribute')
- [CollectionAccessType](#T-IoC-CollectionAccessType 'IoC.CollectionAccessType')
  - [ModifyExistingContent](#F-IoC-CollectionAccessType-ModifyExistingContent 'IoC.CollectionAccessType.ModifyExistingContent')
  - [None](#F-IoC-CollectionAccessType-None 'IoC.CollectionAccessType.None')
  - [Read](#F-IoC-CollectionAccessType-Read 'IoC.CollectionAccessType.Read')
  - [UpdatedContent](#F-IoC-CollectionAccessType-UpdatedContent 'IoC.CollectionAccessType.UpdatedContent')
- [CollectionFeature](#T-IoC-Features-CollectionFeature 'IoC.Features.CollectionFeature')
  - [Shared](#F-IoC-Features-CollectionFeature-Shared 'IoC.Features.CollectionFeature.Shared')
  - [Apply()](#M-IoC-Features-CollectionFeature-Apply-IoC-IContainer- 'IoC.Features.CollectionFeature.Apply(IoC.IContainer)')
- [ConfigurationFeature](#T-IoC-Features-ConfigurationFeature 'IoC.Features.ConfigurationFeature')
  - [Shared](#F-IoC-Features-ConfigurationFeature-Shared 'IoC.Features.ConfigurationFeature.Shared')
  - [Apply()](#M-IoC-Features-ConfigurationFeature-Apply-IoC-IContainer- 'IoC.Features.ConfigurationFeature.Apply(IoC.IContainer)')
- [Container](#T-IoC-Container 'IoC.Container')
  - [Parent](#P-IoC-Container-Parent 'IoC.Container.Parent')
  - [Create(name)](#M-IoC-Container-Create-System-String- 'IoC.Container.Create(System.String)')
  - [Create(configurations)](#M-IoC-Container-Create-IoC-IConfiguration[]- 'IoC.Container.Create(IoC.IConfiguration[])')
  - [Create(name,configurations)](#M-IoC-Container-Create-System-String,IoC-IConfiguration[]- 'IoC.Container.Create(System.String,IoC.IConfiguration[])')
  - [CreateBasic(name)](#M-IoC-Container-CreateBasic-System-String- 'IoC.Container.CreateBasic(System.String)')
  - [Dispose()](#M-IoC-Container-Dispose 'IoC.Container.Dispose')
  - [GetEnumerator()](#M-IoC-Container-GetEnumerator 'IoC.Container.GetEnumerator')
  - [Subscribe()](#M-IoC-Container-Subscribe-System-IObserver{IoC-ContainerEvent}- 'IoC.Container.Subscribe(System.IObserver{IoC.ContainerEvent})')
  - [ToString()](#M-IoC-Container-ToString 'IoC.Container.ToString')
  - [TryGetDependency()](#M-IoC-Container-TryGetDependency-IoC-Key,IoC-IDependency@,IoC-ILifetime@- 'IoC.Container.TryGetDependency(IoC.Key,IoC.IDependency@,IoC.ILifetime@)')
  - [TryGetResolver\`\`1()](#M-IoC-Container-TryGetResolver``1-System-Type,System-Object,IoC-Resolver{``0}@,IoC-IContainer- 'IoC.Container.TryGetResolver``1(System.Type,System.Object,IoC.Resolver{``0}@,IoC.IContainer)')
  - [TryGetResolver\`\`1()](#M-IoC-Container-TryGetResolver``1-System-Type,IoC-Resolver{``0}@,IoC-IContainer- 'IoC.Container.TryGetResolver``1(System.Type,IoC.Resolver{``0}@,IoC.IContainer)')
  - [TryRegister()](#M-IoC-Container-TryRegister-System-Collections-Generic-IEnumerable{IoC-Key},IoC-IDependency,IoC-ILifetime,System-IDisposable@- 'IoC.Container.TryRegister(System.Collections.Generic.IEnumerable{IoC.Key},IoC.IDependency,IoC.ILifetime,System.IDisposable@)')
- [ContainerEvent](#T-IoC-ContainerEvent 'IoC.ContainerEvent')
  - [Container](#F-IoC-ContainerEvent-Container 'IoC.ContainerEvent.Container')
  - [EventTypeType](#F-IoC-ContainerEvent-EventTypeType 'IoC.ContainerEvent.EventTypeType')
  - [Key](#F-IoC-ContainerEvent-Key 'IoC.ContainerEvent.Key')
- [ContainerSingletonLifetime](#T-IoC-Lifetimes-ContainerSingletonLifetime 'IoC.Lifetimes.ContainerSingletonLifetime')
  - [#ctor()](#M-IoC-Lifetimes-ContainerSingletonLifetime-#ctor-System-Func{IoC-ILifetime}- 'IoC.Lifetimes.ContainerSingletonLifetime.#ctor(System.Func{IoC.ILifetime})')
  - [Clone()](#M-IoC-Lifetimes-ContainerSingletonLifetime-Clone 'IoC.Lifetimes.ContainerSingletonLifetime.Clone')
  - [CreateKey()](#M-IoC-Lifetimes-ContainerSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]- 'IoC.Lifetimes.ContainerSingletonLifetime.CreateKey(IoC.IContainer,System.Object[])')
  - [ToString()](#M-IoC-Lifetimes-ContainerSingletonLifetime-ToString 'IoC.Lifetimes.ContainerSingletonLifetime.ToString')
- [Context](#T-IoC-Context 'IoC.Context')
  - [Args](#F-IoC-Context-Args 'IoC.Context.Args')
  - [Container](#F-IoC-Context-Container 'IoC.Context.Container')
  - [Key](#F-IoC-Context-Key 'IoC.Context.Key')
- [Context\`1](#T-IoC-Context`1 'IoC.Context`1')
  - [It](#F-IoC-Context`1-It 'IoC.Context`1.It')
- [ContractAnnotationAttribute](#T-IoC-ContractAnnotationAttribute 'IoC.ContractAnnotationAttribute')
- [CoreFeature](#T-IoC-Features-CoreFeature 'IoC.Features.CoreFeature')
  - [Shared](#F-IoC-Features-CoreFeature-Shared 'IoC.Features.CoreFeature.Shared')
  - [Apply()](#M-IoC-Features-CoreFeature-Apply-IoC-IContainer- 'IoC.Features.CoreFeature.Apply(IoC.IContainer)')
- [EventType](#T-IoC-ContainerEvent-EventType 'IoC.ContainerEvent.EventType')
  - [Registration](#F-IoC-ContainerEvent-EventType-Registration 'IoC.ContainerEvent.EventType.Registration')
  - [Unregistration](#F-IoC-ContainerEvent-EventType-Unregistration 'IoC.ContainerEvent.EventType.Unregistration')
- [ExpressionCompilerFeature](#T-IoC-Features-ExpressionCompilerFeature 'IoC.Features.ExpressionCompilerFeature')
  - [Shared](#F-IoC-Features-ExpressionCompilerFeature-Shared 'IoC.Features.ExpressionCompilerFeature.Shared')
  - [Apply()](#M-IoC-Features-ExpressionCompilerFeature-Apply-IoC-IContainer- 'IoC.Features.ExpressionCompilerFeature.Apply(IoC.IContainer)')
- [Feature](#T-IoC-Features-Feature 'IoC.Features.Feature')
  - [BasicSet](#F-IoC-Features-Feature-BasicSet 'IoC.Features.Feature.BasicSet')
  - [DefaultSet](#F-IoC-Features-Feature-DefaultSet 'IoC.Features.Feature.DefaultSet')
- [Fluent](#T-IoC-Fluent 'IoC.Fluent')
  - [CreateChild(parent,name)](#M-IoC-Fluent-CreateChild-IoC-IContainer,System-String- 'IoC.Fluent.CreateChild(IoC.IContainer,System.String)')
  - [Validate(container)](#M-IoC-Fluent-Validate-IoC-IContainer- 'IoC.Fluent.Validate(IoC.IContainer)')
- [FluentBind](#T-IoC-FluentBind 'IoC.FluentBind')
  - [AnyTag\`\`1(binding)](#M-IoC-FluentBind-AnyTag``1-IoC-IBinding{``0}- 'IoC.FluentBind.AnyTag``1(IoC.IBinding{``0})')
  - [As\`\`1(binding,lifetime)](#M-IoC-FluentBind-As``1-IoC-IBinding{``0},IoC-Lifetime- 'IoC.FluentBind.As``1(IoC.IBinding{``0},IoC.Lifetime)')
  - [Bind(container,types)](#M-IoC-FluentBind-Bind-IoC-IContainer,System-Type[]- 'IoC.FluentBind.Bind(IoC.IContainer,System.Type[])')
  - [Bind\`\`1(container)](#M-IoC-FluentBind-Bind``1-IoC-IContainer- 'IoC.FluentBind.Bind``1(IoC.IContainer)')
  - [Bind\`\`2(container)](#M-IoC-FluentBind-Bind``2-IoC-IContainer- 'IoC.FluentBind.Bind``2(IoC.IContainer)')
  - [Bind\`\`3(container)](#M-IoC-FluentBind-Bind``3-IoC-IContainer- 'IoC.FluentBind.Bind``3(IoC.IContainer)')
  - [Bind\`\`4(container)](#M-IoC-FluentBind-Bind``4-IoC-IContainer- 'IoC.FluentBind.Bind``4(IoC.IContainer)')
  - [Bind\`\`5(container)](#M-IoC-FluentBind-Bind``5-IoC-IContainer- 'IoC.FluentBind.Bind``5(IoC.IContainer)')
  - [Bind\`\`6(container)](#M-IoC-FluentBind-Bind``6-IoC-IContainer- 'IoC.FluentBind.Bind``6(IoC.IContainer)')
  - [Bind\`\`7(container)](#M-IoC-FluentBind-Bind``7-IoC-IContainer- 'IoC.FluentBind.Bind``7(IoC.IContainer)')
  - [Bind\`\`8(container)](#M-IoC-FluentBind-Bind``8-IoC-IContainer- 'IoC.FluentBind.Bind``8(IoC.IContainer)')
  - [Lifetime\`\`1(binding,lifetime)](#M-IoC-FluentBind-Lifetime``1-IoC-IBinding{``0},IoC-ILifetime- 'IoC.FluentBind.Lifetime``1(IoC.IBinding{``0},IoC.ILifetime)')
  - [Tag\`\`1(binding,tagValue)](#M-IoC-FluentBind-Tag``1-IoC-IBinding{``0},System-Object- 'IoC.FluentBind.Tag``1(IoC.IBinding{``0},System.Object)')
  - [To(binding,type,constructorFilter)](#M-IoC-FluentBind-To-IoC-IBinding{System-Object},System-Type,System-Predicate{System-Reflection-ConstructorInfo}- 'IoC.FluentBind.To(IoC.IBinding{System.Object},System.Type,System.Predicate{System.Reflection.ConstructorInfo})')
  - [To\`\`1(binding,constructorFilter)](#M-IoC-FluentBind-To``1-IoC-IBinding{``0},System-Predicate{System-Reflection-ConstructorInfo}- 'IoC.FluentBind.To``1(IoC.IBinding{``0},System.Predicate{System.Reflection.ConstructorInfo})')
  - [To\`\`1(binding,factory,statements)](#M-IoC-FluentBind-To``1-IoC-IBinding{``0},System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentBind.To``1(IoC.IBinding{``0},System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [ToSelf(registrationToken)](#M-IoC-FluentBind-ToSelf-System-IDisposable- 'IoC.FluentBind.ToSelf(System.IDisposable)')
- [FluentConfiguration](#T-IoC-FluentConfiguration 'IoC.FluentConfiguration')
  - [Apply(container,configurationText)](#M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-String[]- 'IoC.FluentConfiguration.Apply(IoC.IContainer,System.String[])')
  - [Apply(container,configurationStreams)](#M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-IO-Stream[]- 'IoC.FluentConfiguration.Apply(IoC.IContainer,System.IO.Stream[])')
  - [Apply(container,configurationReaders)](#M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-IO-TextReader[]- 'IoC.FluentConfiguration.Apply(IoC.IContainer,System.IO.TextReader[])')
  - [Apply(container,configurations)](#M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-Collections-Generic-IEnumerable{IoC-IConfiguration}- 'IoC.FluentConfiguration.Apply(IoC.IContainer,System.Collections.Generic.IEnumerable{IoC.IConfiguration})')
  - [Apply(container,configurations)](#M-IoC-FluentConfiguration-Apply-IoC-IContainer,IoC-IConfiguration[]- 'IoC.FluentConfiguration.Apply(IoC.IContainer,IoC.IConfiguration[])')
  - [Using(container,configurationText)](#M-IoC-FluentConfiguration-Using-IoC-IContainer,System-String[]- 'IoC.FluentConfiguration.Using(IoC.IContainer,System.String[])')
  - [Using(container,configurationStreams)](#M-IoC-FluentConfiguration-Using-IoC-IContainer,System-IO-Stream[]- 'IoC.FluentConfiguration.Using(IoC.IContainer,System.IO.Stream[])')
  - [Using(container,configurationReaders)](#M-IoC-FluentConfiguration-Using-IoC-IContainer,System-IO-TextReader[]- 'IoC.FluentConfiguration.Using(IoC.IContainer,System.IO.TextReader[])')
  - [Using(container,configurations)](#M-IoC-FluentConfiguration-Using-IoC-IContainer,IoC-IConfiguration[]- 'IoC.FluentConfiguration.Using(IoC.IContainer,IoC.IConfiguration[])')
  - [Using\`\`1(container)](#M-IoC-FluentConfiguration-Using``1-IoC-IContainer- 'IoC.FluentConfiguration.Using``1(IoC.IContainer)')
- [FluentGet](#T-IoC-FluentGet 'IoC.FluentGet')
  - [Get(resolving,type,args)](#M-IoC-FluentGet-Get-IoC-FluentGet-Resolving,System-Type,System-Object[]- 'IoC.FluentGet.Get(IoC.FluentGet.Resolving,System.Type,System.Object[])')
  - [Get(resolving,type)](#M-IoC-FluentGet-Get-IoC-FluentGet-Resolving,System-Type- 'IoC.FluentGet.Get(IoC.FluentGet.Resolving,System.Type)')
  - [Get\`\`1(resolving,args)](#M-IoC-FluentGet-Get``1-IoC-FluentGet-Resolving,System-Object[]- 'IoC.FluentGet.Get``1(IoC.FluentGet.Resolving,System.Object[])')
  - [Get\`\`1(resolving)](#M-IoC-FluentGet-Get``1-IoC-FluentGet-Resolving- 'IoC.FluentGet.Get``1(IoC.FluentGet.Resolving)')
  - [Tag(container,tag)](#M-IoC-FluentGet-Tag-IoC-IContainer,System-Object- 'IoC.FluentGet.Tag(IoC.IContainer,System.Object)')
- [FluentRegister](#T-IoC-FluentRegister 'IoC.FluentRegister')
  - [Register\`\`1(container,lifetime,tags)](#M-IoC-FluentRegister-Register``1-IoC-IContainer,IoC-ILifetime,System-Object[]- 'IoC.FluentRegister.Register``1(IoC.IContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`1(container,factory,lifetime,tags,statements)](#M-IoC-FluentRegister-Register``1-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentRegister.Register``1(IoC.IContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`2(container,lifetime,tags)](#M-IoC-FluentRegister-Register``2-IoC-IContainer,IoC-ILifetime,System-Object[]- 'IoC.FluentRegister.Register``2(IoC.IContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`2(container,factory,lifetime,tags,statements)](#M-IoC-FluentRegister-Register``2-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentRegister.Register``2(IoC.IContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`3(container,lifetime,tags)](#M-IoC-FluentRegister-Register``3-IoC-IContainer,IoC-ILifetime,System-Object[]- 'IoC.FluentRegister.Register``3(IoC.IContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`3(container,factory,lifetime,tags,statements)](#M-IoC-FluentRegister-Register``3-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentRegister.Register``3(IoC.IContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`4(container,lifetime,tags)](#M-IoC-FluentRegister-Register``4-IoC-IContainer,IoC-ILifetime,System-Object[]- 'IoC.FluentRegister.Register``4(IoC.IContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`4(container,factory,lifetime,tags,statements)](#M-IoC-FluentRegister-Register``4-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentRegister.Register``4(IoC.IContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`5(container,lifetime,tags)](#M-IoC-FluentRegister-Register``5-IoC-IContainer,IoC-ILifetime,System-Object[]- 'IoC.FluentRegister.Register``5(IoC.IContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`5(container,factory,lifetime,tags,statements)](#M-IoC-FluentRegister-Register``5-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentRegister.Register``5(IoC.IContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`6(container,lifetime,tags)](#M-IoC-FluentRegister-Register``6-IoC-IContainer,IoC-ILifetime,System-Object[]- 'IoC.FluentRegister.Register``6(IoC.IContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`6(container,factory,lifetime,tags,statements)](#M-IoC-FluentRegister-Register``6-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentRegister.Register``6(IoC.IContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`7(container,lifetime,tags)](#M-IoC-FluentRegister-Register``7-IoC-IContainer,IoC-ILifetime,System-Object[]- 'IoC.FluentRegister.Register``7(IoC.IContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`7(container,factory,lifetime,tags,statements)](#M-IoC-FluentRegister-Register``7-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentRegister.Register``7(IoC.IContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`8(container,lifetime,tags)](#M-IoC-FluentRegister-Register``8-IoC-IContainer,IoC-ILifetime,System-Object[]- 'IoC.FluentRegister.Register``8(IoC.IContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`8(container,factory,lifetime,tags,statements)](#M-IoC-FluentRegister-Register``8-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentRegister.Register``8(IoC.IContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
  - [Register\`\`9(container,lifetime,tags)](#M-IoC-FluentRegister-Register``9-IoC-IContainer,IoC-ILifetime,System-Object[]- 'IoC.FluentRegister.Register``9(IoC.IContainer,IoC.ILifetime,System.Object[])')
  - [Register\`\`9(container,factory,lifetime,tags,statements)](#M-IoC-FluentRegister-Register``9-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'IoC.FluentRegister.Register``9(IoC.IContainer,System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}},IoC.ILifetime,System.Object[],System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[])')
- [FluentResolve](#T-IoC-FluentResolve 'IoC.FluentResolve')
  - [GetResolver\`\`1(type,tag,container)](#M-IoC-FluentResolve-GetResolver``1-IoC-IContainer,System-Type,System-Object- 'IoC.FluentResolve.GetResolver``1(IoC.IContainer,System.Type,System.Object)')
  - [GetResolver\`\`1(type,container)](#M-IoC-FluentResolve-GetResolver``1-IoC-IContainer,System-Type- 'IoC.FluentResolve.GetResolver``1(IoC.IContainer,System.Type)')
  - [Resolve(container,type,args)](#M-IoC-FluentResolve-Resolve-IoC-IContainer,System-Type,System-Object[]- 'IoC.FluentResolve.Resolve(IoC.IContainer,System.Type,System.Object[])')
  - [Resolve(container,type)](#M-IoC-FluentResolve-Resolve-IoC-IContainer,System-Type- 'IoC.FluentResolve.Resolve(IoC.IContainer,System.Type)')
  - [Resolve\`\`1(container)](#M-IoC-FluentResolve-Resolve``1-IoC-Container- 'IoC.FluentResolve.Resolve``1(IoC.Container)')
  - [Resolve\`\`1(container,args)](#M-IoC-FluentResolve-Resolve``1-IoC-Container,System-Object[]- 'IoC.FluentResolve.Resolve``1(IoC.Container,System.Object[])')
  - [Resolve\`\`1(container,args)](#M-IoC-FluentResolve-Resolve``1-IoC-IContainer,System-Object[]- 'IoC.FluentResolve.Resolve``1(IoC.IContainer,System.Object[])')
  - [Resolve\`\`1(container)](#M-IoC-FluentResolve-Resolve``1-IoC-IContainer- 'IoC.FluentResolve.Resolve``1(IoC.IContainer)')
- [FuncFeature](#T-IoC-Features-FuncFeature 'IoC.Features.FuncFeature')
  - [Shared](#F-IoC-Features-FuncFeature-Shared 'IoC.Features.FuncFeature.Shared')
  - [Apply()](#M-IoC-Features-FuncFeature-Apply-IoC-IContainer- 'IoC.Features.FuncFeature.Apply(IoC.IContainer)')
- [GenericTypeArgumentAttribute](#T-IoC-GenericTypeArgumentAttribute 'IoC.GenericTypeArgumentAttribute')
- [IBinding\`1](#T-IoC-IBinding`1 'IoC.IBinding`1')
  - [Container](#P-IoC-IBinding`1-Container 'IoC.IBinding`1.Container')
  - [Lifetime](#P-IoC-IBinding`1-Lifetime 'IoC.IBinding`1.Lifetime')
  - [Tags](#P-IoC-IBinding`1-Tags 'IoC.IBinding`1.Tags')
  - [Types](#P-IoC-IBinding`1-Types 'IoC.IBinding`1.Types')
- [IConfiguration](#T-IoC-IConfiguration 'IoC.IConfiguration')
  - [Apply(container)](#M-IoC-IConfiguration-Apply-IoC-IContainer- 'IoC.IConfiguration.Apply(IoC.IContainer)')
- [IContainer](#T-IoC-IContainer 'IoC.IContainer')
  - [Parent](#P-IoC-IContainer-Parent 'IoC.IContainer.Parent')
  - [TryGetDependency(key,dependency,lifetime)](#M-IoC-IContainer-TryGetDependency-IoC-Key,IoC-IDependency@,IoC-ILifetime@- 'IoC.IContainer.TryGetDependency(IoC.Key,IoC.IDependency@,IoC.ILifetime@)')
  - [TryGetResolver\`\`1(type,tag,resolver,container)](#M-IoC-IContainer-TryGetResolver``1-System-Type,System-Object,IoC-Resolver{``0}@,IoC-IContainer- 'IoC.IContainer.TryGetResolver``1(System.Type,System.Object,IoC.Resolver{``0}@,IoC.IContainer)')
  - [TryGetResolver\`\`1(type,resolver,container)](#M-IoC-IContainer-TryGetResolver``1-System-Type,IoC-Resolver{``0}@,IoC-IContainer- 'IoC.IContainer.TryGetResolver``1(System.Type,IoC.Resolver{``0}@,IoC.IContainer)')
  - [TryRegister(keys,dependency,lifetime,registrationToken)](#M-IoC-IContainer-TryRegister-System-Collections-Generic-IEnumerable{IoC-Key},IoC-IDependency,IoC-ILifetime,System-IDisposable@- 'IoC.IContainer.TryRegister(System.Collections.Generic.IEnumerable{IoC.Key},IoC.IDependency,IoC.ILifetime,System.IDisposable@)')
- [IDependency](#T-IoC-IDependency 'IoC.IDependency')
  - [Expression](#P-IoC-IDependency-Expression 'IoC.IDependency.Expression')
- [IExpressionBuilder\`1](#T-IoC-Extensibility-IExpressionBuilder`1 'IoC.Extensibility.IExpressionBuilder`1')
  - [Build(expression,key,container,context)](#M-IoC-Extensibility-IExpressionBuilder`1-Build-System-Linq-Expressions-Expression,IoC-Key,IoC-IContainer,`0- 'IoC.Extensibility.IExpressionBuilder`1.Build(System.Linq.Expressions.Expression,IoC.Key,IoC.IContainer,`0)')
- [IIssueResolver](#T-IoC-Extensibility-IIssueResolver 'IoC.Extensibility.IIssueResolver')
  - [CannotGetGenericTypeArguments(type)](#M-IoC-Extensibility-IIssueResolver-CannotGetGenericTypeArguments-System-Type- 'IoC.Extensibility.IIssueResolver.CannotGetGenericTypeArguments(System.Type)')
  - [CannotGetResolver\`\`1(container,key)](#M-IoC-Extensibility-IIssueResolver-CannotGetResolver``1-IoC-IContainer,IoC-Key- 'IoC.Extensibility.IIssueResolver.CannotGetResolver``1(IoC.IContainer,IoC.Key)')
  - [CannotParseLifetime(statementText,statementLineNumber,statementPosition,lifetimeName)](#M-IoC-Extensibility-IIssueResolver-CannotParseLifetime-System-String,System-Int32,System-Int32,System-String- 'IoC.Extensibility.IIssueResolver.CannotParseLifetime(System.String,System.Int32,System.Int32,System.String)')
  - [CannotParseTag(statementText,statementLineNumber,statementPosition,tag)](#M-IoC-Extensibility-IIssueResolver-CannotParseTag-System-String,System-Int32,System-Int32,System-String- 'IoC.Extensibility.IIssueResolver.CannotParseTag(System.String,System.Int32,System.Int32,System.String)')
  - [CannotParseType(statementText,statementLineNumber,statementPosition,typeName)](#M-IoC-Extensibility-IIssueResolver-CannotParseType-System-String,System-Int32,System-Int32,System-String- 'IoC.Extensibility.IIssueResolver.CannotParseType(System.String,System.Int32,System.Int32,System.String)')
  - [CannotRegister(container,keys)](#M-IoC-Extensibility-IIssueResolver-CannotRegister-IoC-IContainer,IoC-Key[]- 'IoC.Extensibility.IIssueResolver.CannotRegister(IoC.IContainer,IoC.Key[])')
  - [CannotResolveDependency(container,key)](#M-IoC-Extensibility-IIssueResolver-CannotResolveDependency-IoC-IContainer,IoC-Key- 'IoC.Extensibility.IIssueResolver.CannotResolveDependency(IoC.IContainer,IoC.Key)')
  - [CyclicDependenceDetected(key,reentrancy)](#M-IoC-Extensibility-IIssueResolver-CyclicDependenceDetected-IoC-Key,System-Int32- 'IoC.Extensibility.IIssueResolver.CyclicDependenceDetected(IoC.Key,System.Int32)')
- [ILifetime](#T-IoC-ILifetime 'IoC.ILifetime')
  - [Clone()](#M-IoC-ILifetime-Clone 'IoC.ILifetime.Clone')
  - [GetOrCreate\`\`1(container,args,resolver)](#M-IoC-ILifetime-GetOrCreate``1-IoC-IContainer,System-Object[],IoC-Resolver{``0}- 'IoC.ILifetime.GetOrCreate``1(IoC.IContainer,System.Object[],IoC.Resolver{``0})')
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
  - [Inject\`\`1(container)](#M-IoC-Injections-Inject``1-IoC-IContainer- 'IoC.Injections.Inject``1(IoC.IContainer)')
  - [Inject\`\`1(container,tag)](#M-IoC-Injections-Inject``1-IoC-IContainer,System-Object- 'IoC.Injections.Inject``1(IoC.IContainer,System.Object)')
  - [Inject\`\`1(container,destination,source)](#M-IoC-Injections-Inject``1-IoC-IContainer,``0,``0- 'IoC.Injections.Inject``1(IoC.IContainer,``0,``0)')
- [InstantHandleAttribute](#T-IoC-InstantHandleAttribute 'IoC.InstantHandleAttribute')
- [InvokerParameterNameAttribute](#T-IoC-InvokerParameterNameAttribute 'IoC.InvokerParameterNameAttribute')
- [ItemCanBeNullAttribute](#T-IoC-ItemCanBeNullAttribute 'IoC.ItemCanBeNullAttribute')
- [ItemNotNullAttribute](#T-IoC-ItemNotNullAttribute 'IoC.ItemNotNullAttribute')
- [IValidator](#T-IoC-Extensibility-IValidator 'IoC.Extensibility.IValidator')
  - [Validate(container)](#M-IoC-Extensibility-IValidator-Validate-IoC-IContainer- 'IoC.Extensibility.IValidator.Validate(IoC.IContainer)')
- [Key](#T-IoC-Key 'IoC.Key')
  - [#ctor(type,tag)](#M-IoC-Key-#ctor-System-Type,System-Object- 'IoC.Key.#ctor(System.Type,System.Object)')
  - [AnyTag](#F-IoC-Key-AnyTag 'IoC.Key.AnyTag')
  - [Tag](#F-IoC-Key-Tag 'IoC.Key.Tag')
  - [Type](#F-IoC-Key-Type 'IoC.Key.Type')
  - [ToString()](#M-IoC-Key-ToString 'IoC.Key.ToString')
- [LazyFeature](#T-IoC-Features-LazyFeature 'IoC.Features.LazyFeature')
  - [Shared](#F-IoC-Features-LazyFeature-Shared 'IoC.Features.LazyFeature.Shared')
  - [Apply()](#M-IoC-Features-LazyFeature-Apply-IoC-IContainer- 'IoC.Features.LazyFeature.Apply(IoC.IContainer)')
- [Lifetime](#T-IoC-Lifetime 'IoC.Lifetime')
  - [ContainerSingleton](#F-IoC-Lifetime-ContainerSingleton 'IoC.Lifetime.ContainerSingleton')
  - [ScopeSingleton](#F-IoC-Lifetime-ScopeSingleton 'IoC.Lifetime.ScopeSingleton')
  - [Singleton](#F-IoC-Lifetime-Singleton 'IoC.Lifetime.Singleton')
  - [ThreadSingleton](#F-IoC-Lifetime-ThreadSingleton 'IoC.Lifetime.ThreadSingleton')
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
- [NotifyPropertyChangedInvocatorAttribute](#T-IoC-NotifyPropertyChangedInvocatorAttribute 'IoC.NotifyPropertyChangedInvocatorAttribute')
- [NotNullAttribute](#T-IoC-NotNullAttribute 'IoC.NotNullAttribute')
- [PathReferenceAttribute](#T-IoC-PathReferenceAttribute 'IoC.PathReferenceAttribute')
- [ProvidesContextAttribute](#T-IoC-ProvidesContextAttribute 'IoC.ProvidesContextAttribute')
- [PublicAPIAttribute](#T-IoC-PublicAPIAttribute 'IoC.PublicAPIAttribute')
- [PureAttribute](#T-IoC-PureAttribute 'IoC.PureAttribute')
- [RazorSectionAttribute](#T-IoC-RazorSectionAttribute 'IoC.RazorSectionAttribute')
- [RegexPatternAttribute](#T-IoC-RegexPatternAttribute 'IoC.RegexPatternAttribute')
- [Resolver\`1](#T-IoC-Resolver`1 'IoC.Resolver`1')
- [Resolving](#T-IoC-FluentGet-Resolving 'IoC.FluentGet.Resolving')
  - [Container](#F-IoC-FluentGet-Resolving-Container 'IoC.FluentGet.Resolving.Container')
  - [Tag](#F-IoC-FluentGet-Resolving-Tag 'IoC.FluentGet.Resolving.Tag')
- [Scope](#T-IoC-Scope 'IoC.Scope')
  - [#ctor(scopeKey)](#M-IoC-Scope-#ctor-System-Object- 'IoC.Scope.#ctor(System.Object)')
  - [Current](#P-IoC-Scope-Current 'IoC.Scope.Current')
  - [Dispose()](#M-IoC-Scope-Dispose 'IoC.Scope.Dispose')
- [ScopeSingletonLifetime](#T-IoC-Lifetimes-ScopeSingletonLifetime 'IoC.Lifetimes.ScopeSingletonLifetime')
  - [#ctor()](#M-IoC-Lifetimes-ScopeSingletonLifetime-#ctor-System-Func{IoC-ILifetime}- 'IoC.Lifetimes.ScopeSingletonLifetime.#ctor(System.Func{IoC.ILifetime})')
  - [Clone()](#M-IoC-Lifetimes-ScopeSingletonLifetime-Clone 'IoC.Lifetimes.ScopeSingletonLifetime.Clone')
  - [CreateKey()](#M-IoC-Lifetimes-ScopeSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]- 'IoC.Lifetimes.ScopeSingletonLifetime.CreateKey(IoC.IContainer,System.Object[])')
  - [ToString()](#M-IoC-Lifetimes-ScopeSingletonLifetime-ToString 'IoC.Lifetimes.ScopeSingletonLifetime.ToString')
- [SingletonBasedLifetime\`1](#T-IoC-Lifetimes-SingletonBasedLifetime`1 'IoC.Lifetimes.SingletonBasedLifetime`1')
  - [#ctor()](#M-IoC-Lifetimes-SingletonBasedLifetime`1-#ctor-System-Func{IoC-ILifetime}- 'IoC.Lifetimes.SingletonBasedLifetime`1.#ctor(System.Func{IoC.ILifetime})')
  - [Clone()](#M-IoC-Lifetimes-SingletonBasedLifetime`1-Clone 'IoC.Lifetimes.SingletonBasedLifetime`1.Clone')
  - [CreateKey(container,args)](#M-IoC-Lifetimes-SingletonBasedLifetime`1-CreateKey-IoC-IContainer,System-Object[]- 'IoC.Lifetimes.SingletonBasedLifetime`1.CreateKey(IoC.IContainer,System.Object[])')
  - [Dispose()](#M-IoC-Lifetimes-SingletonBasedLifetime`1-Dispose 'IoC.Lifetimes.SingletonBasedLifetime`1.Dispose')
  - [GetOrCreate\`\`1()](#M-IoC-Lifetimes-SingletonBasedLifetime`1-GetOrCreate``1-IoC-IContainer,System-Object[],IoC-Resolver{``0}- 'IoC.Lifetimes.SingletonBasedLifetime`1.GetOrCreate``1(IoC.IContainer,System.Object[],IoC.Resolver{``0})')
- [SingletonLifetime](#T-IoC-Lifetimes-SingletonLifetime 'IoC.Lifetimes.SingletonLifetime')
  - [Build()](#M-IoC-Lifetimes-SingletonLifetime-Build-System-Linq-Expressions-Expression,IoC-Key,IoC-IContainer,System-Object- 'IoC.Lifetimes.SingletonLifetime.Build(System.Linq.Expressions.Expression,IoC.Key,IoC.IContainer,System.Object)')
  - [Clone()](#M-IoC-Lifetimes-SingletonLifetime-Clone 'IoC.Lifetimes.SingletonLifetime.Clone')
  - [Dispose()](#M-IoC-Lifetimes-SingletonLifetime-Dispose 'IoC.Lifetimes.SingletonLifetime.Dispose')
  - [GetOrCreate\`\`1()](#M-IoC-Lifetimes-SingletonLifetime-GetOrCreate``1-IoC-IContainer,System-Object[],IoC-Resolver{``0}- 'IoC.Lifetimes.SingletonLifetime.GetOrCreate``1(IoC.IContainer,System.Object[],IoC.Resolver{``0})')
  - [ToString()](#M-IoC-Lifetimes-SingletonLifetime-ToString 'IoC.Lifetimes.SingletonLifetime.ToString')
- [SourceTemplateAttribute](#T-IoC-SourceTemplateAttribute 'IoC.SourceTemplateAttribute')
- [StringFormatMethodAttribute](#T-IoC-StringFormatMethodAttribute 'IoC.StringFormatMethodAttribute')
  - [#ctor(formatParameterName)](#M-IoC-StringFormatMethodAttribute-#ctor-System-String- 'IoC.StringFormatMethodAttribute.#ctor(System.String)')
- [TaskFeature](#T-IoC-Features-TaskFeature 'IoC.Features.TaskFeature')
  - [Shared](#F-IoC-Features-TaskFeature-Shared 'IoC.Features.TaskFeature.Shared')
  - [Apply()](#M-IoC-Features-TaskFeature-Apply-IoC-IContainer- 'IoC.Features.TaskFeature.Apply(IoC.IContainer)')
- [TerminatesProgramAttribute](#T-IoC-TerminatesProgramAttribute 'IoC.TerminatesProgramAttribute')
- [ThreadSingletonLifetime](#T-IoC-Lifetimes-ThreadSingletonLifetime 'IoC.Lifetimes.ThreadSingletonLifetime')
  - [#ctor()](#M-IoC-Lifetimes-ThreadSingletonLifetime-#ctor-System-Func{IoC-ILifetime}- 'IoC.Lifetimes.ThreadSingletonLifetime.#ctor(System.Func{IoC.ILifetime})')
  - [Clone()](#M-IoC-Lifetimes-ThreadSingletonLifetime-Clone 'IoC.Lifetimes.ThreadSingletonLifetime.Clone')
  - [CreateKey()](#M-IoC-Lifetimes-ThreadSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]- 'IoC.Lifetimes.ThreadSingletonLifetime.CreateKey(IoC.IContainer,System.Object[])')
  - [ToString()](#M-IoC-Lifetimes-ThreadSingletonLifetime-ToString 'IoC.Lifetimes.ThreadSingletonLifetime.ToString')
- [TT](#T-IoC-TT 'IoC.TT')
- [TT1](#T-IoC-TT1 'IoC.TT1')
- [TT2](#T-IoC-TT2 'IoC.TT2')
- [TT3](#T-IoC-TT3 'IoC.TT3')
- [TT4](#T-IoC-TT4 'IoC.TT4')
- [TT5](#T-IoC-TT5 'IoC.TT5')
- [TT6](#T-IoC-TT6 'IoC.TT6')
- [TT7](#T-IoC-TT7 'IoC.TT7')
- [TT8](#T-IoC-TT8 'IoC.TT8')
- [TupleFeature](#T-IoC-Features-TupleFeature 'IoC.Features.TupleFeature')
  - [Shared](#F-IoC-Features-TupleFeature-Shared 'IoC.Features.TupleFeature.Shared')
  - [Apply()](#M-IoC-Features-TupleFeature-Apply-IoC-IContainer- 'IoC.Features.TupleFeature.Apply(IoC.IContainer)')
- [UsedImplicitlyAttribute](#T-IoC-UsedImplicitlyAttribute 'IoC.UsedImplicitlyAttribute')
- [ValidationResult](#T-IoC-ValidationResult 'IoC.ValidationResult')
  - [ResolvedKey](#F-IoC-ValidationResult-ResolvedKey 'IoC.ValidationResult.ResolvedKey')
  - [UnresolvedKeys](#F-IoC-ValidationResult-UnresolvedKeys 'IoC.ValidationResult.UnresolvedKeys')
  - [IsValid](#P-IoC-ValidationResult-IsValid 'IoC.ValidationResult.IsValid')
- [ValueProviderAttribute](#T-IoC-ValueProviderAttribute 'IoC.ValueProviderAttribute')
- [WellknownContainers](#T-IoC-WellknownContainers 'IoC.WellknownContainers')
  - [Child](#F-IoC-WellknownContainers-Child 'IoC.WellknownContainers.Child')
  - [Current](#F-IoC-WellknownContainers-Current 'IoC.WellknownContainers.Current')
  - [Parent](#F-IoC-WellknownContainers-Parent 'IoC.WellknownContainers.Parent')
- [XamlItemBindingOfItemsControlAttribute](#T-IoC-XamlItemBindingOfItemsControlAttribute 'IoC.XamlItemBindingOfItemsControlAttribute')
- [XamlItemsControlAttribute](#T-IoC-XamlItemsControlAttribute 'IoC.XamlItemsControlAttribute')

<a name='assembly'></a>
# IoC [#](#assembly 'Go To Here') [=](#contents 'Back To Contents')

<a name='T-IoC-AspMvcActionAttribute'></a>
## AspMvcActionAttribute [#](#T-IoC-AspMvcActionAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC action. If applied to a method, the MVC action name is calculated implicitly from the context. Use this attribute for custom wrappers similar to `System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)`.

<a name='T-IoC-AspMvcActionSelectorAttribute'></a>
## AspMvcActionSelectorAttribute [#](#T-IoC-AspMvcActionSelectorAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. When applied to a parameter of an attribute, indicates that this parameter is an MVC action name.

##### Example

```
[ActionName("Foo")]
            public ActionResult Login(string returnUrl) {
              ViewBag.ReturnUrl = Url.Action("Foo"); // OK
              return RedirectToAction("Bar"); // Error: Cannot resolve action
            }
```

<a name='T-IoC-AspMvcAreaAttribute'></a>
## AspMvcAreaAttribute [#](#T-IoC-AspMvcAreaAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC area. Use this attribute for custom wrappers similar to `System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)`.

<a name='T-IoC-AspMvcControllerAttribute'></a>
## AspMvcControllerAttribute [#](#T-IoC-AspMvcControllerAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC controller. If applied to a method, the MVC controller name is calculated implicitly from the context. Use this attribute for custom wrappers similar to `System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String, String)`.

<a name='T-IoC-AspMvcDisplayTemplateAttribute'></a>
## AspMvcDisplayTemplateAttribute [#](#T-IoC-AspMvcDisplayTemplateAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC display template. Use this attribute for custom wrappers similar to `System.Web.Mvc.Html.DisplayExtensions.DisplayForModel(HtmlHelper, String)`.

<a name='T-IoC-AspMvcEditorTemplateAttribute'></a>
## AspMvcEditorTemplateAttribute [#](#T-IoC-AspMvcEditorTemplateAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC editor template. Use this attribute for custom wrappers similar to `System.Web.Mvc.Html.EditorExtensions.EditorForModel(HtmlHelper, String)`.

<a name='T-IoC-AspMvcMasterAttribute'></a>
## AspMvcMasterAttribute [#](#T-IoC-AspMvcMasterAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC Master. Use this attribute for custom wrappers similar to `System.Web.Mvc.Controller.View(String, String)`.

<a name='T-IoC-AspMvcModelTypeAttribute'></a>
## AspMvcModelTypeAttribute [#](#T-IoC-AspMvcModelTypeAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC model type. Use this attribute for custom wrappers similar to `System.Web.Mvc.Controller.View(String, Object)`.

<a name='T-IoC-AspMvcPartialViewAttribute'></a>
## AspMvcPartialViewAttribute [#](#T-IoC-AspMvcPartialViewAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC partial view. If applied to a method, the MVC partial view name is calculated implicitly from the context. Use this attribute for custom wrappers similar to `System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(HtmlHelper, String)`.

<a name='T-IoC-AspMvcSuppressViewErrorAttribute'></a>
## AspMvcSuppressViewErrorAttribute [#](#T-IoC-AspMvcSuppressViewErrorAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Allows disabling inspections for MVC views within a class or a method.

<a name='T-IoC-AspMvcTemplateAttribute'></a>
## AspMvcTemplateAttribute [#](#T-IoC-AspMvcTemplateAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. Indicates that a parameter is an MVC template. Use this attribute for custom wrappers similar to `System.ComponentModel.DataAnnotations.UIHintAttribute(System.String)`.

<a name='T-IoC-AspMvcViewAttribute'></a>
## AspMvcViewAttribute [#](#T-IoC-AspMvcViewAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC view component. If applied to a method, the MVC view name is calculated implicitly from the context. Use this attribute for custom wrappers similar to `System.Web.Mvc.Controller.View(Object)`.

<a name='T-IoC-AspMvcViewComponentAttribute'></a>
## AspMvcViewComponentAttribute [#](#T-IoC-AspMvcViewComponentAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC view component name.

<a name='T-IoC-AspMvcViewComponentViewAttribute'></a>
## AspMvcViewComponentViewAttribute [#](#T-IoC-AspMvcViewComponentViewAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC view component view. If applied to a method, the MVC view component view name is default.

<a name='T-IoC-AssertionConditionAttribute'></a>
## AssertionConditionAttribute [#](#T-IoC-AssertionConditionAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates the condition parameter of the assertion method. The method itself should be marked by [AssertionMethodAttribute](#T-IoC-AssertionMethodAttribute 'IoC.AssertionMethodAttribute') attribute. The mandatory argument of the attribute is the assertion type.

<a name='T-IoC-AssertionConditionType'></a>
## AssertionConditionType [#](#T-IoC-AssertionConditionType 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Specifies assertion type. If the assertion method argument satisfies the condition, then the execution continues. Otherwise, execution is assumed to be halted.

<a name='F-IoC-AssertionConditionType-IS_FALSE'></a>
### IS_FALSE `constants` [#](#F-IoC-AssertionConditionType-IS_FALSE 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Marked parameter should be evaluated to false.

<a name='F-IoC-AssertionConditionType-IS_NOT_NULL'></a>
### IS_NOT_NULL `constants` [#](#F-IoC-AssertionConditionType-IS_NOT_NULL 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Marked parameter should be evaluated to not null value.

<a name='F-IoC-AssertionConditionType-IS_NULL'></a>
### IS_NULL `constants` [#](#F-IoC-AssertionConditionType-IS_NULL 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Marked parameter should be evaluated to null value.

<a name='F-IoC-AssertionConditionType-IS_TRUE'></a>
### IS_TRUE `constants` [#](#F-IoC-AssertionConditionType-IS_TRUE 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Marked parameter should be evaluated to true.

<a name='T-IoC-AssertionMethodAttribute'></a>
## AssertionMethodAttribute [#](#T-IoC-AssertionMethodAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that the marked method is assertion method, i.e. it halts control flow if one of the conditions is satisfied. To set the condition, mark one of the parameters with [AssertionConditionAttribute](#T-IoC-AssertionConditionAttribute 'IoC.AssertionConditionAttribute') attribute.

<a name='T-IoC-BaseTypeRequiredAttribute'></a>
## BaseTypeRequiredAttribute [#](#T-IoC-BaseTypeRequiredAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

When applied to a target attribute, specifies a requirement for any type marked with the target attribute to implement or inherit specific type or types.

##### Example

```
[BaseTypeRequired(typeof(IComponent)] // Specify requirement
            class ComponentAttribute : Attribute { }
            
            [Component] // ComponentAttribute requires implementing IComponent interface
            class MyComponent : IComponent { }
```

<a name='T-IoC-CanBeNullAttribute'></a>
## CanBeNullAttribute [#](#T-IoC-CanBeNullAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that the value of the marked element could be `null` sometimes, so the check for `null` is necessary before its usage.

##### Example

```
[CanBeNull] object Test() =&gt; null;
            
            void UseTest() {
              var p = Test();
              var s = p.ToString(); // Warning: Possible 'System.NullReferenceException'
            }
```

<a name='T-IoC-CannotApplyEqualityOperatorAttribute'></a>
## CannotApplyEqualityOperatorAttribute [#](#T-IoC-CannotApplyEqualityOperatorAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that the value of the marked type (or its derivatives) cannot be compared using '==' or '!=' operators and `Equals()` should be used instead. However, using '==' or '!=' for comparison with `null` is always permitted.

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
## CollectionAccessAttribute [#](#T-IoC-CollectionAccessAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates how method, constructor invocation or property access over collection type affects content of the collection.

<a name='T-IoC-CollectionAccessType'></a>
## CollectionAccessType [#](#T-IoC-CollectionAccessType 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

<a name='F-IoC-CollectionAccessType-ModifyExistingContent'></a>
### ModifyExistingContent `constants` [#](#F-IoC-CollectionAccessType-ModifyExistingContent 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Method can change content of the collection but does not add new elements.

<a name='F-IoC-CollectionAccessType-None'></a>
### None `constants` [#](#F-IoC-CollectionAccessType-None 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Method does not use or modify content of the collection.

<a name='F-IoC-CollectionAccessType-Read'></a>
### Read `constants` [#](#F-IoC-CollectionAccessType-Read 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Method only reads content of the collection but does not modify it.

<a name='F-IoC-CollectionAccessType-UpdatedContent'></a>
### UpdatedContent `constants` [#](#F-IoC-CollectionAccessType-UpdatedContent 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Method can add new elements to the collection.

<a name='T-IoC-Features-CollectionFeature'></a>
## CollectionFeature [#](#T-IoC-Features-CollectionFeature 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Features

##### Summary

Allows to resolve enumeration of all instances related to corresponding bindings.

<a name='F-IoC-Features-CollectionFeature-Shared'></a>
### Shared `constants` [#](#F-IoC-Features-CollectionFeature-Shared 'Go To Here') [=](#contents 'Back To Contents')

<a name='M-IoC-Features-CollectionFeature-Apply-IoC-IContainer-'></a>
### Apply() `method` [#](#M-IoC-Features-CollectionFeature-Apply-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Features-ConfigurationFeature'></a>
## ConfigurationFeature [#](#T-IoC-Features-ConfigurationFeature 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Features

##### Summary

Allows to configure via a text metadata.

<a name='F-IoC-Features-ConfigurationFeature-Shared'></a>
### Shared `constants` [#](#F-IoC-Features-ConfigurationFeature-Shared 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The shared instance.

<a name='M-IoC-Features-ConfigurationFeature-Apply-IoC-IContainer-'></a>
### Apply() `method` [#](#M-IoC-Features-ConfigurationFeature-Apply-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Container'></a>
## Container [#](#T-IoC-Container 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

The IoC container implementation.

<a name='P-IoC-Container-Parent'></a>
### Parent `property` [#](#P-IoC-Container-Parent 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

<a name='M-IoC-Container-Create-System-String-'></a>
### Create(name) `method` [#](#M-IoC-Container-Create-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a root container with default features.

##### Returns

The roor container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The optional name of the container. |

<a name='M-IoC-Container-Create-IoC-IConfiguration[]-'></a>
### Create(configurations) `method` [#](#M-IoC-Container-Create-IoC-IConfiguration[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a root container with specified features.

##### Returns

The roor container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurations | [IoC.IConfiguration[]](#T-IoC-IConfiguration[] 'IoC.IConfiguration[]') | The set of features. |

<a name='M-IoC-Container-Create-System-String,IoC-IConfiguration[]-'></a>
### Create(name,configurations) `method` [#](#M-IoC-Container-Create-System-String,IoC-IConfiguration[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a root container with specified name and features.

##### Returns

The roor container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The optional name of the container. |
| configurations | [IoC.IConfiguration[]](#T-IoC-IConfiguration[] 'IoC.IConfiguration[]') | The set of features. |

<a name='M-IoC-Container-CreateBasic-System-String-'></a>
### CreateBasic(name) `method` [#](#M-IoC-Container-CreateBasic-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a root container with basic features.

##### Returns

The roor container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The optional name of the container. |

<a name='M-IoC-Container-Dispose'></a>
### Dispose() `method` [#](#M-IoC-Container-Dispose 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-GetEnumerator'></a>
### GetEnumerator() `method` [#](#M-IoC-Container-GetEnumerator 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-Subscribe-System-IObserver{IoC-ContainerEvent}-'></a>
### Subscribe() `method` [#](#M-IoC-Container-Subscribe-System-IObserver{IoC-ContainerEvent}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-ToString'></a>
### ToString() `method` [#](#M-IoC-Container-ToString 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-TryGetDependency-IoC-Key,IoC-IDependency@,IoC-ILifetime@-'></a>
### TryGetDependency() `method` [#](#M-IoC-Container-TryGetDependency-IoC-Key,IoC-IDependency@,IoC-ILifetime@- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-TryGetResolver``1-System-Type,System-Object,IoC-Resolver{``0}@,IoC-IContainer-'></a>
### TryGetResolver\`\`1() `method` [#](#M-IoC-Container-TryGetResolver``1-System-Type,System-Object,IoC-Resolver{``0}@,IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-TryGetResolver``1-System-Type,IoC-Resolver{``0}@,IoC-IContainer-'></a>
### TryGetResolver\`\`1() `method` [#](#M-IoC-Container-TryGetResolver``1-System-Type,IoC-Resolver{``0}@,IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Container-TryRegister-System-Collections-Generic-IEnumerable{IoC-Key},IoC-IDependency,IoC-ILifetime,System-IDisposable@-'></a>
### TryRegister() `method` [#](#M-IoC-Container-TryRegister-System-Collections-Generic-IEnumerable{IoC-Key},IoC-IDependency,IoC-ILifetime,System-IDisposable@- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-ContainerEvent'></a>
## ContainerEvent [#](#T-IoC-ContainerEvent 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Provides information about changes in the container.

<a name='F-IoC-ContainerEvent-Container'></a>
### Container `constants` [#](#F-IoC-ContainerEvent-Container 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The target container.

<a name='F-IoC-ContainerEvent-EventTypeType'></a>
### EventTypeType `constants` [#](#F-IoC-ContainerEvent-EventTypeType 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The type of event.

<a name='F-IoC-ContainerEvent-Key'></a>
### Key `constants` [#](#F-IoC-ContainerEvent-Key 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The chenged binding key.

<a name='T-IoC-Lifetimes-ContainerSingletonLifetime'></a>
## ContainerSingletonLifetime [#](#T-IoC-Lifetimes-ContainerSingletonLifetime 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Lifetimes

##### Summary

Represents singleton per container lifetime.

<a name='M-IoC-Lifetimes-ContainerSingletonLifetime-#ctor-System-Func{IoC-ILifetime}-'></a>
### #ctor() `constructor` [#](#M-IoC-Lifetimes-ContainerSingletonLifetime-#ctor-System-Func{IoC-ILifetime}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-IoC-Lifetimes-ContainerSingletonLifetime-Clone'></a>
### Clone() `method` [#](#M-IoC-Lifetimes-ContainerSingletonLifetime-Clone 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ContainerSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]-'></a>
### CreateKey() `method` [#](#M-IoC-Lifetimes-ContainerSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ContainerSingletonLifetime-ToString'></a>
### ToString() `method` [#](#M-IoC-Lifetimes-ContainerSingletonLifetime-ToString 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Context'></a>
## Context [#](#T-IoC-Context 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the resolving context.

<a name='F-IoC-Context-Args'></a>
### Args `constants` [#](#F-IoC-Context-Args 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The optional resolving arguments.

<a name='F-IoC-Context-Container'></a>
### Container `constants` [#](#F-IoC-Context-Container 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The resolving container.

<a name='F-IoC-Context-Key'></a>
### Key `constants` [#](#F-IoC-Context-Key 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The resolving key.

<a name='T-IoC-Context`1'></a>
## Context\`1 [#](#T-IoC-Context`1 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represent the resolving context with an instance.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='F-IoC-Context`1-It'></a>
### It `constants` [#](#F-IoC-Context`1-It 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The resolved instance.

<a name='T-IoC-ContractAnnotationAttribute'></a>
## ContractAnnotationAttribute [#](#T-IoC-ContractAnnotationAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Describes dependency between method input and output.

<a name='T-IoC-Features-CoreFeature'></a>
## CoreFeature [#](#T-IoC-Features-CoreFeature 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Features

##### Summary

Adds the set of core features like lifetimes and default containers.

<a name='F-IoC-Features-CoreFeature-Shared'></a>
### Shared `constants` [#](#F-IoC-Features-CoreFeature-Shared 'Go To Here') [=](#contents 'Back To Contents')

<a name='M-IoC-Features-CoreFeature-Apply-IoC-IContainer-'></a>
### Apply() `method` [#](#M-IoC-Features-CoreFeature-Apply-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-ContainerEvent-EventType'></a>
## EventType [#](#T-IoC-ContainerEvent-EventType 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.ContainerEvent

##### Summary

The types of event.

<a name='F-IoC-ContainerEvent-EventType-Registration'></a>
### Registration `constants` [#](#F-IoC-ContainerEvent-EventType-Registration 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

A new registration was created.

<a name='F-IoC-ContainerEvent-EventType-Unregistration'></a>
### Unregistration `constants` [#](#F-IoC-ContainerEvent-EventType-Unregistration 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The registration was removed.

<a name='T-IoC-Features-ExpressionCompilerFeature'></a>
## ExpressionCompilerFeature [#](#T-IoC-Features-ExpressionCompilerFeature 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Features

##### Summary

Allows to compile fast delegates.

<a name='F-IoC-Features-ExpressionCompilerFeature-Shared'></a>
### Shared `constants` [#](#F-IoC-Features-ExpressionCompilerFeature-Shared 'Go To Here') [=](#contents 'Back To Contents')

<a name='M-IoC-Features-ExpressionCompilerFeature-Apply-IoC-IContainer-'></a>
### Apply() `method` [#](#M-IoC-Features-ExpressionCompilerFeature-Apply-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Features-Feature'></a>
## Feature [#](#T-IoC-Features-Feature 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Features

##### Summary

Provides defaults for features.

<a name='F-IoC-Features-Feature-BasicSet'></a>
### BasicSet `constants` [#](#F-IoC-Features-Feature-BasicSet 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The enumeration of default features.

<a name='F-IoC-Features-Feature-DefaultSet'></a>
### DefaultSet `constants` [#](#F-IoC-Features-Feature-DefaultSet 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The enumeration of default features.

<a name='T-IoC-Fluent'></a>
## Fluent [#](#T-IoC-Fluent 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Extension method for IoC container.

<a name='M-IoC-Fluent-CreateChild-IoC-IContainer,System-String-'></a>
### CreateChild(parent,name) `method` [#](#M-IoC-Fluent-CreateChild-IoC-IContainer,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates child container.

##### Returns

The child container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parent | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The parent container. |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of child container. |

<a name='M-IoC-Fluent-Validate-IoC-IContainer-'></a>
### Validate(container) `method` [#](#M-IoC-Fluent-Validate-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Validates the target container.

##### Returns

The validation result.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

<a name='T-IoC-FluentBind'></a>
## FluentBind [#](#T-IoC-FluentBind 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents extensions to add bindings to a container.

<a name='M-IoC-FluentBind-AnyTag``1-IoC-IBinding{``0}-'></a>
### AnyTag\`\`1(binding) `method` [#](#M-IoC-FluentBind-AnyTag``1-IoC-IBinding{``0}- 'Go To Here') [=](#contents 'Back To Contents')

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
### As\`\`1(binding,lifetime) `method` [#](#M-IoC-FluentBind-As``1-IoC-IBinding{``0},IoC-Lifetime- 'Go To Here') [=](#contents 'Back To Contents')

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

<a name='M-IoC-FluentBind-Bind-IoC-IContainer,System-Type[]-'></a>
### Bind(container,types) `method` [#](#M-IoC-FluentBind-Bind-IoC-IContainer,System-Type[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Binds the type(s).

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| types | [System.Type[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type[] 'System.Type[]') |  |

<a name='M-IoC-FluentBind-Bind``1-IoC-IContainer-'></a>
### Bind\`\`1(container) `method` [#](#M-IoC-FluentBind-Bind``1-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Binds the type.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The contract type. |

<a name='M-IoC-FluentBind-Bind``2-IoC-IContainer-'></a>
### Bind\`\`2(container) `method` [#](#M-IoC-FluentBind-Bind``2-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type. |

<a name='M-IoC-FluentBind-Bind``3-IoC-IContainer-'></a>
### Bind\`\`3(container) `method` [#](#M-IoC-FluentBind-Bind``3-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |

<a name='M-IoC-FluentBind-Bind``4-IoC-IContainer-'></a>
### Bind\`\`4(container) `method` [#](#M-IoC-FluentBind-Bind``4-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |

<a name='M-IoC-FluentBind-Bind``5-IoC-IContainer-'></a>
### Bind\`\`5(container) `method` [#](#M-IoC-FluentBind-Bind``5-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |

<a name='M-IoC-FluentBind-Bind``6-IoC-IContainer-'></a>
### Bind\`\`6(container) `method` [#](#M-IoC-FluentBind-Bind``6-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |
| T1 | The contract type #1. |
| T2 | The contract type #2. |
| T3 | The contract type #3. |
| T4 | The contract type #4. |
| T5 | The contract type #5. |

<a name='M-IoC-FluentBind-Bind``7-IoC-IContainer-'></a>
### Bind\`\`7(container) `method` [#](#M-IoC-FluentBind-Bind``7-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

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

<a name='M-IoC-FluentBind-Bind``8-IoC-IContainer-'></a>
### Bind\`\`8(container) `method` [#](#M-IoC-FluentBind-Bind``8-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Binds multiple types.

##### Returns

The binding token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

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

<a name='M-IoC-FluentBind-Lifetime``1-IoC-IBinding{``0},IoC-ILifetime-'></a>
### Lifetime\`\`1(binding,lifetime) `method` [#](#M-IoC-FluentBind-Lifetime``1-IoC-IBinding{``0},IoC-ILifetime- 'Go To Here') [=](#contents 'Back To Contents')

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
### Tag\`\`1(binding,tagValue) `method` [#](#M-IoC-FluentBind-Tag``1-IoC-IBinding{``0},System-Object- 'Go To Here') [=](#contents 'Back To Contents')

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

<a name='M-IoC-FluentBind-To-IoC-IBinding{System-Object},System-Type,System-Predicate{System-Reflection-ConstructorInfo}-'></a>
### To(binding,type,constructorFilter) `method` [#](#M-IoC-FluentBind-To-IoC-IBinding{System-Object},System-Type,System-Predicate{System-Reflection-ConstructorInfo}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates full auto-wiring.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding{System.Object}](#T-IoC-IBinding{System-Object} 'IoC.IBinding{System.Object}') | The binding token. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The instance type. |
| constructorFilter | [System.Predicate{System.Reflection.ConstructorInfo}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Predicate 'System.Predicate{System.Reflection.ConstructorInfo}') | The constructor's filter. |

<a name='M-IoC-FluentBind-To``1-IoC-IBinding{``0},System-Predicate{System-Reflection-ConstructorInfo}-'></a>
### To\`\`1(binding,constructorFilter) `method` [#](#M-IoC-FluentBind-To``1-IoC-IBinding{``0},System-Predicate{System-Reflection-ConstructorInfo}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates full auto-wiring.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| binding | [IoC.IBinding{\`\`0}](#T-IoC-IBinding{``0} 'IoC.IBinding{``0}') | The binding token. |
| constructorFilter | [System.Predicate{System.Reflection.ConstructorInfo}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Predicate 'System.Predicate{System.Reflection.ConstructorInfo}') | The constructor's filter. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentBind-To``1-IoC-IBinding{``0},System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### To\`\`1(binding,factory,statements) `method` [#](#M-IoC-FluentBind-To``1-IoC-IBinding{``0},System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates manual auto-wiring.

##### Returns

The registration token.

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

<a name='M-IoC-FluentBind-ToSelf-System-IDisposable-'></a>
### ToSelf(registrationToken) `method` [#](#M-IoC-FluentBind-ToSelf-System-IDisposable- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Puts the registration token to the target contaier to manage it.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| registrationToken | [System.IDisposable](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IDisposable 'System.IDisposable') |  |

<a name='T-IoC-FluentConfiguration'></a>
## FluentConfiguration [#](#T-IoC-FluentConfiguration 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents extensons to configure a container.

<a name='M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-String[]-'></a>
### Apply(container,configurationText) `method` [#](#M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-String[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Applies text configurations for the target container.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| configurationText | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The text configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-IO-Stream[]-'></a>
### Apply(container,configurationStreams) `method` [#](#M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-IO-Stream[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Applies text configurations from streams for the target container.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| configurationStreams | [System.IO.Stream[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream[] 'System.IO.Stream[]') | The set of streams with text configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-IO-TextReader[]-'></a>
### Apply(container,configurationReaders) `method` [#](#M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-IO-TextReader[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Applies text configurations from text readers for the target container.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| configurationReaders | [System.IO.TextReader[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.TextReader[] 'System.IO.TextReader[]') | The set of text readers with text configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-Collections-Generic-IEnumerable{IoC-IConfiguration}-'></a>
### Apply(container,configurations) `method` [#](#M-IoC-FluentConfiguration-Apply-IoC-IContainer,System-Collections-Generic-IEnumerable{IoC-IConfiguration}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Applies configurations for the target container.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| configurations | [System.Collections.Generic.IEnumerable{IoC.IConfiguration}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{IoC.IConfiguration}') | The configurations. |

<a name='M-IoC-FluentConfiguration-Apply-IoC-IContainer,IoC-IConfiguration[]-'></a>
### Apply(container,configurations) `method` [#](#M-IoC-FluentConfiguration-Apply-IoC-IContainer,IoC-IConfiguration[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Applies configurations for the target container.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| configurations | [IoC.IConfiguration[]](#T-IoC-IConfiguration[] 'IoC.IConfiguration[]') | The configurations. |

<a name='M-IoC-FluentConfiguration-Using-IoC-IContainer,System-String[]-'></a>
### Using(container,configurationText) `method` [#](#M-IoC-FluentConfiguration-Using-IoC-IContainer,System-String[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Applies text configurations for the target container.

##### Returns

The target container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| configurationText | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The text configurations. |

<a name='M-IoC-FluentConfiguration-Using-IoC-IContainer,System-IO-Stream[]-'></a>
### Using(container,configurationStreams) `method` [#](#M-IoC-FluentConfiguration-Using-IoC-IContainer,System-IO-Stream[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Applies text configurations from streams for the target container.

##### Returns

The target container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| configurationStreams | [System.IO.Stream[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream[] 'System.IO.Stream[]') | The set of streams with text configurations. |

<a name='M-IoC-FluentConfiguration-Using-IoC-IContainer,System-IO-TextReader[]-'></a>
### Using(container,configurationReaders) `method` [#](#M-IoC-FluentConfiguration-Using-IoC-IContainer,System-IO-TextReader[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Applies text configurations from text readers for the target container.

##### Returns

The target container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| configurationReaders | [System.IO.TextReader[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.TextReader[] 'System.IO.TextReader[]') | The set of text readers with text configurations. |

<a name='M-IoC-FluentConfiguration-Using-IoC-IContainer,IoC-IConfiguration[]-'></a>
### Using(container,configurations) `method` [#](#M-IoC-FluentConfiguration-Using-IoC-IContainer,IoC-IConfiguration[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Applies configurations for the target container.

##### Returns

The target container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| configurations | [IoC.IConfiguration[]](#T-IoC-IConfiguration[] 'IoC.IConfiguration[]') | The configurations. |

<a name='M-IoC-FluentConfiguration-Using``1-IoC-IContainer-'></a>
### Using\`\`1(container) `method` [#](#M-IoC-FluentConfiguration-Using``1-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Applies configuration for the target container.

##### Returns

The target container.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of configuration. |

<a name='T-IoC-FluentGet'></a>
## FluentGet [#](#T-IoC-FluentGet 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents extensions to get an instance from a continer.

<a name='M-IoC-FluentGet-Get-IoC-FluentGet-Resolving,System-Type,System-Object[]-'></a>
### Get(resolving,type,args) `method` [#](#M-IoC-FluentGet-Get-IoC-FluentGet-Resolving,System-Type,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resolving | [IoC.FluentGet.Resolving](#T-IoC-FluentGet-Resolving 'IoC.FluentGet.Resolving') |  |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The instance type. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

<a name='M-IoC-FluentGet-Get-IoC-FluentGet-Resolving,System-Type-'></a>
### Get(resolving,type) `method` [#](#M-IoC-FluentGet-Get-IoC-FluentGet-Resolving,System-Type- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resolving | [IoC.FluentGet.Resolving](#T-IoC-FluentGet-Resolving 'IoC.FluentGet.Resolving') |  |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The instance type. |

<a name='M-IoC-FluentGet-Get``1-IoC-FluentGet-Resolving,System-Object[]-'></a>
### Get\`\`1(resolving,args) `method` [#](#M-IoC-FluentGet-Get``1-IoC-FluentGet-Resolving,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resolving | [IoC.FluentGet.Resolving](#T-IoC-FluentGet-Resolving 'IoC.FluentGet.Resolving') |  |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentGet-Get``1-IoC-FluentGet-Resolving-'></a>
### Get\`\`1(resolving) `method` [#](#M-IoC-FluentGet-Get``1-IoC-FluentGet-Resolving- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| resolving | [IoC.FluentGet.Resolving](#T-IoC-FluentGet-Resolving 'IoC.FluentGet.Resolving') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-FluentGet-Tag-IoC-IContainer,System-Object-'></a>
### Tag(container,tag) `method` [#](#M-IoC-FluentGet-Tag-IoC-IContainer,System-Object- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Specifies the tag of the instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| tag | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The tag value. |

<a name='T-IoC-FluentRegister'></a>
## FluentRegister [#](#T-IoC-FluentRegister 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents extensions for registration in a container.

<a name='M-IoC-FluentRegister-Register``1-IoC-IContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`1(container,lifetime,tags) `method` [#](#M-IoC-FluentRegister-Register``1-IoC-IContainer,IoC-ILifetime,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowring type. |

<a name='M-IoC-FluentRegister-Register``1-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`1(container,factory,lifetime,tags,statements) `method` [#](#M-IoC-FluentRegister-Register``1-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |

<a name='M-IoC-FluentRegister-Register``2-IoC-IContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`2(container,lifetime,tags) `method` [#](#M-IoC-FluentRegister-Register``2-IoC-IContainer,IoC-ILifetime,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowring type. |
| T1 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``2-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`2(container,factory,lifetime,tags,statements) `method` [#](#M-IoC-FluentRegister-Register``2-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``3-IoC-IContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`3(container,lifetime,tags) `method` [#](#M-IoC-FluentRegister-Register``3-IoC-IContainer,IoC-ILifetime,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowring type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``3-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`3(container,factory,lifetime,tags,statements) `method` [#](#M-IoC-FluentRegister-Register``3-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``4-IoC-IContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`4(container,lifetime,tags) `method` [#](#M-IoC-FluentRegister-Register``4-IoC-IContainer,IoC-ILifetime,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowring type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``4-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`4(container,factory,lifetime,tags,statements) `method` [#](#M-IoC-FluentRegister-Register``4-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``5-IoC-IContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`5(container,lifetime,tags) `method` [#](#M-IoC-FluentRegister-Register``5-IoC-IContainer,IoC-ILifetime,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowring type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |
| T4 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``5-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`5(container,factory,lifetime,tags,statements) `method` [#](#M-IoC-FluentRegister-Register``5-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |
| T4 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``6-IoC-IContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`6(container,lifetime,tags) `method` [#](#M-IoC-FluentRegister-Register``6-IoC-IContainer,IoC-ILifetime,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowring type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |
| T4 | The additional contract type. |
| T5 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``6-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`6(container,factory,lifetime,tags,statements) `method` [#](#M-IoC-FluentRegister-Register``6-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |
| T4 | The additional contract type. |
| T5 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``7-IoC-IContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`7(container,lifetime,tags) `method` [#](#M-IoC-FluentRegister-Register``7-IoC-IContainer,IoC-ILifetime,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowring type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |
| T4 | The additional contract type. |
| T5 | The additional contract type. |
| T6 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``7-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`7(container,factory,lifetime,tags,statements) `method` [#](#M-IoC-FluentRegister-Register``7-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |
| T4 | The additional contract type. |
| T5 | The additional contract type. |
| T6 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``8-IoC-IContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`8(container,lifetime,tags) `method` [#](#M-IoC-FluentRegister-Register``8-IoC-IContainer,IoC-ILifetime,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowring type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |
| T4 | The additional contract type. |
| T5 | The additional contract type. |
| T6 | The additional contract type. |
| T7 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``8-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`8(container,factory,lifetime,tags,statements) `method` [#](#M-IoC-FluentRegister-Register``8-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |
| T4 | The additional contract type. |
| T5 | The additional contract type. |
| T6 | The additional contract type. |
| T7 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``9-IoC-IContainer,IoC-ILifetime,System-Object[]-'></a>
### Register\`\`9(container,lifetime,tags) `method` [#](#M-IoC-FluentRegister-Register``9-IoC-IContainer,IoC-ILifetime,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The autowring type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |
| T4 | The additional contract type. |
| T5 | The additional contract type. |
| T6 | The additional contract type. |
| T7 | The additional contract type. |
| T8 | The additional contract type. |

<a name='M-IoC-FluentRegister-Register``9-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]-'></a>
### Register\`\`9(container,factory,lifetime,tags,statements) `method` [#](#M-IoC-FluentRegister-Register``9-IoC-IContainer,System-Linq-Expressions-Expression{System-Func{IoC-Context,``0}},IoC-ILifetime,System-Object[],System-Linq-Expressions-Expression{System-Action{IoC-Context{``0}}}[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers a binding.

##### Returns

The registration token.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| factory | [System.Linq.Expressions.Expression{System.Func{IoC.Context,\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{IoC.Context,``0}}') | The expression to create an instance. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The target lifetime. |
| tags | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The tags. |
| statements | [System.Linq.Expressions.Expression{System.Action{IoC.Context{\`\`0}}}[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Action{IoC.Context{``0}}}[]') | The set of expressions to initialize an instance. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The base type. |
| T1 | The additional contract type. |
| T2 | The additional contract type. |
| T3 | The additional contract type. |
| T4 | The additional contract type. |
| T5 | The additional contract type. |
| T6 | The additional contract type. |
| T7 | The additional contract type. |
| T8 | The additional contract type. |

<a name='T-IoC-FluentResolve'></a>
## FluentResolve [#](#T-IoC-FluentResolve 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents extensions to resolve from a container.

<a name='M-IoC-FluentResolve-GetResolver``1-IoC-IContainer,System-Type,System-Object-'></a>
### GetResolver\`\`1(type,tag,container) `method` [#](#M-IoC-FluentResolve-GetResolver``1-IoC-IContainer,System-Type,System-Object- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the resolver.

##### Returns

The resolver.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target type. |
| tag | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The tag of binding. |
| container | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The resolver type. |

<a name='M-IoC-FluentResolve-GetResolver``1-IoC-IContainer,System-Type-'></a>
### GetResolver\`\`1(type,container) `method` [#](#M-IoC-FluentResolve-GetResolver``1-IoC-IContainer,System-Type- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the resolver.

##### Returns

The resolver.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target type. |
| container | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The resolver type. |

<a name='M-IoC-FluentResolve-Resolve-IoC-IContainer,System-Type,System-Object[]-'></a>
### Resolve(container,type,args) `method` [#](#M-IoC-FluentResolve-Resolve-IoC-IContainer,System-Type,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The instance type. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The optional arguments. |

<a name='M-IoC-FluentResolve-Resolve-IoC-IContainer,System-Type-'></a>
### Resolve(container,type) `method` [#](#M-IoC-FluentResolve-Resolve-IoC-IContainer,System-Type- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The instance type. |

<a name='M-IoC-FluentResolve-Resolve``1-IoC-Container-'></a>
### Resolve\`\`1(container) `method` [#](#M-IoC-FluentResolve-Resolve``1-IoC-Container- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an instance.

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

<a name='M-IoC-FluentResolve-Resolve``1-IoC-Container,System-Object[]-'></a>
### Resolve\`\`1(container,args) `method` [#](#M-IoC-FluentResolve-Resolve``1-IoC-Container,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an instance.

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

<a name='M-IoC-FluentResolve-Resolve``1-IoC-IContainer,System-Object[]-'></a>
### Resolve\`\`1(container,args) `method` [#](#M-IoC-FluentResolve-Resolve``1-IoC-IContainer,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an instance.

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

<a name='M-IoC-FluentResolve-Resolve``1-IoC-IContainer-'></a>
### Resolve\`\`1(container) `method` [#](#M-IoC-FluentResolve-Resolve``1-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='T-IoC-Features-FuncFeature'></a>
## FuncFeature [#](#T-IoC-Features-FuncFeature 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Features

##### Summary

Allows to resolve Funcs.

<a name='F-IoC-Features-FuncFeature-Shared'></a>
### Shared `constants` [#](#F-IoC-Features-FuncFeature-Shared 'Go To Here') [=](#contents 'Back To Contents')

<a name='M-IoC-Features-FuncFeature-Apply-IoC-IContainer-'></a>
### Apply() `method` [#](#M-IoC-Features-FuncFeature-Apply-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-GenericTypeArgumentAttribute'></a>
## GenericTypeArgumentAttribute [#](#T-IoC-GenericTypeArgumentAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the generic type parameter marker.

<a name='T-IoC-IBinding`1'></a>
## IBinding\`1 [#](#T-IoC-IBinding`1 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

The container's binding.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='P-IoC-IBinding`1-Container'></a>
### Container `property` [#](#P-IoC-IBinding`1-Container 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The target container.

<a name='P-IoC-IBinding`1-Lifetime'></a>
### Lifetime `property` [#](#P-IoC-IBinding`1-Lifetime 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The specified lifetime instance or null.

<a name='P-IoC-IBinding`1-Tags'></a>
### Tags `property` [#](#P-IoC-IBinding`1-Tags 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The tags to mark the binding.

<a name='P-IoC-IBinding`1-Types'></a>
### Types `property` [#](#P-IoC-IBinding`1-Types 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The type to bind.

<a name='T-IoC-IConfiguration'></a>
## IConfiguration [#](#T-IoC-IConfiguration 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

The container's configuration.

<a name='M-IoC-IConfiguration-Apply-IoC-IContainer-'></a>
### Apply(container) `method` [#](#M-IoC-IConfiguration-Apply-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Apply the configuration for the target container.

##### Returns

The enumeration of registration tokens.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

<a name='T-IoC-IContainer'></a>
## IContainer [#](#T-IoC-IContainer 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

The IoC container.

<a name='P-IoC-IContainer-Parent'></a>
### Parent `property` [#](#P-IoC-IContainer-Parent 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The parent container.

<a name='M-IoC-IContainer-TryGetDependency-IoC-Key,IoC-IDependency@,IoC-ILifetime@-'></a>
### TryGetDependency(key,dependency,lifetime) `method` [#](#M-IoC-IContainer-TryGetDependency-IoC-Key,IoC-IDependency@,IoC-ILifetime@- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers the dependency and lifetime.

##### Returns

True if successful.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [IoC.Key](#T-IoC-Key 'IoC.Key') | The key. |
| dependency | [IoC.IDependency@](#T-IoC-IDependency@ 'IoC.IDependency@') | The dependency. |
| lifetime | [IoC.ILifetime@](#T-IoC-ILifetime@ 'IoC.ILifetime@') | The lifetime. |

<a name='M-IoC-IContainer-TryGetResolver``1-System-Type,System-Object,IoC-Resolver{``0}@,IoC-IContainer-'></a>
### TryGetResolver\`\`1(type,tag,resolver,container) `method` [#](#M-IoC-IContainer-TryGetResolver``1-System-Type,System-Object,IoC-Resolver{``0}@,IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the resolver.

##### Returns

True if successful.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The target type. |
| tag | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The tag of binding. |
| resolver | [IoC.Resolver{\`\`0}@](#T-IoC-Resolver{``0}@ 'IoC.Resolver{``0}@') | The resolver. |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The resolver type. |

<a name='M-IoC-IContainer-TryGetResolver``1-System-Type,IoC-Resolver{``0}@,IoC-IContainer-'></a>
### TryGetResolver\`\`1(type,resolver,container) `method` [#](#M-IoC-IContainer-TryGetResolver``1-System-Type,IoC-Resolver{``0}@,IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the resolver.

##### Returns

True if successful.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The target type. |
| resolver | [IoC.Resolver{\`\`0}@](#T-IoC-Resolver{``0}@ 'IoC.Resolver{``0}@') | The resolver. |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The resolver type. |

<a name='M-IoC-IContainer-TryRegister-System-Collections-Generic-IEnumerable{IoC-Key},IoC-IDependency,IoC-ILifetime,System-IDisposable@-'></a>
### TryRegister(keys,dependency,lifetime,registrationToken) `method` [#](#M-IoC-IContainer-TryRegister-System-Collections-Generic-IEnumerable{IoC-Key},IoC-IDependency,IoC-ILifetime,System-IDisposable@- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Registers the binding to the target container.

##### Returns

True if successful.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keys | [System.Collections.Generic.IEnumerable{IoC.Key}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{IoC.Key}') | The set of keys. |
| dependency | [IoC.IDependency](#T-IoC-IDependency 'IoC.IDependency') | The dependency. |
| lifetime | [IoC.ILifetime](#T-IoC-ILifetime 'IoC.ILifetime') | The lifetime. |
| registrationToken | [System.IDisposable@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IDisposable@ 'System.IDisposable@') | The registration token. |

<a name='T-IoC-IDependency'></a>
## IDependency [#](#T-IoC-IDependency 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents a IoC dependency.

<a name='P-IoC-IDependency-Expression'></a>
### Expression `property` [#](#P-IoC-IDependency-Expression 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The expression for dependency which is used to create a build graph.

<a name='T-IoC-Extensibility-IExpressionBuilder`1'></a>
## IExpressionBuilder\`1 [#](#T-IoC-Extensibility-IExpressionBuilder`1 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Extensibility

##### Summary

Allows to build expresion for lifetimes.

<a name='M-IoC-Extensibility-IExpressionBuilder`1-Build-System-Linq-Expressions-Expression,IoC-Key,IoC-IContainer,`0-'></a>
### Build(expression,key,container,context) `method` [#](#M-IoC-Extensibility-IExpressionBuilder`1-Build-System-Linq-Expressions-Expression,IoC-Key,IoC-IContainer,`0- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Builds the expression.

##### Returns

The new expression.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expression | [System.Linq.Expressions.Expression](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression') | The base expression to get an instance. |
| key | [IoC.Key](#T-IoC-Key 'IoC.Key') | The key. |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| context | [\`0](#T-`0 '`0') | The expression build context. |

<a name='T-IoC-Extensibility-IIssueResolver'></a>
## IIssueResolver [#](#T-IoC-Extensibility-IIssueResolver 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Extensibility

##### Summary

Allows to specify behaviour for cases with issue.

<a name='M-IoC-Extensibility-IIssueResolver-CannotGetGenericTypeArguments-System-Type-'></a>
### CannotGetGenericTypeArguments(type) `method` [#](#M-IoC-Extensibility-IIssueResolver-CannotGetGenericTypeArguments-System-Type- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles the scenario when cannot extract generic type arguments.

##### Returns

The extracted generic type arguments.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The instance type. |

<a name='M-IoC-Extensibility-IIssueResolver-CannotGetResolver``1-IoC-IContainer,IoC-Key-'></a>
### CannotGetResolver\`\`1(container,key) `method` [#](#M-IoC-Extensibility-IIssueResolver-CannotGetResolver``1-IoC-IContainer,IoC-Key- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles the scenario when cannot get a resolver.

##### Returns

The resolver.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| key | [IoC.Key](#T-IoC-Key 'IoC.Key') | The resolving key. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The instance type. |

<a name='M-IoC-Extensibility-IIssueResolver-CannotParseLifetime-System-String,System-Int32,System-Int32,System-String-'></a>
### CannotParseLifetime(statementText,statementLineNumber,statementPosition,lifetimeName) `method` [#](#M-IoC-Extensibility-IIssueResolver-CannotParseLifetime-System-String,System-Int32,System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles the scenario when cannot parse a lifetime from a text.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| statementText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The statement containing a lifetime metadata. |
| statementLineNumber | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The line number in the source data. |
| statementPosition | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The position at the line of the source data. |
| lifetimeName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text with a lifetime metadata. |

<a name='M-IoC-Extensibility-IIssueResolver-CannotParseTag-System-String,System-Int32,System-Int32,System-String-'></a>
### CannotParseTag(statementText,statementLineNumber,statementPosition,tag) `method` [#](#M-IoC-Extensibility-IIssueResolver-CannotParseTag-System-String,System-Int32,System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles the scenario when cannot parse a tag from a text.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| statementText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The statement containing a tag metadata. |
| statementLineNumber | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The line number in the source data. |
| statementPosition | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The position at the line of the source data. |
| tag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text with a tag metadata. |

<a name='M-IoC-Extensibility-IIssueResolver-CannotParseType-System-String,System-Int32,System-Int32,System-String-'></a>
### CannotParseType(statementText,statementLineNumber,statementPosition,typeName) `method` [#](#M-IoC-Extensibility-IIssueResolver-CannotParseType-System-String,System-Int32,System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles the scenario when cannot parse a type from a text.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| statementText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The statement containing a type metadata. |
| statementLineNumber | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The line number in the source data. |
| statementPosition | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The position at the line of the source data. |
| typeName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text with a type metadata. |

<a name='M-IoC-Extensibility-IIssueResolver-CannotRegister-IoC-IContainer,IoC-Key[]-'></a>
### CannotRegister(container,keys) `method` [#](#M-IoC-Extensibility-IIssueResolver-CannotRegister-IoC-IContainer,IoC-Key[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles the scenario when binding cannot be registered.

##### Returns

The registration tiken.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| keys | [IoC.Key[]](#T-IoC-Key[] 'IoC.Key[]') | The set of binding keys. |

<a name='M-IoC-Extensibility-IIssueResolver-CannotResolveDependency-IoC-IContainer,IoC-Key-'></a>
### CannotResolveDependency(container,key) `method` [#](#M-IoC-Extensibility-IIssueResolver-CannotResolveDependency-IoC-IContainer,IoC-Key- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles the scenario when the dependency cannot be resolved.

##### Returns

The pair of the dependency and of the lifetime.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| key | [IoC.Key](#T-IoC-Key 'IoC.Key') | The resolving key. |

<a name='M-IoC-Extensibility-IIssueResolver-CyclicDependenceDetected-IoC-Key,System-Int32-'></a>
### CyclicDependenceDetected(key,reentrancy) `method` [#](#M-IoC-Extensibility-IIssueResolver-CyclicDependenceDetected-IoC-Key,System-Int32- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles the scenario when a cyclic dependence was detected.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [IoC.Key](#T-IoC-Key 'IoC.Key') | The resolving key. |
| reentrancy | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The level of reentrancy. |

<a name='T-IoC-ILifetime'></a>
## ILifetime [#](#T-IoC-ILifetime 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents a lifetime for an instance.

<a name='M-IoC-ILifetime-Clone'></a>
### Clone() `method` [#](#M-IoC-ILifetime-Clone 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Clone this lifetime to use with generic instances.

##### Returns



##### Parameters

This method has no parameters.

<a name='M-IoC-ILifetime-GetOrCreate``1-IoC-IContainer,System-Object[],IoC-Resolver{``0}-'></a>
### GetOrCreate\`\`1(container,args,resolver) `method` [#](#M-IoC-ILifetime-GetOrCreate``1-IoC-IContainer,System-Object[],IoC-Resolver{``0}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or creates an instance.

##### Returns

The instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The resolving container. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The resolving arguments. |
| resolver | [IoC.Resolver{\`\`0}](#T-IoC-Resolver{``0} 'IoC.Resolver{``0}') | The base resolver. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of the instance. |

<a name='T-IoC-ImplicitNotNullAttribute'></a>
## ImplicitNotNullAttribute [#](#T-IoC-ImplicitNotNullAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Implicitly apply [NotNull]/[ItemNotNull] annotation to all the of type members and parameters in particular scope where this annotation is used (type declaration or whole assembly).

<a name='T-IoC-ImplicitUseKindFlags'></a>
## ImplicitUseKindFlags [#](#T-IoC-ImplicitUseKindFlags 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

<a name='F-IoC-ImplicitUseKindFlags-Access'></a>
### Access `constants` [#](#F-IoC-ImplicitUseKindFlags-Access 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Only entity marked with attribute considered used.

<a name='F-IoC-ImplicitUseKindFlags-Assign'></a>
### Assign `constants` [#](#F-IoC-ImplicitUseKindFlags-Assign 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Indicates implicit assignment to a member.

<a name='F-IoC-ImplicitUseKindFlags-InstantiatedNoFixedConstructorSignature'></a>
### InstantiatedNoFixedConstructorSignature `constants` [#](#F-IoC-ImplicitUseKindFlags-InstantiatedNoFixedConstructorSignature 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Indicates implicit instantiation of a type.

<a name='F-IoC-ImplicitUseKindFlags-InstantiatedWithFixedConstructorSignature'></a>
### InstantiatedWithFixedConstructorSignature `constants` [#](#F-IoC-ImplicitUseKindFlags-InstantiatedWithFixedConstructorSignature 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Indicates implicit instantiation of a type with fixed constructor signature. That means any unused constructor parameters won't be reported as such.

<a name='T-IoC-ImplicitUseTargetFlags'></a>
## ImplicitUseTargetFlags [#](#T-IoC-ImplicitUseTargetFlags 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Specify what is considered used implicitly when marked with [MeansImplicitUseAttribute](#T-IoC-MeansImplicitUseAttribute 'IoC.MeansImplicitUseAttribute') or [UsedImplicitlyAttribute](#T-IoC-UsedImplicitlyAttribute 'IoC.UsedImplicitlyAttribute').

<a name='F-IoC-ImplicitUseTargetFlags-Members'></a>
### Members `constants` [#](#F-IoC-ImplicitUseTargetFlags-Members 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Members of entity marked with attribute are considered used.

<a name='F-IoC-ImplicitUseTargetFlags-WithMembers'></a>
### WithMembers `constants` [#](#F-IoC-ImplicitUseTargetFlags-WithMembers 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Entity marked with attribute and all its members considered used.

<a name='T-IoC-Injections'></a>
## Injections [#](#T-IoC-Injections 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Injection extensions.

<a name='M-IoC-Injections-Inject``1-IoC-IContainer-'></a>
### Inject\`\`1(container) `method` [#](#M-IoC-Injections-Inject``1-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Injects the dependency. Just a marker.

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
### Inject\`\`1(container,tag) `method` [#](#M-IoC-Injections-Inject``1-IoC-IContainer,System-Object- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Injects the dependency. Just a marker.

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
### Inject\`\`1(container,destination,source) `method` [#](#M-IoC-Injections-Inject``1-IoC-IContainer,``0,``0- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Injects the dependency. Just a marker.

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

<a name='T-IoC-InstantHandleAttribute'></a>
## InstantHandleAttribute [#](#T-IoC-InstantHandleAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Tells code analysis engine if the parameter is completely handled when the invoked method is on stack. If the parameter is a delegate, indicates that delegate is executed while the method is executed. If the parameter is an enumerable, indicates that it is enumerated while the method is executed.

<a name='T-IoC-InvokerParameterNameAttribute'></a>
## InvokerParameterNameAttribute [#](#T-IoC-InvokerParameterNameAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that the function argument should be string literal and match one of the parameters of the caller function. For example, ReSharper annotates the parameter of [ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException').

##### Example

```
void Foo(string param) {
              if (param == null)
                throw new ArgumentNullException("par"); // Warning: Cannot resolve symbol
            }
```

<a name='T-IoC-ItemCanBeNullAttribute'></a>
## ItemCanBeNullAttribute [#](#T-IoC-ItemCanBeNullAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Can be appplied to symbols of types derived from IEnumerable as well as to symbols of Task and Lazy classes to indicate that the value of a collection item, of the Task.Result property or of the Lazy.Value property can be null.

<a name='T-IoC-ItemNotNullAttribute'></a>
## ItemNotNullAttribute [#](#T-IoC-ItemNotNullAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Can be appplied to symbols of types derived from IEnumerable as well as to symbols of Task and Lazy classes to indicate that the value of a collection item, of the Task.Result property or of the Lazy.Value property can never be null.

<a name='T-IoC-Extensibility-IValidator'></a>
## IValidator [#](#T-IoC-Extensibility-IValidator 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Extensibility

##### Summary

Represents a container validator.

<a name='M-IoC-Extensibility-IValidator-Validate-IoC-IContainer-'></a>
### Validate(container) `method` [#](#M-IoC-Extensibility-IValidator-Validate-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Calidates a container.

##### Returns

The validation results.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |

<a name='T-IoC-Key'></a>
## Key [#](#T-IoC-Key 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the key of binding.

<a name='M-IoC-Key-#ctor-System-Type,System-Object-'></a>
### #ctor(type,tag) `constructor` [#](#M-IoC-Key-#ctor-System-Type,System-Object- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates the instance of Key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') |  |
| tag | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='F-IoC-Key-AnyTag'></a>
### AnyTag `constants` [#](#F-IoC-Key-AnyTag 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The marker object for any tag.

<a name='F-IoC-Key-Tag'></a>
### Tag `constants` [#](#F-IoC-Key-Tag 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The tag.

<a name='F-IoC-Key-Type'></a>
### Type `constants` [#](#F-IoC-Key-Type 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The type.

<a name='M-IoC-Key-ToString'></a>
### ToString() `method` [#](#M-IoC-Key-ToString 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Features-LazyFeature'></a>
## LazyFeature [#](#T-IoC-Features-LazyFeature 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Features

##### Summary

Allows to resolve Lazy.

<a name='F-IoC-Features-LazyFeature-Shared'></a>
### Shared `constants` [#](#F-IoC-Features-LazyFeature-Shared 'Go To Here') [=](#contents 'Back To Contents')

<a name='M-IoC-Features-LazyFeature-Apply-IoC-IContainer-'></a>
### Apply() `method` [#](#M-IoC-Features-LazyFeature-Apply-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Lifetime'></a>
## Lifetime [#](#T-IoC-Lifetime 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

The enumeration of well-known lifetimes.

<a name='F-IoC-Lifetime-ContainerSingleton'></a>
### ContainerSingleton `constants` [#](#F-IoC-Lifetime-ContainerSingleton 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Singleton per container

<a name='F-IoC-Lifetime-ScopeSingleton'></a>
### ScopeSingleton `constants` [#](#F-IoC-Lifetime-ScopeSingleton 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Singleton per scope

<a name='F-IoC-Lifetime-Singleton'></a>
### Singleton `constants` [#](#F-IoC-Lifetime-Singleton 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Single instance per registration

<a name='F-IoC-Lifetime-ThreadSingleton'></a>
### ThreadSingleton `constants` [#](#F-IoC-Lifetime-ThreadSingleton 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Thread per thread

<a name='F-IoC-Lifetime-Transient'></a>
### Transient `constants` [#](#F-IoC-Lifetime-Transient 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Default lifetime. New instance each time (default).

<a name='T-IoC-LinqTunnelAttribute'></a>
## LinqTunnelAttribute [#](#T-IoC-LinqTunnelAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that method is pure LINQ method, with postponed enumeration (like Enumerables.Select, .Where). This annotation allows inference of [InstantHandle] annotation for parameters of delegate type by analyzing LINQ method chains.

<a name='T-IoC-LocalizationRequiredAttribute'></a>
## LocalizationRequiredAttribute [#](#T-IoC-LocalizationRequiredAttribute 'Go To Here') [=](#contents 'Back To Contents')

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
## MacroAttribute [#](#T-IoC-MacroAttribute 'Go To Here') [=](#contents 'Back To Contents')

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

You can apply the attribute on the whole method or on any of its additional parameters. The macro expression is defined in the [Expression](#P-IoC-MacroAttribute-Expression 'IoC.MacroAttribute.Expression') property. When applied on a method, the target template parameter is defined in the [Target](#P-IoC-MacroAttribute-Target 'IoC.MacroAttribute.Target') property. To apply the macro silently for the parameter, set the [Editable](#P-IoC-MacroAttribute-Editable 'IoC.MacroAttribute.Editable') property value = -1.

<a name='P-IoC-MacroAttribute-Editable'></a>
### Editable `property` [#](#P-IoC-MacroAttribute-Editable 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Allows specifying which occurrence of the target parameter becomes editable when the template is deployed.

##### Remarks

If the target parameter is used several times in the template, only one occurrence becomes editable; other occurrences are changed synchronously. To specify the zero-based index of the editable occurrence, use values >= 0. To make the parameter non-editable when the template is expanded, use -1.

<a name='P-IoC-MacroAttribute-Expression'></a>
### Expression `property` [#](#P-IoC-MacroAttribute-Expression 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Allows specifying a macro that will be executed for a [SourceTemplateAttribute](#T-IoC-SourceTemplateAttribute 'IoC.SourceTemplateAttribute') parameter when the template is expanded.

<a name='P-IoC-MacroAttribute-Target'></a>
### Target `property` [#](#P-IoC-MacroAttribute-Target 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Identifies the target parameter of a [SourceTemplateAttribute](#T-IoC-SourceTemplateAttribute 'IoC.SourceTemplateAttribute') if the [MacroAttribute](#T-IoC-MacroAttribute 'IoC.MacroAttribute') is applied on a template method.

<a name='T-IoC-MeansImplicitUseAttribute'></a>
## MeansImplicitUseAttribute [#](#T-IoC-MeansImplicitUseAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Should be used on attributes and causes ReSharper to not mark symbols marked with such attributes as unused (as well as by other usage inspections)

<a name='T-IoC-MustUseReturnValueAttribute'></a>
## MustUseReturnValueAttribute [#](#T-IoC-MustUseReturnValueAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that the return value of method invocation must be used.

<a name='T-IoC-NoEnumerationAttribute'></a>
## NoEnumerationAttribute [#](#T-IoC-NoEnumerationAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that IEnumerable, passed as parameter, is not enumerated.

<a name='T-IoC-NoReorder'></a>
## NoReorder [#](#T-IoC-NoReorder 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Prevents the Member Reordering feature from tossing members of the marked class.

##### Remarks

The attribute must be mentioned in your member reordering patterns

<a name='T-IoC-NotifyPropertyChangedInvocatorAttribute'></a>
## NotifyPropertyChangedInvocatorAttribute [#](#T-IoC-NotifyPropertyChangedInvocatorAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that the method is contained in a type that implements `System.ComponentModel.INotifyPropertyChanged` interface and this method is used to notify that some property value changed.

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

<a name='T-IoC-NotNullAttribute'></a>
## NotNullAttribute [#](#T-IoC-NotNullAttribute 'Go To Here') [=](#contents 'Back To Contents')

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

<a name='T-IoC-PathReferenceAttribute'></a>
## PathReferenceAttribute [#](#T-IoC-PathReferenceAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that a parameter is a path to a file or a folder within a web project. Path can be relative or absolute, starting from web root (~).

<a name='T-IoC-ProvidesContextAttribute'></a>
## ProvidesContextAttribute [#](#T-IoC-ProvidesContextAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates the type member or parameter of some type, that should be used instead of all other ways to get the value that type. This annotation is useful when you have some "context" value evaluated and stored somewhere, meaning that all other ways to get this value must be consolidated with existing one.

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
## PublicAPIAttribute [#](#T-IoC-PublicAPIAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

This attribute is intended to mark publicly available API which should not be removed and so is treated as used.

<a name='T-IoC-PureAttribute'></a>
## PureAttribute [#](#T-IoC-PureAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that a method does not make any observable state changes. The same as `System.Diagnostics.Contracts.PureAttribute`.

##### Example

```
[Pure] int Multiply(int x, int y) =&gt; x * y;
            
            void M() {
              Multiply(123, 42); // Waring: Return value of pure method is not used
            }
```

<a name='T-IoC-RazorSectionAttribute'></a>
## RazorSectionAttribute [#](#T-IoC-RazorSectionAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Razor attribute. Indicates that a parameter or a method is a Razor section. Use this attribute for custom wrappers similar to `System.Web.WebPages.WebPageBase.RenderSection(String)`.

<a name='T-IoC-RegexPatternAttribute'></a>
## RegexPatternAttribute [#](#T-IoC-RegexPatternAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that parameter is regular expression pattern.

<a name='T-IoC-Resolver`1'></a>
## Resolver\`1 [#](#T-IoC-Resolver`1 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the resolver delegate.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [T:IoC.Resolver\`1](#T-T-IoC-Resolver`1 'T:IoC.Resolver`1') | The resolving container. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The type of resolving instance. |

<a name='T-IoC-FluentGet-Resolving'></a>
## Resolving [#](#T-IoC-FluentGet-Resolving 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.FluentGet

##### Summary

Represents the resolving token.

<a name='F-IoC-FluentGet-Resolving-Container'></a>
### Container `constants` [#](#F-IoC-FluentGet-Resolving-Container 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The target container.

<a name='F-IoC-FluentGet-Resolving-Tag'></a>
### Tag `constants` [#](#F-IoC-FluentGet-Resolving-Tag 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The tag value for resolving.

<a name='T-IoC-Scope'></a>
## Scope [#](#T-IoC-Scope 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the scope which could be used with `Lifetime.ScopeSingleton`

<a name='M-IoC-Scope-#ctor-System-Object-'></a>
### #ctor(scopeKey) `constructor` [#](#M-IoC-Scope-#ctor-System-Object- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates the instance of a new scope.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scopeKey | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The key of scope. |

<a name='P-IoC-Scope-Current'></a>
### Current `property` [#](#P-IoC-Scope-Current 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The current scope.

<a name='M-IoC-Scope-Dispose'></a>
### Dispose() `method` [#](#M-IoC-Scope-Dispose 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Lifetimes-ScopeSingletonLifetime'></a>
## ScopeSingletonLifetime [#](#T-IoC-Lifetimes-ScopeSingletonLifetime 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Lifetimes

##### Summary

Represents singleton per scope lifetime.

<a name='M-IoC-Lifetimes-ScopeSingletonLifetime-#ctor-System-Func{IoC-ILifetime}-'></a>
### #ctor() `constructor` [#](#M-IoC-Lifetimes-ScopeSingletonLifetime-#ctor-System-Func{IoC-ILifetime}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-IoC-Lifetimes-ScopeSingletonLifetime-Clone'></a>
### Clone() `method` [#](#M-IoC-Lifetimes-ScopeSingletonLifetime-Clone 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ScopeSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]-'></a>
### CreateKey() `method` [#](#M-IoC-Lifetimes-ScopeSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ScopeSingletonLifetime-ToString'></a>
### ToString() `method` [#](#M-IoC-Lifetimes-ScopeSingletonLifetime-ToString 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Lifetimes-SingletonBasedLifetime`1'></a>
## SingletonBasedLifetime\`1 [#](#T-IoC-Lifetimes-SingletonBasedLifetime`1 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Lifetimes

##### Summary

Represents the abstaction for singleton based lifetimes.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TKey |  |

<a name='M-IoC-Lifetimes-SingletonBasedLifetime`1-#ctor-System-Func{IoC-ILifetime}-'></a>
### #ctor() `constructor` [#](#M-IoC-Lifetimes-SingletonBasedLifetime`1-#ctor-System-Func{IoC-ILifetime}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-IoC-Lifetimes-SingletonBasedLifetime`1-Clone'></a>
### Clone() `method` [#](#M-IoC-Lifetimes-SingletonBasedLifetime`1-Clone 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-SingletonBasedLifetime`1-CreateKey-IoC-IContainer,System-Object[]-'></a>
### CreateKey(container,args) `method` [#](#M-IoC-Lifetimes-SingletonBasedLifetime`1-CreateKey-IoC-IContainer,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates key for singleton.

##### Returns

The created key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| container | [IoC.IContainer](#T-IoC-IContainer 'IoC.IContainer') | The target container. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | The arfuments. |

<a name='M-IoC-Lifetimes-SingletonBasedLifetime`1-Dispose'></a>
### Dispose() `method` [#](#M-IoC-Lifetimes-SingletonBasedLifetime`1-Dispose 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-SingletonBasedLifetime`1-GetOrCreate``1-IoC-IContainer,System-Object[],IoC-Resolver{``0}-'></a>
### GetOrCreate\`\`1() `method` [#](#M-IoC-Lifetimes-SingletonBasedLifetime`1-GetOrCreate``1-IoC-IContainer,System-Object[],IoC-Resolver{``0}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-Lifetimes-SingletonLifetime'></a>
## SingletonLifetime [#](#T-IoC-Lifetimes-SingletonLifetime 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Lifetimes

##### Summary

Represents singleton lifetime.

<a name='M-IoC-Lifetimes-SingletonLifetime-Build-System-Linq-Expressions-Expression,IoC-Key,IoC-IContainer,System-Object-'></a>
### Build() `method` [#](#M-IoC-Lifetimes-SingletonLifetime-Build-System-Linq-Expressions-Expression,IoC-Key,IoC-IContainer,System-Object- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-SingletonLifetime-Clone'></a>
### Clone() `method` [#](#M-IoC-Lifetimes-SingletonLifetime-Clone 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-SingletonLifetime-Dispose'></a>
### Dispose() `method` [#](#M-IoC-Lifetimes-SingletonLifetime-Dispose 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-SingletonLifetime-GetOrCreate``1-IoC-IContainer,System-Object[],IoC-Resolver{``0}-'></a>
### GetOrCreate\`\`1() `method` [#](#M-IoC-Lifetimes-SingletonLifetime-GetOrCreate``1-IoC-IContainer,System-Object[],IoC-Resolver{``0}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-SingletonLifetime-ToString'></a>
### ToString() `method` [#](#M-IoC-Lifetimes-SingletonLifetime-ToString 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-SourceTemplateAttribute'></a>
## SourceTemplateAttribute [#](#T-IoC-SourceTemplateAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

An extension method marked with this attribute is processed by ReSharper code completion as a 'Source Template'. When extension method is completed over some expression, it's source code is automatically expanded like a template at call site.

##### Example

In this example, the 'forEach' method is a source template available over all values of enumerable types, producing ordinary C# 'foreach' statement and placing caret inside block:

```
[SourceTemplate]
            public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; xs) {
              foreach (var x in xs) {
                 //$ $END$
              }
            }
```

##### Remarks

Template method body can contain valid source code and/or special comments starting with '$'. Text inside these comments is added as source code when the template is applied. Template parameters can be used either as additional method parameters or as identifiers wrapped in two '$' signs. Use the [MacroAttribute](#T-IoC-MacroAttribute 'IoC.MacroAttribute') attribute to specify macros for parameters.

<a name='T-IoC-StringFormatMethodAttribute'></a>
## StringFormatMethodAttribute [#](#T-IoC-StringFormatMethodAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that the marked method builds string by format pattern and (optional) arguments. Parameter, which contains format string, should be given in constructor. The format string should be in [Format](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Format 'System.String.Format(System.IFormatProvider,System.String,System.Object[])')-like form.

##### Example

```
[StringFormatMethod("message")]
            void ShowError(string message, params object[] args) { /* do something */ }
            
            void Foo() {
              ShowError("Failed: {0}"); // Warning: Non-existing argument in format string
            }
```

<a name='M-IoC-StringFormatMethodAttribute-#ctor-System-String-'></a>
### #ctor(formatParameterName) `constructor` [#](#M-IoC-StringFormatMethodAttribute-#ctor-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| formatParameterName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Specifies which parameter of an annotated method should be treated as format-string |

<a name='T-IoC-Features-TaskFeature'></a>
## TaskFeature [#](#T-IoC-Features-TaskFeature 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Features

##### Summary

Allows to resolve Tasks.

<a name='F-IoC-Features-TaskFeature-Shared'></a>
### Shared `constants` [#](#F-IoC-Features-TaskFeature-Shared 'Go To Here') [=](#contents 'Back To Contents')

<a name='M-IoC-Features-TaskFeature-Apply-IoC-IContainer-'></a>
### Apply() `method` [#](#M-IoC-Features-TaskFeature-Apply-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-TerminatesProgramAttribute'></a>
## TerminatesProgramAttribute [#](#T-IoC-TerminatesProgramAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that the marked method unconditionally terminates control flow execution. For example, it could unconditionally throw exception.

<a name='T-IoC-Lifetimes-ThreadSingletonLifetime'></a>
## ThreadSingletonLifetime [#](#T-IoC-Lifetimes-ThreadSingletonLifetime 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Lifetimes

##### Summary

Represents singleton per thread lifetime.

<a name='M-IoC-Lifetimes-ThreadSingletonLifetime-#ctor-System-Func{IoC-ILifetime}-'></a>
### #ctor() `constructor` [#](#M-IoC-Lifetimes-ThreadSingletonLifetime-#ctor-System-Func{IoC-ILifetime}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='M-IoC-Lifetimes-ThreadSingletonLifetime-Clone'></a>
### Clone() `method` [#](#M-IoC-Lifetimes-ThreadSingletonLifetime-Clone 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ThreadSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]-'></a>
### CreateKey() `method` [#](#M-IoC-Lifetimes-ThreadSingletonLifetime-CreateKey-IoC-IContainer,System-Object[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-IoC-Lifetimes-ThreadSingletonLifetime-ToString'></a>
### ToString() `method` [#](#M-IoC-Lifetimes-ThreadSingletonLifetime-ToString 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-TT'></a>
## TT [#](#T-IoC-TT 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the generic type parameter marker.

<a name='T-IoC-TT1'></a>
## TT1 [#](#T-IoC-TT1 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the generic type parameter marker.

<a name='T-IoC-TT2'></a>
## TT2 [#](#T-IoC-TT2 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the generic type parameter marker.

<a name='T-IoC-TT3'></a>
## TT3 [#](#T-IoC-TT3 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the generic type parameter marker.

<a name='T-IoC-TT4'></a>
## TT4 [#](#T-IoC-TT4 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the generic type parameter marker.

<a name='T-IoC-TT5'></a>
## TT5 [#](#T-IoC-TT5 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the generic type parameter marker.

<a name='T-IoC-TT6'></a>
## TT6 [#](#T-IoC-TT6 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the generic type parameter marker.

<a name='T-IoC-TT7'></a>
## TT7 [#](#T-IoC-TT7 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the generic type parameter marker.

<a name='T-IoC-TT8'></a>
## TT8 [#](#T-IoC-TT8 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the generic type parameter marker.

<a name='T-IoC-Features-TupleFeature'></a>
## TupleFeature [#](#T-IoC-Features-TupleFeature 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC.Features

##### Summary

Allows to resolve Tuples.

<a name='F-IoC-Features-TupleFeature-Shared'></a>
### Shared `constants` [#](#F-IoC-Features-TupleFeature-Shared 'Go To Here') [=](#contents 'Back To Contents')

<a name='M-IoC-Features-TupleFeature-Apply-IoC-IContainer-'></a>
### Apply() `method` [#](#M-IoC-Features-TupleFeature-Apply-IoC-IContainer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-IoC-UsedImplicitlyAttribute'></a>
## UsedImplicitlyAttribute [#](#T-IoC-UsedImplicitlyAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Indicates that the marked symbol is used implicitly (e.g. via reflection, in external library), so this symbol will not be marked as unused (as well as by other usage inspections).

<a name='T-IoC-ValidationResult'></a>
## ValidationResult [#](#T-IoC-ValidationResult 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents a container's validation result.

<a name='F-IoC-ValidationResult-ResolvedKey'></a>
### ResolvedKey `constants` [#](#F-IoC-ValidationResult-ResolvedKey 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Keys that were resolved successfully.

<a name='F-IoC-ValidationResult-UnresolvedKeys'></a>
### UnresolvedKeys `constants` [#](#F-IoC-ValidationResult-UnresolvedKeys 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Keys that were not resolved successfully.

<a name='P-IoC-ValidationResult-IsValid'></a>
### IsValid `property` [#](#P-IoC-ValidationResult-IsValid 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

True if the container could be used successfully.

<a name='T-IoC-ValueProviderAttribute'></a>
## ValueProviderAttribute [#](#T-IoC-ValueProviderAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

For a parameter that is expected to be one of the limited set of values. Specify fields of which type should be used as values for this parameter.

<a name='T-IoC-WellknownContainers'></a>
## WellknownContainers [#](#T-IoC-WellknownContainers 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

Represents the enumeration of well-known containers.

<a name='F-IoC-WellknownContainers-Child'></a>
### Child `constants` [#](#F-IoC-WellknownContainers-Child 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates new child container.

<a name='F-IoC-WellknownContainers-Current'></a>
### Current `constants` [#](#F-IoC-WellknownContainers-Current 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Current container.

<a name='F-IoC-WellknownContainers-Parent'></a>
### Parent `constants` [#](#F-IoC-WellknownContainers-Parent 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Parent container.

<a name='T-IoC-XamlItemBindingOfItemsControlAttribute'></a>
## XamlItemBindingOfItemsControlAttribute [#](#T-IoC-XamlItemBindingOfItemsControlAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

XAML attribute. Indicates the property of some `BindingBase`-derived type, that is used to bind some item of `ItemsControl`-derived type. This annotation will enable the `DataContext` type resolve for XAML bindings for such properties.

##### Remarks

Property should have the tree ancestor of the `ItemsControl` type or marked with the [XamlItemsControlAttribute](#T-IoC-XamlItemsControlAttribute 'IoC.XamlItemsControlAttribute') attribute.

<a name='T-IoC-XamlItemsControlAttribute'></a>
## XamlItemsControlAttribute [#](#T-IoC-XamlItemsControlAttribute 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

IoC

##### Summary

XAML attribute. Indicates the type that has `ItemsSource` property and should be treated as `ItemsControl`-derived type, to enable inner items `DataContext` type resolve.
