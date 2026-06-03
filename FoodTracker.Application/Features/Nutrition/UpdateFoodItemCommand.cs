using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class UpdateFoodItemCommand : IRequest<Result<FoodItemDto>>
{
    public Guid UserId { get; init; }
    public Guid FoodItemId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal CaloriesPer100g { get; init; }
    public decimal ProteinsPer100g { get; init; }
    public decimal FatsPer100g { get; init; }
    public decimal CarbsPer100g { get; init; }
    public decimal PortionGrams { get; init; }
    public string? PortionHint { get; init; }
    public string? Category { get; init; }
}