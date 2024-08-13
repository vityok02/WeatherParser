using Domain.Locations;

namespace Infrastructure.Services.WeatherApi;

public interface IWeatherApiUriBuilder
{
    string BuildMultiDayForecastPath(Coordinates coordinates, int days);
    string BuildNowcastPath(Coordinates coordinates);
    string BuildDailyForecastPath(Coordinates coordinates, DateTime date);
}
