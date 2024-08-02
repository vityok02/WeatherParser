using Domain.Abstract;
using Domain.Locations;
using System.Collections.ObjectModel;

namespace Domain.Users;

public class User : BaseEntity
{
    public ICollection<Location> Locations { get; } = new Collection<Location>();
    public Location? CurrentLocation { get; private set; }
    public long? CurrentLocationId { get; private set; }

    public User()
    { }

    public User(long id)
    {
        Id = id;
    }

    public bool HasLocation() => CurrentLocation != null;

    public void SetCurrentLocation(Location location)
    {
        if (!Locations.Any(l => l.Name == location.Name))
        {
            Locations.Add(location);
        }

        CurrentLocation = location;
    }
}
