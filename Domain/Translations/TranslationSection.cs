namespace Domain.Translations;

public sealed class TranslationSection : Dictionary<string, string>
{
    public new string this[string key] =>
        TryGetValue(key, out var value)
        ? value
        : string.Empty;
}
