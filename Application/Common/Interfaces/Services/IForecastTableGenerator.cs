using Domain.Weathers;

namespace Application.Common.Interfaces.Services;

public interface IForecastTableGenerator
{
    string CreateDailyForecastTable(DailyForecast dailyForecast);
    string CreateMultiDayForecastTable(Forecast forecast);
}