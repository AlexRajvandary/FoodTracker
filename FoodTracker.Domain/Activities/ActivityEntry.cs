namespace FoodTracker.Domain.Activities;

/// <summary>Запись тренировки пользователя.</summary>
public class ActivityEntry
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ActivityTypeId { get; set; }
    public string ActivityName { get; set; } = string.Empty;
    public DateTime OccurredAtUtc { get; set; }
    public int? DurationMinutes { get; set; }
    public int? RepetitionsCount { get; set; }
    public decimal CaloriesBurned { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
