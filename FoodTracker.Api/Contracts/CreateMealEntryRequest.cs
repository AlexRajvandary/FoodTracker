namespace FoodTracker.Api.Contracts;

public sealed class CreateMealEntryRequest
{
    public Guid FoodItemId { get; set; }
    public DateTime ConsumedAt { get; set; }
    public decimal? GramsConsumed { get; set; }
    public decimal? PortionCount { get; set; }
    public string? PortionNote { get; set; }
}
