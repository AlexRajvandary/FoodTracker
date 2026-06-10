using FoodTracker.Domain.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodTracker.Infrastructure.Persistence.Configurations;

public sealed class FoodItemCategoryConfiguration : IEntityTypeConfiguration<FoodItemCategory>
{
    public void Configure(EntityTypeBuilder<FoodItemCategory> entity)
    {
        entity.ToTable("food_item_categories");

        entity.HasKey(x => new
        {
            x.FoodItemId,
            x.FoodCategoryId
        });

        entity.HasOne(x => x.FoodItem)
            .WithMany(x => x.FoodItemCategories)
            .HasForeignKey(x => x.FoodItemId);

        entity.HasOne(x => x.FoodCategory)
            .WithMany(x => x.FoodItemCategories)
            .HasForeignKey(x => x.FoodCategoryId);

        entity.HasIndex(x => x.FoodCategoryId);
    }
}