using Infrastructure.Translations.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Translations;

public class TranslationsParser : ITranslationsParser
{
    private readonly IPathProvider _pathProvider;
    private readonly JsonSerializerOptions _options;
    private readonly ILogger<TextProvider> _logger;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan CacheTime = TimeSpan.FromMinutes(2);

    public TranslationsParser(
        IPathProvider pathProvider,
        ILogger<TextProvider> logger,
        IMemoryCache cache)
    {
        _pathProvider = pathProvider;
        _cache = cache;
        _logger = logger;
        _options = new()
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    public Translation GetTranslations(string language)
    {
        var translation = _cache.GetOrCreate(
            language,
            entry =>
            {
                entry.SetAbsoluteExpiration(CacheTime);


                var content = File.ReadAllText(_pathProvider.GetFileName(language));

                LanguagePack? pack = JsonSerializer
                    .Deserialize<LanguagePack>(
                        content, _options);

                if (pack?.Translations is null)
                {
                    _logger.LogError("The specified language was not found in the file");
                    return default!;
                }

                return pack.Translations;
            });

        return translation!;
    }
}