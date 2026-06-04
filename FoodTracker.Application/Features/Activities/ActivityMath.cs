using FoodTracker.Domain.Activities;

namespace FoodTracker.Application.Features.Activities;

public static class ActivityMath
{
    public static bool TryResolveCaloriesBurned(
        ActivityType type,
        int? durationMinutes,
        int? repetitionsCount,
        decimal? caloriesBurnedOverride,
        out decimal calories,
        out string? errorMessage)
    {
        if (caloriesBurnedOverride is { } manual)
        {
            if (manual < 0)
            {
                calories = 0;
                errorMessage = "Калории не могут быть отрицательными.";
                return false;
            }

            calories = Round2(manual);
            errorMessage = null;
            return true;
        }

        if (durationMinutes is { } dm && dm > 0 && type.CaloriesPerHour is { } cph)
        {
            calories = Round2(dm / 60m * cph);
            errorMessage = null;
            return true;
        }

        if (repetitionsCount is { } rc && rc > 0 && type.CaloriesPer100Reps is { } c100)
        {
            calories = Round2(rc / 100m * c100);
            errorMessage = null;
            return true;
        }

        calories = 0;
        errorMessage =
            "Укажите caloriesBurned или задайте длительность (мин) при известных ккал/час у типа, либо число повторов при известных ккал/100 повторов.";
        return false;
    }

    public static decimal RoundCalories(decimal v) => Round2(v);

    private static decimal Round2(decimal v) => Math.Round(v, 2, MidpointRounding.AwayFromZero);
}
