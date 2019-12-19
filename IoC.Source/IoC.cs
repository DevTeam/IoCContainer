﻿
/*
IoC Container

https://github.com/DevTeam/IoCContainer

Important note: do not use any internal classes, structures, enums, interfaces, methods, fields or properties
because it may be changed even in minor updates of package.

MIT License

Copyright (c) 2018-2020 Nikolay Pianikov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

#pragma warning disable CS1658 // Warning is overriding an error
#pragma warning disable nullable
#pragma warning restore CS1658 // Warning is overriding an error

// ReSharper disable All

#region Properties

#region AssemblyInfo

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IoC.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001003fa521b0b16e978a933ecce70646c632538351d320a226a64b2c93238b3ba699cb66233e5722c25dd64f816c2aef8d2f1426983ea8c4750902f4a8b03cb00da22e7c978f56cdcfc711ea0a3625016a2ec2238093912799a3cda4ee787592738c7d21f6eed5e3a6d1b03f657ac3880672f2394144bd2359fddf17e464abd947a0")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]

#endregion

#endregion

#region IoC

#region Annotations

// ReSharper disable All
namespace IoC
{
    /* MIT License

    Copyright (c) 2016 JetBrains http://www.jetbrains.com

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE. */
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591
    // ReSharper disable UnusedMember.Global
    // ReSharper disable MemberCanBePrivate.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable IntroduceOptionalParameters.Global
    // ReSharper disable MemberCanBeProtected.Global
    // ReSharper disable InconsistentNaming

    /// <summary>
    /// Indicates that the value of the marked element could be <c>null</c> sometimes,
    /// so the check for <c>null</c> is necessary before its usage.
    /// </summary>
    /// <example><code>
    /// [CanBeNull] object Test() => null;
    /// 
    /// void UseTest() {
    ///   var p = Test();
    ///   var s = p.ToString(); // Warning: Possible 'System.NullReferenceException'
    /// }
    /// </code></example>
    [AttributeUsage(
      AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
      AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
      AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
    public sealed class CanBeNullAttribute : Attribute { }

    /// <summary>
    /// Indicates that the value of the marked element could never be <c>null</c>.
    /// </summary>
    /// <example><code>
    /// [NotNull] object Foo() {
    ///   return null; // Warning: Possible 'null' assignment
    /// }
    /// </code></example>
    [AttributeUsage(
      AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
      AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
      AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
    public sealed class NotNullAttribute : Attribute { }

    /// <summary>
    /// Can be appplied to symbols of types derived from IEnumerable as well as to symbols of Task
    /// and Lazy classes to indicate that the value of a collection item, of the Task.Result property
    /// or of the Lazy.Value property can never be null.
    /// </summary>
    [AttributeUsage(
      AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
      AttributeTargets.Delegate | AttributeTargets.Field)]
    public sealed class ItemNotNullAttribute : Attribute { }

    /// <summary>
    /// Can be appplied to symbols of types derived from IEnumerable as well as to symbols of Task
    /// and Lazy classes to indicate that the value of a collection item, of the Task.Result property
    /// or of the Lazy.Value property can be null.
    /// </summary>
    [AttributeUsage(
      AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
      AttributeTargets.Delegate | AttributeTargets.Field)]
    public sealed class ItemCanBeNullAttribute : Attribute { }

    /// <summary>
    /// Implicitly apply [NotNull]/[ItemNotNull] annotation to all the of type members and parameters
    /// in particular scope where this annotation is used (type declaration or whole assembly).
    /// </summary>
    [AttributeUsage(
      AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Assembly)]
    public sealed class ImplicitNotNullAttribute : Attribute { }

    /// <summary>
    /// Indicates that the marked method builds string by format pattern and (optional) arguments.
    /// Parameter, which contains format string, should be given in constructor. The format string
    /// should be in <see cref="string.Format(IFormatProvider,string,object[])"/>-like form.
    /// </summary>
    /// <example><code>
    /// [StringFormatMethod("message")]
    /// void ShowError(string message, params object[] args) { /* do something */ }
    /// 
    /// void Foo() {
    ///   ShowError("Failed: {0}"); // Warning: Non-existing argument in format string
    /// }
    /// </code></example>
    [AttributeUsage(
      AttributeTargets.Constructor | AttributeTargets.Method |
      AttributeTargets.Property | AttributeTargets.Delegate)]
    public sealed class StringFormatMethodAttribute : Attribute
    {
        /// <param name="formatParameterName">
        /// Specifies which parameter of an annotated method should be treated as format-string
        /// </param>
        public StringFormatMethodAttribute([NotNull] string formatParameterName)
        {
            FormatParameterName = formatParameterName;
        }

        [NotNull]
        public string FormatParameterName { get; private set; }
    }

    /// <summary>
    /// For a parameter that is expected to be one of the limited set of values.
    /// Specify fields of which type should be used as values for this parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class ValueProviderAttribute : Attribute
    {
        public ValueProviderAttribute([NotNull] string name)
        {
            Name = name;
        }

        [NotNull]
        public string Name { get; private set; }
    }

    /// <summary>
    /// Indicates that the function argument should be string literal and match one
    /// of the parameters of the caller function. For example, ReSharper annotates
    /// the parameter of <see cref="System.ArgumentNullException"/>.
    /// </summary>
    /// <example><code>
    /// void Foo(string param) {
    ///   if (param == null)
    ///     throw new ArgumentNullException("par"); // Warning: Cannot resolve symbol
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class InvokerParameterNameAttribute : Attribute { }

    /// <summary>
    /// Indicates that the method is contained in a type that implements
    /// <c>System.ComponentModel.INotifyPropertyChanged</c> interface and this method
    /// is used to notify that some property value changed.
    /// </summary>
    /// <remarks>
    /// The method should be non-static and conform to one of the supported signatures:
    /// <list>
    /// <item><c>NotifyChanged(string)</c></item>
    /// <item><c>NotifyChanged(params string[])</c></item>
    /// <item><c>NotifyChanged{T}(Expression{Func{T}})</c></item>
    /// <item><c>NotifyChanged{T,U}(Expression{Func{T,U}})</c></item>
    /// <item><c>SetProperty{T}(ref T, T, string)</c></item>
    /// </list>
    /// </remarks>
    /// <example><code>
    /// public class Foo : INotifyPropertyChanged {
    ///   public event PropertyChangedEventHandler PropertyChanged;
    /// 
    ///   [NotifyPropertyChangedInvocator]
    ///   protected virtual void NotifyChanged(string propertyName) { ... }
    ///
    ///   string _name;
    /// 
    ///   public string Name {
    ///     get { return _name; }
    ///     set { _name = value; NotifyChanged("LastName"); /* Warning */ }
    ///   }
    /// }
    /// </code>
    /// Examples of generated notifications:
    /// <list>
    /// <item><c>NotifyChanged("Property")</c></item>
    /// <item><c>NotifyChanged(() =&gt; Property)</c></item>
    /// <item><c>NotifyChanged((VM x) =&gt; x.Property)</c></item>
    /// <item><c>SetProperty(ref myField, value, "Property")</c></item>
    /// </list>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
    {
        public NotifyPropertyChangedInvocatorAttribute() { }
        public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
        {
            ParameterName = parameterName;
        }

        [CanBeNull]
        public string ParameterName { get; private set; }
    }

    /// <summary>
    /// Describes dependency between method input and output.
    /// </summary>
    /// <syntax>
    /// <p>Function Definition Table syntax:</p>
    /// <list>
    /// <item>FDT      ::= FDTRow [;FDTRow]*</item>
    /// <item>FDTRow   ::= Input =&gt; Output | Output &lt;= Input</item>
    /// <item>Input    ::= ParameterName: Value [, Input]*</item>
    /// <item>Output   ::= [ParameterName: Value]* {halt|stop|void|nothing|Value}</item>
    /// <item>Value    ::= true | false | null | notnull | canbenull</item>
    /// </list>
    /// If method has single input parameter, it's name could be omitted.<br/>
    /// Using <c>halt</c> (or <c>void</c>/<c>nothing</c>, which is the same)
    /// for method output means that the methos doesn't return normally.<br/>
    /// <c>canbenull</c> annotation is only applicable for output parameters.<br/>
    /// You can use multiple <c>[ContractAnnotation]</c> for each FDT row,
    /// or use single attribute with rows separated by semicolon.<br/>
    /// </syntax>
    /// <examples><list>
    /// <item><code>
    /// [ContractAnnotation("=> halt")]
    /// public void TerminationMethod()
    /// </code></item>
    /// <item><code>
    /// [ContractAnnotation("halt &lt;= condition: false")]
    /// public void Assert(bool condition, string text) // regular assertion method
    /// </code></item>
    /// <item><code>
    /// [ContractAnnotation("s:null => true")]
    /// public bool IsNullOrEmpty(string s) // string.IsNullOrEmpty()
    /// </code></item>
    /// <item><code>
    /// // A method that returns null if the parameter is null,
    /// // and not null if the parameter is not null
    /// [ContractAnnotation("null => null; notnull => notnull")]
    /// public object Transform(object data) 
    /// </code></item>
    /// <item><code>
    /// [ContractAnnotation("s:null=>false; =>true,result:notnull; =>false, result:null")]
    /// public bool TryParse(string s, out Person result)
    /// </code></item>
    /// </list></examples>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ContractAnnotationAttribute : Attribute
    {
        public ContractAnnotationAttribute([NotNull] string contract)
          : this(contract, false) { }

        public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
        {
            Contract = contract;
            ForceFullStates = forceFullStates;
        }

        [NotNull]
        public string Contract { get; private set; }
        public bool ForceFullStates { get; private set; }
    }

    /// <summary>
    /// Indicates that marked element should be localized or not.
    /// </summary>
    /// <example><code>
    /// [LocalizationRequiredAttribute(true)]
    /// class Foo {
    ///   string str = "my string"; // Warning: Localizable string
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class LocalizationRequiredAttribute : Attribute
    {
        public LocalizationRequiredAttribute() : this(true) { }
        public LocalizationRequiredAttribute(bool required)
        {
            Required = required;
        }

        public bool Required { get; private set; }
    }

    /// <summary>
    /// Indicates that the value of the marked type (or its derivatives)
    /// cannot be compared using '==' or '!=' operators and <c>Equals()</c>
    /// should be used instead. However, using '==' or '!=' for comparison
    /// with <c>null</c> is always permitted.
    /// </summary>
    /// <example><code>
    /// [CannotApplyEqualityOperator]
    /// class NoEquality { }
    /// 
    /// class UsesNoEquality {
    ///   void Test() {
    ///     var ca1 = new NoEquality();
    ///     var ca2 = new NoEquality();
    ///     if (ca1 != null) { // OK
    ///       bool condition = ca1 == ca2; // Warning
    ///     }
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class CannotApplyEqualityOperatorAttribute : Attribute { }

    /// <summary>
    /// When applied to a target attribute, specifies a requirement for any type marked
    /// with the target attribute to implement or inherit specific type or types.
    /// </summary>
    /// <example><code>
    /// [BaseTypeRequired(typeof(IComponent)] // Specify requirement
    /// class ComponentAttribute : Attribute { }
    /// 
    /// [Component] // ComponentAttribute requires implementing IComponent interface
    /// class MyComponent : IComponent { }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [BaseTypeRequired(typeof(Attribute))]
    public sealed class BaseTypeRequiredAttribute : Attribute
    {
        public BaseTypeRequiredAttribute([NotNull] Type baseType)
        {
            BaseType = baseType;
        }

        [NotNull]
        public Type BaseType { get; private set; }
    }

    /// <summary>
    /// Indicates that the marked symbol is used implicitly (e.g. via reflection, in external library),
    /// so this symbol will not be marked as unused (as well as by other usage inspections).
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class UsedImplicitlyAttribute : Attribute
    {
        public UsedImplicitlyAttribute()
          : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default) { }

        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
          : this(useKindFlags, ImplicitUseTargetFlags.Default) { }

        public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
          : this(ImplicitUseKindFlags.Default, targetFlags) { }

        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        public ImplicitUseKindFlags UseKindFlags { get; private set; }
        public ImplicitUseTargetFlags TargetFlags { get; private set; }
    }

    /// <summary>
    /// Should be used on attributes and causes ReSharper to not mark symbols marked with such attributes
    /// as unused (as well as by other usage inspections)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.GenericParameter)]
    public sealed class MeansImplicitUseAttribute : Attribute
    {
        public MeansImplicitUseAttribute()
          : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default) { }

        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
          : this(useKindFlags, ImplicitUseTargetFlags.Default) { }

        public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
          : this(ImplicitUseKindFlags.Default, targetFlags) { }

        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        [UsedImplicitly]
        public ImplicitUseKindFlags UseKindFlags { get; private set; }
        [UsedImplicitly]
        public ImplicitUseTargetFlags TargetFlags { get; private set; }
    }

    [Flags]
    public enum ImplicitUseKindFlags
    {
        Default = Access | Assign | InstantiatedWithFixedConstructorSignature,
        /// <summary>Only entity marked with attribute considered used.</summary>
        Access = 1,
        /// <summary>Indicates implicit assignment to a member.</summary>
        Assign = 2,
        /// <summary>
        /// Indicates implicit instantiation of a type with fixed constructor signature.
        /// That means any unused constructor parameters won't be reported as such.
        /// </summary>
        InstantiatedWithFixedConstructorSignature = 4,
        /// <summary>Indicates implicit instantiation of a type.</summary>
        InstantiatedNoFixedConstructorSignature = 8,
    }

    /// <summary>
    /// Specify what is considered used implicitly when marked
    /// with <see cref="MeansImplicitUseAttribute"/> or <see cref="UsedImplicitlyAttribute"/>.
    /// </summary>
    [Flags]
    public enum ImplicitUseTargetFlags
    {
        Default = Itself,
        Itself = 1,
        /// <summary>Members of entity marked with attribute are considered used.</summary>
        Members = 2,
        /// <summary>Entity marked with attribute and all its members considered used.</summary>
        WithMembers = Itself | Members
    }

    /// <summary>
    /// This attribute is intended to mark publicly available API
    /// which should not be removed and so is treated as used.
    /// </summary>
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    public sealed class PublicAPIAttribute : Attribute
    {
        public PublicAPIAttribute() { }
        public PublicAPIAttribute([NotNull] string comment)
        {
            Comment = comment;
        }

        [CanBeNull]
        public string Comment { get; private set; }
    }

    /// <summary>
    /// Tells code analysis engine if the parameter is completely handled when the invoked method is on stack.
    /// If the parameter is a delegate, indicates that delegate is executed while the method is executed.
    /// If the parameter is an enumerable, indicates that it is enumerated while the method is executed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class InstantHandleAttribute : Attribute { }

    /// <summary>
    /// Indicates that a method does not make any observable state changes.
    /// The same as <c>System.Diagnostics.Contracts.PureAttribute</c>.
    /// </summary>
    /// <example><code>
    /// [Pure] int Multiply(int x, int y) => x * y;
    /// 
    /// void M() {
    ///   Multiply(123, 42); // Waring: Return value of pure method is not used
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PureAttribute : Attribute { }

    /// <summary>
    /// Indicates that the return value of method invocation must be used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MustUseReturnValueAttribute : Attribute
    {
        public MustUseReturnValueAttribute() { }
        public MustUseReturnValueAttribute([NotNull] string justification)
        {
            Justification = justification;
        }

        [CanBeNull]
        public string Justification { get; private set; }
    }

    /// <summary>
    /// Indicates the type member or parameter of some type, that should be used instead of all other ways
    /// to get the value that type. This annotation is useful when you have some "context" value evaluated
    /// and stored somewhere, meaning that all other ways to get this value must be consolidated with existing one.
    /// </summary>
    /// <example><code>
    /// class Foo {
    ///   [ProvidesContext] IBarService _barService = ...;
    /// 
    ///   void ProcessNode(INode node) {
    ///     DoSomething(node, node.GetGlobalServices().Bar);
    ///     //              ^ Warning: use value of '_barService' field
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(
      AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Method |
      AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.GenericParameter)]
    public sealed class ProvidesContextAttribute : Attribute { }

    /// <summary>
    /// Indicates that a parameter is a path to a file or a folder within a web project.
    /// Path can be relative or absolute, starting from web root (~).
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class PathReferenceAttribute : Attribute
    {
        public PathReferenceAttribute() { }
        public PathReferenceAttribute([NotNull, PathReference] string basePath)
        {
            BasePath = basePath;
        }

        [CanBeNull]
        public string BasePath { get; private set; }
    }

    /// <summary>
    /// An extension method marked with this attribute is processed by ReSharper code completion
    /// as a 'Source Template'. When extension method is completed over some expression, it's source code
    /// is automatically expanded like a template at call site.
    /// </summary>
    /// <remarks>
    /// Template method body can contain valid source code and/or special comments starting with '$'.
    /// Text inside these comments is added as source code when the template is applied. Template parameters
    /// can be used either as additional method parameters or as identifiers wrapped in two '$' signs.
    /// Use the <see cref="MacroAttribute"/> attribute to specify macros for parameters.
    /// </remarks>
    /// <example>
    /// In this example, the 'forEach' method is a source template available over all values
    /// of enumerable types, producing ordinary C# 'foreach' statement and placing caret inside block:
    /// <code>
    /// [SourceTemplate]
    /// public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; xs) {
    ///   foreach (var x in xs) {
    ///      //$ $END$
    ///   }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SourceTemplateAttribute : Attribute { }

    /// <summary>
    /// Allows specifying a macro for a parameter of a <see cref="SourceTemplateAttribute">source template</see>.
    /// </summary>
    /// <remarks>
    /// You can apply the attribute on the whole method or on any of its additional parameters. The macro expression
    /// is defined in the <see cref="MacroAttribute.Expression"/> property. When applied on a method, the target
    /// template parameter is defined in the <see cref="MacroAttribute.Target"/> property. To apply the macro silently
    /// for the parameter, set the <see cref="MacroAttribute.Editable"/> property value = -1.
    /// </remarks>
    /// <example>
    /// Applying the attribute on a source template method:
    /// <code>
    /// [SourceTemplate, Macro(Target = "item", Expression = "suggestVariableName()")]
    /// public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; collection) {
    ///   foreach (var item in collection) {
    ///     //$ $END$
    ///   }
    /// }
    /// </code>
    /// Applying the attribute on a template method parameter:
    /// <code>
    /// [SourceTemplate]
    /// public static void something(this Entity x, [Macro(Expression = "guid()", Editable = -1)] string newguid) {
    ///   /*$ var $x$Id = "$newguid$" + x.ToString();
    ///   x.DoSomething($x$Id); */
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class MacroAttribute : Attribute
    {
        /// <summary>
        /// Allows specifying a macro that will be executed for a <see cref="SourceTemplateAttribute">source template</see>
        /// parameter when the template is expanded.
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// Allows specifying which occurrence of the target parameter becomes editable when the template is deployed.
        /// </summary>
        /// <remarks>
        /// If the target parameter is used several times in the template, only one occurrence becomes editable;
        /// other occurrences are changed synchronously. To specify the zero-based index of the editable occurrence,
        /// use values >= 0. To make the parameter non-editable when the template is expanded, use -1.
        /// </remarks>>
        public int Editable { get; set; }

        /// <summary>
        /// Identifies the target parameter of a <see cref="SourceTemplateAttribute">source template</see> if the
        /// <see cref="MacroAttribute"/> is applied on a template method.
        /// </summary>
        public string Target { get; set; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcAreaMasterLocationFormatAttribute : Attribute
    {
        public AspMvcAreaMasterLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull]
        public string Format { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
    {
        public AspMvcAreaPartialViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull]
        public string Format { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
    {
        public AspMvcAreaViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull]
        public string Format { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcMasterLocationFormatAttribute : Attribute
    {
        public AspMvcMasterLocationFormatAttribute(string format)
        {
            Format = format;
        }

        public string Format { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcPartialViewLocationFormatAttribute : Attribute
    {
        public AspMvcPartialViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull]
        public string Format { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcViewLocationFormatAttribute : Attribute
    {
        public AspMvcViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull]
        public string Format { get; private set; }
    }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC action. If applied to a method, the MVC action name is calculated
    /// implicitly from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcActionAttribute : Attribute
    {
        public AspMvcActionAttribute() { }
        public AspMvcActionAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }

        [CanBeNull]
        public string AnonymousProperty { get; private set; }
    }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC area.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcAreaAttribute : Attribute
    {
        public AspMvcAreaAttribute() { }
        public AspMvcAreaAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }

        [CanBeNull]
        public string AnonymousProperty { get; private set; }
    }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is
    /// an MVC controller. If applied to a method, the MVC controller name is calculated
    /// implicitly from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcControllerAttribute : Attribute
    {
        public AspMvcControllerAttribute() { }
        public AspMvcControllerAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }

        [CanBeNull]
        public string AnonymousProperty { get; private set; }
    }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC Master. Use this attribute
    /// for custom wrappers similar to <c>System.Web.Mvc.Controller.View(String, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcMasterAttribute : Attribute { }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC model type. Use this attribute
    /// for custom wrappers similar to <c>System.Web.Mvc.Controller.View(String, Object)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcModelTypeAttribute : Attribute { }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC
    /// partial view. If applied to a method, the MVC partial view name is calculated implicitly
    /// from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcPartialViewAttribute : Attribute { }

    /// <summary>
    /// ASP.NET MVC attribute. Allows disabling inspections for MVC views within a class or a method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AspMvcSuppressViewErrorAttribute : Attribute { }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC display template.
    /// Use this attribute for custom wrappers similar to 
    /// <c>System.Web.Mvc.Html.DisplayExtensions.DisplayForModel(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcDisplayTemplateAttribute : Attribute { }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC editor template.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.EditorExtensions.EditorForModel(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcEditorTemplateAttribute : Attribute { }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC template.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.ComponentModel.DataAnnotations.UIHintAttribute(System.String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcTemplateAttribute : Attribute { }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC view component. If applied to a method, the MVC view name is calculated implicitly
    /// from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Controller.View(Object)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcViewAttribute : Attribute { }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC view component name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcViewComponentAttribute : Attribute { }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC view component view. If applied to a method, the MVC view component view name is default.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcViewComponentViewAttribute : Attribute { }

    /// <summary>
    /// ASP.NET MVC attribute. When applied to a parameter of an attribute,
    /// indicates that this parameter is an MVC action name.
    /// </summary>
    /// <example><code>
    /// [ActionName("Foo")]
    /// public ActionResult Login(string returnUrl) {
    ///   ViewBag.ReturnUrl = Url.Action("Foo"); // OK
    ///   return RedirectToAction("Bar"); // Error: Cannot resolve action
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public sealed class AspMvcActionSelectorAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class HtmlElementAttributesAttribute : Attribute
    {
        public HtmlElementAttributesAttribute() { }
        public HtmlElementAttributesAttribute([NotNull] string name)
        {
            Name = name;
        }

        [CanBeNull]
        public string Name { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class HtmlAttributeValueAttribute : Attribute
    {
        public HtmlAttributeValueAttribute([NotNull] string name)
        {
            Name = name;
        }

        [NotNull]
        public string Name { get; private set; }
    }

    /// <summary>
    /// Razor attribute. Indicates that a parameter or a method is a Razor section.
    /// Use this attribute for custom wrappers similar to 
    /// <c>System.Web.WebPages.WebPageBase.RenderSection(String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class RazorSectionAttribute : Attribute { }

    /// <summary>
    /// Indicates how method, constructor invocation or property access
    /// over collection type affects content of the collection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property)]
    public sealed class CollectionAccessAttribute : Attribute
    {
        public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
        {
            CollectionAccessType = collectionAccessType;
        }

        public CollectionAccessType CollectionAccessType { get; private set; }
    }

    [Flags]
    public enum CollectionAccessType
    {
        /// <summary>Method does not use or modify content of the collection.</summary>
        None = 0,
        /// <summary>Method only reads content of the collection but does not modify it.</summary>
        Read = 1,
        /// <summary>Method can change content of the collection but does not add new elements.</summary>
        ModifyExistingContent = 2,
        /// <summary>Method can add new elements to the collection.</summary>
        UpdatedContent = ModifyExistingContent | 4
    }

    /// <summary>
    /// Indicates that the marked method is assertion method, i.e. it halts control flow if
    /// one of the conditions is satisfied. To set the condition, mark one of the parameters with 
    /// <see cref="AssertionConditionAttribute"/> attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AssertionMethodAttribute : Attribute { }

    /// <summary>
    /// Indicates the condition parameter of the assertion method. The method itself should be
    /// marked by <see cref="AssertionMethodAttribute"/> attribute. The mandatory argument of
    /// the attribute is the assertion type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AssertionConditionAttribute : Attribute
    {
        public AssertionConditionAttribute(AssertionConditionType conditionType)
        {
            ConditionType = conditionType;
        }

        public AssertionConditionType ConditionType { get; private set; }
    }

    /// <summary>
    /// Specifies assertion type. If the assertion method argument satisfies the condition,
    /// then the execution continues. Otherwise, execution is assumed to be halted.
    /// </summary>
    public enum AssertionConditionType
    {
        /// <summary>Marked parameter should be evaluated to true.</summary>
        IS_TRUE = 0,
        /// <summary>Marked parameter should be evaluated to false.</summary>
        IS_FALSE = 1,
        /// <summary>Marked parameter should be evaluated to null value.</summary>
        IS_NULL = 2,
        /// <summary>Marked parameter should be evaluated to not null value.</summary>
        IS_NOT_NULL = 3,
    }

    /// <summary>
    /// Indicates that the marked method unconditionally terminates control flow execution.
    /// For example, it could unconditionally throw exception.
    /// </summary>
    [Obsolete("Use [ContractAnnotation('=> halt')] instead")]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TerminatesProgramAttribute : Attribute { }

    /// <summary>
    /// Indicates that method is pure LINQ method, with postponed enumeration (like Enumerables.Select,
    /// .Where). This annotation allows inference of [InstantHandle] annotation for parameters
    /// of delegate type by analyzing LINQ method chains.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LinqTunnelAttribute : Attribute { }

    /// <summary>
    /// Indicates that IEnumerable, passed as parameter, is not enumerated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class NoEnumerationAttribute : Attribute { }

    /// <summary>
    /// Indicates that parameter is regular expression pattern.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class RegexPatternAttribute : Attribute { }

    /// <summary>
    /// XAML attribute. Indicates the type that has <c>ItemsSource</c> property and should be treated
    /// as <c>ItemsControl</c>-derived type, to enable inner items <c>DataContext</c> type resolve.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class XamlItemsControlAttribute : Attribute { }

    /// <summary>
    /// XAML attribute. Indicates the property of some <c>BindingBase</c>-derived type, that
    /// is used to bind some item of <c>ItemsControl</c>-derived type. This annotation will
    /// enable the <c>DataContext</c> type resolve for XAML bindings for such properties.
    /// </summary>
    /// <remarks>
    /// Property should have the tree ancestor of the <c>ItemsControl</c> type or
    /// marked with the <see cref="XamlItemsControlAttribute"/> attribute.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class XamlItemBindingOfItemsControlAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AspChildControlTypeAttribute : Attribute
    {
        public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
        {
            TagName = tagName;
            ControlType = controlType;
        }

        [NotNull]
        public string TagName { get; private set; }
        [NotNull]
        public Type ControlType { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class AspDataFieldAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class AspDataFieldsAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AspMethodPropertyAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AspRequiredAttributeAttribute : Attribute
    {
        public AspRequiredAttributeAttribute([NotNull] string attribute)
        {
            Attribute = attribute;
        }

        [NotNull]
        public string Attribute { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AspTypePropertyAttribute : Attribute
    {
        public bool CreateConstructorReferences { get; private set; }

        public AspTypePropertyAttribute(bool createConstructorReferences)
        {
            CreateConstructorReferences = createConstructorReferences;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorImportNamespaceAttribute : Attribute
    {
        public RazorImportNamespaceAttribute([NotNull] string name)
        {
            Name = name;
        }

        [NotNull]
        public string Name { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorInjectionAttribute : Attribute
    {
        public RazorInjectionAttribute([NotNull] string type, [NotNull] string fieldName)
        {
            Type = type;
            FieldName = fieldName;
        }

        [NotNull]
        public string Type { get; private set; }
        [NotNull]
        public string FieldName { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorDirectiveAttribute : Attribute
    {
        public RazorDirectiveAttribute([NotNull] string directive)
        {
            Directive = directive;
        }

        [NotNull]
        public string Directive { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RazorHelperCommonAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RazorLayoutAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RazorWriteLiteralMethodAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RazorWriteMethodAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class RazorWriteMethodParameterAttribute : Attribute { }

    /// <summary>
    /// Prevents the Member Reordering feature from tossing members of the marked class.
    /// </summary>
    /// <remarks>
    /// The attribute must be mentioned in your member reordering patterns
    /// </remarks>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class NoReorder : Attribute { }
}


#endregion
#region AutowiringStrategies

namespace IoC
{
    using System;
    using Core;

    /// <summary>
    /// Provides autowiring strategies.
    /// </summary>
    public static class AutowiringStrategies
    {
        /// <summary>
        /// Creates aspect oriented autowiring strategy.
        /// </summary>
        /// <returns>The instance of aspect oriented autowiring strategy.</returns>
        public static IAutowiringStrategy AspectOriented() => AspectOrientedMetadata.Empty;

        /// <summary>
        /// Specifies type selector for aspect oriented autowiring strategy.
        /// </summary>
        /// <typeparam name="TTypeAttribute">The type metadata attribute.</typeparam>
        /// <param name="strategy">The base aspect oriented autowiring strategy.</param>
        /// <param name="typeSelector">The type selector.</param>
        /// <returns>The instance of aspect oriented autowiring strategy.</returns>
        public static IAutowiringStrategy Type<TTypeAttribute>(this IAutowiringStrategy strategy, [NotNull] Func<TTypeAttribute, Type> typeSelector)
            where TTypeAttribute : Attribute =>
            AspectOrientedMetadata.Type(
                GuardAspectOrientedMetadata(strategy ?? throw new ArgumentNullException(nameof(strategy))),
                typeSelector ?? throw new ArgumentNullException(nameof(typeSelector)));

        /// <summary>
        /// Specifies order selector for aspect oriented autowiring strategy.
        /// </summary>
        /// <typeparam name="TOrderAttribute">The order metadata attribute.</typeparam>
        /// <param name="strategy">The base aspect oriented autowiring strategy.</param>
        /// <param name="orderSelector">The type selector.</param>
        /// <returns>The instance of aspect oriented autowiring strategy.</returns>
        public static IAutowiringStrategy Order<TOrderAttribute>(this IAutowiringStrategy strategy, [NotNull] Func<TOrderAttribute, IComparable> orderSelector)
            where TOrderAttribute : Attribute =>
            AspectOrientedMetadata.Order(
                GuardAspectOrientedMetadata(strategy ?? throw new ArgumentNullException(nameof(strategy))),
                orderSelector ?? throw new ArgumentNullException(nameof(orderSelector)));

        /// <summary>
        /// Specifies tag selector for aspect oriented autowiring strategy.
        /// </summary>
        /// <typeparam name="TTagAttribute">The tag metadata attribute.</typeparam>
        /// <param name="strategy">The base aspect oriented autowiring strategy.</param>
        /// <param name="tagSelector">The tag selector.</param>
        /// <returns>The instance of aspect oriented autowiring strategy.</returns>
        public static IAutowiringStrategy Tag<TTagAttribute>(this IAutowiringStrategy strategy, [NotNull] Func<TTagAttribute, object> tagSelector)
            where TTagAttribute : Attribute =>
            AspectOrientedMetadata.Tag(
                GuardAspectOrientedMetadata(strategy ?? throw new ArgumentNullException(nameof(strategy))),
                tagSelector ?? throw new ArgumentNullException(nameof(tagSelector)));

        private static AspectOrientedMetadata GuardAspectOrientedMetadata([NotNull] IAutowiringStrategy strategy)
        {
            switch (strategy)
            {
                case AspectOrientedMetadata aspectOrientedMetadata:
                    return aspectOrientedMetadata;
                default:
                    throw new ArgumentException($"{nameof(strategy)} should be an aspect oriented autowiring strategy.", nameof(strategy));
            }
        }
    }
}

#endregion
#region Container

namespace IoC
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Core;
    using Features;
    using static Key;
    using FullKey = Key;
    using ShortKey = System.Type;
    using ResolverDelegate = System.Delegate;

    /// <summary>
    /// The IoC container implementation.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("Name = {" + nameof(ToString) + "()}")]
    [DebuggerTypeProxy(typeof(ContainerDebugView))]
    public sealed class Container: IContainer
    {
        private static long _containerId;
        [NotNull] private static readonly Lazy<Container> CoreRootContainer = new Lazy<Container>(() => CreateRootContainer(Feature.CoreSet), true);
        [NotNull] private static readonly Lazy<Container> DefaultRootContainer = new Lazy<Container>(() => CreateRootContainer(Feature.DefaultSet), true);
        [NotNull] private static readonly Lazy<Container> LightRootContainer = new Lazy<Container>(() => CreateRootContainer(Feature.LightSet), true);

        [NotNull] private readonly IContainer _parent;
        [NotNull] private readonly string _name;
        [NotNull] private readonly ILockObject _lockObject;
        [NotNull] private readonly Subject<ContainerEvent> _eventSubject;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [NotNull] private Table<FullKey, DependencyEntry> _dependencies = Table<FullKey, DependencyEntry>.Empty;
        [NotNull] private Table<ShortKey, DependencyEntry> _dependenciesForTagAny = Table<ShortKey, DependencyEntry>.Empty;
        [NotNull] internal volatile Table<FullKey, ResolverDelegate> Resolvers = Table<FullKey, ResolverDelegate>.Empty;
        [NotNull] internal volatile Table<ShortKey, ResolverDelegate> ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
        private readonly RegistrationTracker _registrationTracker;

        /// <summary>
        /// Creates a root container with default features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The root container.</returns>
        [NotNull]
        public static Container Create([NotNull] string name = "") => 
            Create(name ?? throw new ArgumentNullException(nameof(name)), DefaultRootContainer.Value);

        /// <summary>
        /// Creates a root container with minimal set of features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The root container.</returns>
        [NotNull]
        public static Container CreateCore([NotNull] string name = "") => 
            Create(name ?? throw new ArgumentNullException(nameof(name)), CoreRootContainer.Value);

        /// <summary>
        /// Creates a root container with minimalist default features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The root container.</returns>
        [NotNull]
        public static Container CreateLight([NotNull] string name = "") => 
            Create(name ?? throw new ArgumentNullException(nameof(name)), LightRootContainer.Value);

        [NotNull]
        private static Container Create([NotNull] string name, [NotNull] IContainer parentContainer)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (parentContainer == null) throw new ArgumentNullException(nameof(parentContainer));
            return new Container(CreateContainerName(name), parentContainer, parentContainer.Resolve<ILockObject>());
        }

        private static Container CreateRootContainer([NotNull][ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            var lockObject = new LockObject();
            var container = new Container(string.Empty, NullContainer.Shared, lockObject);
            container.ApplyConfigurations(configurations);
            container.Register<ILockObject>(ctx => lockObject);
            return container;
        }

        internal Container([NotNull] string name, [NotNull] IContainer parent, [NotNull] ILockObject lockObject)
        {
            _lockObject = lockObject;
            _name = $"{parent}/{name ?? throw new ArgumentNullException(nameof(name))}";
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _eventSubject = new Subject<ContainerEvent>(_lockObject);
            _registrationTracker = new RegistrationTracker(this);

            // Subscribe to events from the parent container
            RegisterResource(_parent.Subscribe(_eventSubject));

            // Creates a subscription to track infrastructure registrations
            RegisterResource(_eventSubject.Subscribe(_registrationTracker));

            // Register the current container in the parent container
            _parent.RegisterResource(this);

            // Notifies parent container about the child container creation
            (_parent as Container)?._eventSubject.OnNext(new ContainerEvent(this, EventType.CreateContainer));

            // Notifies about existing registrations in parent containers
            _eventSubject.OnNext(new ContainerEvent(_parent, EventType.RegisterDependency) { Keys = _parent.SelectMany(i => i) });
        }

        /// <inheritdoc />
        public IContainer Parent => _parent;

        /// <inheritdoc />
        public override string ToString() => _name;

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryRegisterDependency(IEnumerable<FullKey> keys, IDependency dependency, ILifetime lifetime, out IToken dependencyToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var isRegistered = true;
            var registeredKeys = new List<FullKey>();
            var dependencyEntry = new DependencyEntry(_lockObject, this, dependency, lifetime, Disposable.Create(UnregisterKeys), registeredKeys);

            void UnregisterKeys()
            {
                lock (_lockObject)
                {
                    foreach (var curKey in registeredKeys)
                    {
                        if (curKey.Tag == AnyTag)
                        {
                            TryUnregister(curKey.Type, ref _dependenciesForTagAny);
                        }
                        else
                        {
                            TryUnregister(curKey, ref _dependencies);
                        }
                    }

                    _eventSubject.OnNext(new ContainerEvent(this, EventType.UnregisterDependency) { Keys = registeredKeys, Dependency = dependency, Lifetime = lifetime });
                }
            }

            try
            {
                lock (_lockObject)
                {
                    var dependenciesForTagAny = _dependenciesForTagAny;
                    var dependencies = _dependencies;

                    foreach (var curKey in keys)
                    {
                        var type = curKey.Type.ToGenericType();
                        var key = type != curKey.Type ? new FullKey(type, curKey.Tag) : curKey;

                        if (key.Tag == AnyTag)
                        {
                            var hashCode = key.Type.GetHashCode();
                            isRegistered &= dependenciesForTagAny.GetByRef(hashCode, key.Type) == default(DependencyEntry);
                            if (isRegistered)
                            {
                                dependenciesForTagAny = dependenciesForTagAny.Set(hashCode, key.Type, dependencyEntry);
                            }
                        }
                        else
                        {
                            var hashCode = key.GetHashCode();
                            isRegistered &= dependencies.Get(hashCode, key) == default(DependencyEntry);
                            if (isRegistered)
                            {
                                dependencies = dependencies.Set(hashCode, key, dependencyEntry);
                            }
                        }

                        if (!isRegistered)
                        {
                            break;
                        }

                        registeredKeys.Add(key);
                    }

                    if (isRegistered)
                    {
                        _dependenciesForTagAny = dependenciesForTagAny;
                        _dependencies = dependencies;
                        _eventSubject.OnNext(new ContainerEvent(this, EventType.RegisterDependency) { Keys = registeredKeys, Dependency = dependency, Lifetime = lifetime });
                    }
                }
            }
            catch (Exception ex)
            {
                _eventSubject.OnNext(new ContainerEvent(this, EventType.RegisterDependency) { Keys = registeredKeys, Dependency = dependency, Lifetime = lifetime, Error = ex, IsSuccess = false });
                isRegistered = false;
                throw;
            }
            finally
            {
                if (isRegistered)
                {
                    dependencyToken = dependencyEntry;
                }
                else
                {
                    dependencyEntry.Dispose();
                    dependencyToken = default(IToken);
                }
            }

            return isRegistered;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public bool TryGetResolver<T>(ShortKey type, object tag, out Resolver<T> resolver, out Exception error, IContainer resolvingContainer = null)
        {
            FullKey key;
            int hashCode;
            if (tag == null)
            {
                hashCode = type.GetHashCode();
                resolver = (Resolver<T>)ResolversByType.GetByRef(hashCode, type);
                if (resolver != default(Resolver<T>)) // found in resolvers by type
                {
                    error = default(Exception);
                    return true;
                }

                key = new FullKey(type);
            }
            else
            {
                key = new FullKey(type, tag);
                hashCode = key.GetHashCode();
                resolver = (Resolver<T>)Resolvers.Get(hashCode, key);
                if (resolver != default(Resolver<T>)) // found in resolvers
                {
                    error = default(Exception);
                    return true;
                }
            }

            // tries finding in dependencies
            bool hasDependency;
            lock (_lockObject)
            {
                hasDependency = TryGetDependency(key, hashCode, out var dependencyEntry);
                if (hasDependency)
                {
                    // tries creating resolver
                    resolvingContainer = resolvingContainer ?? this;
                    resolvingContainer = dependencyEntry.Lifetime?.SelectResolvingContainer(this, resolvingContainer) ?? resolvingContainer;
                    if (!dependencyEntry.TryCreateResolver(
                        key,
                        resolvingContainer,
                        _registrationTracker,
                        _eventSubject,
                        out var resolverDelegate,
                        out error))
                    {
                        resolver = default(Resolver<T>);
                        return false;
                    }

                    resolver = (Resolver<T>) resolverDelegate;
                }
            }

            if (!hasDependency)
            {
                // tries finding in parent
                if (!_parent.TryGetResolver(type, tag, out resolver, out error, resolvingContainer ?? this))
                {
                    resolver = default(Resolver<T>);
                    return false;
                }
            }

            // If it is resolving container only
            if (resolvingContainer == null || resolvingContainer == this)
            {
                // Add resolver to tables
                lock (_lockObject)
                {
                    Resolvers = Resolvers.Set(hashCode, key, resolver);
                    if (tag == null)
                    {
                        ResolversByType = ResolversByType.Set(hashCode, type, resolver);
                    }
                }
            }

            error = default(Exception);
            return true;
        }

        /// <inheritdoc />
        public bool TryGetDependency(FullKey key, out IDependency dependency, out ILifetime lifetime)
        {
            if (!TryGetDependency(key, key.GetHashCode(), out var dependencyEntry))
            {
                return _parent.TryGetDependency(key, out dependency, out lifetime);
            }

            dependency = dependencyEntry.Dependency;
            lifetime = dependencyEntry.GetLifetime(key.Type);
            return true;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // Notifies parent container about the child container disposing
            (_parent as Container)?._eventSubject.OnNext(new ContainerEvent(this, EventType.DisposeContainer));

            _parent.UnregisterResource(this);
            List<IDisposable> entriesToDispose;
            lock (_lockObject)
            {
                entriesToDispose = new List<IDisposable>(_dependencies.Count + _dependenciesForTagAny.Count + _resources.Count);
                entriesToDispose.AddRange(_dependencies.Select(i => i.Value));
                entriesToDispose.AddRange(_dependenciesForTagAny.Select(i => i.Value));
                entriesToDispose.AddRange(_resources);
                _dependencies = Table<FullKey, DependencyEntry>.Empty;
                _dependenciesForTagAny = Table<ShortKey, DependencyEntry>.Empty;
                Reset();
                _resources.Clear();
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < entriesToDispose.Count; index++)
            {
                entriesToDispose[index].Dispose();
            }

            _eventSubject.OnCompleted();
        }

        /// <inheritdoc />
        public void RegisterResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_lockObject)
            {
                _resources.Add(resource);
            }
        }

        /// <inheritdoc />
        public void UnregisterResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_lockObject)
            {
                _resources.Remove(resource);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <inheritdoc />
        public IEnumerator<IEnumerable<FullKey>> GetEnumerator() =>
            GetAllKeys().Concat(_parent).GetEnumerator();

        /// <inheritdoc />
        public IDisposable Subscribe(IObserver<ContainerEvent> observer) =>
            _eventSubject.Subscribe(observer ?? throw new ArgumentNullException(nameof(observer)));

        internal void Reset()
        {
            lock (_lockObject)
            {
                Resolvers = Table<FullKey, ResolverDelegate>.Empty;
                ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
            }
        }

        [MethodImpl((MethodImplOptions) 256)]
        private IEnumerable<IEnumerable<FullKey>> GetAllKeys()
        {
            lock (_lockObject)
            {
                return _dependencies.Select(i => i.Value.Keys).Concat(_dependenciesForTagAny.Select(i => i.Value.Keys)).Distinct();
            }
        }

        [MethodImpl((MethodImplOptions) 256)]
        private bool TryUnregister<TKey>(TKey key, [NotNull] ref Table<TKey, DependencyEntry> entries)
        {
            entries = entries.Remove(key.GetHashCode(), key, out var unregistered);
            if (!unregistered)
            {
                return false;
            }

            return true;
        }

        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        internal static string CreateContainerName([CanBeNull] string name = "") =>
            !string.IsNullOrWhiteSpace(name) ? name : Interlocked.Increment(ref _containerId).ToString(CultureInfo.InvariantCulture);

        [MethodImpl((MethodImplOptions) 256)]
        private void ApplyConfigurations(IEnumerable<IConfiguration> configurations) =>
            _resources.Add(this.Apply(configurations));

        [MethodImpl((MethodImplOptions)256)]
        private bool TryGetDependency(FullKey key, int hashCode, out DependencyEntry dependencyEntry)
        {
            lock (_lockObject)
            {
                dependencyEntry = _dependencies.Get(hashCode, key);
                if (dependencyEntry != default(DependencyEntry))
                {
                    return true;
                }

                var type = key.Type;
                var typeDescriptor = type.Descriptor();

                // Generic type
                if (typeDescriptor.IsConstructedGenericType())
                {
                    var genericType = typeDescriptor.GetGenericTypeDefinition();
                    var genericKey = new FullKey(genericType, key.Tag);
                    // For generic type
                    dependencyEntry = _dependencies.Get(genericKey.GetHashCode(), genericKey);
                    if (dependencyEntry != default(DependencyEntry))
                    {
                        return true;
                    }

                    // For generic type and Any tag
                    dependencyEntry = _dependenciesForTagAny.GetByRef(genericType.GetHashCode(), genericType);
                    if (dependencyEntry != default(DependencyEntry))
                    {
                        return true;
                    }
                }

                // For Any tag
                dependencyEntry = _dependenciesForTagAny.GetByRef(type.GetHashCode(), type);
                if (dependencyEntry != default(DependencyEntry))
                {
                    return true;
                }

                // For array
                if (typeDescriptor.IsArray())
                {
                    var arrayKey = new FullKey(typeof(IArray), key.Tag);
                    // For generic type
                    dependencyEntry = _dependencies.Get(arrayKey.GetHashCode(), arrayKey);
                    if (dependencyEntry != default(DependencyEntry))
                    {
                        return true;
                    }

                    // For generic type and Any tag
                    dependencyEntry = _dependenciesForTagAny.GetByRef(typeof(IArray).GetHashCode(), typeof(IArray));
                    if (dependencyEntry != default(DependencyEntry))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private class ContainerDebugView
        {
            private readonly Container _container;

            public ContainerDebugView([NotNull] Container container) =>
                _container = container ?? throw new ArgumentNullException(nameof(container));

            [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
            public FullKey[] Keys => _container.GetAllKeys().SelectMany(i => i).ToArray();

            public IContainer Parent => _container.Parent is NullContainer ? null : _container.Parent;

            public int ResolversCount => _container.Resolvers.Count + _container.ResolversByType.Count;

            public int ResourcesCount => _container._resources.Count;
        }
    }
}


#endregion
#region ContainerEvent

namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Provides information about changes in the container.
    /// </summary>
    [PublicAPI]
    public struct ContainerEvent
    {
        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] public readonly IContainer Container;

        /// <summary>
        /// The type of event.
        /// </summary>
        public readonly EventType EventType;

        /// <summary>
        /// True if it is success.
        /// </summary>
        public bool IsSuccess;

        /// <summary>
        /// Error during operation.
        /// </summary>
        public Exception Error;

        /// <summary>
        /// The changed keys.
        /// </summary>
        [CanBeNull] public IEnumerable<Key> Keys;

        /// <summary>
        /// Related dependency.
        /// </summary>
        [CanBeNull] public IDependency Dependency;

        /// <summary>
        /// Related lifetime.
        /// </summary>
        [CanBeNull] public ILifetime Lifetime;

        /// <summary>
        /// Related lifetime.
        /// </summary>
        [CanBeNull] public LambdaExpression ResolverExpression;

        /// <summary>
        /// Create new instance of container event.
        /// </summary>
        /// <param name="container">The origin container.</param>
        /// <param name="eventType">The event type.</param>
        public ContainerEvent([NotNull] IContainer container, EventType eventType)
        {
            Container = container;
            EventType = eventType;
            IsSuccess = true;
            Error = default(Exception);
            Keys = default(IEnumerable<Key>);
            Dependency = default(IDependency);
            Lifetime = default(ILifetime);
            ResolverExpression = default(LambdaExpression);
        }
    }
}


#endregion
#region Context'1

namespace IoC
{
    /// <summary>
    /// Represent the resolving context with an instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    public sealed class Context<T>: Context
    {
        /// <summary>
        /// The resolved instance.
        /// </summary>
        [NotNull] public readonly T It;

        internal Context(
            T it,
            Key key,
            [NotNull] IContainer container,
            [NotNull] [ItemCanBeNull] params object[] args)
            : base(key, container, args) =>
            It = it;
    }
}


#endregion
#region Context

namespace IoC
{
    /// <summary>
    /// Represents the resolving context.
    /// </summary>
    [PublicAPI]
    public class Context
    {
        /// <summary>
        /// The resolving key.
        /// </summary>
        public readonly Key Key;

        /// <summary>
        /// The resolving container.
        /// </summary>
        [NotNull] public readonly IContainer Container;

        /// <summary>
        /// The optional resolving arguments.
        /// </summary>
        [NotNull][ItemCanBeNull] public readonly object[] Args;

        internal Context(
            Key key,
            [NotNull] IContainer container,
            [NotNull][ItemCanBeNull] params object[] args)
        {
            Key = key;
            Container = container;
            Args = args;
        }
    }
}


#endregion
#region EventType

namespace IoC
{
    /// <summary>
    /// The types of event.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// On container creation.
        /// </summary>
        CreateContainer,

        /// <summary>
        /// On container dispose.
        /// </summary>
        DisposeContainer,

        /// <summary>
        /// On dependency registration.
        /// </summary>
        RegisterDependency,

        /// <summary>
        /// On dependency unregistration.
        /// </summary>
        UnregisterDependency,

        /// <summary>
        /// On resolver compilation
        /// </summary>
        ResolverCompilation
    }
}

#endregion
#region Fluent

namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Extension methods for IoC containers and configurations.
    /// </summary>
    [PublicAPI]
    public static class Fluent
    {
        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="parentContainer">The parent container.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Create([NotNull] this IContainer parentContainer, [NotNull] string name = "")
        {
            if (parentContainer == null) throw new ArgumentNullException(nameof(parentContainer));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return parentContainer.GetResolver<IContainer>(WellknownContainers.NewChild.AsTag())(parentContainer, name);
        }

        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="token">The parent container token.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Create([NotNull] this IToken token, [NotNull] string name = "")
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return token.Container.GetResolver<IContainer>(WellknownContainers.NewChild.AsTag())(token.Container, name);
        }

        /// <summary>
        /// Buildups an instance which was not registered in container. Can be used as entry point of DI.
        /// </summary>
        /// <param name="configuration">The configurations.</param>
        /// <param name="args">The optional arguments.</param>
        /// <typeparam name="TInstance">The instance type.</typeparam>
        /// <returns>The disposable instance holder.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IHolder<TInstance> BuildUp<TInstance>([NotNull] this IConfiguration configuration, [NotNull] [ItemCanBeNull] params object[] args)
            where TInstance : class
            => Container.Create().Using(configuration ?? throw new ArgumentNullException(nameof(configuration))).BuildUp<TInstance>(args ?? throw new ArgumentNullException(nameof(args)));

        /// <summary>
        /// Buildups an instance.
        /// Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.
        /// </summary>
        /// <typeparam name="TInstance">The instance type.</typeparam>
        /// <param name="token">The target container token.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The disposable instance holder.</returns>
        [NotNull]
        public static IHolder<TInstance> BuildUp<TInstance>([NotNull] this IToken token, [NotNull] [ItemCanBeNull] params object[] args)
            where TInstance : class =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.BuildUp<TInstance>(args ?? throw new ArgumentNullException(nameof(args)));

        /// <summary>
        /// Buildups an instance.
        /// Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.
        /// </summary>
        /// <typeparam name="TInstance">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The disposable instance holder.</returns>
        [NotNull]
        public static IHolder<TInstance> BuildUp<TInstance>([NotNull] this IContainer container, [NotNull] [ItemCanBeNull] params object[] args)
            where TInstance : class
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (container.TryGetResolver<TInstance>(typeof(TInstance), null, out var resolver, out _, container))
            {
                return new Holder<TInstance>(new Token(container, Disposable.Empty), resolver(container, args));
            }

            var buildId = Guid.NewGuid();
            var token = container.Bind<TInstance>().Tag(buildId).To();
            try
            {
                var instance = container.Resolve<TInstance>(buildId.AsTag(), args);
                return new Holder<TInstance>(token, instance);
            }
            catch
            {
                token.Dispose();
                throw;
            }
        }
    }
}

#endregion
#region FluentAutowiring

namespace IoC
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;

    /// <summary>
    /// Represents extensions for autowiring.
    /// </summary>
    public static class FluentAutowiring
    {
        /// <summary>
        /// Injects dependency to parameter.
        /// </summary>
        /// <typeparam name="TMethodInfo"></typeparam>
        /// <param name="method">The target method or constructor.</param>
        /// <param name="parameterPosition">The parameter's position.</param>
        /// <param name="dependencyType">The dependency's type.</param>
        /// <param name="dependencyTag">The optional dependency's tag value.</param>
        /// <returns>True if success.</returns>
        public static bool TryInjectDependency<TMethodInfo>([NotNull] this IMethod<TMethodInfo> method, int parameterPosition, [NotNull] Type dependencyType, [CanBeNull] object dependencyTag = null)
            where TMethodInfo: MethodBase
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (dependencyType == null) throw new ArgumentNullException(nameof(dependencyType));
            if (parameterPosition < 0) throw new ArgumentOutOfRangeException(nameof(parameterPosition));
            var containerExpression = Expression.Field(Expression.Constant(null, TypeDescriptor<Context>.Type), nameof(Context.Container));
            var parameterExpression = Expression.Call(Injections.InjectWithTagMethodInfo, containerExpression, Expression.Constant(dependencyType), Expression.Constant(dependencyTag)).Convert(dependencyType);
            method.SetParameterExpression(parameterPosition, parameterExpression);
            return true;
        }
    }
}


#endregion
#region FluentBind

namespace IoC
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Linq.Expressions;
    using Core;
    using Issues;

    /// <summary>
    /// Represents extensions to add bindings to the container.
    /// </summary>
    [PublicAPI]
    public static partial class FluentBind
    {
        /// <summary>
        /// Binds the type(s).
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="types"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<object> Bind([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Type[] types)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (types.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(types));
            return new Binding<object>(container, types);
        }

        /// <summary>
        /// Binds the type(s).
        /// </summary>
        /// <param name="token">The container binding token.</param>
        /// <param name="types"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<object> Bind([NotNull] this IToken token, [NotNull] [ItemNotNull] params Type[] types)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (types.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(types));
            return new Binding<object>(token, types);
        }

        /// <summary>
        /// Binds the type.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type);
        }

        /// <summary>
        /// Binds the type.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="token">The container binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IToken token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type);
        }

        /// <summary>
        /// Assigns well-known lifetime to the binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="lifetime"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> As<T>([NotNull] this IBinding<T> binding, Lifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, lifetime);
        }

        /// <summary>
        /// Assigns the lifetime to the binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="lifetime"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Lifetime<T>([NotNull] this IBinding<T> binding, [NotNull] ILifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            return new Binding<T>(binding, lifetime);
        }

        /// <summary>
        /// Assigns the autowiring strategy to the binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="autowiringStrategy"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Autowiring<T>([NotNull] this IBinding<T> binding, [NotNull] IAutowiringStrategy autowiringStrategy)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (autowiringStrategy == null) throw new ArgumentNullException(nameof(autowiringStrategy));
            return new Binding<T>(binding, autowiringStrategy);
        }

        /// <summary>
        /// Marks the binding by the tag. Is it possible to use multiple times.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="tagValue"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Tag<T>([NotNull] this IBinding<T> binding, [CanBeNull] object tagValue = null)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, tagValue);
        }

        /// <summary>
        /// Marks the binding to be used for any tags.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> AnyTag<T>([NotNull] this IBinding<T> binding)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return binding.Tag(Key.AnyTag);
        }

        /// <summary>
        /// Registers autowiring binding.
        /// </summary>
        /// <param name="binding">The binding token.</param>
        /// <param name="type">The instance type.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken To(
            [NotNull] this IBinding<object> binding,
            [NotNull] Type type,
            [NotNull][ItemNotNull] params Expression<Action<Context<object>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return CreateDependencyToken(binding, CreateDependency(binding, new FullAutowiringDependency(type, binding.AutowiringStrategy, statements)));
        }

        /// <summary>
        /// Registers autowiring binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull][ItemNotNull] params Expression<Action<Context<T>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return CreateDependencyToken(binding, CreateDependency(binding, new FullAutowiringDependency(TypeDescriptor<T>.Type, binding.AutowiringStrategy, statements)));
        }

        /// <summary>
        /// Registers autowiring binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull] Expression<Func<Context, T>> factory,
            [NotNull][ItemNotNull] params Expression<Action<Context<T>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return CreateDependencyToken(binding, CreateDependency(binding, new AutowiringDependency(factory, binding.AutowiringStrategy, statements)));
        }

        [NotNull]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private static IDisposable CreateDependency<T>([NotNull] this IBinding<T> binding, [NotNull] IDependency dependency)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));

            var tags = binding.Tags.DefaultIfEmpty(null);
            var keys =
                from type in binding.Types
                from tag in tags
                select new Key(type, tag);

            return binding.Container.TryRegisterDependency(keys, dependency, binding.Lifetime, out var dependencyToken)
                ? dependencyToken
                : binding.Container.Resolve<ICannotRegister>().Resolve(binding.Container, keys.ToArray());
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        private static IToken CreateDependencyToken<T>(IBinding<T> binding, IDisposable dependency)
        {
            var tokens = binding.Tokens.ToList();
            tokens.Add(dependency.AsTokenOf(binding.Container));
            return Disposable.Create(tokens).AsTokenOf(binding.Container);
        }
    }
}

