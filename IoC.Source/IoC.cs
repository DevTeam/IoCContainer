
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
        /// Create an aspect oriented autowiring strategy.
        /// </summary>
        /// <returns>The instance of aspect oriented autowiring strategy.</returns>
        public static IAutowiringStrategy AspectOriented() => AspectOrientedMetadata.Empty;

        /// <summary>
        /// Specify a type selector for an aspect oriented autowiring strategy.
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
        /// Specify an order selector for an aspect oriented autowiring strategy.
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
        /// Specify a tag selector for an aspect oriented autowiring strategy.
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
    using static Key;
    using FullKey = Key;
    using ShortKey = System.Type;
    using ResolverDelegate = System.Delegate;

    /// <summary>
    /// The base IoC container implementation.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("Name = {" + nameof(ToString) + "()}")]
    [DebuggerTypeProxy(typeof(ContainerDebugView))]
    public sealed class Container : IMutableContainer
    {
        private static long _containerId;

        [NotNull] internal Table<FullKey, ResolverDelegate> Resolvers = Table<FullKey, ResolverDelegate>.Empty;
        [NotNull] internal Table<ShortKey, ResolverDelegate> ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
        [NotNull] private Table<FullKey, Registration> _registrations = Table<FullKey, Registration>.Empty;
        [NotNull] private Table<ShortKey, Registration> _registrationsTagAny = Table<ShortKey, Registration>.Empty;

        private bool _isDisposed;
        [NotNull] private readonly IContainer _parent;
        [NotNull] private readonly string _name;
        [NotNull] private readonly ILockObject _lockObject;
        [NotNull] private readonly Subject<ContainerEvent> _eventSubject;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [NotNull] private readonly RegistrationTracker _registrationTracker;

        /// <summary>
        /// Creates a root container with default features.
        /// </summary>
        /// <param name="configurations"></param>
        /// <returns>The root container.</returns>
        [PublicAPI]
        [NotNull]
        [MethodImpl((MethodImplOptions)0x100)]
        public static Container Create([NotNull] [ItemNotNull] params IConfiguration[] configurations) =>
            Create(string.Empty, configurations ?? throw new ArgumentNullException(nameof(configurations)));

        /// <summary>
        /// Creates a root container with default features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <param name="configurations"></param>
        /// <returns>The root container.</returns>
        [PublicAPI]
        [NotNull]
        [MethodImpl((MethodImplOptions)0x200)]
        public static Container Create([NotNull] string name = "", [NotNull][ItemNotNull] params IConfiguration[] configurations)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));

            // Create a root container
            var lockObject = new LockObject();
            var rootContainer = new Container(string.Empty, NullContainer.Shared, lockObject);
            rootContainer.Register<ILockObject>(ctx => lockObject);
            if (configurations.Length > 0)
            {
                rootContainer.ApplyConfigurations(configurations);
            }
            else
            {
                rootContainer.ApplyConfigurations(Features.DefaultFeature.Set);
            }

            // Create a target container
            var container = new Container(CreateContainerName(name), rootContainer, lockObject);
            container.RegisterResource(rootContainer);
            return container;
        }

        [MethodImpl((MethodImplOptions)0x200)]
        internal Container([NotNull] string name, [NotNull] IContainer parent, [NotNull] ILockObject lockObject)
        {
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(parent));
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
            (_parent as Container)?._eventSubject.OnNext(ContainerEvent.NewContainer(this));

            // Notifies about existing registrations in parent containers
            _eventSubject.OnNext(ContainerEvent.RegisterDependency(_parent, _parent.SelectMany(i => i)));
        }

        /// <inheritdoc />
        public IContainer Parent => _parent;

        /// <inheritdoc />
        public override string ToString() => _name;

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        [MethodImpl((MethodImplOptions)0x200)]
        public bool TryRegisterDependency(IEnumerable<FullKey> keys, IDependency dependency, ILifetime lifetime, out IToken dependencyToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));

            var isRegistered = true;
            lock (_lockObject)
            {
                CheckIsNotDisposed();

                var registeredKeys = new List<FullKey>();
                var dependencyEntry = new Registration(this, _eventSubject, dependency, lifetime, Disposable.Create(() => UnregisterKeys(registeredKeys, dependency, lifetime)), registeredKeys);
                try
                {
                    var dependenciesForTagAny = _registrationsTagAny;
                    var dependencies = _registrations;
                    foreach (var curKey in keys)
                    {
                        var type = curKey.Type.ToGenericType();
                        var key = type != curKey.Type ? new FullKey(type, curKey.Tag) : curKey;
                        if (key.Tag == AnyTag)
                        {
                            isRegistered &= !dependenciesForTagAny.TryGetByType(key.Type, out _);
                            if (isRegistered)
                            {
                                dependenciesForTagAny = dependenciesForTagAny.Set(key.Type, dependencyEntry);
                            }
                        }
                        else
                        {
                            isRegistered &= !dependencies.TryGetByKey(key, out _);
                            if (isRegistered)
                            {
                                dependencies = dependencies.Set(key, dependencyEntry);
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
                        _registrationsTagAny = dependenciesForTagAny;
                        _registrations = dependencies;
                        _eventSubject.OnNext(ContainerEvent.RegisterDependency(this, registeredKeys, dependency, lifetime));
                    }
                }
                catch (Exception error)
                {
                    _eventSubject.OnNext(ContainerEvent.RegisterDependencyFailed(this, registeredKeys, dependency, lifetime, error));
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
            }

            return isRegistered;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        [MethodImpl((MethodImplOptions)0x200)]
        public bool TryGetResolver<T>(ShortKey type, object tag, out Resolver<T> resolver, out Exception error, IContainer resolvingContainer = null)
        {
            FullKey key;
            if (tag == null)
            {
                if (ResolversByType.TryGetByType(type, out var curResolver)) // found in resolvers by type
                {
                    resolver = (Resolver<T>) curResolver;
                    error = default(Exception);
                    return true;
                }

                key = new FullKey(type);
            }
            else
            {
                key = new FullKey(type, tag);
                if (Resolvers.TryGetByKey(key, out var curResolver)) // found in resolvers
                {
                    resolver = (Resolver<T>) curResolver;
                    error = default(Exception);
                    return true;
                }
            }

            return TryGetResolver(key, out resolver, out error, resolvingContainer);
        }

        [MethodImpl((MethodImplOptions)0x200)]
        internal bool TryGetResolver<T>(FullKey key, out Resolver<T> resolver, out Exception error, [CanBeNull] IContainer resolvingContainer)
        {
            // tries finding in dependencies
            lock (_lockObject)
            {
                CheckIsNotDisposed();
                if (TryGetRegistration(key, out var registration))
                {
                    // tries creating resolver
                    resolvingContainer = resolvingContainer ?? this;
                    resolvingContainer = registration.Lifetime?.SelectResolvingContainer(this, resolvingContainer) ?? resolvingContainer;
                    if (!registration.TryCreateResolver(key, resolvingContainer, (resolvingContainer as Container ?? this)._registrationTracker, out resolver, out error))
                    {
                        return false;
                    }
                }
                else
                {
                    // tries finding in parent
                    if (!_parent.TryGetResolver(key.Type, key.Tag, out resolver, out error, resolvingContainer ?? this))
                    {
                        resolver = default(Resolver<T>);
                        return false;
                    }
                }

                // If it is resolving container only
                if (resolvingContainer == null || Equals(resolvingContainer, this))
                {
                    // Add resolver to tables
                    Resolvers = Resolvers.Set(key, resolver);
                    if (key.Tag == null)
                    {
                        ResolversByType = ResolversByType.Set(key.Type, resolver);
                    }
                }
            }

            error = default(Exception);
            return true;
        }

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)0x200)]
        public bool TryGetDependency(FullKey key, out IDependency dependency, out ILifetime lifetime)
        {
            lock (_lockObject)
            {
                CheckIsNotDisposed();

                if (!TryGetRegistration(key, out var registration))
                {
                    return _parent.TryGetDependency(key, out dependency, out lifetime);
                }

                dependency = registration.Dependency;
                lifetime = registration.GetLifetime(key.Type);
                return true;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            try
            {
                // Notifies parent container about the child container disposing
                (_parent as Container)?._eventSubject.OnNext(ContainerEvent.DisposeContainer(this));

                _parent.UnregisterResource(this);
                List<IDisposable> entriesToDispose;
                lock (_lockObject)
                {
                    entriesToDispose = new List<IDisposable>(_registrations.Count + _registrationsTagAny.Count + _resources.Count);
                    entriesToDispose.AddRange(_registrations.Select(i => i.Value));
                    entriesToDispose.AddRange(_registrationsTagAny.Select(i => i.Value));
                    entriesToDispose.AddRange(_resources);
                    _registrations = Table<FullKey, Registration>.Empty;
                    _registrationsTagAny = Table<ShortKey, Registration>.Empty;
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
            finally
            {
                _isDisposed = true;
            }
        }

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)0x200)]
        public void RegisterResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_lockObject)
            {
                CheckIsNotDisposed();
                _resources.Add(resource);
            }
        }

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)0x200)]
        public void UnregisterResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_lockObject)
            {
                _resources.Remove(resource);
            }
        }

        [MethodImpl((MethodImplOptions)0x200)]
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)0x200)]
        public IEnumerator<IEnumerable<FullKey>> GetEnumerator() =>
            GetAllKeys().Concat(_parent).GetEnumerator();

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)0x200)]
        public IDisposable Subscribe(IObserver<ContainerEvent> observer)
        {
            lock (_lockObject)
            {
                CheckIsNotDisposed();
                return _eventSubject.Subscribe(observer ?? throw new ArgumentNullException(nameof(observer)));
            }
        }

        [MethodImpl((MethodImplOptions)0x200)]
        internal void Reset()
        {
            lock (_lockObject)
            {
                Resolvers = Table<FullKey, ResolverDelegate>.Empty;
                ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
            }
        }

        [MethodImpl((MethodImplOptions)0x100)]
        private void CheckIsNotDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(ToString());
            }
        }

        [MethodImpl((MethodImplOptions)0x200)]
        private void UnregisterKeys(List<FullKey> registeredKeys, IDependency dependency, ILifetime lifetime)
        {
            lock (_lockObject)
            {
                CheckIsNotDisposed();

                foreach (var curKey in registeredKeys)
                {
                    if (curKey.Tag == AnyTag)
                    {
                        TryUnregister(curKey.Type, ref _registrationsTagAny);
                    }
                    else
                    {
                        TryUnregister(curKey, ref _registrations);
                    }
                }

                _eventSubject.OnNext(ContainerEvent.UnregisterDependency(this, registeredKeys, dependency, lifetime));
            }
        }

        [MethodImpl((MethodImplOptions)0x100)]
        private IEnumerable<IEnumerable<FullKey>> GetAllKeys()
        {
            lock (_lockObject)
            {
                CheckIsNotDisposed();
                return _registrations.Select(i => i.Value.Keys).Concat(_registrationsTagAny.Select(i => i.Value.Keys)).Distinct();
            }
        }

        [MethodImpl((MethodImplOptions)0x100)]
        private bool TryUnregister<TKey>(TKey key, [NotNull] ref Table<TKey, Registration> entries)
        {
            entries = entries.Remove(key, out var unregistered);
            if (!unregistered)
            {
                return false;
            }

            return true;
        }

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        internal static string CreateContainerName([CanBeNull] string name = "") =>
            !string.IsNullOrWhiteSpace(name) ? name : Interlocked.Increment(ref _containerId).ToString(CultureInfo.InvariantCulture);

        [MethodImpl((MethodImplOptions)0x100)]
        private void ApplyConfigurations(params IConfiguration[] configurations) =>
            _resources.Add(this.Apply(configurations));

        [MethodImpl((MethodImplOptions)0x200)]
        private bool TryGetRegistration(FullKey key, out Registration registration)
        {
            if (_registrations.TryGetByKey(key, out registration))
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
                if (_registrations.TryGetByKey(genericKey, out registration))
                {
                    return true;
                }

                // For generic type and Any tag
                if (_registrationsTagAny.TryGetByType(genericType, out registration))
                {
                    return true;
                }
            }

            // For Any tag
            if (_registrationsTagAny.TryGetByType(type, out registration))
            {
                return true;
            }

            // For array
            if (typeDescriptor.IsArray())
            {
                var arrayKey = new FullKey(typeof(IArray), key.Tag);
                // For generic type
                if (_registrations.TryGetByKey(arrayKey, out registration))
                {
                    return true;
                }

                // For generic type and Any tag
                if (_registrationsTagAny.TryGetByType(typeof(IArray), out registration))
                {
                    return true;
                }
            }

            return false;
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
        public readonly bool IsSuccess;

        /// <summary>
        /// Error during operation.
        /// </summary>
        [CanBeNull] public readonly Exception Error;

        /// <summary>
        /// The changed keys.
        /// </summary>
        [CanBeNull] public readonly IEnumerable<Key> Keys;

        /// <summary>
        /// Related dependency.
        /// </summary>
        [CanBeNull] public readonly IDependency Dependency;

        /// <summary>
        /// Related lifetime.
        /// </summary>
        [CanBeNull] public readonly ILifetime Lifetime;

        /// <summary>
        /// Related lifetime.
        /// </summary>
        [CanBeNull] public readonly LambdaExpression ResolverExpression;

        internal ContainerEvent(
            [NotNull] IContainer container,
            EventType eventType,
            bool isSuccess,
            [CanBeNull] Exception error,
            [CanBeNull] IEnumerable<Key> keys,
            [CanBeNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [CanBeNull] LambdaExpression resolverExpression)
        {
            Container = container;
            EventType = eventType;
            IsSuccess = isSuccess;
            Error = error;
            Keys = keys;
            Dependency = dependency;
            Lifetime = lifetime;
            ResolverExpression = resolverExpression;
        }

        internal static ContainerEvent NewContainer(
            [NotNull] IContainer newContainer)
        {
            return new ContainerEvent(
                newContainer,
                EventType.CreateContainer,
                true,
                default(Exception),
                default(IEnumerable<Key>),
                default(IDependency),
                default(ILifetime),
                default(LambdaExpression));
        }

        internal static ContainerEvent DisposeContainer(
            [NotNull] IContainer disposingContainer)
        {
            return new ContainerEvent(
                disposingContainer,
                EventType.DisposeContainer,
                true,
                default(Exception),
                default(IEnumerable<Key>),
                default(IDependency),
                default(ILifetime),
                default(LambdaExpression));
        }

        internal static ContainerEvent RegisterDependency(
            [NotNull] IContainer registeringContainer,
            [NotNull] IEnumerable<Key> keys,
            [CanBeNull] IDependency dependency = default(IDependency),
            [CanBeNull] ILifetime lifetime = default(ILifetime))
        {
            return new ContainerEvent(
                registeringContainer,
                EventType.RegisterDependency,
                true,
                default(Exception),
                keys,
                dependency,
                lifetime,
                default(LambdaExpression));
        }

        internal static ContainerEvent RegisterDependencyFailed(
            [NotNull] IContainer registeringContainer,
            [NotNull] IEnumerable<Key> keys,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] Exception error)
        {
            return new ContainerEvent(
                registeringContainer,
                EventType.RegisterDependency,
                false,
                error,
                keys,
                dependency,
                lifetime,
                default(LambdaExpression));
        }

        internal static ContainerEvent UnregisterDependency(
            [NotNull] IContainer registeringContainer,
            [NotNull] IEnumerable<Key> keys,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime)
        {
            return new ContainerEvent(
                registeringContainer,
                EventType.ContainerStateSingletonLifetime,
                true,
                default(Exception),
                keys,
                dependency,
                lifetime,
                default(LambdaExpression));
        }

        internal static ContainerEvent Compilation(
            [NotNull] IContainer registeringContainer,
            [NotNull] IEnumerable<Key> keys,
            [NotNull] LambdaExpression resolverExpression)
        {
            return new ContainerEvent(
                registeringContainer,
                EventType.ResolverCompilation,
                true,
                default(Exception),
                keys,
                default(IDependency),
                default(ILifetime),
                resolverExpression);
        }

        internal static ContainerEvent CompilationFailed(
            [NotNull] IContainer registeringContainer,
            [NotNull] IEnumerable<Key> keys,
            [NotNull] LambdaExpression resolverExpression,
            [NotNull] Exception error)
        {
            return new ContainerEvent(
                registeringContainer,
                EventType.ResolverCompilation,
                false,
                error,
                keys,
                default(IDependency),
                default(ILifetime),
                resolverExpression);
        }
    }
}


#endregion
#region Context'1

namespace IoC
{
    /// <summary>
    /// Represents the initializing context.
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
    /// Container event types.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// On container creation.
        /// </summary>
        CreateContainer,

        /// <summary>
        /// On container disposing.
        /// </summary>
        DisposeContainer,

        /// <summary>
        /// On dependency registration.
        /// </summary>
        RegisterDependency,

        /// <summary>
        /// On dependency unregistration.
        /// </summary>
        ContainerStateSingletonLifetime,

        /// <summary>
        /// On resolver compilation.
        /// </summary>
        ResolverCompilation
    }
}

#endregion
#region FluentBind

namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Linq.Expressions;
    using Core;
    using Dependencies;
    using Issues;

    /// <summary>
    /// Represents extensions to add bindings to the container.
    /// </summary>
    [PublicAPI]
    public static partial class FluentBind
    {
        /// <summary>
        /// Determines if the container or any his parents have a binding.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="type">The contract type.</param>
        /// <param name="tag">The tag value.</param>
        /// <returns><c>True</c> if the binding exists.</returns>
        public static bool IsBound(this IContainer container, Type type, object tag = null) =>
            container.TryGetDependency(new Key(type, tag), out _, out _);

        /// <summary>
        /// Determines if the container or any his parents have a binding.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag value.</param>
        /// <returns><c>True</c> if the binding exists.</returns>
        public static bool IsBound<T>(this IContainer container, object tag = null) =>
            container.IsBound(typeof(T), tag);

        /// <summary>
        /// Determines if a related instance can be resolved.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="type">The contract type.</param>
        /// <param name="tag">The tag value.</param>
        /// <returns><c>True</c> if the binding exists.</returns>
        public static bool CanResolve(this IContainer container, Type type, object tag = null) =>
            container.TryGetResolver<object>(type, tag, out _, out _, container);

        /// <summary>
        /// Determines if a related instance can be resolved.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag value.</param>
        /// <returns><c>True</c> if the binding exists.</returns>
        public static bool CanResolve<T>(this IContainer container, object tag = null) =>
            container.TryGetResolver<T>(typeof(T), tag, out _, out _, container);

        /// <summary>
        /// Binds types.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="types">A set of contract types.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<object> Bind([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params Type[] types)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (types.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(types));
            return new Binding<object>(container, types);
        }

        /// <summary>
        /// Binds types.
        /// </summary>
        /// <param name="token">The container binding token.</param>
        /// <param name="types">A set of contract types.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T));
        }

        /// <summary>
        /// Binds the type.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="token">The container binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IToken token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T));
        }

        /// <summary>
        /// Binds the type.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="binding"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IBinding binding)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T));
        }

        /// <summary>
        /// Assigns well-known lifetime to the binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding"></param>
        /// <param name="lifetime"></param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
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
        [MethodImpl((MethodImplOptions)0x100)]
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
        [MethodImpl((MethodImplOptions)0x100)]
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
        /// <param name="binding">The binding token.</param>
        /// <param name="tagValue">The tag value.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
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
        [MethodImpl((MethodImplOptions)0x100)]
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken To(
            [NotNull] this IBinding<object> binding,
            [NotNull] Type type,
            [NotNull][ItemNotNull] params Expression<Action<Context<object>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return binding.To(new AutowiringDependency(type, binding.AutowiringStrategy, statements));
        }

        /// <summary>
        /// Registers autowiring binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull][ItemNotNull] params Expression<Action<Context<T>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return binding.To(new AutowiringDependency(typeof(T), binding.AutowiringStrategy, statements));
        }

        /// <summary>
        /// Registers autowiring binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull] Expression<Func<Context, T>> factory,
            [NotNull][ItemNotNull] params Expression<Action<Context<T>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return binding.To(new ExpressionDependency(factory, binding.AutowiringStrategy, statements));
        }

        /// <summary>
        /// Registers autowiring binding.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="dependency">The dependency.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull] IDependency dependency)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var tags = binding.Tags.DefaultIfEmpty(null);
            var keys = (
                from type in binding.Types
                from tag in tags
                select new Key(type, tag))
                .ToArray();

            var token = (binding.Container.TryRegisterDependency(keys, dependency, binding.Lifetime, out var dependencyToken)
                ? dependencyToken
                : binding.Container.Resolve<ICannotRegister>().Resolve(binding.Container, keys, dependency, binding.Lifetime)).AsTokenOf(binding.Container);

            var tokens = new List<IToken>(binding.Tokens) { token };
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IMutableContainer container)
            where T: T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1));
        }

        
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IBinding binding)
            where T: T1
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1));
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IToken token)
            where T: T1
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1));
        }
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IMutableContainer container)
            where T: T1, T2
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2));
        }

        
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IBinding binding)
            where T: T1, T2
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2));
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <param name="token">The binding token.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2>([NotNull] this IToken token)
            where T: T1, T2
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3));
        }

        
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IBinding binding)
            where T: T1, T2, T3
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IToken token)
            where T: T1, T2, T3
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        
        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type #1.</typeparam>
        /// <typeparam name="T2">The contract type #2.</typeparam>
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IToken token)
            where T: T1, T2, T3, T4
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>([NotNull] this IMutableContainer container)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31), typeof(T32));
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
        /// <param name="binding">The target binding.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>([NotNull] this IBinding binding)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new Binding<T>(binding, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31), typeof(T32));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>([NotNull] this IToken token)
            where T: T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return new Binding<T>(token, typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16), typeof(T17), typeof(T18), typeof(T19), typeof(T20), typeof(T21), typeof(T22), typeof(T23), typeof(T24), typeof(T25), typeof(T26), typeof(T27), typeof(T28), typeof(T29), typeof(T30), typeof(T31), typeof(T32));
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IConfiguration Create([NotNull] Func<IContainer, IToken> configurationFactory) =>
            new ConfigurationFromDelegate(configurationFactory ?? throw new ArgumentNullException(nameof(configurationFactory)));

        /// <summary>
        /// Converts a disposable resource to the container's token.
        /// </summary>
        /// <param name="disposableToken">A disposable resource.</param>
        /// <param name="container">The target container.</param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken AsTokenOf([NotNull] this IDisposable disposableToken, [NotNull] IMutableContainer container) =>
            new Token(container ?? throw new ArgumentNullException(nameof(container)), disposableToken ?? throw new ArgumentNullException(nameof(disposableToken)));

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params string[] configurationText) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurationText);

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params Stream[] configurationStreams) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurationStreams);

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params TextReader[] configurationReaders) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurationReaders);

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] IEnumerable<IConfiguration> configurations)
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] IEnumerable<IConfiguration> configurations) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurations);

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params IConfiguration[] configurations) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurations);

        /// <summary>
        /// Applies a configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The target container token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Apply<T>([NotNull] this IMutableContainer container)
            where T : IConfiguration, new() =>
            (container ?? throw new ArgumentNullException(nameof(container))).Apply(new T());

        /// <summary>
        /// Applies a configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="token">The target container token.</param>
        /// <returns>The target container token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IMutableContainer Using([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IMutableContainer Using([NotNull] this IToken token, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IMutableContainer Using<T>([NotNull] this IMutableContainer container)
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IMutableContainer Using<T>([NotNull] this IToken token)
            where T : IConfiguration, new()
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Using<T>();
        }

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        private static IToken ApplyConfigurationFromData<T>([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params T[] configurationData)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationData == null) throw new ArgumentNullException(nameof(configurationData));
            if (configurationData.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationData));
            return container.Apply(configurationData.Select(configurationItem => container.Resolve<IConfiguration>(configurationItem)).ToArray());
        }
    }
}

