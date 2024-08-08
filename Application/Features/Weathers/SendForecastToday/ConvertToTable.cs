using CoreHtmlToImage;
using Domain.Weathers;
using System.Text;
using Telegram.Bot.Types;

namespace Application.Features.Weathers.SendForecastToday;

public static class ConvertToTable
{
    public static InputFile ToTable(Forecast forecast)
    {
        var hourlyForecast = forecast.DailyForecast
            .First().HourlyForecast
            .Where(h => h.Time.Hour % 3 == 0).ToArray();

        string table = CreateTable(hourlyForecast);

        HtmlBuilder htmlBuilder = new();

        string styles = System.IO.File.ReadAllText("table.css", Encoding.UTF8);

        var html = htmlBuilder
            .SetStyles(styles)
            .AddHtml(table)
            .Build();

        Console.WriteLine(html);

        InputFile file = ConvertToImage(html);
        return file;
    }

    private static InputFile ConvertToImage(string html)
    {
        var converter = new HtmlConverter();
        var bytes = converter.FromHtmlString(html);
        var stream = new MemoryStream(bytes);

        return InputFile.FromStream(stream);
    }

    private static string CreateTable(HourlyForecast[] hourlyForecast)
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

public class HtmlBuilder
{
    private StringBuilder _builder = new();

    public HtmlBuilder SetStyles(string styles)
    {
        _builder.AppendLine("<style>");
        _builder.Append(styles);
        _builder.AppendLine("</style>");

        return this;
    }

    public HtmlBuilder AddHtml(string html)
    {
        _builder.AppendLine(html);
        return this;
    }

    public string Build()
    {
        return _builder.ToString();
    }
}

public class HtmlTableBuilder
{
    private StringBuilder _builder = new("<table>");
    private List<string> rows = [];

    public HtmlTableBuilder AddRow(string headColumn, string[] cols)
    {
        StringBuilder row = new();
        row.AppendLine("<tr>");

        row.AppendLine($"<th>{headColumn}</th>");

        foreach (var col in cols)
        {
            row.AppendLine($"<td>{col}</td>");
        }

        row.AppendLine("</tr>");
        rows.Add(row.ToString());

        return this;
    }

    public string Build()
    {
        _builder.AppendLine($"{string.Join("", rows)}</table>");

        return _builder.ToString();
    }
}
