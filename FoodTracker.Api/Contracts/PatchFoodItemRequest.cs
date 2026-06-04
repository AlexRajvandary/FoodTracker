namespace FoodTracker.Api.Contracts;

public sealed class PatchFoodItemRequest
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public decimal? CaloriesPer100g { get; set; }
    public decimal? ProteinsPer100g { get; set; }
    public decimal? FatsPer100g { get; set; }
    public decimal? CarbsPer100g { get; set; }
}