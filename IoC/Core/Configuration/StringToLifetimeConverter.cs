namespace IoC.Core.Configuration
{
    using System;
    using System.Text.RegularExpressions;
    using Issues;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StringToLifetimeConverter: IConverter<string, Statement, Lifetime>
    {
        [NotNull] private readonly ICannotParseLifetime _cannotParseLifetime;
        private static readonly Regex Regex = new Regex(@"(?:\s*\.\s*As\s*\(\s*([\w.^)]+)\s*\)\s*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public StringToLifetimeConverter([NotNull] ICannotParseLifetime cannotParseLifetime)
        {
            _cannotParseLifetime = cannotParseLifetime ?? throw new ArgumentNullException(nameof(cannotParseLifetime));
        }

        public bool TryConvert(Statement statement, string text, out Lifetime dst)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            Match match = null;
            var success = false;
            dst = Lifetime.Transient;
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
                    dst = (Lifetime) Enum.Parse(TypeDescriptor<Lifetime>.Type, lifetimeName, true);
                }
                catch (Exception)
                {
                    dst = _cannotParseLifetime.Resolve(statement.Text, statement.LineNumber, statement.Position, lifetimeName);
                }

                success = true;
            }
            while (true);
            return success;
        }
    }
}
