using Application.Commands.Weathers.Formatting.HourlyForecast;
using Application.Common.Interfaces.Localization;
using Application.Common.Interfaces.Services;
using Domain.Translations;
using Domain.Weathers;
using System.Globalization;
using System.Text;

namespace Application.Services;

public class ForecastTableGenerator
{
    private readonly IHtmlTableBuilder _tableBuilder;

    public ForecastTableGenerator(
        IHtmlTableBuilder tableBuilder,
        ITranslationService translationService)
    {
        _tableBuilder = tableBuilder;
    }

    public string CreateDailyForecastTable(
        DailyForecast dailyForecast,
        Translation translation)
    {
        var hourlyForecast = dailyForecast.HourlyForecast
            .Where(hf => hf.Time.Hour % 3 == 0)
            .Select(hf => hf.ToFormattedHourlyForecast(translation));

        _tableBuilder
            .AddRow(translation.Weather["Time"], 
                hourlyForecast.Select(h => h.Time).ToArray())
            .AddRow(translation.Weather["Temperature"],
                hourlyForecast.Select(h => h.Temp).ToArray())
            .AddRow(translation.Weather["FeelsLike"],
                hourlyForecast.Select(h => h.FeelsLikeTemp).ToArray())
            .AddRow(translation.Weather["Humidity"],
                hourlyForecast.Select(h => h.Humidity).ToArray())
            .AddRow(translation.Weather["WindSpeed"],
                hourlyForecast.Select(h => h.WindSpeed).ToArray())
            .AddRow(translation.Weather["Cloudiness"],
                hourlyForecast.Select(h => h.Cloudiness).ToArray())
            .AddRow(translation.Weather["Condition"],
                hourlyForecast.Select(h => h.Condition).ToArray())
            .AddRow(string.Empty,
                hourlyForecast.Select(h => GetImage(h.ConditionIconLink)).ToArray());

        var table = _tableBuilder.Build();

        return table;
    }

    private string GetImage(string link)
    {
        return $"<img src=\"https:{link}\" />";
    }
}
