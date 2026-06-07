namespace FoodTracker.Api.Contracts;

public sealed class CreateFoodItemRequest
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double CaloriesPer100g { get; set; }
    public double? ProteinsPer100g { get; set; }
    public double? FatsPer100g { get; set; }
    public double? CarbsPer100g { get; set; }
}