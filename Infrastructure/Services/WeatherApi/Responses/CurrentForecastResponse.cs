namespace Infrastructure.Services.WeatherApi.Responses;

public record CurrentForecastResponse(
    Location Location,
    Current Current
);
