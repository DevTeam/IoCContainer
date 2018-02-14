namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Core.Configuration;

    [PublicAPI]
    public sealed class ConfigurationFeature : IConfiguration
    {
        public static readonly IConfiguration Shared = new ConfigurationFeature();

        private ConfigurationFeature()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container
                .Bind<IConverter<IEnumerable<Statement>, BindingContext, BindingContext>>()
                .As(Lifetime.ContainerSingleton)
                .To<StatementsToBindingContextConverter>();

            yield return container
                .Bind<IConverter<Statement, BindingContext, BindingContext>>()
                .Tag("Bind")
                .As(Lifetime.ContainerSingleton)
                .To<StatementToBindingConverter>();

            yield return container
                .Bind<IConverter<Statement, BindingContext, BindingContext>>()
                .Tag("using")
                .As(Lifetime.ContainerSingleton)
                .To<StatementToNamespacesConverter>();

            yield return container
                .Bind<IConverter<Statement, BindingContext, BindingContext>>()
                .Tag("ref")
                .As(Lifetime.ContainerSingleton)
                .To<StatementToReferencesConverter>();

            yield return container
                .Bind<IConverter<string, BindingContext, Type>>()
                .As(Lifetime.ContainerSingleton)
                .To<StringToTypeConverter>();

            yield return container
                .Bind<IConverter<string, Statement, Lifetime>>()
                .As(Lifetime.ContainerSingleton)
                .To<StringToLifetimeConverter>();

            yield return container
                .Bind<IConverter<string, Statement, IEnumerable<object>>>()
                .As(Lifetime.ContainerSingleton)
                .To<StringToTagsConverter>();

            yield return container
                .Bind<IConfiguration>()
                .To(ctx => CreateTextConfiguration(ctx));
        }

        private static TextConfiguration CreateTextConfiguration(Context ctx)
        {
            if (ctx.Args.Length != 1)
            {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentOutOfRangeException("Should have one argument.");
            }

            TextReader reader;
            switch (ctx.Args[0])
            {
                case string text:
                    reader = new StringReader(text);
                    break;

                case Stream stream:
                    reader = new StreamReader(stream);
                    break;

                case TextReader textReader:
                    reader = textReader;
                    break;

                default:
                    throw new ArgumentException("Invalid type of argument.");
            }

            return new TextConfiguration(reader, ctx.Container.Get<IConverter<IEnumerable<Statement>, BindingContext, BindingContext>>());
        }
    }
}
