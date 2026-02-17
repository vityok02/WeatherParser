using Application.Common.Abstract;
using CoreHtmlToImage;
using System.Text;

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
        var bytes = Encoding.UTF8.GetBytes(html);

        var imageBytes = _converter
            .FromHtmlString(Encoding.UTF8.GetString(bytes));
        var stream = new MemoryStream(imageBytes);
        return new FileWrapper(stream);
    }
}