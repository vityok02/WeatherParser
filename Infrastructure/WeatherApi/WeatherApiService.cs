using Application.Features.Weathers;
using Domain;
using Domain.Locations;
using Infrastructure.WeatherApi;
using Infrastructure.WeatherApi.Response;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Weathers;

public class WeatherApiService : IWeatherApiService
{
    private readonly HttpClient _client;
    private readonly WeatherApiConfiguration _configuration;

    public WeatherApiService(HttpClient client, IOptions<WeatherApiConfiguration> options)
    {
        _client = client;
        _configuration = options.Value;
    }

    public async Task<Result<Weather>> GetCurrentWeatherAsync(Coordinates coordinates)
    {
        var query = new List<KeyValuePair<string, string>>
        {
            new("q", coordinates.ToString()),
            new("key", _configuration.Key),
        };

        QueryBuilder qb = new QueryBuilder(query);
        var path = "/v1/current.json";

        var response = await _client.GetAsync(path + qb);

        if (!response.IsSuccessStatusCode)
        {
            return Result<Weather>.Failure("Weathers.UnsuccessfulResponse", $"Status code: {response.StatusCode}");
        }

        try
        {
            var weatherResponse = JsonSerializer
                .Deserialize<WeatherApiResponse>(response.Content.ReadAsStringAsync().Result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (weatherResponse is null)
            {
                return Result<Weather>.Failure("Weathers.FailedToGet", "Could not get weather from response");
            }

            var weather = weatherResponse.ToWeather();

            return Result<Weather>.Success(weather);
        }
        catch (Exception ex)
        {
            return Result<Weather>.Failure("Weather.FailedToGet", ex.Message);
        }
    }
}
