using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Services.HtmlProcessing;
using Domain.Translations;
using Domain.Weathers;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TableConverter
{
    private readonly IStyleLoader _styleLoader;
    private readonly HtmlToImageConverter _converter;
    private readonly ForecastTableGenerator _tableGenerator;
    private readonly ILogger<TableConverter> _logger;

    public TableConverter(
        HtmlToImageConverter converter,
        IStyleLoader styleLoader,
        ForecastTableGenerator tableGenerator,
        ILogger<TableConverter> logger)
    {
        _converter = converter;
        _tableGenerator = tableGenerator;
        _styleLoader = styleLoader;
        _logger = logger;
    }

    public FileWrapper ToTable(DailyForecast forecast, Translation translation)
    {
        string table = _tableGenerator.CreateDailyForecastTable(forecast, translation);
        var path = Path.Combine(AppContext.BaseDirectory, "table.css");
        _logger.LogInformation("Getting file by path: @{path}", path);
        string styles = _styleLoader.LoadStyles(path);

        HtmlBuilder htmlBuilder = new();

        var html = htmlBuilder
            .SetStyles(styles)
            .AddHtml(table)
            .Build();

        _logger.LogInformation($"Generated html code\n{html}");

        return _converter.ConvertToImage(html);
    }
}
