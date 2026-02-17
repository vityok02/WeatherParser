using Domain.Languages;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedDataAsync(AppDbContext dbContext)
    {
        if (await dbContext.Languages.AnyAsync())
        {
            return;
        }

        await dbContext.Languages.AddAsync(new Language(1, Languages.English, "en"));
        await dbContext.Languages.AddAsync(new Language(2, Languages.Ukrainian, "uk"));
              
        await dbContext.SaveChangesAsync();
    }
}
