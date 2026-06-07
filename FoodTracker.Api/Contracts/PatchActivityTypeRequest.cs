namespace FoodTracker.Api.Contracts;

public sealed class PatchActivityTypeRequest
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public double? CaloriesPerHour { get; init; }
    public double? CaloriesPer100Reps { get; init; }
    public string? Category { get; init; }
}