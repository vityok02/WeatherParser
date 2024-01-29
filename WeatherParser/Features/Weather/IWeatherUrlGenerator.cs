namespace WeatherParser.Features.Weather;

public interface IWeatherUrlGenerator
{
    string GenerateUrl(double latitude, double longitude);
}
