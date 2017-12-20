﻿namespace IoC
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
        [NotNull] public readonly Type ContractType;
        [NotNull] public readonly object[] Args;

        public Context(
            long registrationId,
            Key key,
            [NotNull] IContainer registrationContainer,
            [NotNull] IContainer resolvingContainer,
            [NotNull] Type contractType,
            [NotNull] params object[] args)
        {
            RegistrationId = registrationId;
            Key = key;
            RegistrationContainer = registrationContainer;
            ResolvingContainer = resolvingContainer;
            ContractType = contractType;
            Args = args;
        }
    }
}
