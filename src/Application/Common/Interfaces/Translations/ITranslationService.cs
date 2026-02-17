using Domain.Translations;

namespace Application.Common.Interfaces.Translations;

public interface ITranslationService
{
    public Translation GetTranslation(string language);
}
