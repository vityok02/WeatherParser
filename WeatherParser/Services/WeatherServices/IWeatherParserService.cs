using HtmlAgilityPack;

namespace WeatherParser.Services.WeatherServices;

public interface IWeatherParserService
{
    HtmlDocument ParseWeather(string htmlPath);
}
