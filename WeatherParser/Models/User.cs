namespace WeatherParser.Models;

public class User : Telegram.Bot.Types.User
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public bool HasLocation() => Latitude != 0.0 && Longitude != 0.0;

    public User SetLocation(double latitude, double longitude)
    {
        return new User()
        {
            Latitude = latitude,
            Longitude = longitude
        };
    }
}
