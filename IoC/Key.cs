namespace IoC
{
    using System;

    [PublicAPI]
    public struct Key
    {
        [NotNull] internal readonly Type ContractType;
        [CanBeNull] internal readonly object Tag;

        public Key([NotNull] Type type, [CanBeNull] object tag = null)
        {
            ContractType = type ?? throw new ArgumentNullException(nameof(type));
            Tag = tag;
        }

        public override string ToString()
        {
            return $"Key: [{ContractType.Name} {Tag ?? "empty"}]";
        }
    }
}
