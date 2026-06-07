namespace FoodTracker.Api.Contracts;

public sealed class GetPeriodBalanceRequest
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}