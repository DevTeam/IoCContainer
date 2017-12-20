namespace IoC.Impl
{
    using System.Collections.Generic;

    internal abstract class SingletoneBaseLifetime: ILifetime
    {
        public object GetOrCreate(Context context, IFactory factory)
        {
            var key = CeateKey(context);
            var instances = GetInstances(context);
            if (instances.TryGetValue(key, out var instance))
            {
                return instance;
            }

            instance = factory.Create(context);
            instances.Add(key, instance);
            return instance;
        }

        [NotNull]
        protected abstract IInstanceKey CeateKey(Context context);

        [NotNull]
        protected abstract IDictionary<IInstanceKey, object> GetInstances(Context context);
    }
}
