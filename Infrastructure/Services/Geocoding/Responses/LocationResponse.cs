using Infrastructure.Services.Geocoding.Responses;

namespace WeatherParser.Features.Geocoding.GeocodingRecords;

public record LocationResponse(
    string Id,
    string Text,
    double[] Center,
    LocationContext[] Context)
{
    public string FullPlaceName => $"{Text}, {string.Join(", ", Context.Select(c => c.Text))}";
};
