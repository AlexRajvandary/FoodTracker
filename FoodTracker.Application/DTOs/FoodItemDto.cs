namespace FoodTracker.Application.DTOs;

public sealed class FoodItemDto
{
    public string Barcode { get; init; } = string.Empty;
    public string? Brand { get; init; }
    public double CaloriesPer100g { get; init; }
    public IReadOnlyList<string> Categories { get; init; } = [];
    public IReadOnlyList<string> Countries { get; init; } = [];
    public double? CarbsPer100g { get; init; }
    public string? Description { get; init; }
    public double? FatsPer100g { get; init; }
    public double? FiberPer100g { get; init; }
    public Guid Id { get; init; }
    public string? ImageUrl { get; init; }
    public string Name { get; init; } = string.Empty;
    public double? ProteinsPer100g { get; init; }
    public double? SaltPer100g { get; init; }
    public double? SaturatedFatPer100g { get; init; }
    public double? ServingSizeGrams { get; init; }
    public double? SugarsPer100g { get; init; }
}