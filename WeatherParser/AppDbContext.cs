using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace WeatherParser;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=TelegramBot;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().Property(u => u.Id)
            .ValueGeneratedNever();
    }
}
