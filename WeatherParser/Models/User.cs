namespace WeatherParser.Models;

public class User : Telegram.Bot.Types.User
{
    public ICollection<Location>? Locations { get; }
    public Location? CurrentLocation { get; private set; }
    public long? CurrentLocationId { get; private set; }
    public bool HasLocation => CurrentLocation != null;

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
