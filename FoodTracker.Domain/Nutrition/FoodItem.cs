namespace FoodTracker.Domain.Nutrition;

/// <summary>Продукт каталога (на 100 г).</summary>
public class FoodItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double CaloriesPer100g { get; set; }
    public double ProteinsPer100g { get; set; }
    public double FatsPer100g { get; set; }
    public double CarbsPer100g { get; set; }
    public double? PortionGrams { get; set; }
    public string? Category { get; set; }
    public Guid? OwnerUserId { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}