namespace FoodTracker.Domain.Activities;

public class ActivityType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? CaloriesPerHour { get; set; }
    public decimal? CaloriesPer100Reps { get; set; }
    public string? Category { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}