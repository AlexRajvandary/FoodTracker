using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class DeleteActivityTypeCommand : IRequest<Result>
{
    public Guid UserId { get; init; }
    public Guid ActivityTypeId { get; init; }
}