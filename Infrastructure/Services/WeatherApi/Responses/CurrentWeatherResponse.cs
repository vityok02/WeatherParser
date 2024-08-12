namespace Infrastructure.Services.WeatherApi.Responses;

public record CurrentWeatherResponse(
    Location Location,
    Current Current
);
