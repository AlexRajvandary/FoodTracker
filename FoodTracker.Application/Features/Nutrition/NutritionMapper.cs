using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Nutrition;

namespace FoodTracker.Application.Features.Nutrition;

internal static class NutritionMapper
{
    public static FoodItemDto ToDto(this FoodItem x) =>
        new()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            CaloriesPer100g = x.CaloriesPer100g,
            ProteinsPer100g = x.ProteinsPer100g,
            FatsPer100g = x.FatsPer100g,
            CarbsPer100g = x.CarbsPer100g,
            ServingSizeGrams = x.ServingSizeGrams,
            Categories = x.FoodItemCategories
            .Select(c => c.FoodCategory.Name)
            .ToList()
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
                .ToList()
        };
}