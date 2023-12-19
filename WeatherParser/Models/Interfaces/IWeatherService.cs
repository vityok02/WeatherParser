namespace WeatherParser.Models.Interfaces;

public interface IWeatherService
{
    Weather GetWeather(double latitude, double longitude);
}
