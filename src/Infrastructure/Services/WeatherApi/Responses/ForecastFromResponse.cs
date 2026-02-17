namespace Infrastructure.Services.WeatherApi.Responses;

public record ForecastFromResponse(
    ForecastDay[] ForecastDay
    );
