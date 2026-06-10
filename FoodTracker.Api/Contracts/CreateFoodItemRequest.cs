namespace FoodTracker.Api.Contracts;

public sealed class CreateFoodItemRequest
{
    public string Barcode { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public double CaloriesPer100g { get; set; }
    public IReadOnlyList<Guid> CategoryIds { get; set; } = [];
    public double? CarbsPer100g { get; set; }
    public string? Description { get; set; }
    public double? FatsPer100g { get; set; }
    public double? FiberPer100g { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; } = string.Empty;
    public IReadOnlyList<string> NewCategoryNames { get; set; } = [];
    public double? ProteinsPer100g { get; set; }
    public double? SaltPer100g { get; set; }
    public double? SaturatedFatPer100g { get; set; }
    public double? ServingSizeGrams { get; set; }
    public double? SugarsPer100g { get; set; }
}