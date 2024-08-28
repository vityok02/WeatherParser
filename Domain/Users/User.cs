using Domain.Abstract;
using Domain.Languages;
using Domain.Locations;

namespace Domain.Users;

public class User : BaseEntity
{
    public ICollection<Location> Locations { get; } = [];
    public Location? CurrentLocation { get; private set; }
    public long? CurrentLocationId { get; private set; }
    public Language Language { get; private set; } = null!;
    public long LanguageId { get; private set; }

    public User()
    { }

    public User(long id, long languageId)
    {
        Id = id;
        LanguageId = languageId;
    }

    public bool HasLocation() => CurrentLocation != null;

    public void SetCurrentLocation(Location location)
    {
        var existingLocation = Locations.FirstOrDefault(l => 
            l.Name == location.Name
            && l.Coordinates.Longitude == location.Coordinates.Longitude
            && l.Coordinates.Latitude == location.Coordinates.Latitude);

        if (existingLocation is null)
        {
            Locations.Add(location);
            CurrentLocation = location;
        }
        else
        {
            CurrentLocation = existingLocation;
        }
    }

    public void UpdateLanguage()
    {
        Language = Language; 
    }
}
