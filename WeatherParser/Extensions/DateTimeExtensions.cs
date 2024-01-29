using System.Globalization;

namespace WeatherParser.Extensions;

public static class DateTimeExtensions
{
    public static DateTime Extract(this string input)
    {
        string[] words = input.ToString().Split(' ');
        int indexOfOf = Array.IndexOf(words, "of");

        if (indexOfOf != -1 && indexOfOf + 1 < words.Length)
        {
            string timeString = words[indexOfOf + 1];
            string format = GetFormat(timeString);

            if (DateTime.TryParseExact(timeString, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime extractedTime))
            {
                if (words[3] == "pm")
                {
                    extractedTime = extractedTime.AddHours(12);
                    return extractedTime;
                }
                return extractedTime;
            }
        }

        return DateTime.MinValue;
    }

    private static string GetFormat(string time)
    {
        if(time.Split()[0].Length == 2)
        {
            return "hh:mm";
        }
        return "h:mm";
    }
}
