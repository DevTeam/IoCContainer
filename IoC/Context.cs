namespace IoC
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "NotAccessedField.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [PublicAPI]
    public struct Context
    {
        public readonly int RegistrationId;
        public readonly Key RegistrationKey;
        [NotNull] public readonly IContainer RegistrationContainer;
        public readonly Key ResolvingKey;
        [NotNull] public readonly IContainer ResolvingContainer;
        [NotNull][ItemCanBeNull] public readonly object[] Args;
        internal readonly bool IsConstructedGenericResolvingContractType;

        internal Context(
            int registrationId,
            Key registrationKey,
            [NotNull] IContainer registrationContainer,
            Key resolvingKey,
            [NotNull] IContainer resolvingContainer,
            [NotNull][ItemCanBeNull] object[] args,
            bool isConstructedGenericResolvingContractType)
        {
            RegistrationId = registrationId;
            RegistrationKey = registrationKey;
            RegistrationContainer = registrationContainer;
            ResolvingKey = resolvingKey;
            ResolvingContainer = resolvingContainer;
            Args = args;
            IsConstructedGenericResolvingContractType = isConstructedGenericResolvingContractType;
        }
    }
}
