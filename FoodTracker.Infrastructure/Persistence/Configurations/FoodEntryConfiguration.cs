using FoodTracker.Domain.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodTracker.Infrastructure.Persistence.Configurations;

public class FoodEntryConfiguration : IEntityTypeConfiguration<FoodEntry>
{
    public void Configure(EntityTypeBuilder<FoodEntry> entity)
    {
        entity.ToTable("food_entries");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.FoodName).HasMaxLength(500).IsRequired();
        entity.Property(e => e.PortionNote).HasMaxLength(500);
        entity.Property(e => e.GramsConsumed).HasPrecision(18, 4);
        entity.Property(e => e.Calories).HasPrecision(18, 4);
        entity.Property(e => e.Proteins).HasPrecision(18, 4);
        entity.Property(e => e.Fats).HasPrecision(18, 4);
        entity.Property(e => e.Carbs).HasPrecision(18, 4);
        entity.HasIndex(e => new { e.UserId, e.ConsumedAtUtc });
        entity
            .HasOne<FoodItem>()
            .WithMany()
            .HasForeignKey(e => e.FoodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
