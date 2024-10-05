using Application.Common.Constants;
using Domain.Abstract;
using Domain.Translations;
using Domain.Weathers;
using System.Text;
namespace Application.Commands.Weathers.Commands.SendWeatherNow;

public static class WeatherExtensions
{
    public static string ToFormattedWeather(
        this CurrentForecast weather,
        Translation translation)
    {
        StringBuilder sb = new();

        sb
            .AppendLineIfNotNull(
                weather.CurrentTemperature,
                $"🌡{translation.Weather["Temperature"]}: {Convert.ToInt32(weather.CurrentTemperature)}{Units.Celsius}")
            .AppendLineIfNotNull(
                weather.FeelsLike,
                $"🤔{translation.Weather["FeelsLike"]}: {Convert.ToInt32(weather.FeelsLike)}{Units.Celsius}")
            .AppendLineIfNotNull(
                weather.Humidity,
                $"💧{translation.Weather["Humidity"]}: {weather.Humidity}%")
            .AppendLineIfNotNull(
                weather.WindSpeed,
                $"💨{translation.Weather["WindSpeed"]}: {weather.WindSpeed} {translation.Units["Kph"]}")
            .AppendLineIfNotNull(
                weather.WindDirection,
                $"🧭{translation.Weather["WindDirection"]}: {weather.WindDirection}")
            .AppendLineIfNotNull(
                weather.Cloud,
                $"☁{translation.Weather["Cloudiness"]}: {weather.Cloud}%")
            .AppendLineIfNotNull(
                weather.ConditionText,
                $"🌤{translation.Weather["Condition"]}: {weather.ConditionText}")
            .AppendLineIfNotNull(
                weather.ObservationTime,
                $"🕒{translation.Weather["UpdatedTime"]}: {TimeOnly.FromDateTime(weather.ObservationTime!.Value)}");

        return sb.ToString();
    }
}