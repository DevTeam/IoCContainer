namespace IoC.Core
{
    using System;
    using Issues;

    internal sealed class CannotResolveGenericTypeArgument : ICannotResolveGenericTypeArgument
    {
        public static readonly ICannotResolveGenericTypeArgument Shared = new CannotResolveGenericTypeArgument();

        public Type Resolve(IBuildContext buildContext, Type type, int genericTypeArgPosition, Type genericTypeArg)
        {
            var typeDescriptor = type.Descriptor();
            var genericTypeArgs = typeDescriptor.IsGenericTypeDefinition() ? typeDescriptor.GetGenericTypeParameters() : typeDescriptor.GetGenericTypeArguments();
            throw new InvalidOperationException($"Cannot resolve the generic type argument \'{genericTypeArgs[genericTypeArgPosition]}\' at position {genericTypeArgPosition} of the type {typeDescriptor.Type.GetShortName()}.");
        }
    }
}
