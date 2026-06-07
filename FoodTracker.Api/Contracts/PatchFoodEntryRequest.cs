namespace FoodTracker.Api.Contracts;

public class PatchFoodEntryRequest
{
    public DateTime? ConsumedAtUtc { get; init; }
    public double? GramsConsumed { get; init; }
    public Guid FoodItemId { get; init; }
}