#endregion
#region FluentBindGenerated

namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Represents extensions to add bindings to the container.
    /// </summary>
    public static partial class FluentBind
    {
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IContainer container)
            where T: T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IToken token)
            where T: T1
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IContainer container)
            where T: T1, T2
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IToken token)
            where T: T1, T2
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IContainer container)
            where T: T1, T2, T3
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IToken token)
            where T: T1, T2, T3
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IToken token)
            where T: T1, T2, T3, T4
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <typeparam name="T31">The contract type #31.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type, TypeDescriptor<T31>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <typeparam name="T31">The contract type #31.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type, TypeDescriptor<T31>.Type);
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <typeparam name="T31">The contract type #31.</typeparam>
        /// <typeparam name="T32">The contract type #32.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>([NotNull] this IContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type, TypeDescriptor<T31>.Type, TypeDescriptor<T32>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <typeparam name="T8">The contract type #8.</typeparam>
        /// <typeparam name="T9">The contract type #9.</typeparam>
        /// <typeparam name="T10">The contract type #10.</typeparam>
        /// <typeparam name="T11">The contract type #11.</typeparam>
        /// <typeparam name="T12">The contract type #12.</typeparam>
        /// <typeparam name="T13">The contract type #13.</typeparam>
        /// <typeparam name="T14">The contract type #14.</typeparam>
        /// <typeparam name="T15">The contract type #15.</typeparam>
        /// <typeparam name="T16">The contract type #16.</typeparam>
        /// <typeparam name="T17">The contract type #17.</typeparam>
        /// <typeparam name="T18">The contract type #18.</typeparam>
        /// <typeparam name="T19">The contract type #19.</typeparam>
        /// <typeparam name="T20">The contract type #20.</typeparam>
        /// <typeparam name="T21">The contract type #21.</typeparam>
        /// <typeparam name="T22">The contract type #22.</typeparam>
        /// <typeparam name="T23">The contract type #23.</typeparam>
        /// <typeparam name="T24">The contract type #24.</typeparam>
        /// <typeparam name="T25">The contract type #25.</typeparam>
        /// <typeparam name="T26">The contract type #26.</typeparam>
        /// <typeparam name="T27">The contract type #27.</typeparam>
        /// <typeparam name="T28">The contract type #28.</typeparam>
        /// <typeparam name="T29">The contract type #29.</typeparam>
        /// <typeparam name="T30">The contract type #30.</typeparam>
        /// <typeparam name="T31">The contract type #31.</typeparam>
        /// <typeparam name="T32">The contract type #32.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type, TypeDescriptor<T9>.Type, TypeDescriptor<T10>.Type, TypeDescriptor<T11>.Type, TypeDescriptor<T12>.Type, TypeDescriptor<T13>.Type, TypeDescriptor<T14>.Type, TypeDescriptor<T15>.Type, TypeDescriptor<T16>.Type, TypeDescriptor<T17>.Type, TypeDescriptor<T18>.Type, TypeDescriptor<T19>.Type, TypeDescriptor<T20>.Type, TypeDescriptor<T21>.Type, TypeDescriptor<T22>.Type, TypeDescriptor<T23>.Type, TypeDescriptor<T24>.Type, TypeDescriptor<T25>.Type, TypeDescriptor<T26>.Type, TypeDescriptor<T27>.Type, TypeDescriptor<T28>.Type, TypeDescriptor<T29>.Type, TypeDescriptor<T30>.Type, TypeDescriptor<T31>.Type, TypeDescriptor<T32>.Type);
        }
    }
}

#endregion
#region FluentConfiguration

namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Represents extensions to configure a container.
    /// </summary>
    [PublicAPI]
    public static class FluentConfiguration
    {
        /// <summary>
        /// Creates configuration from factory.
        /// </summary>
        /// <param name="configurationFactory">The configuration factory.</param>
        /// <returns>The configuration instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IConfiguration Create([NotNull] Func<IContainer, IToken> configurationFactory) =>
            new ConfigurationFromDelegate(configurationFactory ?? throw new ArgumentNullException(nameof(configurationFactory)));

        /// <summary>
        /// Converts a disposable resource to the container's token.
        /// </summary>
        /// <param name="disposableToken">A disposable resource.</param>
        /// <param name="container">The target container.</param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken AsTokenOf([NotNull] this IDisposable disposableToken, [NotNull] IContainer container) =>
            new Token(container ?? throw new ArgumentNullException(nameof(container)), disposableToken ?? throw new ArgumentNullException(nameof(disposableToken)));

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.ApplyConfigurationFromData(configurationText);
        }

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params string[] configurationText) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurationText);

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.ApplyConfigurationFromData(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params Stream[] configurationStreams) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurationStreams);

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.ApplyConfigurationFromData(configurationReaders);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params TextReader[] configurationReaders) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurationReaders);

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            return Disposable.Create(configurations.Select(i => i.Apply(container)).SelectMany(i => i).ToArray()).AsTokenOf(container);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] IEnumerable<IConfiguration> configurations) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurations);

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            return container.Apply((IEnumerable<IConfiguration>) configurations);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params IConfiguration[] configurations) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurations);

        /// <summary>
        /// Applies a configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The target container token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply<T>([NotNull] this IContainer container)
            where T : IConfiguration, new() =>
            (container ?? throw new ArgumentNullException(nameof(container))).Apply(new T());

        /// <summary>
        /// Applies a configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="token">The target container token.</param>
        /// <returns>The target container token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply<T>([NotNull] this IToken token)
            where T : IConfiguration, new() =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply<T>();

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            container.RegisterResource(container.Apply(configurations));
            return container;
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using([NotNull] this IToken token, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Using(configurations);
        }

        /// <summary>
        /// Uses a configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using<T>([NotNull] this IContainer container)
            where T : IConfiguration, new()
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Using(new T());
        }

        /// <summary>
        /// Uses a configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="token">The target container token.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using<T>([NotNull] this IToken token)
            where T : IConfiguration, new()
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Using<T>();
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        private static IToken ApplyConfigurationFromData<T>([NotNull] this IContainer container, [NotNull] [ItemNotNull] params T[] configurationData)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationData == null) throw new ArgumentNullException(nameof(configurationData));
            if (configurationData.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationData));
            return container.Apply(configurationData.Select(configurationItem => container.Resolve<IConfiguration>(configurationItem)).ToArray());
        }
    }
}

#endregion
#region FluentGetResolver

namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;
    using Issues;

    /// <summary>
    /// Represents extensions to get a resolver from the container.
    /// </summary>
    [PublicAPI]
    public static class FluentGetResolver
    {
        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag)
            => container.TryGetResolver<T>(type, tag.Value, out var resolver, out var error) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type, tag), error);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container"></param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(type, tag.Value, out resolver, out _);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, Tag tag)
            => container.GetResolver<T>(TypeDescriptor<T>.Type, tag);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container">The target container.</param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, Tag tag, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(TypeDescriptor<T>.Type, tag, out resolver);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, [NotNull] Type type)
            => container.TryGetResolver<T>(type, null, out var resolver, out var error) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type), error);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="container">The target container.</param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, [NotNull] Type type, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(type, null, out resolver, out _);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container)
            => container.GetResolver<T>(TypeDescriptor<T>.Type);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(TypeDescriptor<T>.Type, out resolver);

        /// <summary>
        /// Creates tag.
        /// </summary>
        /// <param name="tagValue">The tag value.</param>
        /// <returns>The tag.</returns>
        [MethodImpl((MethodImplOptions)256)]
        public static Tag AsTag([CanBeNull] this object tagValue) => new Tag(tagValue);
    }
}

#endregion
#region FluentNativeResolve

namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Represents extensions to resolve from the native container.
    /// </summary>
    [PublicAPI]
    public static class FluentNativeResolve
    {
        // ReSharper disable once RedundantNameQualifier
        private static readonly object[] EmptyArgs = Core.CoreExtensions.EmptyArray<object>();

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container)
        {
            return ((Resolver<T>)container.ResolversByType.GetByRef(TypeDescriptor<T>.HashCode, TypeDescriptor<T>.Type)
                    ?? container.GetResolver<T>(TypeDescriptor<T>.Type))(container, EmptyArgs);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, Tag tag)
        {
            var key = new Key(TypeDescriptor<T>.Type, tag);
            return ((Resolver<T>)container.Resolvers.Get(key.GetHashCode(), key)
                    ?? container.GetResolver<T>(TypeDescriptor<T>.Type, tag))(container, EmptyArgs);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] [ItemCanBeNull] params object[] args)
        {
            return ((Resolver<T>)container.ResolversByType.GetByRef(TypeDescriptor<T>.HashCode, TypeDescriptor<T>.Type)
                    ?? container.GetResolver<T>(TypeDescriptor<T>.Type))(container, args);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, Tag tag, [NotNull] [ItemCanBeNull] params object[] args)
        {
            var key = new Key(TypeDescriptor<T>.Type, tag);
            return ((Resolver<T>)container.Resolvers.Get(key.GetHashCode(), key)
                    ?? container.GetResolver<T>(TypeDescriptor<T>.Type, tag))(container, args);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type)
        {
            return ((Resolver<T>)container.ResolversByType.GetByRef(type.GetHashCode(), type)
                    ?? container.GetResolver<T>(type))(container, EmptyArgs);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type, Tag tag)
        {
            var key = new Key(type, tag);
            return ((Resolver<T>)container.Resolvers.Get(key.GetHashCode(), key)
                    ?? container.GetResolver<T>(type, tag))(container, EmptyArgs);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static object Resolve<T>([NotNull] this Container container, [NotNull] Type type, [NotNull] [ItemCanBeNull] params object[] args)
        {
            return ((Resolver<T>)container.ResolversByType.GetByRef(type.GetHashCode(), type)
                    ?? container.GetResolver<T>(type))(container, args);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static object Resolve<T>([NotNull] this Container container, [NotNull] Type type, Tag tag, [NotNull] [ItemCanBeNull] params object[] args)
        {
            var key = new Key(type, tag);
            return ((Resolver<T>)container.Resolvers.Get(key.GetHashCode(), key)
                    ?? container.GetResolver<T>(type, tag))(container, args);
        }
    }
}

#endregion
#region FluentResolve

namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents extensions to resolve from the container.
    /// </summary>
    [PublicAPI]
    public static class FluentResolve
    {
        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull][ItemCanBeNull] params object[] args) 
            => container.GetResolver<T>()(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, Tag tag, [NotNull][ItemCanBeNull] params object[] args)
            => container.GetResolver<T>(tag)(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull] Type type, [NotNull][ItemCanBeNull] params object[] args) 
            => container.GetResolver<T>(type)(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag, [NotNull][ItemCanBeNull] params object[] args)
            => container.GetResolver<T>(type, tag)(container, args);
    }
}

#endregion
#region FluentScope

namespace IoC
{
    using System;

    /// <summary>
    /// Represents extensions dealing with scopes.
    /// </summary>
    public static class FluentScope
    {
        /// <summary>
        /// Creates new resolving scope. Can be used with <c>ScopeSingleton</c>.
        /// </summary>
        /// <param name="container">A container to resolve a scope.</param>
        /// <returns>Tne new scope instance.</returns>
        [NotNull]
        public static IScope CreateScope([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Resolve<IScope>();
        }
    }
}


#endregion
#region FluentTrace

namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Core;

    /// <summary>
    /// Represents extensions to trace the container.
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class FluentTrace
    {
        /// <summary>
        /// Gets container trace source.
        /// </summary>
        /// <param name="container">The target container to trace.</param>
        /// <returns>The race source.</returns>
        public static IObservable<TraceEvent> ToTraceSource([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            return Observable.Create<TraceEvent>(observer =>
            {
                IDictionary<IContainer, IDisposable> subscriptions = new Dictionary<IContainer, IDisposable>();
                Subscribe(container, subscriptions, observer, container.Resolve<IConverter<ContainerEvent, IContainer, string>>());
                return Disposable.Create(subscriptions.Values);
            });
        }

        /// <summary>
        /// Trace container action by handler.
        /// </summary>
        /// <param name="container">The target container to trace.</param>
        /// <param name="onTraceMessage">The trace handler.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IContainer container, [NotNull] Action<string> onTraceMessage)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (onTraceMessage == null) throw new ArgumentNullException(nameof(onTraceMessage));

            return new Token(
                container,
                container
                    .ToTraceSource()
                    .Subscribe(
                        value => onTraceMessage(value.Message),
                        error => { onTraceMessage($"The error is occured during tracing \"{error}\"."); },
                        () => { onTraceMessage("The tracing is completed."); }));
        }

        /// <summary>
        /// Trace container action by handler.
        /// </summary>
        /// <param name="token">The token of target container to trace.</param>
        /// <param name="onTraceMessage">The trace handler.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IToken token, [NotNull] Action<string> onTraceMessage) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Trace(onTraceMessage ?? throw new ArgumentNullException(nameof(onTraceMessage)));

#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETCOREAPP1_0&& !NETCOREAPP1_1 && !WINDOWS_UWP
        /// <summary>
        /// Trace container action by handler.
        /// </summary>
        /// <param name="container">The target container to trace.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IContainer container) =>
            (container ?? throw new ArgumentNullException(nameof(container))).Trace(message => System.Diagnostics.Trace.WriteLine(message));

        /// <summary>
        /// Trace container action by handler.
        /// </summary>
        /// <param name="token">The token of target container to trace.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IToken token) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Trace();
#endif

        private static void Subscribe(
            IContainer container,
            IDictionary<IContainer, IDisposable> subscriptions,
            IObserver<TraceEvent> observer,
            IConverter<ContainerEvent, IContainer, string> converter)
        {
            lock (subscriptions)
            {
                if (subscriptions.ContainsKey(container))
                {
                    return;
                }

                var subscription = container.Subscribe(
                    value =>
                    {
                        switch (value.EventType)
                        {
                            case EventType.CreateContainer:
                                if (value.Container.Parent != container) return;
                                Subscribe(value.Container, subscriptions, observer, converter);
                                break;

                            case EventType.DisposeContainer:
                                if (value.Container.Parent != container) return;
                                lock (subscriptions)
                                {
                                    if (subscriptions.TryGetValue(value.Container, out var subscriptionToDispose))
                                    {
                                        subscriptions.Remove(value.Container);
                                        subscriptionToDispose.Dispose();
                                    }
                                }

                                break;

                            default:
                                if (value.Container != container) return;
                                break;

                        }

                        if (!converter.TryConvert(value.Container, value, out var message))
                        {
                            message = value.ToString();
                        }

                        observer.OnNext(new TraceEvent(value, message));
                    },
                    observer.OnError,
                    observer.OnCompleted);

                subscriptions.Add(container, subscription);
            }
        }

        /// <summary>
        /// Represents a trace event.
        /// </summary>
        public struct TraceEvent
        {
            /// <summary>
            /// The origin container event.
            /// </summary>
            public readonly ContainerEvent ContainerEvent;
            /// <summary>
            /// The trace message.
            /// </summary>
            [NotNull] public readonly string Message;

            internal TraceEvent(ContainerEvent containerEvent, [NotNull] string message)
            {
                ContainerEvent = containerEvent;
                Message = message ?? throw new ArgumentNullException(nameof(message));
            }
        }
    }
}


#endregion
#region GenericTypeArgumentAttribute

namespace IoC
{
    using System;

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GenericTypeArgumentAttribute : Attribute { }
}


#endregion
#region GenericTypeArguments

// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace IoC
{
    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT1 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT2 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT3 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT4 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT5 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT6 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT7 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT8 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT9 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT10 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT11 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT12 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT13 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT14 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT15 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT16 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT17 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT18 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT19 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT20 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT21 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT22 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT23 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT24 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT25 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT26 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT27 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT28 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT29 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT30 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT31 { }

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TT32 { }

}


#endregion
#region IAutowiringStrategy

namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents an abstraction for an autowiring method.
    /// </summary>
    [PublicAPI]
    public interface IAutowiringStrategy
    {
        /// <summary>
        /// Resolves type to create an instance.
        /// </summary>
        /// <param name="registeredType">Registered type.</param>
        /// <param name="resolvingType">Resolving type.</param>
        /// <param name="instanceType">The type to create an instance.</param>
        /// <returns>True if the type was resolved.</returns>
        bool TryResolveType([NotNull] Type registeredType, [NotNull] Type resolvingType, out Type instanceType);

        /// <summary>
        /// Resolves a constructor from a set of available constructors.
        /// </summary>
        /// <param name="constructors">The set of available constructors.</param>
        /// <param name="constructor">The resolved constructor.</param>
        /// <returns>True if the constructor was resolved.</returns>
        bool TryResolveConstructor([NotNull][ItemNotNull] IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor);

        /// <summary>
        /// Resolves initializing methods from a set of available methods/setters in the order which will be used to invoke them.
        /// </summary>
        /// <param name="methods">The set of available methods.</param>
        /// <param name="initializers">The set of initializing methods in the appropriate order.</param>
        /// <returns>True if initializing methods were resolved.</returns>
        bool TryResolveInitializers([NotNull][ItemNotNull] IEnumerable<IMethod<MethodInfo>> methods, [ItemNotNull] out IEnumerable<IMethod<MethodInfo>> initializers);
    }
}


#endregion
#region IBinding

namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The container's binding.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    // ReSharper disable once UnusedTypeParameter
    public interface IBinding<in T>
    {
        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] IContainer Container { get; }

        /// <summary>
        /// Binding tokens.
        /// </summary>
        [NotNull] IEnumerable<IToken> Tokens { get; }

        /// <summary>
        /// The type to bind.
        /// </summary>
        [NotNull][ItemNotNull] IEnumerable<Type> Types { get; }

        /// <summary>
        /// The tags to mark the binding.
        /// </summary>
        [NotNull][ItemCanBeNull] IEnumerable<object> Tags { get; }

        /// <summary>
        /// The specified lifetime instance or null.
        /// </summary>
        [CanBeNull] ILifetime Lifetime { get; }

        /// <summary>
        /// The specified autowiring strategy or null.
        /// </summary>
        [CanBeNull] IAutowiringStrategy AutowiringStrategy { get; }
    }
}


#endregion
#region IBuildContext

namespace IoC
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents the abstraction for build context.
    /// </summary>
    [PublicAPI]
    public interface IBuildContext
    {
        /// <summary>
        /// The target key.
        /// </summary>
        Key Key { get; }

        /// <summary>
        /// The depth of current context.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] IContainer Container { get; }

        /// <summary>
        /// Autowiring strategy.
        /// </summary>
        [NotNull] IAutowiringStrategy AutowiringStrategy { get; }

        /// <summary>
        /// The dependency expression.
        /// </summary>
        [NotNull] Expression DependencyExpression { get; }

        /// <summary>
        /// Creates a child context.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="container">The container.</param>
        /// <returns>The new build context.</returns>
        [NotNull] IBuildContext CreateChild(Key key, [NotNull] IContainer container);

        /// <summary>
        /// Prepares base expression, replacing generic types' markers.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression ReplaceTypes([NotNull] Expression baseExpression);

        /// <summary>
        /// Bind a raw type to a target type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="targetType">The target type.</param>
        void BindTypes([NotNull] Type type, [NotNull]Type targetType);

        /// <summary>
        /// Try replacing generic types' markers.
        /// </summary>
        /// <param name="type">The target raw type.</param>
        /// <param name="targetType">The replacing type.</param>
        /// <returns></returns>
        bool TryReplaceType([NotNull] Type type, out Type targetType);

        /// <summary>
        /// Prepares base expression, injecting dependencies.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <param name="instanceExpression">The instance expression.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression InjectDependencies([NotNull] Expression baseExpression, [CanBeNull] ParameterExpression instanceExpression = null);

        /// <summary>
        /// Prepares base expression, adding a lifetime.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <returns></returns>
        [NotNull] Expression AddLifetime([NotNull] Expression baseExpression, [CanBeNull] ILifetime lifetime);
    }
}

#endregion
#region IBuilder

namespace IoC
{
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a builder for an instance.
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Builds the expression.
        /// </summary>
        /// <param name="context">Current context for building.</param>
        /// <param name="bodyExpression">The expression body to get an instance.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([NotNull] IBuildContext context, [NotNull] Expression bodyExpression);
    }
}


#endregion
#region ICompiler

namespace IoC
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a expression compiler.
    /// </summary>
    [PublicAPI]
    public interface ICompiler
    {
        /// <summary>
        /// Compiles an expression to a delegate.
        /// </summary>
        /// <param name="context">Current context for building.</param>
        /// <param name="expression">The lambda expression to compile.</param>
        /// <param name="resolver">The compiled resolver delegate.</param>
        /// <returns>True if success.</returns>
        bool TryCompile([NotNull] IBuildContext context, [NotNull] LambdaExpression expression, out Delegate resolver);
    }
}


#endregion
#region IConfiguration

namespace IoC
{
    using System.Collections.Generic;

    /// <summary>
    /// The container's configuration.
    /// </summary>
    [PublicAPI]
    public interface IConfiguration
    {
        /// <summary>
        /// Apply the configuration for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <returns>The enumeration of dependency tokens.</returns>
        [NotNull][ItemNotNull] IEnumerable<IToken> Apply([NotNull] IContainer container);
    }
}


#endregion
#region IContainer

namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Inversion of Control container.
    /// </summary>
    [PublicAPI]
    public interface IContainer: IEnumerable<IEnumerable<Key>>, IObservable<ContainerEvent>, IResourceRegistry
    {
        /// <summary>
        /// The parent container or null if it has no a parent.
        /// </summary>
        [CanBeNull] IContainer Parent { get; }

        /// <summary>
        /// Tries registering the dependency with lifetime.
        /// </summary>
        /// <param name="keys">The set of keys to register.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="dependencyToken">The dependency token.</param>
        /// <returns>True if successful.</returns>
        bool TryRegisterDependency([NotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out IToken dependencyToken);

        /// <summary>
        /// Tries getting the dependency with lifetime.
        /// </summary>
        /// <param name="key">The key to get a dependency.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>True if successful.</returns>
        bool TryGetDependency(Key key, out IDependency dependency, [CanBeNull] out ILifetime lifetime);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The type of instance producing by the resolver.</typeparam>
        /// <param name="type">The binding's type.</param>
        /// <param name="tag">The binding's tag or null if there is no tag.</param>
        /// <param name="resolver">The resolver.</param>
        /// <param name="error">The error occurring during resolving.</param>
        /// <param name="resolvingContainer">The resolving container and null if it is the current container.</param>
        /// <returns>True if successful.</returns>
        bool TryGetResolver<T>([NotNull] Type type, [CanBeNull] object tag, out Resolver<T> resolver, out Exception error, [CanBeNull] IContainer resolvingContainer = null);
    }
}


#endregion
#region IDependency

namespace IoC
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a IoC dependency.
    /// </summary>
    [PublicAPI]
    public interface IDependency
    {
        /// <summary>
        /// Builds an expression.
        /// </summary>
        /// <param name="buildContext">The build context,</param>
        /// <param name="lifetime">The target lifetime,</param>
        /// <param name="baseExpression">The resulting expression.</param>
        /// <param name="error">The error.</param>
        /// <returns>True if success.</returns>
        bool TryBuildExpression([NotNull] IBuildContext buildContext, [CanBeNull] ILifetime lifetime, out Expression baseExpression, out Exception error);
    }
}


#endregion
#region IHolder

namespace IoC
{
    /// <summary>
    /// Represents a holder for a created instance.
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    [PublicAPI]
    public interface IHolder<out TInstance>: IToken
    {
        /// <summary>
        /// The created instance.
        /// </summary>
        [NotNull] TInstance Instance { get; }
    }
}

#endregion
#region ILifetime

namespace IoC
{
    using System;

    /// <summary>
    /// Represents a lifetime for an instance.
    /// </summary>
    [PublicAPI]
    public interface ILifetime: IBuilder, IDisposable
    {
        /// <summary>
        /// Creates the similar lifetime to use with generic instances.
        /// </summary>
        /// <returns></returns>
        ILifetime Create();

        /// <summary>
        /// Select default container to resolve dependencies.
        /// </summary>
        /// <param name="registrationContainer">The container where the entry was registered.</param>
        /// <param name="resolvingContainer">The container which is used to resolve an instance.</param>
        /// <returns>The selected container.</returns>
        [NotNull] IContainer SelectResolvingContainer([NotNull] IContainer registrationContainer, [NotNull] IContainer resolvingContainer);
    }
}


#endregion
#region IMethod

namespace IoC
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Represents an abstraction for autowiring method.
    /// </summary>
    /// <typeparam name="TMethodInfo">The type of method info.</typeparam>
    [PublicAPI]
    public interface IMethod<out TMethodInfo>
        where TMethodInfo: MethodBase
    {
        /// <summary>
        /// The method's information.
        /// </summary>
        [NotNull] TMethodInfo Info { get; }

        /// <summary>
        /// Provides parameters' expressions.
        /// </summary>
        /// <returns>Parameters' expressions</returns>
        [NotNull][ItemNotNull] IEnumerable<Expression> GetParametersExpressions([NotNull] IBuildContext buildContext);

        /// <summary>
        /// Sets the parameter expression at the position.
        /// </summary>
        /// <param name="parameterPosition">The parameter position.</param>
        /// <param name="parameterExpression">The parameter expression.</param>
        void SetParameterExpression(int parameterPosition, [NotNull] Expression parameterExpression);
    }
}


#endregion
#region Injections

// ReSharper disable UnusedParameter.Global
namespace IoC
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Injection extensions.
    /// </summary>
    [PublicAPI]
    public static class Injections
    {
        internal static readonly string JustAMarkerError = $"The method `{nameof(Inject)}` is a marker method and has no implementation. It should be used to configure dependency injection via the constructor or initialization expressions only. In other cases please use `{nameof(FluentResolve.Resolve)}` method.";
        [NotNull] internal static readonly MethodInfo InjectGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectWithTagGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectingAssignmentGenericMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectWithTagMethodInfo;

        static Injections()
        {
            Expression<Func<object>> injectGenExpression = () => Inject<object>(default(IContainer));
            InjectGenericMethodInfo = ((MethodCallExpression)injectGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Func<object>> injectWithTagGenExpression = () => Inject<object>(default(IContainer), null);
            InjectWithTagGenericMethodInfo = ((MethodCallExpression)injectWithTagGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Action<object, object>> assignmentCallGenExpression = (item1, item2) => Inject<object>(default(IContainer), null, null);
            InjectingAssignmentGenericMethodInfo = ((MethodCallExpression)assignmentCallGenExpression.Body).Method.GetGenericMethodDefinition();

            Expression<Func<object>> injectExpression = () => Inject(default(IContainer), typeof(object));
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method;

            Expression<Func<object>> injectWithTagExpression = () => Inject(default(IContainer), typeof(object), (object)null);
            InjectWithTagMethodInfo = ((MethodCallExpression)injectWithTagExpression.Body).Method;
        }

        /// <summary>
        /// Injects the dependency. Just an injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>([NotNull] this IContainer container) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects the dependency. Just an injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>([NotNull] this IContainer container, [CanBeNull] object tag) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects the dependency. Just an injection marker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="destination">The destination member for injection.</param>
        /// <param name="source">The source of injection.</param>
        public static void Inject<T>([NotNull] this IContainer container, [NotNull] T destination, [CanBeNull] T source) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects the dependency. Just an injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static object Inject([NotNull] this IContainer container, [NotNull] Type type) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects the dependency. Just an injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static object Inject([NotNull] this IContainer container, [NotNull] Type type, [CanBeNull] object tag) =>
            throw new NotImplementedException(JustAMarkerError);
    }
}


#endregion
#region IResourceRegistry

namespace IoC
{
    using System;

    /// <summary>
    /// Represents the resource's registry.
    /// </summary>
    [PublicAPI]
    public interface IResourceRegistry: IDisposable
    {
        /// <summary>
        /// Registers a resource to the registry.
        /// </summary>
        /// <param name="resource">The target resource.</param>
        void RegisterResource([NotNull] IDisposable resource);

        /// <summary>
        /// Unregisters a resource from the registry.
        /// </summary>
        /// <param name="resource">The target resource.</param>
        void UnregisterResource([NotNull] IDisposable resource);
    }
}


#endregion
#region IScope

namespace IoC
{
    using System;

    /// <summary>
    /// Represents the scope which could be used with <c>Lifetime.ScopeSingleton</c>
    /// </summary>
    [PublicAPI]
    public interface IScope : IDisposable
    {
        /// <summary>
        /// Activate the scope.
        /// </summary>
        /// <returns>The token to deactivate the scope.</returns>
        IDisposable Activate();
    }
}

#endregion
#region IToken

namespace IoC
{
    using System;

    /// <summary>
    /// The binding token to manage binding lifetime.
    /// </summary>
    public interface IToken: IDisposable
    {
        /// <summary>
        /// The owner container.
        /// </summary>
        IContainer Container { get; }
    }
}


#endregion
#region Key

namespace IoC
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents a dependency key.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("Type = {" + nameof(Type) + "}, Tag = {" + nameof(Tag) + "}")]
    public struct Key: IEquatable<Key>
    {
        /// <summary>
        /// The marker object for any tag.
        /// </summary>
        [NotNull] public static readonly object AnyTag = new AnyTagObject();

        /// <summary>
        /// The type.
        /// </summary>
        [NotNull] public readonly Type Type;

        /// <summary>
        /// The tag.
        /// </summary>
        [CanBeNull] public readonly object Tag;

        private readonly int _hashCode;

        /// <summary>
        /// Creates the instance of Key.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tag"></param>
        public Key([NotNull] Type type, [CanBeNull] object tag = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Tag = tag;
            unchecked
            {
                _hashCode = (tag?.GetHashCode() * 397 ?? 0) ^ type.GetHashCode();
            }
        }

        /// <inheritdoc />
        public override string ToString() => $"[Type = {Type.FullName}, Tag = {Tag ?? "empty"}, HashCode = {GetHashCode()}]";

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        // ReSharper disable once PossibleNullReferenceException
        public override bool Equals(object obj) => Equals((Key)obj);

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public bool Equals(Key other) => ReferenceEquals(Type, other.Type) && (ReferenceEquals(Tag, other.Tag) || Equals(Tag, other.Tag));

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public override int GetHashCode() => _hashCode;

        private struct AnyTagObject
        {
            public override string ToString() => "any";
        }
    }
}


#endregion
#region Lifetime

namespace IoC
{
    /// <summary>
    /// The enumeration of well-known lifetimes.
    /// </summary>
    [PublicAPI]
    public enum Lifetime
    {
        /// <summary>
        /// It is default lifetime. A new instance wll be created each time.
        /// </summary>
        Transient = 1,

        /// <summary>
        /// Singleton
        /// </summary>
        Singleton = 2,

        /// <summary>
        /// Singleton per container
        /// </summary>
        ContainerSingleton = 3,

        /// <summary>
        /// Singleton per scope
        /// </summary>
        ScopeSingleton = 4
    }
}


#endregion
#region Resolver

namespace IoC
{
    /// <summary>
    /// Represents the resolver delegate.
    /// </summary>
    /// <typeparam name="T">The type of resolving instance.</typeparam>
    /// <param name="container">The resolving container.</param>
    /// <param name="args">The optional resolving arguments.</param>
    /// <returns>The resolved instance.</returns>
    [PublicAPI]
    [NotNull] public delegate T Resolver<out T>([NotNull] IContainer container, [NotNull][ItemCanBeNull] params object[] args);
}


#endregion
#region Tag

namespace IoC
{
    /// <summary>
    /// Represents a tag holder.
    /// </summary>
    [PublicAPI]

    public struct Tag
    {
        internal readonly object Value;

        internal Tag([CanBeNull] object value) => Value = value;

        /// <inheritdoc />
        public override string ToString() => Value?.ToString() ?? "empty";
    }
}


#endregion
#region WellknownContainers

namespace IoC
{
    /// <summary>
    /// The enumeration of well-known containers.
    /// </summary>
    [PublicAPI]
    public enum WellknownContainers
    {
        /// <summary>
        /// Parent container.
        /// </summary>
        Parent = 1,

        /// <summary>
        /// Creates new child container.
        /// </summary>
        NewChild = 2
    }
}


#endregion
#region WellknownExpressions

namespace IoC
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Core;

    /// <summary>
    /// The list of well-known expressions.
    /// </summary>
    [PublicAPI]
    public static class WellknownExpressions
    {
        /// <summary>
        /// The container parameter.
        /// </summary>
        [NotNull]
        public static readonly ParameterExpression ContainerParameter = Expression.Parameter(TypeDescriptor<IContainer>.Type, nameof(Context.Container));

        /// <summary>
        /// The args parameters.
        /// </summary>
        [NotNull]
        public static readonly ParameterExpression ArgsParameter = Expression.Parameter(TypeDescriptor<object[]>.Type, nameof(Context.Args));
    }
}


#endregion

#endregion

#region Features

#region CollectionFeature

