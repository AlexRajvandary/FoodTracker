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
            PortionGrams = x.PortionGrams,
            PortionHint = x.PortionHint,
            Category = x.Category,
        };

    public static FoodEntryDto ToDto(this FoodEntry x) =>
        new()
        {
            Id = x.Id,
            FoodItemId = x.FoodItemId,
            FoodName = x.FoodName,
            ConsumedAt = x.ConsumedAtUtc,
            GramsConsumed = x.GramsConsumed,
            PortionNote = x.PortionNote,
            Calories = x.Calories,
            Proteins = x.Proteins,
            Fats = x.Fats,
            Carbs = x.Carbs,
        };
}
