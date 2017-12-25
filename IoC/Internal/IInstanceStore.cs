namespace IoC.Internal
{
    using System.Collections.Concurrent;

    internal interface IInstanceStore
    {
        [NotNull]
        ConcurrentDictionary<IInstanceKey, object> GetInstances();
    }
}
