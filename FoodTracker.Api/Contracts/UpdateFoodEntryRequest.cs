namespace FoodTracker.Api.Contracts;

public sealed class UpdateFoodEntryRequest
{
    public decimal? GramsConsumed { get; set; }
    public string? PortionNote { get; set; }
    public DateTime? ConsumedAt { get; set; }
}
