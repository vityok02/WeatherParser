using System.Text;

namespace Application.Services.HtmlProcessing;

public class HtmlBuilder
{
    private StringBuilder _builder = new(@"<head><meta charset=""UTF-8""></head>");

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
