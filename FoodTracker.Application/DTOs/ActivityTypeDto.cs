namespace FoodTracker.Application.DTOs;

public sealed class ActivityTypeDto
{
    public string? Category { get; init; }
    public double? CaloriesPer100Reps { get; init; }
    public double? CaloriesPerHour { get; init; }
    public string? Description { get; init; }
    public Guid Id { get; init; }
    public required string Name { get; init; }
}