using Common.Constants;
using Domain;
using Domain.Languages;

namespace Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedDataAsync(AppDbContext dbContext)
    {
        await dbContext.Languages.AddAsync(new Language(Languages.English));
        await dbContext.Languages.AddAsync(new Language(Languages.Ukrainian));
              
        await dbContext.SaveChangesAsync();
    }
}
