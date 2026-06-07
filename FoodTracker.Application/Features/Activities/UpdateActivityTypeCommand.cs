using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class UpdateActivityTypeCommand : IRequest<Result<ActivityTypeDto>>
{
    public Guid UserId { get; init; }
    public Guid ActivityTypeId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public double? CaloriesPerHour { get; init; }
    public double? CaloriesPer100Reps { get; init; }
    public string? Category { get; init; }
}