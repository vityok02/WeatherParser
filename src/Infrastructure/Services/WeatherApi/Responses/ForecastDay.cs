namespace Infrastructure.Services.WeatherApi.Responses;

public record ForecastDay(
    string Date,
    Day Day,
    Hour[] Hour
    );
