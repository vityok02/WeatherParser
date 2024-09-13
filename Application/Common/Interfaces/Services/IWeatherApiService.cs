using Domain.Abstract;
using Domain.Locations;
using Domain.Weathers;

namespace Application.Common.Interfaces.Services;

public interface IWeatherApiService
{
    Task<Result<CurrentForecast>> GetNowcastAsync(Coordinates coordinates, string languageCode);
    Task<Result<DailyForecast>> GetDailyForecastAsync(Coordinates coordinates, string languageCode, DateTime date);
    Task<Result<Forecast>> GetMultiDayForecastAsync(Coordinates coordinates, string languageCode, int days);
}
