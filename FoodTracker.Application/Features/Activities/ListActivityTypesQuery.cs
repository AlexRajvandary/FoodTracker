using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class ListActivityTypesQuery : IRequest<Result<IReadOnlyList<ActivityTypeDto>>>
{
    public string? Q { get; init; }
}
