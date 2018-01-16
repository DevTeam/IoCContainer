namespace IoC
{
    using System;
    using System.Collections.Generic;

    [PublicAPI]
    public interface IConfiguration
    {
        [NotNull][ItemNotNull] IEnumerable<IDisposable> Apply([NotNull] IContainer container);
    }
}
