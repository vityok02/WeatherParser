using Domain.Abstract;
using Domain.Locations;

namespace Domain.Users;

public class User : BaseEntity
{
    public ICollection<Location>? Locations { get; }
    public Location? CurrentLocation { get; private set; }
    public long? CurrentLocationId { get; private set; }
    public bool HasLocation => CurrentLocation != null;

    public User()
    { }

    public User(long id)
    {
        Id = id;
    }

    public void SetCurrentLocation(string locationName, Coordinates coordinates)
    {
        Location location = new(locationName, coordinates);

        CurrentLocation = location;
    }

    public void SetCurrentLocation(Location location)
    {
        CurrentLocation = location;
        CurrentLocationId = location.Id;
    }
}
