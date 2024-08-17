namespace Application.Commands.Weathers.Formatting.HourlyForecast;

public static class FormattedHourlyForecastExtension
{
    public static FormattedHourlyForecast ToFormattedHourlyForecast(this Domain.Weathers.HourlyForecast forecast)
    {
        // TODO: Celsius symbol
        string celsius = "C";

        return new FormattedHourlyForecast(
            forecast.Time.ToShortTimeString(),
            $"{Convert.ToInt32(forecast.Temp)}{celsius}",
            $"{Convert.ToInt32(forecast.FeelsLikeTemp)}{celsius}",
            $"{Convert.ToInt32(forecast.Humidity)}%",
            $"{Convert.ToInt32(forecast.WindSpeed)} kph",
            $"{Convert.ToInt32(forecast.Cloud)}%",
            forecast.Condition.Text,
            forecast.Condition.IconLink
            );
    }
}
