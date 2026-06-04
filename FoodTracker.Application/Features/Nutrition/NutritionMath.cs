using FoodTracker.Domain.Nutrition;

namespace FoodTracker.Application.Features.Nutrition;

public static class NutritionMath
{
    public static bool TryResolveGrams(
        FoodItem item,
        decimal? gramsConsumed,
        decimal? portionCount,
        out decimal grams,
        out string? errorMessage)
    {
        if (gramsConsumed is { } g && g > 0)
        {
            grams = g;
            errorMessage = null;
            return true;
        }

        if (portionCount is { } pc && pc > 0 && item.PortionGrams is { } pg && pg > 0)
        {
            grams = pc * pg;
            errorMessage = null;
            return true;
        }

        grams = 0;
        errorMessage = "Укажите gramsConsumed > 0 или portionCount с известной массой порции продукта.";
        return false;
    }

    public static (decimal Calories, decimal Proteins, decimal Fats, decimal Carbs) ComputeTotals(
        FoodItem item,
        decimal gramsConsumed)
    {
        var factor = gramsConsumed / 100m;
        return (
            Round2(item.CaloriesPer100g * factor),
            Round2(item.ProteinsPer100g * factor),
            Round2(item.FatsPer100g * factor),
            Round2(item.CarbsPer100g * factor));
    }

    private static decimal Round2(decimal v) => Math.Round(v, 2, MidpointRounding.AwayFromZero);
}
