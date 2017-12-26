namespace IoC
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "NotAccessedField.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [PublicAPI]
    public struct Context
    {
        public readonly long RegistrationId;
        public readonly Key Key;
        [NotNull] public readonly IContainer RegistrationContainer;
        [NotNull] public readonly IContainer ResolvingContainer;
        [NotNull] public readonly Type TargetContractType;
        internal readonly bool IsConstructedGenericTargetContractType;
        [NotNull] public readonly object[] Args;

        internal Context(
            long registrationId,
            Key key,
            [NotNull] IContainer registrationContainer,
            [NotNull] IContainer resolvingContainer,
            [NotNull] Type targetContractType,
            bool isConstructedGenericTargetContractType,
            [NotNull] params object[] args)
        {
            RegistrationId = registrationId;
            Key = key;
            RegistrationContainer = registrationContainer;
            ResolvingContainer = resolvingContainer;
            TargetContractType = targetContractType;
            Args = args;
            IsConstructedGenericTargetContractType = isConstructedGenericTargetContractType;
        }
    }
}
