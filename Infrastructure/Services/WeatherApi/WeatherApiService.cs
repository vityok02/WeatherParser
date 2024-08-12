using Application.Features.Weathers;
using Domain.Abstract;
using Domain.Locations;
using Domain.Weathers;
using Infrastructure.Services.WeatherApi;
using Infrastructure.Services.WeatherApi.Responses;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.Services.WeatherApi;

public class WeatherApiService : IWeatherApiService
{
    private readonly HttpClient _client;
    private readonly WeatherApiConfiguration _configuration;
    private const string PathPrefix = "/v1";

    public WeatherApiService(HttpClient client, IOptions<WeatherApiConfiguration> options)
    {
        _client = client;
        _configuration = options.Value;
    }

    public async Task<Result<CurrentWeather>> GetCurrentWeatherAsync(Coordinates coordinates)
    {

        QueryBuilder qb = new(
            new List<KeyValuePair<string, string>>
            {
                new("q", coordinates.ToString()),
                new("key", _configuration.Key),
            });

        var path = $"{PathPrefix}/current.json";

        try
        {
            var weatherResponse = await _client
                .GetFromJsonAsync<CurrentWeatherResponse>(path + qb);

            if (weatherResponse is null)
            {
                return Result<CurrentWeather>.Failure("WeatherService.FailedToGetWeatherResponse", "Could not get weather from response");
            }

            var weather = weatherResponse.ToWeather();

            return Result<CurrentWeather>.Success(weather);
        }
        catch (Exception ex)
        {
            return Result<CurrentWeather>.Failure("WeatherService.FailedToGetWeatherResponse", ex.Message);
        }
    }

    public async Task<Result<Forecast>> GetForecastAsync(Coordinates coordinates, int days)
    {
        var path = $"{PathPrefix}/forecast.json";

        QueryBuilder qb = new(
            new List<KeyValuePair<string, string>>
            {
                new("q", coordinates.ToString()),
                new("days", days.ToString()),
                new("key", _configuration.Key),
            });

        try
        {
            var forecastResponse = await _client.GetFromJsonAsync<ForecastResponse>(path + qb);

            if (forecastResponse is null)
            {
                return Result<Forecast>.Failure("WeathersService.FailedToGetForecastResponse");
            }

            var forecast = forecastResponse.ToForecast();

            return Result<Forecast>.Success(forecast);
        }
        catch (Exception ex)
        {
            return Result<Forecast>.Failure("WeatherService.FailedToGetForecastResponse", ex.Message);
        }
    }
}
