using HtmlAgilityPack;
using WeatherParser.Extensions;
using WeatherParser.Models;

namespace WeatherParser.Services.WeatherServices;

public class WeatherService : IWeatherService
{
    private readonly IWeatherParserService _parser;
    private readonly IWeatherUrlGenerator _weatherUrlGenerator;

    public WeatherService(IWeatherParserService parser, IWeatherUrlGenerator weatherUrlGenerator)
    {
        _parser = parser;
        _weatherUrlGenerator = weatherUrlGenerator;
    }

    public Weather GetWeather(Coordinates coordinates)
    {
        var htmlPath = _weatherUrlGenerator.GenerateUrl(coordinates.Latitude, coordinates.Longitude);
        var htmlDoc = _parser.ParseWeather(htmlPath);

        var currentTemperatureNode = htmlDoc.DocumentNode
            .SelectSingleNode("/html/body/div[1]/main/div[2]/main/div[1]/div/section/div/div/div[2]/div[1]/div[1]/span");
        var minTemperatureNode = htmlDoc.DocumentNode
            .SelectSingleNode(@"//*[@id=""WxuCurrentConditions-main-eb4b02cb-917b-45ec-97ec-d4eb947f6b6a""]/div/section/div/div/div[2]/div[1]/div[1]/div[2]/span[2]");
        var maxTemperatureNode = htmlDoc.DocumentNode
            .SelectSingleNode("//*[@id=\"WxuCurrentConditions-main-eb4b02cb-917b-45ec-97ec-d4eb947f6b6a\"]/div/section/div/div/div[2]/div[1]/div[1]/div[2]/span[1]");
        var currentTimeNode = htmlDoc.DocumentNode
            .SelectSingleNode("/html/body/div[1]/main/div[2]/main/div[1]/div/section/div/div/div[1]/span");
        var locationNode = htmlDoc.DocumentNode
            .SelectSingleNode("//*[@id=\"WxuCurrentConditions-main-eb4b02cb-917b-45ec-97ec-d4eb947f6b6a\"]/div/section/div/div/div[1]/h1");

        var currentTemperature = currentTemperatureNode?.FirstChild?.InnerText?.ToInt();
        var minTemperature = minTemperatureNode.FirstChild.InnerText.ToInt();
        var maxTemperature = maxTemperatureNode.FirstChild.InnerText.ToInt();
        var location = locationNode.InnerText;
        var currentTime = currentTimeNode.InnerText.Extract();

        var weather = new Weather()
        {
            CurrentTemperature = currentTemperature,
            MinTemperature = minTemperature,
            MaxTemperature = maxTemperature,
            Location = location,
            ObservationTime = currentTime,
        };

        return weather;
    }
}
