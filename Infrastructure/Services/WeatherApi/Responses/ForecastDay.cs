namespace Infrastructure.Services.WeatherApi.Responses;

public record ForecastDay(
    Day Day,
    Hour[] Hour
    );
