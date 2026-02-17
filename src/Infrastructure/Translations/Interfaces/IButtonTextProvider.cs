namespace Infrastructure.Translations.Interfaces;

public interface IButtonTextProvider
{
    string GetButtonTranslation(string key, string language);
}