// ReSharper disable MemberCanBeProtected.Local
namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    // ReSharper disable once RedundantUsingDirective
    using System.Threading;
    // ReSharper disable once RedundantUsingDirective
    using System.Threading.Tasks;
    using Core;


    /// <summary>
    /// Allows to resolve enumeration of all instances related to corresponding bindings.
    /// </summary>
    [PublicAPI]
    public sealed class CollectionFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new CollectionFeature();

        private CollectionFeature()
        { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var containerSingletonResolver = container.GetResolver<ILifetime>(Lifetime.ContainerSingleton.AsTag());
            yield return container.Register<IEnumerable<TT>>(ctx => new Enumeration<TT>(ctx, ctx.Container.Inject<ILockObject>()), containerSingletonResolver(container));
            yield return container.Register<List<TT>, IList<TT>, ICollection<TT>>(ctx => ctx.Container.Inject<IEnumerable<TT>>().ToList());
            yield return container.Register(ctx => ctx.Container.Inject<IEnumerable<TT>>().ToArray());
            yield return container.Register<HashSet<TT>, ISet<TT>>(ctx => new HashSet<TT>(ctx.Container.Inject<IEnumerable<TT>>()));
            yield return container.Register<IObservable<TT>>(ctx => new Observable<TT>(ctx.Container.Inject<IEnumerable<TT>>()), containerSingletonResolver(container));
#if !NET40
            yield return container.Register<ReadOnlyCollection<TT>, IReadOnlyList<TT>, IReadOnlyCollection<TT>>(ctx => new ReadOnlyCollection<TT>(ctx.Container.Inject<List<TT>>()));
#endif
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            yield return container.Register<IAsyncEnumerable<TT>>(ctx => new AsyncEnumeration<TT>(ctx, ctx.Container.Inject<ILockObject>()), containerSingletonResolver(container));
#endif
        }

        internal class Observable<T>: IObservable<T>
        {
            private readonly IEnumerable<T> _source;

            public Observable([NotNull] IEnumerable<T> source) => _source = source ?? throw new ArgumentNullException(nameof(source));

            public IDisposable Subscribe(IObserver<T> observer)
            {
                foreach (var value in _source)
                {
                    observer.OnNext(value);
                }

                observer.OnCompleted();
                return Disposable.Empty;
            }
        }

        private class Enumeration<T> : EnumerationBase<T>, IEnumerable<T>
        {
            public Enumeration([NotNull] Context context, [NotNull] ILockObject lockObject)
            : base(context, lockObject)
            { }

            [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
            public IEnumerator<T> GetEnumerator()
            {
                var resolvers = GetResolvers();

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < resolvers.Length; i++)
                {
                    yield return resolvers[i](Context.Container, Context.Args);
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
        private class AsyncEnumeration<T> : EnumerationBase<T>, IAsyncEnumerable<T>
        {
            public AsyncEnumeration([NotNull] Context context, [NotNull] ILockObject lockObject)
                : base(context, lockObject)
            { }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()) => 
                new AsyncEnumerator<T>(this, cancellationToken);
        }

        private class AsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly AsyncEnumeration<T> _enumeration;
            private readonly CancellationToken _cancellationToken;
            private readonly TaskScheduler _taskScheduler;
            private int _index = -1;

            public AsyncEnumerator(AsyncEnumeration<T> enumeration, CancellationToken cancellationToken)
            {
                _enumeration = enumeration;
                _cancellationToken = cancellationToken;
                var container = enumeration.Context.Container;
                _taskScheduler = container.TryGetResolver(out Resolver<TaskScheduler> taskSchedulerResolver) ? taskSchedulerResolver(container) : TaskScheduler.Current;
            }

            public async ValueTask<bool> MoveNextAsync() =>
                await new ValueTask<bool>(
                    StartTask(
                        new Task<bool>(
                            () =>
                            {
                                var resolvers = _enumeration.GetResolvers();
                                var index = Interlocked.Increment(ref _index);
                                if (index >= resolvers.Length)
                                {
                                    return false;
                                }

                                Current = resolvers[index](_enumeration.Context.Container, _enumeration.Context.Args);
                                return true;
                            },
                            _cancellationToken)));

            public T Current { get; private set; }

            public ValueTask DisposeAsync() => new ValueTask();

            private Task<bool> StartTask(Task<bool> task)
            {
                task.Start(_taskScheduler);
                return task;
            }
        }
#endif

        private class EnumerationBase<T>: IObserver<ContainerEvent>, IDisposable
        {
            [NotNull] protected internal readonly Context Context;
            private readonly ILockObject _lockObject;
            private readonly IDisposable _subscription;
            private volatile Resolver<T>[] _resolvers;

            public EnumerationBase([NotNull] Context context, [NotNull] ILockObject lockObject)
            {
                Context = context;
                _lockObject = lockObject;
                _subscription = context.Container.Subscribe(this);
                Reset();
            }

            public void OnNext(ContainerEvent value) => Reset();

            public void OnError(Exception error) { }

            public void OnCompleted() { }

            public void Dispose() => _subscription.Dispose();

            public Resolver<T>[] GetResolvers()
            {
                lock (_lockObject)
                {
                    var resolvers = _resolvers;
                    if (resolvers != null)
                    {
                        return resolvers;
                    }
                    
                    resolvers = GetResolvers(Context.Container).ToArray();
                    _resolvers = resolvers;

                    return resolvers;
                }
            }

            private void Reset()
            {
                lock (_lockObject)
                {
                    _resolvers = null;
                }
            }

            private static IEnumerable<Resolver<T>> GetResolvers(IContainer container)
            {
                var targetType = TypeDescriptorExtensions.Descriptor<T>();
                var isConstructedGenericType = targetType.IsConstructedGenericType();
                var genericTargetType = default(TypeDescriptor);
                Type[] genericTypeArguments = null;
                if (isConstructedGenericType)
                {
                    genericTargetType = targetType.GetGenericTypeDefinition().Descriptor();
                    genericTypeArguments = targetType.GetGenericTypeArguments();
                }

                foreach (var keyGroup in container)
                {
                    foreach (var key in keyGroup)
                    {
                        Type typeToResolve = null;
                        var registeredType = key.Type.Descriptor();
                        if (registeredType.IsGenericTypeDefinition())
                        {
                            if (isConstructedGenericType && genericTargetType.IsAssignableFrom(registeredType))
                            {
                                typeToResolve = registeredType.MakeGenericType(genericTypeArguments);
                            }
                        }
                        else
                        {
                            if (targetType.IsAssignableFrom(registeredType))
                            {
                                typeToResolve = key.Type;
                            }
                        }

                        if (typeToResolve == null)
                        {
                            continue;
                        }

                        var tag = key.Tag;
                        if (tag == null || ReferenceEquals(tag, Key.AnyTag))
                        {
                            yield return container.GetResolver<T>(typeToResolve);
                        }
                        else
                        {
                            yield return container.GetResolver<T>(typeToResolve, tag.AsTag());
                        }

                        break;
                    }
                }
            }
        }
    }
}


#endregion
#region ConfigurationFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Core;
    using Core.Configuration;

    /// <summary>
    /// Allows to configure via a text metadata.
    /// </summary>
    [PublicAPI]
    public sealed class ConfigurationFeature : IConfiguration
    {
        /// <summary>
        /// The default instance.
        /// </summary>
        public static readonly IConfiguration Default = new ConfigurationFeature();

        private ConfigurationFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var containerSingletonResolver = container.GetResolver<ILifetime>(Lifetime.ContainerSingleton.AsTag());
            yield return container.Register<StatementsToBindingContextConverter, IConverter<IEnumerable<Statement>, BindingContext, BindingContext>>(containerSingletonResolver(container));
            yield return container.Register<StatementToBindingConverter, IConverter<Statement, BindingContext, BindingContext>>(containerSingletonResolver(container), new object[] { "Bind" });
            yield return container.Register<StatementToNamespacesConverter, IConverter<Statement, BindingContext, BindingContext>>(containerSingletonResolver(container), new object[] {"using"});
            yield return container.Register<StatementToReferencesConverter, IConverter<Statement, BindingContext, BindingContext>>(containerSingletonResolver(container), new object[] { "ref" });
            yield return container.Register<StringToTypeConverter, IConverter<string, BindingContext, Type>>(containerSingletonResolver(container));
            yield return container.Register<StringToLifetimeConverter, IConverter<string, Statement, Lifetime>>(containerSingletonResolver(container));
            yield return container.Register<StringToTagsConverter, IConverter<string, Statement, IEnumerable<object>>>(containerSingletonResolver(container));
            yield return container.Register<IConfiguration>(ctx => CreateTextConfiguration(ctx, ctx.Container.Inject<IConverter<IEnumerable<Statement>, BindingContext, BindingContext>>()));
        }

        internal static TextConfiguration CreateTextConfiguration([NotNull] Context ctx, [NotNull] IConverter<IEnumerable<Statement>, BindingContext, BindingContext> converter)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (converter == null) throw new ArgumentNullException(nameof(converter));
            // ReSharper disable once NotResolvedInText
            if (ctx.Args.Length != 1) throw new ArgumentOutOfRangeException("Should have single argument.");

            TextReader reader;
            switch (ctx.Args[0])
            {
                case string text:
                    reader = new StringReader(text);
                    break;

                case Stream stream:
                    reader = new StreamReader(stream);
                    break;

                case TextReader textReader:
                    reader = textReader;
                    break;

                default:
                    throw new ArgumentException("Invalid type of argument.");
            }

            return new TextConfiguration(reader, converter);
        }
    }
}


#endregion
#region CoreFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Core;
    using Lifetimes;

    /// <summary>
    /// Adds the set of core features like lifetimes and default containers.
    /// </summary>
    [PublicAPI]
    public sealed class CoreFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new CoreFeature();

        private CoreFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => FoundCyclicDependency.Shared);
            yield return container.Register(ctx => CannotBuildExpression.Shared);
            yield return container.Register(ctx => CannotGetGenericTypeArguments.Shared);
            yield return container.Register(ctx => CannotGetResolver.Shared);
            yield return container.Register(ctx => CannotParseLifetime.Shared);
            yield return container.Register(ctx => CannotParseTag.Shared);
            yield return container.Register(ctx => CannotParseType.Shared);
            yield return container.Register(ctx => CannotRegister.Shared);
            yield return container.Register(ctx => CannotResolveConstructor.Shared);
            yield return container.Register(ctx => CannotResolveDependency.Shared);
            yield return container.Register(ctx => CannotResolveType.Shared);

            yield return container.Register(ctx => DefaultAutowiringStrategy.Shared);
            yield return container.Register(ctx => ctx.Container.GetResolver<TT>(ctx.Key.Tag.AsTag()), null, Feature.AnyTag);

            // Lifetimes
            yield return container.Register<ILifetime>(ctx => new SingletonLifetime(), null, new object[] { Lifetime.Singleton });
            yield return container.Register<ILifetime>(ctx => new ContainerSingletonLifetime(), null, new object[] { Lifetime.ContainerSingleton });
            yield return container.Register<ILifetime>(ctx => new ScopeSingletonLifetime(), null, new object[] { Lifetime.ScopeSingleton });

            // Scope
            yield return container.Register<IScope>(ctx => new Scope(ctx.Container.Inject<ILockObject>()));

            // ThreadLocal
            yield return container.Register(ctx => new ThreadLocal<TT>(() => ctx.Container.Inject<TT>(ctx.Key.Tag)), null, Feature.AnyTag);

            // Containers
            // Current
            yield return container.Register<IContainer, IResourceRegistry, IObservable<ContainerEvent>>(ctx => ctx.Container);
            
            // New child
            yield return container.Register<IContainer>(
                ctx => new Container(
                    ctx.Args.Length == 1
                        ? Container.CreateContainerName(ctx.Args[0] as string)
                        : Container.CreateContainerName(string.Empty),
                    ctx.Container,
                    ctx.Container.Inject<ILockObject>()),
                null,
                new object[] { WellknownContainers.NewChild });
            
            // Parent
            yield return container.Register(ctx => ctx.Container.Parent, null, new object[] { WellknownContainers.Parent });

            yield return container.Register(ctx => ContainerEventToStringConverter.Shared);
            yield return container.Register(ctx => TypeToStringConverter.Shared);
        }
    }
}


#endregion
#region Feature

namespace IoC.Features
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides defaults for features.
    /// </summary>
    internal static class Feature
    {
        public static readonly object[] AnyTag = { Key.AnyTag };

        /// <summary>
        /// Core features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> CoreSet = new[]
        {
            CoreFeature.Default
        };

        /// <summary>
        /// Default features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> DefaultSet = Combine(
            CoreSet,
            new[]
            {
                CollectionFeature.Default,
                FuncFeature.Default,
                TaskFeature.Default,
                TupleFeature.Default,
                LazyFeature.Default,
                ConfigurationFeature.Default
            });

        /// <summary>
        /// The light set of features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> LightSet = Combine(
            CoreSet,
            new[]
            {
                CollectionFeature.Default,
                FuncFeature.Light,
                TaskFeature.Default,
                TupleFeature.Light,
                LazyFeature.Default,
                ConfigurationFeature.Default
            });

        private static IEnumerable<IConfiguration> Combine(params IEnumerable<IConfiguration>[] configurations)
        {
            return configurations.SelectMany(i => i);
        }
    }
}


#endregion
#region FuncFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;

    /// <summary>
    /// Allows to resolve Functions.
    /// </summary>
    [PublicAPI]
    public sealed  class FuncFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new FuncFeature();
        /// The high-performance instance.
        public static readonly IConfiguration Light = new FuncFeature(true);

        private readonly bool _light;

        private FuncFeature(bool light = false) => _light = light;

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register<Func<TT>>(ctx => () => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT>>(ctx => arg1 => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT>>(ctx => (arg1, arg2) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT>>(ctx => (arg1, arg2, arg3) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT>>(ctx => (arg1, arg2, arg3, arg4) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4), null, Feature.AnyTag);

            if (_light) yield break;

            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5, arg6) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5, arg6), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8), null, Feature.AnyTag);
        }
    }
}


#endregion
#region LazyFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;

    /// <summary>
    /// Allows to resolve Lazy.
    /// </summary>
    public class LazyFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new LazyFeature();

        private LazyFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => new Lazy<TT>(() => ctx.Container.Inject<TT>(ctx.Key.Tag), true), null, Feature.AnyTag);
        }
    }
}


#endregion
#region TaskFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Core;

    /// <summary>
    /// Allows to resolve Tasks.
    /// </summary>
    [PublicAPI]
    public sealed  class TaskFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new TaskFeature();

        private TaskFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => TaskScheduler.Current);
            yield return container.Register(ctx => StartTask(new Task<TT>(ctx.Container.Inject<Func<TT>>(ctx.Key.Tag)), ctx.Container.Inject<TaskScheduler>()), null, Feature.AnyTag);
#if !NET40 && !NET403 && !NET45 && !NET45 && !NET451 && !NET452 && !NET46 && !NET461 && !NET462 && !NET47 && !NET48 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2&& !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !WINDOWS_UWP
            yield return container.Register(ctx => new ValueTask<TT>(StartTask(new Task<TT>(ctx.Container.Inject<Func<TT>>(ctx.Key.Tag)), ctx.Container.Inject<TaskScheduler>())), null, Feature.AnyTag);
#endif
        }

        [MethodImpl((MethodImplOptions)256)]
        private static Task<T> StartTask<T>(Task<T> task, TaskScheduler taskScheduler)
        {
            task.Start(taskScheduler);
            return task;
        }
    }
}


#endregion
#region TupleFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;

    /// <summary>
    /// Allows to resolve Tuples.
    /// </summary>
    [PublicAPI]
    public sealed  class TupleFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new TupleFeature();
        /// The high-performance instance.
        public static readonly IConfiguration Light = new TupleFeature(true);

        private readonly bool _light;

        private TupleFeature(bool light = false) => _light = light;

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => new Tuple<TT>(ctx.Container.Inject<TT>(ctx.Key.Tag)), null, Feature.AnyTag);

            yield return container.Register(ctx => new Tuple<TT1, TT2>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag)), null, Feature.AnyTag);

            yield return container.Register(ctx => new Tuple<TT1, TT2, TT3>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag)), null, Feature.AnyTag);

            yield return container.Register(ctx => new Tuple<TT1, TT2, TT3, TT4>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag)), null, Feature.AnyTag);

            if (!_light)
            {
                yield return container.Register(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5>(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag)), null, Feature.AnyTag);

                yield return container.Register(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5, TT6>(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag)), null, Feature.AnyTag);

                yield return container.Register(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5, TT6, TT7>(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag),
                    ctx.Container.Inject<TT7>(ctx.Key.Tag)), null, Feature.AnyTag);

                yield return container.Register(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8>(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag),
                    ctx.Container.Inject<TT7>(ctx.Key.Tag),
                    ctx.Container.Inject<TT8>(ctx.Key.Tag)), null, Feature.AnyTag);
            }

#if !NET40 && !NET403 && !NET45 && !NET45 && !NET451 && !NET452 && !NET46 && !NET461 && !NET462 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2&& !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !WINDOWS_UWP
            yield return container.Register(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag)), null, Feature.AnyTag);

            yield return container.Register(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag)), null, Feature.AnyTag);

            yield return container.Register(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag)), null, Feature.AnyTag);

            if (!_light)
            {
                yield return container.Bind<(TT1, TT2, TT3, TT4, TT5)>().AnyTag().To(ctx => CreateTuple(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag)));

                yield return container.Register(ctx => CreateTuple(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag)), null, Feature.AnyTag);

                yield return container.Register(ctx => CreateTuple(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag),
                    ctx.Container.Inject<TT7>(ctx.Key.Tag)), null, Feature.AnyTag);

                yield return container.Register(ctx => CreateTuple(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag),
                    ctx.Container.Inject<TT7>(ctx.Key.Tag),
                    ctx.Container.Inject<TT8>(ctx.Key.Tag)), null, Feature.AnyTag);
            }
#endif
        }

#if !NET40 && !NET403 && !NET45 && !NET45 && !NET451 && !NET452 && !NET46 && !NET461 && !NET462 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2&& !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !WINDOWS_UWP
        internal static (T1, T2) CreateTuple<T1, T2>(T1 val1, T2 val2) => (val1, val2);
        internal static (T1, T2, T3) CreateTuple<T1, T2, T3>(T1 val1, T2 val2, T3 val3) => (val1, val2, val3);
        internal static (T1, T2, T3, T4) CreateTuple<T1, T2, T3, T4>(T1 val1, T2 val2, T3 val3, T4 val4) => (val1, val2, val3, val4);
        internal static (T1, T2, T3, T4, T5) CreateTuple<T1, T2, T3, T4, T5>(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5) => (val1, val2, val3, val4, val5);
        internal static (T1, T2, T3, T4, T5, T6) CreateTuple<T1, T2, T3, T4, T5, T6>(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5, T6 val6) => (val1, val2, val3, val4, val5, val6);
        internal static (T1, T2, T3, T4, T5, T6, T7) CreateTuple<T1, T2, T3, T4, T5, T6, T7>(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5, T6 val6, T7 val7) => (val1, val2, val3, val4, val5, val6, val7);
        internal static (T1, T2, T3, T4, T5, T6, T7, T8) CreateTuple<T1, T2, T3, T4, T5, T6, T7, T8>(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5, T6 val6, T7 val7, T8 val8) => (val1, val2, val3, val4, val5, val6, val7, val8);
#endif
    }
}


#endregion

#endregion

#region Lifetimes

#region ContainerSingletonLifetime

namespace IoC.Lifetimes
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using Core;

    /// <summary>
    /// Represents singleton per container lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class ContainerSingletonLifetime: KeyBasedLifetime<IContainer>
    {
        /// <inheritdoc />
        protected override IContainer CreateKey(IContainer container, object[] args) => container;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ContainerSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ContainerSingletonLifetime();

        /// <inheritdoc />
        protected override T OnNewInstanceCreated<T>(T newInstance, IContainer targetContainer, IContainer container, object[] args)
        {
            if (newInstance is IDisposable disposable)
            {
                targetContainer.RegisterResource(disposable);
            }

#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            if (newInstance is IAsyncDisposable asyncDisposable)
            {
                targetContainer.RegisterResource(asyncDisposable.ToDisposable());
            }
#endif

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnInstanceReleased(object releasedInstance, IContainer targetContainer)
        {
            if (releasedInstance is IDisposable disposable)
            {
                targetContainer.UnregisterResource(disposable);
                disposable.Dispose();
            }

#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            if (releasedInstance is IAsyncDisposable asyncDisposable)
            {
                disposable = asyncDisposable.ToDisposable();
                targetContainer.UnregisterResource(disposable);
                disposable.Dispose();
            }
#endif
        }
    }
}


#endregion
#region KeyBasedLifetime

namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using static Core.TypeDescriptorExtensions;
    using static WellknownExpressions;

    /// <summary>
    /// Represents the abstraction for singleton based lifetimes.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    [PublicAPI]
    public abstract class KeyBasedLifetime<TKey>: ILifetime
    {
        private static readonly FieldInfo InstancesFieldInfo = Descriptor<KeyBasedLifetime<TKey>>().GetDeclaredFields().Single(i => i.Name == nameof(_instances));
        private static readonly MethodInfo CreateKeyMethodInfo = Descriptor<KeyBasedLifetime<TKey>>().GetDeclaredMethods().Single(i => i.Name == nameof(CreateKey));
        private static readonly MethodInfo GetMethodInfo = typeof(CoreExtensions).Descriptor().GetDeclaredMethods().Single(i => i.Name == (TypeDescriptor<TKey>.Descriptor.IsValueType() ? nameof(CoreExtensions.Get) : nameof(CoreExtensions.GetByRef))).MakeGenericMethod(typeof(TKey), typeof(object));
        private static readonly MethodInfo SetMethodInfo = Descriptor<Table<TKey, object>>().GetDeclaredMethods().Single(i => i.Name == nameof(Table<TKey, object>.Set));
        private static readonly MethodInfo OnNewInstanceCreatedMethodInfo = Descriptor<KeyBasedLifetime<TKey>>().GetDeclaredMethods().Single(i => i.Name == nameof(OnNewInstanceCreated));
        private static readonly ParameterExpression KeyVar = Expression.Variable(TypeDescriptor<TKey>.Type, "key");

        [NotNull] private readonly ILockObject _lockObject = new LockObject();
        private volatile Table<TKey, object> _instances = Table<TKey, object>.Empty;

        /// <inheritdoc />
        public Expression Build(IBuildContext context, Expression bodyExpression)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (context == null) throw new ArgumentNullException(nameof(context));
            var returnType = context.Key.Type;
            var thisConst = Expression.Constant(this);
            var instanceVar = Expression.Variable(returnType, "val");
            var instancesField = Expression.Field(thisConst, InstancesFieldInfo);
            var lockObjectConst = Expression.Constant(_lockObject);
            var onNewInstanceCreatedMethodInfo = OnNewInstanceCreatedMethodInfo.MakeGenericMethod(returnType);
            var assignInstanceExpression = Expression.Assign(instanceVar, Expression.Call(null, GetMethodInfo, instancesField, SingletonBasedLifetimeShared.HashCodeVar, KeyVar).Convert(returnType));
            var isNullExpression = Expression.ReferenceEqual(instanceVar, ExpressionBuilderExtensions.NullConst);

            return Expression.Block(
                // Key key;
                // int hashCode;
                // T instance;
                new[] { KeyVar, SingletonBasedLifetimeShared.HashCodeVar, instanceVar },
                // var key = CreateKey(container, args);
                Expression.Assign(KeyVar, Expression.Call(thisConst, CreateKeyMethodInfo, ContainerParameter, ArgsParameter)),
                // var hashCode = key.GetHashCode();
                Expression.Assign(SingletonBasedLifetimeShared.HashCodeVar, Expression.Call(KeyVar, ExpressionBuilderExtensions.GetHashCodeMethodInfo)),
                // var instance = (T)_instances.Get(hashCode, key);
                assignInstanceExpression,
                // if (instance == null)
                Expression.Condition(
                    isNullExpression,
                    Expression.Block(
                        // lock (this._lockObject)
                        Expression.Block(
                            // var instance = (T)_instances.Get(hashCode, key);
                            assignInstanceExpression,
                            // if (instance == null)
                            Expression.IfThen(
                                Expression.Equal(instanceVar, ExpressionBuilderExtensions.NullConst),
                                Expression.Block(
                                    // instance = new T();
                                    Expression.Assign(instanceVar, bodyExpression),
                                    // Instances = _instances.Set(hashCode, key, instance);
                                    Expression.Assign(instancesField, Expression.Call(instancesField, SetMethodInfo, SingletonBasedLifetimeShared.HashCodeVar, KeyVar, instanceVar))
                                )
                            )
                        ).Lock(lockObjectConst),
                        // OnNewInstanceCreated(instance, key, container, args);
                        Expression.Call(thisConst, onNewInstanceCreatedMethodInfo, instanceVar, KeyVar, ContainerParameter, ArgsParameter)),
                        // else {
                        // return instance;
                        instanceVar
                    )
                    // }
            );
        }

        /// <inheritdoc />
        public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        /// <inheritdoc />
        public virtual void Dispose()
        {
            Table<TKey, object> instances;
            lock (_lockObject)
            {
                instances = _instances;
                _instances = Table<TKey, object>.Empty;
            }

            foreach (var instance in instances)
            {
                OnInstanceReleased(instance.Value, instance.Key);
            }
        }

        /// <inheritdoc />
        public abstract ILifetime Create();

        /// <summary>
        /// Creates key for singleton.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The created key.</returns>
        protected abstract TKey CreateKey(IContainer container, object[] args);

        /// <summary>
        /// Is invoked on the new instance creation.
        /// </summary>
        /// <param name="newInstance">The new instance.</param>
        /// <param name="key">The instance key.</param>
        /// <param name="container">The target container.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The created instance.</returns>
        protected abstract T OnNewInstanceCreated<T>(T newInstance, TKey key, IContainer container, object[] args);

        /// <summary>
        /// Is invoked on the instance was released.
        /// </summary>
        /// <param name="releasedInstance">The released instance.</param>
        /// <param name="key">The instance key.</param>
        protected abstract void OnInstanceReleased(object releasedInstance, TKey key);
    }

    internal static class SingletonBasedLifetimeShared
    {
        internal static readonly ParameterExpression HashCodeVar = Expression.Variable(TypeDescriptor<int>.Type, "hashCode");
    }
}


#endregion
#region ScopeSingletonLifetime

namespace IoC.Lifetimes
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using Core;

    /// <summary>
    /// Represents singleton per scope lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class ScopeSingletonLifetime: KeyBasedLifetime<IScope>
    {
        /// <inheritdoc />
        protected override IScope CreateKey(IContainer container, object[] args) => Scope.Current;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ScopeSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ScopeSingletonLifetime();

        /// <inheritdoc />
        protected override T OnNewInstanceCreated<T>(T newInstance, IScope scope, IContainer container, object[] args)
        {
            if (!(scope is IResourceRegistry resourceRegistry))
            {
                return newInstance;
            }

            if (newInstance is IDisposable disposable)
            {
                resourceRegistry.RegisterResource(disposable);
            }

#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
                if (newInstance is IAsyncDisposable asyncDisposable)
                {
                    resourceRegistry.RegisterResource(asyncDisposable.ToDisposable());
                }
#endif

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnInstanceReleased(object releasedInstance, IScope scope)
        {
            if (!(scope is IResourceRegistry resourceRegistry))
            {
                return;
            }

            if (releasedInstance is IDisposable disposable)
            {
                resourceRegistry.UnregisterResource(disposable);
                disposable.Dispose();
            }

#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            if (releasedInstance is IAsyncDisposable asyncDisposable)
            {
                disposable = asyncDisposable.ToDisposable();
                resourceRegistry.UnregisterResource(disposable);
                disposable.Dispose();
            }
#endif
        }
    }
}


#endregion
#region SingletonLifetime

namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using static Core.TypeDescriptorExtensions;

    /// <summary>
    /// Represents singleton lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class SingletonLifetime : ILifetime
    {
        private static readonly FieldInfo InstanceFieldInfo = Descriptor<SingletonLifetime>().GetDeclaredFields().Single(i => i.Name == nameof(_instance));

        [NotNull] private readonly ILockObject _lockObject = new LockObject();
#pragma warning disable CS0649, IDE0044
        private volatile object _instance;
#pragma warning restore CS0649, IDE0044

        /// <inheritdoc />
        public Expression Build(IBuildContext context, Expression expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (context == null) throw new ArgumentNullException(nameof(context));

            var thisConst = Expression.Constant(this);
            var lockObjectConst = Expression.Constant(_lockObject);
            var instanceField = Expression.Field(thisConst, InstanceFieldInfo);
            var typedInstance = instanceField.Convert(expression.Type);
            var isNullExpression = Expression.ReferenceEqual(instanceField, ExpressionBuilderExtensions.NullConst);

            return Expression.Block(Expression.IfThen(
                isNullExpression,
                // if (this._instance == null)
                // lock (this._lockObject)
                Expression.IfThen(
                    // if (this._instance == null)
                    isNullExpression,
                    // this._instance = new T();
                    Expression.Assign(instanceField, expression)).Lock(lockObjectConst)),
                // return this._instance
                typedInstance);
        }

        /// <inheritdoc />
        public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            registrationContainer;

        /// <inheritdoc />
        public void Dispose()
        {
            IDisposable disposable;
            lock (_lockObject)
            {
                disposable = _instance as IDisposable;
            }

            disposable?.Dispose();

#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            IAsyncDisposable asyncDisposable;
            lock (_lockObject)
            {
                asyncDisposable = _instance as IAsyncDisposable;
            }

            asyncDisposable?.ToDisposable().Dispose();
#endif
        }

        /// <inheritdoc />
        public ILifetime Create() => new SingletonLifetime();

        /// <inheritdoc />
        public override string ToString() => Lifetime.Singleton.ToString();
    }
}


#endregion

#endregion

#region Issues

#region DependencyDescription

namespace IoC.Issues
{
    /// <summary>
    /// Represents the dependency.
    /// </summary>
    [PublicAPI]
    public struct DependencyDescription
    {
        /// <summary>
        /// The resolved dependency.
        /// </summary>
        [NotNull] public readonly IDependency Dependency;
        /// <summary>
        /// The lifetime to use.
        /// </summary>
        [CanBeNull] public readonly ILifetime Lifetime;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="dependency">The resolved dependency.</param>
        /// <param name="lifetime">The lifetime to use</param>
        public DependencyDescription([NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime)
        {
            Dependency = dependency;
            Lifetime = lifetime;
        }
    }
}


#endregion
#region ICannotBuildExpression

namespace IoC.Issues
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Resolves the scenario when cannot build expression.
    /// </summary>
    [PublicAPI]
    public interface ICannotBuildExpression
    {
        /// <summary>
        /// Resolves the scenario when cannot build expression.
        /// </summary>
        /// <param name="buildContext">The build context.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="error">The error.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression Resolve([NotNull] IBuildContext buildContext, [NotNull] IDependency dependency, ILifetime lifetime, Exception error);
    }
}


#endregion
#region ICannotGetGenericTypeArguments

namespace IoC.Issues
{
    using System;

    /// <summary>
    /// Resolves the scenario when cannot extract generic type arguments.
    /// </summary>
    [PublicAPI]
    public interface ICannotGetGenericTypeArguments
    {
        /// <summary>
        /// Resolves the scenario when cannot extract generic type arguments.
        /// </summary>
        /// <param name="type">The instance type.</param>
        /// <returns>The extracted generic type arguments.</returns>
        [NotNull] [ItemNotNull] Type[] Resolve([NotNull] Type type);
    }
}


#endregion
#region ICannotGetResolver

namespace IoC.Issues
{
    using System;

    /// <summary>
    /// Resolves the scenario when cannot get a resolver.
    /// </summary>
    [PublicAPI]
    public interface ICannotGetResolver
    {
        /// <summary>
        /// Resolves the scenario when cannot get a resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="key">The resolving key.</param>
        /// <param name="error">The error.</param>
        /// <returns>The resolver.</returns>
        [NotNull] Resolver<T> Resolve<T>([NotNull] IContainer container, Key key, [NotNull] Exception error);
    }
}


#endregion
#region ICannotParseLifetime

namespace IoC.Issues
{
    /// <summary>
    /// Resolves the scenario when cannot parse a lifetime from a text.
    /// </summary>
    [PublicAPI]
    public interface ICannotParseLifetime
    {
        /// <summary>
        /// Resolves the scenario when cannot parse a lifetime from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a lifetime metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="lifetimeName">The text with a lifetime metadata.</param>
        /// <returns></returns>
        Lifetime Resolve([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string lifetimeName);
    }
}


#endregion
#region ICannotParseTag

namespace IoC.Issues
{
    /// <summary>
    /// Resolves the scenario when cannot parse a tag from a text.
    /// </summary>
    [PublicAPI]
    public interface ICannotParseTag
    {
        /// <summary>
        /// Resolves the scenario when cannot parse a tag from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a tag metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="tag">The text with a tag metadata.</param>
        /// <returns></returns>
        [CanBeNull] object Resolve(string statementText, int statementLineNumber, int statementPosition, [NotNull] string tag);
    }
}


#endregion
#region ICannotParseType

namespace IoC.Issues
{
    using System;

