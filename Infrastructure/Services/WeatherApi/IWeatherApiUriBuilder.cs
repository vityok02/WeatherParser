using Domain.Locations;

namespace Infrastructure.Services.WeatherApi;

public interface IWeatherApiUriBuilder
{
    string BuildMultiDayForecastPath(Coordinates coordinates, string languageCode, int days);
    string BuildNowcastPath(Coordinates coordinates, string languageCode);
    string BuildDailyForecastPath(Coordinates coordinates, string languageCode, DateTime date);
}
