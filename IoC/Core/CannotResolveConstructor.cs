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

        public IMethod<ConstructorInfo> Resolve(IBuildContext buildContext, IEnumerable<IMethod<ConstructorInfo>> constructors)
        {
            if (constructors == null) throw new ArgumentNullException(nameof(constructors));
            var type = constructors.Single().Info.DeclaringType;
            throw new InvalidOperationException($"Cannot find a constructor for the type {type}.\n{buildContext}");
        }
    }
}