using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = "Server=tcp:weatherparser-db.database.windows.net,1433;Initial Catalog=WeatherBotDb;Persist Security Info=False;User ID=Abdul4ik02;Password=Shaurmatop69.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        optionsBuilder.UseSqlServer(connectionString,
            providerOptions => providerOptions.EnableRetryOnFailure(10));

        return new AppDbContext(optionsBuilder.Options);
    }
}
