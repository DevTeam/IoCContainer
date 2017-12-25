namespace IoC.Internal
{
    using System.Collections.Concurrent;

    internal abstract class SingletoneBaseLifetime: ILifetime
    {
        public object GetOrCreate(Context context, IFactory factory)
        {
            return GetInstances(context).GetOrAdd(CeateKey(context), i => factory.Create(context));
        }

        [NotNull]
        protected abstract IInstanceKey CeateKey(Context context);

        [NotNull]
        protected abstract ConcurrentDictionary<IInstanceKey, object> GetInstances(Context context);
    }
}
