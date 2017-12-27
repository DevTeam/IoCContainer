namespace IoC.Tests
{
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
    }
}
