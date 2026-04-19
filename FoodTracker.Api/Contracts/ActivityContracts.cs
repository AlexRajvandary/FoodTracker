namespace FoodTracker.Api.Contracts;

public sealed class CreateActivityEntryRequest
{
    public Guid ActivityTypeId { get; set; }
    public DateTime OccurredAt { get; set; }
    public int? DurationMinutes { get; set; }
    public int? RepetitionsCount { get; set; }
    public decimal? CaloriesBurned { get; set; }
}

public sealed class UpdateActivityEntryRequest
{
    public DateTime? OccurredAt { get; set; }
    public int? DurationMinutes { get; set; }
    public int? RepetitionsCount { get; set; }
    public decimal? CaloriesBurned { get; set; }
}
