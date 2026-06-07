using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class PatchFoodItemCommand : IRequest<Result<FoodItemDto>>
{
    public Guid UserId { get; init; }
    public Guid FoodItemId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public double? CaloriesPer100g { get; init; }
    public double? ProteinsPer100g { get; init; }
    public double? FatsPer100g { get; init; }
    public double? CarbsPer100g { get; init; }
}