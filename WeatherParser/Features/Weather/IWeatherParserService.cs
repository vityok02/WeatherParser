using HtmlAgilityPack;

namespace WeatherParser.Features.Weather;

public interface IWeatherParserService
{
    HtmlDocument ParseWeather(string htmlPath);
}
