using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodEntriesQuery : IRequest<Result<IReadOnlyList<FoodEntryDto>>>
{
    public required Guid UserId { get; init; }
    public DateTime? FromUtc { get; init; }
    public DateTime? ToUtc { get; init; }
    public DateOnly? Date { get; init; }
}
