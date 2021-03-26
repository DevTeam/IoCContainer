namespace IoC.Benchmark
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;
    using Model;

    internal class TestTypeBuilder
    {
        private readonly ICollection<Type> _types = new List<Type>();

        public static readonly TestTypeBuilder Default = new TestTypeBuilder(5, 3);

        public TestTypeBuilder(int levelsCount, int dependenciesCount)
        {
            var assembly = DefineAssembly($"TestAssembly_{levelsCount}_{dependenciesCount}");
            var module = DefineModule(assembly, "module");
            RootType = RegisterType(module, levelsCount, dependenciesCount, true);
        }

        public int Count { get; private set; }

        public Type RootType { get; }

        public IEnumerable<Type> Types => _types;

        private Type RegisterType(ModuleBuilder moduleBuilder, int levelsCount, int dependenciesCount, bool isRoot)
        {
            Type[] args;
            if (levelsCount == 0)
            {
                args = new Type[0];
            }
            else
            {
                args = new Type[dependenciesCount];
                for (var argIndex = 0; argIndex < dependenciesCount; argIndex++)
                {
                    args[argIndex] = RegisterType(moduleBuilder, levelsCount - 1, dependenciesCount, false);
                }
            }

            var typeBuilder = DefineType(moduleBuilder, $"Type_{Count++}", isRoot,  args);
            var type = typeBuilder.CreateType();
            _types.Add(type);
            return type;
        }

        private static AssemblyBuilder DefineAssembly(string assemblyName)
        {
#if NET40
            return AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.RunAndCollect);
#else
            return AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.RunAndCollect);
#endif
        }

        private static ModuleBuilder DefineModule(AssemblyBuilder assemblyBuilder, string moduleName) =>
            assemblyBuilder.DefineDynamicModule(moduleName);

        private static TypeBuilder DefineType(ModuleBuilder moduleBuilder, string typeName, bool isRoot, params Type[] parameters)
        {
            var typeBuilder =  moduleBuilder.DefineType(
                typeName,
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout | TypeAttributes.Sealed,
                null,
                isRoot ? new [] { typeof(ICompositionRoot) } : null);

            var ctorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard,
                parameters);

            var ctorGen = ctorBuilder.GetILGenerator();
            ctorGen.Emit(OpCodes.Ret);

            if (isRoot)
            {
                var doSomethingMethod = typeBuilder.DefineMethod(nameof(ICompositionRoot.DoSomething), MethodAttributes.Public | MethodAttributes.Virtual, CallingConventions.Standard);
                var gen = doSomethingMethod.GetILGenerator();
                gen.Emit(OpCodes.Ret);
            }

            return typeBuilder;
        }
    }
}
