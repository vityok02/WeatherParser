namespace WeatherParser.Extensions;

public static class StringExtensions
{
    public static bool ContainsAnySymbol(this string str, char[] symbols)
    {
        foreach (var symbol in symbols)
        {
            if (str.Contains(symbol))
            {
                return true;
            }
        }

        return false;
    }

    public static string ToStringWithPoint(this double input)
    {
        return input.ToString().Replace(",", ".");
    }
}
