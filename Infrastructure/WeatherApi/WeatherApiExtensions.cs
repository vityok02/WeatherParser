using Domain;
using Infrastructure.WeatherApi.Response;

namespace Infrastructure.WeatherApi;

public static class WeatherApiExtensions
{
    public static Weather ToWeather(this WeatherApiResponse response)
    {
        bool.TryParse(response.Current.Is_day.ToString(), out bool isDay);
        DateTime.TryParse(response.Current.Last_updated, out DateTime lastUpdated);

        return new Weather(
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
