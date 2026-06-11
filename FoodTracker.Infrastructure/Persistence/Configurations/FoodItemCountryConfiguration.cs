using FoodTracker.Domain.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodTracker.Infrastructure.Persistence.Configurations;

public sealed class FoodItemCountryConfiguration : IEntityTypeConfiguration<FoodItemCountry>
{
    public void Configure(EntityTypeBuilder<FoodItemCountry> entity)
    {
        entity.ToTable("food_item_countries");

        entity.HasKey(x => new
        {
            x.FoodItemId,
            x.CountryId
        });

        entity.HasOne(x => x.FoodItem)
            .WithMany(x => x.FoodItemCountries)
            .HasForeignKey(x => x.FoodItemId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(x => x.Country)
            .WithMany(x => x.FoodItemCountries)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}