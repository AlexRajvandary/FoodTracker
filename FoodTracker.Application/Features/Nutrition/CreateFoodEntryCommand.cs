using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class CreateFoodEntryCommand : IRequest<Result<FoodEntryDto>>
{
    public required Guid UserId { get; init; }
    public Guid FoodItemId { get; init; }
    public DateTime ConsumedAt { get; init; }
    public decimal? GramsConsumed { get; init; }
    public decimal? PortionCount { get; init; }
    public string? PortionNote { get; init; }
}
