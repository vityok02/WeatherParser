using Domain.Abstract;

namespace Domain.Locations;

public class Coordinates : BaseEntity
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public Coordinates()
    {

    }

    public Coordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public override string ToString()
    {
        return
            $"{Longitude.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture)}," +
            $"{Latitude.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture)}";
    }
}