using FoodTracker.Domain.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodTracker.Infrastructure.Persistence.Configurations;

public sealed class FoodCategoryConfiguration : IEntityTypeConfiguration<FoodCategory>
{
    public void Configure(EntityTypeBuilder<FoodCategory> entity)
    {
        entity.ToTable("food_categories");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Name)
            .HasMaxLength(400)
            .IsRequired();

        entity.Property(x => x.ExternalId)
            .HasMaxLength(400);

        entity.HasMany(x => x.FoodItemCategories)
            .WithOne(x => x.FoodCategory)
            .HasForeignKey(x => x.FoodCategoryId);

        entity.HasIndex(x => x.Name)
            .IsUnique();

        entity.HasIndex(x => x.ExternalId);
    }
}