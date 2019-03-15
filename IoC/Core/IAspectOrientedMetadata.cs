namespace IoC.Core
{
    using System;

    internal interface IAspectOrientedMetadata
    {
        bool TryGetType([NotNull] Attribute attribute, out Type type);

        bool TryGetOrder([NotNull] Attribute attribute, out IComparable comparable);

        bool TryGetTag([NotNull] Attribute attribute, out object tag);
    }
}