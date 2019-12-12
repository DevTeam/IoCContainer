namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Core;
    using Core.Configuration;

    /// <summary>
    /// Allows to configure via a text metadata.
    /// </summary>
    [PublicAPI]
    public sealed class ConfigurationFeature : IConfiguration
    {
        /// <summary>
        /// The default instance.
        /// </summary>
        public static readonly IConfiguration Default = new ConfigurationFeature();

        private ConfigurationFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var containerSingletonResolver = container.GetResolver<ILifetime>(Lifetime.ContainerSingleton.AsTag());
            yield return container.Register<StatementsToBindingContextConverter, IConverter<IEnumerable<Statement>, BindingContext, BindingContext>>(containerSingletonResolver(container));
            yield return container.Register<StatementToBindingConverter, IConverter<Statement, BindingContext, BindingContext>>(containerSingletonResolver(container), new object[] { "Bind" });
            yield return container.Register<StatementToNamespacesConverter, IConverter<Statement, BindingContext, BindingContext>>(containerSingletonResolver(container), new object[] {"using"});
            yield return container.Register<StatementToReferencesConverter, IConverter<Statement, BindingContext, BindingContext>>(containerSingletonResolver(container), new object[] { "ref" });
            yield return container.Register<StringToTypeConverter, IConverter<string, BindingContext, Type>>(containerSingletonResolver(container));
            yield return container.Register<StringToLifetimeConverter, IConverter<string, Statement, Lifetime>>(containerSingletonResolver(container));
            yield return container.Register<StringToTagsConverter, IConverter<string, Statement, IEnumerable<object>>>(containerSingletonResolver(container));
            yield return container.Register<IConfiguration>(ctx => CreateTextConfiguration(ctx, ctx.Container.Inject<IConverter<IEnumerable<Statement>, BindingContext, BindingContext>>()));
        }

        internal static TextConfiguration CreateTextConfiguration([NotNull] Context ctx, [NotNull] IConverter<IEnumerable<Statement>, BindingContext, BindingContext> converter)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (converter == null) throw new ArgumentNullException(nameof(converter));
            // ReSharper disable once NotResolvedInText
            if (ctx.Args.Length != 1) throw new ArgumentOutOfRangeException("Should have single argument.");

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

            return new TextConfiguration(reader, converter);
        }
    }
}
