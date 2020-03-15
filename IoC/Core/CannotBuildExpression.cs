﻿namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using Issues;

    internal class CannotBuildExpression : ICannotBuildExpression
    {
        public static readonly ICannotBuildExpression Shared = new CannotBuildExpression();

        private CannotBuildExpression() { }

        public Expression Resolve(IBuildContext buildContext, IDependency dependency, ILifetime lifetime, Exception error)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            throw new InvalidOperationException($"Cannot build expression for the key {buildContext.Key} in the container {buildContext.Container}.\n{buildContext}", error);
        }
    }
}