#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETCOREAPP2_0 && !NETCOREAPP2_1 && !WINDOWS_UWP
namespace IoC.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using Extensibility;
    using Features;
    using static Extensibility.WellknownExpressions;
    using static TypeDescriptorExtensions;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class ExpressionCompilerOptimizing : IExpressionCompiler
    {
        private const string ResolverModuleName = "DynamicModule";
        private const string ResolverTypeName = "DynamicResolver";
        private const string ResolveMethodName = "Resolve";
        private const string SnkResourceKey = "IoC.DevTeam.snk";

        [NotNull] private static readonly Type[] ResolverParameterTypes = ResolverParameters.Select(i => i.Type).ToArray();
        [NotNull] private static readonly ModuleBuilder ModuleBuilder;
        private static int _resolverTypeId;

        static ExpressionCompilerOptimizing()
        {
            var domain = AppDomain.CurrentDomain;
            var assembly = Descriptor<ExpressionCompilerOptimizing>().GetAssembly();
            using (var keyStream = assembly.GetManifestResourceStream(SnkResourceKey))
            using (var keyReader = new BinaryReader(keyStream ?? throw new InvalidOperationException($"Resource with key \"{SnkResourceKey}\" was not found.")))
            {
                var key = keyReader.ReadBytes((int)keyStream.Length);
                var assemblyName = new AssemblyName { Name = HighPerformanceFeature.ShortDynamicAssemblyName, KeyPair = new StrongNameKeyPair(key) };
                var assemblyBuilder = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                ModuleBuilder = assemblyBuilder.DefineDynamicModule(ResolverModuleName);
            }
        }

        public bool IsReferenceConstantSupported => false;

        public Delegate Compile(LambdaExpression resolverExpression)
            => TryCompile(resolverExpression ?? throw new ArgumentNullException(nameof(resolverExpression))) ?? ExpressionCompilerDefault.Shared.Compile(resolverExpression);

        [CanBeNull]
        private Delegate TryCompile(LambdaExpression resolverExpression)
        {
            try
            {
                var returnTypeDescriptor = resolverExpression.ReturnType.Descriptor();
                if (!returnTypeDescriptor.IsPublic())
                {
                    return null;
                }

                if (resolverExpression.CanReduce)
                {
                    resolverExpression = (LambdaExpression)resolverExpression.Reduce();
                }

                var typeName = ResolverTypeName + System.Threading.Interlocked.Increment(ref _resolverTypeId);
                var typeBuilder = ModuleBuilder.DefineType(typeName, TypeAttributes.Public);
                var methodBuilder = typeBuilder.DefineMethod(ResolveMethodName, MethodAttributes.Public | MethodAttributes.Static, resolverExpression.ReturnType, ResolverParameterTypes);
                resolverExpression.CompileToMethod(methodBuilder);
                var methodInfo = typeBuilder.CreateType().GetMethod(ResolveMethodName);
                return methodInfo == null ? null : Delegate.CreateDelegate(resolverExpression.ReturnType.ToResolverType(), methodInfo);
            }
            catch
            {
                return null;
            }
        }
    }
}
#endif
