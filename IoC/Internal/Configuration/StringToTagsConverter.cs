namespace IoC.Internal.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StringToTagsConverter : IConverter<string, Statement, IEnumerable<object>>
    {
        private static readonly Regex Regex = new Regex(@"(?:\s*\.\s*Tag\s*\(\s*([^)]*)\s*\)\s*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public bool TryConvert(Statement context, string text, out IEnumerable<object> tags)
        {
            var tagSet = new HashSet<object>();
            Match match = null;
            do
            {
                match = match?.NextMatch() ?? Regex.Match(text);
                if (!match.Success)
                {
                    break;
                }

                var tag = match.Groups[1].Value.Trim();
                // empty
                if (string.IsNullOrWhiteSpace(tag))
                {
                    tagSet.Add(null);
                    continue;
                }

                // string
                if (tag.StartsWith("\"") && tag.EndsWith("\""))
                {
                    tagSet.Add(tag.Substring(1, tag.Length - 2));
                    continue;
                }

                // char
                if (tag.Length == 3 && tag.StartsWith("'") && tag.EndsWith("'"))
                {
                    tagSet.Add(tag[1]);
                    continue;
                }

                // int
                if (int.TryParse(tag, NumberStyles.Any, CultureInfo.InvariantCulture, out var intValue))
                {
                    tagSet.Add(intValue);
                    continue;
                }

                // long
                if (long.TryParse(tag, NumberStyles.Any, CultureInfo.InvariantCulture, out var longValue))
                {
                    tagSet.Add(longValue);
                    continue;
                }

                // double
                if (double.TryParse(tag, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
                {
                    tagSet.Add(doubleValue);
                    continue;
                }

                // DateTimeOffset
                if (DateTimeOffset.TryParse(tag, out var dateTimeOffsetValue))
                {
                    tagSet.Add(dateTimeOffsetValue);
                }
            }
            while (true);

            tags = tagSet;
            return tagSet.Any();
        }
    }
}
