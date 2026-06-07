namespace FoodTracker.Api.Contracts;

public sealed class CreateActivityTypeRequest
{
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public double? CaloriesPerHour { get; init; }
    public double? CaloriesPer100Reps { get; init; }
    public string Category { get; init; } = string.Empty;
}