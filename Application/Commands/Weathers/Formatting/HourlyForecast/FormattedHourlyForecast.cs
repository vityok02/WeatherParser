namespace Application.Commands.Weathers.Formatting.HourlyForecast;

public record FormattedHourlyForecast(
    string Time,
    string Temp,
    string FeelsLikeTemp,
    string Humidity,
    string WindSpeed,
    string Cloudiness,
    string Condition,
    string ConditionIconLink);
