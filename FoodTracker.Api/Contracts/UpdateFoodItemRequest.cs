namespace FoodTracker.Api.Contracts;

public sealed class UpdateFoodItemRequest
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal CaloriesPer100g { get; init; }
    public decimal ProteinsPer100g { get; init; }
    public decimal FatsPer100g { get; init; }
    public decimal CarbsPer100g { get; init; }
}
