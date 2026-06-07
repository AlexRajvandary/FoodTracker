using FoodTracker.Domain.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodTracker.Infrastructure.Persistence.Configurations;

public class FoodItemConfiguration : IEntityTypeConfiguration<FoodItem>
{
    public void Configure(EntityTypeBuilder<FoodItem> entity)
    {
        entity.ToTable("food_items");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        entity.Property(e => e.Description).HasMaxLength(2000);
        entity.Property(e => e.Category).HasMaxLength(100);
        entity.Property(e => e.CaloriesPer100g).HasPrecision(18, 4);
        entity.Property(e => e.ProteinsPer100g).HasPrecision(18, 4);
        entity.Property(e => e.FatsPer100g).HasPrecision(18, 4);
        entity.Property(e => e.CarbsPer100g).HasPrecision(18, 4);
        entity.Property(e => e.PortionGrams).HasPrecision(18, 4);
        entity.HasIndex(e => e.Name);
        entity.HasIndex(e => e.Category);
    }
}
