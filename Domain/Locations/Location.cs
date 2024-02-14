using Domain.Abstract;
using Domain.Users;

namespace Domain.Locations;

public class Location : BaseEntity
{
    public string? Name { get; private set; }
    public Coordinates? Coordinates { get; private set; }
    public ICollection<User>? Users { get; }

    public Location() { }

    public Location(string locationName, Coordinates coordinates)
    {
        Name = locationName;
        Coordinates = coordinates;
    }
}
