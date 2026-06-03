namespace FoodTracker.Api.Contracts;

public sealed class PatchFoodItemRequest
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal? CaloriesPer100g { get; init; }
    public decimal? ProteinsPer100g { get; init; }
    public decimal? FatsPer100g { get; init; }
    public decimal? CarbsPer100g { get; init; }
}