#endregion
#region FluentContainer

namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Extension methods for IoC containers and configurations.
    /// </summary>
    [PublicAPI]
    public static class FluentContainer
    {
        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="parentContainer">The parent container.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IMutableContainer Create([NotNull] this IContainer parentContainer, [NotNull] string name = "")
        {
            if (parentContainer == null) throw new ArgumentNullException(nameof(parentContainer));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return parentContainer.Resolve<IMutableContainer>(parentContainer, name);
        }

        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="token">The parent container token.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IMutableContainer Create([NotNull] this IToken token, [NotNull] string name = "")
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return token.Container.Resolve<IMutableContainer>(token.Container, name);
        }

        /// <summary>
        /// Buildups an instance which was not registered in container. Can be used as entry point of DI.
        /// </summary>
        /// <param name="configuration">The configurations.</param>
        /// <param name="args">The optional arguments.</param>
        /// <typeparam name="TInstance">The instance type.</typeparam>
        /// <returns>The disposable instance holder.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static ICompositionRoot<TInstance> BuildUp<TInstance>([NotNull] this IConfiguration configuration, [NotNull] [ItemCanBeNull] params object[] args)
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static ICompositionRoot<TInstance> BuildUp<TInstance>([NotNull] this IToken token, [NotNull] [ItemCanBeNull] params object[] args)
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
        [MethodImpl((MethodImplOptions)0x200)]
        public static ICompositionRoot<TInstance> BuildUp<TInstance>([NotNull] this IMutableContainer container, [NotNull] [ItemCanBeNull] params object[] args)
            where TInstance : class
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (container.TryGetResolver<TInstance>(out var resolver))
            {
                return new CompositionRoot<TInstance>(new Token(container, Disposable.Empty), resolver(container, args));
            }

            var buildId = Guid.NewGuid();
            var token = container.Bind<TInstance>().Tag(buildId).To();
            try
            {
                var instance = container.Resolve<TInstance>(buildId.AsTag(), args);
                return new CompositionRoot<TInstance>(token, instance);
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
#region FluentGetResolver

namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag) =>
            container is Container nativeContainer 
                ? nativeContainer.GetResolver<T>(type, tag)
                : container.TryGetResolver<T>(type, tag.Value, out var resolver, out var error) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type, tag), error);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container"></param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(type, tag.Value, out resolver, out _);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, Tag tag) =>
            container is Container nativeContainer
                ? nativeContainer.GetResolver<T>(typeof(T), tag)
                : container.GetResolver<T>(typeof(T), tag);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="tag">The tag of binding.</param>
        /// <param name="container">The target container.</param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, Tag tag, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(typeof(T), tag, out resolver);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The resolver type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container, [NotNull] Type type) =>
            container is Container nativeContainer
                ? nativeContainer.GetResolver<T>(type)
                : container.TryGetResolver<T>(type, null, out var resolver, out var error) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type), error);

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="container">The target container.</param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, [NotNull] Type type, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(type, null, out resolver, out _);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this IContainer container) =>
            container is Container nativeContainer
                ? nativeContainer.GetResolver<T>()
                : container.GetResolver<T>(typeof(T));

        /// <summary>
        /// Tries getting the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="resolver"></param>
        /// <returns>True if success.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        public static bool TryGetResolver<T>([NotNull] this IContainer container, [NotNull] out Resolver<T> resolver)
            => container.TryGetResolver(typeof(T), out resolver);

        /// <summary>
        /// Creates tag.
        /// </summary>
        /// <param name="tagValue">The tag value.</param>
        /// <returns>The tag.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        public static Tag AsTag([CanBeNull] this object tagValue) => new Tag(tagValue);
    }
}

#endregion
#region FluentNativeGetResolver

// ReSharper disable ForCanBeConvertedToForeach
namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;
    using Issues;

    /// <summary>
    /// Represents extensions to resolve from the native container.
    /// </summary>
    [PublicAPI]
    public static class FluentNativeGetResolver
    {
        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x200)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container)
        {
            var bucket = container.ResolversByType.Buckets[TypeDescriptor<T>.HashCode & container.ResolversByType.Divisor].KeyValues;
            for (var index = 0; index < bucket.Length; index++)
            {
                var item = bucket[index];
                if (typeof(T) == item.Key)
                {
                    return (Resolver<T>)item.Value;
                }
            }

            return container.TryGetResolver<T>(new Key(typeof(T)), out var resolver, out var error, container) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(typeof(T)), error);
        }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x200)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container, Tag tag)
        {
            var key = new Key(typeof(T), tag);
            var bucket = container.Resolvers.Buckets[key.GetHashCode() & container.Resolvers.Divisor].KeyValues;
            for (var index = 0; index < bucket.Length; index++)
            {
                var item = bucket[index];
                if (key.Equals(item.Key))
                {
                    return (Resolver<T>)item.Value;
                }
            }

            return container.TryGetResolver<T>(new Key(typeof(T), tag.Value), out var resolver, out var error, container) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(typeof(T), tag), error);
        }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x200)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container, [NotNull] Type type)
        {
            var bucket = container.ResolversByType.Buckets[type.GetHashCode() & container.ResolversByType.Divisor].KeyValues;
            for (var index = 0; index < bucket.Length; index++)
            {
                var item = bucket[index];
                if (type == item.Key)
                {
                    return (Resolver<T>) item.Value;
                }
            }

            return container.TryGetResolver<T>(new Key(type), out var resolver, out var error, container) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type), error);
        }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The resolver.</returns>
        [MethodImpl((MethodImplOptions)0x200)]
        [NotNull]
        public static Resolver<T> GetResolver<T>([NotNull] this Container container, [NotNull] Type type, Tag tag)
        {
            var key = new Key(type, tag);
            var bucket = container.Resolvers.Buckets[key.GetHashCode() & container.Resolvers.Divisor].KeyValues;
            for (var index = 0; index < bucket.Length; index++)
            {
                var item = bucket[index];
                if (key.Equals(item.Key))
                {
                    return (Resolver<T>)item.Value;
                }
            }

            return container.TryGetResolver<T>(new Key(type, tag.Value), out var resolver, out var error, container) ? resolver : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(type, tag), error);
        }
    }
}

#endregion
#region FluentNativeResolve

// ReSharper disable ForCanBeConvertedToForeach
namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;
    using Issues;

    /// <summary>
    /// Represents extensions to resolve from the native container.
    /// </summary>
    [PublicAPI]
    public static class FluentNativeResolve
    {
        internal static readonly object[] EmptyArgs = CoreExtensions.EmptyArray<object>();

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x200)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container)
        {
            var bucket = container.ResolversByType.Buckets[TypeDescriptor<T>.HashCode & container.ResolversByType.Divisor].KeyValues;
            for (var index = 0; index < bucket.Length; index++)
            {
                var item = bucket[index];
                if (typeof(T) == item.Key)
                {
                    return ((Resolver<T>)item.Value)(container, EmptyArgs);
                }
            }

            return (
                container.TryGetResolver<T>(new Key(typeof(T)), out var resolver, out var error, container)
                    ? resolver
                    : container.Resolve<ICannotGetResolver>().Resolve<T>(container, new Key(typeof(T)), error)
            )(container, EmptyArgs);
        }

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, Tag tag) =>
            container.GetResolver<T>(tag)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] [ItemCanBeNull] params object[] args) =>
            container.GetResolver<T>()(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, Tag tag, [NotNull] [ItemCanBeNull] params object[] args) =>
            container.GetResolver<T>(tag)(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type) =>
            container.GetResolver<T>(type)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type, Tag tag) =>
            container.GetResolver<T>(type, tag)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type, [NotNull] [ItemCanBeNull] params object[] args) =>
            container.GetResolver<T>(type)(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this Container container, [NotNull] Type type, Tag tag, [NotNull] [ItemCanBeNull] params object[] args) =>
            container.GetResolver<T>(type, tag)(container, args);
    }
}

#endregion
#region FluentResolve

namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Represents extensions to resolve from the container.
    /// </summary>
    [PublicAPI]
    public static class FluentResolve
    {
        private static readonly object[] EmptyArgs = CoreExtensions.EmptyArray<object>();

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container) =>
            container.GetResolver<T>()(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull][ItemCanBeNull] params object[] args) 
            => container.GetResolver<T>()(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, Tag tag)
            => container.GetResolver<T>(tag)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, Tag tag, [NotNull][ItemCanBeNull] params object[] args)
            => container.GetResolver<T>(tag)(container, args);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull] Type type)
            => container.GetResolver<T>(type)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
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
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static T Resolve<T>([NotNull] this IContainer container, [NotNull] Type type, Tag tag)
            => container.GetResolver<T>(type, tag)(container, EmptyArgs);

        /// <summary>
        /// Resolves an instance.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="type">The resolving instance type.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
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
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents extensions dealing with scopes.
    /// </summary>
    public static class FluentScope
    {
        /// <summary>
        /// Creates a new resolving scope. Can be used with <c>ScopeSingleton</c>.
        /// </summary>
        /// <param name="container">A container to resolve a scope.</param>
        /// <returns>Tne new scope instance.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IScope CreateScope([NotNull] this IContainer container) =>
            (container ?? throw new ArgumentNullException(nameof(container))).Resolve<IScope>();
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
        /// Gets a container trace source.
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
        /// Traces container actions through a handler.
        /// </summary>
        /// <param name="container">The target container to trace.</param>
        /// <param name="onTraceEvent">The trace handler.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IMutableContainer container, [NotNull] Action<TraceEvent> onTraceEvent)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (onTraceEvent == null) throw new ArgumentNullException(nameof(onTraceEvent));

            return new Token(
                container,
                container
                    .ToTraceSource()
                    .Subscribe(
                        onTraceEvent,
                        error => { },
                        () => { }));
        }

        /// <summary>
        /// Traces container actions through a handler.
        /// </summary>
        /// <param name="token">The token of target container to trace.</param>
        /// <param name="onTraceEvent">The trace handler.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IToken token, [NotNull] Action<TraceEvent> onTraceEvent) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Trace(onTraceEvent ?? throw new ArgumentNullException(nameof(onTraceEvent)));

#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETCOREAPP1_0&& !NETCOREAPP1_1 && !WINDOWS_UWP
        /// <summary>
        /// Traces container actions through a <c>System.Diagnostics.Trace</c>.
        /// </summary>
        /// <param name="container">The target container to trace.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IMutableContainer container) =>
            (container ?? throw new ArgumentNullException(nameof(container))).Trace(e => System.Diagnostics.Trace.WriteLine(e.Message));

        /// <summary>
        /// Traces container actions through a <c>System.Diagnostics.Trace</c>.
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

                        observer.OnNext(new TraceEvent(value, message ?? string.Empty));
                    },
                    observer.OnError,
                    observer.OnCompleted);

                subscriptions.Add(container, subscription);
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
    /// Represents the generic type arguments marker.
    /// </summary>
    [PublicAPI, AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class GenericTypeArgumentAttribute : Attribute { }
}


#endregion
#region GenericTypeArguments


// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace IoC
{
    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS { }

/// <summary>
    /// Represents the generic type arguments marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable: System.IDisposable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable: System.IComparable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT1 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC1 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI1 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS1 { }

/// <summary>
    /// Represents the generic type arguments marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable1: System.IDisposable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable1: System.IComparable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable1<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable1<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable1<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator1<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection1<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList1<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet1<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer1<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer1<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary1<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable1<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver1<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT2 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC2 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI2 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS2 { }

/// <summary>
    /// Represents the generic type arguments marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable2: System.IDisposable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable2: System.IComparable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable2<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable2<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable2<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator2<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection2<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList2<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet2<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer2<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer2<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary2<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable2<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver2<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT3 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC3 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI3 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS3 { }

/// <summary>
    /// Represents the generic type arguments marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable3: System.IDisposable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable3: System.IComparable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable3<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable3<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable3<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator3<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection3<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList3<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet3<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer3<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer3<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary3<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable3<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver3<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT4 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC4 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI4 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS4 { }

/// <summary>
    /// Represents the generic type arguments marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable4: System.IDisposable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable4: System.IComparable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable4<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable4<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable4<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator4<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection4<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList4<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet4<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer4<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer4<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary4<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable4<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver4<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT5 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC5 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI5 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS5 { }

/// <summary>
    /// Represents the generic type arguments marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable5: System.IDisposable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable5: System.IComparable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable5<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable5<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable5<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator5<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection5<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList5<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet5<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer5<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer5<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary5<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable5<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver5<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT6 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC6 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI6 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS6 { }

/// <summary>
    /// Represents the generic type arguments marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable6: System.IDisposable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable6: System.IComparable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable6<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable6<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable6<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator6<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection6<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList6<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet6<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer6<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer6<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary6<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable6<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver6<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT7 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC7 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI7 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS7 { }

/// <summary>
    /// Represents the generic type arguments marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable7: System.IDisposable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable7: System.IComparable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable7<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable7<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable7<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator7<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection7<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList7<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet7<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer7<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer7<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary7<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable7<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver7<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT8 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC8 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI8 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS8 { }

/// <summary>
    /// Represents the generic type arguments marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable8: System.IDisposable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable8: System.IComparable { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable8<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable8<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable8<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator8<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection8<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList8<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet8<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer8<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer8<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary8<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable8<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type arguments marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver8<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT9 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC9 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI9 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS9 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT10 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC10 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI10 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS10 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT11 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC11 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI11 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS11 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT12 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC12 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI12 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS12 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT13 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC13 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI13 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS13 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT14 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC14 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI14 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS14 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT15 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC15 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI15 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS15 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT16 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC16 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI16 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS16 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT17 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC17 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI17 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS17 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT18 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC18 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI18 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS18 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT19 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC19 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI19 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS19 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT20 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC20 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI20 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS20 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT21 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC21 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI21 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS21 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT22 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC22 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI22 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS22 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT23 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC23 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI23 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS23 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT24 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC24 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI24 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS24 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT25 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC25 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI25 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS25 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT26 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC26 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI26 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS26 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT27 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC27 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI27 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS27 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT28 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC28 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI28 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS28 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT29 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC29 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI29 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS29 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT30 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC30 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI30 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS30 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT31 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC31 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI31 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS31 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT32 { }

    /// <summary>
    /// Represents the generic type arguments marker for a reference type with defaul constructor.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public class TTC32 { }

    /// <summary>
    /// Represents the generic type arguments marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI32 { }

    /// <summary>
    /// Represents the generic type arguments marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS32 { }


    internal static class GenericTypeArguments
    {
        internal static readonly System.Type[] Arguments =
        {
            typeof(TT),
            typeof(TT1),
            typeof(TT2),
            typeof(TT3),
            typeof(TT4),
            typeof(TT5),
            typeof(TT6),
            typeof(TT7),
            typeof(TT8),
            typeof(TT9),
            typeof(TT10),
            typeof(TT11),
            typeof(TT12),
            typeof(TT13),
            typeof(TT14),
            typeof(TT15),
            typeof(TT16),
            typeof(TT17),
            typeof(TT18),
            typeof(TT19),
            typeof(TT20),
            typeof(TT21),
            typeof(TT22),
            typeof(TT23),
            typeof(TT24),
            typeof(TT25),
            typeof(TT26),
            typeof(TT27),
            typeof(TT28),
            typeof(TT29),
            typeof(TT30),
            typeof(TT31),
            typeof(TT32),
        };
    }
}


#endregion
#region IAutowiringStrategy

namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents an abstraction for autowiring strategy.
    /// </summary>
    [PublicAPI]
    public interface IAutowiringStrategy
    {
        /// <summary>
        /// Resolves type to create an instance.
        /// </summary>
        /// <param name="container">Current container.</param>
        /// <param name="registeredType">Registered type.</param>
        /// <param name="resolvingType">Resolving type.</param>
        /// <param name="instanceType">The type to create an instance.</param>
        /// <returns>True if the type was resolved.</returns>
        bool TryResolveType([NotNull] IContainer container, [NotNull] Type registeredType, [NotNull] Type resolvingType, out Type instanceType);

        /// <summary>
        /// Resolves a constructor from a set of available constructors.
        /// </summary>
        /// <param name="container">Current container.</param>
        /// <param name="constructors">The set of available constructors.</param>
        /// <param name="constructor">The resolved constructor.</param>
        /// <returns>True if the constructor was resolved.</returns>
        bool TryResolveConstructor([NotNull] IContainer container, [NotNull][ItemNotNull] IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor);

        /// <summary>
        /// Resolves initializing methods from a set of available methods/setters in the specific order which will be used to invoke them.
        /// </summary>
        /// <param name="container">Current container.</param>
        /// <param name="methods">The set of available methods.</param>
        /// <param name="initializers">The set of initializing methods in the appropriate order.</param>
        /// <returns>True if initializing methods were resolved.</returns>
        bool TryResolveInitializers([NotNull] IContainer container, [NotNull][ItemNotNull] IEnumerable<IMethod<MethodInfo>> methods, [ItemNotNull] out IEnumerable<IMethod<MethodInfo>> initializers);
    }
}


#endregion
#region IBinding

namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The an abstract containers binding.
    /// </summary>
    [PublicAPI]
    // ReSharper disable once UnusedTypeParameter
    public interface IBinding
    {
        /// <summary>
        /// The target container to configure.
        /// </summary>
        [NotNull] IMutableContainer Container { get; }

        /// <summary>
        /// Binding tokens.
        /// </summary>
        [NotNull] IEnumerable<IToken> Tokens { get; }

        /// <summary>
        /// The contract type to bind.
        /// </summary>
        [NotNull] [ItemNotNull] IEnumerable<Type> Types { get; }

        /// <summary>
        /// The tags to mark this binding.
        /// </summary>
        [NotNull] [ItemCanBeNull] IEnumerable<object> Tags { get; }

        /// <summary>
        /// The lifetime instance or null by default.
        /// </summary>
        [CanBeNull] ILifetime Lifetime { get; }

        /// <summary>
        /// The autowiring strategy or null by default.
        /// </summary>
        [CanBeNull] IAutowiringStrategy AutowiringStrategy { get; }
    }

    /// <summary>
    /// The containers binding.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    // ReSharper disable once UnusedTypeParameter
    public interface IBinding<in T> : IBinding { }
}


#endregion
#region IBuildContext

namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an abstract build context.
    /// </summary>
    [PublicAPI]
    public interface IBuildContext
    {
        /// <summary>
        /// Provides a parent context or <c>null</c>.
        /// </summary>
        [CanBeNull] IBuildContext Parent { get; }

        /// <summary>
        /// The target key to build resolver.
        /// </summary>
        Key Key { get; }

        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] IContainer Container { get; }

        /// <summary>
        /// The depth of current context in the build tree.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// The list of compilers.
        /// </summary>
        [NotNull] IEnumerable<ICompiler> Compilers { get; }

        /// <summary>
        /// The current autowiring strategy.
        /// </summary>
        [NotNull] IAutowiringStrategy AutowiringStrategy { get; }

        /// <summary>
        /// The args parameters.
        /// </summary>
        [NotNull] ParameterExpression ArgsParameter { get; }

        /// <summary>
        /// The container parameter.
        /// </summary>
        [NotNull] ParameterExpression ContainerParameter { get; }

        /// <summary>
        /// Creates a child context.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="container">The container.</param>
        /// <returns>The new build context.</returns>
        [NotNull] IBuildContext CreateChild(Key key, [NotNull] IContainer container);

        /// <summary>
        /// Create the expression.
        /// </summary>
        /// <param name="defaultExpression">The default expression.</param>
        /// <returns>The expression.</returns>
        [NotNull] Expression CreateExpression([CanBeNull] Expression defaultExpression = null);

        /// <summary>
        /// Finalizes an expression and adds a lifetime.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <returns></returns>
        [NotNull] Expression FinalizeExpression([NotNull] Expression baseExpression, [CanBeNull] ILifetime lifetime);

        /// <summary>
        /// Adds types mapping.
        /// </summary>
        /// <param name="fromType">Type to map.</param>
        /// <param name="toType">The target type.</param>
        void MapType([NotNull] Type fromType, [NotNull] Type toType);

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        /// <param name="parameterExpression">The parameters expression to add.</param>
        void AddParameter([NotNull] ParameterExpression parameterExpression);

        /// <summary>
        /// Compiles a lambda expression to delegate.
        /// </summary>
        /// <param name="lambdaExpression">The lambda expression to compile.</param>
        /// <param name="lambdaCompiled">The compiled lambda.</param>
        /// <param name="error">Compilation error.</param>
        /// <returns>True if success.</returns>
        bool TryCompile([NotNull] LambdaExpression lambdaExpression, out Delegate lambdaCompiled, out Exception error);
    }
}