    /// <summary>
    /// Resolves the scenario when cannot parse a type from a text.
    /// </summary>
    [PublicAPI]
    public interface ICannotParseType
    {
        /// <summary>
        /// Resolves the scenario when cannot parse a type from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a type metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="typeName">The text with a type metadata.</param>
        /// <returns></returns>
        [NotNull] Type Resolve([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string typeName);
    }
}


#endregion
#region ICannotRegister

namespace IoC.Issues
{
    /// <summary>
    /// Resolves the scenario when a new binding cannot be registered.
    /// </summary>
    [PublicAPI]
    public interface ICannotRegister
    {
        /// <summary>
        /// Resolves the scenario when a new binding cannot be registered.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="keys">The set of binding keys.</param>
        /// <returns>The dependency token.</returns>
        [NotNull] IToken Resolve([NotNull] IContainer container, [NotNull] Key[] keys);
    }
}


#endregion
#region ICannotResolveConstructor

namespace IoC.Issues
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Resolves the scenario when cannot resolve a constructor.
    /// </summary>
    [PublicAPI]
    public interface ICannotResolveConstructor
    {
        /// <summary>
        /// Resolves the scenario when cannot resolve a constructor.
        /// </summary>
        /// <param name="constructors">Available constructors.</param>
        /// <returns>The constructor.</returns>
        [NotNull] IMethod<ConstructorInfo> Resolve([NotNull] IEnumerable<IMethod<ConstructorInfo>> constructors);
    }
}


#endregion
#region ICannotResolveDependency

namespace IoC.Issues
{
    /// <summary>
    /// Resolves issue with unknown dependency.
    /// </summary>
    [PublicAPI]
    public interface ICannotResolveDependency
    {
        /// <summary>
        /// Resolves the scenario when the dependency was not found.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="key">The resolving key.</param>
        /// <returns>The pair of the dependency and of the lifetime.</returns>
        DependencyDescription Resolve([NotNull] IContainer container, Key key);
    }
}


#endregion
#region ICannotResolveType

namespace IoC.Issues
{
    using System;

    /// <summary>
    /// Resolves the scenario when cannot resolve the instance type.
    /// </summary>
    [PublicAPI]
    public interface ICannotResolveType
    {
        /// <summary>
        /// Resolves the scenario when cannot resolve the instance type.
        /// </summary>
        /// <param name="registeredType">Registered type.</param>
        /// <param name="resolvingType">Resolving type.</param>
        /// <returns>The type to create an instance.</returns>
        Type Resolve([NotNull] Type registeredType, [NotNull] Type resolvingType);
    }
}


#endregion
#region IFoundCyclicDependency

namespace IoC.Issues
{
    /// <summary>
    /// Resolves the scenario when a cyclic dependency was detected.
    /// </summary>
    [PublicAPI]
    public interface IFoundCyclicDependency
    {
        /// <summary>
        /// Resolves the scenario when a cyclic dependency was detected.
        /// </summary>
        /// <param name="key">The resolving key.</param>
        /// <param name="reentrancy">The level of reentrancy.</param>
        void Resolve(Key key, int reentrancy);
    }
}


#endregion

#endregion

#region Core

#region AspectOrientedAutowiringStrategy

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    internal class AspectOrientedAutowiringStrategy: IAutowiringStrategy
    {
        [NotNull] private readonly IAspectOrientedMetadata _metadata;

        public AspectOrientedAutowiringStrategy([NotNull] IAspectOrientedMetadata metadata)
        {
            _metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        /// <inheritdoc />
        public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType)
        {
            instanceType = default(Type);
            // Says that the default logic should be used
            return false;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
        {
            constructor = PrepareMethods(constructors).FirstOrDefault();
            if (constructor == null && DefaultAutowiringStrategy.Shared.TryResolveConstructor(constructors, out var defaultConstructor))
            {
                // Initialize default ctor
                constructor = PrepareMethods(new[] { defaultConstructor }, true).FirstOrDefault();
            }

            // Says that current logic should be used
            return constructor != default(IMethod<ConstructorInfo>);
        }

        /// <inheritdoc />
        public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = PrepareMethods(methods);
            // Says that current logic should be used
            return true;
        }

        private IEnumerable<IMethod<TMethodInfo>> PrepareMethods<TMethodInfo>(IEnumerable<IMethod<TMethodInfo>> methods, bool enforceSelection = false)
            where TMethodInfo : MethodBase =>
            from method in methods
            let methodMetadata = new Metadata(_metadata, method.Info.GetCustomAttributes(true))
            where enforceSelection || !methodMetadata.IsEmpty
            orderby methodMetadata.Order
            where (
                    from parameter in method.Info.GetParameters()
                    let parameterMetadata = new Metadata(_metadata, parameter.GetCustomAttributes(true))
                    select method.TryInjectDependency(parameter.Position, parameterMetadata.Type ?? parameter.ParameterType, parameterMetadata.Tag ?? methodMetadata.Tag))
                .All(isInjected => isInjected)
            select method;

        private struct Metadata
        {
            [CanBeNull] public readonly Type Type;
            [CanBeNull] public readonly IComparable Order;
            [CanBeNull] public readonly object Tag;

            public Metadata(IAspectOrientedMetadata metadata, IEnumerable<object> attributes)
            {
                Type = default(Type);
                Order = null;
                Tag = default(object);
                foreach (var attribute in attributes)
                {
                    if (!(attribute is Attribute attributeValue))
                    {
                        continue;
                    }

                    if (Type == default(Type) && metadata.TryGetType(attributeValue, out var curType))
                    {
                        Type = curType;
                    }

                    if (Order == null && metadata.TryGetOrder(attributeValue, out var curOrder))
                    {
                        Order = curOrder;
                    }

                    if (Tag == default(object) && metadata.TryGetTag(attributeValue, out var curTag))
                    {
                        Tag = curTag;
                    }

                    if (Type != default(Type) && Order != null && Tag != default(object))
                    {
                        break;
                    }
                }
            }

            public bool IsEmpty => Type == default(Type) && Order == null && Tag == default(object);
        }
    }
}


#endregion
#region AspectOrientedMetadata

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    /// <summary>
    /// Metadata for aspect oriented autowiring strategy.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    internal struct AspectOrientedMetadata: IAspectOrientedMetadata, IAutowiringStrategy
    {
        internal static readonly AspectOrientedMetadata Empty = new AspectOrientedMetadata(new Dictionary<Type, Func<Attribute, Type>>(), new Dictionary<Type, Func<Attribute, IComparable>>(), new Dictionary<Type, Func<Attribute, object>>());
        private readonly IDictionary<Type, Func<Attribute, Type>> _typeSelectors; 
        private readonly IDictionary<Type, Func<Attribute, IComparable>> _orderSelectors;
        private readonly IDictionary<Type, Func<Attribute, object>> _tagSelectors;
        private readonly ILockObject _lockObject;
        private volatile IAutowiringStrategy _autowiringStrategy;

        public static AspectOrientedMetadata Type<TTypeAttribute>(AspectOrientedMetadata metadata, Func<TTypeAttribute, Type> typeSelector)
            where TTypeAttribute : Attribute =>
            new AspectOrientedMetadata(
                new Dictionary<Type, Func<Attribute, Type>>(metadata._typeSelectors) { [typeof(TTypeAttribute)] = attribute => typeSelector((TTypeAttribute)attribute) },
                new Dictionary<Type, Func<Attribute, IComparable>>(metadata._orderSelectors),
                new Dictionary<Type, Func<Attribute, object>>(metadata._tagSelectors));

        public static AspectOrientedMetadata Order<TOrderAttribute>(AspectOrientedMetadata metadata, Func<TOrderAttribute, IComparable> orderSelector)
            where TOrderAttribute : Attribute =>
            new AspectOrientedMetadata(
                new Dictionary<Type, Func<Attribute, Type>>(metadata._typeSelectors),
                new Dictionary<Type, Func<Attribute, IComparable>>(metadata._orderSelectors) { [typeof(TOrderAttribute)] = attribute => orderSelector((TOrderAttribute)attribute) },
                new Dictionary<Type, Func<Attribute, object>>(metadata._tagSelectors));

        public static AspectOrientedMetadata Tag<TTagAttribute>(AspectOrientedMetadata metadata, Func<TTagAttribute, object> tagSelector)
            where TTagAttribute : Attribute =>
            new AspectOrientedMetadata(
                new Dictionary<Type, Func<Attribute, Type>>(metadata._typeSelectors),
                new Dictionary<Type, Func<Attribute, IComparable>>(metadata._orderSelectors),
                new Dictionary<Type, Func<Attribute, object>>(metadata._tagSelectors) { [typeof(TTagAttribute)] = attribute => tagSelector((TTagAttribute)attribute) });

        private AspectOrientedMetadata(
            [NotNull] IDictionary<Type, Func<Attribute, Type>> typeSelectors,
            [NotNull] IDictionary<Type, Func<Attribute, IComparable>> orderSelectors,
            [NotNull] IDictionary<Type, Func<Attribute, object>> tagSelectors)
        {
            _lockObject = new LockObject();
            _typeSelectors = typeSelectors;
            _orderSelectors = orderSelectors;
            _tagSelectors = tagSelectors;
            _autowiringStrategy = default(IAutowiringStrategy);
        }

        bool IAspectOrientedMetadata.TryGetType(Attribute attribute, out Type type)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            if (_typeSelectors.TryGetValue(attribute.GetType(), out var selector))
            {
                type = selector(attribute);
                return true;
            }

            type = default(Type);
            return false;
        }

        bool IAspectOrientedMetadata.TryGetOrder(Attribute attribute, out IComparable comparable)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            if (_orderSelectors.TryGetValue(attribute.GetType(), out var selector))
            {
                comparable = selector(attribute);
                return true;
            }

            comparable = default(IComparable);
            return false;
        }

        bool IAspectOrientedMetadata.TryGetTag(Attribute attribute, out object tag)
        {
            if (attribute == null) throw new ArgumentNullException(nameof(attribute));
            if (_tagSelectors.TryGetValue(attribute.GetType(), out var selector))
            {
                tag = selector(attribute);
                return true;
            }

            tag = default(object);
            return false;
        }

        /// <inheritdoc />
        public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType) =>
            GetAutowiringStrategy().TryResolveType(
                registeredType ?? throw new ArgumentNullException(nameof(registeredType)),
                resolvingType ?? throw new ArgumentNullException(nameof(resolvingType)),
                out instanceType);

        /// <inheritdoc />
        public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor) =>
            GetAutowiringStrategy().TryResolveConstructor(
                constructors ?? throw new ArgumentNullException(nameof(constructors)),
                out constructor);

        /// <inheritdoc />
        public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers) =>
            GetAutowiringStrategy().TryResolveInitializers(
                methods ?? throw new ArgumentNullException(nameof(methods)),
                out initializers);

        private IAutowiringStrategy GetAutowiringStrategy()
        {
            if (_autowiringStrategy != default(IAutowiringStrategy))
            {
                return _autowiringStrategy;
            }

            lock (_lockObject)
            {
                if (_autowiringStrategy == default(IAutowiringStrategy))
                {
                    _autowiringStrategy = new AspectOrientedAutowiringStrategy(this);
                }
            }

            return _autowiringStrategy;
        }
    }
}

#endregion
#region Autowiring

namespace IoC.Core
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class Autowiring
    {
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static Expression ApplyInitializers(
            IBuildContext buildContext,
            IAutowiringStrategy autoWiringStrategy,
            TypeDescriptor typeDescriptor,
            bool isComplexType,
            Expression baseExpression,
            Expression[] statements)
        {
            var isDefaultAutoWiringStrategy = DefaultAutowiringStrategy.Shared == autoWiringStrategy;
            var defaultMethods = GetMethods(typeDescriptor.GetDeclaredMethods());
            if (!autoWiringStrategy.TryResolveInitializers(defaultMethods, out var initializers))
            {
                if (isDefaultAutoWiringStrategy || !DefaultAutowiringStrategy.Shared.TryResolveInitializers(defaultMethods, out initializers))
                {
                    initializers = Enumerable.Empty<IMethod<MethodInfo>>();
                }
            }

            var curInitializers = initializers.ToArray();
            if (curInitializers.Length > 0)
            {
                var thisVar = Expression.Variable(baseExpression.Type, "this");
                baseExpression = Expression.Block(
                    new[] { thisVar },
                    Expression.Assign(thisVar, baseExpression),
                    Expression.Block(
                        from initializer in curInitializers
                        select Expression.Call(thisVar, initializer.Info, initializer.GetParametersExpressions(buildContext))
                    ),
                    thisVar
                );
            }

            if (!isDefaultAutoWiringStrategy)
            {
                baseExpression = buildContext.InjectDependencies(baseExpression);
            }

            if (statements.Length == 0)
            {
                return baseExpression;
            }

            var contextItVar = ReplaceTypes(buildContext, isComplexType, Expression.Variable(baseExpression.Type, "ctx.It"));
            baseExpression = Expression.Block(
                new[] { contextItVar },
                Expression.Assign(contextItVar, baseExpression),
                ReplaceTypes(buildContext, isComplexType, Expression.Block(statements)),
                contextItVar
            );

            return buildContext.InjectDependencies(baseExpression, contextItVar);
        }

        public static bool IsComplexType(TypeDescriptor typeDescriptor) => 
            typeDescriptor.IsConstructedGenericType() || typeDescriptor.IsGenericTypeDefinition() || typeDescriptor.IsArray();

        public static T ReplaceTypes<T>(IBuildContext buildContext, bool isComplexType, T expression)
            where T : Expression =>
            isComplexType ? (T)buildContext.ReplaceTypes(expression) : expression;

        [IoC.NotNull]
        [MethodImpl((MethodImplOptions)256)]
        public static IEnumerable<IMethod<TMethodInfo>> GetMethods<TMethodInfo>([IoC.NotNull] IEnumerable<TMethodInfo> methodInfos)
            where TMethodInfo : MethodBase
            => methodInfos
                .Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic))
                .Select(info => new Method<TMethodInfo>(info));
    }
}


#endregion
#region AutowiringDependency

namespace IoC.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    internal class AutowiringDependency: IDependency
    {
        private readonly Expression _expression;
        [CanBeNull] private readonly IAutowiringStrategy _autoWiringStrategy;
        [NotNull] [ItemNotNull] private readonly Expression[] _statements;
        private readonly bool _isComplexType;

        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public AutowiringDependency(
            [NotNull] LambdaExpression constructor,
            [CanBeNull] IAutowiringStrategy autoWiringStrategy = default(IAutowiringStrategy),
            [NotNull][ItemNotNull] params LambdaExpression[] statements)
            :this(
                constructor?.Body ?? throw new ArgumentNullException(nameof(constructor)),
                autoWiringStrategy,
                statements?.Select(i => i.Body)?.ToArray() ?? throw new ArgumentNullException(nameof(statements)))
        {
        }

        public AutowiringDependency(
            [NotNull] Expression constructorExpression,
            [CanBeNull] IAutowiringStrategy autoWiringStrategy = default(IAutowiringStrategy),
            [NotNull][ItemNotNull] params Expression[] statementExpressions)
        {
            _expression = constructorExpression ?? throw new ArgumentNullException(nameof(constructorExpression));
            _autoWiringStrategy = autoWiringStrategy;
            _isComplexType = Autowiring.IsComplexType(_expression.Type.Descriptor());
            _statements = (statementExpressions ?? throw new ArgumentNullException(nameof(statementExpressions))).ToArray();
        }

        public Expression Expression { get; }

        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                baseExpression = Autowiring.ReplaceTypes(buildContext, _isComplexType, _expression);
                baseExpression = Autowiring.ApplyInitializers(
                    buildContext,
                    _autoWiringStrategy ?? buildContext.AutowiringStrategy,
                    baseExpression.Type.Descriptor(),
                    _isComplexType,
                    baseExpression,
                    _statements);

                if (_statements.Length == 0)
                {
                    if (_isComplexType)
                    {
                        baseExpression = buildContext.ReplaceTypes(baseExpression);
                    }

                    baseExpression = buildContext.InjectDependencies(baseExpression);
                }

                baseExpression = buildContext.AddLifetime(baseExpression, lifetime);
                error = default(Exception);
                return true;
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                baseExpression = default(Expression);
                return false;
            }
        }

        public override string ToString() => $"{_expression}";
    }
}


#endregion
#region Binding

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal struct Binding<T>: IBinding<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        public Binding([NotNull] IContainer container, [NotNull][ItemNotNull] params Type[] types)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            Tokens = Enumerable.Empty<IToken>();
            Types = types ?? throw new ArgumentNullException(nameof(types));
            Lifetime = default(ILifetime);
            Tags = Enumerable.Empty<object>();
            AutowiringStrategy = default(IAutowiringStrategy);
        }

        // ReSharper disable once StaticMemberInGenericType
        public Binding([NotNull] IToken token, [NotNull][ItemNotNull] params Type[] types)
        {
            if (token == null) { throw new ArgumentNullException(nameof(token)); }
            Container = token.Container;
            Tokens = new[] { token };
            Types = types ?? throw new ArgumentNullException(nameof(types));
            Lifetime = default(ILifetime);
            Tags = Enumerable.Empty<object>();
            AutowiringStrategy = default(IAutowiringStrategy);
        }

        public Binding([NotNull] IBinding<T> binding, Lifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Tokens = binding.Tokens;
            Types = binding.Types;
            Tags = binding.Tags;
            Lifetime = lifetime != IoC.Lifetime.Transient ? binding.Container.Resolve<ILifetime>(lifetime.AsTag(), binding.Container) : null;
            AutowiringStrategy = binding.AutowiringStrategy;
        }

        public Binding([NotNull] IBinding<T> binding, [NotNull] ILifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Tokens = binding.Tokens;
            Types = binding.Types;
            Lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            Tags = binding.Tags;
            AutowiringStrategy = binding.AutowiringStrategy;
        }

        public Binding([NotNull] IBinding<T> binding, [CanBeNull] object tagValue)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Tokens = binding.Tokens;
            Types = binding.Types;
            Lifetime = binding.Lifetime;
            Tags = binding.Tags.Concat(Enumerable.Repeat(tagValue, 1));
            AutowiringStrategy = binding.AutowiringStrategy;
        }

        public Binding([NotNull] IBinding<T> binding, [NotNull] IAutowiringStrategy autowiringStrategy)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Tokens = binding.Tokens;
            Types = binding.Types;
            Lifetime = binding.Lifetime;
            Tags = binding.Tags;
            AutowiringStrategy = autowiringStrategy;
        }

        public IContainer Container { get; }

        public IEnumerable<IToken> Tokens { get; }

        public IEnumerable<Type> Types { get; }

        public IEnumerable<object> Tags { get; }

        public ILifetime Lifetime { get; }

        public IAutowiringStrategy AutowiringStrategy { get; }
    }
}


#endregion
#region BuildContext

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Issues;

    /// <summary>
    /// Represents build context.
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [PublicAPI]
    internal class BuildContext : IBuildContext
    {
        private static readonly ICollection<IBuilder> EmptyBuilders = new List<IBuilder>();
        private readonly IEnumerable<IDisposable> _resources;
        [NotNull] private readonly IEnumerable<IBuilder> _builders;
        private readonly IDictionary<Type, Type> _typesMap = new Dictionary<Type, Type>();

        internal BuildContext(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IEnumerable<IDisposable> resources,
            [NotNull] IEnumerable<IBuilder> builders,
            [NotNull] IAutowiringStrategy defaultAutowiringStrategy,
            int depth = 0)
        {
            Key = key;
            Container = resolvingContainer ?? throw new ArgumentNullException(nameof(resolvingContainer));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _builders = builders ?? throw new ArgumentNullException(nameof(builders));
            AutowiringStrategy = defaultAutowiringStrategy ?? throw new ArgumentNullException(nameof(defaultAutowiringStrategy));
            Depth = depth;
        }

        public Key Key { get; }

        public IContainer Container { get; }

        public IAutowiringStrategy AutowiringStrategy { get; }

        public int Depth { get; }

        public IBuildContext CreateChild(Key key, IContainer container) => 
            CreateChildInternal(key, container ?? throw new ArgumentNullException(nameof(container)));

        public Expression ReplaceTypes(Expression baseExpression) =>
            TypeReplacerExpressionBuilder.Shared.Build(baseExpression, this, _typesMap);

        public void BindTypes(Type type, Type targetType) =>
            TypeMapper.Shared.Map(type, targetType, _typesMap);

        public bool TryReplaceType(Type type, out Type targetType) =>
            _typesMap.TryGetValue(type, out targetType);

        public Expression InjectDependencies(Expression baseExpression, ParameterExpression instanceExpression = null) =>
            DependencyInjectionExpressionBuilder.Shared.Build(baseExpression, this, instanceExpression);

        public Expression AddLifetime(Expression baseExpression, ILifetime lifetime)
        {
            if (_builders.Any())
            {
                var buildContext = CreateChildInternal(Key, Container, forBuilders: true);
                foreach (var builder in _builders)
                {
                    baseExpression = baseExpression.Convert(Key.Type);
                    baseExpression = builder.Build(buildContext, baseExpression);
                }
            }

            baseExpression = baseExpression.Convert(Key.Type);
            return LifetimeExpressionBuilder.Shared.Build(baseExpression, this, lifetime);
        }

        private IBuildContext CreateChildInternal(Key key, IContainer container, bool forBuilders = false)
        {
            if (_typesMap.TryGetValue(key.Type, out var type))
            {
                key = new Key(type, key.Tag);
            }

            return new BuildContext(key, container, _resources, forBuilders ? EmptyBuilders : _builders, AutowiringStrategy, Depth + 1);
        }

        public Expression DependencyExpression
        {
            get
            {
                if (!Container.TryGetDependency(Key, out var dependency, out var lifetime))
                {
                    try
                    {
                        var dependencyDescription = Container.Resolve<ICannotResolveDependency>().Resolve(Container, Key);
                        dependency = dependencyDescription.Dependency;
                        lifetime = dependencyDescription.Lifetime;
                    }
                    catch (Exception ex)
                    {
                        throw new BuildExpressionException(ex.Message, ex.InnerException);
                    }
                }

                if (Depth >= 128)
                {
                    Container.Resolve<IFoundCyclicDependency>().Resolve(Key, Depth);
                }

                if (dependency.TryBuildExpression(this, lifetime, out var expression, out var error))
                {
                    return expression;
                }

                try
                {
                    return Container.Resolve<ICannotBuildExpression>().Resolve(this, dependency, lifetime, error);
                }
                catch (Exception)
                {
                    throw error;
                }
            }
        }
    }
}


#endregion
#region BuildExpressionException

namespace IoC.Core
{
    using System;

#if !WINDOWS_UWP && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETCOREAPP1_0 && !NETCOREAPP1_1
    [Serializable]
#endif
    internal class BuildExpressionException: InvalidOperationException
    {
        public BuildExpressionException(string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        { }
    }
}


#endregion
#region CannotBuildExpression

namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using Issues;

    internal class CannotBuildExpression : ICannotBuildExpression
    {
        public static readonly ICannotBuildExpression Shared = new CannotBuildExpression();

        private CannotBuildExpression() { }

        public Expression Resolve(IBuildContext buildContext, IDependency dependency, ILifetime lifetime, Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            throw new InvalidOperationException($"Cannot build expression for the key {buildContext.Key} in the container {buildContext.Container}.", error);
        }
    }
}

#endregion
#region CannotGetGenericTypeArguments

namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotGetGenericTypeArguments : ICannotGetGenericTypeArguments
    {
        public static readonly ICannotGetGenericTypeArguments Shared = new CannotGetGenericTypeArguments();

        private CannotGetGenericTypeArguments() { }

        public Type[] Resolve(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            throw new InvalidOperationException($"Cannot get generic type arguments from the type {type.Name}.");
        }
    }
}

#endregion
#region CannotGetResolver

namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotGetResolver : ICannotGetResolver
    {
        public static readonly ICannotGetResolver Shared = new CannotGetResolver();

        private CannotGetResolver() { }

        public Resolver<T> Resolve<T>(IContainer container, Key key, Exception error)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (error == null) throw new ArgumentNullException(nameof(error));
            throw new InvalidOperationException($"Cannot get resolver for the key {key} from the container {container}.", error);
        }
    }
}

#endregion
#region CannotParseLifetime

namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotParseLifetime : ICannotParseLifetime
    {
        public static readonly ICannotParseLifetime Shared = new CannotParseLifetime();

        private CannotParseLifetime() { }

        public Lifetime Resolve(string statementText, int statementLineNumber, int statementPosition, string lifetimeName) => 
            throw new InvalidOperationException($"Cannot parse the lifetime {lifetimeName} in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
    }
}

#endregion
#region CannotParseTag

namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotParseTag : ICannotParseTag
    {
        public static readonly ICannotParseTag Shared = new CannotParseTag();

        private CannotParseTag() { }

        public object Resolve(string statementText, int statementLineNumber, int statementPosition, string tag) => 
            throw new InvalidOperationException($"Cannot parse the tag {tag} in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
    }
}

#endregion
#region CannotParseType

namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotParseType : ICannotParseType
    {
        public static readonly ICannotParseType Shared = new CannotParseType();

        private CannotParseType() { }

        public Type Resolve(string statementText, int statementLineNumber, int statementPosition, string typeName) => 
            throw new InvalidOperationException($"Cannot parse the type {typeName} in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
    }
}

#endregion
#region CannotRegister

namespace IoC.Core
{
    using System;
    using System.Linq;
    using Issues;

    internal class CannotRegister : ICannotRegister
    {
        public static readonly ICannotRegister Shared = new CannotRegister();

        private CannotRegister() { }

        public IToken Resolve(IContainer container, Key[] keys)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            throw new InvalidOperationException($"Keys {string.Join(", ", keys.Select(i => i.ToString()))} cannot be registered in the container {container}.");
        }
    }
}

#endregion
#region CannotResolveConstructor

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Issues;

    internal class CannotResolveConstructor : ICannotResolveConstructor
    {
        public static readonly ICannotResolveConstructor Shared = new CannotResolveConstructor();

        private CannotResolveConstructor() { }

        public IMethod<ConstructorInfo> Resolve(IEnumerable<IMethod<ConstructorInfo>> constructors)
        {
            if (constructors == null) throw new ArgumentNullException(nameof(constructors));
            var type = constructors.Single().Info.DeclaringType;
            throw new InvalidOperationException($"Cannot find a constructor for the type {type}.");
        }
    }
}

#endregion
#region CannotResolveDependency

namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotResolveDependency : ICannotResolveDependency
    {
        public static readonly ICannotResolveDependency Shared = new CannotResolveDependency();

        private CannotResolveDependency() { }

        public DependencyDescription Resolve(IContainer container, Key key)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            throw new InvalidOperationException($"Cannot find the dependency for the key {key} in the container {container}.");
        }
    }
}

#endregion
#region CannotResolveType

namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotResolveType : ICannotResolveType
    {
        public static readonly ICannotResolveType Shared = new CannotResolveType();

        private CannotResolveType() { }

        public Type Resolve(Type registeredType, Type resolvingType)
        {
            if (registeredType == null) throw new ArgumentNullException(nameof(registeredType));
            if (resolvingType == null) throw new ArgumentNullException(nameof(resolvingType));
            throw new InvalidOperationException($"Cannot resolve instance type based on the registered type {registeredType} for resolving type {registeredType}.");
        }
    }
}

#endregion
#region ConfigurationFromDelegate

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal class ConfigurationFromDelegate: IConfiguration
    {
        [NotNull] private readonly Func<IContainer, IToken> _configurationFactory;

        public ConfigurationFromDelegate([NotNull] Func<IContainer, IToken> configurationFactory) => 
            _configurationFactory = configurationFactory ?? throw new ArgumentNullException(nameof(configurationFactory));

        public IEnumerable<IToken> Apply(IContainer container)
        {
            yield return _configurationFactory(container);
        }
    }
}


#endregion
#region ContainerEventToStringConverter

namespace IoC.Core
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using static TypeDescriptorExtensions;

    internal class ContainerEventToStringConverter: IConverter<ContainerEvent, IContainer, string>
    {
        private static readonly PropertyInfo PropertyInfo = Descriptor<Expression>().GetDeclaredProperties().SingleOrDefault(i => i.Name == "DebugView");
        public static readonly IConverter<ContainerEvent, IContainer, string> Shared = new ContainerEventToStringConverter();

        private ContainerEventToStringConverter() { }

        public bool TryConvert(IContainer context, ContainerEvent src, out string dst)
        {
            string text;
            switch (src.EventType)
            {
                case EventType.CreateContainer:
                    text = "was created.";
                    break;

                case EventType.DisposeContainer:
                    text = "was disposed.";
                    break;

                case EventType.RegisterDependency:
                    text = $"adds {FormatDependency(src)}.";
                    break;

                case EventType.UnregisterDependency:
                    text = $"removes {FormatDependency(src)}.";
                    break;

                case EventType.ResolverCompilation:
                    var body = src.ResolverExpression?.Body;
                    text = $"compiles {FormatDependency(src)} from:\n{GetString(GetDebugView(body))}.";
                    break;

                default:
                    dst = default(string);
                    return false;
            }

            dst = FormatPrefix(context) + text;
            return true;
        }

        [CanBeNull] private static string GetDebugView([CanBeNull] Expression expression) => 
            expression == null ? null : PropertyInfo?.GetValue(expression, null) as string ?? expression.ToString();

        [NotNull] private static string FormatDependency(ContainerEvent containerEvent)
        {
            var lifetime = containerEvent.Lifetime != null ? $" as {GetString(containerEvent.Lifetime, string.Empty)}" : string.Empty;
            return $"{FormatKeys(containerEvent)} implemented by {Quoted(GetString(containerEvent.Dependency))}{lifetime}";
        }

        [NotNull] private static string FormatKeys(ContainerEvent containerEvent) => 
            containerEvent.Keys != null ? string.Join(", ", containerEvent.Keys.Select(FormatKey)) : "";

        [NotNull] private static string FormatKey(Key key)
        {
            string tag;
            if (key.Tag == Key.AnyTag)
            {
                tag = " of any";
            }
            else
            {
                if (key.Tag == null)
                {
                    tag = string.Empty;
                }
                else
                {
                    tag = $" of {key.Tag}";
                }
            }

            return $"{key.Type.Descriptor()}{tag}";
        }

        [NotNull] private static string FormatPrefix([NotNull] IContainer container) => $"[{container}] ";

        [NotNull] private static string GetString<T>([CanBeNull] T value, string defaultString = "null")
            where T: class =>
            value?.ToString() ?? defaultString;

        [NotNull] private static string Quoted([NotNull] string text) =>
            $"\"{text}\"";
    }
}


