using Microsoft.EntityFrameworkCore;

namespace WeatherParser.Data;

public class AppDbContext : DbContext
{
    public DbSet<Models.User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var userBuilder = builder.Entity<Models.User>();

        userBuilder
            .Property(u => u.Id)
            .ValueGeneratedNever();
    }
}