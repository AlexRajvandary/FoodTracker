using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class CreateFoodItemCommand : IRequest<Result<FoodItemDto>>
{
    public string Barcode { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public double CaloriesPer100g { get; init; }
    public IReadOnlyList<Guid> CategoryIds { get; init; } = [];
    public IReadOnlyList<Guid> CountryIds { get; init; } = [];
    public IReadOnlyList<string> NewCategoryNames { get; init; } = [];
    public double? CarbsPer100g { get; init; }
    public string? Description { get; init; }
    public string? ExternalId { get; init; }
    public double? FatsPer100g { get; init; }
    public double? FiberPer100g { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; init; } = string.Empty;
    public double? ProteinsPer100g { get; init; }
    public double? SaltPer100g { get; set; }
    public double? SaturatedFatPer100g { get; set; }
    public double? ServingSizeGrams { get; set; }
    public double? SugarsPer100g { get; set; }
    public required Guid UserId { get; init; }
}