#endregion
#region IBuilder

namespace IoC
{
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an abstract builder for an instance.
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Builds the expression based on a build context.
        /// </summary>
        /// <param name="context">Current build context.</param>
        /// <param name="bodyExpression">The expression body to build an instance resolver.</param>
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
    /// Represents an abstract expression compiler.
    /// </summary>
    [PublicAPI]
    public interface ICompiler
    {
        /// <summary>
        /// Compiles a lambda expression to delegate.
        /// </summary>
        /// <param name="context">Current context for building.</param>
        /// <param name="lambdaExpression">The lambda expression to compile.</param>
        /// <param name="lambdaCompiled">The compiled lambda.</param>
        /// <param name="error">Compilation error.</param>
        /// <returns>True if success.</returns>
        bool TryCompile([NotNull] IBuildContext context, [NotNull] LambdaExpression lambdaExpression, out Delegate lambdaCompiled, out Exception error);
    }
}


#endregion
#region ICompositionRoot

namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstract composition root.
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    [PublicAPI]
    public interface ICompositionRoot<out TInstance>: IDisposable
    {
        /// <summary>
        /// The composition root instance.
        /// </summary>
        [NotNull] TInstance Instance { get; }
    }
}

#endregion
#region IConfiguration

namespace IoC
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an abstract containers configuration.
    /// </summary>
    [PublicAPI]
    public interface IConfiguration
    {
        /// <summary>
        /// Applies a configuration to the target mutable container.
        /// </summary>
        /// <param name="container">The target mutable container to configure.</param>
        /// <returns>The enumeration of configuration tokens which allows to cancel that changes.</returns>
        [NotNull][ItemNotNull] IEnumerable<IToken> Apply([NotNull] IMutableContainer container);
    }
}


#endregion
#region IContainer

namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an abstract Inversion of Control container.
    /// </summary>
    [PublicAPI]
    public interface IContainer: IEnumerable<IEnumerable<Key>>, IObservable<ContainerEvent>, IResourceRegistry
    {
        /// <summary>
        /// Provides a parent container or <c>null</c> if it does not have a parent.
        /// </summary>
        [CanBeNull] IContainer Parent { get; }

        /// <summary>
        /// Provides a dependency and a lifetime for the registered key.
        /// </summary>
        /// <param name="key">The key to get a dependency.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>True if successful.</returns>
        bool TryGetDependency(Key key, out IDependency dependency, [CanBeNull] out ILifetime lifetime);

        /// <summary>
        /// Provides a resolver for a specific type and tag or error if something goes wrong.
        /// </summary>
        /// <typeparam name="T">The type of instance producing by the resolver.</typeparam>
        /// <param name="type">The binding type.</param>
        /// <param name="tag">The binding tag or null if there is no tag.</param>
        /// <param name="resolver">The resolver to get an instance.</param>
        /// <param name="error">Error that occurs when resolving.</param>
        /// <param name="resolvingContainer">The resolving container and null if the resolving container is the current container.</param>
        /// <returns><c>True</c> if successful and a resolver was provided.</returns>
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
    /// Represents an abstract IoC dependency.
    /// </summary>
    [PublicAPI]
    public interface IDependency
    {
        /// <summary>
        /// Builds an expression for dependency based on the current build context and specified lifetime.
        /// </summary>
        /// <param name="buildContext">The build context.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="expression">The resulting expression for the current dependency.</param>
        /// <param name="error">The error if something goes wrong.</param>
        /// <returns><c>True</c> if successful and an expression was provided.</returns>
        bool TryBuildExpression([NotNull] IBuildContext buildContext, [CanBeNull] ILifetime lifetime, out Expression expression, out Exception error);
    }
}


#endregion
#region ILifetime

namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstraction of container lifetime.
    /// </summary>
    [PublicAPI]
    public interface ILifetime: IBuilder, IDisposable
    {
        /// <summary>
        /// Creates the similar lifetime to use with generic instances.
        /// </summary>
        /// <returns>The new lifetime instance.</returns>
        ILifetime Create();

        /// <summary>
        /// Provides a container to resolve dependencies.
        /// </summary>
        /// <param name="registrationContainer">The container where a dependency was registered.</param>
        /// <param name="resolvingContainer">The container which is used to resolve an instance.</param>
        /// <returns>The selected container.</returns>
        [NotNull] IContainer SelectResolvingContainer([NotNull] IContainer registrationContainer, [NotNull] IContainer resolvingContainer);
    }
}


#endregion
#region IMethod

namespace IoC
{
    using System;
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
        /// The methods information.
        /// </summary>
        [NotNull] TMethodInfo Info { get; }

        /// <summary>
        /// Provides a set of parameters expressions.
        /// </summary>
        /// <returns>Parameters' expressions</returns>
        [NotNull][ItemNotNull] IEnumerable<Expression> GetParametersExpressions([NotNull] IBuildContext buildContext);

        /// <summary>
        /// Specifies the expression of method parameter at the position.
        /// </summary>
        /// <param name="parameterPosition">The parameter position.</param>
        /// <param name="parameterExpression">The parameter expression.</param>
        void SetExpression(int parameterPosition, [NotNull] Expression parameterExpression);

        /// <summary>
        /// Specifies the dependency type and tag for method parameter at the position.
        /// </summary>
        /// <param name="parameterPosition">The parameter position.</param>
        /// <param name="dependencyType">The dependency type.</param>
        /// <param name="dependencyTag">The optional dependency tag value.</param>
        /// <param name="isOptional"><c>True</c> if it is optional dependency.</param>
        /// <param name="args">The optional arguments.</param>
        void SetDependency(int parameterPosition, [NotNull] Type dependencyType, [CanBeNull] object dependencyTag = null, bool isOptional = false, params object[] args);
    }
}


#endregion
#region IMutableContainer

namespace IoC
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an abstract of configurable Inversion of Control container.
    /// </summary>
    [PublicAPI]

    public interface IMutableContainer: IContainer, IDisposable
    {
        /// <summary>
        /// Registers the dependency and the lifetime for the specified dependency key.
        /// </summary>
        /// <param name="keys">The set of keys to register.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="dependencyToken">The dependency token to unregister this dependency key.</param>
        /// <returns><c>True</c> if is registered successfully.</returns>
        bool TryRegisterDependency([NotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out IToken dependencyToken);
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
    /// A set of injection markers.
    /// </summary>
    [PublicAPI]
    public static class Injections
    {
        private static readonly string JustAMarkerError = $"The method `{nameof(Inject)}` is a marker method and has no implementation. It should be used to configure dependency injection via the constructor or initialization expressions. In other cases please use `{nameof(FluentResolve.Resolve)}` methods.";
        [NotNull] internal static readonly MethodInfo InjectGenericMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => Inject<object>(default(IContainer)))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo TryInjectGenericMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => TryInject<object>(default(IContainer)))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo TryInjectValueGenericMethodInfo = ((MethodCallExpression)((Expression<Func<TTS?>>)(() => TryInjectValue<TTS>(default(IContainer)))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo InjectWithTagGenericMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => Inject<object>(default(IContainer), null))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo TryInjectWithTagGenericMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => TryInject<object>(default(IContainer), null))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo TryInjectValueWithTagGenericMethodInfo = ((MethodCallExpression)((Expression<Func<TTS?>>)(() => TryInjectValue<TTS>(default(IContainer), null))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo AssignGenericMethodInfo = ((MethodCallExpression)((Expression<Action<object, object>>) ((item1, item2) => Assign<object>(default(IContainer), null, null))).Body).Method.GetGenericMethodDefinition();
        [NotNull] internal static readonly MethodInfo InjectMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => Inject(default(IContainer), typeof(object)))).Body).Method;
        [NotNull] internal static readonly MethodInfo TryInjectMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => TryInject(default(IContainer), typeof(object)))).Body).Method;
        [NotNull] internal static readonly MethodInfo InjectWithTagMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => Inject(default(IContainer), typeof(object), (object)null))).Body).Method;
        [NotNull] internal static readonly MethodInfo TryInjectWithTagMethodInfo = ((MethodCallExpression)((Expression<Func<object>>) (() => TryInject(default(IContainer), typeof(object), null))).Body).Method;

        /// <summary>
        /// Injects a dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>([NotNull] this IContainer container) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull] public static T TryInject<T>([NotNull] this IContainer container) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a value type dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull]
        public static T? TryInjectValue<T>([NotNull] this IContainer container) where T : struct =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>([NotNull] this IContainer container, [CanBeNull] object tag, params object[] args) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull] public static T TryInject<T>([NotNull] this IContainer container, [CanBeNull] object tag, params object[] args) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a value type dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull]
        public static T? TryInjectValue<T>([NotNull] this IContainer container, [CanBeNull] object tag, params object[] args) where T : struct =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just the injection marker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="destination">The destination member for injection.</param>
        /// <param name="source">The source of injection.</param>
        public static IContainer Assign<T>([NotNull] this IContainer container, [NotNull] T destination, [CanBeNull] T source) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just the injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static object Inject([NotNull] this IContainer container, [NotNull] Type type) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a dependency. Just the injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull] public static object TryInject([NotNull] this IContainer container, [NotNull] Type type) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Injects a dependency. Just the injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The injected instance.</returns>
        public static object Inject([NotNull] this IContainer container, [NotNull] Type type, [CanBeNull] object tag, params object[] args) =>
            throw new NotImplementedException(JustAMarkerError);

        /// <summary>
        /// Try to inject a dependency. Just the injection marker.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="type">The type of dependency.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The injected instance or <c>default(T)</c>.</returns>
        [CanBeNull] public static object TryInject([NotNull] this IContainer container, [NotNull] Type type, [CanBeNull] object tag, params object[] args) =>
            throw new NotImplementedException(JustAMarkerError);
    }
}


#endregion
#region IResourceRegistry

namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstraction of the resource registry.
    /// </summary>
    [PublicAPI]
    public interface IResourceRegistry
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
    /// Represents an abstraction of a scope which is used with <c>Lifetime.ScopeSingleton</c>.
    /// </summary>
    [PublicAPI]
    public interface IScope : IDisposable
    {
        /// <summary>
        /// Activate the scope.
        /// </summary>
        /// <returns>The token to deactivate the activated scope.</returns>
        IDisposable Activate();
    }
}

#endregion
#region IToken

namespace IoC
{
    using System;

    /// <summary>
    /// Represents an abstraction of a binding token.
    /// </summary>
    public interface IToken: IDisposable
    {
        /// <summary>
        /// The configurable container owning the registered binding.
        /// </summary>
        IMutableContainer Container { get; }
    }
}


#endregion
#region Key

namespace IoC
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Represents a dependency key.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("Type = {" + nameof(Type) + "}, Tag = {" + nameof(Tag) + "}")]
    public struct Key
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

        /// <summary>
        /// Creates the instance of Key.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tag"></param>
        public Key([NotNull] Type type, [CanBeNull] object tag = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Tag = tag;
        }

        /// <inheritdoc />
        [Pure]
        public override string ToString() => $"[Type = {Type.FullName}, Tag = {Tag ?? "empty"}, HashCode = {GetHashCode()}]";

        /// <inheritdoc />
        [Pure]
        // ReSharper disable once PossibleNullReferenceException
        [MethodImpl((MethodImplOptions)0x200)]
        public override bool Equals(object obj) => this.Equals((Key)obj);

        /// <inheritdoc />
        [Pure]
        [MethodImpl((MethodImplOptions)0x100)]
        public bool Equals(Key other) => ReferenceEquals(Type, other.Type) && (ReferenceEquals(Tag, other.Tag) || Equals(Tag, other.Tag));

        /// <inheritdoc />
        [Pure]
        [MethodImpl((MethodImplOptions)0x200)]
        public override int GetHashCode()
        {
            unchecked
            {
                return (Tag?.GetHashCode() * 397 ?? 0) ^ Type.GetHashCode();
            }
        }

        private class AnyTagObject
        {
            [Pure]
            public override string ToString() => "any";
        }
    }
}


#endregion
#region Lifetime

namespace IoC
{
    /// <summary>
    /// A set of well-known lifetimes.
    /// </summary>
    [PublicAPI]
    public enum Lifetime
    {
        /// <summary>
        /// For a new instance each time (default).
        /// </summary>
        Transient = 1,

        /// <summary>
        /// For a singleton instance.
        /// </summary>
        Singleton = 2,

        /// <summary>
        /// For a singleton instance per container.
        /// </summary>
        ContainerSingleton = 3,

        /// <summary>
        /// For a singleton instance per scope.
        /// </summary>
        ScopeSingleton = 4
    }
}


#endregion
#region Resolver

namespace IoC
{
    /// <summary>
    /// Represents an abstraction of instance resolver.
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
#region TraceEvent

namespace IoC
{
    /// <summary>
    /// Represents a container trace event.
    /// </summary>
    [PublicAPI]
    public struct TraceEvent
    {
        /// <summary>
        /// The original container event.
        /// </summary>
        public readonly ContainerEvent ContainerEvent;

        /// <summary>
        /// The trace message.
        /// </summary>
        [NotNull] public readonly string Message;

        /// <summary>
        /// Creates new instance of a trace event.
        /// </summary>
        /// <param name="containerEvent">The original container event.</param>
        /// <param name="message">The trace message.</param>
        internal TraceEvent(ContainerEvent containerEvent, [NotNull] string message)
        {
            ContainerEvent = containerEvent;
            Message = message;
        }
    }
}

#endregion

#endregion

#region Features

#region CollectionFeature

// ReSharper disable MemberCanBeProtected.Local
// ReSharper disable ForCanBeConvertedToForeach
namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
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
        public static readonly IConfiguration Set = new CollectionFeature();

        private CollectionFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(new[] { typeof(IEnumerable<TT>) }, new EnumerableDependency(), null, new[] { Key.AnyTag });
            yield return container.Register(new[] { typeof(TT[]) }, new ArrayDependency(), null, new []{ Key.AnyTag });
            yield return container.Register<List<TT>, IList<TT>, ICollection<TT>>(ctx => new List<TT>(ctx.Container.Inject<TT[]>(ctx.Key)), null, new[] { Key.AnyTag });
            yield return container.Register<HashSet<TT>, ISet<TT>>(ctx => new HashSet<TT>(ctx.Container.Inject<TT[]>(ctx.Key)), null, new[] { Key.AnyTag });
            yield return container.Register<IObservable<TT>>(ctx => new Observable<TT>(ctx.Container.Inject<IEnumerable<TT>>(ctx.Key)), null, new[] { Key.AnyTag });
#if !NET40
            yield return container.Register<ReadOnlyCollection<TT>, IReadOnlyList<TT>, IReadOnlyCollection<TT>>(ctx => new ReadOnlyCollection<TT>(ctx.Container.Inject<TT[]>(ctx.Key)), null, new[] { Key.AnyTag });
#endif
#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            yield return container.Register<IAsyncEnumerable<TT>>(ctx => new AsyncEnumeration<TT>(ctx.Container.Inject<IEnumerable<TT>>(ctx.Key)), null, new[] { Key.AnyTag });
#endif
        }

        [MethodImpl((MethodImplOptions)0x200)]
        private static IEnumerable<Key> GetKeys(IContainer container, Type type)
        {
            var targetType = type.Descriptor();
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
                        yield return new Key(typeToResolve);
                    }
                    else
                    {
                        yield return new Key(typeToResolve, tag);
                    }

                    break;
                }
            }
        }

        private sealed class Observable<T>: IObservable<T>
        {
            private readonly IEnumerable<T> _instances;

            public Observable(IEnumerable<T> instances) => _instances = instances;

            [MethodImpl((MethodImplOptions)0x200)]
            public IDisposable Subscribe(IObserver<T> observer)
            {
                try
                {
                    foreach(var instance in _instances)
                    {
                        observer.OnNext(instance);
                    }
                }
                catch (Exception error)
                {
                    observer.OnError(error);
                }

                observer.OnCompleted();
                return Disposable.Empty;
            }
        }

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
        private sealed class AsyncEnumeration<T> : IAsyncEnumerable<T>
        {
            private readonly IEnumerable<T> _instances;

            public AsyncEnumeration(IEnumerable<T> instances) => _instances = instances;

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()) => 
                new AsyncEnumerator<T>(_instances.GetEnumerator());
        }

        private sealed class AsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _enumerator;

            public AsyncEnumerator(IEnumerator<T> enumerator) => 
                _enumerator = enumerator;

            public async ValueTask<bool> MoveNextAsync() =>
                await new ValueTask<bool>(_enumerator.MoveNext());

            public T Current => _enumerator.Current;

            public ValueTask DisposeAsync() => new ValueTask();
        }
#endif

        private class EnumerableDependency : IDependency
        {
            private static readonly TypeDescriptor EnumerableTypeDescriptor = typeof(Enumerable<>).Descriptor();

            public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
            {
                var type = buildContext.Key.Type.Descriptor();
                var keyComparer = buildContext.Key.Tag as IComparer<Key>;
                if (!type.IsConstructedGenericType())
                {
                    throw new BuildExpressionException($"Unsupported enumerable type {type}.", null);
                }

                var genericTypeArguments = type.GetGenericTypeArguments();
                if (genericTypeArguments.Length != 1)
                {
                    throw new BuildExpressionException($"Unsupported enumerable type {type}.", null);
                }

                var elementType = genericTypeArguments[0];
                var allKeys = GetKeys(buildContext.Container, elementType);
                if (keyComparer != null)
                {
                    allKeys = allKeys.OrderBy(i => i, keyComparer);
                }
                var keys = allKeys.ToArray();
                var positionVar = Expression.Variable(typeof(int));

                var conditionExpression =
                    keys.Length < 5
                        ? CreateConditions(buildContext, keys, elementType, positionVar)
                        : CreateSwitchCases(buildContext, keys, elementType, positionVar);

                if (buildContext.TryCompile(Expression.Lambda(conditionExpression, positionVar, buildContext.ContainerParameter, buildContext.ArgsParameter), out var factory, out error))
                {
                    var ctor = EnumerableTypeDescriptor.MakeGenericType(elementType).Descriptor().GetDeclaredConstructors().Single();
                    var enumerableExpression = Expression.New(ctor, Expression.Constant(factory), Expression.Constant(keys.Length), buildContext.ContainerParameter, buildContext.ArgsParameter);
                    expression = enumerableExpression;
                    return true;
                }

                expression = default(Expression);
                return false;
            }

            private static Expression CreateConditions(IBuildContext buildContext, Key[] keys, Type elementType, ParameterExpression positionVar)
            {
                var conditionExpression = CreateDefault(elementType);
                for (var i = keys.Length - 1; i >= 0; i--)
                {
                    var context = buildContext.CreateChild(keys[i], buildContext.Container);
                    conditionExpression = Expression.Condition(
                        Expression.Equal(positionVar, Expression.Constant(i)),
                        Expression.Convert(context.CreateExpression(), elementType),
                        conditionExpression);
                }

                return conditionExpression;
            }

            private static Expression CreateSwitchCases(IBuildContext buildContext, Key[] keys, Type elementType, ParameterExpression positionVar)
            {
                var cases = new SwitchCase[keys.Length];
                for (var i = 0; i < keys.Length; i++)
                {
                    var context = buildContext.CreateChild(keys[i], buildContext.Container);
                    cases[i] = Expression.SwitchCase(Expression.Convert(context.CreateExpression(), elementType), Expression.Constant(i));
                }

                return Expression.Switch(positionVar, CreateDefault(elementType), cases);
            }

            private static Expression CreateDefault(Type elementType) =>
                Expression.Block(
                    Expression.Throw(Expression.Constant(new BuildExpressionException("Invalid enumeration state.", null))),
                    Expression.Default(elementType));
        }

        private sealed class Enumerable<T> : IEnumerable<T>
        {
            private readonly Func<int, IContainer, object[], T> _factory;
            private readonly int _count;
            private readonly IContainer _container;
            private readonly object[] _args;

            public Enumerable([NotNull] Func<int, IContainer, object[], T> factory, int count, IContainer container, object[] args)
            {
                _factory = factory;
                _count = count;
                _container = container;
                _args = args;
            }

            [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
            public IEnumerator<T> GetEnumerator() => new Enumerator<T>(_factory, _count, _container, _args);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private sealed class Enumerator<T> : IEnumerator<T>
        {
            private readonly Func<int, IContainer, object[], T> _factory;
            private readonly int _count;
            private readonly IContainer _container;
            private readonly object[] _args;
            private int _index = -1;
            private bool _hasCurrent;
            private T _current;

            public Enumerator([NotNull] Func<int, IContainer, object[], T> factory, int count, IContainer container, object[] args)
            {
                _factory = factory;
                _count = count;
                _container = container;
                _args = args;
            }

            [MethodImpl((MethodImplOptions)0x200)]
            public bool MoveNext()
            {
                _hasCurrent = false;
                _current = default(T);
                return ++_index < _count;
            }

            public T Current
            {
                [MethodImpl((MethodImplOptions)0x200)]
                get
                {
                    if (!_hasCurrent)
                    {
                        _hasCurrent = true;
                        _current = _factory(_index, _container, _args);
                    }

                    return _current;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public void Reset() => _index = -1;
        }


        private class ArrayDependency: IDependency
        {
            public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
            {
                var type = buildContext.Key.Type.Descriptor();
                var keyComparer = buildContext.Key.Tag as IComparer<Key>;
                var elementType = type.GetElementType();
                if (elementType == null)
                {
                    throw new BuildExpressionException($"Unsupported array type {type}.", null);
                }

                var allKeys = GetKeys(buildContext.Container, elementType);
                if (keyComparer != null)
                {
                    allKeys = allKeys.OrderBy(i => i, keyComparer);
                }
                
                var keys = allKeys.ToArray();
                var expressions = new Expression[keys.Length];
                for (var i = 0; i < keys.Length; i++)
                {
                    var context = buildContext.CreateChild(keys[i], buildContext.Container);
                    expressions[i] = context.CreateExpression();
                }

                expression = Expression.NewArrayInit(elementType, expressions);
                error = default(Exception);
                return true;
            }
        }
    }
}


#endregion
#region CommonTypesFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Core;
    using static Core.FluentRegister;

    /// <summary>
    /// Allows to resolve common types like a <c>Lazy</c>.
    /// </summary>
    public sealed class CommonTypesFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new CommonTypesFeature();

        private CommonTypesFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => new Lazy<TT>(() => ctx.Container.Inject<TT>(ctx.Key.Tag), true), null, AnyTag);
            yield return container.Register(ctx => ctx.Container.TryInjectValue<TTS>(ctx.Key.Tag), null, AnyTag);
            yield return container.Register(ctx => new ThreadLocal<TT>(() => ctx.Container.Inject<TT>(ctx.Key.Tag)), null, AnyTag);
        }
    }
}


