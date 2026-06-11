namespace FoodTracker.Domain.Nutrition;

public class FoodItem
{
    public string Barcode { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public double CaloriesPer100g { get; set; }
    public ICollection<FoodItemCategory> FoodItemCategories { get; set; } = [];
    public ICollection<FoodItemCountry> FoodItemCountries { get; set; } = [];
    public double CarbsPer100g { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public string? Description { get; set; }
    public long? ExternalId { get; set; }
    public double FatsPer100g { get; set; }
    public double? FiberPer100g { get; set; }
    public Guid Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? OwnerUserId { get; set; }
    public double ProteinsPer100g { get; set; }
    public double? SaltPer100g { get; set; }
    public double? SaturatedFatPer100g { get; set; }
    public double? ServingSizeGrams { get; set; }
    public double? SugarsPer100g { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}