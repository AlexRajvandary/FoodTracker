using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class DeleteActivityEntryCommand : IRequest<Result>
{
    public required Guid UserId { get; init; }
    public Guid EntryId { get; init; }
}
