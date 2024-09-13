namespace Domain.Translations;

public class TranslationSection : Dictionary<string, string>
{
    public string this[string key] =>
        TryGetValue(key, out var value)
        ? value
        : string.Empty;
}
