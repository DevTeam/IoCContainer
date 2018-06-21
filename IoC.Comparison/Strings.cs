namespace IoC.Comparison
{
    using System.Globalization;

    internal static class Strings
    {
        public static string ToShortString(this long number)
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
