namespace FoodTracker.Api.Contracts;

public sealed class GetDailyBalanceRequest
{
    public DateTime Date { get; init; }
}