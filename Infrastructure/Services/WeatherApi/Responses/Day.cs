namespace Infrastructure.Services.WeatherApi.Responses;

public record Day(
    double MaxTemp_c,
    double MinTemp_c,
    double AvgTemp_c,
    double MaxWind_kph,
    double AvgHumidity,
    int Daily_will_it_rain,
    int Daily_chance_of_rain,
    int Daily_will_it_snow,
    int Daily_chance_of_snow,
    Condition Condition
    );
