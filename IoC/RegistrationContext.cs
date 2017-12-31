namespace IoC
{
    public class RegistrationContext
    {
        public readonly int RegistrationId;
        public readonly Key RegistrationKey;
        [NotNull] public readonly IContainer RegistrationContainer;

        internal RegistrationContext(
            int registrationId,
            Key registrationKey,
            [NotNull] IContainer registrationContainer)
        {
            RegistrationId = registrationId;
            RegistrationKey = registrationKey;
            RegistrationContainer = registrationContainer;
        }
    }
}
