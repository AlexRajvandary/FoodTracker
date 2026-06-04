using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using FoodTracker.Domain.Nutrition;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class CreateFoodEntryCommandHandler : IRequestHandler<CreateFoodEntryCommand, Result<FoodEntryDto>>
{
    private readonly IFoodItemRepository _items;
    private readonly IFoodEntryRepository _entries;

    public CreateFoodEntryCommandHandler(IFoodItemRepository items, IFoodEntryRepository entries)
    {
        _items = items;
        _entries = entries;
    }

    public async Task<Result<FoodEntryDto>> Handle(CreateFoodEntryCommand request, CancellationToken cancellationToken)
    {
        var item = await _items.GetByIdAsync(request.FoodItemId, cancellationToken).ConfigureAwait(false);
        if (item is null)
        {
            return Result<FoodEntryDto>.Failure(new Error(FoodErrorCodes.FoodItemNotFound, "Продукт не найден."));
        }

        if (!NutritionMath.TryResolveGrams(item, request.GramsConsumed, request.PortionCount, out var grams, out var msg))
        {
            return Result<FoodEntryDto>.Failure(new Error(FoodErrorCodes.InvalidAmount, msg ?? "Некорректная порция."));
        }

        var (cal, p, f, c) = NutritionMath.ComputeTotals(item, grams);
        var consumedAt = NormalizeUtc(request.ConsumedAt);
        var entry = new FoodEntry
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            FoodItemId = item.Id,
            FoodName = item.Name,
            ConsumedAtUtc = consumedAt,
            GramsConsumed = grams,
            PortionNote = request.PortionNote,
            Calories = cal,
            Proteins = p,
            Fats = f,
            Carbs = c,
            CreatedAtUtc = DateTime.UtcNow,
        };

        await _entries.AddAsync(entry, cancellationToken).ConfigureAwait(false);
        return Result<FoodEntryDto>.Success(entry.ToDto());
    }

    private static DateTime NormalizeUtc(DateTime dt)
    {
        return dt.Kind switch
        {
            DateTimeKind.Utc => dt,
            DateTimeKind.Local => dt.ToUniversalTime(),
            _ => DateTime.SpecifyKind(dt, DateTimeKind.Utc),
        };
    }
}