#endregion
#region CoreExtensions

// ReSharper disable LoopCanBeConvertedToQuery
namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class CoreExtensions
    {
        [MethodImpl((MethodImplOptions)256)]
        public static bool SequenceEqual<T>([NotNull] this T[] array1, [NotNull] T[] array2) =>
            ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);

        [MethodImpl((MethodImplOptions)256)]
        public static int GetHash<T>([NotNull][ItemNotNull] this T[] items)
        {
            unchecked
            {
                var code = 0;
                // ReSharper disable once ForCanBeConvertedToForeach
                // ReSharper disable once LoopCanBeConvertedToQuery
                for (var i = 0; i < items.Length; i++)
                {
                    code = (code * 397) ^ items[i].GetHashCode();
                }

                return code;
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        public static T[] EmptyArray<T>() =>
            Empty<T>.Array;

        [MethodImpl((MethodImplOptions) 256)]
        public static T[] CreateArray<T>(int size = 0, T value = default(T))
        {
            if (size == 0)
            {
                return EmptyArray<T>();
            }

            var array = new T[size];
            for (var i = 0; i < size; i++)
            {
                array[i] = value;
            }

            return array;
        }

        [MethodImpl((MethodImplOptions) 256)]
        [Pure]
        [NotNull]
        public static T[] Add<T>([NotNull] this T[] source, [CanBeNull] T value)
        {
            var length = source.Length;
            var destination = new T[length + 1];
            Array.Copy(
                source,
                source.GetLowerBound(0),
                destination,
                destination.GetLowerBound(0),
                length);
            destination[length] = value;
            return destination;
        }

        [MethodImpl((MethodImplOptions) 256)]
        [Pure]
        public static T[] Copy<T>([NotNull] this T[] previous)
        {
            var length = previous.Length;
            var result = new T[length];
            Array.Copy(previous, result, length);
            return result;
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public static TValue Get<TKey, TValue>(this Table<TKey, TValue> table, int hashCode, TKey key)
            where TKey: struct
            where TValue : class
        {
            var items = table.Buckets[hashCode & table.Divisor].KeyValues;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (item.Key.Equals(key))
                {
                    return item.Value;
                }
            }

            return default(TValue);
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public static TValue GetByRef<TKey, TValue>(this Table<TKey, TValue> table, int hashCode, TKey key)
            where TKey: class
            where TValue : class
        {
            var items = table.Buckets[hashCode & table.Divisor].KeyValues;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (item.Key == key)
                {
                    return item.Value;
                }
            }

            return default(TValue);
        }

        private static class Empty<T>
        {
            public static readonly T[] Array = new T[0];
        }
    }
}


#endregion
#region DefaultAutowiringStrategy

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class DefaultAutowiringStrategy: IAutowiringStrategy
    {
        public static readonly IAutowiringStrategy Shared = new DefaultAutowiringStrategy();

        private DefaultAutowiringStrategy()
        { }

        [MethodImpl((MethodImplOptions)256)]
        public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType)
        {
            instanceType = default(Type);
            return false;
        }

        public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
            => (constructor = constructors.OrderBy(i => GetOrder(i.Info)).FirstOrDefault()) != null;

        public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = Enumerable.Empty<IMethod<MethodInfo>>();
            return true;
        }

        [MethodImpl((MethodImplOptions)256)]
        private static int GetOrder(MethodBase method)
        {
            var order = method.GetParameters().Length + 1;

            if (method.GetCustomAttributes(typeof(ObsoleteAttribute), true).Any())
            {
                order <<= 4;
            }

            if (!method.IsPublic)
            {
                order <<= 8;
            }

            return order;
        }
    }
}


#endregion
#region DefaultCompiler

namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal class DefaultCompiler : ICompiler
    {
        public static readonly ICompiler Shared = new DefaultCompiler();

        private DefaultCompiler() { }

        public bool TryCompile(IBuildContext context, LambdaExpression expression, out Delegate resolver)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (expression.CanReduce)
            {
                expression = (LambdaExpression)expression.Reduce();
            }

            resolver = expression.Compile();
            return true;
        }
    }
}

#endregion
#region DependencyEntry

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using static System.String;

    internal sealed class DependencyEntry : IToken
    {
        /// <summary>
        /// All resolvers parameters.
        /// </summary>
        [NotNull] [ItemNotNull] internal static readonly IEnumerable<ParameterExpression> ResolverParameters = new List<ParameterExpression> { WellknownExpressions.ContainerParameter, WellknownExpressions.ArgsParameter };

        [CanBeNull] internal readonly ILifetime Lifetime;
        [NotNull] internal readonly IEnumerable<Key> Keys;
        [NotNull] private readonly ILockObject _lockObject;
        [NotNull] internal readonly IDependency Dependency;

        [NotNull] private readonly IEnumerable<IDisposable> _resources;
        private volatile Table<LifetimeKey, ILifetime> _lifetimes = Table<LifetimeKey, ILifetime>.Empty;
        private bool _disposed;

        public DependencyEntry(
            [NotNull] ILockObject lockObject,
            [NotNull] IContainer container,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource,
            [NotNull] IEnumerable<Key> keys)
        {
            Container = container;
            _lockObject = lockObject;
            Dependency = dependency;
            Lifetime = lifetime;
            Keys = keys;
            if (lifetime is IDisposable disposableLifetime)
            {
                _resources = new[] { resource, disposableLifetime };
            }
            else
            {
                _resources = new[] { resource };
            }
        }

        public IContainer Container { get; }

        public bool TryCreateResolver(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IRegistrationTracker registrationTracker,
            [NotNull] IObserver<ContainerEvent> eventObserver,
            out Delegate resolver,
            out Exception error)
        {
            lock (_lockObject)
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(DependencyEntry));
                }
            }

            var typeDescriptor = key.Type.Descriptor();
            var buildContext = new BuildContext(key, resolvingContainer, _resources, registrationTracker.Builders, registrationTracker.AutowiringStrategy);
            var lifetime = GetLifetime(typeDescriptor);
            if (!Dependency.TryBuildExpression(buildContext, lifetime, out var expression, out error))
            {
                resolver = default(Delegate);
                return false;
            }

            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), expression, false, ResolverParameters);
            resolver = default(Delegate);
            try
            {
                foreach (var compiler in registrationTracker.Compilers)
                {
                    if (compiler.TryCompile(buildContext, resolverExpression, out resolver))
                    {
                        eventObserver.OnNext(new ContainerEvent(Container, EventType.ResolverCompilation) {Keys = Enumerable.Repeat(key, 1), Dependency = Dependency, Lifetime = lifetime, ResolverExpression = resolverExpression });
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                eventObserver.OnNext(new ContainerEvent(Container, EventType.ResolverCompilation) { Keys = Enumerable.Repeat(key, 1), Dependency = Dependency, Lifetime = lifetime, ResolverExpression = resolverExpression, IsSuccess = false, Error = ex });
                error = ex;
                return false;
            }

            error = default(Exception);
            return resolver != default(Delegate);
        }

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public ILifetime GetLifetime([NotNull] Type type) => GetLifetime(type.Descriptor());

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        private ILifetime GetLifetime(TypeDescriptor typeDescriptor)
        {
            if (Lifetime == default(ILifetime))
            {
                return default(ILifetime);
            }
            
            if (!typeDescriptor.IsConstructedGenericType())
            {
                return Lifetime;
            }

            var lifetimeKey = new LifetimeKey(typeDescriptor.GetGenericTypeArguments());
            var lifetimeHashCode = lifetimeKey.GetHashCode();
            lock (_lockObject)
            {
                var lifetime = _lifetimes.Get(lifetimeHashCode, lifetimeKey);
                if (lifetime == default(ILifetime))
                {

                    lifetime = Lifetime.Create();
                    _lifetimes = _lifetimes.Set(lifetimeHashCode, lifetimeKey, lifetime);
                }

                return lifetime;
            }
        }

        public void Dispose()
        {
            lock (_lockObject)
            {
                if (_disposed)
                {
                    return;
                }
                
                _disposed = true;
            }
            
            foreach (var resource in _resources)
            {
                resource.Dispose();
            }
            
            foreach (var lifetime in _lifetimes)
            {
                lifetime.Value.Dispose();
            }
        }

        public override string ToString() => $"{Join(", ", Keys.Select(i => i.ToString()))} as {Lifetime?.ToString() ?? IoC.Lifetime.Transient.ToString()}";

        private struct LifetimeKey
        {
            private readonly Type[] _genericTypes;
            private readonly int _hashCode;

            public LifetimeKey(Type[] genericTypes)
            {
                _genericTypes = genericTypes;
                _hashCode = genericTypes.GetHash();
            }

            // ReSharper disable once PossibleNullReferenceException
            public override bool Equals(object obj) => Equals((LifetimeKey)obj);

            public override int GetHashCode() => _hashCode;

            private bool Equals(LifetimeKey other) => CoreExtensions.SequenceEqual(_genericTypes, other._genericTypes);
        }
    }
}


#endregion
#region DependencyInjectionExpressionBuilder

namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal class DependencyInjectionExpressionBuilder: IExpressionBuilder<Expression>
    {
        public static readonly IExpressionBuilder<Expression> Shared = new DependencyInjectionExpressionBuilder();

        private DependencyInjectionExpressionBuilder()
        { }

        public Expression Build(Expression bodyExpression, IBuildContext buildContext, Expression thisExpression)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var visitor = new DependencyInjectionExpressionVisitor(buildContext, thisExpression);
            var newExpression = visitor.Visit(bodyExpression);
            return newExpression ?? bodyExpression;
        }
    }
}


#endregion
#region DependencyInjectionExpressionVisitor

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using static WellknownExpressions;
    using static TypeDescriptorExtensions;
    // ReSharper disable once RedundantNameQualifier
    using IContainer = IoC.IContainer;

    internal class DependencyInjectionExpressionVisitor: ExpressionVisitor
    {
        private static readonly Key ContextKey = TypeDescriptor<Context>.Key;
        private static readonly TypeDescriptor ContextTypeDescriptor = TypeDescriptor<Context>.Descriptor;
        private static readonly TypeDescriptor GenericContextTypeDescriptor = typeof(Context<>).Descriptor();
        [NotNull] private static readonly ConstructorInfo ContextConstructor;
        [NotNull] private readonly IContainer _container;
        [NotNull] private readonly IBuildContext _buildContext;
        [CanBeNull] private readonly Expression _thisExpression;

        static DependencyInjectionExpressionVisitor()
        {
            ContextConstructor = Descriptor<Context>().GetDeclaredConstructors().Single();
        }

        public DependencyInjectionExpressionVisitor([NotNull] IBuildContext buildContext, [CanBeNull] Expression thisExpression)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            _container = buildContext.Container;
            _buildContext = buildContext;
            _thisExpression = thisExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression methodCall)
        {
            if (methodCall.Method.IsGenericMethod)
            {
                // container.Inject<T>()
                if (Equals(methodCall.Method.GetGenericMethodDefinition(), Injections.InjectGenericMethodInfo))
                {
                    // container
                    var containerExpression = Visit(methodCall.Arguments[0]);
                    // type
                    var type = methodCall.Method.GetGenericArguments()[0];

                    var key = new Key(type);
                    return CreateDependencyExpression(key, containerExpression);
                }

                // container.Inject<T>(tag)
                if (Equals(methodCall.Method.GetGenericMethodDefinition(), Injections.InjectWithTagGenericMethodInfo))
                {
                    // container
                    var containerExpression = Visit(methodCall.Arguments[0]);

                    // type
                    var type = methodCall.Method.GetGenericArguments()[0];

                    // tag
                    var tagExpression = methodCall.Arguments[1];
                    var tag = GetTag(tagExpression);

                    var key = new Key(type, tag);
                    return CreateDependencyExpression(key, containerExpression);
                }

                // container.Inject<T>(destination, source)
                if (Equals(methodCall.Method.GetGenericMethodDefinition(), Injections.InjectingAssignmentGenericMethodInfo))
                {
                    var dstExpression = Visit(methodCall.Arguments[1]);
                    var srcExpression = Visit(methodCall.Arguments[2]);
                    if (dstExpression != null && srcExpression != null)
                    {
                        return Expression.Assign(dstExpression, srcExpression);
                    }
                }
            }
            else
            {
                // container.Inject(type)
                if (Equals(methodCall.Method, Injections.InjectMethodInfo))
                {
                    // container
                    var containerExpression = Visit(methodCall.Arguments[0]);
                    // type
                    // ReSharper disable once NotResolvedInText
                    var type = (Type)((ConstantExpression)Visit(methodCall.Arguments[1]) ?? throw new BuildExpressionException("Invalid argument", new ArgumentException("Invalid argument", "type"))).Value;

                    var key = new Key(type);
                    return CreateDependencyExpression(key, containerExpression);
                }

                // container.Inject(type, tag)
                if (Equals(methodCall.Method, Injections.InjectWithTagMethodInfo))
                {
                    // container
                    var containerExpression = Visit(methodCall.Arguments[0]);
                    // type
                    // ReSharper disable once NotResolvedInText
                    var type = (Type)((ConstantExpression)Visit(methodCall.Arguments[1]) ?? throw new BuildExpressionException("Invalid argument", new ArgumentException("Invalid argument", "type"))).Value;
                    // tag
                    var tagExpression = methodCall.Arguments[2];
                    var tag = GetTag(tagExpression);

                    var key = new Key(type, tag);
                    return CreateDependencyExpression(key, containerExpression);
                }
            }

            // ctx.It
            if (methodCall.Object is MemberExpression memberExpression && memberExpression.Member is FieldInfo fieldInfo)
            {
                var typeDescriptor = fieldInfo.DeclaringType.Descriptor();
                if (typeDescriptor.IsConstructedGenericType() && typeDescriptor.GetGenericTypeDefinition() == typeof(Context<>) && fieldInfo.Name == nameof(Context<object>.It))
                {
                    return Expression.Call(_thisExpression, methodCall.Method, InjectAll(methodCall.Arguments));
                }
            }

            if (methodCall.Object == null)
            {
                return Expression.Call(methodCall.Method, InjectAll(methodCall.Arguments));
            }

            return Expression.Call(Visit(methodCall.Object), methodCall.Method, InjectAll(methodCall.Arguments));
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var result = base.VisitUnary(node);

            if (result is UnaryExpression unaryExpression)
            {
                switch (unaryExpression.NodeType)
                {
                    case ExpressionType.Convert:
                        var baseType = unaryExpression.Type.Descriptor();
                        var type = unaryExpression.Operand.Type.Descriptor();
                        if (baseType.IsValueType() == type.IsValueType() && baseType.IsAssignableFrom(type))
                        {
                            return unaryExpression.Operand;
                        }

                        break;
                }
            }

            return result;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (TryReplaceContextFields(node.Member.DeclaringType, node.Member.Name, out var expression))
            {
                return expression;
            }

            return base.VisitMember(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Type == TypeDescriptor<Context>.Type)
            {
                return CreateNewContextExpression();
            }

            var typeDescriptor = node.Type.Descriptor();
            if (typeDescriptor.IsConstructedGenericType() && typeDescriptor.GetGenericTypeDefinition() == typeof(Context<>))
            {
                var contextType = GenericContextTypeDescriptor.MakeGenericType(typeDescriptor.GetGenericTypeArguments()).Descriptor();
                var ctor = contextType.GetDeclaredConstructors().Single();
                return Expression.New(
                    ctor,
                    _thisExpression,
                    Expression.Constant(_buildContext.Key),
                    ContainerParameter,
                    ArgsParameter);
            }

            return base.VisitParameter(node);
        }

        [NotNull]
        private Expression CreateNewContextExpression() =>
            Expression.New(
                ContextConstructor,
                Expression.Constant(_buildContext.Key),
                ContainerParameter,
                ArgsParameter);

        [CanBeNull]
        private object GetTag([NotNull] Expression tagExpression)
        {
            if (tagExpression == null) throw new ArgumentNullException(nameof(tagExpression));
            object tag;
            switch (tagExpression)
            {
                case ConstantExpression constant:
                    tag = constant.Value;
                    break;

                default:
                    var expression = Visit(tagExpression) ?? throw new BuildExpressionException($"Invalid tag expression {tagExpression}.", new InvalidOperationException());
                    var tagFunc = Expression.Lambda<Func<object>>(expression, true).Compile();
                    tag = tagFunc();
                    break;
            }

            return tag;
        }

        [NotNull]
        private IEnumerable<Expression> InjectAll(IEnumerable<Expression> expressions) => 
            expressions.Select(Visit);

        [NotNull]
        private IContainer SelectedContainer([NotNull] Expression containerExpression)
        {
            if (containerExpression is ParameterExpression parameterExpression && parameterExpression.Type == TypeDescriptor<IContainer>.Type)
            {
                return _container;
            }

            var containerSelectorExpression = Expression.Lambda<ContainerSelector>(containerExpression, true, ContainerParameter);
            var selectContainer = containerSelectorExpression.Compile();
            return selectContainer(_container);
        }

        [NotNull]
        private Expression CreateDependencyExpression(Key key, [CanBeNull] Expression containerExpression)
        {
            if (Equals(key, ContextKey))
            {
                return CreateNewContextExpression();
            }

            var selectedContainer = containerExpression != null ? SelectedContainer(containerExpression) : _container;
            return _buildContext.CreateChild(key, selectedContainer).DependencyExpression;
        }

        private bool TryReplaceContextFields([CanBeNull] Type type, string name, out Expression expression)
        {
            if (type == null)
            {
                expression = default(Expression);
                return false;
            }

            var typeDescriptor = type.Descriptor();
            if (ContextTypeDescriptor.IsAssignableFrom(typeDescriptor))
            {
                // ctx.Key
                if (name == nameof(Context.Key))
                {
                    expression = Expression.Constant(_buildContext.Key);
                    return true;
                }

                // ctx.Container
                if (name == nameof(Context.Container))
                {
                    expression = ContainerParameter;
                    return true;
                }

                // ctx.Args
                if (name == nameof(Context.Args))
                {
                    expression = ArgsParameter;
                    return true;
                }
            }

            if (typeDescriptor.IsConstructedGenericType())
            {
                if (GenericContextTypeDescriptor.IsAssignableFrom(typeDescriptor.GetGenericTypeDefinition().Descriptor()))
                {
                    // ctx.It
                    if (name == nameof(Context<object>.It))
                    {
                        expression = _thisExpression;
                        return true;
                    }
                }
            }

            expression = default(Expression);
            return false;
        }

        private delegate IContainer ContainerSelector(IContainer container);
    }
}


#endregion
#region Disposable

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal static class Disposable
    {
        [NotNull]
        public static readonly IDisposable Empty = EmptyDisposable.Shared;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Create([NotNull] Action action)
        {
#if DEBUG
            if (action == null) throw new ArgumentNullException(nameof(action));
#endif
            return new DisposableAction(action);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Create([NotNull][ItemCanBeNull] IEnumerable<IDisposable> disposables)
        {
#if DEBUG
            if (disposables == null) throw new ArgumentNullException(nameof(disposables));
#endif
            return new CompositeDisposable(disposables);
        }

#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
        public static IDisposable ToDisposable([NotNull] this IAsyncDisposable asyncDisposable)
        {
#if DEBUG
            if (asyncDisposable == null) throw new ArgumentNullException(nameof(asyncDisposable));
#endif
            return new DisposableAction(() => { asyncDisposable.DisposeAsync().AsTask().Wait(); }, asyncDisposable);
        }
#endif
        private sealed class DisposableAction : IDisposable
        {
            [NotNull] private readonly Action _action;
            [CanBeNull] private readonly object _key;
            private int _counter;

            public DisposableAction([NotNull] Action action, [CanBeNull] object key = null)
            {
                _action = action;
                _key = key ?? action;
            }

            public void Dispose()
            {
                if (Interlocked.Increment(ref _counter) != 1) return;
                _action();
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is DisposableAction other && Equals(_key, other._key);
            }

            public override int GetHashCode() => 
                _key != null ? _key.GetHashCode() : 0;
        }

        private sealed class CompositeDisposable : IDisposable
        {
            private readonly IEnumerable<IDisposable> _disposables;
            private int _counter;

            public CompositeDisposable(IEnumerable<IDisposable> disposables)
                => _disposables = disposables;

            public void Dispose()
            {
                if (Interlocked.Increment(ref _counter) != 1) return;
                foreach (var disposable in _disposables)
                {
                    disposable?.Dispose();
                }
            }
        }

        private class EmptyDisposable: IDisposable
        {
            [NotNull] public static readonly IDisposable Shared = new EmptyDisposable();

            private EmptyDisposable() { }

            public void Dispose() { }
        }
    }
}


#endregion
#region ExpressionBuilderExtensions

namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using static TypeDescriptorExtensions;

    internal static class ExpressionBuilderExtensions
    {
        private static readonly TypeDescriptor ResolverGenericTypeDescriptor = typeof(Resolver<>).Descriptor();
        internal static readonly MethodInfo GetHashCodeMethodInfo = Descriptor<object>().GetDeclaredMethods().Single(i => i.Name == nameof(GetHashCode));
        internal static readonly Expression NullConst = Expression.Constant(null);
        private static readonly MethodInfo EnterMethodInfo = typeof(Monitor).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(Monitor.Enter) && i.GetParameters().Length == 1);
        private static readonly MethodInfo ExitMethodInfo = typeof(Monitor).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(Monitor.Exit));

        [MethodImpl((MethodImplOptions)256)]
        public static Expression Convert(this Expression expression, Type type)
        {
            var baseTypeDescriptor = expression.Type.Descriptor();
            var typeDescriptor = type.Descriptor();
            if (typeDescriptor.IsAssignableFrom(baseTypeDescriptor))
            {
                return expression;
            }

            return Expression.Convert(expression, type);
        }

        [MethodImpl((MethodImplOptions)256)]
        public static Type ToResolverType(this Type type) =>
            ResolverGenericTypeDescriptor.MakeGenericType(type);

        [MethodImpl((MethodImplOptions)256)]
        public static Expression Lock(this Expression body, Expression lockObject) =>
            Expression.TryFinally(
                Expression.Block(
                    Expression.Call(EnterMethodInfo, lockObject),
                    body), 
                Expression.Call(ExitMethodInfo, lockObject));
    }
}

#endregion
#region FluentRegister

// ReSharper disable RedundantNameQualifier
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Issues;

    /// <summary>
    /// Represents extensions to register a dependency in the container.
    /// </summary>
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    [PublicAPI]
    internal static partial class FluentRegister
    {
        private static readonly IEnumerable<object> DefaultTags = new object[] { null };

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [IoC.NotNull]
        public static IToken Register<T>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null) 
            => container.Register(new[] { TypeDescriptor<T>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [IoC.NotNull]
        public static IToken Register<T>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [IoC.NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            => container.Register(new[] { TypeDescriptor<T>.Type }, new AutowiringDependency(factory, null, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="types">The set of types.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        [IoC.NotNull]
        public static IToken Register([NotNull] this IContainer container, [NotNull][ItemNotNull] IEnumerable<Type> types, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime = null, [CanBeNull][ItemCanBeNull] params object[] tags)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var keys =
                from type in types
                from tag in tags ?? DefaultTags
                select new Key(type, tag);

            return container.TryRegisterDependency(keys, dependency, lifetime, out var dependencyToken) 
                ? dependencyToken
                : container.Resolve<ICannotRegister>().Resolve(container, keys.ToArray());
        }
    }
}

#endregion
#region FluentRegisterGenerated

namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents extensions to add bindings to the container.
    /// </summary>
    internal static partial class FluentRegister
    {
        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T: T1
            => container.Register(new[] { TypeDescriptor<T1>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T: T1
            // ReSharper disable once CoVariantArrayConversion
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type }, new AutowiringDependency(factory, null, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1, T2>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T: T1, T2
            => container.Register(new[] { TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1, T2>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T: T1, T2
            // ReSharper disable once CoVariantArrayConversion
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type }, new AutowiringDependency(factory, null, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1, T2, T3>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T: T1, T2, T3
            => container.Register(new[] { TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Register<T, T1, T2, T3>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T: T1, T2, T3
            // ReSharper disable once CoVariantArrayConversion
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type }, new AutowiringDependency(factory, null, statements), lifetime, tags);

    }
}

#endregion
#region FoundCyclicDependency

namespace IoC.Core
{
    using System;
    using Issues;

    internal class FoundCyclicDependency : IFoundCyclicDependency
    {
        public static readonly IFoundCyclicDependency Shared = new FoundCyclicDependency();

        private FoundCyclicDependency() { }

        public void Resolve(Key key, int reentrancy)
        {
            if (reentrancy <= 0) throw new ArgumentOutOfRangeException(nameof(reentrancy));
            if (reentrancy >= 256)
            {
                throw new InvalidOperationException($"The cyclic dependency detected resolving the dependency {key}. The reentrancy is {reentrancy}.");
            }
        }
    }
}

#endregion
#region FullAutowiringDependency

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Issues;

    internal class FullAutowiringDependency: IDependency
    {
        private static readonly Type[] GenericTypeArguments =
        {
            TypeDescriptor<TT>.Type,
            TypeDescriptor<TT1>.Type,
            TypeDescriptor<TT2>.Type,
            TypeDescriptor<TT3>.Type,
            TypeDescriptor<TT4>.Type,
            TypeDescriptor<TT5>.Type,
            TypeDescriptor<TT6>.Type,
            TypeDescriptor<TT7>.Type,
            TypeDescriptor<TT8>.Type,
            TypeDescriptor<TT9>.Type,
            TypeDescriptor<TT10>.Type,
            TypeDescriptor<TT11>.Type,
            TypeDescriptor<TT12>.Type,
            TypeDescriptor<TT13>.Type,
            TypeDescriptor<TT14>.Type,
            TypeDescriptor<TT15>.Type
        };

        [NotNull] private readonly Type _type;
        [CanBeNull] private readonly IAutowiringStrategy _autoWiringStrategy;
        private readonly bool _hasGenericParamsWithConstraints;
        private readonly Dictionary<int, TypeDescriptor> _genericParamsWithConstraints = new Dictionary<int, TypeDescriptor>();
        private readonly Type[] _registeredGenericTypeParameters;
        private readonly TypeDescriptor _registeredTypeDescriptor;
        [NotNull] [ItemNotNull] private readonly Expression[] _statements;
        private readonly bool _isComplexType;

        public FullAutowiringDependency(
            [NotNull] Type type,
            [CanBeNull] IAutowiringStrategy autoWiringStrategy = null,
            [NotNull][ItemNotNull] params LambdaExpression[] statements)
        {
            if (statements == null) throw new ArgumentNullException(nameof(statements));
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _autoWiringStrategy = autoWiringStrategy;
            _statements = statements.Select(i => i.Body).ToArray();
            _registeredTypeDescriptor = type.Descriptor();
            _isComplexType = Autowiring.IsComplexType(_registeredTypeDescriptor);

            if (_registeredTypeDescriptor.IsInterface())
            {
                throw new ArgumentException($"Type \"{type}\" should not be an interface.", nameof(type));
            }

            if (_registeredTypeDescriptor.IsAbstract())
            {
                throw new ArgumentException($"Type \"{type}\" should not be an abstract class.", nameof(type));
            }

            if (!_registeredTypeDescriptor.IsGenericTypeDefinition())
            {
                return;
            }

            _registeredGenericTypeParameters = _registeredTypeDescriptor.GetGenericTypeParameters();
            var genericTypePos = 0;
            var typesMap = new Dictionary<Type, Type>();
            for (var position = 0; position < _registeredGenericTypeParameters.Length; position++)
            {
                var genericType = _registeredGenericTypeParameters[position];
                if (!genericType.IsGenericParameter)
                {
                    continue;
                }

                var descriptor = genericType.Descriptor();
                if (!descriptor.GetGenericParameterConstraints().Any())
                {
                    if (!typesMap.TryGetValue(genericType, out var curType))
                    {
                        try
                        {
                            curType = GenericTypeArguments[genericTypePos++];
                            typesMap[genericType] = curType;
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            throw new BuildExpressionException("Too many generic arguments.", ex);
                        }
                    }

                    _registeredGenericTypeParameters[position] = curType;
                }
                else
                {
                    _genericParamsWithConstraints[position] = descriptor;
                }
            }

            if (_genericParamsWithConstraints.Count == 0)
            {
                _type = _registeredTypeDescriptor.MakeGenericType(_registeredGenericTypeParameters);
            }
            else
            {
                _hasGenericParamsWithConstraints = true;
            }
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                var autoWiringStrategy = _autoWiringStrategy ?? buildContext.AutowiringStrategy;
                var isDefaultAutoWiringStrategy = DefaultAutowiringStrategy.Shared == autoWiringStrategy;
                if (!autoWiringStrategy.TryResolveType(_type, buildContext.Key.Type, out var instanceType))
                {
                    instanceType = _hasGenericParamsWithConstraints
                        ? GetInstanceTypeBasedOnTargetGenericConstrains(buildContext.Key.Type) ?? buildContext.Container.Resolve<ICannotResolveType>().Resolve(_type, buildContext.Key.Type)
                        : _type;
                }

                var typeDescriptor = instanceType.Descriptor();
                if (typeDescriptor.IsConstructedGenericType())
                {
                    buildContext.BindTypes(instanceType, buildContext.Key.Type);
                    var genericArgs = typeDescriptor.GetGenericTypeArguments();
                    var isReplaced = false;
                    for (var position = 0; position < genericArgs.Length; position++)
                    {
                        if (buildContext.TryReplaceType(genericArgs[position], out var type))
                        {
                            genericArgs[position] = type;
                            isReplaced = true;
                        }
                    }

                    if (isReplaced)
                    {
                        typeDescriptor = typeDescriptor.GetGenericTypeDefinition().MakeGenericType(genericArgs).Descriptor();
                    }
                }

                var defaultConstructors = Autowiring.GetMethods(typeDescriptor.GetDeclaredConstructors());
                if (!autoWiringStrategy.TryResolveConstructor(defaultConstructors, out var ctor))
                {
                    if (isDefaultAutoWiringStrategy || !DefaultAutowiringStrategy.Shared.TryResolveConstructor(defaultConstructors, out ctor))
                    {
                        ctor = buildContext.Container.Resolve<ICannotResolveConstructor>().Resolve(defaultConstructors);
                    }
                }

                baseExpression = Autowiring.ApplyInitializers(
                    buildContext,
                    autoWiringStrategy,
                    typeDescriptor,
                    _isComplexType,
                    Expression.New(ctor.Info, ctor.GetParametersExpressions(buildContext)),
                    _statements);

                baseExpression = buildContext.AddLifetime(baseExpression, lifetime);
                error = default(Exception);
                return true;
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                baseExpression = default(Expression);
                return false;
            }
        }

        [CanBeNull]
        internal Type GetInstanceTypeBasedOnTargetGenericConstrains(Type targetType)
        {
            var registeredGenericTypeParameters = new Type[_registeredGenericTypeParameters.Length];
            Array.Copy(_registeredGenericTypeParameters, registeredGenericTypeParameters, _registeredGenericTypeParameters.Length);
            var resolvingTypeDescriptor = targetType.Descriptor();
            var resolvingTypeDefinitionDescriptor = resolvingTypeDescriptor.GetGenericTypeDefinition().Descriptor();
            var resolvingTypeDefinitionGenericTypeParameters = resolvingTypeDefinitionDescriptor.GetGenericTypeParameters();
            var constraintsMap = resolvingTypeDescriptor
                .GetGenericTypeArguments()
                .Zip(resolvingTypeDefinitionGenericTypeParameters, (type, typeDefinition) => Tuple.Create(type, typeDefinition.Descriptor().GetGenericParameterConstraints()))
                .ToArray();

            var canBeResolved = true;
            foreach (var item in _genericParamsWithConstraints)
            {
                var position = item.Key;
                var descriptor = item.Value;
                var constraints = descriptor.GetGenericParameterConstraints();

                var isDefined = false;
                foreach (var constraintsEntry in constraintsMap)
                {
                    if (!CoreExtensions.SequenceEqual(constraints, constraintsEntry.Item2))
                    {
                        continue;
                    }

                    registeredGenericTypeParameters[position] = constraintsEntry.Item1;
                    isDefined = true;
                    break;
                }

                if (!isDefined)
                {
                    canBeResolved = false;
                    break;
                }
            }

            return canBeResolved ? _registeredTypeDescriptor.MakeGenericType(registeredGenericTypeParameters) : null;
        }

        public override string ToString() => $"new {_type.Descriptor()}(...)";
    }
}


#endregion
#region Holder

namespace IoC.Core
{
    using System;

    internal class Holder<TInstance> : IHolder<TInstance>
    {
        [NotNull] private readonly IToken _token;

        public Holder([NotNull] IToken token, [NotNull] TInstance instance)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            Instance = instance;
        }

        public IContainer Container => _token.Container;

        public TInstance Instance { get; }

        public void Dispose()
        {
            using (_token.Container)
            using (_token)
            {
                if (Instance is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}

#endregion
#region IArray

namespace IoC.Core
{
    /// <summary>
    /// Marker interface for arrays.
    /// </summary>
    internal interface IArray { }
}


#endregion
#region IAspectOrientedMetadata

namespace IoC.Core
{
    using System;

    internal interface IAspectOrientedMetadata
    {
        bool TryGetType([NotNull] Attribute attribute, out Type type);

        bool TryGetOrder([NotNull] Attribute attribute, out IComparable comparable);

        bool TryGetTag([NotNull] Attribute attribute, out object tag);
    }
}

#endregion
#region IConverter

namespace IoC.Core
{
    internal interface IConverter<in TSrc, in TContext, TDst>
    {
        bool TryConvert([NotNull] TContext context, [NotNull] TSrc src, out TDst dst);
    }
}


#endregion
#region IExpressionBuilder

namespace IoC.Core
{
    using System.Linq.Expressions;

    /// <summary>
    /// Allows to build expression for lifetimes.
    /// </summary>
    [PublicAPI]
    internal interface IExpressionBuilder<in TContext>
    {
        /// <summary>
        /// Builds the expression.
        /// </summary>
        /// <param name="bodyExpression">The expression body to get an instance.</param>
        /// <param name="buildContext">The build context.</param>
        /// <param name="context">The expression build context.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([NotNull] Expression bodyExpression, [NotNull] IBuildContext buildContext, [CanBeNull] TContext context = default(TContext));
    }
}


#endregion
#region ILockObject

namespace IoC.Core
{
    internal interface ILockObject { }
}


#endregion
#region IRegistrationTracker

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal interface IRegistrationTracker : IObserver<ContainerEvent>
    {
        [NotNull] IEnumerable<IBuilder> Builders { get; }

        [NotNull] IAutowiringStrategy AutowiringStrategy { get; }

        [NotNull] IEnumerable<ICompiler> Compilers { get; }
    }
}

#endregion
#region ISubject

namespace IoC.Core
{
    using System;

    internal interface ISubject<T>: IObservable<T>, IObserver<T> { }
}


#endregion
#region LifetimeExpressionBuilder

namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal class LifetimeExpressionBuilder : IExpressionBuilder<ILifetime>
    {
        public static readonly IExpressionBuilder<ILifetime> Shared = new LifetimeExpressionBuilder();

        private LifetimeExpressionBuilder() { }

        public Expression Build(Expression bodyExpression, IBuildContext buildContext, ILifetime lifetime)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            return lifetime?.Build(buildContext, bodyExpression) ?? bodyExpression;
        }
    }
}


#endregion
#region LockObject

namespace IoC.Core
{
    internal class LockObject: ILockObject { }
}


#endregion
#region Method

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class Method<TMethodInfo>: IMethod<TMethodInfo> where TMethodInfo: MethodBase
    {
        private readonly Expression[] _parametersExpressions;
        private readonly ParameterInfo[] _parameters;

        public Method([NotNull] TMethodInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            _parameters = info.GetParameters();
            _parametersExpressions = new Expression[_parameters.Length];
        }

        public TMethodInfo Info { get; }

        public IEnumerable<Expression> GetParametersExpressions(IBuildContext buildContext)
        {
            for (var parameterPosition = 0; parameterPosition < _parametersExpressions.Length; parameterPosition++)
            {
                var expression = _parametersExpressions[parameterPosition];
                if (expression != null)
                {
                    yield return expression;
                }
                else
                {
                    var key = new Key(_parameters[parameterPosition].ParameterType);
                    yield return buildContext.CreateChild(key, buildContext.Container).DependencyExpression;
                }
            }
        }

        public void SetParameterExpression(int parameterPosition, Expression parameterExpression)
        {
            if (parameterPosition < 0 || parameterPosition >= _parametersExpressions.Length) throw new ArgumentOutOfRangeException(nameof(parameterPosition));
            _parametersExpressions[parameterPosition] = parameterExpression ?? throw new ArgumentNullException(nameof(parameterExpression));
        }
    }
}

#endregion
#region NullContainer

namespace IoC.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class NullContainer : IContainer
    {
        public static readonly IContainer Shared = new NullContainer();
        private static readonly NotSupportedException NotSupportedException = new NotSupportedException();

        private NullContainer() { }

        public IContainer Parent => throw new NotSupportedException();

        public bool TryRegisterDependency(IEnumerable<Key> keys, IDependency dependency, ILifetime lifetime, out IToken dependencyToken)
            => throw NotSupportedException;

        public bool TryGetDependency(Key key, out IDependency dependency, out ILifetime lifetime)
        {
            dependency = default(IDependency);
            lifetime = default(ILifetime);
            return false;
        }

        public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, out Exception error, IContainer resolvingContainer = null)
        {
            resolver = default(Resolver<T>);
            error = NotSupportedException;
            return false;
        }


        public void RegisterResource(IDisposable resource) { }

        public void UnregisterResource(IDisposable resource) { }

        public void Dispose() { }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<IEnumerable<Key>> GetEnumerator() => Enumerable.Empty<IEnumerable<Key>>().GetEnumerator();

        public IDisposable Subscribe(IObserver<ContainerEvent> observer) => Disposable.Empty;

        public override string ToString() => string.Empty;
    }
}

