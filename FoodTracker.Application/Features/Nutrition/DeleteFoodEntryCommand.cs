using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class DeleteFoodEntryCommand : IRequest<Result>
{
    public required Guid UserId { get; init; }
    public Guid EntryId { get; init; }
}
