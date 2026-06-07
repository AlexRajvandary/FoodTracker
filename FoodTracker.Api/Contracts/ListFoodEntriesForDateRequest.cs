namespace FoodTracker.Api.Contracts;

public sealed class ListFoodEntriesForDateRequest
{
    public DateOnly Date { get; set; }
}
