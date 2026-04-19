namespace FoodTracker.Application.Features.Activities;

public sealed class ActivityTypeDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal? CaloriesPerHour { get; init; }
    public decimal? CaloriesPer100Reps { get; init; }
}

public sealed class ActivityEntryDto
{
    public Guid Id { get; init; }
    public Guid ActivityTypeId { get; init; }
    public string ActivityName { get; init; } = string.Empty;
    public DateTime OccurredAt { get; init; }
    public int? DurationMinutes { get; init; }
    public int? RepetitionsCount { get; init; }
    public decimal CaloriesBurned { get; init; }
}
