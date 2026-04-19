namespace FoodTracker.Api.Contracts;

public sealed class CreateFoodEntryRequest
{
    public Guid FoodItemId { get; set; }
    public DateTime ConsumedAt { get; set; }
    public decimal? GramsConsumed { get; set; }
    public decimal? PortionCount { get; set; }
    public string? PortionNote { get; set; }
}

public sealed class UpdateFoodEntryRequest
{
    public decimal? GramsConsumed { get; set; }
    public string? PortionNote { get; set; }
    public DateTime? ConsumedAt { get; set; }
}

public sealed class CreateFoodItemRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal CaloriesPer100g { get; set; }
    public decimal? ProteinsPer100g { get; set; }
    public decimal? FatsPer100g { get; set; }
    public decimal? CarbsPer100g { get; set; }
}
