using Application.Commands.Weathers.Formatting.HourlyForecast;
using Application.Services.HtmlProcessing;
using Domain.Weathers;

namespace Application.Services;

public interface IForecastTableGenerator
{
    string CreateDailyForecastTable(DailyForecast dailyForecast);
    string CreateMultiDayForecastTable(Forecast forecast);
}

public class ForecastTableGenerator
{
    private readonly IHtmlTableBuilder _tableBuilder;

    public ForecastTableGenerator(IHtmlTableBuilder tableBuilder)
    {
        _tableBuilder = tableBuilder;
    }

    public string CreateDailyForecastTable(DailyForecast dailyForecast)
    {
        var hourlyForecast = dailyForecast.HourlyForecast
            .Where(hf => hf.Time.Hour % 3 == 0)
            .Select(hf => hf.ToFormattedHourlyForecast());

        _tableBuilder
            .AddRow("Time", 
                hourlyForecast.Select(h => h.Time).ToArray())
            .AddRow("Temperature", 
                hourlyForecast.Select(h => h.Temp).ToArray())
            .AddRow("Feels like",
                hourlyForecast.Select(h => h.FeelsLikeTemp).ToArray())
            .AddRow("Humidity",
                hourlyForecast.Select(h => h.Humidity).ToArray())
            .AddRow("Wind speed",
                hourlyForecast.Select(h => h.WindSpeed).ToArray())
            .AddRow("Cloudiness",
                hourlyForecast.Select(h => h.Cloudiness).ToArray())
            .AddRow("Condition",
                hourlyForecast.Select(h => $"{h.Condition}").ToArray())
            .AddRow("",
                hourlyForecast.Select(h => $"<img src=\"https:{h.ConditionIconLink}\" />").ToArray());

        var table = _tableBuilder.Build();

        return table;
    }
}
