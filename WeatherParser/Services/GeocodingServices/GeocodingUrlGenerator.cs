namespace WeatherParser.Services.GeocodingServices;

public class GeocodingUrlGenerator : IGeocodingUrlGenerator
{
    private readonly IConfiguration _configuration;

    public GeocodingUrlGenerator(HostBuilderContext hostBuilderContext)
    {
        _configuration = hostBuilderContext.Configuration;
    }

    public string GenerateUrl(string[] queryParams)
    {
        var path = _configuration["GeocodingParsingConnection:Path"];
        var token = _configuration["GeocodingParsingConnection:Token"];
        var query = string.Join(",", queryParams);

        var url = $"{path}/{query}.json?key={token}";
        return url;
    }
}
