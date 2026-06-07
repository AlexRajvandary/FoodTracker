namespace FoodTracker.Api.Contracts;

public sealed class CreateFoodEntryRequest
{
    public DateTime ConsumedAtUtc { get; set; }
    public Guid FoodId { get; set; }
    public double? GramsConsumed { get; set; }
    public double? PortionsConsumed { get; set; }
}