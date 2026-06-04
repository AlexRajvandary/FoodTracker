namespace FoodTracker.Api.Contracts;

public sealed class PatchActivityTypeRequest
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal? CaloriesPerHour { get; init; }
    public decimal? CaloriesPer100Reps { get; init; }
    public string? Category { get; init; }
}