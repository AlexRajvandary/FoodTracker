namespace FoodTracker.Application.DTOs;

public class FoodItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Category { get; init; } = string.Empty;
    public string? Description { get; init; }
    public double CaloriesPer100g { get; init; }
    public double? ProteinsPer100g { get; init; }
    public double? FatsPer100g { get; init; }
    public double? CarbsPer100g { get; init; }
    public double? PortionGrams { get; init; }
    public string? PortionHint { get; init; }
}