using Application.Common.Interfaces.Services;
using Domain.Abstract;
using Domain.Locations;
using Domain.Weathers;
using Infrastructure.Services.WeatherApi.Responses;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Services.WeatherApi;

public class WeatherApiService : IWeatherApiService
{
    private readonly HttpClient _client;
    private readonly ILogger<WeatherApiService> _logger;
    private readonly IWeatherApiUriBuilder _uriBuilder;

    public WeatherApiService(
        HttpClient client,
        ILogger<WeatherApiService> logger,
        IWeatherApiUriBuilder uriBuilder)
    {
        _client = client;
        _logger = logger;
        _uriBuilder = uriBuilder;
    }

    public async Task<Result<CurrentForecast>> GetNowcastAsync(
        Coordinates coordinates, string languageCode)
    {
        var uri = _uriBuilder
            .BuildNowcastPath(coordinates, languageCode);
        var nowcastResponse = await 
            GetForecastResponseAsync<CurrentForecastResponse>(uri);

        if (nowcastResponse.IsFailure)
        {
            return Result<CurrentForecast>
                .Failure(nowcastResponse.Error!);
        }

        var nowcast = nowcastResponse.Value!
            .ToWeather();
        return Result<CurrentForecast>
            .Success(nowcast);
    }

    public async Task<Result<DailyForecast>> GetDailyForecastAsync(
        Coordinates coordinates, string languageCode, DateTime date)
    {
        var uri = _uriBuilder
            .BuildDailyForecastPath(coordinates, languageCode, date);
        var forecastResponse = await 
            GetForecastResponseAsync<ForecastResponse>(uri);

        if (forecastResponse.IsFailure)
        {
            return Result<DailyForecast>
                .Failure(forecastResponse.Error!);
        }

        var forecast = forecastResponse.Value!
            .Forecast.ForecastDay
            .Where(f => f.Date == date.ToString("yyyy-MM-dd"))
            .FirstOrDefault();

        if (forecast is null)
        {
            _logger.LogInformation("Variable is null: {@forecast}",
                forecast);
            return Result<DailyForecast>.Failure(WeatherServiceErrors.ForecastNull);
        }

        return Result<DailyForecast>.Success(forecast.ToDailyForecast());
    }

    public async Task<Result<Forecast>> GetMultiDayForecastAsync(
        Coordinates coordinates, string languageCode, int days)
    {
        var uri = _uriBuilder
            .BuildMultiDayForecastPath(coordinates, languageCode, days);
        var forecastResponse = await 
            GetForecastResponseAsync<ForecastResponse>(uri);

        if (forecastResponse.IsFailure)
        {
            return Result<Forecast>
                .Failure(forecastResponse.Error!);
        }

        var forecast = forecastResponse.Value!
            .ToForecast();
        return Result<Forecast>
            .Success(forecast);
    }

    private async Task<Result<T>> GetForecastResponseAsync<T>(string uri)
    {
        try
        {
            var forecastResponse = await _client
                .GetFromJsonAsync<T>(uri);

            if (forecastResponse is null)
            {
                return Result<T>.Failure(WeatherServiceErrors.ResponseNull);
            }

            return Result<T>.Success(forecastResponse);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Description: {@description}",
                ex.Message);

            return Result<T>.Failure(ClientErrors.HttpResponseError);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Description: {@description}",
                ex.Message);

            return Result<T>.Failure(ClientErrors.JsonError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Description: {@description}",
                ex.Message);

            return Result<T>.Failure(ClientErrors.UnexpectedError);
        }
    }
}
