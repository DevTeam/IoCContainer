namespace IoC.Core.Configuration
{
    using System;

    internal sealed class Binding
    {
        public Binding(
            [NotNull] Type[] types,
            Lifetime lifetime,
            [NotNull][ItemCanBeNull] object[] tags,
            [NotNull] Type instanceType)
        {
            Types = types ?? throw new ArgumentNullException(nameof(types));
            Lifetime = lifetime;
            Tags = tags ?? throw new ArgumentNullException(nameof(tags));
            InstanceType = instanceType ?? throw new ArgumentNullException(nameof(instanceType));
        }

        public Type[] Types { get; }

        public Lifetime Lifetime { get; }

        public object[] Tags { get; }

        public Type InstanceType { get; }
    }
}
