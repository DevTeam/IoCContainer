﻿namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal struct RegistrationToken: IDisposable
    {
        [NotNull] internal readonly IContainer Container;
        [NotNull] private readonly IDisposable _registration;

        public RegistrationToken([NotNull] IContainer container, [NotNull] IDisposable registration)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _registration = registration ?? throw new ArgumentNullException(nameof(registration));
        }

        [MethodImpl((MethodImplOptions)256)]
        public void Dispose() => _registration.Dispose();
    }
}
