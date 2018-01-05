namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Internal;

    public sealed class EnumerableFeature : IConfiguration
    {
        public static readonly IConfiguration Shared = new EnumerableFeature();

        private EnumerableFeature()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            yield return container
                .Bind(typeof(IEnumerable<>))
                .To(typeof(InstanceEnumerable<>));
        }

        private sealed class InstanceEnumerable<T> : IEnumerable<T>
        {
            private static readonly Key ResolvingKey = new Key(typeof(T));
            private static readonly bool IsGenericResolvingType = typeof(T).IsConstructedGenericType();
            private readonly IEnumerable<Key> _allKeys;
            private readonly ResolvingContext _resolvingContext;

            public InstanceEnumerable(ResolvingContext context)
            {
                _allKeys = context.ResolvingContainer as IEnumerable<Key>;
                _resolvingContext = new ResolvingContext(context.RegistrationContext)
                {
                    ResolvingKey = ResolvingKey,
                    ResolvingContainer = context.ResolvingContainer,
                    Args = context.Args,
                    IsGenericResolvingType = IsGenericResolvingType
                };
            }

            public IEnumerator<T> GetEnumerator()
            {
                var keys =
                    from key in _allKeys
                    where key.ContractType == _resolvingContext.ResolvingKey.ContractType
                    select key;

                foreach (var key in keys)
                {
                    if (!_resolvingContext.ResolvingContainer.TryGetResolver(key, out var resolver))
                    {
                        continue;
                    }

                    yield return (T)resolver.Resolve(_resolvingContext.ResolvingKey, _resolvingContext.ResolvingContainer, 0, _resolvingContext.Args);
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
