using Domain.Translations;

namespace Infrastructure.Translations.Interfaces;

public interface ITranslationsParser
{
    Translation GetTranslations(string language);
}
