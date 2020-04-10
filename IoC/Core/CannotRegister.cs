﻿namespace IoC.Core
{
    using System;
    using System.Linq;
    using Issues;

    internal sealed class CannotRegister : ICannotRegister
    {
        public static readonly ICannotRegister Shared = new CannotRegister();

        private CannotRegister() { }

        public IToken Resolve(IContainer container, Key[] keys)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            throw new InvalidOperationException($"Keys {string.Join(", ", keys.Select(i => i.ToString()))} cannot be registered in the container {container}.");
        }
    }
}