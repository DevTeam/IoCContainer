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
            throw new BuildExpressionException($"Cannot find a constructor for {buildContext.Key}.\n{buildContext}", null);
        }
    }
}