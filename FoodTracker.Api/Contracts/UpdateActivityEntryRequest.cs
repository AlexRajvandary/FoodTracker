namespace FoodTracker.Api.Contracts;

public sealed class UpdateActivityEntryRequest
{
    public DateTime? OccurredAt { get; set; }
    public int? DurationMinutes { get; set; }
    public int? RepetitionsCount { get; set; }
    public decimal? CaloriesBurned { get; set; }
}
