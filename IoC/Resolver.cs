﻿namespace IoC
{
    [PublicAPI]
    [CanBeNull] public delegate T Resolver<out T>([NotNull] IContainer container, [NotNull][ItemCanBeNull] params object[] args);
}
