using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class PatchFoodItemCommand : IRequest<Result<FoodItemDto>>
{
    public string? Barcode { get; init; }
    public string? Brand { get; init; }
    public double? CaloriesPer100g { get; init; }
    public IReadOnlyList<Guid>? CategoryIds { get; init; }
    public double? CarbsPer100g { get; init; }
    public string? Description { get; init; }
    public double? FatsPer100g { get; init; }
    public Guid FoodItemId { get; init; }
    public double? FiberPer100g { get; init; }
    public string? ImageUrl { get; init; }
    public string? Name { get; init; }
    public IReadOnlyList<string>? NewCategoryNames { get; init; }
    public double? ProteinsPer100g { get; init; }
    public double? SaltPer100g { get; init; }
    public double? SaturatedFatPer100g { get; init; }
    public double? ServingSizeGrams { get; init; }
    public double? SugarsPer100g { get; init; }
    public Guid UserId { get; init; }
}