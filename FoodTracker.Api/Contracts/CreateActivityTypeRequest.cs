namespace FoodTracker.Api.Contracts;

public sealed class CreateActivityTypeRequest
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal? CaloriesPerHour { get; init; }
    public decimal? CaloriesPer100Reps { get; init; }
    public string? Category { get; init; }
}