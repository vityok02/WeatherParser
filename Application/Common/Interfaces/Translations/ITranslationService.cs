using Domain.Translations;

namespace Application.Common.Interfaces.Localization;

public interface ITranslationService
{
    public Translation GetTranslation(string language);
}
