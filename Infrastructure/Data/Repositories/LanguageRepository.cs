using Domain.Languages;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class LanguageRepository : ILanguageRepository
{
    private readonly AppDbContext _context;

    public LanguageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Language>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Languages.ToArrayAsync(cancellationToken);
    }
}
