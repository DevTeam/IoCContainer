namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [PublicAPI]
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
            private readonly object[] _args;
            private static readonly Key ResolvingKey = Key.Create<T>();
            private readonly IEnumerable<Key> _allKeys;
            private readonly IContainer _container;

            public InstanceEnumerable(Context context)
            {
                _container = context.Container;
                _args = context.Args;
                _allKeys = context.Container;
            }

            public IEnumerator<T> GetEnumerator()
            {
                var keys =
                    from key in _allKeys
                    where key.Type == ResolvingKey.Type
                    select key;

                foreach (var key in keys)
                {
                    if (!_container.TryGetResolver<T>(key, out var resolver, _container))
                    {
                        continue;
                    }

                    yield return resolver(_container, _args);
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
