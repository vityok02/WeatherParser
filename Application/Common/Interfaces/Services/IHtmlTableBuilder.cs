using Application.Services.HtmlProcessing;

namespace Application.Common.Interfaces.Services;

public interface IHtmlTableBuilder
{
    HtmlTableBuilder AddRow(string headColumn, string[] cols);
    string Build();
}