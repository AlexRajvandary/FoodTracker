namespace FoodTracker.Api.Contracts;

public sealed class UpdateFoodItemRequest
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public double CaloriesPer100g { get; init; }
    public double ProteinsPer100g { get; init; }
    public double FatsPer100g { get; init; }
    public double CarbsPer100g { get; init; }
}