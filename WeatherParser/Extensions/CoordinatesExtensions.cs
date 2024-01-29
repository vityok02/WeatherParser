using Newtonsoft.Json.Linq;
using WeatherParser.Models;

namespace WeatherParser.Extensions;

public static class CoordinatesExtensions
{
    public static Coordinates Extract(JToken locationJson)
    {
        JToken jsonCoordinates = locationJson["geometry"]!["coordinates"]!;

        double latitude = jsonCoordinates[1]!.Value<double>();
        double longitude = jsonCoordinates[0]!.Value<double>();

        return new Coordinates(latitude, longitude);
    }
}
