using Microsoft.EntityFrameworkCore;
using WeatherParser.Models;

namespace WeatherParser.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Location> Locations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var userBuilder = builder.Entity<User>();
        var locaitonBuilder = builder.Entity<Location>();

        userBuilder
            .Property(u => u.Id)
            .ValueGeneratedNever();

        userBuilder
            .HasOne(u => u.CurrentLocation)
            .WithMany()
            .HasForeignKey(u => u.CurrentLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        userBuilder.HasMany(u => u.Locations)
            .WithMany(l => l.Users);


        locaitonBuilder
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
    }
}