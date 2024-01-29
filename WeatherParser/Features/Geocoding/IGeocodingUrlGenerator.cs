namespace WeatherParser.Features.GeocodingServices;

public interface IGeocodingUrlGenerator
{
    string GenerateUrl(string[] queryParameters);
}
