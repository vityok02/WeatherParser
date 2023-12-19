namespace WeatherParser.Extensions;

public static class CoordinateParsingExtensions
{
    public static string ToStringWithPoint(this double input)
    {
        return input.ToString().Replace(",", ".");
    }
}
