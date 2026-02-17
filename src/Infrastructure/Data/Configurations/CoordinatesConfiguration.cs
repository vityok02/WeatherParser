using Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CoordinatesConfiguration : IEntityTypeConfiguration<Coordinates>
{
    public void Configure(EntityTypeBuilder<Coordinates> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();
    }
}
