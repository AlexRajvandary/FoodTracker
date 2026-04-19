namespace FoodTracker.Application.Features.Nutrition;

public class FoodItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal CaloriesPer100g { get; init; }
    public decimal ProteinsPer100g { get; init; }
    public decimal FatsPer100g { get; init; }
    public decimal CarbsPer100g { get; init; }
    public decimal? PortionGrams { get; init; }
    public string? PortionHint { get; init; }
}

public sealed class FoodCatalogItemDto : FoodItemDto
{
    public string Category { get; init; } = string.Empty;
}

public sealed class FoodEntryDto
{
    public Guid Id { get; init; }
    public Guid FoodItemId { get; init; }
    public string FoodName { get; init; } = string.Empty;
    public DateTime ConsumedAt { get; init; }
    public decimal GramsConsumed { get; init; }
    public string? PortionNote { get; init; }
    public decimal Calories { get; init; }
    public decimal Proteins { get; init; }
    public decimal Fats { get; init; }
    public decimal Carbs { get; init; }
}
