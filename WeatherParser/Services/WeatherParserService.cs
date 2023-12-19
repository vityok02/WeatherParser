using HtmlAgilityPack;
using WeatherParser.Models.Interfaces;

namespace WeatherParser.Services;

public class WeatherParserService : IWeatherParserService
{
    private readonly IHtmlPathBuilder _pathGenerator;

    public WeatherParserService(IHtmlPathBuilder pathGenerator)
    {
        _pathGenerator = pathGenerator;
    }

    public HtmlDocument ParseWeather(string htmlPath)
    {
        HtmlWeb web = new HtmlWeb();

        return web.Load(htmlPath);
    }
}
