using Domain;
using Domain.Locations;

namespace Application.Features.Weathers;

public interface IWeatherApiService
{
    Task<Result<Weather>> GetCurrentWeatherAsync(Coordinates coordinates);
}
