using Application.Common.Interfaces.Services;
using System.Text;

namespace Application.Services.HtmlProcessing;

public class HtmlTableBuilder : IHtmlTableBuilder
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
