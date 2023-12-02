using HtmlAgilityPack;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using static WeatherParser.Extensions;

namespace WeatherParser;

public class Weather
{
    public int CurrentTemperature { get; set; }
    public int MinTemperature { get; set; }
    public int MaxTemperature { get; set; }
    public DateTime? CurrentTime { get; set; }
    public DateTime DateTime { get; set; }
    public string Location { get; set; }

    public Weather()
    {
        GetWeather();
    }

    private Weather(int currentTemperature, int minTemperature, int maxTemperature, DateTime currentTime, string location)
    {
        CurrentTemperature = currentTemperature;
        MinTemperature = minTemperature;
        MaxTemperature = maxTemperature;
        CurrentTime = currentTime;
        Location = location;
    }

    public Weather GetWeather()
    {
        var html = @"https://weather.com/weather/today/l/49.23,28.47?par=google&unit=m";

        HtmlWeb web = new HtmlWeb();

        var htmlDoc = web.Load(html);

        var currentTemperatureNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/main/div[2]/main/div[1]/div/section/div/div/div[2]/div[1]/div[1]/span");
        var minTemperatureNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"WxuCurrentConditions-main-eb4b02cb-917b-45ec-97ec-d4eb947f6b6a\"]/div/section/div/div/div[2]/div[1]/div[1]/div[2]/span[2]");
        var maxTemperatureNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"WxuCurrentConditions-main-eb4b02cb-917b-45ec-97ec-d4eb947f6b6a\"]/div/section/div/div/div[2]/div[1]/div[1]/div[2]/span[1]");
        var currentTimeNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/main/div[2]/main/div[1]/div/section/div/div/div[1]/span");
        var locationNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"WxuCurrentConditions-main-eb4b02cb-917b-45ec-97ec-d4eb947f6b6a\"]/div/section/div/div/div[1]/h1");

        CurrentTemperature = currentTemperatureNode.FirstChild.InnerText.ToInt();
        MinTemperature = minTemperatureNode.FirstChild.InnerText.ToInt();
        MaxTemperature = maxTemperatureNode.FirstChild.InnerText.ToInt();
        Location = locationNode.InnerText;
        CurrentTime = ExtractTime(currentTimeNode.InnerText.ToString()!);

        return this;

        static DateTime ExtractTime(string input)
        {
            string format = "hh:mm";

            string[] words = input.Split(' ');

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

    public string GetWeatherToSend()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Current temperature: {CurrentTemperature.ToString()}");
        sb.AppendLine($"Minimal temperature: {MinTemperature.ToString()}");
        sb.AppendLine($"Maximum temperature: {MaxTemperature.ToString()}");
        sb.AppendLine($"Time: {CurrentTime.ToString()}");
        sb.AppendLine($"Location: {Location}");

        return sb.ToString();
    }
}