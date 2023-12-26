namespace WeatherParser.Models;

public class User : Telegram.Bot.Types.User
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public bool HasLocation() => Latitude != 0.0 && Longitude != 0.0;

    public void SetLocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
