using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class ListActivityTypesQuery : IRequest<Result<PagedList<ActivityTypeDto>>>
{
    public string? Query { get; init; }
    public string? Category { get; init; }
    public int? Page { get; init; }
    public int? PageSize { get; init; }
}