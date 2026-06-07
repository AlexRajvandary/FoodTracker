namespace FoodTracker.Application.DTOs;

public sealed class DailyBalanceDto
{
    public required IReadOnlyList<ActivityEntryDto> ActivityEntries { get; init; }
    public double BasalCalories { get; init; }
    public double CaloriesBurned { get; init; }
    public double CaloriesConsumed { get; init; }
    public double Carbs { get; init; }
    public DateTime Date { get; init; }
    public double Fats { get; init; }
    public required IReadOnlyList<FoodEntryDto> FoodEntries { get; init; }
    public double Proteins { get; init; }
}