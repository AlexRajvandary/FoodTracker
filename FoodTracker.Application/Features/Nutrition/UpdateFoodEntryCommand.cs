using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class UpdateFoodEntryCommand : IRequest<Result<FoodEntryDto>>
{
    public required Guid UserId { get; init; }
    public Guid EntryId { get; init; }
    public decimal? GramsConsumed { get; init; }
    public string? PortionNote { get; init; }
    public DateTime? ConsumedAt { get; init; }
}
