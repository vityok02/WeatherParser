﻿using Forecast = Domain.Weathers.Forecast;
using Infrastructure.WeatherApi.Responses;
using Domain.Weathers;

namespace Infrastructure.WeatherApi;

public static class ForecastExtensions
{
    public static Forecast ToForecast(this ForecastResponse response)
    {
        IEnumerable<DailyForecast> dailyForecasts = response.Forecast.ForecastDay
            .Select(d =>  
                d.ToDailyForecast());

        return new Forecast(dailyForecasts);
    }

    private static DailyForecast ToDailyForecast(this ForecastDay forecastDay)
    {
        var day = forecastDay.Day;

        return new(
            Convert.ToDateTime(day.Date),
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

    private static Domain.Weathers.Condition ToCondition(this Responses.Condition condition)
    {
        return new Domain.Weathers.Condition(condition.Text, condition.Icon);
    }

    private static HourlyForecast ToHourlyForecast(this Hour hour)
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
