namespace Infrastructure.WeatherApi.Responses;

public record ForecastFromResponse(
    ForecastDay[] ForecastDay
    );
