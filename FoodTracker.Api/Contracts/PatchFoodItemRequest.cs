namespace FoodTracker.Api.Contracts;

public sealed class PatchFoodItemRequest
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public double? CaloriesPer100g { get; set; }
    public double? ProteinsPer100g { get; set; }
    public double? FatsPer100g { get; set; }
    public double? CarbsPer100g { get; set; }
}