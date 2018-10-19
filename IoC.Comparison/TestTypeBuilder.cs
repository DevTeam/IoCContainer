namespace IoC.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    internal class TestTypeBuilder
    {
        private readonly ICollection<Type> _types = new List<Type>();

        public TestTypeBuilder(int levelsCount, int dependenciesCount)
        {
            var assembly = DefineAssembly($"TestAssembly_{levelsCount}_{dependenciesCount}");
            var module = DefineModule(assembly, "module");
            RootType = RegisterType(module, levelsCount, dependenciesCount);
        }

        public int Count { get; private set; }

        public Type RootType { get; }

        public IEnumerable<Type> Types => _types;

        private Type RegisterType(ModuleBuilder moduleBuilder, int levelsCount, int dependenciesCount)
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
                    args[argIndex] = RegisterType(moduleBuilder, levelsCount - 1, dependenciesCount);
                }
            }

            var typeBuilder = DefineType(moduleBuilder, $"Type_{Count++}", args);
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

        private static TypeBuilder DefineType(ModuleBuilder moduleBuilder, string typeName, params Type[] parameters)
        {
            var typeBuilder =  moduleBuilder.DefineType(
                typeName,
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout,
                null);

            var ctorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard,
                parameters);

            var ctorGen = ctorBuilder.GetILGenerator();
            ctorGen.Emit(OpCodes.Ret);

            return typeBuilder;
        }
    }
}
