namespace IoC.Internal.Configuration
{
    using System;

    internal class Binding
    {
        public Binding(
            Type[] contractTypes,
            Lifetime lifetime,
            object[] tags,
            Type instanceType)
        {
            ContractTypes = contractTypes;
            Lifetime = lifetime;
            Tags = tags;
            InstanceType = instanceType;
        }

        public Type[] ContractTypes { get; }

        public Lifetime Lifetime { get; }

        public object[] Tags { get; }

        public Type InstanceType { get; }
    }
}
