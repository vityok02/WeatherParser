using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.HasMany(u => u.Locations)
            .WithMany(l => l.Users);

        builder.HasOne(u => u.CurrentLocation)
            .WithMany()
            .HasForeignKey(u => u.CurrentLocationId);
    }
}
