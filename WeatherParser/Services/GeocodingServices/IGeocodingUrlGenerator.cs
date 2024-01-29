namespace WeatherParser.Services.GeocodingServices;

public interface IGeocodingUrlGenerator
{
    string GenerateUrl(string[] queryParameters);
}
