using WeatherParser.Models;

namespace WeatherParser.Features.Weather;

public interface IWeatherService
{
    Models.Weather GetWeather(Coordinates coordinates);
}
