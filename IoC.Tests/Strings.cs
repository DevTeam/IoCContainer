namespace IoC.Tests
{
    using System.Globalization;

    internal static class Strings
    {
        public static string Width([NotNull] this string text, int value)
        {
            if (text.Length < value)
            {
                return text + new string(' ', value - text.Length);
            }

            return text;
        }

        public static string ToShortString(this int number)
        {
            if (number > 999999999)
            {
                return number.ToString("0,,,.###B", CultureInfo.InvariantCulture);
            }

            if (number > 999999)
            {
                return number.ToString("0,,.##M", CultureInfo.InvariantCulture);
            }

            if (number > 999)
            {
                return number.ToString("0,.#K", CultureInfo.InvariantCulture);
            }

            return number.ToString(CultureInfo.InvariantCulture);
        }
    }
}
