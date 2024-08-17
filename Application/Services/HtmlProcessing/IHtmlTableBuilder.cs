namespace Application.Services.HtmlProcessing;

public interface IHtmlTableBuilder
{
    HtmlTableBuilder AddRow(string headColumn, string[] cols);
    string Build();
}