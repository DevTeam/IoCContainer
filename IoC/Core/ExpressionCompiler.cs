#if !NET40 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETCOREAPP2_0
namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class ExpressionCompiler : IExpressionCompiler
    {
        private readonly ModuleBuilder _moduleBuilder;
        private int _resolverTypeId;

        public ExpressionCompiler()
        {
            var domain = AppDomain.CurrentDomain;
            var assemblyBuilder = domain.DefineDynamicAssembly(new AssemblyName { Name = "DynamicAssembly" }, AssemblyBuilderAccess.Run);
            _moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");
        }

        public Delegate Compile(LambdaExpression resolverExpression)
        {
            if (resolverExpression == null) throw new ArgumentNullException(nameof(resolverExpression));
            return TryCompile(resolverExpression) ?? ExpressionCompilerSkippingSecurityCheck.Shared.Compile(resolverExpression);
        }

        [CanBeNull]
        private Delegate TryCompile(LambdaExpression resolverExpression)
        {
            try
            {
                var returnTypeInfo = resolverExpression.ReturnType.Info();
                if (!returnTypeInfo.IsPublic)
                {
                    return null;
                }

                var typeName = "DynamicResolver" + System.Threading.Interlocked.Increment(ref _resolverTypeId);
                var typeBuilder = _moduleBuilder.DefineType(typeName, TypeAttributes.Public);
                var methodBuilder = typeBuilder.DefineMethod("Resolve", MethodAttributes.Public | MethodAttributes.Static, resolverExpression.ReturnType, ExpressionExtensions.ParameterTypes);
                resolverExpression.CompileToMethod(methodBuilder);
                return typeBuilder.CreateType().GetMethod("Resolve")?.CreateDelegate(resolverExpression.ReturnType.ToResolverType());
            }
            catch
            {
                return null;
            }
        }
    }
}
#endif
