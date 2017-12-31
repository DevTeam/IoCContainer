namespace IoC
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "NotAccessedField.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [PublicAPI]
    public struct ResolvingContext
    {
        public readonly RegistrationContext RegistrationContext;
        public readonly Key ResolvingKey;
        [NotNull] public readonly IContainer ResolvingContainer;
        [NotNull][ItemCanBeNull] public readonly object[] Args;
        internal readonly bool IsConstructedGenericResolvingContractType;

        internal ResolvingContext(
            RegistrationContext registrationContext,
            Key resolvingKey,
            [NotNull] IContainer resolvingContainer,
            [NotNull][ItemCanBeNull] object[] args,
            bool isConstructedGenericResolvingContractType)
        {
            RegistrationContext = registrationContext;
            ResolvingKey = resolvingKey;
            ResolvingContainer = resolvingContainer;
            Args = args;
            IsConstructedGenericResolvingContractType = isConstructedGenericResolvingContractType;
        }
    }
}
