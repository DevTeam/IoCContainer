namespace IoC.Features.AspNetCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    
    internal class AutowiringStrategy: IAutowiringStrategy
    {
        public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType)
        {
            instanceType = default(Type);
            return false;
        }

        public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
        {
            var ctors =
                from ctor in constructors
                where ctor.Info.IsPublic
                let parameters = ctor.Info.GetParameters()
                where parameters.Length == 1
                from parameter in parameters
                let type = parameter.ParameterType.GetTypeInfo()
                where type.IsPublic && type.IsInterface
                select ctor;

            constructor = ctors.FirstOrDefault();
            return constructor != null;
        }

        public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = default(IEnumerable<IMethod<MethodInfo>>);
            return false;
        }
    }
}
