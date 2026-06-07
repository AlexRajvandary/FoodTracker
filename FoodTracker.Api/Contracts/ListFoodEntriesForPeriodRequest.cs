namespace FoodTracker.Api.Contracts;

public sealed class ListFoodEntriesForPeriodRequest
{
    public DateTime FromUtc { get; set; }
    public DateTime ToUtc { get; set; }
}