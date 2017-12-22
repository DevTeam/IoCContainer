namespace IoC.Internal
{
    using System.Collections.Generic;

    internal interface IInstanceStore
    {
        [NotNull]
        IDictionary<IInstanceKey, object> GetInstances();
    }
}
