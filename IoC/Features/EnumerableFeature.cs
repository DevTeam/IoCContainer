namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class EnumerableFeature: IConfiguration
    {
        public static readonly IConfiguration Shared = new EnumerableFeature();

        private EnumerableFeature()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container
                .Map(typeof(IEnumerable<>))
                .To(CreateEnumerable);
        }

        private static object CreateEnumerable(Context ctx)
        {
            var keys =
                from key in ctx.ResolvingContainer as IEnumerable<Key> ?? Enumerable.Empty<Key>()
                where key.Contract.Type == ctx.ContractType
                select key;

            if (!ctx.ContractType.IsConstructedGenericType)
            {
                throw new InvalidOperationException();
            }

            var instanceType = typeof(InstanceEnumerable<>).MakeGenericType(ctx.ContractType.GenericTypeArguments);
            return Activator.CreateInstance(instanceType, keys, ctx);
        }

        private class InstanceEnumerable<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _instances;

            public InstanceEnumerable(IEnumerable<Key> key, Context context)
            {
                _instances = GetInstances(key, context);
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _instances.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private static IEnumerable<T> GetInstances(IEnumerable<Key> keys, Context context)
            {
                foreach (var key in keys)
                {
                    if (!context.ResolvingContainer.TryGetResolver(key, out var resolver))
                    {
                        continue;
                    }

                    yield return (T)resolver.Resolve(context.ResolvingContainer, context.ContractType, context.Args);
                }
            }
        }
    }
}
