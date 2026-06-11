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

        entity.Property(e => e.Name)
            .HasMaxLength(500)
            .IsRequired();

        entity.Property(e => e.Barcode)
            .HasMaxLength(64);

        entity.Property(e => e.Brand)
            .HasMaxLength(500);

        entity.Property(e => e.Description)
            .HasColumnType("text");

        entity.Property(e => e.ImageUrl)
            .HasMaxLength(2048);

        entity.Property(e => e.CaloriesPer100g)
            .HasPrecision(18, 4);

        entity.Property(e => e.ProteinsPer100g)
            .HasPrecision(18, 4);

        entity.Property(e => e.FatsPer100g)
            .HasPrecision(18, 4);

        entity.Property(e => e.CarbsPer100g)
            .HasPrecision(18, 4);

        entity.Property(e => e.FiberPer100g)
            .HasPrecision(18, 4);

        entity.Property(e => e.SaltPer100g)
            .HasPrecision(18, 4);

        entity.Property(e => e.SaturatedFatPer100g)
            .HasPrecision(18, 4);

        entity.Property(e => e.SugarsPer100g)
            .HasPrecision(18, 4);

        entity.Property(e => e.ServingSizeGrams)
            .HasPrecision(18, 4);

        entity.HasMany(e => e.FoodItemCategories)
            .WithOne(e => e.FoodItem)
            .HasForeignKey(e => e.FoodItemId);

        entity.HasIndex(e => e.Name);

        entity.HasIndex(x => x.Barcode)
            .IsUnique();

        entity.HasIndex(e => e.Brand);

        entity.Property(x => x.ExternalId)
            .HasMaxLength(500);

        entity.HasIndex(x => x.ExternalId);
    }
}