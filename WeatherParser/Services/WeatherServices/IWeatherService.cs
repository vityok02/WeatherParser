using WeatherParser.Models;

namespace WeatherParser.Services.WeatherServices;

public interface IWeatherService
{
    Weather GetWeather(Coordinates coordinates);
}
