namespace Domain.Languages;

public interface ILanguageRepository
{
    Task<IEnumerable<Language>> GetAll(CancellationToken cancellationToken);
}
