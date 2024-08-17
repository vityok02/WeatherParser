using Application.Common.Interfaces;
using Domain.Abstract;
using Domain.Locations;
using Infrastructure.Services.Geocoding.Response;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Services.Geocoding;

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

    public async Task<Result<Location[]>> GetPlacesByName(
        string locationName,
        CancellationToken cancellationToken = default)
    {
        var url = GetPath(locationName);

        try
        {
            var geocodingResponse = await _client
                .GetFromJsonAsync<GeocodingResponse>(url, cancellationToken);

            var locations = geocodingResponse?.Features?
                .Select(s => s.ToAppLocation());

            if (locations is null)
            {
                return Result<Location[]>.Failure(GeocodingErrors.LocationsNull);
            }

            return Result<Location[]>
                .Success(locations.ToArray());
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP Request Error: {@description}",
                GeocodingErrors.HttpRequestError.Description);

            return Result<Location[]>.Failure(GeocodingErrors.HttpRequestError);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Deserialization Error: {description}",
                GeocodingErrors.DeserializationError.Description);

            return Result<Location[]>.Failure(GeocodingErrors.DeserializationError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected Error: {description}", 
                GeocodingErrors.UnexpectedError.Description);

            return Result<Location[]>.Failure(GeocodingErrors.UnexpectedError);
        }
    }

    private Uri GetPath(string locationName)
    {
        QueryBuilder qb = new(
            new List<KeyValuePair<string, string>>
            {
                new( "key", _cfg.Token )
            });

        return new Uri($"{_cfg.Path}/{locationName}.json{qb}");
    }
}
