using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class GetActivityTypeQuery : IRequest<Result<ActivityTypeDto>>
{
    public Guid UserId { get; init; }
    public Guid ActivityTypeId { get; init; }
}