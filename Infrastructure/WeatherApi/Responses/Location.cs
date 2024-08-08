namespace Infrastructure.WeatherApi.Responses;

public record Location(
    string Name,
    string Region,
    string Country,
    double Lat,
    double Lon,
    string Localtime
);
