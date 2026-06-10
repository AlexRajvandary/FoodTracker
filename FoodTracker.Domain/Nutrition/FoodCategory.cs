namespace FoodTracker.Domain.Nutrition;

public class FoodCategory
{
    public DateTime CreatedAtUtc { get; set; }
    public string? ExternalId { get; set; }
    public ICollection<FoodItemCategory> FoodItemCategories { get; set; } = [];
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}