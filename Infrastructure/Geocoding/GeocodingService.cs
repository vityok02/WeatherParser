using Application.Interfaces;
using Domain.Abstract;
using Domain.Locations;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherParser.Features.Geocoding.GeocodingRecords;

namespace Infrastructure.Geocoding;

public class GeocodingService : IGeocodingService
{
    private readonly ILogger<GeocodingService> _logger;
    private readonly GeocodingConfiguration _cfg;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public GeocodingService(
        ILogger<GeocodingService> logger,
        IOptions<GeocodingConfiguration> geocodingConfiguration,
        HttpClient client)
    {
        _logger = logger;
        _cfg = geocodingConfiguration.Value;
        _client = client;
    }

    public async Task<Result<string>> GetPlaceName(
        Coordinates coordinates, CancellationToken cancellationToken)
    {
        return Result<string>.Success(string.Empty);
    }

    public async Task<Result<Feature[]>> GetPlacesByName(
        string locationName,
        CancellationToken cancellationToken = default)
    {
        string url = GetPath(locationName);

        var response = await _client.GetAsync(url, cancellationToken);

        var locationsResult = await GetLocationsResult(response, cancellationToken);

        if (locationsResult.IsFailure)
        {
            return Result<Feature[]>.Failure(locationsResult.Error!);
        }

        _logger.LogInformation("The location successfully get");

        return Result<Feature[]>
            .Success(locationsResult.Value);
    }

    private string GetPath(string locationName)
    {
        QueryBuilder qb = new(
            new List<KeyValuePair<string, string>>
            {
                new( "key", _cfg.Token )
            });

        var url = $"{_cfg.Path}/{locationName}.json{qb}";
        return url;
    }

    private async Task<Result<Feature[]>> GetLocationsResult(
        HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStreamAsync(cancellationToken);

        try
        {
            if (!response.IsSuccessStatusCode)
            {
                var responseError = await JsonSerializer
                    .DeserializeAsync<ErrorResponse>(content, _jsonOptions, cancellationToken);

                return HandleError<Feature[]>("Response.UnsuccessStatusCode", responseError?.Message);
            }

            var deserializedObject = await JsonSerializer
                .DeserializeAsync<RootObject>(content, _jsonOptions, cancellationToken);

            var features = deserializedObject?.Features;

            if (features is null)
            {
                HandleError<Feature[]>("Features.IsNull");
            }

            return Result<Feature[]>.Success(features);
        }
        catch (Exception ex)
        {
            return HandleError<Feature[]>(ex, "Failed to deserialize object");
        }
    }

    private Result<T> HandleError<T>(Exception ex, string code, string? message = null)
    {
        _logger.LogError(ex, message);
        return Result<T>.Failure(code, message);
    }

    private Result<T> HandleError<T>(string code, string? description = null)
    {
        _logger.LogError($"{code}. {description}");
        return Result<T>.Failure(code, description);
    }
}
