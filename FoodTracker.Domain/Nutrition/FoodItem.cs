namespace FoodTracker.Domain.Nutrition;

/// <summary>Продукт каталога (на 100 г).</summary>
public class FoodItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal CaloriesPer100g { get; set; }
    public decimal ProteinsPer100g { get; set; }
    public decimal FatsPer100g { get; set; }
    public decimal CarbsPer100g { get; set; }
    public decimal? PortionGrams { get; set; }
    public string? PortionHint { get; set; }
    public string? Category { get; set; }
    public Guid? OwnerUserId { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}