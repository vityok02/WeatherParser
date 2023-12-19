using HtmlAgilityPack;

namespace WeatherParser.Models.Interfaces;

public interface IWeatherParserService
{
    HtmlDocument ParseWeather(string htmlPath);
}
