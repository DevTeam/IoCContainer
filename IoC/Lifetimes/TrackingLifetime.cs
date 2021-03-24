namespace IoC.Lifetimes
{
    using System;

    /// <summary>
    /// Represents the abstraction for a lifetime with states.
    /// </summary>
    [PublicAPI]
    public abstract class TrackingLifetime: SimpleLifetime
    {
        private readonly TrackTypes _trackTypes;
        
        protected TrackingLifetime(TrackTypes trackTypes) =>
            _trackTypes = trackTypes;

        /// <summary>
        /// Invoked before the new instance creation.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The created instance.</returns>
        protected virtual void BeforeCreating(IContainer container, object[] args) { }

        /// <summary>
        /// Invoked after a new instance has been created.
        /// </summary>
        /// <param name="newInstance">The new instance.</param>
        /// <param name="container">The target container.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The created instance.</returns>
        protected virtual object AfterCreation(object newInstance, IContainer container, object[] args)
            => newInstance;

        protected override T GetOrCreateInstance<T>(Resolver<T> resolver, IContainer container, object[] args)
        {
            if (_trackTypes.HasFlag(TrackTypes.BeforeCreating))
            {
                BeforeCreating(container, args);
            }

            var newInstance = resolver(container, args);

            if (_trackTypes.HasFlag(TrackTypes.AfterCreation))
            {
                AfterCreation(newInstance, container, args);
            }

            return newInstance;
        }

        [Flags]
        public enum TrackTypes: byte
        {
            AfterCreation = 0b1,
            BeforeCreating = 0b10
        }
    }
}
