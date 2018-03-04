#if !NET40 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETCOREAPP2_0
namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;

    /// <summary>
    /// Allows to compile fast delegates.
    /// </summary>
    public class ExpressionCompilerFeature : IConfiguration
    {
        /// The shared instance.
        public static readonly IConfiguration Shared = new ExpressionCompilerFeature();

        private ExpressionCompilerFeature()
        {
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            yield return container.Register<ExpressionCompiler, IExpressionCompiler>();
        }
    }
}
#endif