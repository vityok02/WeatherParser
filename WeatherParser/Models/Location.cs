using WeatherParser.Abstract;

namespace WeatherParser.Models;

public class Location : BaseEntity
{
    public string? Name { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public ICollection<User>? Users { get; }

    public Location() { }

    public Location(string locationName, Coordinates coordinates)
    {
        Name = locationName;
        Latitude = coordinates.Latitude;
        Longitude = coordinates.Longitude;
    }
}
