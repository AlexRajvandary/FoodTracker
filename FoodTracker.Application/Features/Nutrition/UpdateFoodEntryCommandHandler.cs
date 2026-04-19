using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class UpdateFoodEntryCommandHandler : IRequestHandler<UpdateFoodEntryCommand, Result<FoodEntryDto>>
{
    private readonly IFoodItemRepository _items;
    private readonly IFoodEntryRepository _entries;

    public UpdateFoodEntryCommandHandler(IFoodItemRepository items, IFoodEntryRepository entries)
    {
        _items = items;
        _entries = entries;
    }

    public async Task<Result<FoodEntryDto>> Handle(UpdateFoodEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = await _entries.GetOwnedAsync(request.UserId, request.EntryId, cancellationToken).ConfigureAwait(false);
        if (entry is null)
        {
            return Result<FoodEntryDto>.Failure(new Error(FoodErrorCodes.EntryNotFound, "Запись не найдена."));
        }

        if (request.GramsConsumed is { } grams && grams > 0)
        {
            var item = await _items.GetByIdAsync(entry.FoodItemId, cancellationToken).ConfigureAwait(false);
            if (item is null)
            {
                return Result<FoodEntryDto>.Failure(new Error(FoodErrorCodes.FoodItemNotFound, "Продукт не найден."));
            }

            var (cal, p, f, c) = NutritionMath.ComputeTotals(item, grams);
            entry.GramsConsumed = grams;
            entry.Calories = cal;
            entry.Proteins = p;
            entry.Fats = f;
            entry.Carbs = c;
        }

        if (request.PortionNote is not null)
        {
            entry.PortionNote = request.PortionNote;
        }

        if (request.ConsumedAt is { } at)
        {
            entry.ConsumedAtUtc = NormalizeUtc(at);
        }

        await _entries.UpdateAsync(entry, cancellationToken).ConfigureAwait(false);
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
