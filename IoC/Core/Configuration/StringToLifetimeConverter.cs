namespace IoC.Core.Configuration
{
    using System;
    using System.Text.RegularExpressions;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StringToLifetimeConverter: IConverter<string, Statement, Lifetime>
    {
        [NotNull] private readonly IIssueResolver _issueResolver;
        private static readonly Regex Regex = new Regex(@"(?:\s*\.\s*As\s*\(\s*([\w.^)]+)\s*\)\s*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public StringToLifetimeConverter([NotNull] IIssueResolver issueResolver)
        {
            _issueResolver = issueResolver ?? throw new ArgumentNullException(nameof(issueResolver));
        }

        public bool TryConvert(Statement statement, string text, out Lifetime lifetime)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            Match match = null;
            var success = false;
            lifetime = Lifetime.Transient;
            do
            {
                match = match?.NextMatch() ?? Regex.Match(text);
                if (!match.Success)
                {
                    break;
                }

                var lifetimeName = match.Groups[1].Value.Replace(" ", string.Empty).Replace($"{nameof(Lifetime)}.", string.Empty).Trim();
                try
                {
                    lifetime = (Lifetime) Enum.Parse(TypeDescriptor<Lifetime>.Type, lifetimeName, true);
                }
                catch (Exception)
                {
                    lifetime = _issueResolver.CannotParseLifetime(statement.Text, statement.LineNumber, statement.Position, lifetimeName);
                }

                success = true;
            }
            while (true);
            return success;
        }
    }
}
