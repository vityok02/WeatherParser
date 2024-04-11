using Domain;
using Domain.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherParser.Features.Geocoding.GeocodingRecords;

namespace Infrastructure.Weathers.WeatherApi;

public class WeatherApiService
{
    private readonly WeatherApiConfiguration _configuration;
    private readonly ILogger _logger;

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public WeatherApiService(IOptions<WeatherApiConfiguration> configuration, ILogger logger)
    {
        _configuration = configuration.Value;
        _logger = logger;
    }

    public async Task<Result<CurrentWeather>> GetCurrentWeather(string parameter, CancellationToken cancellationToken)
    {
        HttpClient client = new HttpClient();

        var path = PathBuilder.BuildPath(_configuration.Path + $"{ApiMethods.Current}.json", parameter, _configuration.Token);

        var result = await GetResponse(client, path, cancellationToken);
        if (result.IsFailure)
        {
            return Result<CurrentWeather>.Failure(result.Error!);
        }

        var weather = JsonSerializer.Deserialize<CurrentWeather>(result.Value!);

        if (weather is null)
        {
            _logger.LogError("Weather is null");
            return Result<CurrentWeather>.Failure(new Error("Weather is null"));
        }

        return Result<CurrentWeather>.Success(weather);
    }

    private async Task<Result<string>> GetResponse(HttpClient client, string path, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await client.GetAsync(path);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var responseError = JsonSerializer
                .Deserialize<ErrorResponse>(content, _serializerOptions)!;

            _logger.LogError(responseError.Message);

            return Result<string>
                .Failure(new Error($"{responseError.StatusCode}, {responseError.Message}"));
        }

        return Result<string>.Success(content);
    }
}