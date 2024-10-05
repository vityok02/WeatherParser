using WeatherParser.Features.Geocoding.GeocodingRecords;

namespace Infrastructure.Services.Geocoding.Responses;

public record GeocodingResponse(
    LocationResponse[]? Features);