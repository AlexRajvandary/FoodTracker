using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class CreateActivityEntryCommand : IRequest<Result<ActivityEntryDto>>
{
    public required Guid UserId { get; init; }
    public Guid ActivityTypeId { get; init; }
    public DateTime OccurredAt { get; init; }
    public int? DurationMinutes { get; init; }
    public int? RepetitionsCount { get; init; }
    public decimal? CaloriesBurned { get; init; }
}
