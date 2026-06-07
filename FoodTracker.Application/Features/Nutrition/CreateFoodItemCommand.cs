using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class CreateFoodItemCommand : IRequest<Result<FoodItemDto>>
{
    public required Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public double CaloriesPer100g { get; init; }
    public double? ProteinsPer100g { get; init; }
    public double? FatsPer100g { get; init; }
    public double? CarbsPer100g { get; init; }
    public double? PortionGrams { get; set; }
    public string? Category { get; set; }
}