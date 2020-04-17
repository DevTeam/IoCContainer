﻿// ReSharper disable IdentifierTypo
// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable EmptyConstructor
// ReSharper disable UnusedVariable
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class NullableValueTypeResolving
    {
        [Fact]
        // $visible=true
        // $tag=injection
        // $priority=03
        // $description=Nullable Value Type Resolving
        public void Run()
        {
            // {
            // Create the container and configure it
            using var container = Container.Create()
                .Bind<int>().Tag(1).To(ctx => 1)
                .Container;

            // Resolve an instance
            var val1 = container.Resolve<int?>(1.AsTag());
            var val2 = container.Resolve<int?>(2.AsTag());
            var val3 = container.Resolve<int?>();

            // Check the optional dependency
            val1.Value.ShouldBe(1);
            val2.HasValue.ShouldBeFalse();
            val3.HasValue.ShouldBeFalse();
            // }
        }
    }
}