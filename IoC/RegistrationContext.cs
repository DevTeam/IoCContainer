namespace IoC
{
    using System;

    public sealed class RegistrationContext
    {
        public readonly int RegistrationId;
        [NotNull] public readonly IContainer RegistrationContainer;

        public RegistrationContext(
            int registrationId,
            [NotNull] IContainer registrationContainer)
        {
#if DEBUG
            if (registrationContainer == null) throw new ArgumentNullException(nameof(registrationContainer));
#endif
            RegistrationId = registrationId;
            RegistrationContainer = registrationContainer;
        }
    }
}
