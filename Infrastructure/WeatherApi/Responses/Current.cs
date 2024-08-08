namespace Infrastructure.WeatherApi.Responses;

public record Current(
    long Last_updated_epoch,
    string Last_updated,
    double Temp_c,
    double Temp_f,
    int Is_day,
    Condition Condition,
    double Wind_mph,
    double Wind_kph,
    int Wind_degree,
    string Wind_dir,
    double Pressure_mb,
    double Pressure_in,
    double Precip_mm,
    double Precip_in,
    int Humidity,
    int Cloud,
    double Feelslike_c,
    double Feelslike_f,
    double Vis_km,
    double Vis_miles,
    //int Uv,
    double Gust_mph,
    double Gust_kph
);
