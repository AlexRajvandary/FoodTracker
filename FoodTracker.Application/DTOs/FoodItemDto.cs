namespace FoodTracker.Application.DTOs;

public class FoodItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Category { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal CaloriesPer100g { get; init; }
    public decimal ProteinsPer100g { get; init; }
    public decimal FatsPer100g { get; init; }
    public decimal CarbsPer100g { get; init; }
    public decimal? PortionGrams { get; init; }
    public string? PortionHint { get; init; }
}