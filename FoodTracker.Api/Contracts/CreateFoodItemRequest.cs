namespace FoodTracker.Api.Contracts;

public sealed class CreateFoodItemRequest
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal CaloriesPer100g { get; set; }
    public decimal? ProteinsPer100g { get; set; }
    public decimal? FatsPer100g { get; set; }
    public decimal? CarbsPer100g { get; set; }
}