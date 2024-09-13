namespace Application.Common.Interfaces.Localization;

public interface IMessageTranslationService
{
    string GetMessageTranslation(string key, string language);
}