#endregion
#region Observable

namespace IoC.Core
{
    using System;

    internal static class Observable
    {
        [NotNull]
        public static IObservable<T> Create<T>([NotNull] Func<IObserver<T>, IDisposable> factory) => 
            new InternalObservable<T>(factory ?? throw new ArgumentNullException(nameof(factory)));

        public static IDisposable Subscribe<T>([NotNull] this IObservable<T> source, [NotNull] Action<T> onNext, [NotNull] Action<Exception> onError, [NotNull] Action oncComplete) => 
            (source ?? throw new ArgumentNullException(nameof(source))).Subscribe(
                new InternalObserver<T>(
                    onNext ?? throw new ArgumentNullException(nameof(onNext)),
                    onError ?? throw new ArgumentNullException(nameof(onError)),
                    oncComplete ?? throw new ArgumentNullException(nameof(oncComplete))));

        public static IObservable<TResult> Select<T, TResult>(this IObservable<T> source, Func<T, TResult> selector) =>
            Create<TResult>(observer => source.Subscribe(
                value => { observer.OnNext(selector(value)); },
                observer.OnError,
                observer.OnCompleted));

        public static IObservable<T> Where<T>(this IObservable<T> source, Predicate<T> filter) =>
            Create<T>(observer => source.Subscribe(
                value => { if(filter(value)) observer.OnNext(value); },
                observer.OnError,
                observer.OnCompleted));

        private class InternalObservable<T>: IObservable<T>
        {
            private readonly Func<IObserver<T>, IDisposable> _factory;

            public InternalObservable(Func<IObserver<T>, IDisposable> factory) => _factory = factory;

            public IDisposable Subscribe([NotNull] IObserver<T> observer) => _factory(observer ?? throw new ArgumentNullException(nameof(observer)));
        }

        private class InternalObserver<T>: IObserver<T>
        {
            private readonly Action<T> _onNext;
            private readonly Action<Exception> _onError;
            private readonly Action _oncComplete;

            public InternalObserver(Action<T> onNext, Action<Exception> onError, Action oncComplete)
            {
                _onNext = onNext;
                _onError = onError;
                _oncComplete = oncComplete;
            }

            public void OnNext(T value) => _onNext(value);

            public void OnError(Exception error) => _onError(error);

            public void OnCompleted() => _oncComplete();
        }
    }
}


#endregion
#region RegistrationTracker

// ReSharper disable ForCanBeConvertedToForeach
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal class RegistrationTracker : IRegistrationTracker
    {
        private readonly Container _container;
        private readonly ITracker[] _trackers = new ITracker[3];
        private readonly Tracker<IBuilder> _builderTracker;
        private readonly Tracker<IAutowiringStrategy> _autowiringStrategyTracker;
        private readonly Tracker<ICompiler> _compilerTracker;

        public RegistrationTracker([NotNull] Container container)
        {
            _container = container;

            _trackers[0] = _autowiringStrategyTracker = new Tracker<IAutowiringStrategy>((list, val) => list.Insert(0, val));
            _autowiringStrategyTracker.Items.Add(DefaultAutowiringStrategy.Shared);

            _trackers[1] = _compilerTracker = new Tracker<ICompiler>((list, val) => list.Insert(0, val));
            _compilerTracker.Items.Add(DefaultCompiler.Shared);

            _trackers[2] = _builderTracker = new Tracker<IBuilder>((list, val) => list.Add(val));
        }

        public IEnumerable<IBuilder> Builders => _builderTracker.Items;

        public IAutowiringStrategy AutowiringStrategy => _autowiringStrategyTracker.Items[0];

        public IEnumerable<ICompiler> Compilers => _compilerTracker.Items;

        public void OnNext(ContainerEvent value)
        {
            if (value.Keys == null)
            {
                return;
            }

            IContainer container;
            switch (value.EventType)
            {
                case EventType.RegisterDependency:
                    _container.Reset();
                    container = value.Container;
                    foreach (var key in value.Keys)
                    {
                        for (var index = 0; index < _trackers.Length; index++)
                        {
                            if (_trackers[index].Track(key, container))
                            {
                                break;
                            }
                        }
                    }

                    break;

                case EventType.UnregisterDependency:
                    _container.Reset();
                    container = value.Container;
                    foreach (var key in value.Keys)
                    {
                        for (var index = 0; index < _trackers.Length; index++)
                        {
                            if (_trackers[index].Untrack(key, container))
                            {
                                break;
                            }
                        }
                    }

                    break;
            }
        }

        public void OnError(Exception error) { }

        public void OnCompleted() { }

        private interface ITracker
        {
            bool Track(Key key, IContainer container);

            bool Untrack(Key key, IContainer container);
        }

        private class Tracker<T> : ITracker
        {
            private readonly Action<IList<T>, T> _updater;
            private readonly Dictionary<IContainer, T> _map = new Dictionary<IContainer, T>();
            public readonly IList<T> Items = new List<T>();

            public Tracker(Action<IList<T>, T> updater) => 
                _updater = updater;

            public bool Track(Key key, IContainer container)
            {
                if (key.Type != typeof(T))
                {
                    return false;
                }

                if (_map.ContainsKey(container))
                {
                    return true;
                }

                if (container.TryGetResolver<T>(key.Type, key.Tag, out var resolver, out _, container))
                {
                    var instance = resolver(container);
                    _map.Add(container, instance);
                    _updater(Items, instance);
                }

                return true;
            }

            public bool Untrack(Key key, IContainer container)
            {
                if (key.Type != typeof(T))
                {
                    return false;
                }

                if (_map.TryGetValue(container, out var instance))
                {
                    Items.Remove(instance);
                }

                return true;
            }
        }
    }
}


#endregion
#region Scope

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    [DebuggerDisplay("{" + nameof(ToString) + "()} with {" + nameof(ResourceCount) + "} resources")]
    internal sealed class Scope: IScope, IResourceRegistry
    {
        private static long _currentScopeKey;
        [NotNull] private static readonly Scope Default = new Scope(new LockObject());
        [CanBeNull] [ThreadStatic] private static Scope _current;
        internal readonly long ScopeKey;
        [NotNull] private readonly ILockObject _lockObject;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [CanBeNull] private Scope _prevScope;

        [NotNull] internal static Scope Current => _current ?? Default;

        public Scope([NotNull] ILockObject lockObject, long key)
        {
            ScopeKey = key;
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(lockObject));
        }

        public Scope([NotNull] ILockObject lockObject)
        {
            ScopeKey = Interlocked.Increment(ref _currentScopeKey);
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(lockObject));
        }

        public IDisposable Activate()
        {
            if (ReferenceEquals(this, Current))
            {
                return Disposable.Empty;
            }

            _prevScope = Current;

            _current = this;
            return Disposable.Create(() => { _current = _prevScope ?? throw new NotSupportedException(); });
        }

        public void Dispose()
        {
            List<IDisposable> resources;
            lock (_lockObject)
            {
                 resources = _resources.ToList();
                 _resources.Clear();
            }

            foreach (var resource in resources)
            {
                resource.Dispose();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return ScopeKey.Equals(((Scope) obj).ScopeKey);
        }

        public void RegisterResource(IDisposable resource)
        {
            lock (_lockObject)
            {
                _resources.Add(resource ?? throw new ArgumentNullException(nameof(resource)));
            }
        }

        public void UnregisterResource(IDisposable resource)
        {
            lock (_lockObject)
            {
                _resources.Remove(resource ?? throw new ArgumentNullException(nameof(resource)));
            }
        }

        public override int GetHashCode() => ScopeKey.GetHashCode();

        public override string ToString() => $"#{ScopeKey} Scope";

        internal int ResourceCount
        {
            get
            {
                lock (_lockObject)
                {
                    return _resources.Count;
                }
            }
        }
    }
}


#endregion
#region Subject

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal class Subject<T>: ISubject<T>
    {
        private readonly ILockObject _lockObject;
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

        public Subject([NotNull] ILockObject lockObject)
        {
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(lockObject));
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            lock (_lockObject)
            {
                _observers.Add(observer);
            }

            return Disposable.Create(() =>
            {
                lock (_lockObject)
                {
                    _observers.Remove(observer);
                }
            });
        }

        public void OnNext(T value)
        {
            lock (_lockObject)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnNext(value);
                }
            }
        }

        public void OnError(Exception error)
        {
            lock (_lockObject)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnError(error);
                }
            }
        }

        public void OnCompleted()
        {
            lock (_lockObject)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnCompleted();
                }
            }
        }
    }
}


#endregion
#region Table

namespace IoC.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal class Table<TKey, TValue>: IEnumerable<Table<TKey, TValue>.KeyValue>
    {
        public static readonly Table<TKey, TValue> Empty = new Table<TKey, TValue>(CoreExtensions.CreateArray(4, Bucket.EmptyBucket), 3, 0);
        public readonly int Count;
        public readonly int Divisor;
        public readonly Bucket[] Buckets;

        [MethodImpl((MethodImplOptions)256)]
        private Table(Bucket[] buckets, int divisor, int count)
        {
            Buckets = buckets;
            Divisor = divisor;
            Count = count;
        }

        [MethodImpl((MethodImplOptions)256)]
        private Table(Table<TKey, TValue> origin, int hashCode, TKey key, TValue value)
        {
            int newBucketIndex;
            Count = origin.Count + 1;
            if (origin.Count > origin.Divisor)
            {
                Divisor = (origin.Divisor + 1) << 2 - 1;
                Buckets = CoreExtensions.CreateArray(Divisor + 1, Bucket.EmptyBucket);
                var originBuckets = origin.Buckets;
                for (var originBucketIndex = 0; originBucketIndex < originBuckets.Length; originBucketIndex++)
                {
                    var originKeyValues = originBuckets[originBucketIndex].KeyValues;
                    for (var index = 0; index < originKeyValues.Length; index++)
                    {
                        var keyValue = originKeyValues[index];
                        newBucketIndex = keyValue.HashCode & Divisor;
                        Buckets[newBucketIndex] = Buckets[newBucketIndex].Add(keyValue);
                    }
                }
            }
            else
            {
                Divisor = origin.Divisor;
                Buckets = origin.Buckets.Copy();
            }

            newBucketIndex = hashCode & Divisor;
            Buckets[newBucketIndex] = Buckets[newBucketIndex].Add(new KeyValue(hashCode, key, value));
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public IEnumerator<KeyValue> GetEnumerator()
        {
            for (var bucketIndex = 0; bucketIndex < Buckets.Length; bucketIndex++)
            {
                var keyValues = Buckets[bucketIndex].KeyValues;
                for (var index = 0; index < keyValues.Length; index++)
                {
                    var keyValue = keyValues[index];
                    yield return keyValue;
                }
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public Table<TKey, TValue> Set(int hashCode, TKey key, TValue value)
        {
            return new Table<TKey, TValue>(this, hashCode, key, value);
        }

        [Pure]
        public Table<TKey, TValue> Remove(int hashCode, TKey key, out bool removed)
        {
            removed = false;
            var newBuckets = CoreExtensions.CreateArray(Divisor + 1, Bucket.EmptyBucket);
            var newBucketsArray = newBuckets;
            var bucketIndex = hashCode & Divisor;
            for (var curBucketIndex = 0; curBucketIndex < Buckets.Length; curBucketIndex++)
            {
                if (curBucketIndex != bucketIndex)
                {
                    newBucketsArray[curBucketIndex] = Buckets[curBucketIndex].Copy();
                    continue;
                }

                // Bucket to remove an element
                var bucket = Buckets[bucketIndex];
                var keyValues = bucket.KeyValues;
                for (var index = 0; index < keyValues.Length; index++)
                {
                    var keyValue = keyValues[index];
                    // Remove the element
                    if (keyValue.HashCode == hashCode && (ReferenceEquals(keyValue.Key, key) || Equals(keyValue.Key, key)))
                    {
                        newBucketsArray[bucketIndex] = bucket.Remove(index);
                        removed = true;
                    }
                }
            }

            return new Table<TKey, TValue>(newBuckets, Divisor, removed ? Count - 1: Count);
        }

        internal struct KeyValue
        {
            public readonly int HashCode;
            public readonly TKey Key;
            public readonly TValue Value;

            [MethodImpl((MethodImplOptions)256)]
            public KeyValue(int hashCode, TKey key, TValue value)
            {
                HashCode = hashCode;
                Key = key;
                Value = value;
            }
        }

        internal struct Bucket
        {
            public static readonly Bucket EmptyBucket = new Bucket(0);
            public readonly KeyValue[] KeyValues;

            [MethodImpl((MethodImplOptions)256)]
            private Bucket(KeyValue[] keyValues)
            {
                KeyValues = keyValues.Length == 0 ? CoreExtensions.EmptyArray<KeyValue>() : keyValues.Copy();
            }

            [MethodImpl((MethodImplOptions)256)]
            private Bucket(int count)
            {
                KeyValues = CoreExtensions.CreateArray<KeyValue>(count);
            }

            [MethodImpl((MethodImplOptions)256)]
            public Bucket Copy()
            {
                return new Bucket(KeyValues);
            }

            [Pure]
            [MethodImpl((MethodImplOptions)256)]
            public Bucket Add(KeyValue keyValue)
            {
                return new Bucket(KeyValues.Add(keyValue));
            }

            [Pure]
            [MethodImpl((MethodImplOptions)256)]
            public Bucket Remove(int index)
            {
                var count = KeyValues.Length;
                var newBucket = new Bucket(count - 1);
                var newKeyValues = newBucket.KeyValues;
                var keyValues = KeyValues;
                for (var newIndex = 0; newIndex < index; newIndex++)
                {
                    newKeyValues[newIndex] = keyValues[newIndex];
                }

                for (var newIndex = index + 1; newIndex < count; newIndex++)
                {
                    newKeyValues[newIndex - 1] = keyValues[newIndex];
                }

                return newBucket;
            }
        }
    }
}

#endregion
#region Token

namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal struct Token: IToken
    {
        [NotNull] private readonly IDisposable _dependencyToken;

        public Token([NotNull] IContainer container, [NotNull] IDisposable dependencyToken)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _dependencyToken = dependencyToken ?? throw new ArgumentNullException(nameof(dependencyToken));
        }

        public IContainer Container { get; }

        [MethodImpl((MethodImplOptions)256)]
        public void Dispose() => _dependencyToken.Dispose();
    }
}


