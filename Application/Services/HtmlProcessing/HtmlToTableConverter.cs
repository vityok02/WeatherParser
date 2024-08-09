using Application.Abstract;
using CoreHtmlToImage;

namespace Application.Services.HtmlProcessing;

public class HtmlToImageConverter
{
    private readonly HtmlConverter _converter;

    public HtmlToImageConverter(HtmlConverter converter)
    {
        _converter = converter;
    }

    public FileWrapper ConvertToImage(string html)
    {
        var bytes = _converter.FromHtmlString(html);
        var stream = new MemoryStream(bytes);

        return new FileWrapper(stream);
    }
}