using System.Globalization;

namespace WeatherParser.Extensions;

public static class DateTimeParsingExtensions
{
    public static DateTime ExtractTime(this string input)
    {
        string format = "hh:mm";
        var words = input.ToString().Split(' ');

        int indexOfOf = Array.IndexOf(words, "of");
        if (indexOfOf != -1 && indexOfOf + 1 < words.Length)
        {
            string timeString = words[indexOfOf + 1];

            DateTime extractedTime;
            if (DateTime.TryParseExact(timeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out extractedTime))
            {
                return extractedTime;
            }
        }

        return DateTime.MinValue;
    }
}
