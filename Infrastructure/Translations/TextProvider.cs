using Infrastructure.Translations.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Translations;

public class TextProvider : ITextProvider
{
    private readonly ILogger<TextProvider> _logger;
    private readonly ITranslationsParser _translationsParser;

    public TextProvider(
        ILogger<TextProvider> logger,
        ITranslationsParser translationsParser)
    {
        _logger = logger;
        _translationsParser = translationsParser;
    }

    public string GetButtonTranslation(string key, string language)
    {
        var translation = GetTranslation(language);
        return translation.Buttons[key] ?? default!;
    }

    public string GetMessageTranslation(string key, string language)
    {
        var translation = GetTranslation(language);
        return translation.Messages[key] ?? default!;
    }

    private Translation GetTranslation(string language)
    {
        try
        {
            return _translationsParser.GetTranslations(language);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error looking up the text");
            return default!;
        }
    }
}
