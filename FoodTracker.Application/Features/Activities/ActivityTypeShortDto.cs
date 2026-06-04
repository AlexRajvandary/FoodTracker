namespace FoodTracker.Application.Features.Activities;

public sealed class ActivityTypeShortDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal? CaloriesPerHour { get; init; }
    public decimal? CaloriesPer100Reps { get; init; }
    public string? Category { get; init; }
}