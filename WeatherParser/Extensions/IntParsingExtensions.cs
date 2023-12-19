namespace WeatherParser.Extensions;

public static class IntParsingExtensions
{
    public static int ToInt(this string value)
    {
        int intValue = 0;
        int.TryParse(value, out intValue);

        return intValue;
    }
}
