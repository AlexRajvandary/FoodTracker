using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class ListFoodEntriesQueryHandler : IRequestHandler<ListFoodEntriesQuery, Result<IReadOnlyList<FoodEntryDto>>>
{
    private readonly IFoodEntryRepository _entries;

    public ListFoodEntriesQueryHandler(IFoodEntryRepository entries)
    {
        _entries = entries;
    }

    public async Task<Result<IReadOnlyList<FoodEntryDto>>> Handle(ListFoodEntriesQuery request, CancellationToken cancellationToken)
    {
        var list = await _entries
            .ListForUserAsync(request.UserId, request.FromUtc, request.ToUtc, request.Date, cancellationToken)
            .ConfigureAwait(false);
        var dto = list.Select(x => x.ToDto()).ToList();
        return Result<IReadOnlyList<FoodEntryDto>>.Success(dto);
    }
}
