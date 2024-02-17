namespace WeatherParser.Features.Geocoding.GeocodingRecords;

public record ErrorResponse(string Message, string Error, int StatusCode);
