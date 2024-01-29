using HtmlAgilityPack;

namespace WeatherParser.Services.WeatherServices;

public class WeatherParserService : IWeatherParserService
{
    public HtmlDocument ParseWeather(string htmlPath)
    {
        HtmlWeb web = new();
        return web.Load(htmlPath);
    }
}
