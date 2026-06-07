namespace FoodTracker.Application.DTOs;

public class ShortFoodItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Category { get; init; } = string.Empty;
    public double CaloriesPer100g { get; init; }
    public double? ProteinsPer100g { get; init; }
    public double? FatsPer100g { get; init; }
    public double? CarbsPer100g { get; init; }
}