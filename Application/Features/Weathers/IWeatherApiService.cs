using Domain.Abstract;
using Domain.Locations;
using Domain.Weathers;

namespace Application.Features.Weathers;

public interface IWeatherApiService
{
    Task<Result<CurrentForecast>> GetNowcastAsync(Coordinates coordinates);
    Task<Result<Forecast>> GetDailyForecastAsync(Coordinates coordinates, DateTime date);
    Task<Result<Forecast>> GetMultiDayForecastAsync(Coordinates coordinates, int days);
}