#endregion
#region TypeDescriptor

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal struct TypeDescriptor
    {
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETCOREAPP2_0 && !NETCOREAPP2_1 && !NETCOREAPP2_2 && !WINDOWS_UWP
        private const BindingFlags DefaultBindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.Static;
        // ReSharper disable once MemberCanBePrivate.Global
        internal readonly Type Type;

        [MethodImpl((MethodImplOptions)256)]
        public TypeDescriptor([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type AsType() => Type;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public Guid GetId() => Type.GUID;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Assembly GetAssembly() => Type.Assembly;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsValueType() => Type.IsValueType;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsArray() => Type.IsArray;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsPublic() => Type.IsPublic;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        [Pure]
        public Type GetElementType() => Type.GetElementType();

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsInterface() => Type.IsInterface;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsAbstract() => Type.IsAbstract;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericParameter() => Type.IsGenericParameter;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsConstructedGenericType() => Type.IsGenericType && !Type.IsGenericTypeDefinition;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericTypeDefinition() => Type.IsGenericTypeDefinition;

        public bool IsGenericTypeArgument() => Type.GetCustomAttributes(TypeDescriptor<GenericTypeArgumentAttribute>.Type, true).Length > 0;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeArguments() => Type.GetGenericArguments();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericParameterConstraints() => Type.GetGenericParameterConstraints();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeParameters() => Type.GetGenericArguments();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<ConstructorInfo> GetDeclaredConstructors() => Type.GetConstructors(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<MethodInfo> GetDeclaredMethods() => Type.GetMethods(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<MemberInfo> GetDeclaredMembers() => Type.GetMembers(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<FieldInfo> GetDeclaredFields() => Type.GetFields(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<PropertyInfo> GetDeclaredProperties() => Type.GetProperties(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        [Pure]
        public Type GetBaseType() => Type.BaseType;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<Type> GetImplementedInterfaces() => Type.GetInterfaces();

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsAssignableFrom(TypeDescriptor typeDescriptor)
        {
            return Type.IsAssignableFrom(typeDescriptor.Type);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type MakeGenericType([NotNull] params Type[] typeArguments)
        {
            if (typeArguments == null) throw new ArgumentNullException(nameof(typeArguments));
            return Type.MakeGenericType(typeArguments);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type GetGenericTypeDefinition() => Type.GetGenericTypeDefinition();

        public override string ToString() => TypeToStringConverter.Convert(Type);

        public override bool Equals(object obj)
        {
            return obj is TypeDescriptor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        private bool Equals(TypeDescriptor other)
        {
            return Type == other.Type;
        }
#else
        internal readonly Type Type;
        private readonly TypeInfo _typeInfo;

        [MethodImpl((MethodImplOptions)256)]
        public TypeDescriptor([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            _typeInfo = type.GetTypeInfo();
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type AsType() => Type;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public Guid GetId() => _typeInfo.GUID;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Assembly GetAssembly() => _typeInfo.Assembly;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsValueType() => _typeInfo.IsValueType;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsInterface() => _typeInfo.IsInterface;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsAbstract() => _typeInfo.IsAbstract;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericParameter() => _typeInfo.IsGenericParameter;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsArray() => _typeInfo.IsArray;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsPublic() => _typeInfo.IsPublic;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        [Pure]
        public Type GetElementType() => _typeInfo.GetElementType();

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsConstructedGenericType() => Type.IsConstructedGenericType;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericTypeDefinition() => _typeInfo.IsGenericTypeDefinition;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeArguments() => _typeInfo.GenericTypeArguments;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericParameterConstraints() => _typeInfo.GetGenericParameterConstraints();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeParameters() => _typeInfo.GenericTypeParameters;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsGenericTypeArgument() => _typeInfo.GetCustomAttribute<GenericTypeArgumentAttribute>(true) != null;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<ConstructorInfo> GetDeclaredConstructors() => _typeInfo.DeclaredConstructors;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<MethodInfo> GetDeclaredMethods() => _typeInfo.DeclaredMethods;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<MemberInfo> GetDeclaredMembers() => _typeInfo.DeclaredMembers;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<FieldInfo> GetDeclaredFields() => _typeInfo.DeclaredFields;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<PropertyInfo> GetDeclaredProperties() => _typeInfo.DeclaredProperties;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        [Pure]
        public Type GetBaseType() => _typeInfo.BaseType;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public IEnumerable<Type> GetImplementedInterfaces() => _typeInfo.ImplementedInterfaces;

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public bool IsAssignableFrom(TypeDescriptor typeDescriptor)
        {
            return _typeInfo.IsAssignableFrom(typeDescriptor._typeInfo);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type MakeGenericType([NotNull] params Type[] typeArguments)
        {
            if (typeArguments == null) throw new ArgumentNullException(nameof(typeArguments));
            return Type.MakeGenericType(typeArguments);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        [Pure]
        public Type GetGenericTypeDefinition() => Type.GetGenericTypeDefinition();

        public override string ToString() => TypeToStringConverter.Convert(Type);

        public override bool Equals(object obj)
        {
            return obj is TypeDescriptor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Type != null ? Type.GetHashCode() : 0;
        }

        private bool Equals(TypeDescriptor other)
        {
            return Type == other.Type;
        }
#endif
    }
}



#endregion
#region TypeDescriptorExtensions

namespace IoC.Core
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class TypeDescriptorExtensions
    {
        [MethodImpl((MethodImplOptions) 256)]
        public static TypeDescriptor Descriptor(this Type type) => new TypeDescriptor(type);

        [MethodImpl((MethodImplOptions) 256)]
        public static TypeDescriptor Descriptor<T>() => TypeDescriptor<T>.Descriptor;

        [MethodImpl((MethodImplOptions)256)]
        public static Assembly LoadAssembly(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(assemblyName));
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static Type ToGenericType([NotNull] this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var typeDescriptor = type.Descriptor();
            if (typeDescriptor.IsArray())
            {
                var elementType = typeDescriptor.GetElementType();
                if (elementType.Descriptor().IsGenericTypeArgument())
                {
                    return typeof(IArray);
                }
            }

            if (!typeDescriptor.IsConstructedGenericType())
            {
                return type;
            }

            if (typeDescriptor.GetGenericTypeArguments().Any(t => t.Descriptor().IsGenericTypeArgument()))
            {
                return typeDescriptor.GetGenericTypeDefinition();
            }

            return type;
        }
    }
}


#endregion
#region TypeDescriptor`1

namespace IoC.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    internal static class TypeDescriptor<T>
    {
        [NotNull] public static readonly Type Type = typeof(T);

        public static readonly int HashCode = Type.GetHashCode();

        public static readonly TypeDescriptor Descriptor = new TypeDescriptor(Type);

        public static readonly Key Key = new Key(Type);
    }
}


#endregion
#region TypeMapper

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal class TypeMapper
    {
        public static readonly TypeMapper Shared = new TypeMapper();

        private TypeMapper() { }

        public void Map(Type type, Type targetType, IDictionary<Type, Type> typesMap)
        {
            if (type == targetType)
            {
                return;
            }

            if (typesMap.ContainsKey(type))
            {
                return;
            }

            // Generic type marker
            var typeDescriptor = type.Descriptor();
            if (typeDescriptor.IsGenericTypeArgument())
            {
                typesMap[type] = targetType;
                return;
            }

            var targetTypeDescriptor = targetType.Descriptor();

            // Constructed generic
            if (targetTypeDescriptor.IsConstructedGenericType())
            {
                if (typeDescriptor.GetId() == targetTypeDescriptor.GetId())
                {
                    typesMap[typeDescriptor.Type] = targetTypeDescriptor.Type;
                    var typeArgs = typeDescriptor.GetGenericTypeArguments();
                    var targetTypeArgs = targetTypeDescriptor.GetGenericTypeArguments();
                    for (var i = 0; i < typeArgs.Length; i++)
                    {
                        Map(typeArgs[i], targetTypeArgs[i], typesMap);
                    }

                    return;
                }

                foreach (var implementedInterface in targetTypeDescriptor.GetImplementedInterfaces())
                {
                    Map(type, implementedInterface, typesMap);
                }

                foreach (var implementedInterface in typeDescriptor.GetImplementedInterfaces())
                {
                    Map(implementedInterface, targetType, typesMap);
                }

                return;
            }

            // Array
            if (targetTypeDescriptor.IsArray())
            {
                Map(typeDescriptor.GetElementType(), targetTypeDescriptor.GetElementType(), typesMap);
                typesMap[typeDescriptor.Type] = targetTypeDescriptor.Type;
            }
        }
    }
}


#endregion
#region TypeReplacerExpressionBuilder

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class TypeReplacerExpressionBuilder : IExpressionBuilder<IDictionary<Type, Type>>
    {
        public static readonly IExpressionBuilder<IDictionary<Type, Type>> Shared = new TypeReplacerExpressionBuilder();

        private TypeReplacerExpressionBuilder() { }

        public Expression Build(Expression bodyExpression, IBuildContext buildContext, IDictionary<Type, Type> typesMap)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            typesMap = typesMap ?? new Dictionary<Type, Type>();
            if (bodyExpression.Type != buildContext.Key.Type)
            {
                TypeMapper.Shared.Map(bodyExpression.Type, buildContext.Key.Type, typesMap);
                if (typesMap.Count > 0)
                {
                    var typeReplacingExpressionVisitor = new TypeReplacerExpressionVisitor(typesMap);
                    var newExpression = typeReplacingExpressionVisitor.Visit(bodyExpression);
                    if (newExpression != null)
                    {
                        return newExpression;
                    }
                }
            }

            return bodyExpression;
        }
    }
}


#endregion
#region TypeReplacerExpressionVisitor

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class TypeReplacerExpressionVisitor: ExpressionVisitor
    {
        [NotNull] private readonly IDictionary<Type, Type> _typesMap;
        [NotNull] private readonly Dictionary<string, ParameterExpression> _parameters = new Dictionary<string, ParameterExpression>();

        public TypeReplacerExpressionVisitor([NotNull] IDictionary<Type, Type> typesMap)
        {
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));
        }

        protected override Expression VisitNew(NewExpression node)
        {
            var newTypeDescriptor = ReplaceType(node.Type).Descriptor();
            var newConstructor = newTypeDescriptor.GetDeclaredConstructors().Single(i => !i.IsPrivate && Match(node.Constructor.GetParameters(), i.GetParameters()));
            return Expression.New(newConstructor, ReplaceAll(node.Arguments));
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var newType = ReplaceType(node.Type);
            switch (node.NodeType)
            {
                case ExpressionType.Convert:
                    var newOperand = Visit(node.Operand);
                    if (newOperand == null)
                    {
                        return base.VisitUnary(node);
                    }

                    return newOperand.Convert(newType);

                default:
                    return base.VisitUnary(node);
            }
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var newDeclaringType = ReplaceType(node.Method.DeclaringType).Descriptor();
            var newMethod = newDeclaringType.GetDeclaredMethods().SingleOrDefault(i => i.Name == node.Method.Name && Match(node.Method.GetParameters(), i.GetParameters()));
            if (newMethod == null)
            {
                throw new BuildExpressionException($"Cannot find method {node.Method} in the {node.Method.DeclaringType}.", new InvalidOperationException());
            }

            if (newMethod.IsGenericMethod)
            {
                newMethod = newMethod.MakeGenericMethod(ReplaceTypes(node.Method.GetGenericArguments()));
            }

            return node.Object != null 
                ? Expression.Call(Visit(node.Object), newMethod, ReplaceAll(node.Arguments))
                : Expression.Call(newMethod, ReplaceAll(node.Arguments));
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var newDeclaringTypeDescriptor = ReplaceType(node.Member.DeclaringType).Descriptor();
            if (newDeclaringTypeDescriptor.IsConstructedGenericType())
            {
                newDeclaringTypeDescriptor = ReplaceType(newDeclaringTypeDescriptor.AsType()).Descriptor();
            }

            var newMember = newDeclaringTypeDescriptor.GetDeclaredMembers().Single(i => i.Name == node.Member.Name);
            var newExpression = Visit(node.Expression);
            if (newExpression == null)
            {
                return base.VisitMember(node);
            }

            return Expression.MakeMemberAccess(newExpression, newMember);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_parameters.TryGetValue(node.Name, out var newNode))
            {
                return newNode;
            }

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (node.IsByRef)
            {
                newNode = Expression.Parameter(ReplaceType(node.Type).MakeByRefType(), node.Name);
            }
            else
            {
                newNode = Expression.Parameter(ReplaceType(node.Type), node.Name);
            }

            _parameters[node.Name] = newNode;
            return newNode;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value is Type type)
            {
                return Expression.Constant(ReplaceType(type), node.Type);
            }

            return Expression.Constant(node.Value, ReplaceType(node.Type));
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var parameters = node.Parameters.Select(VisitParameter).Cast<ParameterExpression>();
            var body = Visit(node.Body);
            if (body == null)
            {
                return base.VisitLambda(node);
            }

            return Expression.Lambda(ReplaceType(node.Type), body, parameters);
        }

        protected override Expression VisitNewArray(NewArrayExpression node) => 
            Expression.NewArrayInit(ReplaceType(node.Type.GetElementType()), ReplaceAll(node.Expressions));

        protected override Expression VisitListInit(ListInitExpression node)
        {
            var newExpression = (NewExpression)Visit(node.NewExpression);
            if (newExpression == null)
            {
                return node;
            }

            return Expression.ListInit(newExpression, node.Initializers.Select(VisitInitializer));
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Assign:
                    return Expression.Assign(Visit(node.Left) ?? throw new InvalidOperationException(), Visit(node.Right) ?? throw new InvalidOperationException());
            }

            return base.VisitBinary(node);
        }

        private ElementInit VisitInitializer(ElementInit node)
        {
            var newDeclaringType = ReplaceType(node.AddMethod.DeclaringType).Descriptor();
            var newMethod = newDeclaringType.GetDeclaredMethods().SingleOrDefault(i => i.Name == node.AddMethod.Name && Match(node.AddMethod.GetParameters(), i.GetParameters()));
            if (newMethod == null)
            {
                throw new BuildExpressionException($"Cannot find method {node.AddMethod} in the {node.AddMethod.DeclaringType}.", new InvalidOperationException());
            }

            if (newMethod.IsGenericMethod)
            {
                newMethod = newMethod.MakeGenericMethod(ReplaceTypes(node.AddMethod.GetGenericArguments()));
            }

            return Expression.ElementInit(newMethod, ReplaceAll(node.Arguments));
        }

        private bool Match(ParameterInfo[] baseParams, ParameterInfo[] newParams)
        {
            if (baseParams.Length != newParams.Length)
            {
                return false;
            }

            for (var i = 0; i < baseParams.Length; i++)
            {
                if (baseParams[i].Name != newParams[i].Name)
                {
                    return false;
                }

                var paramTypeDescriptor = newParams[i].ParameterType.Descriptor();
                if (paramTypeDescriptor.IsGenericParameter())
                {
                    return true;
                }

                if (ReplaceType(baseParams[i].ParameterType).Descriptor().GetId() != paramTypeDescriptor.GetId())
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl((MethodImplOptions) 256)]
        private Type[] ReplaceTypes(Type[] types)
        {
            for (var i = 0; i < types.Length; i++)
            {
                types[i] = ReplaceType(types[i]);
            }

            return types;
        }

        private Type ReplaceType(Type type)
        {
            if (_typesMap.TryGetValue(type, out var newType))
            {
                return newType;
            }

            var typeDescriptor = type.Descriptor();
            if (typeDescriptor.IsArray())
            {
                var elementType = typeDescriptor.GetElementType();
                var newElementType = ReplaceType(typeDescriptor.GetElementType());
                if (elementType != newElementType)
                {
                    return newElementType.MakeArrayType();
                }

                return type;
            }

            if (typeDescriptor.IsConstructedGenericType())
            {
                var genericTypes = ReplaceTypes(typeDescriptor.GetGenericTypeArguments());
                return typeDescriptor.GetGenericTypeDefinition().Descriptor().MakeGenericType(ReplaceTypes(genericTypes));
            }

            return type;
        }

        [MethodImpl((MethodImplOptions) 256)]
        private IEnumerable<Expression> ReplaceAll(IEnumerable<Expression> expressions) => 
            expressions.Select(Visit);
    }
}


#endregion
#region TypeToStringConverter

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Configuration;

    internal class TypeToStringConverter: IConverter<Type, Type, string>
    {
        public static readonly IConverter<Type, Type, string> Shared = new TypeToStringConverter();
        private static readonly IDictionary<Type, string> PrimitiveTypes = StringToTypeConverter.PrimitiveTypes.ToDictionary(i => i.Value, i => i.Key);

        private TypeToStringConverter() { }

        public bool TryConvert(Type context, Type type, out string typeName)
        {
            typeName = Convert(type);
            return true;
        }

        public static string Convert(Type type)
        {
            if (PrimitiveTypes.TryGetValue(type, out var typeName))
            {
                return typeName;
            }

            var typeDescriptor = type.Descriptor();
            if (typeDescriptor.IsConstructedGenericType())
            {
                return $"{GetTypeName(type)}<{string.Join(", ", typeDescriptor.GetGenericTypeArguments().Select(Convert))}>";
            }

            if (typeDescriptor.IsGenericTypeDefinition())
            {
                return $"{GetTypeName(type)}<{string.Join(", ", typeDescriptor.GetGenericTypeParameters().Select(Convert))}>";
            }

            return type.Name;
        }

        private static string GetTypeName(Type type)
        {
            var name = type.Name;
            var lastCharIndex = name.IndexOf('`');
            if (lastCharIndex > 0)
            {
                return name.Substring(0, lastCharIndex);
            }

            return name;
        }
    }
}


#endregion

#endregion

#region Core\Configuration

#region Binding

namespace IoC.Core.Configuration
{
    using System;

    internal sealed class Binding
    {
        public Binding(
            [NotNull] Type[] types,
            Lifetime lifetime,
            [NotNull][ItemCanBeNull] object[] tags,
            [NotNull] Type instanceType)
        {
            Types = types ?? throw new ArgumentNullException(nameof(types));
            Lifetime = lifetime;
            Tags = tags ?? throw new ArgumentNullException(nameof(tags));
            InstanceType = instanceType ?? throw new ArgumentNullException(nameof(instanceType));
        }

        public Type[] Types { get; }

        public Lifetime Lifetime { get; }

        public object[] Tags { get; }

        public Type InstanceType { get; }
    }
}


#endregion
#region BindingContext

namespace IoC.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal sealed class BindingContext
    {
        public static readonly BindingContext Empty = new BindingContext(Enumerable.Empty<Assembly>(), Enumerable.Empty<string>(), Enumerable.Empty<Binding>());

        public BindingContext(
            [NotNull] IEnumerable<Assembly> assemblies,
            [NotNull] IEnumerable<string> namespaces,
            [NotNull] IEnumerable<Binding> bindings)
        {
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
            if (namespaces == null) throw new ArgumentNullException(nameof(namespaces));
            if (bindings == null) throw new ArgumentNullException(nameof(bindings));
            Assemblies = assemblies as Assembly[] ?? assemblies.ToArray();
            Namespaces = namespaces as string[] ?? namespaces.ToArray();
            Bindings = bindings as Binding[] ?? bindings.ToArray();
        }

        [NotNull] public IEnumerable<Assembly> Assemblies { get; }

        [NotNull] public IEnumerable<string> Namespaces { get; }

        [NotNull] public IEnumerable<Binding> Bindings { get; }
    }
}


#endregion
#region Separators

namespace IoC.Core.Configuration
{
    internal static class Separators
    {
        public static readonly char Statement = ';';

        public static readonly char Type = ',';

        public static readonly char Assembly = ',';

        public static readonly char Namespace = ',';
    }
}


#endregion
#region Statement

namespace IoC.Core.Configuration
{
    using System;

    internal struct Statement
    {
        [NotNull] public readonly string Text;
        public readonly int LineNumber;
        public readonly int Position;

        public Statement([NotNull] string text, int lineNumber, int position)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            if (lineNumber < 0) throw new ArgumentOutOfRangeException(nameof(lineNumber));
            if (position < 0) throw new ArgumentOutOfRangeException(nameof(position));
            LineNumber = lineNumber;
            Position = position;
        }
    }
}


#endregion
#region StatementsToBindingContextConverter

namespace IoC.Core.Configuration
{
    using System;
    using System.Collections.Generic;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StatementsToBindingContextConverter : IConverter<IEnumerable<Statement>, BindingContext, BindingContext>
    {
        private readonly IEnumerable<IConverter<Statement, BindingContext, BindingContext>> _statementToContextConverters;

        public StatementsToBindingContextConverter(
            [NotNull] IEnumerable<IConverter<Statement, BindingContext, BindingContext>> statementToContextConverters)
        {
            _statementToContextConverters = statementToContextConverters ?? throw new ArgumentNullException(nameof(statementToContextConverters));
        }

        public bool TryConvert(BindingContext baseContext, IEnumerable<Statement> statements, out BindingContext dst)
        {
            if (baseContext == null) throw new ArgumentNullException(nameof(baseContext));
            if (statements == null) throw new ArgumentNullException(nameof(statements));
            foreach (var statement in statements)
            {
                foreach (var statementToContextConverter in _statementToContextConverters)
                {
                    if (statementToContextConverter.TryConvert(baseContext, statement, out var newContext))
                    {
                        baseContext = newContext;
                        break;
                    }
                }
            }

            dst = baseContext;
            return true;
        }
    }
}


#endregion
#region StatementToBindingConverter

// ReSharper disable IdentifierTypo
namespace IoC.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Issues;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StatementToBindingConverter: IConverter<Statement, BindingContext, BindingContext>
    {
        private static readonly Regex BindingRegex = new Regex(@"Bind<(?<contractTypes>[\w.,\s<>]+)>\(\)(?<config>.*)\.To<\s*(?<instanceType>[\w.<>]+)\s*>\(\)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        private readonly IConverter<string, BindingContext, Type> _typeConverter;
        [NotNull] private readonly IConverter<string, Statement, Lifetime> _lifetimeConverter;
        [NotNull] private readonly IConverter<string, Statement, IEnumerable<object>> _tagsConverter;
        private readonly ICannotParseType _сannotParseType;

        public StatementToBindingConverter(
            [NotNull] IConverter<string, BindingContext, Type> typeConverter,
            [NotNull] IConverter<string, Statement, Lifetime> lifetimeConverter,
            [NotNull] IConverter<string, Statement, IEnumerable<object>> tagsConverter,
            [NotNull] ICannotParseType сannotParseType)
        {
            _typeConverter = typeConverter ?? throw new ArgumentNullException(nameof(typeConverter));
            _lifetimeConverter = lifetimeConverter ?? throw new ArgumentNullException(nameof(lifetimeConverter));
            _tagsConverter = tagsConverter ?? throw new ArgumentNullException(nameof(tagsConverter));
            _сannotParseType = сannotParseType ?? throw new ArgumentNullException(nameof(сannotParseType));
        }

        public bool TryConvert(BindingContext baseContext, Statement statement, out BindingContext dst)
        {
            if (baseContext == null) throw new ArgumentNullException(nameof(baseContext));
            var bindingMatch = BindingRegex.Match(statement.Text);
            if (bindingMatch.Success)
            {
                var instanceTypeName = bindingMatch.Groups["instanceType"].Value;
                if (!_typeConverter.TryConvert(baseContext, instanceTypeName, out var instanceType))
                {
                    instanceType = _сannotParseType.Resolve(statement.Text, statement.LineNumber, statement.Position, instanceTypeName);
                }

                var contractTypes = new List<Type>();
                foreach (var contractTypeName in bindingMatch.Groups["contractTypes"]?.Value.Split(Separators.Type) ?? Enumerable.Empty<string>())
                {
                    if (!_typeConverter.TryConvert(baseContext, contractTypeName, out var contractType))
                    {
                        contractType = _сannotParseType.Resolve(statement.Text, statement.LineNumber, statement.Position, contractTypeName);
                    }

                    contractTypes.Add(contractType);
                }

                var lifetime = Lifetime.Transient;
                var tags = new List<object>();
                var config = bindingMatch.Groups["config"]?.Value;
                if (config != null)
                {
                    if (_lifetimeConverter.TryConvert(statement, config, out var curLifetime))
                    {
                        lifetime = curLifetime;
                    }

                    if (_tagsConverter.TryConvert(statement, config, out var curTags))
                    {
                        tags.AddRange(curTags);
                    }

                    var binding = new Binding(contractTypes.Distinct().ToArray(), lifetime, tags.ToArray(), instanceType);

                    dst = new BindingContext(
                        baseContext.Assemblies,
                        baseContext.Namespaces,
                        baseContext.Bindings.Concat(Enumerable.Repeat(binding, 1)).Distinct());

                    return true;
                }
            }

            dst = default(BindingContext);
            return false;
        }
    }
}


#endregion
#region StatementToNamespacesConverter

namespace IoC.Core.Configuration
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StatementToNamespacesConverter : IConverter<Statement, BindingContext, BindingContext>
    {
        private static readonly Regex Regex = new Regex(@"using\s+([\w.,\s]+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public bool TryConvert(BindingContext baseContext, Statement statement, out BindingContext dst)
        {
            if (baseContext == null) throw new ArgumentNullException(nameof(baseContext));
            var match = Regex.Match(statement.Text);
            if (match.Success)
            {
                var namespaces = match.Groups[1].Value.Split(Separators.Namespace).Select(i => i.Trim()).Where(i => !string.IsNullOrWhiteSpace(i));
                dst = new BindingContext(
                    baseContext.Assemblies,
                    baseContext.Namespaces.Concat(namespaces).Distinct(),
                    baseContext.Bindings);
                return true;
            }

            dst = default(BindingContext);
            return false;
        }
    }
}


#endregion
#region StatementToReferencesConverter

namespace IoC.Core.Configuration
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StatementToReferencesConverter : IConverter<Statement, BindingContext, BindingContext>
    {
        private static readonly Regex Regex = new Regex(@"ref\s+([\w.,\s]+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public bool TryConvert(BindingContext baseContext, Statement statement, out BindingContext dst)
        {
            if (baseContext == null) throw new ArgumentNullException(nameof(baseContext));
            var match = Regex.Match(statement.Text);
            if (match.Success)
            {
                var assemblies = match.Groups[1].Value.Split(Separators.Assembly).Select(TypeDescriptorExtensions.LoadAssembly);
                dst = new BindingContext(
                    baseContext.Assemblies.Concat(assemblies).Distinct(),
                    baseContext.Namespaces,
                    baseContext.Bindings);
                return true;
            }

            dst = default(BindingContext);
            return false;
        }
    }
}


#endregion
#region StringToLifetimeConverter

namespace IoC.Core.Configuration
{
    using System;
    using System.Text.RegularExpressions;
    using Issues;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StringToLifetimeConverter: IConverter<string, Statement, Lifetime>
    {
        [NotNull] private readonly ICannotParseLifetime _cannotParseLifetime;
        private static readonly Regex Regex = new Regex(@"(?:\s*\.\s*As\s*\(\s*([\w.^)]+)\s*\)\s*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public StringToLifetimeConverter([NotNull] ICannotParseLifetime cannotParseLifetime)
        {
            _cannotParseLifetime = cannotParseLifetime ?? throw new ArgumentNullException(nameof(cannotParseLifetime));
        }

        public bool TryConvert(Statement statement, string text, out Lifetime dst)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            Match match = null;
            var success = false;
            dst = Lifetime.Transient;
            do
            {
                match = match?.NextMatch() ?? Regex.Match(text);
                if (!match.Success)
                {
                    break;
                }

                var lifetimeName = match.Groups[1].Value.Replace(" ", string.Empty).Replace($"{nameof(Lifetime)}.", string.Empty).Trim();
                try
                {
                    dst = (Lifetime) Enum.Parse(TypeDescriptor<Lifetime>.Type, lifetimeName, true);
                }
                catch (Exception)
                {
                    dst = _cannotParseLifetime.Resolve(statement.Text, statement.LineNumber, statement.Position, lifetimeName);
                }

                success = true;
            }
            while (true);
            return success;
        }
    }
}


#endregion
#region StringToTagsConverter

namespace IoC.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StringToTagsConverter : IConverter<string, Statement, IEnumerable<object>>
    {
        private static readonly Regex Regex = new Regex(@"(?:\s*\.\s*Tag\s*\(\s*([^)]*)\s*\)\s*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public bool TryConvert(Statement context, string text, out IEnumerable<object> dst)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            var tagSet = new HashSet<object>();
            Match match = null;
            do
            {
                match = match?.NextMatch() ?? Regex.Match(text);
                if (!match.Success)
                {
                    break;
                }

                var tag = match.Groups[1].Value.Trim();
                // empty
                if (string.IsNullOrWhiteSpace(tag))
                {
                    tagSet.Add(null);
                    continue;
                }

                // string
                if (tag.StartsWith("\"") && tag.EndsWith("\""))
                {
                    tagSet.Add(tag.Substring(1, tag.Length - 2));
                    continue;
                }

                // char
                if (tag.Length == 3 && tag.StartsWith("'") && tag.EndsWith("'"))
                {
                    tagSet.Add(tag[1]);
                    continue;
                }

                // int
                if (int.TryParse(tag, NumberStyles.Any, CultureInfo.InvariantCulture, out var intValue))
                {
                    tagSet.Add(intValue);
                    continue;
                }

                // long
                if (long.TryParse(tag, NumberStyles.Any, CultureInfo.InvariantCulture, out var longValue))
                {
                    tagSet.Add(longValue);
                    continue;
                }

                // double
                if (double.TryParse(tag, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
                {
                    tagSet.Add(doubleValue);
                    continue;
                }

                // DateTimeOffset
                if (DateTimeOffset.TryParse(tag, out var dateTimeOffsetValue))
                {
                    tagSet.Add(dateTimeOffsetValue);
                }
            }
            while (true);

            dst = tagSet;
            return tagSet.Any();
        }
    }
}


#endregion
#region StringToTypeConverter

namespace IoC.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StringToTypeConverter : IConverter<string, BindingContext, Type>
    {
        // ReSharper disable StringLiteralTypo
        internal static readonly IDictionary<string, Type> PrimitiveTypes = new Dictionary<string, Type>
        {
            {"byte", TypeDescriptor<byte>.Type},
            {"sbyte", TypeDescriptor<sbyte>.Type},
            {"int", TypeDescriptor<int>.Type},
            {"uint", TypeDescriptor<uint>.Type},
            {"short", TypeDescriptor<short>.Type},
            {"ushort", TypeDescriptor<ushort>.Type},
            {"long", TypeDescriptor<long>.Type},
            {"ulong", TypeDescriptor<ulong>.Type},
            {"float", TypeDescriptor<float>.Type},
            {"double", TypeDescriptor<double>.Type},
            {"char", TypeDescriptor<char>.Type},
            {"object", TypeDescriptor<object>.Type},
            {"string", TypeDescriptor<string>.Type},
            {"decimal", TypeDescriptor<decimal>.Type}
        };

        public bool TryConvert(BindingContext context, string typeName, out Type dst)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (string.IsNullOrWhiteSpace(typeName))
            {
                dst = default(Type);
                return false;
            }

            if (TryResolveSimpleType(typeName, out dst))
            {
                return true;
            }

            var typeDescription = new TypeDescription(context.Assemblies, context.Namespaces, typeName);
            if (!typeDescription.IsValid)
            {
                return false;
            }

            dst = typeDescription.Type;
            return true;
        }

        private static bool TryResolveSimpleType(string typeName, out Type type)
        {
            if (typeName == null)
            {
                type = default(Type);
                return false;
            }

            if (PrimitiveTypes.TryGetValue(typeName, out type))
            {
                return true;
            }

            if (typeName.Contains("<") || typeName.Contains(">"))
            {
                return false;
            }

            if (ReflectionLoad(typeName, out type))
            {
                return true;
            }

            return false;
        }

        private static bool ReflectionLoad(string typeName, out Type type)
        {
            type = Type.GetType(typeName, false);
            return type != null;
        }

        private sealed class TypeDescription
        {
            private readonly ICollection<Assembly> _assemblies;
            private readonly ICollection<string> _namespaces;

            public TypeDescription(
                IEnumerable<Assembly> assemblies,
                IEnumerable<string> namespaces,
                string typeName)
            {
                _assemblies = assemblies as ICollection<Assembly> ?? assemblies.ToList();
                _namespaces = namespaces as ICollection<string> ?? namespaces.ToList();
                GenericTypeArgs = new List<TypeDescription>();

                if (typeName == null)
                {
                    return;
                }

                typeName = typeName.Trim();
                if (string.IsNullOrEmpty(typeName))
                {
                    IsValid = true;
                    IsUndefined = true;
                    return;
                }

                var genericStartIndex = typeName.IndexOf('<');
                var genericFinishIndex = typeName.LastIndexOf('>');
                if (genericStartIndex >= 0 && genericFinishIndex >= 0)
                {
                    if (genericStartIndex < genericFinishIndex)
                    {
                        var genericTypeArgsStr = typeName.Substring(genericStartIndex + 1, genericFinishIndex - genericStartIndex - 1);
                        typeName = typeName.Substring(0, genericStartIndex);
                        var isValid = true;
                        var nested = 0;
                        var index = 0;
                        var curIndex = 0;
                        var requiredArgs = 1;
                        var args = new List<string>();
                        foreach (var genericTypeArgsChar in genericTypeArgsStr)
                        {
                            switch (genericTypeArgsChar)
                            {
                                case '<':
                                    nested++;
                                    break;

                                case '>':
                                    nested--;
                                    if (nested < 0)
                                    {
                                        isValid = false;
                                    }

                                    break;

                                case ',':
                                    if (nested == 0)
                                    {
                                        args.Add(genericTypeArgsStr.Substring(index, curIndex - index).Trim());
                                        index = curIndex + 1;
                                        requiredArgs++;
                                    }

                                    break;
                            }

                            if (!isValid)
                            {
                                break;
                            }

                            curIndex++;
                        }

                        if (isValid && requiredArgs > args.Count)
                        {
                            args.Add(genericTypeArgsStr.Substring(index, curIndex - index).Trim());
                        }

                        foreach (var genericTypeArgStr in args)
                        {
                            var genericTypeDescriptor = new TypeDescription(_assemblies, _namespaces, genericTypeArgStr);
                            isValid &= genericTypeDescriptor.IsValid;
                            if (!isValid)
                            {
                                break;
                            }

                            GenericTypeArgs.Add(genericTypeDescriptor);
                        }

                        if (!isValid)
                        {
                            return;
                        }

                        if (GenericTypeArgs.Count > 0)
                        {
                            var cnt = 0;
                            var genericArgsSb = new StringBuilder($"`{GenericTypeArgs.Count}");
                            if (GenericTypeArgs.Any(i => !i.IsUndefined))
                            {
                                genericArgsSb.Append('[');
                                foreach (var genericTypeArg in GenericTypeArgs)
                                {
                                    if (cnt > 0)
                                    {
                                        genericArgsSb.Append(',');
                                    }

                                    cnt++;
                                    if (!genericTypeArg.IsUndefined)
                                    {
                                        genericArgsSb.Append('[');
                                        genericArgsSb.Append(genericTypeArg.Type.AssemblyQualifiedName);
                                        genericArgsSb.Append(']');
                                    }
                                    else
                                    {
                                        genericArgsSb.Append("[]");
                                    }
                                }
                                genericArgsSb.Append(']');
                            }

                            typeName = typeName + genericArgsSb;
                        }

                        IsValid = LoadTypeUsingVariants(typeName);
                        return;
                    }
                }

                if (genericStartIndex == -1 && genericFinishIndex == -1)
                {
                    IsValid = LoadTypeUsingVariants(typeName);
                }
            }

            public bool IsValid { get; }

            private IList<TypeDescription> GenericTypeArgs { get; }

            private bool IsUndefined { get; }

            public Type Type { get; private set; }

            private bool LoadTypeUsingVariants(string typeName)
            {
                if (LoadType(typeName))
                {
                    return true;
                }

                foreach (var usingName in _namespaces)
                {
                    var fullTypeName = $"{usingName}.{typeName}";
                    if (LoadType(fullTypeName))
                    {
                        return true;
                    }
                }

                return false;
            }

            private bool LoadType(string typeName)
            {
                if (ReflectionLoad(typeName, out Type type))
                {
                    OnTypeLoaded(type);
                    return true;
                }

                if (LoadTypeInternal(typeName))
                {
                    return true;
                }

                while (TryGetNestedTypeName(typeName, out var newTypeName))
                {
                    if (LoadTypeInternal(newTypeName))
                    {
                        return true;
                    }

                    typeName = newTypeName;
                }

                return false;
            }

            private bool TryGetNestedTypeName([NotNull] string typeName, out string nestedTypeName)
            {
                if (typeName == null) throw new ArgumentNullException(nameof(typeName));
                var pointIndex = -1;
                var nested = 0;
                for (var i = typeName.Length - 1; i >= 0; i--)
                {
                    switch (typeName[i])
                    {
                        case ']':
                            nested++;
                            continue;

                        case '[':
                            nested--;
                            continue;
                    }

                    if (nested > 0)
                    {
                        continue;
                    }

                    if (typeName[i] == '.')
                    {
                        pointIndex = i;
                        break;
                    }
                }

                if (pointIndex < 0)
                {
                    nestedTypeName = default(string);
                    return false;
                }

                nestedTypeName = typeName.Substring(0, pointIndex) + "+" + typeName.Substring(pointIndex + 1, typeName.Length - pointIndex - 1);
                return true;
            }

            private bool LoadTypeInternal(string typeName)
            {
                if (TryResolveSimpleType(typeName, out Type type))
                {
                    OnTypeLoaded(type);
                    return true;
                }

                foreach (var reference in _assemblies)
                {
                    var assemblyQualifiedName = $"{typeName}, {reference.GetName()}";
                    type = Type.GetType(assemblyQualifiedName);
                    if (type != null)
                    {
                        OnTypeLoaded(type);
                        return true;
                    }

                    type = reference.GetType(assemblyQualifiedName);
                    if (type != null)
                    {
                        OnTypeLoaded(type);
                        return true;
                    }
                }

                return false;
            }

            private void OnTypeLoaded(Type type)
            {
                Type = type;
            }
        }
    }
}

#endregion
#region TextConfiguration

namespace IoC.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal sealed class TextConfiguration : IConfiguration
    {
        private static readonly string StatementSeparator = "" + Separators.Statement;
        [NotNull] private readonly IConverter<IEnumerable<Statement>, BindingContext, BindingContext> _bindingsConverter;
        [NotNull] private readonly IEnumerable<Statement> _statements;

        public TextConfiguration(
            [NotNull] TextReader textReader,
            [NotNull] IConverter<IEnumerable<Statement>, BindingContext, BindingContext> bindingsConverter)
        {
            _bindingsConverter = bindingsConverter ?? throw new ArgumentNullException(nameof(bindingsConverter));
            _statements = GetStatements(textReader ?? throw new ArgumentNullException(nameof(textReader)));
        }

        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (_bindingsConverter.TryConvert(BindingContext.Empty, _statements, out var context))
            {
                return
                    from binding in context.Bindings
                    let curBinding = binding.Tags.Aggregate(
                        container.Bind(binding.Types).As(binding.Lifetime),
                        (current, tag) => current.Tag(tag))
                    select curBinding.As(binding.Lifetime).To(binding.InstanceType);
            }

            return Enumerable.Empty<IToken>();
        }

        private static IEnumerable<Statement> GetStatements([NotNull] TextReader textReader)
        {
            if (textReader == null) throw new ArgumentNullException(nameof(textReader));
            var lineNumber = 0;
            var line = string.Empty;
            do
            {
                var curLine = textReader.ReadLine();
                if (!string.IsNullOrWhiteSpace(curLine) && !curLine.TrimEnd().EndsWith(StatementSeparator))
                {
                    line += curLine;
                    continue;
                }

                line += curLine;
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var prevPosition = 0;
                    do
                    {
                        var position = line.IndexOf(Separators.Statement, prevPosition);
                        if (position == -1)
                        {
                            position = line.Length - prevPosition;
                        }

                        var text = line.Substring(prevPosition, position - prevPosition);
                        yield return new Statement(text.Trim(), lineNumber, prevPosition);
                        prevPosition = position + 1;
                    }
                    while (prevPosition < line.Length);
                    line = string.Empty;
                }
                else
                {
                    yield break;
                }

                lineNumber++;
            }
            while (true);
        }
    }
}


#endregion

#endregion

#pragma warning disable CS1658 // Warning is overriding an error
#pragma warning restore nullable
#pragma warning restore CS1658 // Warning is overriding an error

// ReSharper restore All