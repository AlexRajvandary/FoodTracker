using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class ListActivityTypesQueryHandler : IRequestHandler<ListActivityTypesQuery, Result<IReadOnlyList<ActivityTypeDto>>>
{
    private readonly IActivityTypeRepository _types;

    public ListActivityTypesQueryHandler(IActivityTypeRepository types)
    {
        _types = types;
    }

    public async Task<Result<IReadOnlyList<ActivityTypeDto>>> Handle(ListActivityTypesQuery request, CancellationToken cancellationToken)
    {
        var list = await _types.ListCatalogAsync(request.Query, request.Category, cancellationToken).ConfigureAwait(false);
        return Result<IReadOnlyList<ActivityTypeDto>>.Success(list.Select(x => x.ToDto()).ToList());
    }
}
