namespace FoodTracker.Application.DTOs;

public sealed class PeriodBalanceDto
{
    public double CaloriesBurnedTotal { get; init; }
    public double CaloriesConsumedTotal { get; init; }
    public double CarbsTotal { get; init; }
    public required IReadOnlyList<DailyBalanceDto> DailyBalances { get; init; }
    public double FatsTotal { get; init; }
    public double ProteinsTotal { get; init; }
}