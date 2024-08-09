using System.Text;

namespace Application.Services.HtmlProcessing;

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
