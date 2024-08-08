namespace Infrastructure.WeatherApi.Responses;

public record CurrentWeatherResponse(
    Location Location,
    Current Current
);
