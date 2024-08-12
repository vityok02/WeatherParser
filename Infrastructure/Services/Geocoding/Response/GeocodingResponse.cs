using WeatherParser.Features.Geocoding.GeocodingRecords;

namespace Infrastructure.Services.Geocoding.Response;

public record GeocodingResponse(
    LocationResponse[]? Features);