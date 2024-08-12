using Domain.Weathers;

namespace Infrastructure.Services.WeatherApi;

public static class WeatherApiExtensions
{
    public static CurrentWeather ToWeather(this Responses.CurrentWeatherResponse response)
    {
        bool.TryParse(response.Current.Is_day.ToString(), out bool isDay);
        DateTime.TryParse(response.Current.Last_updated, out DateTime lastUpdated);

        return new CurrentWeather(
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
