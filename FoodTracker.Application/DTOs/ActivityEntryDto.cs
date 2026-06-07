namespace FoodTracker.Application.DTOs;

public sealed class ActivityEntryDto
{
    public Guid ActivityTypeId { get; init; }
    public string ActivityName { get; init; } = string.Empty;
    public decimal CaloriesBurned { get; init; }
    public DateTime Date { get; init; }
    public int? DurationMinutes { get; init; }
    public Guid Id { get; init; }
    public DateTime OccurredAt { get; init; }
    public int? RepetitionsCount { get; init; }
}