#endregion
#region CoreFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Lifetimes;

    /// <summary>
    /// Adds the set of core features like lifetimes and containers.
    /// </summary>
    [PublicAPI]
    public sealed class CoreFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new CoreFeature();

        private CoreFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => FoundCyclicDependency.Shared);
            yield return container.Register(ctx => CannotBuildExpression.Shared);
            yield return container.Register(ctx => CannotGetResolver.Shared);
            yield return container.Register(ctx => CannotRegister.Shared);
            yield return container.Register(ctx => CannotResolveConstructor.Shared);
            yield return container.Register(ctx => CannotResolveDependency.Shared);
            yield return container.Register(ctx => CannotResolveType.Shared);
            yield return container.Register(ctx => CannotResolveGenericTypeArgument.Shared);

            yield return container.Register(ctx => DefaultAutowiringStrategy.Shared);

            // Lifetimes
            yield return container.Register<ILifetime>(ctx => new SingletonLifetime(true), null, new object[] { Lifetime.Singleton });
            yield return container.Register<ILifetime>(ctx => new ContainerSingletonLifetime(), null, new object[] { Lifetime.ContainerSingleton });
            yield return container.Register<ILifetime>(ctx => new ScopeSingletonLifetime(), null, new object[] { Lifetime.ScopeSingleton });

            // Scope
            yield return container.Register<IScope>(ctx => new Scope(ctx.Container.Inject<ILockObject>()));

            // Current container
            yield return container.Register<IContainer, IResourceRegistry, IObservable<ContainerEvent>>(ctx => ctx.Container);

            // New child container
            yield return container.Register<IMutableContainer>(
                ctx => new Container(
                    ctx.Args.Length == 1 ? Container.CreateContainerName(ctx.Args[0] as string) : Container.CreateContainerName(string.Empty),
                    ctx.Container,
                    ctx.Container.Inject<ILockObject>()));

            yield return container.Register<Func<IMutableContainer>>(ctx => () => ctx.Container.Inject<IMutableContainer>());
            yield return container.Register<Func<string, IMutableContainer>>(ctx => name => ctx.Container.Resolve<IMutableContainer>(name));

            yield return container.Register(ctx => ContainerEventToStringConverter.Shared);
            yield return container.Register(ctx => TypeToStringConverter.Shared);
        }
    }
}


#endregion
#region DefaultFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Adds a set of all bundled features.
    /// </summary>
    [PublicAPI]
    public class DefaultFeature: IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new DefaultFeature();

        private static readonly IEnumerable<IConfiguration> Features = new[]
        {
            CoreFeature.Set,
            CollectionFeature.Set,
            FuncFeature.Set,
            TaskFeature.Set,
            TupleFeature.Set,
            CommonTypesFeature.Set
        };

        private DefaultFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return (container ?? throw new ArgumentNullException(nameof(container))).Apply(Features);
        }
    }
}


#endregion
#region FuncFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using Lifetimes;
    using static Core.FluentRegister;

    /// <summary>
    /// Allows to resolve Functions.
    /// </summary>
    [PublicAPI]
    public sealed  class FuncFeature : IConfiguration
    {
        [NotNull] private static readonly MethodInfo ResolveWithTagGenericMethodInfo = ((MethodCallExpression)((Expression<Func<object>>)(() => Resolve<object>(default(IContainer), default(Tag), default(object[])))).Body).Method.GetGenericMethodDefinition();

        /// The default instance.
        public static readonly IConfiguration Set = new FuncFeature();

        /// The high-performance instance.
        public static readonly IConfiguration LightSet = new FuncFeature(true);

        private readonly bool _light;

        private FuncFeature(bool light = false) => _light = light;

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(new[] { typeof(Func<TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT4, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), AnyTag);

            if (_light) yield break;

            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT4, TT5, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT4, TT5, TT6, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), AnyTag);
            yield return container.Register(new[] { typeof(Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8, TT>) }, new FuncDependency(), new ContainerStateSingletonLifetime<TT>(false), AnyTag);
        }

        private class FuncDependency : IDependency
        {
            public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
            {
                var genericTypeArguments = buildContext.Key.Type.Descriptor().GetGenericTypeArguments();
                var paramsCount = genericTypeArguments.Length - 1;
                var instanceType = genericTypeArguments[paramsCount];
                var key = new Key(instanceType, buildContext.Key.Tag);
                var context = buildContext.CreateChild(key, buildContext.Container);
                var curContext = buildContext.Parent;
                var reentrancy = false;
                while (curContext != null)
                {
                    if (curContext.Key.Equals(buildContext.Key))
                    {
                        reentrancy = true;
                        break;
                    }

                    curContext = curContext.Parent;
                }

                Expression instanceExpression;
                if (!reentrancy)
                {
                    instanceExpression = context.CreateExpression();
                }
                else
                {
                    instanceExpression = Expression.Call(
                        null,
                        ResolveWithTagGenericMethodInfo.MakeGenericMethod(instanceType),
                        context.ContainerParameter,
                        Expression.Constant(context.Key.Tag),
                        context.ArgsParameter);
                }

                var parameters = new ParameterExpression[paramsCount];
                var parametersArgs = new Expression[paramsCount];
                for (var i = 0; i < paramsCount; i++)
                {
                    var parameterExpression = Expression.Parameter(genericTypeArguments[i]);
                    parameters[i] = parameterExpression;
                    parametersArgs[i] = parameterExpression.Convert(typeof(object));
                }

                Expression argsExpression;
                if (parameters.Length == 0)
                {
                    argsExpression = Expression.Constant(FluentNativeResolve.EmptyArgs);
                }
                else
                {
                    argsExpression = Expression.NewArrayInit(typeof(object), parametersArgs);
                }

                instanceExpression = Expression.Block(
                    new[] { context.ContainerParameter, context.ArgsParameter },
                    Expression.Assign(context.ContainerParameter, Expression.Constant(context.Container)),
                    Expression.Assign(context.ArgsParameter, argsExpression),
                    instanceExpression);

                if (context.TryCompile(Expression.Lambda(instanceExpression, parameters), out var factory, out error))
                {
                    expression = Expression.Constant(factory);
                    return true;
                }

                expression = default(Expression);
                return false;
            }
        }

        private static T Resolve<T>([NotNull] IContainer container, object tag, [NotNull][ItemCanBeNull] params object[] args)
            => container.GetResolver<T>(new Tag(tag))(container, args);
    }
}


#endregion
#region LightFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Adds a set of all bundled features.
    /// </summary>
    [PublicAPI]
    public class LightFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new LightFeature();

        private static readonly IEnumerable<IConfiguration> Features = new[]
        {
            CoreFeature.Set,
            CollectionFeature.Set,
            FuncFeature.LightSet,
            TaskFeature.Set,
            TupleFeature.LightSet,
            CommonTypesFeature.Set
        };

        private LightFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return (container ?? throw new ArgumentNullException(nameof(container))).Apply(Features);
        }
    }
}


#endregion
#region ResolveUnboundFeature

namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Core;
    using Dependencies;
    using Issues;

    /// <summary>
    /// Allows to resolve unbound dependencies.
    /// </summary>
    public class ResolveUnboundFeature:
        IConfiguration,
        IDisposable,
        IEnumerable<Key>,
        ICannotGetResolver,
        ICannotResolveDependency,
        IDependency
    {
        private readonly bool _supportDefaults;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        private readonly IList<IDisposable> _tokens = new List<IDisposable>();
        private readonly IList<Key> _registeredKeys = new List<Key>();

        /// <summary>
        /// Creates an instance of feature.
        /// </summary>
        public ResolveUnboundFeature()
            : this(true)
        {
        }

        /// <summary>
        /// Creates an instance of feature.
        /// </summary>
        /// <param name="supportDefaults"><c>True</c> to resolve default(T) for unresolved value types.</param>
        /// <param name="autowiringStrategy">The autowiring strategy.</param>
        public ResolveUnboundFeature(bool supportDefaults, [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            _supportDefaults = supportDefaults;
            _autowiringStrategy = autowiringStrategy;
        }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return container.Bind<ICannotResolveDependency>().Bind<ICannotGetResolver>().To(ctx => this);
        }

        Resolver<T> ICannotGetResolver.Resolve<T>(IContainer container, Key key, Exception error)
        {
            if (IsValidType(key.Type) && container is IMutableContainer mutableContainer && mutableContainer.TryRegisterDependency(new[] {key}, this, null, out var token))
            {
                _registeredKeys.Add(key);
                _tokens.Add(token);
                return container.GetResolver<T>(key.Type, key.Tag.AsTag());
            }

            return (container.Parent ?? throw new InvalidOperationException("Parent container should not be null.")).Resolve<ICannotGetResolver>().Resolve<T>(container, key, error);
        }

        DependencyDescription ICannotResolveDependency.Resolve(IBuildContext buildContext)
        {
            if (IsValidType(buildContext.Key.Type))
            {
                return new DependencyDescription(this, null);
            }

            return (buildContext.Container.Parent ?? throw new InvalidOperationException("Parent container should not be null.")).Resolve<ICannotResolveDependency>().Resolve(buildContext);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (var token in _tokens)
            {
                token.Dispose();
            }
        }

        /// <inheritdoc />
        public IEnumerator<Key> GetEnumerator() => _registeredKeys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool IDependency.TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            var type = buildContext.Key.Type;
            if (_supportDefaults)
            {
                if (type.Descriptor().IsValueType())
                {
                    expression = Expression.Default(type);
                    error = default(Exception);
                    return true;
                }
            }

            return
                new AutowiringDependency(type, _autowiringStrategy)
                .TryBuildExpression(buildContext, lifetime, out expression, out error);
        }

        private static bool IsValidType(Type type)
        {
            var typeDescriptor = type.Descriptor();
            return !typeDescriptor.IsAbstract() && !typeDescriptor.IsInterface();
        }
    }
}


#endregion
#region TaskFeature

namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using static Core.FluentRegister;

    /// <summary>
    /// Allows to resolve Tasks.
    /// </summary>
    [PublicAPI]
    public sealed  class TaskFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new TaskFeature();

        /// <summary>
        /// TaskFeature default tag
        /// </summary>
        public static readonly Tag Tag = new Tag(Guid.NewGuid());

        private TaskFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => TaskScheduler.Current);
            yield return container.Register(ctx => CreateTask(ctx.Container.Inject<Func<TT>>(ctx.Key.Tag), ctx.Container.Inject<TaskScheduler>()), null, AnyTag);
#if !NET40 && !NET403 && !NET45 && !NET45 && !NET451 && !NET452 && !NET46 && !NET461 && !NET462 && !NET47 && !NET48 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2&& !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !WINDOWS_UWP
            yield return container.Register(ctx => new ValueTask<TT>(ctx.Container.Inject<TT>(ctx.Key.Tag)), null, AnyTag);
#endif
        }

        private static Task<T> CreateTask<T>(Func<T> factory, TaskScheduler taskScheduler)
        {
            var task = new Task<T>(factory);
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
    using static Core.FluentRegister;

    /// <summary>
    /// Allows to resolve Tuples.
    /// </summary>
    [PublicAPI]
    public sealed  class TupleFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new TupleFeature();
        /// The high-performance instance.
        public static readonly IConfiguration LightSet = new TupleFeature(true);

        private readonly bool _light;

        private TupleFeature(bool light = false) => _light = light;

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => new Tuple<TT>(ctx.Container.Inject<TT>(ctx.Key.Tag)), null, AnyTag);

            yield return container.Register(ctx => new Tuple<TT1, TT2>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag)), null, AnyTag);

            yield return container.Register(ctx => new Tuple<TT1, TT2, TT3>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag)), null, AnyTag);

            yield return container.Register(ctx => new Tuple<TT1, TT2, TT3, TT4>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag)), null, AnyTag);

            if (!_light)
            {
                yield return container.Register(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5>(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag)), null, AnyTag);

                yield return container.Register(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5, TT6>(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag)), null, AnyTag);

                yield return container.Register(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5, TT6, TT7>(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag),
                    ctx.Container.Inject<TT7>(ctx.Key.Tag)), null, AnyTag);

                yield return container.Register(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8>(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag),
                    ctx.Container.Inject<TT7>(ctx.Key.Tag),
                    ctx.Container.Inject<TT8>(ctx.Key.Tag)), null, AnyTag);
            }

