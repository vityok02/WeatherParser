using Domain.Abstract;

namespace Infrastructure.Services.Geocoding;

public static class GeocodingErrors
{
    public static readonly Error LocationsNull = new(
        "Locations.Null",
        "Response content has no locations");

    public static readonly Error HttpRequestError = new(
        "HttpRequest.Failed",
        "Http request failed");

    public static readonly Error DeserializationError = new(
        "JsonDeserialization.Failed",
        "Failed to deserialize the object");

    public static readonly Error UnexpectedError = new(
        "Unexpected.Error",
        "An unexpected error occurred");
}
