namespace WeatherParser.Features.Geocoding.GeocodingRecords;

public record Feature(
    string Id,
    string Text,
    double[] Center,
    Context[] Context)
{
    public string FullPlaceName => $"{Text}, {string.Join(", ", Context.Select(c => c.Text))}";
};
