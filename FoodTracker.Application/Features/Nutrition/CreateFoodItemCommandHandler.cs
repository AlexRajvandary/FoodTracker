using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using FoodTracker.Domain.Nutrition;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class CreateFoodItemCommandHandler : IRequestHandler<CreateFoodItemCommand, Result<FoodItemDto>>
{
    private readonly IFoodItemRepository _items;

    public CreateFoodItemCommandHandler(IFoodItemRepository items)
    {
        _items = items;
    }

    public async Task<Result<FoodItemDto>> Handle(CreateFoodItemCommand request, CancellationToken cancellationToken)
    {
        var item = new FoodItem
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim(),
            CaloriesPer100g = request.CaloriesPer100g,
            ProteinsPer100g = request.ProteinsPer100g ?? 0,
            FatsPer100g = request.FatsPer100g ?? 0,
            CarbsPer100g = request.CarbsPer100g ?? 0,
            PortionGrams = null,
            PortionHint = null,
            Category = null,
            OwnerUserId = request.UserId,
            CreatedAtUtc = DateTime.UtcNow,
        };

        await _items.AddAsync(item, cancellationToken).ConfigureAwait(false);
        return Result<FoodItemDto>.Success(item.ToDto());
    }
}
