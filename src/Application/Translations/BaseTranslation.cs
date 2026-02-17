using System.Text.Json.Serialization;

namespace Application.Translations;

public abstract class BaseTranslation : ITranslation
{
    private readonly Dictionary<string, string> _translations = [];

    [JsonExtensionData]
    public Dictionary<string, string> Translations
    {
        get => _translations;
        set
        {
            foreach (var kvp in value)
            {
                _translations[kvp.Key] = kvp.Value;
            }
        }
    }
    public string this[string key] =>
        _translations.TryGetValue(key, out string? value)
        ? value
        : string.Empty;
}
