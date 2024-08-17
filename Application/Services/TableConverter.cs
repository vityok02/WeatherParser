using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Services.HtmlProcessing;
using Domain.Weathers;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TableConverter
{
    private readonly IStyleLoader _styleLoader;
    private readonly HtmlToImageConverter _converter;
    private readonly ForecastTableGenerator _tableGenerator;
    private readonly ILogger<TableConverter> _logger;
    //abstract htmlBuilder
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

    public FileWrapper ToTable(DailyForecast forecast)
    {
        string table = _tableGenerator.CreateDailyForecastTable(forecast);
        string styles = _styleLoader.LoadStyles("table.css");

        HtmlBuilder htmlBuilder = new HtmlBuilder();

        var html = htmlBuilder
            .SetStyles(styles)
            .AddHtml(table)
            .Build();

        _logger.LogInformation($"Created html code\n{html}");

        return _converter.ConvertToImage(html);
    }
}
