using Domain.Abstract;
using Domain.Users;

namespace Domain.Locations;

public class Location : BaseEntity
{
    public string? Name { get; private set; } = string.Empty;
    public ICollection<User>? Users { get; }
    public Coordinates? Coordinates { get; private set; }
    public long CoordinatesId { get; private set; }

    public Location() { }

    public Location(string locationName, Coordinates coordinates)
    {
        Name = locationName ?? "Undefined";
        Coordinates = coordinates;
    }
}
