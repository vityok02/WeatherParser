namespace WeatherParser.Extensions;

public static class IntExtensions
{
    public static int ToInt(this string value)
    {
        int.TryParse(value, out int intValue);

        return intValue;
    }
}
