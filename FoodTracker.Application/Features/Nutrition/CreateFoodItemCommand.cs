using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class CreateFoodItemCommand : IRequest<Result<FoodItemDto>>
{
    public required Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal CaloriesPer100g { get; init; }
    public decimal? ProteinsPer100g { get; init; }
    public decimal? FatsPer100g { get; init; }
    public decimal? CarbsPer100g { get; init; }
    public decimal? PortionGrams { get; set; }
    public string? PortionHint { get; set; }
    public string? Category { get; set; }
}