#if !NET40 && !NET403 && !NET45 && !NET45 && !NET451 && !NET452 && !NET46 && !NET461 && !NET462 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2&& !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !WINDOWS_UWP
            yield return container.Register(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag)), null, AnyTag);

            yield return container.Register(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag)), null, AnyTag);

            yield return container.Register(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag)), null, AnyTag);

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
                    ctx.Container.Inject<TT6>(ctx.Key.Tag)), null, AnyTag);

                yield return container.Register(ctx => CreateTuple(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag),
                    ctx.Container.Inject<TT7>(ctx.Key.Tag)), null, AnyTag);

                yield return container.Register(ctx => CreateTuple(
                    ctx.Container.Inject<TT1>(ctx.Key.Tag),
                    ctx.Container.Inject<TT2>(ctx.Key.Tag),
                    ctx.Container.Inject<TT3>(ctx.Key.Tag),
                    ctx.Container.Inject<TT4>(ctx.Key.Tag),
                    ctx.Container.Inject<TT5>(ctx.Key.Tag),
                    ctx.Container.Inject<TT6>(ctx.Key.Tag),
                    ctx.Container.Inject<TT7>(ctx.Key.Tag),
                    ctx.Container.Inject<TT8>(ctx.Key.Tag)), null, AnyTag);
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
    /// For a singleton instance per container.
    /// </summary>
    [PublicAPI]
    public sealed class ContainerSingletonLifetime: KeyBasedLifetime<IContainer, object>
    {
        /// <inheritdoc />
        protected override IContainer CreateKey(IContainer container, object[] args) => container;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ContainerSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ContainerSingletonLifetime();

        /// <inheritdoc />
        protected override object OnNewInstanceCreated(object newInstance, IContainer targetContainer, IContainer container, object[] args)
        {
            if (newInstance is IDisposable disposable)
            {
                targetContainer.RegisterResource(disposable);
            }

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
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

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
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
#region ContainerStateSingletonLifetime

namespace IoC.Lifetimes
{
    using System;
    using Core; // ReSharper disable once RedundantUsingDirective

    /// <summary>
    /// For a singleton instance per state.
    /// </summary>
    internal sealed class ContainerStateSingletonLifetime<TValue> : KeyBasedLifetime<IContainer, TValue>
        where TValue : class
    {
        private readonly bool _isDisposable;
        private IDisposable _containerSubscription = Disposable.Empty;

        public ContainerStateSingletonLifetime(bool isDisposable)
        {
            _isDisposable = isDisposable;
        }

        /// <inheritdoc />
        protected override IContainer CreateKey(IContainer container, object[] args) => container;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ContainerSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ContainerStateSingletonLifetime<TValue>(_isDisposable);

        /// <inheritdoc />
        protected override TValue OnNewInstanceCreated(TValue newInstance, IContainer targetContainer, IContainer container, object[] args)
        {
            _containerSubscription = container.Subscribe(
                value =>
                {
                    if (value.IsSuccess && (value.EventType == EventType.RegisterDependency || value.EventType == EventType.ContainerStateSingletonLifetime))
                    {
                        var curContainer = targetContainer;
                        while (curContainer != null && !Remove(container))
                        {
                            curContainer = curContainer.Parent;
                        }
                    }
                },
                e => { },
                () => { });

            if (_isDisposable && newInstance is IDisposable disposable)
            {
                targetContainer.RegisterResource(disposable);
            }

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnInstanceReleased(TValue releasedInstance, IContainer targetContainer)
        {
            _containerSubscription.Dispose();
            if (_isDisposable && releasedInstance is IDisposable disposable)
            {
                targetContainer.UnregisterResource(disposable);
                disposable.Dispose();
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            _containerSubscription.Dispose();
            base.Dispose();
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

    /// <summary>
    /// Represents the abstraction for singleton based lifetimes.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    [PublicAPI]
    public abstract class KeyBasedLifetime<TKey, TValue> : ILifetime
        where TValue: class
    {
        private readonly bool _supportOnNewInstanceCreated;
        private readonly bool _supportOnInstanceReleased;
        private static readonly FieldInfo InstancesFieldInfo = Descriptor<KeyBasedLifetime<TKey, TValue>>().GetDeclaredFields().Single(i => i.Name == nameof(_instances));
        private static readonly MethodInfo CreateKeyMethodInfo = Descriptor<KeyBasedLifetime<TKey, TValue>>().GetDeclaredMethods().Single(i => i.Name == nameof(CreateKey));
        private static readonly MethodInfo GetMethodInfo = Descriptor<Table<TKey, object>>().GetDeclaredMethods().Single(i => i.Name == nameof(Table<TKey, TValue>.Get));
        private static readonly MethodInfo SetMethodInfo = Descriptor<Table<TKey, object>>().GetDeclaredMethods().Single(i => i.Name == nameof(Table<TKey, TValue>.Set));
        private static readonly MethodInfo OnNewInstanceCreatedMethodInfo = Descriptor<KeyBasedLifetime<TKey, TValue>>().GetDeclaredMethods().Single(i => i.Name == nameof(OnNewInstanceCreated));
        private static readonly ParameterExpression KeyVar = Expression.Variable(typeof(TKey), "key");

        [CanBeNull] private readonly object _lockObject;
        private volatile Table<TKey, TValue> _instances = Table<TKey, TValue>.Empty;

        /// <summary>
        /// Creates an instance
        /// </summary>
        /// <param name="supportOnNewInstanceCreated">True to invoke OnNewInstanceCreated</param>
        /// <param name="supportOnInstanceReleased">True to invoke OnInstanceReleased</param>
        /// <param name="threadSafe"><c>True</c> to synchronize operations.</param>
        protected KeyBasedLifetime(
            bool supportOnNewInstanceCreated = true,
            bool supportOnInstanceReleased = true,
            bool threadSafe = true)
        {
            _supportOnNewInstanceCreated = supportOnNewInstanceCreated;
            _supportOnInstanceReleased = supportOnInstanceReleased;
            if (threadSafe)
            {
                _lockObject = new LockObject();
            }
        }

        /// <inheritdoc />
        public Expression Build(IBuildContext context, Expression bodyExpression)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (context == null) throw new ArgumentNullException(nameof(context));
            var returnType = context.Key.Type;
            var thisConst = Expression.Constant(this);
            var instanceVar = Expression.Variable(typeof(TValue));
            var instancesField = Expression.Field(thisConst, InstancesFieldInfo);
            var getExpression = Expression.Assign(instanceVar, Expression.Call(instancesField, GetMethodInfo, KeyVar));
            Expression isEmptyExpression;
            if (returnType.Descriptor().IsValueType())
            {
                isEmptyExpression = Expression.Call(null, ExpressionBuilderExtensions.EqualsMethodInfo, instanceVar.Convert(typeof(TValue)), Expression.Default(typeof(TValue)));
            }
            else
            {
                isEmptyExpression = Expression.ReferenceEqual(instanceVar, Expression.Default(returnType));
            }

            var initNewInstanceExpression = _supportOnNewInstanceCreated 
                ? Expression.Call(thisConst, OnNewInstanceCreatedMethodInfo, instanceVar.Convert(typeof(TValue)), KeyVar, context.ContainerParameter, context.ArgsParameter).Convert(returnType)
                : instanceVar;

            Expression createExpression = Expression.Block(
                // instance = _instances.Get(hashCode, key);
                getExpression,
                // if (instance == default(TValue))
                Expression.Condition(
                    isEmptyExpression,
                    Expression.Block(
                        // instance = new T();
                        Expression.Assign(instanceVar, bodyExpression.Convert(typeof(TValue))),
                        // Instances = _instances.Set(hashCode, key, instance);
                        Expression.Assign(instancesField, Expression.Call(instancesField, SetMethodInfo, KeyVar, instanceVar.Convert(typeof(object)))),
                        // instance or OnNewInstanceCreated(instance, key, container, args);
                        initNewInstanceExpression,
                        instanceVar), 
                    instanceVar));

            if (_lockObject != null)
            {
                // double check
                createExpression = Expression.Block(
                    // instance = _instances.Get(hashCode, key);
                    getExpression,
                    Expression.Condition(isEmptyExpression, createExpression.Lock(Expression.Constant(_lockObject)), instanceVar)
                );
            }

            return Expression.Block(
                // Key key;
                // int hashCode;
                // T instance;
                new[] { KeyVar, instanceVar },
                // TKey key = CreateKey(container, args);
                Expression.Assign(KeyVar, Expression.Call(thisConst, CreateKeyMethodInfo, context.ContainerParameter, context.ArgsParameter)),
                createExpression.Convert(returnType)
            // }
            );
        }

        /// <inheritdoc />
        public IContainer SelectResolvingContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        /// <inheritdoc />
        public virtual void Dispose()
        {
            Table<TKey, TValue> instances;
            if (_lockObject != null)
            {
                lock (_lockObject)
                {
                    instances = _instances;
                    _instances = Table<TKey, TValue>.Empty;
                }
            }
            else
            {
                instances = _instances;
                _instances = Table<TKey, TValue>.Empty;
            }

            if (_supportOnInstanceReleased)
            {
                foreach (var instance in instances)
                {
                    OnInstanceReleased(instance.Value, instance.Key);
                }
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
        protected abstract TValue OnNewInstanceCreated(TValue newInstance, TKey key, IContainer container, object[] args);

        /// <summary>
        /// Is invoked on the instance was released.
        /// </summary>
        /// <param name="releasedInstance">The released instance.</param>
        /// <param name="key">The instance key.</param>
        protected abstract void OnInstanceReleased(TValue releasedInstance, TKey key);

        /// <summary>
        /// Forcibly remove an instance.
        /// </summary>
        /// <param name="key">The instance key.</param>
        protected bool Remove(TKey key)
        {
            bool removed;
            if (_lockObject != null)
            {
                lock (_lockObject)
                {
                    _instances = _instances.Remove(key, out removed);
                }
            }
            else
            {
                _instances = _instances.Remove(key, out removed);
            }

            return removed;
        }
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
    /// For a singleton instance per scope.
    /// </summary>
    [PublicAPI]
    public sealed class ScopeSingletonLifetime: KeyBasedLifetime<IScope, object>
    {
        /// <inheritdoc />
        protected override IScope CreateKey(IContainer container, object[] args) => Scope.Current;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ScopeSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ScopeSingletonLifetime();

        /// <inheritdoc />
        protected override object OnNewInstanceCreated(object newInstance, IScope scope, IContainer container, object[] args)
        {
            if (!(scope is IResourceRegistry resourceRegistry))
            {
                return newInstance;
            }

            if (newInstance is IDisposable disposable)
            {
                resourceRegistry.RegisterResource(disposable);
            }

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
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

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
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
    /// For a singleton instance.
    /// </summary>
    [PublicAPI]
    public sealed class SingletonLifetime : ILifetime
    {
        private static readonly FieldInfo InstanceFieldInfo = Descriptor<SingletonLifetime>().GetDeclaredFields().Single(i => i.Name == nameof(_instance));

        [CanBeNull] private readonly object _lockObject;
#pragma warning disable CS0649, IDE0044
        private volatile object _instance;
#pragma warning restore CS0649, IDE0044

        /// <summary>
        /// Creates an instance of lifetime.
        /// </summary>
        /// <param name="threadSafe"><c>True</c> to synchronize operations.</param>
        public SingletonLifetime(bool threadSafe = true)
        {
            if (threadSafe)
            {
                _lockObject = new LockObject();
            }
        }

        /// <inheritdoc />
        public Expression Build(IBuildContext context, Expression expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (context == null) throw new ArgumentNullException(nameof(context));

            var thisConst = Expression.Constant(this);
            var instanceField = Expression.Field(thisConst, InstanceFieldInfo);
            var typedInstance = instanceField.Convert(expression.Type);
            var isNullExpression = Expression.ReferenceEqual(instanceField, ExpressionBuilderExtensions.NullConst);

            Expression createExpression = Expression.IfThen(
                // if (this._instance == null)
                isNullExpression,
                // this._instance = new T();
                Expression.Assign(instanceField, expression));

            if (_lockObject != null)
            {
                // double check
                createExpression = Expression.IfThen(isNullExpression, createExpression.Lock(Expression.Constant(_lockObject)));
            }

            return Expression.Block(
                createExpression,
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
            if (_lockObject != null)
            {
                lock (_lockObject)
                {
                    disposable = _instance as IDisposable;
                }
            }
            else
            {
                disposable = _instance as IDisposable;
            }

            disposable?.Dispose();

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            IAsyncDisposable asyncDisposable;
            if (_lockObject != null)
            {
                lock (_lockObject)
                {
                    asyncDisposable = _instance as IAsyncDisposable;
                }
            }
            else
            {
                asyncDisposable = _instance as IAsyncDisposable;
            }

            asyncDisposable?.ToDisposable().Dispose();
#endif
        }

        /// <inheritdoc />
        public ILifetime Create() => new SingletonLifetime(_lockObject != null);

        /// <inheritdoc />
        public override string ToString() => Lifetime.Singleton.ToString();
    }
}


#endregion

#endregion

#region Dependencies

#region AutowiringDependency

namespace IoC.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using Issues;

    /// <summary>
    /// Represents the autowiring dependency.
    /// </summary>
    public sealed class AutowiringDependency : IDependency
    {
        [NotNull] private readonly Type _implementationType;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        private readonly bool _hasGenericParamsWithConstraints;
        private readonly List<GenericParamsWithConstraints> _genericParamsWithConstraints;
        private readonly Type[] _genericTypeParameters;
        private readonly TypeDescriptor _typeDescriptor;
        [NotNull] [ItemNotNull] private readonly LambdaExpression[] _initializeInstanceExpressions;
        private readonly IDictionary<Type, Type> _typesMap = new Dictionary<Type, Type>();

        /// <summary>
        /// Creates an instance of dependency.
        /// </summary>
        /// <param name="implementationType">The autowiring implementation type.</param>
        /// <param name="initializeInstanceLambdaStatements">The statements to initialize an instance.</param>
        public AutowiringDependency(
            [NotNull] Type implementationType,
            [NotNull] [ItemNotNull] params LambdaExpression[] initializeInstanceLambdaStatements)
            :this(implementationType, null, initializeInstanceLambdaStatements)
        {
        }

        /// <summary>
        /// Creates an instance of dependency.
        /// </summary>
        /// <param name="implementationType">The autowiring implementation type.</param>
        /// <param name="autowiringStrategy">The autowiring strategy.</param>
        /// <param name="initializeInstanceLambdaStatements">The statements to initialize an instance.</param>
        public AutowiringDependency(
            [NotNull] Type implementationType,
            [CanBeNull] IAutowiringStrategy autowiringStrategy = null,
            [NotNull][ItemNotNull] params LambdaExpression[] initializeInstanceLambdaStatements)
        {
            _implementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
            _autowiringStrategy = autowiringStrategy;
            _initializeInstanceExpressions = initializeInstanceLambdaStatements ?? throw new ArgumentNullException(nameof(initializeInstanceLambdaStatements));
            _typeDescriptor = implementationType.Descriptor();

            if (_typeDescriptor.IsInterface())
            {
                throw new ArgumentException($"Type \"{implementationType}\" should not be an interface.", nameof(implementationType));
            }

            if (_typeDescriptor.IsAbstract())
            {
                throw new ArgumentException($"Type \"{implementationType}\" should not be an abstract class.", nameof(implementationType));
            }

            if (!_typeDescriptor.IsGenericTypeDefinition())
            {
                return;
            }

            _genericTypeParameters = _typeDescriptor.GetGenericTypeParameters();
            if (_genericTypeParameters.Length > GenericTypeArguments.Arguments.Length)
            {
                throw new ArgumentException($"Too many generic type parameters in the type \"{implementationType}\".", nameof(implementationType));
            }

            _genericParamsWithConstraints = new List<GenericParamsWithConstraints>(_genericTypeParameters.Length);
            var genericTypePos = 0;
            for (var position = 0; position < _genericTypeParameters.Length; position++)
            {
                var genericType = _genericTypeParameters[position];
                if (!genericType.IsGenericParameter)
                {
                    continue;
                }

                var descriptor = genericType.Descriptor();
                if (descriptor.GetGenericParameterAttributes() == GenericParameterAttributes.None && !descriptor.GetGenericParameterConstraints().Any())
                {
                    if (!_typesMap.TryGetValue(genericType, out var curType))
                    {
                        try
                        {
                            curType = GenericTypeArguments.Arguments[genericTypePos++];
                            _typesMap[genericType] = curType;
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            throw new BuildExpressionException("Too many generic arguments.", ex);
                        }
                    }

                    _genericTypeParameters[position] = curType;
                }
                else
                {
                    _genericParamsWithConstraints.Add(new GenericParamsWithConstraints(descriptor, position));
                }
            }

            if (_genericParamsWithConstraints.Count == 0)
            {
                _implementationType = _typeDescriptor.MakeGenericType(_genericTypeParameters);
            }
            else
            {
                _hasGenericParamsWithConstraints = true;
            }
        }

        /// <inheritdoc />
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));

            var typesMap = new Dictionary<Type, Type>(_typesMap);
            NewExpression newExpression;
            try
            {
                var autoWiringStrategy = _autowiringStrategy ?? buildContext.AutowiringStrategy;
                var instanceType = ResolveInstanceType(buildContext, autoWiringStrategy);
                var typeDescriptor = CreateTypeDescriptor(buildContext, instanceType, typesMap);
                var ctor = SelectConstructor(buildContext, typeDescriptor, autoWiringStrategy);
                newExpression = Expression.New(ctor.Info, ctor.GetParametersExpressions(buildContext));
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                expression = default(Expression);
                return false;
            }

            return
                new BaseDependency(
                    newExpression,
                    _initializeInstanceExpressions.Select(i => i.Body),
                    typesMap,
                    _autowiringStrategy)
                .TryBuildExpression(buildContext, lifetime, out expression, out error);
        }

        private Type ResolveInstanceType(IBuildContext buildContext, IAutowiringStrategy autoWiringStrategy)
        {
            if (autoWiringStrategy.TryResolveType(buildContext.Container, _implementationType, buildContext.Key.Type, out var instanceType))
            {
                return instanceType;
            }

            if (_hasGenericParamsWithConstraints)
            {
                return GetInstanceTypeBasedOnTargetGenericConstrains(buildContext.Key.Type) ?? buildContext.Container.Resolve<ICannotResolveType>().Resolve(buildContext, _implementationType, buildContext.Key.Type);
            }

            return _implementationType;
        }

        [SuppressMessage("ReSharper", "PossibleMultipleE%numeration")]
        private static IMethod<ConstructorInfo> SelectConstructor(IBuildContext buildContext, TypeDescriptor typeDescriptor, IAutowiringStrategy autoWiringStrategy)
        {
            var constructors = (IEnumerable<IMethod<ConstructorInfo>>)typeDescriptor
                .GetDeclaredConstructors()
                .Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic))
                .Select(info => new Method<ConstructorInfo>(info));

            if (autoWiringStrategy.TryResolveConstructor(buildContext.Container, constructors, out var ctor))
            {
                return ctor;
            }

            if (DefaultAutowiringStrategy.Shared != autoWiringStrategy && DefaultAutowiringStrategy.Shared.TryResolveConstructor(buildContext.Container, constructors, out ctor))
            {
                return ctor;
            }

            return buildContext.Container.Resolve<ICannotResolveConstructor>().Resolve(buildContext, constructors);
        }

        private TypeDescriptor CreateTypeDescriptor(IBuildContext buildContext, Type type, Dictionary<Type, Type> typesMap)
        {
            var typeDescriptor = type.Descriptor();
            if (!typeDescriptor.IsConstructedGenericType())
            {
                return typeDescriptor;
            }

            TypeMapper.Shared.Map(type, buildContext.Key.Type, typesMap);
            foreach (var mapping in typesMap)
            {
                buildContext.MapType(mapping.Key, mapping.Value);
            }

            var genericTypeArgs = typeDescriptor.GetGenericTypeArguments();
            var isReplaced = false;
            for (var position = 0; position < genericTypeArgs.Length; position++)
            {
                var genericTypeArg = genericTypeArgs[position];
                var genericTypeArgDescriptor = genericTypeArg.Descriptor();
                if (genericTypeArgDescriptor.IsGenericTypeDefinition() || genericTypeArgDescriptor.IsGenericTypeArgument())
                {
                    if (typesMap.TryGetValue(genericTypeArg, out var genericArgType))
                    {
                        genericTypeArgs[position] = genericArgType;
                        isReplaced = true;
                    }
                    else
                    {
                        genericTypeArgs[position] = buildContext.Container.Resolve<ICannotResolveGenericTypeArgument>().Resolve(buildContext, _typeDescriptor.Type, position, genericTypeArg);
                        isReplaced = true;
                    }
                }
            }

            if (isReplaced)
            {
                typeDescriptor = typeDescriptor.GetGenericTypeDefinition().MakeGenericType(genericTypeArgs).Descriptor();
            }

            return typeDescriptor;
        }

        [CanBeNull]
        internal Type GetInstanceTypeBasedOnTargetGenericConstrains(Type type)
        {
            var registeredGenericTypeParameters = new Type[_genericTypeParameters.Length];
            Array.Copy(_genericTypeParameters, registeredGenericTypeParameters, _genericTypeParameters.Length);
            var typeDescriptor = type.Descriptor();
            var typeDefinitionDescriptor = typeDescriptor.GetGenericTypeDefinition().Descriptor();
            var typeDefinitionGenericTypeParameters = typeDefinitionDescriptor.GetGenericTypeParameters();
            var constraintsMap = typeDescriptor
                .GetGenericTypeArguments()
                .Zip(typeDefinitionGenericTypeParameters, (genericType, typeDefinition) => Tuple.Create(genericType, typeDefinition.Descriptor().GetGenericParameterConstraints()))
                .ToArray();

            var canBeResolved = true;
            foreach (var item in _genericParamsWithConstraints)
            {
                var constraints = item.TypeDescriptor.GetGenericParameterConstraints();
                var isDefined = false;
                foreach (var constraint in constraintsMap)
                {
                    if (!CoreExtensions.SequenceEqual(constraints, constraint.Item2))
                    {
                        continue;
                    }

                    registeredGenericTypeParameters[item.Position] = constraint.Item1;
                    isDefined = true;
                    break;
                }

                if (!isDefined)
                {
                    canBeResolved = false;
                    break;
                }
            }

            return canBeResolved ? _typeDescriptor.MakeGenericType(registeredGenericTypeParameters) : null;
        }

        /// <inheritdoc />
        public override string ToString() => $"new {_implementationType.Descriptor()}(...)";

        private struct GenericParamsWithConstraints
        {
            public readonly TypeDescriptor TypeDescriptor;
            public readonly int Position;

            public GenericParamsWithConstraints(TypeDescriptor typeDescriptor, int position)
            {
                TypeDescriptor = typeDescriptor;
                Position = position;
            }
        }
    }
}


#endregion
#region BaseDependency

namespace IoC.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;

    /// <summary>
    /// Represents the dependency based on expressions and a map of types.
    /// </summary>
    internal sealed class BaseDependency : IDependency
    {
        [NotNull] private readonly IDictionary<Type, Type> _typesMap;
        private readonly Expression _createInstanceExpression;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        [NotNull] [ItemNotNull] private readonly IEnumerable<Expression> _initializeInstanceExpressions;

        /// <summary>
        /// Creates an instance of dependency.
        /// </summary>
        /// <param name="instanceExpression">The expression to create an instance.</param>
        /// <param name="initializeInstanceExpressions">The statements to initialize an instance.</param>
        /// <param name="typesMap">The type mapping dictionary.</param>
        /// <param name="autowiringStrategy">The autowiring strategy.</param>
        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public BaseDependency(
            [NotNull] Expression instanceExpression,
            [NotNull] [ItemNotNull] IEnumerable<Expression> initializeInstanceExpressions,
            [NotNull] IDictionary<Type, Type> typesMap,
            [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            _createInstanceExpression = instanceExpression ?? throw new ArgumentNullException(nameof(instanceExpression));
            _initializeInstanceExpressions = initializeInstanceExpressions ?? throw new ArgumentNullException(nameof(initializeInstanceExpressions));
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));
            _autowiringStrategy = autowiringStrategy;
        }

        /// <inheritdoc />
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                var autoWiringStrategy = _autowiringStrategy ?? buildContext.AutowiringStrategy;
                var typeDescriptor = _createInstanceExpression.Type.Descriptor();
                expression = ReplaceTypes(buildContext, _createInstanceExpression, _typesMap);
                var thisVar = Expression.Variable(expression.Type);

                var initializeExpressions =
                    GetInitializers(buildContext.Container, autoWiringStrategy, typeDescriptor)
                        .Select(initializer => (Expression)Expression.Call(thisVar, initializer.Info, initializer.GetParametersExpressions(buildContext)))
                        .Concat(_initializeInstanceExpressions.Select(statementExpression => ReplaceTypes(buildContext, statementExpression, _typesMap)))
                        .ToArray();

                if (initializeExpressions.Length > 0)
                {
                    expression = Expression.Block(
                        new[] { thisVar },
                        Expression.Assign(thisVar, expression),
                        Expression.Block(initializeExpressions),
                        thisVar
                    );
                }

                var visitor = new DependencyInjectionExpressionVisitor(buildContext, thisVar);
                expression = visitor.Visit(expression) ?? expression;
                expression = buildContext.FinalizeExpression(expression, lifetime);
                error = default(Exception);
                return true;
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                expression = default(Expression);
                return false;
            }
        }

        /// <inheritdoc />
        public override string ToString() => $"{_createInstanceExpression}";

        private static Expression ReplaceTypes(IBuildContext buildContext, Expression expression, IDictionary<Type, Type> typesMap)
        {
            typesMap = typesMap ?? new Dictionary<Type, Type>();
            if (expression.Type == buildContext.Key.Type)
            {
                return expression;
            }

            TypeMapper.Shared.Map(expression.Type, buildContext.Key.Type, typesMap);
            if (typesMap.Count == 0)
            {
                return expression;
            }

            var visitor = new TypeReplacerExpressionVisitor(typesMap);
            return visitor.Visit(expression) ?? expression;
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private static IEnumerable<IMethod<MethodInfo>> GetInitializers(IContainer container, IAutowiringStrategy autoWiringStrategy, TypeDescriptor typeDescriptor)
        {
            var methods = typeDescriptor.GetDeclaredMethods().Select(info => new Method<MethodInfo>(info));
            if (autoWiringStrategy.TryResolveInitializers(container, methods, out var initializers))
            {
                return initializers;
            }

            if (DefaultAutowiringStrategy.Shared == autoWiringStrategy || !DefaultAutowiringStrategy.Shared.TryResolveInitializers(container, methods, out initializers))
            {
                initializers = Enumerable.Empty<IMethod<MethodInfo>>();
            }

            return initializers;
        }
    }
}


#endregion
#region ExpressionDependency

namespace IoC.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;

    /// <summary>
    /// Represents the dependency based on expressions.
    /// </summary>
    public sealed class ExpressionDependency : IDependency
    {
        private readonly LambdaExpression _instanceExpression;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        [NotNull] [ItemNotNull] private readonly LambdaExpression[] _initializeInstanceExpressions;

        /// <summary>
        /// Creates an instance of dependency.
        /// </summary>
        /// <param name="instanceExpression">The expression to create an instance.</param>
        /// <param name="initializeInstanceExpressions">The statements to initialize an instance.</param>
        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public ExpressionDependency(
            [NotNull] LambdaExpression instanceExpression,
            [NotNull][ItemNotNull] params LambdaExpression[] initializeInstanceExpressions)
            :this(instanceExpression, null, initializeInstanceExpressions)
        {
        }

        /// <summary>
        /// Creates an instance of dependency.
        /// </summary>
        /// <param name="instanceExpression">The expression to create an instance.</param>
        /// <param name="autowiringStrategy">The autowiring strategy.</param>
        /// <param name="initializeInstanceExpressions">The statements to initialize an instance.</param>
        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public ExpressionDependency(
            [NotNull] LambdaExpression instanceExpression,
            [CanBeNull] IAutowiringStrategy autowiringStrategy = default(IAutowiringStrategy),
            [NotNull][ItemNotNull] params LambdaExpression[] initializeInstanceExpressions)
        {
            _instanceExpression = instanceExpression ?? throw new ArgumentNullException(nameof(instanceExpression));
            _autowiringStrategy = autowiringStrategy;
            _initializeInstanceExpressions = initializeInstanceExpressions ?? throw new ArgumentNullException(nameof(initializeInstanceExpressions));
        }

        /// <inheritdoc />
        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var typesMap = new Dictionary<Type, Type>();
            try
            {
                if (_instanceExpression.Type.Descriptor().IsGenericTypeDefinition())
                {
                    TypeMapper.Shared.Map(_instanceExpression.Type, buildContext.Key.Type, typesMap);
                    foreach (var mapping in typesMap)
                    {
                        buildContext.MapType(mapping.Key, mapping.Value);
                    }
                }
            }
            catch (BuildExpressionException ex)
            {
                error = ex;
                expression = default(Expression);
                return false;
            }

            return 
                new BaseDependency(
                    _instanceExpression.Body,
                    _initializeInstanceExpressions.Select(i => i.Body),
                    typesMap,
                    _autowiringStrategy)
                .TryBuildExpression(buildContext, lifetime, out expression, out error);
        }

        /// <inheritdoc />
        public override string ToString() => $"{_instanceExpression}";
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
        /// Creates a new instance.
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
        [NotNull] Expression Resolve([NotNull] IBuildContext buildContext, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, Exception error);
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
    using System.Collections.Generic;

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
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <returns>The dependency token.</returns>
        [NotNull] IToken Resolve([NotNull] IContainer container, [NotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime);
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
        /// <param name="buildContext">The build context.</param>
        /// <param name="constructors">Available constructors.</param>
        /// <returns>The constructor.</returns>
        [NotNull] IMethod<ConstructorInfo> Resolve(IBuildContext buildContext, [NotNull] IEnumerable<IMethod<ConstructorInfo>> constructors);
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
        /// <param name="buildContext">The build context.</param>
        /// <returns>The pair of the dependency and of the lifetime.</returns>
        DependencyDescription Resolve([NotNull] IBuildContext buildContext);
    }
}


#endregion
#region ICannotResolveGenericTypeArgument

namespace IoC.Issues
{
    using System;

    /// <summary>
    /// Resolves the scenario when cannot resolve the generic type argument of an instance type.
    /// </summary>
    [PublicAPI]

    public interface ICannotResolveGenericTypeArgument
    {
        /// <summary>
        /// Resolves the generic type argument of an instance type.
        /// </summary>
        /// <param name="buildContext">The build context.</param>
        /// <param name="type">Registered type.</param>
        /// <param name="genericTypeArgPosition">The generic type argument position in the registered type.</param>
        /// <param name="genericTypeArg">The generic type argument in the registered type.</param>
        /// <returns>The resoled generic type argument.</returns>
        [NotNull] Type Resolve([NotNull] IBuildContext buildContext, [NotNull] Type type, int genericTypeArgPosition, [NotNull] Type genericTypeArg);
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
        /// <param name="buildContext">The build context.</param>
        /// <param name="registeredType">Registered type.</param>
        /// <param name="resolvingType">Resolving type.</param>
        /// <returns>The type to create an instance.</returns>
        Type Resolve(IBuildContext buildContext, [NotNull] Type registeredType, [NotNull] Type resolvingType);
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
        /// <param name="buildContext">The build context.</param>
        void Resolve([NotNull] IBuildContext buildContext);
    }
}


#endregion

#endregion

#region Core

#region AspectOrientedAutowiringStrategy

// ReSharper disable ForCanBeConvertedToForeach
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    internal sealed class AspectOrientedAutowiringStrategy: IAutowiringStrategy
    {
        [NotNull] private readonly IAspectOrientedMetadata _metadata;

        public AspectOrientedAutowiringStrategy([NotNull] IAspectOrientedMetadata metadata)
        {
            _metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        }

        /// <inheritdoc />
        public bool TryResolveType(IContainer container, Type registeredType, Type resolvingType, out Type instanceType)
        {
            instanceType = default(Type);
            // Says that the default logic should be used
            return false;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryResolveConstructor(IContainer container, IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
        {
            constructor = PrepareMethods(constructors).FirstOrDefault();
            if (constructor == null && DefaultAutowiringStrategy.Shared.TryResolveConstructor(container, constructors, out var defaultConstructor))
            {
                // Initialize default ctor
                constructor = PrepareMethods(new[] { defaultConstructor }, true).FirstOrDefault();
            }

            // Says that current logic should be used
            return constructor != default(IMethod<ConstructorInfo>);
        }

        /// <inheritdoc />
        public bool TryResolveInitializers(IContainer container, IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = PrepareMethods(methods);
            // Says that current logic should be used
            return true;
        }

        private IEnumerable<IMethod<TMethodInfo>> PrepareMethods<TMethodInfo>(IEnumerable<IMethod<TMethodInfo>> methods, bool enforceSelection = false)
            where TMethodInfo : MethodBase =>
            from method in methods
            let methodMetadata = new Metadata(_metadata, method.Info.GetCustomAttributes(true))
            let parametersMetadata = GetParametersMetadata(method)
            where enforceSelection || !methodMetadata.IsEmpty || parametersMetadata.Any(i => !i.IsEmpty)
            orderby methodMetadata.Order
            select SetDependencies(method, parametersMetadata, methodMetadata);

        private Metadata[] GetParametersMetadata<TMethodInfo>(IMethod<TMethodInfo> method)
            where TMethodInfo : MethodBase
        {
            var parameters = method.Info.GetParameters();
            var metadata = new Metadata[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                if (param.IsOut)
                {
                    continue;
                }

                metadata[i] = new Metadata(_metadata, param.GetCustomAttributes(true));
            }

            return metadata;
        }

        private IMethod<TMethodInfo> SetDependencies<TMethodInfo>(IMethod<TMethodInfo> method, Metadata[] parametersMetadata, Metadata methodMetadata)
            where TMethodInfo : MethodBase
        {
            var parameters = method.Info.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                if (param.IsOut)
                {
                    continue;
                }

                var parameterMetadata = parametersMetadata[i];
                method.SetDependency(param.Position, parameterMetadata.Type ?? param.ParameterType, parameterMetadata.Tag ?? methodMetadata.Tag, param.IsOptional);
            }

            return method;
        }

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
        public bool TryResolveType(IContainer container, Type registeredType, Type resolvingType, out Type instanceType) =>
            GetAutowiringStrategy().TryResolveType(
                container ?? throw new ArgumentNullException(nameof(container)),
                registeredType ?? throw new ArgumentNullException(nameof(registeredType)),
                resolvingType ?? throw new ArgumentNullException(nameof(resolvingType)),
                out instanceType);

        /// <inheritdoc />
        public bool TryResolveConstructor(IContainer container, IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor) =>
            GetAutowiringStrategy().TryResolveConstructor(
                container ?? throw new ArgumentNullException(nameof(container)),
                constructors ?? throw new ArgumentNullException(nameof(constructors)),
                out constructor);

        /// <inheritdoc />
        public bool TryResolveInitializers(IContainer container, IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers) =>
            GetAutowiringStrategy().TryResolveInitializers(
                container ?? throw new ArgumentNullException(nameof(container)),
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
#region Binding

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal struct Binding<T>: IBinding<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        public Binding([NotNull] IMutableContainer container, [NotNull][ItemNotNull] params Type[] types)
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

        public Binding([NotNull] IBinding binding, [NotNull][ItemNotNull] params Type[] types)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Tokens = binding.Tokens;
            Types = binding.Types.Concat(types);
            Tags = binding.Tags;
            Lifetime = binding.Lifetime;
            AutowiringStrategy = binding.AutowiringStrategy;
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
            Tags = new List<object>(binding.Tags) { tagValue};
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

        public IMutableContainer Container { get; }

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
    using System.Text;
    using Issues;

    /// <summary>
    /// Represents build context.
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [PublicAPI]
    internal sealed class BuildContext : IBuildContext
    {
        [NotNull] private readonly IEnumerable<IBuilder> _builders;
        [NotNull] private readonly IObserver<ContainerEvent> _eventObserver;
        private readonly IList<ParameterExpression> _parameters = new List<ParameterExpression>();
        [NotNull] private readonly IDictionary<Type, Type> _typesMap;

        internal BuildContext(
            [CanBeNull] BuildContext parent,
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IEnumerable<ICompiler> compilers,
            [NotNull] IEnumerable<IBuilder> builders,
            [NotNull] IAutowiringStrategy defaultAutowiringStrategy,
            [NotNull] ParameterExpression argsParameter,
            [NotNull] ParameterExpression containerParameter,
            [NotNull] IObserver<ContainerEvent> eventObserver,
            int depth = 0)
        {
            Parent = parent;
            Key = key;
            Container = resolvingContainer ?? throw new ArgumentNullException(nameof(resolvingContainer));
            Compilers = compilers ?? throw new ArgumentNullException(nameof(compilers));
            _builders = builders ?? throw new ArgumentNullException(nameof(builders));
            _eventObserver = eventObserver ?? throw new ArgumentNullException(nameof(eventObserver));
            AutowiringStrategy = defaultAutowiringStrategy ?? throw new ArgumentNullException(nameof(defaultAutowiringStrategy));
            ArgsParameter = argsParameter ?? throw new ArgumentNullException(nameof(argsParameter));
            ContainerParameter = containerParameter ?? throw new ArgumentNullException(nameof(containerParameter));
            Depth = depth;
            _typesMap = parent == null ? new Dictionary<Type, Type>() : new Dictionary<Type, Type>(parent._typesMap);
        }

        public IBuildContext Parent { get; private set; }

        public Key Key { get; }

        public IContainer Container { get; }

        public IEnumerable<ICompiler> Compilers { get; private set; }

        public IAutowiringStrategy AutowiringStrategy { get; }
        
        public int Depth { get; }

        public ParameterExpression ArgsParameter { get; private set; }

        public ParameterExpression ContainerParameter { get; private set; }

        public IBuildContext CreateChild(Key key, IContainer container) => 
            CreateInternal(key, container ?? throw new ArgumentNullException(nameof(container)));

        public Expression CreateExpression(Expression defaultExpression = null)
        {
            var selectedContainer = Container;
            if (selectedContainer.Parent != null)
            {
                var parent = Parent;
                while (parent != null)
                {
                    if (
                        Key.Equals(parent.Key)
                        && Equals(parent.Container, selectedContainer))
                    {
                        selectedContainer = selectedContainer.Parent;
                        break;
                    }

                    parent = parent.Parent;
                }
            }

            if (!selectedContainer.TryGetDependency(Key, out var dependency, out var lifetime))
            {
                if (Container == selectedContainer || !Container.TryGetDependency(Key, out dependency, out lifetime))
                {
                    if (defaultExpression != null)
                    {
                        return defaultExpression;
                    }

                    try
                    {
                        var dependencyDescription = Container.Resolve<ICannotResolveDependency>().Resolve(this);
                        dependency = dependencyDescription.Dependency;
                        lifetime = dependencyDescription.Lifetime;
                    }
                    catch (Exception ex)
                    {
                        throw new BuildExpressionException(ex.Message, ex.InnerException);
                    }
                }
            }

            if (Depth >= 128)
            {
                Container.Resolve<IFoundCyclicDependency>().Resolve(this);
            }

            if (dependency.TryBuildExpression(this, lifetime, out var expression, out var error))
            {
                return expression;
            }

            return Container.Resolve<ICannotBuildExpression>().Resolve(this, dependency, lifetime, error);
        }

        public void AddParameter(ParameterExpression parameterExpression)
            => _parameters.Add(parameterExpression ?? throw new ArgumentNullException(nameof(parameterExpression)));

        public void MapType(Type fromType, Type toType) => _typesMap[fromType] = toType;

        public Expression FinalizeExpression(Expression baseExpression, ILifetime lifetime)
        {
            if (_parameters.Count > 0)
            {
                baseExpression = Expression.Block(baseExpression.Type, _parameters, baseExpression);
            }

            foreach (var builder in _builders)
            {
                baseExpression = baseExpression.Convert(Key.Type);
                baseExpression = builder.Build(this, baseExpression);
            }

            baseExpression = baseExpression.Convert(Key.Type);
            return lifetime?.Build(this, baseExpression) ?? baseExpression;
        }

        public bool TryCompile(LambdaExpression lambdaExpression, out Delegate lambdaCompiled, out Exception error)
        {
            error = default(Exception);
            try
            {
                foreach (var compiler in Compilers)
                {
                    if (compiler.TryCompile(this, lambdaExpression, out lambdaCompiled, out error))
                    {
                        _eventObserver.OnNext(ContainerEvent.Compilation(Container, new[] { Key }, lambdaExpression));
                        return true;
                    }

                    _eventObserver.OnNext(ContainerEvent.CompilationFailed(Container, new[] { Key }, lambdaExpression, error));
                }
            }
            catch (Exception ex)
            {
                error = ex;
                _eventObserver.OnNext(ContainerEvent.CompilationFailed(Container, new[] { Key }, lambdaExpression, ex));
            }

            lambdaCompiled = default(Delegate);
            return false;
        }

        private IBuildContext CreateInternal(Key key, IContainer container)
        {
            if (_typesMap.TryGetValue(key.Type, out var type))
            {
                key = new Key(type, key.Tag);
            }

            return new BuildContext(
                this,
                key,
                container,
                Compilers,
                _builders,
                AutowiringStrategy,
                ArgsParameter,
                ContainerParameter,
                _eventObserver,
                Depth + 1);
        }

        public override string ToString()
        {
            var path = new List<IBuildContext>();
            IBuildContext context = this;
            while (context != null)
            {
                path.Add(context);
                context = context.Parent;
            }

            var text = new StringBuilder();
            for (var i = path.Count - 1; i >= 0; i--)
            {
                text.AppendLine($"{new string(' ', path[i].Depth << 1)} building {path[i].Key} in {path[i].Container}");
            }

            return text.ToString();
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
    internal sealed class BuildExpressionException : InvalidOperationException
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

    internal sealed class CannotBuildExpression : ICannotBuildExpression
    {
        public static readonly ICannotBuildExpression Shared = new CannotBuildExpression();

        private CannotBuildExpression() { }

        public Expression Resolve(IBuildContext buildContext, IDependency dependency, ILifetime lifetime, Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            throw new InvalidOperationException($"Cannot build expression for the key {buildContext.Key} in the container {buildContext.Container}.\n{buildContext}", error);
        }
    }
}

#endregion
#region CannotGetResolver

namespace IoC.Core
{
    using System;
    using Issues;

    internal sealed class CannotGetResolver : ICannotGetResolver
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
#region CannotRegister

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Issues;

    internal sealed class CannotRegister : ICannotRegister
    {
        public static readonly ICannotRegister Shared = new CannotRegister();

        private CannotRegister() { }

        public IToken Resolve(IContainer container, IEnumerable<Key> keys, IDependency dependency, ILifetime lifetime)
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

    internal sealed class CannotResolveConstructor : ICannotResolveConstructor
    {
        public static readonly ICannotResolveConstructor Shared = new CannotResolveConstructor();

        private CannotResolveConstructor() { }

        public IMethod<ConstructorInfo> Resolve(IBuildContext buildContext, IEnumerable<IMethod<ConstructorInfo>> constructors)
        {
            if (constructors == null) throw new ArgumentNullException(nameof(constructors));
            throw new BuildExpressionException($"Cannot find a constructor for the type {buildContext.Key.Type}.\n{buildContext}", null);
        }
    }
}

#endregion
#region CannotResolveDependency

namespace IoC.Core
{
    using System;
    using Issues;

    internal sealed class CannotResolveDependency : ICannotResolveDependency
    {
        public static readonly ICannotResolveDependency Shared = new CannotResolveDependency();

        private CannotResolveDependency() { }

        public DependencyDescription Resolve(IBuildContext buildContext)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            throw new InvalidOperationException($"Cannot find the dependency for the key {buildContext.Key} in the container {buildContext.Container}.\n{buildContext}");
        }
    }
}

#endregion
#region CannotResolveGenericTypeArgument

namespace IoC.Core
{
    using System;
    using Issues;

    internal sealed class CannotResolveGenericTypeArgument : ICannotResolveGenericTypeArgument
    {
        public static readonly ICannotResolveGenericTypeArgument Shared = new CannotResolveGenericTypeArgument();

        public Type Resolve(IBuildContext buildContext, Type type, int genericTypeArgPosition, Type genericTypeArg)
        {
            var typeDescriptor = type.Descriptor();
            var genericTypeArgs = typeDescriptor.IsGenericTypeDefinition() ? typeDescriptor.GetGenericTypeParameters() : typeDescriptor.GetGenericTypeArguments();
            throw new InvalidOperationException($"Cannot resolve the generic type argument \'{genericTypeArgs[genericTypeArgPosition]}\' at position {genericTypeArgPosition} of the type {typeDescriptor.Type}.");
        }
    }
}


#endregion
#region CannotResolveType

namespace IoC.Core
{
    using System;
    using Issues;

    internal sealed class CannotResolveType : ICannotResolveType
    {
        public static readonly ICannotResolveType Shared = new CannotResolveType();

        private CannotResolveType() { }

        public Type Resolve(IBuildContext buildContext, Type registeredType, Type resolvingType)
        {
            if (registeredType == null) throw new ArgumentNullException(nameof(registeredType));
            if (resolvingType == null) throw new ArgumentNullException(nameof(resolvingType));
            throw new InvalidOperationException($"Cannot resolve instance type based on the registered type {registeredType} for resolving type {registeredType}.\n{buildContext}");
        }
    }
}

#endregion
#region CompositionRoot

namespace IoC.Core
{
    using System;

    internal sealed class CompositionRoot<TInstance> : ICompositionRoot<TInstance>
    {
        [NotNull] private readonly IToken _token;

        public CompositionRoot([NotNull] IToken token, [NotNull] TInstance instance)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            Instance = instance;
        }

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
#region ConfigurationFromDelegate

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal sealed class ConfigurationFromDelegate : IConfiguration
    {
        [NotNull] private readonly Func<IContainer, IToken> _configurationFactory;

        public ConfigurationFromDelegate([NotNull] Func<IContainer, IToken> configurationFactory) => 
            _configurationFactory = configurationFactory ?? throw new ArgumentNullException(nameof(configurationFactory));

        public IEnumerable<IToken> Apply(IMutableContainer container)
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

    internal sealed class ContainerEventToStringConverter : IConverter<ContainerEvent, IContainer, string>
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
                    text = $"add {FormatDependency(src)}.";
                    break;

                case EventType.ContainerStateSingletonLifetime:
                    text = $"remove {FormatDependency(src)}.";
                    break;

                case EventType.ResolverCompilation:
                    var body = src.ResolverExpression?.Body;
                    text = $"compile {FormatDependency(src)} from:\n{GetString(GetDebugView(body))}.";
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
        [MethodImpl((MethodImplOptions)0x100)]
        public static bool SequenceEqual<T>([NotNull] this T[] array1, [NotNull] T[] array2) =>
            ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);

        [MethodImpl((MethodImplOptions)0x200)]
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

        [MethodImpl((MethodImplOptions)0x100)]
        public static T[] EmptyArray<T>() => Empty<T>.Array;

        [MethodImpl((MethodImplOptions)0x200)]
        public static T[] CreateArray<T>(int size = 0, T value = default(T))
        {
            if (size == 0)
            {
                return Empty<T>.Array;
            }

            var array = new T[size];
            if (Equals(value, default(T)))
            {
                return array;
            }

            for (var i = 0; i < size; i++)
            {
                array[i] = value;
            }

            return array;
        }

        [MethodImpl((MethodImplOptions)0x200)]
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

        [Pure]
        [MethodImpl((MethodImplOptions)0x200)]
        public static T[] Copy<T>([NotNull] this T[] previous)
        {
            var length = previous.Length;
            var result = new T[length];
            Array.Copy(previous, result, length);
            return result;
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
    
    internal sealed class DefaultAutowiringStrategy : IAutowiringStrategy
    {
        public static readonly IAutowiringStrategy Shared = new DefaultAutowiringStrategy();

        private DefaultAutowiringStrategy() { }

        public bool TryResolveType(IContainer container, Type registeredType, Type resolvingType, out Type instanceType)
        {
            instanceType = default(Type);
            return false;
        }

        public bool TryResolveConstructor(IContainer container, IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
        {
            var ctors =
                from ctor in constructors
                let isNotObsoleted = !ctor.Info.IsDefined(typeof(ObsoleteAttribute), false)
                let parameters = ctor.Info.GetParameters()
                let canBeResolved = parameters.All(parameter =>
                    parameter.IsOptional ||
                    container.IsBound(parameter.ParameterType) ||
                    container.CanResolve(parameter.ParameterType))
                let order = (parameters.Length + 1) * (canBeResolved ? 0xffff : 1) * (isNotObsoleted ? 0xff : 1)
                orderby order descending
                select ctor;

            constructor = ctors.FirstOrDefault();
            return constructor != null;
        }
        public bool TryResolveInitializers(IContainer container, IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = Enumerable.Empty<IMethod<MethodInfo>>();
            return true;
        }
    }
}


#endregion
#region DefaultCompiler

namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal sealed class DefaultCompiler : ICompiler
    {
        public static readonly ICompiler Shared = new DefaultCompiler();

        private DefaultCompiler() { }

        public bool TryCompile(IBuildContext context, LambdaExpression lambdaExpression, out Delegate lambdaCompiled, out Exception error)
        {
            if (lambdaExpression == null) throw new ArgumentNullException(nameof(lambdaExpression));
            try
            {
                lambdaCompiled = lambdaExpression.Compile();
                error = default(Exception);
                return true;
            }
            catch (Exception ex)
            {
                error = ex;
                lambdaCompiled = default(Delegate);
                return false;
            }
        }
    }
}

#endregion
#region DependencyInjectionExpressionVisitor

// ReSharper disable ConstantNullCoalescingCondition
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using static TypeDescriptorExtensions;
    // ReSharper disable once RedundantNameQualifier
    using IContainer = IoC.IContainer;

    internal sealed class DependencyInjectionExpressionVisitor : ExpressionVisitor
    {
        private static readonly Exception InvalidExpressionError = new BuildExpressionException("Invalid expression", null);

        private static readonly Key ContextKey = new Key(typeof(Context));
        private static readonly TypeDescriptor ContextTypeDescriptor = new TypeDescriptor(typeof(Context));
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

        [SuppressMessage("ReSharper", "NotResolvedInText")]
        protected override Expression VisitMethodCall(MethodCallExpression methodCall)
        {
            var argumentsCount = methodCall.Arguments.Count;
            if (argumentsCount > 0)
            {
                if (methodCall.Method.IsGenericMethod)
                {
                    var genericMethodDefinition = methodCall.Method.GetGenericMethodDefinition();

                    // container.Inject<T>()
                    if (Equals(genericMethodDefinition, Injections.InjectGenericMethodInfo))
                    {
                        var containerExpression = methodCall.Arguments[0];
                        var key = new Key(methodCall.Method.ReturnType);
                        return CreateDependencyExpression(key, containerExpression, null);
                    }

                    if (Equals(genericMethodDefinition, Injections.TryInjectGenericMethodInfo))
                    {
                        var containerExpression = methodCall.Arguments[0];
                        var key = new Key(methodCall.Method.ReturnType);
                        return CreateDependencyExpression(key, containerExpression, Expression.Default(methodCall.Method.ReturnType));
                    }

                    if (Equals(genericMethodDefinition, Injections.TryInjectValueGenericMethodInfo))
                    {
                        var containerExpression = methodCall.Arguments[0];
                        var keyType = methodCall.Method.GetGenericArguments()[0];
                        var key = new Key(keyType);
                        var defaultExpression = Expression.Default(methodCall.Method.ReturnType);
                        var expression = CreateDependencyExpression(key, containerExpression, defaultExpression);
                        if (expression == defaultExpression)
                        {
                            return defaultExpression;
                        }

                        var ctor = methodCall.Method.ReturnType.Descriptor().GetDeclaredConstructors().First(i => i.GetParameters().Length == 1);
                        return Expression.New(ctor, expression);
                    }

                    if (argumentsCount > 2)
                    {
                        // container.Inject<T>(tag, args)
                        if (Equals(genericMethodDefinition, Injections.InjectWithTagGenericMethodInfo))
                        {
                            var containerExpression = methodCall.Arguments[0];
                            var tagExpression = methodCall.Arguments[1];
                            var tag = GetTag(tagExpression);
                            var argsExpression = Visit(methodCall.Arguments[2]);

                            var key = new Key(methodCall.Method.ReturnType, tag);
                            return OverrideArgsAndCreateDependencyExpression(argsExpression, key, containerExpression, null);
                        }

                        if (Equals(genericMethodDefinition, Injections.TryInjectWithTagGenericMethodInfo))
                        {
                            var containerExpression = methodCall.Arguments[0];
                            var tagExpression = methodCall.Arguments[1];
                            var tag = GetTag(tagExpression);
                            var argsExpression = Visit(methodCall.Arguments[2]);

                            var key = new Key(methodCall.Method.ReturnType, tag);
                            return OverrideArgsAndCreateDependencyExpression(argsExpression, key, containerExpression, Expression.Default(methodCall.Method.ReturnType));
                        }

                        if (Equals(genericMethodDefinition, Injections.TryInjectValueWithTagGenericMethodInfo))
                        {
                            var containerExpression = methodCall.Arguments[0];
                            var tagExpression = methodCall.Arguments[1];
                            var tag = GetTag(tagExpression);
                            var argsExpression = Visit(methodCall.Arguments[2]);

                            var keyType = methodCall.Method.GetGenericArguments()[0];
                            var key = new Key(keyType, tag);
                            var defaultExpression = Expression.Default(methodCall.Method.ReturnType);
                            var expression = OverrideArgsAndCreateDependencyExpression(argsExpression, key, containerExpression, defaultExpression);
                            if (expression == defaultExpression)
                            {
                                return defaultExpression;
                            }

                            var ctor = methodCall.Method.ReturnType.Descriptor().GetDeclaredConstructors().First(i => i.GetParameters().Length == 1);
                            return Expression.New(ctor, expression);
                        }

                        if (argumentsCount == 3)
                        {
                            // container.Inject<T>(destination, source)
                            if (Equals(genericMethodDefinition, Injections.AssignGenericMethodInfo))
                            {
                                var dstExpression = Visit(methodCall.Arguments[1]);
                                var srcExpression = Visit(methodCall.Arguments[2]);
                                var containerVar = Expression.Variable(typeof(IContainer));
                                return Expression.Block(
                                    new [] { containerVar },
                                    Expression.Assign(containerVar, Visit(methodCall.Arguments[0])),
                                    Expression.Assign(dstExpression ?? throw InvalidExpressionError, srcExpression ?? throw InvalidExpressionError),
                                    containerVar);
                            }
                        }
                    }
                }
                else
                {
                    if (argumentsCount > 1)
                    {
                        // container.Inject(type)
                        if (Equals(methodCall.Method, Injections.InjectMethodInfo))
                        {
                            var type = (Type) ((ConstantExpression) Visit(methodCall.Arguments[1]) ?? throw InvalidExpressionError).Value ?? throw InvalidExpressionError;
                            var containerExpression = methodCall.Arguments[0];
                            var key = new Key(type);
                            return CreateDependencyExpression(key, containerExpression, null);
                        }

                        if (Equals(methodCall.Method, Injections.TryInjectMethodInfo))
                        {
                            var type = (Type)((ConstantExpression)Visit(methodCall.Arguments[1]) ?? throw InvalidExpressionError).Value ?? throw InvalidExpressionError;
                            var containerExpression = methodCall.Arguments[0];
                            var key = new Key(type);
                            return CreateDependencyExpression(key, containerExpression, Expression.Default(type));
                        }

                        if (argumentsCount > 3)
                        {
                            // container.Inject(type, tag, args)
                            if (Equals(methodCall.Method, Injections.InjectWithTagMethodInfo))
                            {
                                var type = (Type)((ConstantExpression)Visit(methodCall.Arguments[1]) ?? throw InvalidExpressionError).Value ?? throw InvalidExpressionError;
                                var containerExpression = methodCall.Arguments[0];
                                var tagExpression = methodCall.Arguments[2];
                                var tag = GetTag(tagExpression);
                                var argsExpression = Visit(methodCall.Arguments[3]);

                                var key = new Key(type, tag);
                                return OverrideArgsAndCreateDependencyExpression(argsExpression, key, containerExpression, null);
                            }

                            if (Equals(methodCall.Method, Injections.TryInjectWithTagMethodInfo))
                            {
                                var type = (Type)((ConstantExpression)Visit(methodCall.Arguments[1]) ?? throw InvalidExpressionError).Value ?? throw InvalidExpressionError;
                                var containerExpression = methodCall.Arguments[0];
                                var tagExpression = methodCall.Arguments[2];
                                var tag = GetTag(tagExpression);
                                var argsExpression = Visit(methodCall.Arguments[3]);

                                var key = new Key(type, tag);
                                return OverrideArgsAndCreateDependencyExpression(argsExpression, key, containerExpression, Expression.Default(type));
                            }
                        }
                    }
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

        private Expression OverrideArgsAndCreateDependencyExpression(Expression argsExpression, Key key, Expression containerExpression, DefaultExpression defaultExpression)
        {
            if (argsExpression is NewArrayExpression newArrayExpression && newArrayExpression.Expressions.Count == 0)
            {
                return CreateDependencyExpression(key, containerExpression, defaultExpression);
            }

            var argsVar = Expression.Variable(_buildContext.ArgsParameter.Type);
            return Expression.Block(
                new[] { argsVar },
                Expression.Assign(_buildContext.ArgsParameter, argsExpression),
                CreateDependencyExpression(key, containerExpression, defaultExpression));
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var result = base.VisitUnary(node);

            if (result is UnaryExpression unaryExpression)
            {
                switch (unaryExpression.NodeType)
                {
                    case ExpressionType.Convert:
                        if (unaryExpression.Type == unaryExpression.Operand.Type)
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
            if (node.Type == typeof(Context))
            {
                return CreateNewContextExpression();
            }

            if (_thisExpression != null)
            {
                var typeDescriptor = node.Type.Descriptor();
                if (typeDescriptor.IsConstructedGenericType() && typeDescriptor.GetGenericTypeDefinition() == typeof(Context<>))
                {
                    var contextType = GenericContextTypeDescriptor.MakeGenericType(typeDescriptor.GetGenericTypeArguments()).Descriptor();
                    var ctor = contextType.GetDeclaredConstructors().Single();
                    return Expression.New(
                        ctor,
                        _thisExpression,
                        Expression.Constant(_buildContext.Key),
                        _buildContext.ContainerParameter,
                        _buildContext.ArgsParameter);
                }
            }

            return base.VisitParameter(node);
        }

        private Expression CreateNewContextExpression() =>
            Expression.New(
                ContextConstructor,
                Expression.Constant(_buildContext.Key),
                _buildContext.ContainerParameter,
                _buildContext.ArgsParameter);

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
                    // ReSharper disable once ConstantNullCoalescingCondition
                    var expression = Visit(tagExpression) ?? throw new BuildExpressionException($"Invalid tag expression {tagExpression}.", new InvalidOperationException());
                    if (_buildContext.TryCompile(Expression.Lambda(expression, true), out var tagFunc, out var error))
                    {
                        tag = ((Func<object>)tagFunc)();
                    }
                    else
                    {
                        throw error;
                    }

                    break;
            }

            return tag;
        }

        private IEnumerable<Expression> InjectAll(IEnumerable<Expression> expressions) => 
            expressions.Select(Visit);

        private IContainer SelectedContainer([NotNull] Expression containerExpression)
        {
            if (containerExpression is ParameterExpression parameterExpression && parameterExpression.Type == typeof(IContainer))
            {
                return _container;
            }

            var containerSelectorExpression = Expression.Lambda<ContainerSelector>(containerExpression, true, _buildContext.ContainerParameter);
            if (_buildContext.TryCompile(containerSelectorExpression, out var selectContainer, out var error))
            {
                return ((ContainerSelector)selectContainer)(_container);
            }

            throw error;
        }

        private Expression CreateDependencyExpression(Key key, [CanBeNull] Expression containerExpression, DefaultExpression defaultExpression)
        {
            if (Equals(key, ContextKey))
            {
                return CreateNewContextExpression();
            }

            var selectedContainer = containerExpression != null ? SelectedContainer(Visit(containerExpression) ?? throw InvalidExpressionError) : _container;
            return _buildContext.CreateChild(key, selectedContainer).CreateExpression(defaultExpression);
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
                    expression = _buildContext.ContainerParameter;
                    return true;
                }

                // ctx.Args
                if (name == nameof(Context.Args))
                {
                    expression = _buildContext.ArgsParameter;
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

// ReSharper disable RedundantUsingDirective
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

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IDisposable Create([NotNull] Action action)
        {
#if DEBUG
            if (action == null) throw new ArgumentNullException(nameof(action));
#endif
            return new DisposableAction(action);
        }

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IDisposable Create([NotNull][ItemCanBeNull] IEnumerable<IDisposable> disposables)
        {
#if DEBUG
            if (disposables == null) throw new ArgumentNullException(nameof(disposables));
#endif
            return new CompositeDisposable(disposables);
        }

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
        [MethodImpl((MethodImplOptions)0x100)]
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

        private sealed class EmptyDisposable : IDisposable
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

    internal static class ExpressionBuilderExtensions
    {
        private static readonly TypeDescriptor ResolverGenericTypeDescriptor = typeof(Resolver<>).Descriptor();
        internal static readonly Expression NullConst = Expression.Constant(null);
        internal static readonly Expression ContainerExpression = Expression.Field(Expression.Constant(null, typeof(Context)), nameof(Context.Container));
        internal static readonly MethodInfo EqualsMethodInfo = typeof(Object).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(Equals) && i.ReturnType == typeof(bool) && i.GetParameters().Length == 2 && i.GetParameters()[0].ParameterType == typeof(object) && i.GetParameters()[1].ParameterType == typeof(object));
        private static readonly MethodInfo EnterMethodInfo = typeof(Monitor).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(Monitor.Enter) && i.GetParameters().Length == 1);
        private static readonly MethodInfo ExitMethodInfo = typeof(Monitor).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(Monitor.Exit));

        [MethodImpl((MethodImplOptions)0x100)]
        public static Expression Convert(this Expression expression, Type type)
        {
            var targetType = expression.Type.Descriptor();
            if (type != typeof(object))
            {
                var typeDescriptor = type.Descriptor();
                if (typeDescriptor.IsAssignableFrom(targetType))
                {
                    return expression;
                }
            }

            return Expression.Convert(expression, type);
        }

        [MethodImpl((MethodImplOptions)0x100)]
        public static Type ToResolverType(this Type type) =>
            ResolverGenericTypeDescriptor.MakeGenericType(type);

        [MethodImpl((MethodImplOptions)0x100)]
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
    using Dependencies;
    using Issues;

    /// <summary>
    /// Represents extensions to register a dependency in the container.
    /// </summary>
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    [PublicAPI]
    internal static partial class FluentRegister
    {
        internal static readonly object[] AnyTag = { Key.AnyTag };
        private static readonly IEnumerable<object> DefaultTags = new object[] { null };

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The registration token.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [IoC.NotNull]
        public static IToken Register<T>([NotNull] this IMutableContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null) 
            => container.Register(new[] { typeof(T)}, new AutowiringDependency(typeof(T)), lifetime, tags);

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
        [MethodImpl((MethodImplOptions)0x100)]
        [IoC.NotNull]
        public static IToken Register<T>([NotNull] this IMutableContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [IoC.NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            => container.Register(new[] { typeof(T) }, new ExpressionDependency(factory, null, statements), lifetime, tags);

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
        public static IToken Register([NotNull] this IMutableContainer container, [NotNull][ItemNotNull] IEnumerable<Type> types, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime = null, [CanBeNull][ItemCanBeNull] object[] tags = null)
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
                : container.Resolve<ICannotRegister>().Resolve(container, keys, dependency, lifetime);
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
    using Dependencies;

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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Register<T, T1>([NotNull] this IMutableContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T: T1
            => container.Register(new[] { typeof(T1) }, new AutowiringDependency(typeof(T)), lifetime, tags);

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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Register<T, T1>([NotNull] this IMutableContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T: T1
            // ReSharper disable once CoVariantArrayConversion
            => container.Register(new[] { typeof(T), typeof(T1) }, new ExpressionDependency(factory, null, statements), lifetime, tags);

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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Register<T, T1, T2>([NotNull] this IMutableContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T: T1, T2
            => container.Register(new[] { typeof(T1), typeof(T2) }, new AutowiringDependency(typeof(T)), lifetime, tags);

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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Register<T, T1, T2>([NotNull] this IMutableContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T: T1, T2
            // ReSharper disable once CoVariantArrayConversion
            => container.Register(new[] { typeof(T), typeof(T1), typeof(T2) }, new ExpressionDependency(factory, null, statements), lifetime, tags);

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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Register<T, T1, T2, T3>([NotNull] this IMutableContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T: T1, T2, T3
            => container.Register(new[] { typeof(T1), typeof(T2), typeof(T3) }, new AutowiringDependency(typeof(T)), lifetime, tags);

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
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IToken Register<T, T1, T2, T3>([NotNull] this IMutableContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T: T1, T2, T3
            // ReSharper disable once CoVariantArrayConversion
            => container.Register(new[] { typeof(T), typeof(T1), typeof(T2), typeof(T3) }, new ExpressionDependency(factory, null, statements), lifetime, tags);

    }
}

#endregion
#region FoundCyclicDependency

namespace IoC.Core
{
    using System;
    using Issues;

    internal sealed class FoundCyclicDependency : IFoundCyclicDependency
    {
        public static readonly IFoundCyclicDependency Shared = new FoundCyclicDependency();

        private FoundCyclicDependency() { }

        public void Resolve(IBuildContext buildContext)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));

            if (buildContext.Depth >= 256)
            {
                throw new InvalidOperationException($"The cyclic dependency detected resolving the dependency {buildContext.Key}. The reentrancy is {buildContext.Depth}.\n{buildContext}");
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
#region LockObject

namespace IoC.Core
{
    internal sealed class LockObject : ILockObject { }
}


#endregion
#region Method

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal sealed class Method<TMethodInfo> : IMethod<TMethodInfo> where TMethodInfo : MethodBase
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
                var param = _parameters[parameterPosition];
                if (param.IsOut)
                {
                    if (param.ParameterType.HasElementType)
                    {
                        var type = param.ParameterType.GetElementType();
                        if (type != null)
                        {
                            var outParam = Expression.Parameter(type);
                            buildContext.AddParameter(outParam);
                            yield return outParam;
                            continue;
                        }
                    }
                }

                var expression = _parametersExpressions[parameterPosition];
                if (expression != null)
                {
                    yield return expression;
                }
                else
                {
                    var key = new Key(param.ParameterType);
                    Expression defaultExpression;
                    if (param.IsOptional)
                    {
                        if (param.DefaultValue == null)
                        {
                            defaultExpression = Expression.Default(param.ParameterType);
                        }
                        else
                        {
                            defaultExpression = Expression.Constant(param.DefaultValue);
                        }
                    }
                    else
                    {
                        defaultExpression = null;
                    }

                    yield return buildContext.CreateChild(key, buildContext.Container).CreateExpression(defaultExpression);
                }
            }
        }

        public void SetExpression(int parameterPosition, Expression parameterExpression)
        {
            if (parameterPosition < 0 || parameterPosition >= _parametersExpressions.Length) throw new ArgumentOutOfRangeException(nameof(parameterPosition));

            _parametersExpressions[parameterPosition] = parameterExpression ?? throw new ArgumentNullException(nameof(parameterExpression));
        }

        public void SetDependency(int parameterPosition, Type dependencyType, object dependencyTag = null, bool isOptional = false, params object[] args)
        {
            if (dependencyType == null) throw new ArgumentNullException(nameof(dependencyType));
            if (parameterPosition < 0 || parameterPosition >= _parametersExpressions.Length) throw new ArgumentOutOfRangeException(nameof(parameterPosition));

            var injectMethod = isOptional ? Injections.TryInjectWithTagMethodInfo : Injections.InjectWithTagMethodInfo;
            var parameterExpression = Expression.Call(
                    injectMethod,
                    ExpressionBuilderExtensions.ContainerExpression,
                    Expression.Constant(dependencyType),
                    Expression.Constant(dependencyTag),
                    Expression.NewArrayInit(typeof(object), args.Select(Expression.Constant)))
                .Convert(dependencyType);

            SetExpression(parameterPosition, parameterExpression.Convert(dependencyType));
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
    using Issues;

    internal sealed class NullContainer : IContainer
    {
        public static readonly IContainer Shared = new NullContainer();
        private static readonly NotSupportedException NotSupportedException = new NotSupportedException();

        private NullContainer() { }

        public IContainer Parent => null;

        public bool TryGetDependency(Key key, out IDependency dependency, out ILifetime lifetime)
        {
            dependency = default(IDependency);
            lifetime = default(ILifetime);
            return false;
        }

        public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, out Exception error, IContainer resolvingContainer = null)
        {
            if (type == typeof(ICannotGetResolver))
            {
                throw NotSupportedException;
            }

            resolver = default(Resolver<T>);
            error = new InvalidOperationException($"Cannot get resolver for {type} and tag \"{tag}\".");
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

// ReSharper disable UnusedMember.Global
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

        private sealed class InternalObservable<T>: IObservable<T>
        {
            private readonly Func<IObserver<T>, IDisposable> _factory;

            public InternalObservable(Func<IObserver<T>, IDisposable> factory) => _factory = factory;

            public IDisposable Subscribe(IObserver<T> observer) => _factory(observer ?? throw new ArgumentNullException(nameof(observer)));
        }

        private sealed class InternalObserver<T>: IObserver<T>
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
#region Registration

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    internal sealed class Registration : IToken
    {
        /// <summary>
        /// The container parameter.
        /// </summary>
        [NotNull]
        internal static readonly ParameterExpression ContainerParameter = Expression.Parameter(typeof(IContainer));

        /// <summary>
        /// The args parameters.
        /// </summary>
        [NotNull]
        internal static readonly ParameterExpression ArgsParameter = Expression.Parameter(typeof(object[]));

        /// <summary>
        /// All resolvers parameters.
        /// </summary>
        [NotNull] [ItemNotNull] internal static readonly IEnumerable<ParameterExpression> ResolverParameters = new List<ParameterExpression> { ContainerParameter, ArgsParameter };

        [CanBeNull] internal readonly ILifetime Lifetime;
        [NotNull] private readonly IDisposable _resource;
        [NotNull] internal readonly ICollection<Key> Keys;
        [NotNull] private readonly IObserver<ContainerEvent> _eventObserver;
        [NotNull] internal readonly IDependency Dependency;

        private volatile Table<LifetimeKey, ILifetime> _lifetimes = Table<LifetimeKey, ILifetime>.Empty;
        private bool _disposed;

        public Registration(
            [NotNull] IMutableContainer container,
            [NotNull] IObserver<ContainerEvent> eventObserver,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource,
            [NotNull] ICollection<Key> keys)
        {
            Container = container;
            _eventObserver = eventObserver;
            Dependency = dependency;
            Lifetime = lifetime;
            _resource = resource;
            Keys = keys;
        }

        public IMutableContainer Container { get; }

        [MethodImpl((MethodImplOptions)0x200)]
        public bool TryCreateResolver<T>(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IRegistrationTracker registrationTracker,
            out Resolver<T> resolver,
            out Exception error)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Registration));
            }

            var buildContext = new BuildContext(
                null,
                key,
                resolvingContainer,
                registrationTracker.Compilers,
                registrationTracker.Builders,
                registrationTracker.AutowiringStrategy,
                ArgsParameter,
                ContainerParameter,
                _eventObserver);

            var lifetime = GetLifetime(key.Type);
            if (!Dependency.TryBuildExpression(buildContext, lifetime, out var expression, out error))
            {
                resolver = default(Resolver<T>);
                return false;
            }

            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), expression, false, ResolverParameters);
            if (buildContext.TryCompile(resolverExpression, out var resolverDelegate, out error))
            {
                resolver = (Resolver<T>) resolverDelegate;
                return true;
            }

            resolver = default(Resolver<T>);
            return false;
        }

        [MethodImpl((MethodImplOptions)0x200)]
        [CanBeNull]
        public ILifetime GetLifetime([NotNull] Type type)
        {
            if (Lifetime == default(ILifetime))
            {
                return default(ILifetime);
            }

            var hasLifetimes = _lifetimes.Count != 0;
            if (!hasLifetimes && !Keys.Any(key => key.Type.Descriptor().IsGenericTypeDefinition()))
            {
                return Lifetime;
            }

            var lifetimeKey = new LifetimeKey(type.Descriptor().GetGenericTypeArguments());

            if (!hasLifetimes)
            {
                _lifetimes = _lifetimes.Set(lifetimeKey, Lifetime);
                return Lifetime;
            }

            var lifetime = _lifetimes.Get(lifetimeKey);
            if (lifetime != default(ILifetime))
            {
                return lifetime;
            }

            lifetime = Lifetime.Create();
            _lifetimes = _lifetimes.Set(lifetimeKey, lifetime);

            return lifetime;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            Lifetime?.Dispose();
            foreach (var lifetime in _lifetimes)
            {
                lifetime.Value.Dispose();
            }

            _resource.Dispose();
        }

        public override string ToString() => 
            $"{string.Join(", ", Keys.Select(i => i.ToString()))} as {Lifetime?.ToString() ?? IoC.Lifetime.Transient.ToString()}";

        private struct LifetimeKey: IEquatable<LifetimeKey>
        {
            private readonly Type[] _genericTypes;
            private readonly int _hashCode;

            public LifetimeKey(Type[] genericTypes)
            {
                _genericTypes = genericTypes;
                _hashCode = genericTypes.GetHash();
            }

            public bool Equals(LifetimeKey other) =>
                CoreExtensions.SequenceEqual(_genericTypes, other._genericTypes);

            // ReSharper disable once PossibleNullReferenceException
            public override bool Equals(object obj) =>
                CoreExtensions.SequenceEqual(_genericTypes, ((LifetimeKey)obj)._genericTypes);

            public override int GetHashCode() => _hashCode;
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

    internal sealed class RegistrationTracker : IRegistrationTracker
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
            if (value.Keys == null || !value.IsSuccess || value.EventType != EventType.RegisterDependency && value.EventType != EventType.ContainerStateSingletonLifetime)
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

                case EventType.ContainerStateSingletonLifetime:
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

        private sealed class Tracker<T> : ITracker
            where T: class
        {
            private readonly Action<IList<T>, T> _updater;
            public readonly IList<T> Items = new List<T>();
            private Table<IContainer, T> _map = Table<IContainer, T>.Empty;

            public Tracker(Action<IList<T>, T> updater) => 
                _updater = updater;

            public bool Track(Key key, IContainer container)
            {
                if (key.Type != typeof(T))
                {
                    return false;
                }

                if (_map.Get(container) != null)
                {
                    return true;
                }

                if (container.TryGetResolver<T>(key.Type, key.Tag, out var resolver, out _, container))
                {
                    var instance = resolver(container);
                    _map = _map.Set(container, instance);
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

                var instance = _map.Get(container);
                if (instance != null)
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
    internal sealed class Subject<T>: ISubject<T>
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
    internal sealed class Table<TKey, TValue>: IEnumerable<Table<TKey, TValue>.KeyValue>
    {
        private static readonly Bucket EmptyBucket = new Bucket(CoreExtensions.EmptyArray<KeyValue>());
        public static readonly Table<TKey, TValue> Empty = new Table<TKey, TValue>(CoreExtensions.CreateArray(4, EmptyBucket), 3, 0);
        public readonly int Count;
        public readonly int Divisor;
        public readonly Bucket[] Buckets;

        private Table(Bucket[] buckets, int divisor, int count)
        {
            Buckets = buckets;
            Divisor = divisor;
            Count = count;
        }

        [MethodImpl((MethodImplOptions)0x200)]
        private Table(Table<TKey, TValue> origin, TKey key, TValue value)
        {
            int newBucketIndex;
            Count = origin.Count + 1;
            if (origin.Count > origin.Divisor)
            {
                Divisor = (origin.Divisor + 1) * 4 - 1;
                Buckets = CoreExtensions.CreateArray(Divisor + 1, EmptyBucket);
                var originBuckets = origin.Buckets;
                for (var originBucketIndex = 0; originBucketIndex < originBuckets.Length; originBucketIndex++)
                {
                    var originBucket = originBuckets[originBucketIndex];
                    for (var index = 0; index < originBucket.KeyValues.Length; index++)
                    {
                        var keyValue = originBucket.KeyValues[index];
                        newBucketIndex = keyValue.Key.GetHashCode() & Divisor;
                        Buckets[newBucketIndex] = Buckets[newBucketIndex].Add(keyValue);
                    }
                }
            }
            else
            {
                Divisor = origin.Divisor;
                Buckets = origin.Buckets.Copy();
            }

            newBucketIndex = key.GetHashCode() & Divisor;
            Buckets[newBucketIndex] = Buckets[newBucketIndex].Add(new KeyValue(key, value));
        }

        [MethodImpl((MethodImplOptions)0x200)]
        [Pure]
        public TValue Get(TKey key)
        {
            var bucket = Buckets[key.GetHashCode() & Divisor];
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < bucket.KeyValues.Length; index++)
            {
                var item = bucket.KeyValues[index];
                if (ReferenceEquals(key, item.Key) || Equals(key, item.Key))
                {
                    return item.Value;
                }
            }

            return default(TValue);
        }

        [MethodImpl((MethodImplOptions)0x200)]
        [Pure]
        public IEnumerator<KeyValue> GetEnumerator()
        {
            for (var bucketIndex = 0; bucketIndex < Buckets.Length; bucketIndex++)
            {
                var bucket = Buckets[bucketIndex];
                for (var index = 0; index < bucket.KeyValues.Length; index++)
                {
                    yield return bucket.KeyValues[index];
                }
            }
        }

        [Pure]
        [MethodImpl((MethodImplOptions)0x100)]
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public Table<TKey, TValue> Set(TKey key, TValue value) =>
            new Table<TKey, TValue>(this, key, value);

        [MethodImpl((MethodImplOptions)0x200)]
        [Pure]
        public Table<TKey, TValue> Remove(TKey key, out bool removed)
        {
            removed = false;
            var newBuckets = CoreExtensions.CreateArray(Divisor + 1, EmptyBucket);
            var hashCode = key.GetHashCode();
            var bucketIndex = hashCode & Divisor;
            for (var curBucketIndex = 0; curBucketIndex < Buckets.Length; curBucketIndex++)
            {
                var bucket = Buckets[curBucketIndex];
                if (curBucketIndex != bucketIndex)
                {
                    newBuckets[curBucketIndex] = bucket.Copy();
                    continue;
                }

                // Bucket to remove an element
                for (var index = 0; index < bucket.KeyValues.Length; index++)
                {
                    var keyValue = bucket.KeyValues[index];
                    // Remove the element
                    if (keyValue.Key.GetHashCode() == hashCode && (ReferenceEquals(keyValue.Key, key) || Equals(keyValue.Key, key)))
                    {
                        newBuckets[bucketIndex] = bucket.Remove(index);
                        removed = true;
                    }
                }
            }

            return new Table<TKey, TValue>(newBuckets, Divisor, removed ? Count - 1: Count);
        }

        internal struct Bucket
        {
            public readonly KeyValue[] KeyValues;

            public Bucket(KeyValue[] keyValues) => KeyValues = keyValues;

            [MethodImpl((MethodImplOptions)0x100)]
            public Bucket Add(KeyValue keyValue) =>
                new Bucket(KeyValues.Add(keyValue));

            [MethodImpl((MethodImplOptions)0x100)]
            public Bucket Copy() =>
                KeyValues.Length == 0 ? EmptyBucket : new Bucket(KeyValues.Copy());

            [MethodImpl((MethodImplOptions)0x200)]
            public Bucket Remove(int index)
            {
                var newLeyValues = new KeyValue[KeyValues.Length - 1];
                for (var newIndex = 0; newIndex < index; newIndex++)
                {
                    newLeyValues[newIndex] = KeyValues[newIndex];
                }

                for (var newIndex = index + 1; newIndex < KeyValues.Length; newIndex++)
                {
                    newLeyValues[newIndex - 1] = KeyValues[newIndex];
                }

                return new Bucket(newLeyValues);
            }

            public override string ToString() => $"Bucket[{KeyValues.Length}]";
        }

        internal struct KeyValue
        {
            public readonly TKey Key;
            public readonly TValue Value;

            public KeyValue(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}

#endregion
#region TableExtensions

namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class TableExtensions
    {
        [MethodImpl((MethodImplOptions)0x200)]
        [Pure]
        public static bool TryGetByType<TValue>(this Table<Type, TValue> table, Type key, out TValue value)
        {
            var bucket = table.Buckets[key.GetHashCode() & table.Divisor];
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < bucket.KeyValues.Length; index++)
            {
                var item = bucket.KeyValues[index];
                if (key == item.Key)
                {
                    value = item.Value;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        [MethodImpl((MethodImplOptions)0x200)]
        [Pure]
        public static bool TryGetByKey<TValue>(this Table<Key, TValue> table, Key key, out TValue value)
        {
            var bucket = table.Buckets[key.GetHashCode() & table.Divisor];
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < bucket.KeyValues.Length; index++)
            {
                var item = bucket.KeyValues[index];
                if (key.Equals(item.Key))
                {
                    value = item.Value;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }
    }
}

#endregion
#region Token

namespace IoC.Core
{
    using System;

    internal struct Token: IToken
    {
        [NotNull] private readonly IDisposable _dependencyToken;

        public Token([NotNull] IMutableContainer container, [NotNull] IDisposable dependencyToken)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _dependencyToken = dependencyToken ?? throw new ArgumentNullException(nameof(dependencyToken));
        }

        public IMutableContainer Container { get; }

        public void Dispose() => _dependencyToken.Dispose();
    }
}


#endregion
#region TypeDescriptor

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global
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

        public TypeDescriptor([NotNull] Type type) =>
            Type = type ?? throw new ArgumentNullException(nameof(type));

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type AsType() => Type;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public Guid GetId() => Type.GUID;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Assembly GetAssembly() => Type.Assembly;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsValueType() => Type.IsValueType;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsArray() => Type.IsArray;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsPublic() => Type.IsPublic;

        [MethodImpl((MethodImplOptions)0x100)]
        [CanBeNull]
        [Pure]
        public Type GetElementType() => Type.GetElementType();

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsInterface() => Type.IsInterface;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsAbstract() => Type.IsAbstract;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsGenericParameter() => Type.IsGenericParameter;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsConstructedGenericType() => Type.IsGenericType && !Type.IsGenericTypeDefinition;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsGenericTypeDefinition() => Type.IsGenericTypeDefinition;

        public bool IsGenericTypeArgument() => Type.GetCustomAttributes(typeof(GenericTypeArgumentAttribute), true).Length > 0;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeArguments() => Type.GetGenericArguments();

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type[] GetGenericParameterConstraints() => Type.GetGenericParameterConstraints();

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public GenericParameterAttributes GetGenericParameterAttributes() => Type.GenericParameterAttributes;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeParameters() => Type.GetGenericArguments();

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<ConstructorInfo> GetDeclaredConstructors() => Type.GetConstructors(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<MethodInfo> GetDeclaredMethods() => Type.GetMethods(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<MemberInfo> GetDeclaredMembers() => Type.GetMembers(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<FieldInfo> GetDeclaredFields() => Type.GetFields(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<PropertyInfo> GetDeclaredProperties() => Type.GetProperties(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)0x100)]
        [CanBeNull]
        [Pure]
        public Type GetBaseType() => Type.BaseType;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<Type> GetImplementedInterfaces() => Type.GetInterfaces();

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsAssignableFrom(TypeDescriptor typeDescriptor) =>Type.IsAssignableFrom(typeDescriptor.Type);

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type MakeGenericType([NotNull] params Type[] typeArguments) => Type.MakeGenericType(typeArguments ?? throw new ArgumentNullException(nameof(typeArguments)));

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type GetGenericTypeDefinition() => Type.GetGenericTypeDefinition();

        public override string ToString() => TypeToStringConverter.Convert(Type);

        public override bool Equals(object obj) => obj is TypeDescriptor other && Type == other.Type;

        public override int GetHashCode() => Type.GetHashCode();
#else
        internal readonly Type Type;
        private readonly TypeInfo _typeInfo;

        public TypeDescriptor([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            _typeInfo = type.GetTypeInfo();
        }

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type AsType() => Type;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public Guid GetId() => _typeInfo.GUID;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Assembly GetAssembly() => _typeInfo.Assembly;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsValueType() => _typeInfo.IsValueType;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsInterface() => _typeInfo.IsInterface;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsAbstract() => _typeInfo.IsAbstract;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsGenericParameter() => _typeInfo.IsGenericParameter;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsArray() => _typeInfo.IsArray;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsPublic() => _typeInfo.IsPublic;

        [MethodImpl((MethodImplOptions)0x100)]
        [CanBeNull]
        [Pure]
        public Type GetElementType() => _typeInfo.GetElementType();

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsConstructedGenericType() => Type.IsConstructedGenericType;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsGenericTypeDefinition() => _typeInfo.IsGenericTypeDefinition;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeArguments() => _typeInfo.GenericTypeArguments;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type[] GetGenericParameterConstraints() => _typeInfo.GetGenericParameterConstraints();

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public GenericParameterAttributes GetGenericParameterAttributes() => _typeInfo.GenericParameterAttributes;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type[] GetGenericTypeParameters() => _typeInfo.GenericTypeParameters;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsGenericTypeArgument() => _typeInfo.GetCustomAttribute<GenericTypeArgumentAttribute>(true) != null;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<ConstructorInfo> GetDeclaredConstructors() => _typeInfo.DeclaredConstructors;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<MethodInfo> GetDeclaredMethods() => _typeInfo.DeclaredMethods;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<MemberInfo> GetDeclaredMembers() => _typeInfo.DeclaredMembers;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<FieldInfo> GetDeclaredFields() => _typeInfo.DeclaredFields;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<PropertyInfo> GetDeclaredProperties() => _typeInfo.DeclaredProperties;

        [MethodImpl((MethodImplOptions)0x100)]
        [CanBeNull]
        [Pure]
        public Type GetBaseType() => _typeInfo.BaseType;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public IEnumerable<Type> GetImplementedInterfaces() => _typeInfo.ImplementedInterfaces;

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public bool IsAssignableFrom(TypeDescriptor typeDescriptor) => _typeInfo.IsAssignableFrom(typeDescriptor._typeInfo);

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type MakeGenericType([NotNull] params Type[] typeArguments) => Type.MakeGenericType(typeArguments ?? throw new ArgumentNullException(nameof(typeArguments)));

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        [Pure]
        public Type GetGenericTypeDefinition() => Type.GetGenericTypeDefinition();

        public override string ToString() => TypeToStringConverter.Convert(Type);

        public override bool Equals(object obj) => obj is TypeDescriptor other && Type == other.Type;

        public override int GetHashCode() => Type != null ? Type.GetHashCode() : 0;
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
        [MethodImpl((MethodImplOptions)0x100)]
        public static TypeDescriptor Descriptor(this Type type) => new TypeDescriptor(type);

        [MethodImpl((MethodImplOptions)0x100)]
        public static TypeDescriptor Descriptor<T>() => TypeDescriptor<T>.Descriptor;

        [MethodImpl((MethodImplOptions)0x100)]
        public static Assembly LoadAssembly(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(assemblyName));
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        [MethodImpl((MethodImplOptions)0x100)]
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
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    internal static class TypeDescriptor<T>
    {
        public static readonly TypeDescriptor Descriptor = new TypeDescriptor(typeof(T));
        public static readonly int HashCode = typeof(T).GetHashCode();
    }
}


#endregion
#region TypeMapper

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal sealed class TypeMapper
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
#region TypeReplacerExpressionVisitor

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal sealed class TypeReplacerExpressionVisitor : ExpressionVisitor
    {
        [NotNull] private readonly IDictionary<Type, Type> _typesMap;
        [NotNull] private readonly Dictionary<ParameterExpression, ParameterExpression> _parameters = new Dictionary<ParameterExpression, ParameterExpression>();

        public TypeReplacerExpressionVisitor([NotNull] IDictionary<Type, Type> typesMap) =>
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));

        protected override Expression VisitNew(NewExpression node)
        {
            var newTypeDescriptor = ReplaceType(node.Type).Descriptor();
            var newConstructor = newTypeDescriptor.GetDeclaredConstructors().SingleOrDefault(i => !i.IsPrivate && Match(node.Constructor.GetParameters(), i.GetParameters()));
            if (newConstructor == null)
            {
                if (newTypeDescriptor.IsValueType())
                {
                    return Expression.Default(newTypeDescriptor.Type);
                }

                throw new BuildExpressionException($"Cannot find a constructor for {newTypeDescriptor.Type}.", null);
            }

            return Expression.New(newConstructor, ReplaceAll(node.Arguments));
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var newDeclaringType = ReplaceType(node.Method.DeclaringType).Descriptor();
            var newMethod = newDeclaringType
                .GetDeclaredMethods()
                .SingleOrDefault(i => i.Name == node.Method.Name && Match(node.Method.GetParameters(), i.GetParameters()))
                ?? throw new BuildExpressionException($"Cannot find method {node.Method} in the {node.Method.DeclaringType}.", new InvalidOperationException()); ;

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
            return newExpression == null ? base.VisitMember(node) : Expression.MakeMemberAccess(newExpression, newMember);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_parameters.TryGetValue(node, out var newNode))
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

            _parameters[node] = newNode;
            return newNode;
        }
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value is Type type)
            {
                return Expression.Constant(ReplaceType(type), node.Type);
            }

            var newType = ReplaceType(node.Type);
            var value = node.Value;
            return node.Value == null ? (Expression) Expression.Default(newType) : Expression.Constant(value, newType);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var parameters = node.Parameters.Select(VisitParameter).Cast<ParameterExpression>();
            var body = Visit(node.Body);
            return body == null ? base.VisitLambda(node) : Expression.Lambda(ReplaceType(node.Type), body, parameters);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            var elementType = ReplaceType(node.Type.GetElementType());
            var elements = ReplaceAll(node.Expressions).Select(i => i.Convert(elementType));
            return Expression.NewArrayInit(elementType, elements);
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            var newExpression = (NewExpression)Visit(node.NewExpression);
            return newExpression == null ? node : Expression.ListInit(newExpression, node.Initializers.Select(VisitInitializer));
        }

        private ElementInit VisitInitializer(ElementInit node)
        {
            var newDeclaringType = ReplaceType(node.AddMethod.DeclaringType).Descriptor();
            var newMethod = newDeclaringType
                .GetDeclaredMethods()
                .SingleOrDefault(i => i.Name == node.AddMethod.Name && Match(node.AddMethod.GetParameters(), i.GetParameters()))
                ?? throw new BuildExpressionException($"Cannot find method \"{node.AddMethod.Name}\" in the {node.AddMethod.DeclaringType}.", new InvalidOperationException());

            if (newMethod.IsGenericMethod)
            {
                newMethod = newMethod.MakeGenericMethod(ReplaceTypes(node.AddMethod.GetGenericArguments()));
            }

            return Expression.ElementInit(newMethod, ReplaceAll(node.Arguments));
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Convert:
                    return Expression.Convert(Visit(node.Operand), ReplaceType(node.Type));

                case ExpressionType.ConvertChecked:
                    return Expression.ConvertChecked(Visit(node.Operand), ReplaceType(node.Type));

                case ExpressionType.Unbox:
                    return Expression.Unbox(Visit(node.Operand), ReplaceType(node.Type));

                case ExpressionType.TypeAs:
                    return Expression.TypeAs(Visit(node.Operand), ReplaceType(node.Type));

                case ExpressionType.TypeIs:
                    return Expression.TypeIs(Visit(node.Operand), ReplaceType(node.Type));

                default:
                    return base.VisitUnary(node);
            }
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node) =>
            Expression.TypeIs(Visit(node.Expression), ReplaceType(node.TypeOperand));

        protected override Expression VisitConditional(ConditionalExpression node) =>
            Expression.Condition(Visit(node.Test), Visit(node.IfTrue), Visit(node.IfFalse), ReplaceType(node.Type));

        [MethodImpl((MethodImplOptions)0x200)]
        private bool Match(IList<ParameterInfo> baseParams, IList<ParameterInfo> newParams)
        {
            if (baseParams.Count != newParams.Count)
            {
                return false;
            }

            for (var i = 0; i < baseParams.Count; i++)
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

        [MethodImpl((MethodImplOptions)0x200)]
        private Type[] ReplaceTypes(Type[] types)
        {
            for (var i = 0; i < types.Length; i++)
            {
                types[i] = ReplaceType(types[i]);
            }

            return types;
        }

        [MethodImpl((MethodImplOptions)0x200)]
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

        [MethodImpl((MethodImplOptions)0x100)]
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

    internal sealed class TypeToStringConverter : IConverter<Type, Type, string>
    {
        public static readonly IConverter<Type, Type, string> Shared = new TypeToStringConverter();
        private static readonly IDictionary<Type, string> PrimitiveTypes = new Dictionary<Type, string>
        {
            {typeof(byte), "byte"},
            {typeof(sbyte), "sbyte"},
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(long), "long"},
            {typeof(ulong), "ulong"},
            {typeof(float), "float"},
            {typeof(double), "double"},
            {typeof(char), "char"},
            {typeof(object), "object"},
            {typeof(string), "string"},
            {typeof(decimal), "decimal"}
        };

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

#pragma warning disable CS1658 // Warning is overriding an error
#pragma warning restore nullable
#pragma warning restore CS1658 // Warning is overriding an error

// ReSharper restore All