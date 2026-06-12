using FoodTracker.Domain.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodTracker.Infrastructure.Persistence.Configurations;

public sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> entity)
    {
        entity.ToTable("countries");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.IsoCode)
            .HasMaxLength(2)
            .IsRequired();

        entity.HasIndex(x => x.Name)
            .IsUnique();
    }
}