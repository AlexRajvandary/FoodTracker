using FoodTracker.Application.DTOs;

namespace FoodTracker.Application.Features.FoodEntry
{
    internal static class FoodEntryMapper
    {
        public static FoodEntryDto ToDto(this Domain.Nutrition.FoodEntry foodEntry) =>
        new()
        {
            Calories = foodEntry.Calories,
            Carbs = foodEntry.Carbs,
            ConsumedAtUtc = foodEntry.ConsumedAtUtc,
            Fats = foodEntry.Fats,
            FoodId = foodEntry.FoodId,
            Grams = foodEntry.GramsConsumed,
            Id = foodEntry.Id,
            Name = foodEntry.FoodName,
            Proteins = foodEntry.Proteins
        };
    }
}