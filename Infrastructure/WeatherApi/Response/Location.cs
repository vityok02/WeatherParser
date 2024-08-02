namespace Infrastructure.WeatherApi.Response;

public record Location(
    string Name,
    string Region,
    string Country,
    double Lat,
    double Lon,
    string Localtime
);
