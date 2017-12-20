namespace IoC.Impl
{
    using System;
    using System.Collections.Generic;

    internal class ChildConfiguration: IConfiguration
    {
        public static readonly IConfiguration Shared = new ChildConfiguration();

        private ChildConfiguration()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield break;
        }
    }
}
