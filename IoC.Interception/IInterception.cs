namespace IoC.Features
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IInterception<T>
    {
        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] IContainer Container { get; }

        /// <summary>
        /// The types to intercept.
        /// </summary>
        [NotNull][ItemNotNull] IEnumerable<Type> Types { get; }

        /// <summary>
        /// The tags to mark the binding.
        /// </summary>
        [NotNull][ItemCanBeNull] IEnumerable<object> Tags { get; }
    }
}
