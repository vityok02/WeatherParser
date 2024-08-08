using Application.Interfaces;
using Domain.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherParser.Features.Geocoding.GeocodingRecords;

namespace Infrastructure.Geocoding;

public class GeocodingService : IGeocodingService
{
    private readonly ILogger<GeocodingService> _logger;
    private readonly IOptions<GeocodingConfiguration> _geocodingConfiguration;

    public GeocodingService(ILogger<GeocodingService> logger, IOptions<GeocodingConfiguration> geocodingConfiguration)
    {
        _logger = logger;
        _geocodingConfiguration = geocodingConfiguration;
    }

    public async Task<Result<Feature[]>> GetPlacesByName(
        string locationName,
        CancellationToken cancellationToken = default)
    {
        var response = await GetGeocodingResponseAsync([locationName], cancellationToken);

        var locationsResult = await GetLocationsFromResponseAsync(response, cancellationToken);
        if (locationsResult.IsFailure)
        {
            return Result<Feature[]>
                .Failure(locationsResult.Error!);
        }

        var locations = locationsResult.Value;

        return Result<Feature[]>.Success(locations!);
    }

    private async Task<HttpResponseMessage> GetGeocodingResponseAsync(
        string[] parameters,
        CancellationToken cancellationToken = default)
    {
        var cfg = _geocodingConfiguration.Value;

        var geocodingApiUrl = $"{cfg.Path}/{parameters[0]}.json?key={cfg.Token}";
        using var geocodingClient = new HttpClient();

        return await geocodingClient.GetAsync(geocodingApiUrl, cancellationToken);
    }

    private async Task<Result<Feature[]>> GetLocationsFromResponseAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var settings = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        if (!response.IsSuccessStatusCode)
        {
            var responseError = JsonSerializer.Deserialize<ErrorResponse>(content, settings)!;
            var error = new Error(responseError.StatusCode.ToString(), responseError.Message);

            _logger.LogError($"Failure response. Code: {error.Code} Description: {error.Description}", error);

            return Result<Feature[]>
                .Failure(error);
        }

        var rootObject = JsonSerializer.Deserialize<RootObject>(content, settings);
        if (rootObject is null)
        {
            _logger.LogError("Deserialized object is null");
            var error = new Error("404", "Locations not found");

            return Result<Feature[]>
                .Failure(error);
        }

        var features = rootObject.Features;
        if (features.Length == 0)
        {
            return Result<Feature[]>
                .Failure(new("404", "Locations not found"));
        }

        return Result<Feature[]>
            .Success(features);
    }
}
