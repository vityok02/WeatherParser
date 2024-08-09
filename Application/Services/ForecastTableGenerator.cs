using Application.Services.HtmlProcessing;
using Domain.Weathers;

namespace Application.Services;

public class ForecastTableGenerator
{
    public string CreateTable(HourlyForecast[] hourlyForecast)
    {
        HtmlTableBuilder tableBuilder = new();

        tableBuilder.AddRow("Time",
            hourlyForecast.Select(h => h.Time.ToShortTimeString()).ToArray());

        tableBuilder.AddRow("Temperature",
            hourlyForecast.Select(h => $"{(int)h.Temp}C").ToArray());

        tableBuilder.AddRow("Feels like",
            hourlyForecast.Select(h => $"{(int)h.FeelsLikeTemp}C").ToArray());

        tableBuilder.AddRow("Humidity",
            hourlyForecast.Select(h => $"{(int)h.Humidity}%").ToArray());

        tableBuilder.AddRow("Wind speed",
            hourlyForecast.Select(h => $"{(int)h.WindSpeed} kph").ToArray());

        tableBuilder.AddRow("Cloudiness",
            hourlyForecast.Select(h => $"{(int)h.Cloud}%").ToArray());

        tableBuilder.AddRow("Condition",
            hourlyForecast.Select(h => $"{h.Condition.Text}").ToArray());

        tableBuilder.AddRow("",
            hourlyForecast.Select(h => $"<img src=\"https:{h.Condition.IconLink}\" />").ToArray());

        var table = tableBuilder.Build();

        return table;
    }
}
