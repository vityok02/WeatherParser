using Domain.Abstract;
using Domain.Locations;
using Domain.Weathers;

namespace Application.Features.Weathers;

public interface IWeatherApiService
{
    Task<Result<CurrentWeather>> GetCurrentWeatherAsync(Coordinates coordinates);
    Task<Result<Forecast>> GetForecastAsync(Coordinates coordinates, int days);
}
