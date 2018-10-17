
/*
IoC Container

https://github.com/DevTeam/IoCContainer

Important note: do not use any internal classes, structures, enums, interfaces, methods, fields or properties
because it may be changed even in minor updates of package.

MIT License

Copyright (c) 2018 Nikolay Pianikov

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
        [NotNull] private readonly Subject<ContainerEvent> _eventSubject = new Subject<ContainerEvent>();
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [NotNull] private Table<FullKey, DependencyEntry> _dependencies = Table<FullKey, DependencyEntry>.Empty;
        [NotNull] private Table<ShortKey, DependencyEntry> _dependenciesForTagAny = Table<ShortKey, DependencyEntry>.Empty;
        [NotNull] internal volatile Table<FullKey, ResolverDelegate> Resolvers = Table<FullKey, ResolverDelegate>.Empty;
        [NotNull] internal volatile Table<ShortKey, ResolverDelegate> ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
        private RegistrationTracker _registrationTracker;

        /// <summary>
        /// Creates a root container with default features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The root container.</returns>
        [NotNull]
        public static Container Create([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return Create(name, DefaultRootContainer.Value);
        }

        /// <summary>
        /// Creates a root container with minimal set of features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The root container.</returns>
        [NotNull]
        public static Container CreateCore([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return Create(name, CoreRootContainer.Value);
        }

        /// <summary>
        /// Creates a root container with minimalist default features.
        /// </summary>
        /// <param name="name">The optional name of the container.</param>
        /// <returns>The root container.</returns>
        [NotNull]
        public static Container CreateLight([NotNull] string name = "")
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return Create(name, LightRootContainer.Value);
        }

        [NotNull]
        private static Container Create([NotNull] string name, [NotNull] IContainer parentContainer)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (parentContainer == null) throw new ArgumentNullException(nameof(parentContainer));
            return new Container(CreateContainerName(name), parentContainer);
        }

        private static Container CreateRootContainer([NotNull][ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            var container = new Container(string.Empty, NullContainer.Shared);
            container.ApplyConfigurations(configurations);
            return container;
        }

        internal Container([NotNull] string name, [NotNull] IContainer parent)
        {
            _name = $"{parent}/{name ?? throw new ArgumentNullException(nameof(name))}";
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _registrationTracker = new RegistrationTracker(this);

            // Subscribe to events from the parent container
            RegisterResource(_parent.Subscribe(_eventSubject));

            // Creates a subscription to track infrastructure registrations
            RegisterResource(_eventSubject.Subscribe(_registrationTracker));

            // Register the current container in the parent container
            _parent.RegisterResource(this);

            // Notifies about existing registrations in parent containers
            _eventSubject.OnNext(new ContainerEvent(_parent, EventType.DependencyRegistration, _parent.SelectMany(i => i)));
        }

        /// <inheritdoc />
        public IContainer Parent => _parent;

        private IIssueResolver IssueResolver => this.Resolve<IIssueResolver>();

        /// <inheritdoc />
        public override string ToString()
        {
            return _name;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public bool TryRegisterDependency(IEnumerable<FullKey> keys, IDependency dependency, ILifetime lifetime, out IDisposable dependencyToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            var isRegistered = true;
            var registeredKeys = new List<FullKey>();
            var dependencyEntry = new DependencyEntry(dependency, lifetime, Disposable.Create(UnregisterKeys), registeredKeys);

            void UnregisterKeys()
            {
                lock (_eventSubject)
                {
                    foreach (var curKey in registeredKeys)
                    {
                        if (curKey.Tag == AnyTag)
                        {
                            TryUnregister(curKey, curKey.Type, ref _dependenciesForTagAny);
                        }
                        else
                        {
                            TryUnregister(curKey, curKey, ref _dependencies);
                        }
                    }

                    _eventSubject.OnNext(new ContainerEvent(this, EventType.DependencyUnregistration, registeredKeys));
                }
            }

            try
            {
                var dependenciesForTagAny = _dependenciesForTagAny;
                var dependencies = _dependencies;

                lock (_eventSubject)
                {
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
                        _eventSubject.OnNext(new ContainerEvent(this, EventType.DependencyRegistration, registeredKeys));
                    }
                }
            }
            catch (Exception)
            {
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
                    dependencyToken = default(IDisposable);
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
            lock (_eventSubject)
            {
                hasDependency = TryGetDependency(key, hashCode, out var dependencyEntry);
                if (hasDependency)
                {
                    // tries creating resolver
                    resolvingContainer = resolvingContainer ?? this;
                    resolvingContainer = dependencyEntry.Lifetime?.SelectResolvingContainer(this, resolvingContainer) ?? resolvingContainer;
                    if (!dependencyEntry.TryCreateResolver(key, resolvingContainer, _registrationTracker.Builders, _registrationTracker.AutowiringStrategy, out var resolverDelegate, out error))
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
                lock (_eventSubject)
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
            _parent.UnregisterResource(this);
            var entriesToDispose = new List<IDisposable>(_dependencies.Count + _dependenciesForTagAny.Count + _resources.Count);
            lock (_eventSubject)
            {
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
            lock (_eventSubject)
            {
                _resources.Add(resource);
            }
        }

        /// <inheritdoc />
        public void UnregisterResource(IDisposable resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));
            lock (_eventSubject)
            {
                _resources.Remove(resource);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<IEnumerable<FullKey>> GetEnumerator()
        {
            return GetAllKeys().Concat(_parent).GetEnumerator();
        }

        /// <inheritdoc />
        public IDisposable Subscribe(IObserver<ContainerEvent> observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));
            return _eventSubject.Subscribe(observer);
        }
        
        internal void Reset()
        {
            lock (_eventSubject)
            {
                Resolvers = Table<FullKey, ResolverDelegate>.Empty;
                ResolversByType = Table<ShortKey, ResolverDelegate>.Empty;
            }
        }

        [MethodImpl((MethodImplOptions) 256)]
        private IEnumerable<IEnumerable<FullKey>> GetAllKeys()
        {
            lock (_eventSubject)
            {
                return _dependencies.Select(i => i.Value.Keys).Concat(_dependenciesForTagAny.Select(i => i.Value.Keys)).Distinct();
            }
        }

        [MethodImpl((MethodImplOptions) 256)]
        private bool TryUnregister<TKey>(FullKey originalKey, TKey key, [NotNull] ref Table<TKey, DependencyEntry> entries)
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
        internal static string CreateContainerName([CanBeNull] string name = "")
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return !string.IsNullOrWhiteSpace(name) ? name : Interlocked.Increment(ref _containerId).ToString(CultureInfo.InvariantCulture);
        }

        [MethodImpl((MethodImplOptions) 256)]
        private void ApplyConfigurations(IEnumerable<IConfiguration> configurations)
        {
            _resources.Add(this.Apply(configurations));
        }

        [MethodImpl((MethodImplOptions)256)]
        private bool TryGetDependency(FullKey key, int hashCode, out DependencyEntry dependencyEntry)
        {
            lock (_eventSubject)
            {
                dependencyEntry = _dependencies.Get(hashCode, key);
                if (dependencyEntry != default(DependencyEntry))
                {
                    return true;
                }

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
                    var arrayType = typeof(IArray);
                    var arrayKey = new FullKey(arrayType, key.Tag);
                    // For generic type
                    dependencyEntry = _dependencies.Get(arrayKey.GetHashCode(), arrayKey);
                    if (dependencyEntry != default(DependencyEntry))
                    {
                        return true;
                    }

                    // For generic type and Any tag
                    dependencyEntry = _dependenciesForTagAny.GetByRef(arrayType.GetHashCode(), arrayType);
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

            public ContainerDebugView([NotNull] Container container)
            {
                _container = container ?? throw new ArgumentNullException(nameof(container));
            }

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
        public readonly EventType EventTypeType;

        /// <summary>
        /// The changed keys.
        /// </summary>
        public readonly IEnumerable<Key> Keys;

        internal ContainerEvent([NotNull] IContainer container, EventType eventTypeType, IEnumerable<Key> keys)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            EventTypeType = eventTypeType;
            Keys = keys;
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
        public readonly T It;

        internal Context(
            T it,
            Key key,
            [NotNull] IContainer container,
            [NotNull] [ItemCanBeNull] params object[] args)
            : base(key, container, args)
        {
            It = it;
        }
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
        /// The dependency was registered.
        /// </summary>
        DependencyRegistration,

        /// <summary>
        /// The dependency was unregistered.
        /// </summary>
        DependencyUnregistration,
    }
}

#endregion
#region Fluent

namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Extension method for IoC container.
    /// </summary>
    [PublicAPI]
    public static class Fluent
    {
        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="parent">The parent container.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer CreateChild([NotNull] this IContainer parent, [NotNull] string name = "")
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return parent.GetResolver<IContainer>(WellknownContainers.NewChild.AsTag())(parent, name);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        internal static IIssueResolver GetIssueResolver([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Resolve<IIssueResolver>();
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
            var methodInfo = Injections.InjectWithTagMethodInfo.MakeGenericMethod(dependencyType);
            var containerExpression = Expression.Field(Expression.Constant(null, TypeDescriptor<Context>.Type), nameof(Context.Container));
            var parameterExpression = Expression.Call(methodInfo, containerExpression, Expression.Constant(dependencyTag));
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

    /// <summary>
    /// Represents extensions to add bindings to the container.
    /// </summary>
    [PublicAPI]
    public static class FluentBind
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
        /// Binds the type.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T>([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type);
        }

        /// <summary>
        /// Binds multiple types.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <typeparam name="T1">The contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1>([NotNull] this IContainer container)
            where T : T1
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type);
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
            where T : T1, T2
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
        /// <typeparam name="T3">The contract type #3.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3>([NotNull] this IContainer container)
            where T : T1, T2, T3
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
        /// <typeparam name="T4">The contract type #4.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4
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
        /// <typeparam name="T5">The contract type #5.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5
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
        /// <typeparam name="T6">The contract type #6.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5, T6
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
        /// <typeparam name="T7">The contract type #7.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The binding token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IBinding<T> Bind<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IContainer container)
            where T : T1, T2, T3, T4, T5, T6, T7
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new Binding<T>(container, TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type);
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
        /// Creates full auto-wiring.
        /// </summary>
        /// <param name="binding">The binding token.</param>
        /// <param name="type">The instance type.</param>
        /// <param name="autowiringStrategy">The optional autowiring strategy.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable To([NotNull] this IBinding<object> binding, [NotNull] Type type, [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new DependencyToken(binding.Container, CreateDependency(binding, new FullAutowiringDependency(type, autowiringStrategy)));
        }

        /// <summary>
        /// Creates full auto-wiring.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="autowiringStrategy">The optional autowiring strategy.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable To<T>([NotNull] this IBinding<T> binding, [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            return new DependencyToken(binding.Container, CreateDependency(binding, new FullAutowiringDependency(TypeDescriptor<T>.Type, autowiringStrategy)));
        }

        /// <summary>
        /// Creates manual auto-wiring.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="binding">The binding token.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable To<T>(
            [NotNull] this IBinding<T> binding,
            [NotNull] Expression<Func<Context, T>> factory,
            [NotNull][ItemNotNull] params Expression<Action<Context<T>>>[] statements)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            // ReSharper disable once CoVariantArrayConversion
            return new DependencyToken(binding.Container, CreateDependency(binding, new AutowiringDependency(factory, statements)));
        }

        /// <summary>
        /// Puts the dependency token into the target container to manage it.
        /// </summary>
        /// <param name="dependencyToken"></param>
        [MethodImpl((MethodImplOptions)256)]
        public static IContainer ToSelf([NotNull] this IDisposable dependencyToken)
        {
            if (dependencyToken == null) throw new ArgumentNullException(nameof(dependencyToken));
            if (dependencyToken is DependencyToken token)
            {
                token.Container.RegisterResource(dependencyToken);
                return token.Container;
            }

            throw new NotSupportedException();
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
                : binding.Container.GetIssueResolver().CannotRegister(binding.Container, keys.ToArray());
        }
    }
}

#endregion
#region FluentBuildUp

namespace IoC
{
    using System;

    /// <summary>
    /// Represents extensions to build up from the container.
    /// </summary>
    [PublicAPI]
    public static class FluentBuildUp
    {
        /// <summary>
        /// Buildups an instance.
        /// Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        public static T BuildUp<T>(this IContainer container, [NotNull] [ItemCanBeNull] params object[] args)
        {
            if (container.TryGetResolver<T>(typeof(T), null, out var resolver, out _, container))
            {
                return resolver(container, args);
            }

            var buildId = Guid.NewGuid();
            using(container.Bind<T>().Tag(buildId).To())
            {
                return container.Resolve<T>(buildId.AsTag(), args);
            }
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
    using Core;

    /// <summary>
    /// Represents extensions to configure a container.
    /// </summary>
    [PublicAPI]
    public static class FluentConfiguration
    {
        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The dependency token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.ApplyData(configurationText);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.ApplyData(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.ApplyData(configurationReaders);
        }

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.UsingData(configurationText);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.UsingData(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.UsingData(configurationReaders);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            return Disposable.Create(configurations.Select(i => i.Apply(container)).SelectMany(i => i));
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            return container.Apply((IEnumerable<IConfiguration>) configurations);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The target container.</returns>
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
        /// Applies configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using<T>([NotNull] this IContainer container)
            where T : IConfiguration, new()
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Using(new T());
        }

        [NotNull]
        private static IDisposable ApplyData<T>([NotNull] this IContainer container, [NotNull] [ItemNotNull] params T[] configurationData)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationData == null) throw new ArgumentNullException(nameof(configurationData));
            if (configurationData.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationData));
            return container.Apply(configurationData.Select(configurationItem => container.Resolve<IConfiguration>(configurationItem)).ToArray());
        }

        [NotNull]
        private static IContainer UsingData<T>([NotNull] this IContainer container, [NotNull] [ItemNotNull] params T[] configurationData)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationData == null) throw new ArgumentNullException(nameof(configurationData));
            if (configurationData.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationData));
            return container.Using(configurationData.Select(configurationItem => container.Resolve<IConfiguration>(configurationItem)).ToArray());
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
            => container.TryGetResolver<T>(type, tag.Value, out var resolver, out var error) ? resolver : container.GetIssueResolver().CannotGetResolver<T>(container, new Key(type, tag), error);

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
            => container.TryGetResolver<T>(type, null, out var resolver, out var error) ? resolver : container.GetIssueResolver().CannotGetResolver<T>(container, new Key(type), error);

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
#region GenericTypeArgumentAttribute

namespace IoC
{
    using System;

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI, AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GenericTypeArgumentAttribute : Attribute
    {
    }
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
    }
}


#endregion
#region IBuildContext

namespace IoC
{
    using System;
    using System.Collections.Generic;
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
        [NotNull] Expression PrepareTypes([NotNull] Expression baseExpression);

        /// <summary>
        /// Prepares base expression, injecting dependencies. 
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <param name="instanceExpression">The instance expression.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression MakeInjections([NotNull] Expression baseExpression, [CanBeNull] ParameterExpression instanceExpression = null);

        /// <summary>
        /// Wraps by lifetime.
        /// </summary>
        /// <param name="baseExpression">The base expression.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <returns></returns>
        [NotNull] Expression AppendLifetime([NotNull] Expression baseExpression, [CanBeNull] ILifetime lifetime);

        /// <summary>
        /// Appends value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The value type.</param>
        /// <returns>The parameter expression.</returns>
        [NotNull] Expression AppendValue([CanBeNull] object value, [NotNull] Type type);

        /// <summary>
        /// Appends value.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The parameter expression.</returns>
        [NotNull] Expression AppendValue<T>([CanBeNull] T value);

        /// <summary>
        /// Appends variable.
        /// </summary>
        /// <param name="expression">The value expression.</param>
        /// <returns>The parameter expression.</returns>
        [NotNull] ParameterExpression AppendVariable([NotNull] Expression expression);

        /// <summary>
        /// Closes block for specified variables.
        /// </summary>
        /// <param name="targetExpression">The target expression.</param>
        /// <param name="variableExpressions">Variable expressions.</param>
        /// <returns>The resulting block expression.</returns>
        [NotNull] Expression CloseBlock([NotNull] Expression targetExpression, [NotNull][ItemNotNull] params ParameterExpression[] variableExpressions);
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
        /// <param name="bodyExpression">The expression body to get an instance.</param>
        /// <param name="buildContext">The build context.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([NotNull] Expression bodyExpression, [NotNull] IBuildContext buildContext);
    }
}


#endregion
#region IConfiguration

namespace IoC
{
    using System;
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
        [NotNull][ItemNotNull] IEnumerable<IDisposable> Apply([NotNull] IContainer container);
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
        bool TryRegisterDependency([NotNull] IEnumerable<Key> keys, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out IDisposable dependencyToken);

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
#region IIssueResolver

namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Allows to specify behaviour for cases with issue.
    /// </summary>
    [PublicAPI]
    public interface IIssueResolver
    {
        /// <summary>
        /// Handles the scenario when binding cannot be registered.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="keys">The set of binding keys.</param>
        /// <returns>The dependency token.</returns>
        [NotNull] IDisposable CannotRegister([NotNull] IContainer container, [NotNull] Key[] keys);

        /// <summary>
        /// Handles the scenario when the dependency cannot be resolved.
        /// </summary>
        /// <param name="container">The resolving container.</param>
        /// <param name="key">The resolving key.</param>
        /// <returns>The pair of the dependency and of the lifetime.</returns>
        [NotNull] Tuple<IDependency, ILifetime> CannotResolveDependency([NotNull] IContainer container, Key key);

        /// <summary>
        /// Handles the scenario when cannot get a resolver.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="key">The resolving key.</param>
        /// <param name="error">The error.</param>
        /// <returns>The resolver.</returns>
        [NotNull] Resolver<T> CannotGetResolver<T>([NotNull] IContainer container, Key key, [NotNull] Exception error);

        /// <summary>
        /// Handles the scenario when cannot extract generic type arguments.
        /// </summary>
        /// <param name="type">The instance type.</param>
        /// <returns>The extracted generic type arguments.</returns>
        [NotNull][ItemNotNull] Type[] CannotGetGenericTypeArguments([NotNull] Type type);

        /// <summary>
        /// Handles the scenario when a cyclic dependence was detected.
        /// </summary>
        /// <param name="key">The resolving key.</param>
        /// <param name="reentrancy">The level of reentrancy.</param>
        void CyclicDependenceDetected(Key key, int reentrancy);

        /// <summary>
        /// Handles the scenario when cannot parse a type from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a type metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="typeName">The text with a type metadata.</param>
        /// <returns></returns>
        [NotNull] Type CannotParseType([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string typeName);

        /// <summary>
        /// Handles the scenario when cannot parse a lifetime from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a lifetime metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="lifetimeName">The text with a lifetime metadata.</param>
        /// <returns></returns>
        Lifetime CannotParseLifetime([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string lifetimeName);

        /// <summary>
        /// Handles the scenario when cannot parse a tag from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a tag metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="tag">The text with a tag metadata.</param>
        /// <returns></returns>
        [CanBeNull] object CannotParseTag(string statementText, int statementLineNumber, int statementPosition, [NotNull] string tag);

        /// <summary>
        /// Handles the scenario when cannot build expression.
        /// </summary>
        /// <param name="buildContext">The build context.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression CannotBuildExpression([NotNull] IBuildContext buildContext, [NotNull] IDependency dependency, ILifetime lifetime = null);

        /// <summary>
        /// Handles the scenario when cannot resolve a constructor.
        /// </summary>
        /// <param name="constructors">Available constructors.</param>
        /// <returns>The constructor.</returns>
        [NotNull] IMethod<ConstructorInfo> CannotResolveConstructor([NotNull] IEnumerable<IMethod<ConstructorInfo>> constructors);

        /// <summary>
        /// Handles the scenario when cannot resolve the instance type.
        /// </summary>
        /// <param name="registeredType">Registered type.</param>
        /// <param name="resolvingType">Resolving type.</param>
        /// <returns>The type to create an instance.</returns>
        Type CannotResolveType([NotNull] Type registeredType, [NotNull] Type resolvingType);
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
        [NotNull][ItemNotNull] IEnumerable<Expression> GetParametersExpressions();

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
        internal const string JustAMarkerError = "Just a marker. Should be used to configure dependency injection.";
        [NotNull] internal static readonly MethodInfo InjectMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectWithTagMethodInfo;
        [NotNull] internal static readonly MethodInfo InjectingAssignmentMethodInfo;

        static Injections()
        {
            Expression<Func<object>> injectExpression = () => default(IContainer).Inject<object>();
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method.GetGenericMethodDefinition();
            Expression<Func<object>> injectWithTagExpression = () => default(IContainer).Inject<object>(null);
            InjectWithTagMethodInfo = ((MethodCallExpression)injectWithTagExpression.Body).Method.GetGenericMethodDefinition();
            Expression<Action<object, object>> assignmentCallExpression = (item1, item2) => default(IContainer).Inject<object>(null, null);
            InjectingAssignmentMethodInfo = ((MethodCallExpression)assignmentCallExpression.Body).Method.GetGenericMethodDefinition();
        }

        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>(this IContainer container)
        {
            throw new NotImplementedException(JustAMarkerError);
        }

        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <typeparam name="T">The type of dependency.</typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="tag">The tag of dependency.</param>
        /// <returns>The injected instance.</returns>
        public static T Inject<T>(this IContainer container, [CanBeNull] object tag)
        {
            throw new NotImplementedException(JustAMarkerError);
        }

        /// <summary>
        /// Injects the dependency. Just a marker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The resolving container.</param>
        /// <param name="destination">The destination member for injection.</param>
        /// <param name="source">The source of injection.</param>
        public static void Inject<T>(this IContainer container, [NotNull] T destination, [CanBeNull] T source)
        {
            throw new NotImplementedException(JustAMarkerError);
        }
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
#region Key

namespace IoC
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents the container key.
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
        public override string ToString() => $"[Type = {Type.FullName}, Tag = {Tag ?? "empty"}, HashCode = {GetHashCode()}]";

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public override bool Equals(object obj)
        {
            // ReSharper disable once PossibleNullReferenceException
            return Equals((Key)obj);
        }

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public bool Equals(Key other)
        {
            return ReferenceEquals(Type, other.Type) && (ReferenceEquals(Tag, other.Tag) || Equals(Tag, other.Tag));
        }

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public override int GetHashCode()
        {
            unchecked
            {
                return (Tag != null ? Tag.GetHashCode() * 397 : 0) ^ Type.GetHashCode();
            }
        }

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
        /// Default lifetime. New instance each time (default).
        /// </summary>
        Transient = 1,

        /// <summary>
        /// Single instance per dependency
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
#region Scope

namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    /// <summary>
    /// Represents the scope which could be used with <c>Lifetime.ScopeSingleton</c>
    /// </summary>
    [PublicAPI]
    public sealed class Scope: IDisposable
    {
        [NotNull] private static readonly Scope Default = new Scope(DefaultScopeKey.Shared);
        [CanBeNull] [ThreadStatic] private static Scope _current;
        [NotNull] internal readonly object ScopeKey;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [CanBeNull] private Scope _prevScope;

        /// <summary>
        /// The current scope.
        /// </summary>
        [NotNull]
        public static Scope Current => _current ?? Default;

        /// <summary>
        /// Creates the instance of a new scope.
        /// </summary>
        /// <param name="scopeKey">The key of scope.</param>
        public Scope([NotNull] object scopeKey) => ScopeKey = scopeKey ?? throw new ArgumentNullException(nameof(scopeKey));

        /// <summary>
        /// Begins scope.
        /// </summary>
        /// <returns>The scope token to end the scope.</returns>
        public IDisposable Begin()
        {
            _prevScope = Current;
            _current = this;
            return Disposable.Create(() => { _current = _prevScope ?? throw new NotSupportedException(); });
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (var resource in _resources.ToList())
            {
                resource.Dispose();
            }
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return ScopeKey.Equals(((Scope) obj).ScopeKey);
        }

        /// <inheritdoc />
        public override int GetHashCode() => ScopeKey.GetHashCode();

        internal void AddResource(IDisposable resource) => _resources.Add(resource);

        internal void RemoveResource(IDisposable resource) => _resources.Remove(resource);

        private class DefaultScopeKey
        {
            public static readonly object Shared = new DefaultScopeKey();

            private DefaultScopeKey()
            {
            }

            public override string ToString() => "Default Resolving Scope Key";
        }
    }
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
    /// Represents the enumeration of well-known containers.
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

        /// <summary>
        /// All resolvers parameters.
        /// </summary>
        [NotNull][ItemNotNull]
        public static readonly IEnumerable<ParameterExpression> ResolverParameters = new List<ParameterExpression>{ ContainerParameter, ArgsParameter };
    }
}


#endregion

#endregion

#region Features

#region CollectionFeature

namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
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
        {
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var containerSingletonResolver = container.GetResolver<ILifetime>(Lifetime.ContainerSingleton.AsTag());
            yield return container.Register<IEnumerable<TT>>(ctx => new Enumeration<TT>(ctx.Container, ctx.Args), containerSingletonResolver(container));
            yield return container.Register<List<TT>, IList<TT>, ICollection<TT>>(ctx => ctx.Container.Inject<IEnumerable<TT>>().ToList());
            yield return container.Register(ctx => ctx.Container.Inject<IEnumerable<TT>>().ToArray());
            yield return container.Register<HashSet<TT>, ISet<TT>>(ctx => new HashSet<TT>(ctx.Container.Inject<IEnumerable<TT>>()));
            yield return container.Register<IObservable<TT>>(ctx => new Observable<TT>(ctx.Container.Inject<IEnumerable<TT>>()), containerSingletonResolver(container));
#if !NET40
            yield return container.Register<ReadOnlyCollection<TT>, IReadOnlyList<TT>, IReadOnlyCollection<TT>>(ctx => new ReadOnlyCollection<TT>(ctx.Container.Inject<List<TT>>()));
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

        internal class Enumeration<T>: IObserver<ContainerEvent>, IDisposable, IEnumerable<T>
        {
            private readonly IContainer _container;
            [NotNull] [ItemCanBeNull] private readonly object[] _args;
            private readonly IDisposable _subscription;
            private volatile Resolver<T>[] _resolvers;

            public Enumeration([NotNull] IContainer container, [NotNull][ItemCanBeNull] params object[] args)
            {
                _container = container;
                _args = args;
                _subscription = container.Subscribe(this);
                Reset();
            }

            public void OnNext(ContainerEvent value) => Reset();

            public void OnError(Exception error)
            {
            }

            public void OnCompleted()
            {
            }

            public void Dispose() => _subscription.Dispose();

            [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
            public IEnumerator<T> GetEnumerator()
            {
                var resolvers = _resolvers;
                if (resolvers == null)
                {
                    lock (_subscription)
                    {
                        if (_resolvers == null)
                        {
                            _resolvers = GetResolvers(_container).ToArray();
                        }

                        resolvers = _resolvers;
                    }
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < resolvers.Length; i++)
                {
                    yield return resolvers[i](_container, _args);
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private void Reset()
            {
                lock (_subscription)
                {
                    _resolvers = null;
                }
            }

            private static IEnumerable<Resolver<T>> GetResolvers(IContainer container)
            {
                var targetType = TypeDescriptorExtensions.Descriptor<T>();
                var isConstructedGenericType = targetType.IsConstructedGenericType();
                TypeDescriptor genericTargetType = null;
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
        public IEnumerable<IDisposable> Apply(IContainer container)
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
            yield return container.Register<IConfiguration>(ctx => CreateTextConfiguration(ctx));
        }

        internal static TextConfiguration CreateTextConfiguration(Context ctx)
        {
            if (ctx.Args.Length != 1)
            {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentOutOfRangeException("Should have single argument.");
            }

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

            return new TextConfiguration(reader, ctx.Container.Resolve<IConverter<IEnumerable<Statement>, BindingContext, BindingContext>>());
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
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => IssueResolver.Shared);
            yield return container.Register(ctx => DefaultAutowiringStrategy.Shared);
            yield return container.Register(ctx => ctx.Container.GetResolver<TT>(ctx.Key.Tag.AsTag()), null, Feature.AnyTag);

            // Lifetimes
            yield return container.Register<ILifetime>(ctx => new SingletonLifetime(), null, new object[] { Lifetime.Singleton });
            yield return container.Register<ILifetime>(ctx => new ContainerSingletonLifetime(), null, new object[] { Lifetime.ContainerSingleton });
            yield return container.Register<ILifetime>(ctx => new ScopeSingletonLifetime(), null, new object[] { Lifetime.ScopeSingleton });

            // Scope
            long scopeId = 0;
            Func<long> createScopeId = () => Interlocked.Increment(ref scopeId);
            yield return container.Register(ctx => new Scope(createScopeId()));

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
                        : Container.CreateContainerName(string.Empty), ctx.Container),
                null,
                new object[] { WellknownContainers.NewChild });
            
            // Parent
            yield return container.Register(ctx => ctx.Container.Parent, null, new object[] { WellknownContainers.Parent });
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
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register<Func<TT>>(ctx => () => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT>>(ctx => arg1 => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT>>(ctx => (arg1, arg2) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT>>(ctx => (arg1, arg2, arg3) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3), null, Feature.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT>>(ctx => (arg1, arg2, arg3, arg4) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4), null, Feature.AnyTag);
            if (!_light)
            {
                yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5), null, Feature.AnyTag);
                yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5, arg6) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5, arg6), null, Feature.AnyTag);
                yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7), null, Feature.AnyTag);
                yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8), null, Feature.AnyTag);
            }
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
        public IEnumerable<IDisposable> Apply(IContainer container)
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
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => TaskScheduler.Current);
            yield return container.Register(ctx => StartTask(new Task<TT>(ctx.Container.Inject<Func<TT>>(ctx.Key.Tag)), ctx.Container.Inject<TaskScheduler>()), null, Feature.AnyTag);
#if NETCOREAPP2_0 || NETCOREAPP2_1
            yield return container.Register(ctx => new ValueTask<TT>(ctx.Container.Inject<TT>(ctx.Key.Tag)), null, Feature.AnyTag);
#endif
        }

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
        public IEnumerable<IDisposable> Apply(IContainer container)
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
        private static readonly MethodInfo GetMethodInfo = typeof(CoreExtensions).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(CoreExtensions.GetByRef)).MakeGenericMethod(typeof(TKey), typeof(object));
        private static readonly MethodInfo SetMethodInfo = Descriptor<Table<TKey, object>>().GetDeclaredMethods().Single(i => i.Name == nameof(Table<TKey, object>.Set));
        private static readonly MethodInfo OnNewInstanceCreatedMethodInfo = Descriptor<KeyBasedLifetime<TKey>>().GetDeclaredMethods().Single(i => i.Name == nameof(OnNewInstanceCreated));
        private static readonly ParameterExpression KeyVar = Expression.Variable(TypeDescriptor<TKey>.Type, "key");

        [NotNull] private object _lockObject = new object();
        private volatile Table<TKey, object> _instances = Table<TKey, object>.Empty;

        /// <inheritdoc />
        public Expression Build(Expression bodyExpression, IBuildContext buildContext)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var returnType = buildContext.Key.Type;
            var thisConst = buildContext.AppendValue(this);
            var instanceVar = Expression.Variable(returnType, "val");
            var instancesField = Expression.Field(thisConst, InstancesFieldInfo);
            var lockObjectConst = buildContext.AppendValue(_lockObject);
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

    /// <summary>
    /// Represents singleton per scope lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class ScopeSingletonLifetime: KeyBasedLifetime<Scope>
    {
        /// <inheritdoc />
        protected override Scope CreateKey(IContainer container, object[] args) => Scope.Current;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ScopeSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Create() => new ScopeSingletonLifetime();

        /// <inheritdoc />
        protected override T OnNewInstanceCreated<T>(T newInstance, Scope scope, IContainer container, object[] args)
        {
            if (newInstance is IDisposable disposable)
            {
                scope.AddResource(disposable);
            }

            return newInstance;
        }

        /// <inheritdoc />
        protected override void OnInstanceReleased(object releasedInstance, Scope scope)
        {
            if (releasedInstance is IDisposable disposable)
            {
                scope.RemoveResource(disposable);
                disposable.Dispose();
            }
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

#pragma warning disable CS0649
        [NotNull] private object _lockObject = new object();
        private volatile object _instance;
#pragma warning restore CS0649

        /// <inheritdoc />
        public Expression Build(Expression expression, IBuildContext buildContext)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));

            var thisConst = buildContext.AppendValue(this);
            var lockObjectConst = buildContext.AppendValue(_lockObject);
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
        }

        /// <inheritdoc />
        public ILifetime Create() => new SingletonLifetime();

        /// <inheritdoc />
        public override string ToString() => Lifetime.Singleton.ToString();
    }
}


#endregion

#endregion

#region Core

#region AutowiringDependency

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    internal class AutowiringDependency: IDependency
    {
        private readonly Expression _expression;
        [NotNull] [ItemNotNull] private readonly Expression[] _statements;
        private readonly bool _isComplexType;

        [SuppressMessage("ReSharper", "ConstantConditionalAccessQualifier")]
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public AutowiringDependency([NotNull] LambdaExpression constructor, [NotNull][ItemNotNull] params LambdaExpression[] statements)
            :this(
                constructor?.Body ?? throw new ArgumentNullException(nameof(constructor)),
                statements?.Select(i => i.Body)?.ToArray() ?? throw new ArgumentNullException(nameof(statements)))
        {
        }

        public AutowiringDependency([NotNull] Expression constructorExpression, [NotNull][ItemNotNull] params Expression[] statementExpressions)
        {
            _expression = constructorExpression ?? throw new ArgumentNullException(nameof(constructorExpression));
            _isComplexType = IsComplexType(_expression.Type);
            _statements = (statementExpressions ?? throw new ArgumentNullException(nameof(statementExpressions))).ToArray();
        }

        public Expression Expression { get; }

        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression baseExpression, out Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            try
            {
                baseExpression = _expression;
                if (_isComplexType)
                {
                    baseExpression = buildContext.PrepareTypes(baseExpression);
                }

                baseExpression = buildContext.MakeInjections(baseExpression);
                if (_statements.Any())
                {
                    baseExpression = Expression.Block(CreateAutowiringStatements(buildContext, baseExpression));
                }

                baseExpression = buildContext.AppendLifetime(baseExpression, lifetime);
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

        private IEnumerable<Expression> CreateAutowiringStatements(
            [NotNull] IBuildContext buildContext,
            [NotNull] Expression newExpression)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            if (newExpression == null) throw new ArgumentNullException(nameof(newExpression));

            var instanceExpression = buildContext.AppendVariable(newExpression);
            foreach (var statement in _statements)
            {
                var baseExpression = statement;
                if (_isComplexType)
                {
                    baseExpression = buildContext.PrepareTypes(baseExpression);
                }

                baseExpression = buildContext.MakeInjections(baseExpression, instanceExpression);
                yield return baseExpression;
            }

            yield return instanceExpression;
        }

        private bool IsComplexType(Type type)
        {
            var typeDescriptor = type.Descriptor();
            return typeDescriptor.IsConstructedGenericType() || typeDescriptor.IsGenericTypeDefinition() || typeDescriptor.IsArray();
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
        public Binding([NotNull] IContainer container, [NotNull][ItemNotNull] params Type[] types)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            Types = types ?? throw new ArgumentNullException(nameof(types));
            Lifetime = null;
            Tags = Enumerable.Empty<object>();
        }

        public Binding([NotNull] IBinding<T> binding, Lifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Types = binding.Types;
            Tags = binding.Tags;
            Lifetime = lifetime != IoC.Lifetime.Transient ? binding.Container.Resolve<ILifetime>(lifetime.AsTag(), binding.Container) : null;
        }

        public Binding([NotNull] IBinding<T> binding, [NotNull] ILifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Types = binding.Types;
            Lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            Tags = binding.Tags;
        }

        public Binding([NotNull] IBinding<T> binding, [CanBeNull] object tagValue)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Types = binding.Types;
            Lifetime = binding.Lifetime;
            Tags = binding.Tags.Concat(Enumerable.Repeat(tagValue, 1));
        }

        public IContainer Container { get; }

        public IEnumerable<Type> Types { get; }

        public IEnumerable<object> Tags { get; }

        public ILifetime Lifetime { get; }
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
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents build context.
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [PublicAPI]
    internal class BuildContext : IBuildContext
    {
        private static readonly ICollection<IBuilder> EmptyBuilders = new List<IBuilder>();
        // Should be at least internal to be accessible from for compiled code from expressions
        private readonly ICollection<IDisposable> _resources;
        [NotNull] private readonly ICollection<IBuilder> _builders;
        private readonly List<ParameterExpression> _parameters = new List<ParameterExpression>();
        private readonly List<Expression> _statements = new List<Expression>();
        private readonly IDictionary<Type, Type> _typesMap = new Dictionary<Type, Type>();
        private int _curId;

        internal BuildContext(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] ICollection<IDisposable> resources,
            [NotNull] ICollection<IBuilder> builders,
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

        public IBuildContext CreateChild(Key key, IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return CreateChildInternal(key, container);
        }

        public Expression AppendValue(object value, Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return Expression.Constant(value, type);
        }

        public Expression AppendValue<T>(T value) => AppendValue(value, TypeDescriptor<T>.Type);

        public ParameterExpression AppendVariable(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            var varExpression = Expression.Variable(expression.Type, "var" + GenerateId());
            _parameters.Add(varExpression);
            _statements.Add(Expression.Assign(varExpression, expression));
            return varExpression;
        }

        public Expression PrepareTypes(Expression baseExpression) =>
            TypeReplacerExpressionBuilder.Shared.Build(baseExpression, this, _typesMap);

        public Expression MakeInjections(Expression baseExpression, ParameterExpression instanceExpression = null) =>
            DependencyInjectionExpressionBuilder.Shared.Build(baseExpression, this, instanceExpression);

        public Expression AppendLifetime(Expression baseExpression, ILifetime lifetime)
        {
            if (_builders.Count > 0)
            {
                var buildContext = CreateChildInternal(Key, Container, forBuilders: true);
                foreach (var builder in _builders)
                {
                    baseExpression = baseExpression.Convert(Key.Type);
                    baseExpression = builder.Build(baseExpression, buildContext);
                }
            }

            baseExpression = baseExpression.Convert(Key.Type);
            baseExpression = LifetimeExpressionBuilder.Shared.Build(baseExpression, this, lifetime);
            return CloseBlock(baseExpression);
        }

        public Expression CloseBlock(Expression targetExpression, params ParameterExpression[] variableExpressions)
        {
            if (targetExpression == null) throw new ArgumentNullException(nameof(targetExpression));
            if (variableExpressions == null) throw new ArgumentNullException(nameof(variableExpressions));
            var statements = (
                from binaryExpression in _statements.OfType<BinaryExpression>()
                join parameterExpression in variableExpressions on binaryExpression.Left equals parameterExpression
                select (Expression)binaryExpression).ToList();

            var parameterExpressions = variableExpressions.OfType<ParameterExpression>();
            if (!statements.Any() && !parameterExpressions.Any())
            {
                return targetExpression;
            }

            statements.Add(targetExpression);
            return Expression.Block(variableExpressions, statements);
        }

        [NotNull]
        private Expression CloseBlock([NotNull] Expression targetExpression)
        {
            if (targetExpression == null) throw new ArgumentNullException(nameof(targetExpression));
            if (!_parameters.Any() && !_statements.Any())
            {
                return targetExpression;
            }

            _statements.Add(targetExpression);
            return Expression.Block(_parameters, _statements);
        }

        public IBuildContext CreateChildInternal(Key key, IContainer container, bool forBuilders = false)
        {
            var child = new BuildContext(key, container, _resources, forBuilders ? EmptyBuilders : _builders, AutowiringStrategy, Depth + 1);
            child._parameters.AddRange(_parameters);
            child._statements.AddRange(_statements);
            return child;
        }

        [MethodImpl((MethodImplOptions)256)]
        private int GenerateId() => System.Threading.Interlocked.Increment(ref _curId);
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
        {
        }
    }
}


#endregion
#region Cache

namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal class Cache<TKey, TValue>
        where TKey: class
    {
        [NotNull] private readonly object _lockObject = new object();
        [NotNull] private Table<TKey, TValue> _table = Table<TKey, TValue>.Empty;

        [MethodImpl((MethodImplOptions)256)]
        public TValue GetOrCreate(TKey key, Func<TValue> factory)
        {
            var hashCode = key.GetHashCode();
            lock (_lockObject)
            {
                var value = _table.GetByRef(hashCode, key);
                if (Equals(value, default(TValue)))
                {
                    value = factory();
                    _table = _table.Set(hashCode, key, value);
                }

                return value;
            }
        }
    }
}


#endregion
#region CoreExtensions

namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class CoreExtensions
    {
        [MethodImpl((MethodImplOptions)256)]
        public static bool SequenceEqual<T>([NotNull] this T[] array1, [NotNull] T[] array2)
        {
            return ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }

        [MethodImpl((MethodImplOptions)256)]
        public static int GetHash<T>([NotNull][ItemNotNull] this T[] items)
        {
            unchecked
            {
                var code = 0;
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < items.Length; i++)
                {
                    code = (code * 397) ^ items[i].GetHashCode();
                }

                return code;
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        public static T[] EmptyArray<T>()
        {
            return Empty<T>.Array;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static T[] CreateArray<T>(int size = 0, T value = default(T))
        {
            if (size == 0)
            {
                return EmptyArray<T>();
            }

            var array = new T[size];
#if NETCOREAPP2_0 || NETCOREAPP2_1
            Array.Fill(array, value);
#else
            for (var i = 0; i < size; i++)
            {
                array[i] = value;
            }
#endif
            return array;
        }

        [MethodImpl((MethodImplOptions) 256)]
        [Pure]
        [NotNull]
        public static T[] Add<T>([NotNull] this T[] previous, [CanBeNull] T value)
        {
            var length = previous.Length;
            var result = new T[length + 1];
            Array.Copy(previous, result, length);
            result[length] = value;
            return result;
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
        {
        }

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
            return method.IsPublic ? order : order << 10;
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

    internal sealed class DependencyEntry : IDisposable
    {
        [NotNull] internal readonly IDependency Dependency;
        [CanBeNull] public readonly ILifetime Lifetime;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [NotNull] public readonly IList<Key> Keys;
        private readonly object _lockObject = new object();
        private readonly Dictionary<LifetimeKey, ILifetime> _lifetimes = new Dictionary<LifetimeKey, ILifetime>();
        private bool _disposed;

        public DependencyEntry(
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource,
            [NotNull] IList<Key> keys)
        {
            Dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
            Lifetime = lifetime;
            _resources.Add(resource ?? throw new ArgumentNullException(nameof(resource)));
            Keys = keys ?? throw new ArgumentNullException(nameof(keys));
            if (lifetime is IDisposable disposableLifetime)
            {
                _resources.Add(disposableLifetime);
            }
        }

        public bool TryCreateResolver(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] ICollection<IBuilder> builders,
            [NotNull] IAutowiringStrategy defaultAutowiringStrategy,
            out Delegate resolver,
            out Exception error)
        {
            var typeDescriptor = key.Type.Descriptor();
            var buildContext = new BuildContext(key, resolvingContainer, _resources, builders, defaultAutowiringStrategy);
            if (!Dependency.TryBuildExpression(buildContext, GetLifetime(typeDescriptor), out var expression, out error))
            {
                resolver = default(Delegate);
                return false;
            }

            var resolverExpression = Expression.Lambda(buildContext.Key.Type.ToResolverType(), expression, false, WellknownExpressions.ResolverParameters);
            resolver = ExpressionCompiler.Shared.Compile(resolverExpression);
            error = default(Exception);
            return true;
        }

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public ILifetime GetLifetime([NotNull] Type type)
        {
            return GetLifetime(type.Descriptor());
        }

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        private ILifetime GetLifetime(TypeDescriptor typeDescriptor)
        {
            if (Lifetime == null)
            {
                return null;
            }
            
            if (!typeDescriptor.IsConstructedGenericType())
            {
                return Lifetime;
            }

            var lifetimeKey = new LifetimeKey(typeDescriptor.GetGenericTypeArguments());
            ILifetime lifetime;
            lock (_lockObject)
            {
                if (!_lifetimes.TryGetValue(lifetimeKey, out lifetime))
                {
                    lifetime = Lifetime.Create();
                    _lifetimes.Add(lifetimeKey, lifetime);
                }
            }

            return lifetime;
        }

        public void Dispose()
        {
            lock(_lockObject)
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
            
            foreach (var lifetime in _lifetimes.Values)
            {
                lifetime.Dispose();
            }
        }

        public override string ToString()
            => $"{string.Join(", ", Keys.Select(i => i.ToString()))} as {Lifetime?.ToString() ?? IoC.Lifetime.Transient.ToString()}";

        private struct LifetimeKey
        {
            private readonly Type[] _genericTypes;

            public LifetimeKey(Type[] genericTypes) => _genericTypes = genericTypes;

            public override bool Equals(object obj) => obj is LifetimeKey key && Equals(key);

            public override int GetHashCode() => _genericTypes != null ? _genericTypes.GetHash() : 0;

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
        {
        }

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

    internal class DependencyInjectionExpressionVisitor: ExpressionVisitor
    {
        private static readonly Key ContextKey = TypeDescriptor<Context>.Key;
        [NotNull] private static readonly TypeDescriptor ContextTypeDescriptor = TypeDescriptor<Context>.Descriptor;
        [NotNull] private static readonly TypeDescriptor GenericContextTypeDescriptor = typeof(Context<>).Descriptor();
        [NotNull] private static readonly ConstructorInfo ContextConstructor;
        [NotNull] private readonly Stack<Key> _keys = new Stack<Key>();
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
            _keys.Push(buildContext.Key);
            _container = buildContext.Container;
            _buildContext = buildContext;
            _thisExpression = thisExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression methodCall)
        {
            if (methodCall.Method.IsGenericMethod)
            {
                // container.Inject<T>()
                if (Equals(methodCall.Method.GetGenericMethodDefinition(), Injections.InjectMethodInfo))
                {
                    var type = methodCall.Method.GetGenericArguments()[0];
                    var containerExpression = Visit(methodCall.Arguments[0]);
                    var key = new Key(type);
                    return CreateDependencyExpression(key, containerExpression);
                }

                // container.Inject<T>(tag)
                if (Equals(methodCall.Method.GetGenericMethodDefinition(), Injections.InjectWithTagMethodInfo))
                {
                    var type = methodCall.Method.GetGenericArguments()[0];
                    var containerExpression = Visit(methodCall.Arguments[0]);
                    var tagExpression = methodCall.Arguments[1];
                    var tag = GetTag(tagExpression);
                    var key = new Key(type, tag);
                    return CreateDependencyExpression(key, containerExpression);
                }

                // container.Inject<T>(destination, source)
                if (Equals(methodCall.Method.GetGenericMethodDefinition(), Injections.InjectingAssignmentMethodInfo))
                {
                    var dstExpression = Visit(methodCall.Arguments[1]);
                    var srcExpression = Visit(methodCall.Arguments[2]);
                    if (dstExpression != null && srcExpression != null)
                    {
                        return Expression.Assign(dstExpression, srcExpression);
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
                    Expression.Constant(_keys.Peek()),
                    ContainerParameter,
                    ArgsParameter);
            }

            return base.VisitParameter(node);
        }

        [NotNull]
        private Expression CreateNewContextExpression()
        {
            return Expression.New(
                ContextConstructor,
                Expression.Constant(_keys.Peek()),
                ContainerParameter,
                ArgsParameter);
        }

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
        private IEnumerable<Expression> InjectAll(IEnumerable<Expression> expressions)
        {
            return expressions.Select(Visit);
        }

        [NotNull]
        private IContainer SelectedContainer([CanBeNull] Expression containerExpression)
        {
            if (containerExpression != null)
            {
                if (containerExpression is ParameterExpression parameterExpression && parameterExpression.Type == TypeDescriptor<IContainer>.Type)
                {
                    return _container;
                }

                var containerSelectorExpression = Expression.Lambda<ContainerSelector>(containerExpression, true, ContainerParameter);
                var selectContainer = containerSelectorExpression.Compile();
                return selectContainer(_container);
            }

            return _container;
        }

        [NotNull]
        private Expression CreateDependencyExpression(Key key, [CanBeNull] Expression containerExpression)
        {
            if (Equals(key, ContextKey))
            {
                return CreateNewContextExpression();
            }

            var selectedContainer = SelectedContainer(containerExpression);
            if (!selectedContainer.TryGetDependency(key, out var dependency, out var lifetime))
            {
                try
                {
                    var dependencyInfo = _container.Resolve<IIssueResolver>().CannotResolveDependency(selectedContainer, key);
                    dependency = dependencyInfo.Item1;
                    lifetime = dependencyInfo.Item2;
                }
                catch (Exception ex)
                {
                    throw new BuildExpressionException(ex.Message, ex.InnerException);
                }
            }

            _keys.Push(key);
            try
            {
                var childBuildContext = _buildContext.CreateChild(key, selectedContainer);
                if (childBuildContext.Depth >= 64)
                {
                    _container.Resolve<IIssueResolver>().CyclicDependenceDetected(key, childBuildContext.Depth);
                }

                if (dependency.TryBuildExpression(childBuildContext, lifetime, out var expression, out var error))
                {
                    return expression;
                }
                else
                {
                    try
                    {
                        return _container.Resolve<IIssueResolver>().CannotBuildExpression(childBuildContext, dependency, lifetime);
                    }
                    catch (Exception)
                    {
                        throw error;
                    }
                }
            }
            finally
            {
                _keys.Pop();
            }
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
                    expression = Expression.Constant(_keys.Peek());
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
#region DependencyToken

namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal struct DependencyToken: IDisposable
    {
        [NotNull] internal readonly IContainer Container;
        [NotNull] private readonly IDisposable _dependencyToken;

        public DependencyToken([NotNull] IContainer container, [NotNull] IDisposable dependencyToken)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _dependencyToken = dependencyToken ?? throw new ArgumentNullException(nameof(dependencyToken));
        }

        [MethodImpl((MethodImplOptions)256)]
        public void Dispose() => _dependencyToken.Dispose();
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

        private sealed class DisposableAction : IDisposable
        {
            [NotNull] private readonly Action _action;
            private volatile object _lockObject = new object();

            public DisposableAction([NotNull] Action action)
            {
                _action = action;
            }

            public void Dispose()
            {
                var lockObject = _lockObject;
                if (lockObject == null)
                {
                    return;
                }
                
                lock (lockObject)
                {
                    if (_lockObject == null)
                    {
                        return;
                    }

                    _lockObject = null;
                }

                _action();
            }
        }

        private sealed class CompositeDisposable : IDisposable
        {
            private IDisposable[] _disposables;
            
            public CompositeDisposable(IEnumerable<IDisposable> disposables)
                => _disposables = disposables.ToArray();

            public void Dispose()
            {
                var disposables = _disposables;
                if (disposables == null)
                {
                    return;
                }

                lock (disposables)
                {
                    if (_disposables == null)
                    {
                        return;
                    }

                    _disposables = null;
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var index = 0; index < disposables.Length; index++)
                {
                    disposables[index]?.Dispose();
                }
            }
        }

        private class EmptyDisposable: IDisposable
        {
            [NotNull]
            public static readonly IDisposable Shared = new EmptyDisposable();

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
        public static Type ToResolverType(this Type type) => ResolverGenericTypeDescriptor.MakeGenericType(type);

        [MethodImpl((MethodImplOptions)256)]
        public static Expression Lock(this Expression body, Expression lockObject)
        {
            return Expression.TryFinally(
                Expression.Block(
                    Expression.Call(EnterMethodInfo, lockObject),
                    body), 
                Expression.Call(ExitMethodInfo, lockObject));
        }
    }
}

#endregion
#region ExpressionCompiler

namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal class ExpressionCompiler : IExpressionCompiler
    {
        public static readonly IExpressionCompiler Shared = new ExpressionCompiler();

        private ExpressionCompiler() { }

        public Delegate Compile(LambdaExpression resolverExpression)
        {
            if (resolverExpression == null) throw new ArgumentNullException(nameof(resolverExpression));
            if (resolverExpression.CanReduce)
            {
                resolverExpression = (LambdaExpression)resolverExpression.Reduce();
            }

            return resolverExpression.Compile();
        }
    }
}

#endregion
#region FluentRegister

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents extensions to register a dependency in the container.
    /// </summary>
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    [PublicAPI]
    internal static class FluentRegister
    {
        private static readonly IEnumerable<object> DefaultTags = new object[] { null };

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null) 
            => container.Register(new[] { TypeDescriptor<T>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T : T1 
            => container.Register(new[] { TypeDescriptor<T1>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T : T1, T2 
            => container.Register(new[] { TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions) 256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T : T1, T2, T3 
            => container.Register(new[] {TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type}, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <typeparam name="T4">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3, T4>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T : T1, T2, T3, T4
            => container.Register(new[] { TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <typeparam name="T4">The additional contract type.</typeparam>
        /// <typeparam name="T5">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3, T4, T5>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T : T1, T2, T3, T4, T5 
            => container.Register(new[] { TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <typeparam name="T4">The additional contract type.</typeparam>
        /// <typeparam name="T5">The additional contract type.</typeparam>
        /// <typeparam name="T6">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        public static IDisposable Register<T, T1, T2, T3, T4, T5, T6>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T : T1, T2, T3, T4, T5, T6
            => container.Register(new[] { TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <typeparam name="T4">The additional contract type.</typeparam>
        /// <typeparam name="T5">The additional contract type.</typeparam>
        /// <typeparam name="T6">The additional contract type.</typeparam>
        /// <typeparam name="T7">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T : T1, T2, T3, T4, T5, T6, T7
            => container.Register(new[] { TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type }, new FullAutowiringDependency(TypeDescriptor<T>.Type), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The autowiring type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <typeparam name="T4">The additional contract type.</typeparam>
        /// <typeparam name="T5">The additional contract type.</typeparam>
        /// <typeparam name="T6">The additional contract type.</typeparam>
        /// <typeparam name="T7">The additional contract type.</typeparam>
        /// <typeparam name="T8">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IContainer container, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null)
            where T : T1, T2, T3, T4, T5, T6, T7, T8
            => container.Register(new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8) }, new FullAutowiringDependency(typeof(T)), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            => container.Register(new[] { TypeDescriptor<T>.Type }, new AutowiringDependency(factory, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T : T1
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type }, new AutowiringDependency(factory, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T : T1, T2
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type }, new AutowiringDependency(factory, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T : T1, T2, T3
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type }, new AutowiringDependency(factory, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <typeparam name="T4">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3, T4>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T : T1, T2, T3, T4
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type }, new AutowiringDependency(factory, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <typeparam name="T4">The additional contract type.</typeparam>
        /// <typeparam name="T5">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3, T4, T5>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T : T1, T2, T3, T4, T5
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type }, new AutowiringDependency(factory, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <typeparam name="T4">The additional contract type.</typeparam>
        /// <typeparam name="T5">The additional contract type.</typeparam>
        /// <typeparam name="T6">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3, T4, T5, T6>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T : T1, T2, T3, T4, T5, T6
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type }, new AutowiringDependency(factory, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <typeparam name="T4">The additional contract type.</typeparam>
        /// <typeparam name="T5">The additional contract type.</typeparam>
        /// <typeparam name="T6">The additional contract type.</typeparam>
        /// <typeparam name="T7">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3, T4, T5, T6, T7>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T : T1, T2, T3, T4, T5, T6, T7
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type }, new AutowiringDependency(factory, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <typeparam name="T">The base type.</typeparam>
        /// <typeparam name="T1">The additional contract type.</typeparam>
        /// <typeparam name="T2">The additional contract type.</typeparam>
        /// <typeparam name="T3">The additional contract type.</typeparam>
        /// <typeparam name="T4">The additional contract type.</typeparam>
        /// <typeparam name="T5">The additional contract type.</typeparam>
        /// <typeparam name="T6">The additional contract type.</typeparam>
        /// <typeparam name="T7">The additional contract type.</typeparam>
        /// <typeparam name="T8">The additional contract type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="factory">The expression to create an instance.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="statements">The set of expressions to initialize an instance.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Register<T, T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] this IContainer container, Expression<Func<Context, T>> factory, [CanBeNull] ILifetime lifetime = null, [CanBeNull] object[] tags = null, [NotNull] [ItemNotNull] params Expression<Action<Context<T>>>[] statements)
            where T : T1, T2, T3, T4, T5, T6, T7, T8
            => container.Register(new[] { TypeDescriptor<T>.Type, TypeDescriptor<T1>.Type, TypeDescriptor<T2>.Type, TypeDescriptor<T3>.Type, TypeDescriptor<T4>.Type, TypeDescriptor<T5>.Type, TypeDescriptor<T6>.Type, TypeDescriptor<T7>.Type, TypeDescriptor<T8>.Type }, new AutowiringDependency(factory, statements), lifetime, tags);

        /// <summary>
        /// Registers a binding.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="types">The set of types.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        [NotNull]
        public static IDisposable Register([NotNull] this IContainer container, [NotNull][ItemNotNull] IEnumerable<Type> types, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime = null, [CanBeNull][ItemCanBeNull] params object[] tags)
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
                : container.Resolve<IIssueResolver>().CannotRegister(container, keys.ToArray());
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
    using System.Reflection;
    using System.Runtime.CompilerServices;

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

        [NotNull] private static readonly TypeDescriptor GenericContextTypeDescriptor = typeof(Context<>).Descriptor();
        [NotNull] private static Cache<ConstructorInfo, NewExpression> _constructors = new Cache<ConstructorInfo, NewExpression>();
        [NotNull] private static Cache<Type, Expression> _this = new Cache<Type, Expression>();
        [NotNull] private readonly Type _type;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        private readonly bool _hasGenericParamsWithConstraints;
        private readonly Dictionary<int, TypeDescriptor> _genericParamsWithConstraints = new Dictionary<int, TypeDescriptor>();
        private readonly Type[] _registeredGenericTypeParameters;
        private readonly TypeDescriptor _registeredTypeDescriptor;

        public FullAutowiringDependency([NotNull] Type type, [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _autowiringStrategy = autowiringStrategy;
            _registeredTypeDescriptor = type.Descriptor();
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
            var autoWiringStrategy = _autowiringStrategy ?? buildContext.AutowiringStrategy;
            if (!autoWiringStrategy.TryResolveType(_type, buildContext.Key.Type, out var instanceType))
            {
                instanceType = _hasGenericParamsWithConstraints
                    ? GetInstanceTypeBasedOnTargetGenericConstrains(buildContext.Key.Type) ?? buildContext.Container.Resolve<IIssueResolver>().CannotResolveType(_type, buildContext.Key.Type)
                    : _type;
            }

            var typeDescriptor = instanceType.Descriptor();
            var defaultConstructors = CreateMethods(buildContext.Container, typeDescriptor.GetDeclaredConstructors());
            if (!autoWiringStrategy.TryResolveConstructor(defaultConstructors, out var ctor))
            {
                if (DefaultAutowiringStrategy.Shared == autoWiringStrategy || !DefaultAutowiringStrategy.Shared.TryResolveConstructor(defaultConstructors, out ctor))
                {
                    ctor = buildContext.Container.Resolve<IIssueResolver>().CannotResolveConstructor(defaultConstructors);
                }
            }

            var defaultMethods = CreateMethods(buildContext.Container, typeDescriptor.GetDeclaredMethods());
            if (!autoWiringStrategy.TryResolveInitializers(defaultMethods, out var initializers))
            {
                if (DefaultAutowiringStrategy.Shared == autoWiringStrategy || !DefaultAutowiringStrategy.Shared.TryResolveInitializers(defaultMethods, out initializers))
                {
                    initializers = Enumerable.Empty<IMethod<MethodInfo>>();
                }
            }

            var newExpression = _constructors.GetOrCreate(ctor.Info, () => Expression.New(ctor.Info, ctor.GetParametersExpressions()));
            var thisExpression = _this.GetOrCreate(typeDescriptor.AsType(), () =>
            {
                var contextType = GenericContextTypeDescriptor.MakeGenericType(typeDescriptor.AsType());
                var itFieldInfo = contextType.Descriptor().GetDeclaredFields().Single(i => i.Name == nameof(Context<object>.It));
                return Expression.Field(Expression.Parameter(contextType, "context"), itFieldInfo);
            });

            var methodCallExpressions = (
                from initializer in initializers
                select (Expression) Expression.Call(thisExpression, initializer.Info, initializer.GetParametersExpressions())).ToArray();

            var autowiringDependency = new AutowiringDependency(newExpression, methodCallExpressions);
            return autowiringDependency.TryBuildExpression(buildContext, lifetime, out baseExpression, out error);
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

        [NotNull]
        [MethodImpl((MethodImplOptions) 256)]
        private static IEnumerable<IMethod<TMethodInfo>> CreateMethods<TMethodInfo>(IContainer container, [NotNull] IEnumerable<TMethodInfo> methodInfos)
            where TMethodInfo: MethodBase
            => methodInfos
                .Where(method => !method.IsStatic && (method.IsAssembly || method.IsPublic))
                .Select(info => new Method<TMethodInfo>(info));
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
#region IExpressionCompiler

namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a expression compiler.
    /// </summary>
    [PublicAPI]
    internal interface IExpressionCompiler
    {
        /// <summary>
        /// Compiles an expression to a delegate.
        /// </summary>
        /// <param name="resolverExpression">The lambda expression.</param>
        /// <returns>The resulting delegate.</returns>
        [NotNull] Delegate Compile([NotNull] LambdaExpression resolverExpression);
    }
}


#endregion
#region IssueResolver

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal sealed class IssueResolver : IIssueResolver
    {
        public static readonly IIssueResolver Shared = new IssueResolver();

        private IssueResolver() { }

        public Tuple<IDependency, ILifetime> CannotResolveDependency(IContainer container, Key key)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            throw new InvalidOperationException($"Cannot find the dependency for the key {key} in the container {container}.");
        }

        public Resolver<T> CannotGetResolver<T>(IContainer container, Key key, Exception error)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (error == null) throw new ArgumentNullException(nameof(error));
            throw new InvalidOperationException($"Cannot get resolver for the key {key} from the container {container}.", error);
        }

        public Type[] CannotGetGenericTypeArguments(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            throw new InvalidOperationException($"Cannot get generic type arguments from the type {type.Name}.");
        }

        public void CyclicDependenceDetected(Key key, int reentrancy)
        {
            if (reentrancy <= 0) throw new ArgumentOutOfRangeException(nameof(reentrancy));
            if (reentrancy >= 256)
            {
                throw new InvalidOperationException($"The cyclic dependence detected resolving the dependency {key}. The reentrancy is {reentrancy}.");
            }
        }

        public IDisposable CannotRegister(IContainer container, Key[] keys)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            throw new InvalidOperationException($"Keys {string.Join(", ", keys.Select(i => i.ToString()))} cannot be registered in the container {container}.");
        }

        public Type CannotParseType(string statementText, int statementLineNumber, int statementPosition, string typeName)
        {
            throw new InvalidOperationException($"Cannot parse the type {typeName} in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }

        public Lifetime CannotParseLifetime(string statementText, int statementLineNumber, int statementPosition, string lifetimeName)
        {
            throw new InvalidOperationException($"Cannot parse the lifetime {lifetimeName} in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }

        public object CannotParseTag(string statementText, int statementLineNumber, int statementPosition, string tag)
        {
            throw new InvalidOperationException($"Cannot parse the tag {tag} in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }

        public Expression CannotBuildExpression(IBuildContext buildContext, IDependency dependency, ILifetime lifetime)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            throw new InvalidOperationException($"Cannot build expression for the key {buildContext.Key} in the container {buildContext.Container}.");
        }

        public IMethod<ConstructorInfo> CannotResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors)
        {
            if (constructors == null) throw new ArgumentNullException(nameof(constructors));
            var type = constructors.Single().Info.DeclaringType;
            throw new InvalidOperationException($"Cannot find a constructor for the type {type}.");
        }

        public Type CannotResolveType(Type registeredType, Type resolvingType)
        {
            if (registeredType == null) throw new ArgumentNullException(nameof(registeredType));
            if (resolvingType == null) throw new ArgumentNullException(nameof(resolvingType));
            throw new InvalidOperationException($"Cannot resolve instance type based on the registered type {registeredType} for resolving type {registeredType}.");
        }
    }
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
            return lifetime?.Build(bodyExpression, buildContext) ?? bodyExpression;
        }
    }
}


#endregion
#region Method

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using static Injections;

    internal class Method<TMethodInfo>: IMethod<TMethodInfo> where TMethodInfo: MethodBase
    {
        // ReSharper disable once StaticMemberInGenericType
        [NotNull] private static Cache<Type, MethodCallExpression> _injections = new Cache<Type, MethodCallExpression>();
        private readonly Expression[] _parametersExpressions;
        private readonly ParameterInfo[] _parameters;

        public Method([NotNull] TMethodInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            _parameters = info.GetParameters();
            _parametersExpressions = new Expression[_parameters.Length];
        }

        public TMethodInfo Info { get; }

        public IEnumerable<Expression> GetParametersExpressions()
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
                    var paramType = _parameters[parameterPosition].ParameterType;
                    yield return _injections.GetOrCreate(paramType, () =>
                    {
                        var methodInfo = InjectMethodInfo.MakeGenericMethod(paramType);
                        var containerExpression = Expression.Field(Expression.Constant(null, TypeDescriptor<Context>.Type), nameof(Context.Container));
                        return Expression.Call(methodInfo, containerExpression);
                    });
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

        public bool TryRegisterDependency(IEnumerable<Key> keys, IDependency dependency, ILifetime lifetime, out IDisposable dependencyToken)
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
#region RegistrationTracker

namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal class RegistrationTracker: IObserver<ContainerEvent>
    {
        private readonly Container _container;
        private readonly Dictionary<Key, object> _instances = new Dictionary<Key, object>();
        private readonly List<IBuilder> _builders = new List<IBuilder>();
        private readonly List<IAutowiringStrategy> _autowiringStrategies = new List<IAutowiringStrategy> { DefaultAutowiringStrategy.Shared };

        public RegistrationTracker(Container container)
        {
            _container = container;
            _autowiringStrategies.Add(DefaultAutowiringStrategy.Shared);
        }

        public ICollection<IBuilder> Builders => _builders;

        public IAutowiringStrategy AutowiringStrategy => _autowiringStrategies[0];

        public void OnNext(ContainerEvent value)
        {
            _container.Reset();
            switch (value.EventTypeType)
            {
                case EventType.DependencyRegistration:
                    var container = value.Container;
                    foreach (var key in value.Keys)
                    {
                        if (Track<IBuilder>(key, container, i => _builders.Add(i)))
                        {
                            continue;
                        }

                        if (Track<IAutowiringStrategy>(key, container, i => _autowiringStrategies.Insert(0, i)))
                        {
                            continue;
                        }
                    }

                    break;

                case EventType.DependencyUnregistration:
                    foreach (var key in value.Keys)
                    {
                        if (_builders.Count > 0)
                        {
                            if (Untrack<IBuilder>(key, i => _builders.Remove(i)))
                            {
                                continue;
                            }
                        }

                        if (_autowiringStrategies.Count > 1)
                        {
                            if (Untrack<IAutowiringStrategy>(key, i => _autowiringStrategies.Remove(i)))
                            {
                                continue;
                            }
                        }
                    }

                    break;
            }
        }

        public void OnError(Exception error) { }

        public void OnCompleted() { }

        private bool Track<T>(Key key, IContainer container, Action<T> trackAction)
        {
            if (key.Type != typeof(T))
            {
                return false;
            }

            if (!container.TryGetResolver<T>(key.Type, key.Tag, out var resolver, out _, container))
            {
                return false;
            }

            var instance = resolver(container);
            _instances[key] = instance;
            trackAction(instance);
            return true;
        }

        private bool Untrack<T>(Key key, Action<T> untrackAction)
        {
            if (key.Type != typeof(T))
            {
                return false;
            }

            if (!_instances.TryGetValue(key, out var instance))
            {
                return true;
            }

            untrackAction((T)instance);
            return true;
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
    using System.Runtime.CompilerServices;

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal class Subject<T>: IObservable<T>, IObserver<T>
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

        [MethodImpl((MethodImplOptions)256)]
        public IDisposable Subscribe(IObserver<T> observer)
        {
            lock (_observers)
            {
                _observers.Add(observer);
            }

            return Disposable.Create(() =>
            {
                lock (_observers)
                {
                    _observers.Remove(observer);
                }
            });
        }

        [MethodImpl((MethodImplOptions)256)]
        public void OnNext(T value)
        {
            lock (_observers)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnNext(value);
                }
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        public void OnError(Exception error)
        {
            lock (_observers)
            {
                for (var index = 0; index < _observers.Count; index++)
                {
                    _observers[index].OnError(error);
                }
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        public void OnCompleted()
        {
            lock (_observers)
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
#region TypeDescriptor

namespace IoC.Core
{
    using System;

    internal static class TypeDescriptor<T>
    {
        [NotNull] public static readonly Type Type = typeof(T);

        // ReSharper disable once StaticMemberInGenericType
        public static readonly int HashCode = Type.GetHashCode();

        // ReSharper disable once StaticMemberInGenericType
        [NotNull] public static readonly TypeDescriptor Descriptor = Type.Descriptor();

        public static readonly Key Key = new Key(typeof(T));
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
        private static readonly Cache<Type, TypeDescriptor> TypeDescriptors =new Cache<Type, TypeDescriptor>();

        [MethodImpl((MethodImplOptions) 256)]
        public static TypeDescriptor Descriptor(this Type type) =>
            TypeDescriptors.GetOrCreate(type, () => new TypeDescriptor(type));

        [MethodImpl((MethodImplOptions) 256)]
        public static TypeDescriptor Descriptor<T>() =>
            TypeDescriptor<T>.Descriptor;

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
#region TypeDescriptorNet40

#if NET40
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal sealed class TypeDescriptor
    {
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
        public Type AsType() => Type;

        [MethodImpl((MethodImplOptions)256)]
        public Guid GetId() => Type.GUID;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Assembly GetAssembly() => Type.Assembly;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsValueType() => Type.IsValueType;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsArray() => Type.IsArray;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsPublic() => Type.IsPublic;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public Type GetElementType() => Type.GetElementType();

        [MethodImpl((MethodImplOptions)256)]
        public bool IsInterface() => Type.IsInterface;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsGenericParameter() => Type.IsGenericParameter;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsConstructedGenericType() => Type.IsGenericType;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsGenericTypeDefinition() => Type.IsGenericTypeDefinition;

        public bool IsGenericTypeArgument() => Type.GetCustomAttributes(TypeDescriptor<GenericTypeArgumentAttribute>.Type, true).Any();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type[] GetGenericTypeArguments() => Type.GetGenericArguments();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type[] GetGenericParameterConstraints() => Type.GetGenericParameterConstraints();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type[] GetGenericTypeParameters() => Type.GetGenericArguments();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<ConstructorInfo> GetDeclaredConstructors() => Type.GetConstructors(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<MethodInfo> GetDeclaredMethods() => Type.GetMethods(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<MemberInfo> GetDeclaredMembers() => Type.GetMembers(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<FieldInfo> GetDeclaredFields() => Type.GetFields(DefaultBindingFlags);

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public Type GetBaseType() => Type.BaseType;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<Type> GetImplementedInterfaces() => Type.GetInterfaces();

        [MethodImpl((MethodImplOptions)256)]
        public bool IsAssignableFrom([NotNull] TypeDescriptor typeDescriptor)
        {
            if (typeDescriptor == null) throw new ArgumentNullException(nameof(typeDescriptor));
            return Type.IsAssignableFrom(typeDescriptor.Type);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type MakeGenericType([NotNull] params Type[] typeArguments)
        {
            if (typeArguments == null) throw new ArgumentNullException(nameof(typeArguments));
            return Type.MakeGenericType(typeArguments);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type GetGenericTypeDefinition() => Type.GetGenericTypeDefinition();

        public override string ToString() => Type.ToString();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
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
    }
}
#endif


#endregion
#region TypeDescriptorNet45

#if !NET40
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal sealed class TypeDescriptor
    {
        internal readonly Type Type;
        internal readonly Lazy<TypeInfo> TypeInfo;

        [MethodImpl((MethodImplOptions)256)]
        public TypeDescriptor([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            TypeInfo = new Lazy<TypeInfo>(type.GetTypeInfo);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type AsType() => Type;

        [MethodImpl((MethodImplOptions)256)]
        public Guid GetId() => TypeInfo.Value.GUID;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Assembly GetAssembly() => TypeInfo.Value.Assembly;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsValueType() => TypeInfo.Value.IsValueType;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsInterface() => TypeInfo.Value.IsInterface;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsGenericParameter() => TypeInfo.Value.IsGenericParameter;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsArray() => TypeInfo.Value.IsArray;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsPublic() => TypeInfo.Value.IsPublic;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public Type GetElementType() => TypeInfo.Value.GetElementType();

        [MethodImpl((MethodImplOptions)256)]
        public bool IsConstructedGenericType() => Type.IsConstructedGenericType;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsGenericTypeDefinition() => TypeInfo.Value.IsGenericTypeDefinition;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type[] GetGenericTypeArguments() => TypeInfo.Value.GenericTypeArguments;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type[] GetGenericParameterConstraints() => TypeInfo.Value.GetGenericParameterConstraints();

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type[] GetGenericTypeParameters() => TypeInfo.Value.GenericTypeParameters;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsGenericTypeArgument() => TypeInfo.Value.GetCustomAttribute<GenericTypeArgumentAttribute>(true) != null;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<T> GetCustomAttributes<T>(bool inherit)
            where T : Attribute
            => TypeInfo.Value.GetCustomAttributes<T>(inherit);

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<ConstructorInfo> GetDeclaredConstructors() => TypeInfo.Value.DeclaredConstructors;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<MethodInfo> GetDeclaredMethods() => TypeInfo.Value.DeclaredMethods;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<MemberInfo> GetDeclaredMembers() => TypeInfo.Value.DeclaredMembers;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<FieldInfo> GetDeclaredFields() => TypeInfo.Value.DeclaredFields;

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public Type GetBaseType() => TypeInfo.Value.BaseType;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public IEnumerable<Type> GetImplementedInterfaces() => TypeInfo.Value.ImplementedInterfaces;

        [MethodImpl((MethodImplOptions)256)]
        public bool IsAssignableFrom([NotNull] TypeDescriptor typeDescriptor)
        {
            if (typeDescriptor == null) throw new ArgumentNullException(nameof(typeDescriptor));
            return TypeInfo.Value.IsAssignableFrom(typeDescriptor.TypeInfo.Value);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type MakeGenericType([NotNull] params Type[] typeArguments)
        {
            if (typeArguments == null) throw new ArgumentNullException(nameof(typeArguments));
            return Type.MakeGenericType(typeArguments);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public Type GetGenericTypeDefinition() => Type.GetGenericTypeDefinition();

        public override string ToString() => Type.ToString();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is TypeDescriptor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Type != null ? Type.GetHashCode() : 0);
        }

        private bool Equals(TypeDescriptor other)
        {
            return Type == other.Type;
        }
    }
}
#endif


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
            if (node.Type == TypeDescriptor<Type>.Type)
            {
                return Expression.Constant(ReplaceType((Type)node.Value), node.Type);
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

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            return Expression.NewArrayInit(ReplaceType(node.Type.GetElementType()), ReplaceAll(node.Expressions));
        }

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
        private IEnumerable<Expression> ReplaceAll(IEnumerable<Expression> expressions)
        {
            return expressions.Select(Visit);
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
#region IConverter

namespace IoC.Core.Configuration
{
    internal interface IConverter<in TSrc, in TContext, TDst>
    {
        bool TryConvert([NotNull] TContext context, [NotNull] TSrc src, out TDst dts);
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

        public bool TryConvert(BindingContext baseContext, IEnumerable<Statement> statements, out BindingContext context)
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

            context = baseContext;
            return true;
        }
    }
}


#endregion
#region StatementToBindingConverter

namespace IoC.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StatementToBindingConverter: IConverter<Statement, BindingContext, BindingContext>
    {
        private static readonly Regex BindingRegex = new Regex(@"Bind<(?<contractTypes>[\w.,\s<>]+)>\(\)(?<config>.*)\.To<\s*(?<instanceType>[\w.<>]+)\s*>\(\)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        private readonly IConverter<string, BindingContext, Type> _typeConverter;
        [NotNull] private readonly IConverter<string, Statement, Lifetime> _lifetimeConverter;
        [NotNull] private readonly IConverter<string, Statement, IEnumerable<object>> _tagsConverter;
        private readonly IIssueResolver _issueResolver;

        public StatementToBindingConverter(
            [NotNull] IConverter<string, BindingContext, Type> typeConverter,
            [NotNull] IConverter<string, Statement, Lifetime> lifetimeConverter,
            [NotNull] IConverter<string, Statement, IEnumerable<object>> tagsConverter,
            [NotNull] IIssueResolver issueResolver)
        {
            _typeConverter = typeConverter ?? throw new ArgumentNullException(nameof(typeConverter));
            _lifetimeConverter = lifetimeConverter ?? throw new ArgumentNullException(nameof(lifetimeConverter));
            _tagsConverter = tagsConverter ?? throw new ArgumentNullException(nameof(tagsConverter));
            _issueResolver = issueResolver ?? throw new ArgumentNullException(nameof(issueResolver));
        }

        public bool TryConvert(BindingContext baseContext, Statement statement, out BindingContext context)
        {
            if (baseContext == null) throw new ArgumentNullException(nameof(baseContext));
            var bindingMatch = BindingRegex.Match(statement.Text);
            if (bindingMatch.Success)
            {
                var instanceTypeName = bindingMatch.Groups["instanceType"].Value;
                if (!_typeConverter.TryConvert(baseContext, instanceTypeName, out var instanceType))
                {
                    instanceType = _issueResolver.CannotParseType(statement.Text, statement.LineNumber, statement.Position, instanceTypeName);
                }

                var contractTypes = new List<Type>();
                foreach (var contractTypeName in bindingMatch.Groups["contractTypes"]?.Value.Split(Separators.Type) ?? Enumerable.Empty<string>())
                {
                    if (!_typeConverter.TryConvert(baseContext, contractTypeName, out var contractType))
                    {
                        contractType = _issueResolver.CannotParseType(statement.Text, statement.LineNumber, statement.Position, contractTypeName);
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

                    context = new BindingContext(
                        baseContext.Assemblies,
                        baseContext.Namespaces,
                        baseContext.Bindings.Concat(Enumerable.Repeat(binding, 1)).Distinct());

                    return true;
                }
            }

            context = default(BindingContext);
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

        public bool TryConvert(BindingContext baseContext, Statement statement, out BindingContext context)
        {
            if (baseContext == null) throw new ArgumentNullException(nameof(baseContext));
            var match = Regex.Match(statement.Text);
            if (match.Success)
            {
                var namespaces = match.Groups[1].Value.Split(Separators.Namespace).Select(i => i.Trim()).Where(i => !string.IsNullOrWhiteSpace(i));
                context = new BindingContext(
                    baseContext.Assemblies,
                    baseContext.Namespaces.Concat(namespaces).Distinct(),
                    baseContext.Bindings);
                return true;
            }

            context = default(BindingContext);
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

        public bool TryConvert(BindingContext baseContext, Statement statement, out BindingContext context)
        {
            if (baseContext == null) throw new ArgumentNullException(nameof(baseContext));
            var match = Regex.Match(statement.Text);
            if (match.Success)
            {
                var assemblies = match.Groups[1].Value.Split(Separators.Assembly).Select(TypeDescriptorExtensions.LoadAssembly);
                context = new BindingContext(
                    baseContext.Assemblies.Concat(assemblies).Distinct(),
                    baseContext.Namespaces,
                    baseContext.Bindings);
                return true;
            }

            context = default(BindingContext);
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

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StringToLifetimeConverter: IConverter<string, Statement, Lifetime>
    {
        [NotNull] private readonly IIssueResolver _issueResolver;
        private static readonly Regex Regex = new Regex(@"(?:\s*\.\s*As\s*\(\s*([\w.^)]+)\s*\)\s*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public StringToLifetimeConverter([NotNull] IIssueResolver issueResolver)
        {
            _issueResolver = issueResolver ?? throw new ArgumentNullException(nameof(issueResolver));
        }

        public bool TryConvert(Statement statement, string text, out Lifetime lifetime)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            Match match = null;
            var success = false;
            lifetime = Lifetime.Transient;
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
                    lifetime = (Lifetime) Enum.Parse(TypeDescriptor<Lifetime>.Type, lifetimeName, true);
                }
                catch (Exception)
                {
                    lifetime = _issueResolver.CannotParseLifetime(statement.Text, statement.LineNumber, statement.Position, lifetimeName);
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

        public bool TryConvert(Statement context, string text, out IEnumerable<object> tags)
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

            tags = tagSet;
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
        private static readonly Dictionary<string, Type> PrimitiveTypes = new Dictionary<string, Type>
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

        public bool TryConvert(BindingContext context, string typeName, out Type type)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (string.IsNullOrWhiteSpace(typeName))
            {
                type = default(Type);
                return false;
            }

            if (TryResolveSimpleType(typeName, out type))
            {
                return true;
            }

            var typeDescription = new TypeDescription(context.Assemblies, context.Namespaces, typeName);
            if (!typeDescription.IsValid)
            {
                return false;
            }

            type = typeDescription.Type;
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

        public IEnumerable<IDisposable> Apply(IContainer container)
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

            return Enumerable.Empty<IDisposable>();
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
// ReSharper restore All