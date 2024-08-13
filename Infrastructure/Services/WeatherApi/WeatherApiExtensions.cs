using Domain.Weathers;

namespace Infrastructure.Services.WeatherApi;

public static class WeatherApiExtensions
{
    public static CurrentForecast ToWeather(this Responses.CurrentForecastResponse response)
    {
        bool.TryParse(response.Current.Is_day.ToString(), out bool isDay);
        DateTime.TryParse(response.Current.Last_updated, out DateTime lastUpdated);

        return new CurrentForecast(
            response.Current.Temp_c,
            response.Current.Wind_kph,
            response.Current.Wind_dir,
            response.Current.Humidity,
            response.Current.Cloud,
            response.Current.Condition.Text,
            isDay,
            lastUpdated);
    }
}
