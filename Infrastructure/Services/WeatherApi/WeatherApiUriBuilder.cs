using Domain.Locations;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.WeatherApi;

public class WeatherApiUriBuilder : IWeatherApiUriBuilder
{
    private readonly WeatherApiConfiguration _config;

    public WeatherApiUriBuilder(IOptions<WeatherApiConfiguration> config)
    {
        _config = config.Value;
    }

    private const string Prefix = "/v1";

    public string BuildNowcastPath(Coordinates coordinates)
    {
        QueryBuilder query = new(
            new List<KeyValuePair<string, string>>
            {
                new("q", coordinates.ToString()),
                new("key", _config.Key),
            });

        return $"{Prefix}/current.json{query}";
    }

    public string BuildMultiDayForecastPath(Coordinates coordinates, int days)
    {
        QueryBuilder query = new(
            new List<KeyValuePair<string, string>>
            {
                new("q", coordinates.ToString()),
                new("days", days.ToString()),
                new("key", _config.Key),
            });

        return $"{Prefix}/forecast.json{query}";
    }

    public string BuildDailyForecastPath(Coordinates coordinates, DateTime date)
    {
        QueryBuilder query = new(
            new List<KeyValuePair<string, string>>
            {
                new("q", coordinates.ToString()),
                new("days", "1"),
                new("dt", date.ToString("yyyy-MM-dd")),
                new("key", _config.Key),
            });

        return $"{Prefix}/forecast.json{query}";
    }
}
