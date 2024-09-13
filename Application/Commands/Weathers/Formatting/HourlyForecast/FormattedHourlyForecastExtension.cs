namespace Application.Commands.Weathers.Formatting.HourlyForecast;

using Application.Common.Constants;
using Domain.Translations;
using HourlyForecast = Domain.Weathers.HourlyForecast;

public static class FormattedHourlyForecastExtension
{
    public static FormattedHourlyForecast ToFormattedHourlyForecast(
        this HourlyForecast forecast,
        Translation translation)
    {
        // TODO: Celsius symbol
        string celsius = "C";

        return new FormattedHourlyForecast(
            forecast.Time.ToShortTimeString(),
            $"{Convert.ToInt32(forecast.Temp)}{celsius}",
            $"{Convert.ToInt32(forecast.FeelsLikeTemp)}{celsius}",
            $"{Convert.ToInt32(forecast.Humidity)}%",
            $"{Convert.ToInt32(forecast.WindSpeed)} {translation.Units[Units.Kph]}",
            $"{Convert.ToInt32(forecast.Cloud)}%",
            forecast.Condition.Text,
            forecast.Condition.IconLink);
    }
}
