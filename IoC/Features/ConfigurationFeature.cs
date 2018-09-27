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
        public IEnumerable<IDisposable> Apply(IContainer container)
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
            yield return container.Register<IConfiguration>(ctx => CreateTextConfiguration(ctx));
        }

        internal static TextConfiguration CreateTextConfiguration(Context ctx)
        {
            if (ctx.Args.Length != 1)
            {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentOutOfRangeException("Should have single argument.");
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

            return new TextConfiguration(reader, ctx.Container.Resolve<IConverter<IEnumerable<Statement>, BindingContext, BindingContext>>());
        }
    }
}
