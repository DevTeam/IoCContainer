namespace IoC.Internal.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal sealed class TextConfiguration : IConfiguration
    {
        [NotNull] private readonly IConverter<IEnumerable<Statement>, BindingContext, BindingContext> _bindingsConverter;
        [NotNull] private readonly IEnumerable<Statement> _statements;

        public TextConfiguration(
            [NotNull] TextReader textReader,
            [NotNull] IConverter<IEnumerable<Statement>, BindingContext, BindingContext> bindingsConverter)
        {
            _bindingsConverter = bindingsConverter ?? throw new ArgumentNullException(nameof(bindingsConverter));
            _statements = GetStetements(textReader ?? throw new ArgumentNullException(nameof(textReader)));
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (_bindingsConverter.TryConvert(BindingContext.Empty, _statements, out var context))
            {
                return
                    from binding in context.Bindings
                    let registration = binding.Tags.Aggregate(
                        container.Bind(binding.ContractTypes).Lifetime(binding.Lifetime),
                        (current, tag) => current.Tag(tag))
                    select registration.Lifetime(binding.Lifetime).To(binding.InstanceType);
            }

            return Enumerable.Empty<IDisposable>();
        }

        private static IEnumerable<Statement> GetStetements(TextReader textReader)
        {
            var lineNumber = 0;
            var line = string.Empty;
            do
            {
                var curLine = textReader.ReadLine();
                if (!string.IsNullOrWhiteSpace(curLine) && curLine.TrimEnd().Last() != Separators.Statement)
                {
                    line += curLine;
                    continue;
                }

                line += curLine;
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var prevPosition = 0;
                    do
                    {
                        var position = line.IndexOf(Separators.Statement, prevPosition);
                        if (position == -1)
                        {
                            position = line.Length - prevPosition;
                        }

                        var text = line.Substring(prevPosition, position - prevPosition);
                        yield return new Statement(text.Trim(), lineNumber, prevPosition);
                        prevPosition = position + 1;
                    }
                    while (prevPosition < line.Length);
                    line = string.Empty;
                }
                else
                {
                    yield break;
                }

                lineNumber++;
            }
            while (true);
        }
    }
}
