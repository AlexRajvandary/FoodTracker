using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class ListActivityEntriesQuery : IRequest<Result<IReadOnlyList<ActivityEntryDto>>>
{
    public required Guid UserId { get; init; }
    public DateTime? FromUtc { get; init; }
    public DateTime? ToUtc { get; init; }
    public DateOnly? Date { get; init; }
}
