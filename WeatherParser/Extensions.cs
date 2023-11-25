namespace WeatherParser;

public static class Extensions
{
    public static int ToInt(this string value)
    {
        int intValue = 0;
        int.TryParse(value, out intValue);

        return intValue;
    }
}
