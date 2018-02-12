// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace IoC
{
    using System;
    using Core;

    [PublicAPI]
    public abstract class GenericTypeArgument { }

    [PublicAPI]
    public abstract class TT: GenericTypeArgument { }

    [PublicAPI]
    public abstract class TT1 : GenericTypeArgument { }

    [PublicAPI]
    public abstract class TT2 : GenericTypeArgument { }

    [PublicAPI]
    public abstract class TT3 : GenericTypeArgument { }

    [PublicAPI]
    public abstract class TT4 : GenericTypeArgument { }

    [PublicAPI]
    public abstract class TT5 : GenericTypeArgument { }

    [PublicAPI]
    public abstract class TT6 : GenericTypeArgument { }

    [PublicAPI]
    public abstract class TT7 : GenericTypeArgument { }

    [PublicAPI]
    public abstract class TT8 : GenericTypeArgument { }


    internal static class GenericTypeArguments
    {
        public static readonly Type[] Types = {typeof(TT), typeof(TT1), typeof(TT2), typeof(TT3), typeof(TT4), typeof(TT5), typeof(TT6), typeof(TT7), typeof(TT8)};
    }
}
