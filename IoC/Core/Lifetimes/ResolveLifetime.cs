namespace IoC.Core.Lifetimes
{
    using System;

    internal sealed class ResolveLifetime: ILifetime
    {
        //private long _resolvingId;
        //private readonly ConcurrentDictionary<long, ILifetime> _lifetimes = new ConcurrentDictionary<long, ILifetime>();

        public T GetOrCreate<T>(Key key, IContainer container, object[] args, Resolver<T> resolver)
        {
            throw new NotImplementedException();
            //var lifetime = _lifetimes.GetOrAdd(, context.Container.Tag(Lifetime.Singletone).Get<ILifetime>());
            //return lifetime.GetOrCreate(key, container, args, resolver);
        }
    }
}
