namespace IoC.Core
{
    using System;
    using Issues;

    internal sealed class CannotResolveType : ICannotResolveType
    {
        public static readonly ICannotResolveType Shared = new CannotResolveType();

        private CannotResolveType() { }

        public Type Resolve(IBuildContext buildContext, Type registeredType, Type resolvingType)
        {
            if (registeredType == null) throw new ArgumentNullException(nameof(registeredType));
            if (resolvingType == null) throw new ArgumentNullException(nameof(resolvingType));
            throw new InvalidOperationException($"Cannot resolve instance type based on the registered type {registeredType.GetShortName()} for resolving type {registeredType.GetShortName()}.\n{buildContext}");
        }
    }
}