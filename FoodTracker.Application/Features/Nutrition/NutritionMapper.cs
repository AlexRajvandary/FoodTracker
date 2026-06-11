using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Nutrition;

namespace FoodTracker.Application.Features.Nutrition;

internal static class NutritionMapper
{
    public static FoodItemDto ToDto(this FoodItem x) =>
        new()
        {
            Barcode = x.Barcode,
            Brand = x.Brand,
            CaloriesPer100g = x.CaloriesPer100g,
            CarbsPer100g = x.CarbsPer100g,
            Categories = x.FoodItemCategories
                .Select(c => c.FoodCategory.Name)
                .ToList(),
            Countries = x.FoodItemCountries
                .Select(c => c.Country.Name)
                .ToList(),
            Description = x.Description,
            FatsPer100g = x.FatsPer100g,
            FiberPer100g = x.FiberPer100g,
            Id = x.Id,
            ImageUrl = x.ImageUrl,
            Name = x.Name,
            ProteinsPer100g = x.ProteinsPer100g,
            SaltPer100g = x.SaltPer100g,
            SaturatedFatPer100g = x.SaturatedFatPer100g,
            ServingSizeGrams = x.ServingSizeGrams,
            SugarsPer100g = x.SugarsPer100g,
        };

    public static ShortFoodItemDto ToShortDto(this FoodItem x) =>
        new()
        {
            Id = x.Id,
            Name = x.Name,
            CaloriesPer100g = x.CaloriesPer100g,
            ProteinsPer100g = x.ProteinsPer100g,
            FatsPer100g = x.FatsPer100g,
            CarbsPer100g = x.CarbsPer100g,
            Categories = x.FoodItemCategories
                .Select(c => c.FoodCategory.Name)
                .ToList(),
            Countries = x.FoodItemCountries
                .Select(c => c.Country.Name)
                .ToList()
        };
}