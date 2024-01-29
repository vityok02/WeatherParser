namespace WeatherParser.Services.WeatherServices;

public interface IWeatherUrlGenerator
{
    string GenerateUrl(double latitude, double longitude);
}
