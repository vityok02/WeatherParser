namespace Infrastructure.Services.WeatherApi.Responses;

public record Hour(
    string Time,
    double Temp_c,
    double FeelsLike_C,
    double Is_Day,
    Condition Condition,
    double Wind_kph,
    double Humidity,
    double Cloud,
    int Will_it_rain,
    int Chance_of_rain,
    int Will_it_snow,
    int Chance_of_snow
    );