namespace Domain.Languages;

public interface ILanguageRepository
{
    Task<IEnumerable<Language>> GetAllAsync(CancellationToken cancellationToken);

    Task<Language?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
