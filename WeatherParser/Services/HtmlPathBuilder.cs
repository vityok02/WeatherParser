using WeatherParser.Extensions;
using WeatherParser.Models.Interfaces;

namespace WeatherParser.Services;

public class HtmlPathBuilder(HostBuilderContext hostBuilderContext) : IHtmlPathBuilder
{
    private readonly IConfiguration _configuration = hostBuilderContext.Configuration;

    public string GeneratePath(double latitude, double longitude)
    {
        var formatedLatitude = latitude.ToStringWithPoint();
        var formatedLongitude = longitude.ToStringWithPoint();
        var parsingPath = _configuration["ParsingConnection:Path"];
        var parsingParameters = _configuration["ParsingConnection:Parameters"];

        var htmlPath = $"{parsingPath}{formatedLatitude},{formatedLongitude}{parsingParameters}";

        return htmlPath;
    }
}