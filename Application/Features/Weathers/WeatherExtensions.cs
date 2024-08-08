using System.Text;
using Domain.Abstract;
using Domain.Weathers;

namespace Application.Features.Weathers;

public static class WeatherExtensions
{
    public static string ToString(this CurrentWeather weather)
    {
        StringBuilder sb = new StringBuilder();

        sb
            .AppendLineIfNotNull(weather.CurrentTemperature, $"Temperature: {weather.CurrentTemperature}")
            .AppendLineIfNotNull(weather.Humidity, $"Humidity: {weather.Humidity}")
            .AppendLineIfNotNull(weather.WindSpeed, $"Wind speed: {weather.WindSpeed}")
            .AppendLineIfNotNull(weather.WindDirection, $"Wind directoin: {weather.WindDirection}")
            .AppendLineIfNotNull(weather.Cloud, $"Cloudiness: {weather.Cloud}%")
            .AppendLineIfNotNull(weather.ConditionText, $"Description: {weather.ConditionText}")
            .AppendLineIfNotNull(weather.ObservationTime, $"Update time: {TimeOnly.FromDateTime(weather.ObservationTime!.Value)}");

        return sb.ToString();
    }

    private static string GetEmoji(double temperature)
    {
        return temperature switch
        {
            > 30 => "🟥",
            > 20 => "🟧",
            > 10 => "🟨",
            > 0 => "🟩",
            _ => "🟦",
        };
    }
}
