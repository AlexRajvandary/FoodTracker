using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.Features.Nutrition;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class ListActivityTypesQueryHandler : IRequestHandler<ListActivityTypesQuery, Result<PagedList<ActivityTypeShortDto>>>
{
    private readonly IActivityTypeRepository _types;

    public ListActivityTypesQueryHandler(IActivityTypeRepository types)
    {
        _types = types;
    }

    public async Task<Result<PagedList<ActivityTypeShortDto>>> Handle(ListActivityTypesQuery request, CancellationToken cancellationToken)
    {
        var list = await _types.ListCatalogAsync(request.Query, request.Category, request.Page ?? 1, request.PageSize ?? 10, cancellationToken).ConfigureAwait(false);
        var dtos = list.Select(x => x.ToShortDto()).ToList();
        return Result<PagedList<ActivityTypeShortDto>>.Success(new PagedList<ActivityTypeShortDto>(dtos, request.Page ?? 1, request.PageSize ?? 10));
    }
}