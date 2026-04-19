namespace FoodTracker.Domain.Activities;

/// <summary>Справочник типов активности (ккал/час и/или ккал на 100 повторений).</summary>
public class ActivityType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? CaloriesPerHour { get; set; }
    public decimal? CaloriesPer100Reps { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
