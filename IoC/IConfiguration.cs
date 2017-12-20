﻿namespace IoC
{
    using System;
    using System.Collections.Generic;

    [PublicAPI]
    public interface IConfiguration
    {
        [NotNull] IEnumerable<IDisposable> Apply([NotNull] IContainer container);
    }
}
