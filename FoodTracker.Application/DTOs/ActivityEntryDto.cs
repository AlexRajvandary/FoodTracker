namespace FoodTracker.Application.DTOs;

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
