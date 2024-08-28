namespace Infrastructure.Translations.Interfaces;

public interface IMessageTextProvider
{
    string GetMessageTranslation(string key, string language);
}