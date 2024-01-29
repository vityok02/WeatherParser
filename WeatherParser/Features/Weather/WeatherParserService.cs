using HtmlAgilityPack;

namespace WeatherParser.Features.Weather;

public class WeatherParserService : IWeatherParserService
{
    public HtmlDocument ParseWeather(string htmlPath)
    {
        HtmlWeb web = new();
        return web.Load(htmlPath);
    }
}
