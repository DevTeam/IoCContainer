// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace IoC
{
    using System;

    /// <summary>
    /// Represents the generic type parameter marker.
    /// </summary>
    [PublicAPI]
    public abstract class GenericTypeArgument { }

    /// <inheritdoc />
    [PublicAPI]
    public abstract class TT: GenericTypeArgument { }

    /// <inheritdoc />
    [PublicAPI]
    public abstract class TT1 : GenericTypeArgument { }

    /// <inheritdoc />
    [PublicAPI]
    public abstract class TT2 : GenericTypeArgument { }

    /// <inheritdoc />
    [PublicAPI]
    public abstract class TT3 : GenericTypeArgument { }

    /// <inheritdoc />
    [PublicAPI]
    public abstract class TT4 : GenericTypeArgument { }

    /// <inheritdoc />
    [PublicAPI]
    public abstract class TT5 : GenericTypeArgument { }

    /// <inheritdoc />
    [PublicAPI]
    public abstract class TT6 : GenericTypeArgument { }

    /// <inheritdoc />
    [PublicAPI]
    public abstract class TT7 : GenericTypeArgument { }

    /// <inheritdoc />
    [PublicAPI]
    public abstract class TT8 : GenericTypeArgument { }

    internal static class GenericTypeArguments
    {
        public static readonly Type[] Types = {typeof(TT), typeof(TT1), typeof(TT2), typeof(TT3), typeof(TT4), typeof(TT5), typeof(TT6), typeof(TT7), typeof(TT8)};
    }
}
