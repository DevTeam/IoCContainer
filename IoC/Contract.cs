namespace IoC
{
    using System;

    [PublicAPI]
    public struct Contract
    {
        [NotNull] internal readonly Type Type;

        public Contract([NotNull] Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public override string ToString()
        {
            return $"Contract: {Type.Name ?? "null"}";
        }
    }
}
