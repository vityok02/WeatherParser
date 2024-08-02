namespace Infrastructure.WeatherApi.Response;

public record WeatherApiResponse(
    Location Location,
    Current Current
);
