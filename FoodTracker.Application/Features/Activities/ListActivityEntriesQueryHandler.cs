using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class ListActivityEntriesQueryHandler : IRequestHandler<ListActivityEntriesQuery, Result<IReadOnlyList<ActivityEntryDto>>>
{
    private readonly IActivityEntryRepository _entries;

    public ListActivityEntriesQueryHandler(IActivityEntryRepository entries)
    {
        _entries = entries;
    }

    public async Task<Result<IReadOnlyList<ActivityEntryDto>>> Handle(ListActivityEntriesQuery request, CancellationToken cancellationToken)
    {
        var list = await _entries
            .ListForUserAsync(request.UserId, request.FromUtc, request.ToUtc, request.Date, cancellationToken)
            .ConfigureAwait(false);
        return Result<IReadOnlyList<ActivityEntryDto>>.Success(list.Select(x => x.ToDto()).ToList());
    }
}
