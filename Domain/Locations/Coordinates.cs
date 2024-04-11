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
        return Latitude.ToString().Replace(',', '.') + ',' +
            Longitude.ToString().Replace(',', '.');
    }
}