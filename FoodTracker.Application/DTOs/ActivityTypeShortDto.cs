namespace FoodTracker.Application.DTOs;

public sealed class ActivityTypeShortDto
{
    public string? Category { get; init; }
    public double? CaloriesPer100Reps { get; init; }
    public double? CaloriesPerHour { get; init; }
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}