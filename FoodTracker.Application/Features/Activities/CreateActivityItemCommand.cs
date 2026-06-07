using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class CreateActivityTypeCommand : IRequest<Result<ActivityTypeDto>>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public double? CaloriesPerHour { get; init; }
    public double? CaloriesPer100Reps { get; init; }
    public string? Category { get; init; }
}