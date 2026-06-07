namespace FoodTracker.Application.DTOs;

public class ShortFoodItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Category { get; init; } = string.Empty;
    public decimal CaloriesPer100g { get; init; }
    public decimal ProteinsPer100g { get; init; }
    public decimal FatsPer100g { get; init; }
    public decimal CarbsPer100g { get; init; }
}
