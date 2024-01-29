using WeatherParser.Extensions;

namespace WeatherParser.Features.Weather;

public class WeatherUrlGenerator(HostBuilderContext hostBuilderContext) : IWeatherUrlGenerator
{
    private readonly IConfiguration _configuration = hostBuilderContext.Configuration;

    public string GenerateUrl(double latitude, double longitude)
    {
        var formatedLatitude = latitude.ToStringWithPoint();
        var formatedLongitude = longitude.ToStringWithPoint();

        var parsingPath = _configuration["WeatherParsingConnection:Path"];
        var parsingParameters = _configuration["WeatherParsingConnection:Parameters"];

        var htmlPath = $"{parsingPath}/{formatedLatitude},{formatedLongitude}?{parsingParameters}";

        return htmlPath;
    }
}