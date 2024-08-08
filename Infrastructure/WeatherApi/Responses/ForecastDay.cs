namespace Infrastructure.WeatherApi.Responses;

public record ForecastDay(
    Day Day,
    Hour[] Hour
    );
