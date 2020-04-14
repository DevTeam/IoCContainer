namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal struct Binding<T>: IBinding<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        public Binding([NotNull] IMutableContainer container, [NotNull][ItemNotNull] params Type[] types)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            Tokens = Enumerable.Empty<IToken>();
            Types = types ?? throw new ArgumentNullException(nameof(types));
            Lifetime = default(ILifetime);
            Tags = Enumerable.Empty<object>();
            AutowiringStrategy = default(IAutowiringStrategy);
        }

        // ReSharper disable once StaticMemberInGenericType
        public Binding([NotNull] IToken token, [NotNull][ItemNotNull] params Type[] types)
        {
            if (token == null) { throw new ArgumentNullException(nameof(token)); }
            Container = token.Container;
            Tokens = new[] { token };
            Types = types ?? throw new ArgumentNullException(nameof(types));
            Lifetime = default(ILifetime);
            Tags = Enumerable.Empty<object>();
            AutowiringStrategy = default(IAutowiringStrategy);
        }

        public Binding([NotNull] IBinding binding, [NotNull][ItemNotNull] params Type[] types)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Tokens = binding.Tokens;
            Types = binding.Types.Concat(types);
            Tags = binding.Tags;
            Lifetime = binding.Lifetime;
            AutowiringStrategy = binding.AutowiringStrategy;
        }

        public Binding([NotNull] IBinding<T> binding, Lifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Tokens = binding.Tokens;
            Types = binding.Types;
            Tags = binding.Tags;
            Lifetime = lifetime != IoC.Lifetime.Transient ? binding.Container.Resolve<ILifetime>(lifetime.AsTag(), binding.Container) : null;
            AutowiringStrategy = binding.AutowiringStrategy;
        }

        public Binding([NotNull] IBinding<T> binding, [NotNull] ILifetime lifetime)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Tokens = binding.Tokens;
            Types = binding.Types;
            Lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            Tags = binding.Tags;
            AutowiringStrategy = binding.AutowiringStrategy;
        }

        public Binding([NotNull] IBinding<T> binding, [CanBeNull] object tagValue)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Tokens = binding.Tokens;
            Types = binding.Types;
            Lifetime = binding.Lifetime;
            Tags = binding.Tags.Concat(Enumerable.Repeat(tagValue, 1));
            AutowiringStrategy = binding.AutowiringStrategy;
        }

        public Binding([NotNull] IBinding<T> binding, [NotNull] IAutowiringStrategy autowiringStrategy)
        {
            if (binding == null) throw new ArgumentNullException(nameof(binding));
            Container = binding.Container;
            Tokens = binding.Tokens;
            Types = binding.Types;
            Lifetime = binding.Lifetime;
            Tags = binding.Tags;
            AutowiringStrategy = autowiringStrategy;
        }

        public IMutableContainer Container { get; }

        public IEnumerable<IToken> Tokens { get; }

        public IEnumerable<Type> Types { get; }

        public IEnumerable<object> Tags { get; }

        public ILifetime Lifetime { get; }

        public IAutowiringStrategy AutowiringStrategy { get; }
    }
}
