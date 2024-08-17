using Domain.Abstract;
using Domain.Locations;
using Domain.Weathers;

namespace Application.Common.Interfaces.Services;

public interface IWeatherApiService
{
    Task<Result<CurrentForecast>> GetNowcastAsync(Coordinates coordinates);
    Task<Result<DailyForecast>> GetDailyForecastAsync(Coordinates coordinates, DateTime date);
    Task<Result<Forecast>> GetMultiDayForecastAsync(Coordinates coordinates, int days);
}
