namespace IoC
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "NotAccessedField.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [PublicAPI]
    public sealed class ResolvingContext
    {
        public readonly RegistrationContext RegistrationContext;

        public Key ResolvingKey;
        [NotNull] public IContainer ResolvingContainer;
        [NotNull][ItemCanBeNull] public object[] Args;

        internal bool IsGenericResolvingType;

        // ReSharper disable once NotNullMemberIsNotInitialized
        internal ResolvingContext(
            [NotNull] RegistrationContext registrationContext)
        {
#if DEBUG
            if (registrationContext == null) throw new ArgumentNullException(nameof(registrationContext));
#endif
            RegistrationContext = registrationContext;
        }
    }
}
