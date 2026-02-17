using Domain.Weathers;
using Infrastructure.Services.WeatherApi.Responses;
using Forecast = Domain.Weathers.Forecast;

namespace Infrastructure.Services.WeatherApi;

public static class ForecastExtensions
{
    public static Forecast ToForecast(this ForecastResponse response)
    {
        IEnumerable<DailyForecast> dailyForecasts = response.Forecast.ForecastDay
            .Select(d =>
                d.ToDailyForecast());

        return new Forecast(dailyForecasts);
    }

    public static DailyForecast ToDailyForecast(this ForecastDay forecastDay)
    {
        var day = forecastDay.Day;

        return new(
            Convert.ToDateTime(forecastDay.Date),
            day.AvgTemp_c,
            day.MinTemp_c,
            day.MaxTemp_c,
            day.AvgTemp_c,
            day.AvgHumidity,
            new Precipitation(
                Convert.ToBoolean(day.Daily_will_it_rain),
                day.Daily_chance_of_rain,
                Convert.ToBoolean(day.Daily_will_it_snow),
                day.Daily_chance_of_snow),
            day.Condition.ToCondition(),
            forecastDay.Hour.Select(h => h.ToHourlyForecast()));
    }

    public static Domain.Weathers.Condition ToCondition(this Responses.Condition condition)
    {
        return new Domain.Weathers.Condition(condition.Text, condition.Icon);
    }

    public static HourlyForecast ToHourlyForecast(this Hour hour)
    {
        return new HourlyForecast(
            Convert.ToDateTime(hour.Time),
            hour.Temp_c,
            hour.FeelsLike_C,
            hour.Condition.ToCondition(),
            hour.Wind_kph,
            hour.Humidity,
            hour.Cloud);
    }
}
