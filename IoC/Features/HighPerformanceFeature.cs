#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETCOREAPP2_0 && !WINDOWS_UWP
namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Extensibility;

    /// <summary>
    /// Allows to compile fast delegates.
    /// It requires access permissions to types/constructors/initialization methods.
    /// Also you could add the attribute <code>[assembly: InternalsVisibleTo(IoC.Features.HighPerformanceFeature.DynamicAssemblyName)]</code> for your assembly to allow use internal classes/methods/properties in a dependency injection.
    /// </summary>
    public class HighPerformanceFeature : IConfiguration
    {
        internal const string ShortDynamicAssemblyName = "IoC.DynamicAssembly";

        /// <summary>
        /// The full name of dynamic assembly.
        /// Could be use with <c>InternalsVisibleTo</c> attribute.
        /// </summary>
        public const string DynamicAssemblyName = ShortDynamicAssemblyName + ", PublicKey=00240000048000009400000006020000002400005253413100040000010001003fa521b0b16e978a933ecce70646c632538351d320a226a64b2c93238b3ba699cb66233e5722c25dd64f816c2aef8d2f1426983ea8c4750902f4a8b03cb00da22e7c978f56cdcfc711ea0a3625016a2ec2238093912799a3cda4ee787592738c7d21f6eed5e3a6d1b03f657ac3880672f2394144bd2359fddf17e464abd947a0";
        
        /// The default instance.
        public static readonly IConfiguration Default = new HighPerformanceFeature();

        private HighPerformanceFeature() { }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            yield return container.Register<ExpressionCompilerOptimizing, IExpressionCompiler>();
        }
    }
}
#endif