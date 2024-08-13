using Domain.Abstract;

namespace Infrastructure.Services.WeatherApi;

public static class WeatherServiceErrors
{
    public readonly static Error ResponseNull = new(
        "WeatherApiService.NullResponse",
        "Could not get weather from response");

    public readonly static Error FailedWeather = new(
        "WeatherApiService.FailedToGetWeather",
        "Failed to get weather");

    public readonly static Error FailedForecast = new(
        "WeatherApiService.FailedToGetForecast",
        "Failed to get forecast");
}