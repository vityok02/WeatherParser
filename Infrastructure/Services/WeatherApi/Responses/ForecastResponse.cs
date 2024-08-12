namespace Infrastructure.Services.WeatherApi.Responses;

public record ForecastResponse(
    Location Location,
    Current Current,
    ForecastFromResponse Forecast
    );
