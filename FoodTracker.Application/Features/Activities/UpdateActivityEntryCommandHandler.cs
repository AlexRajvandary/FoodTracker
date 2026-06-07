using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class UpdateActivityEntryCommandHandler : IRequestHandler<UpdateActivityEntryCommand, Result<ActivityEntryDto>>
{
    private readonly IActivityTypeRepository _types;
    private readonly IActivityEntryRepository _entries;

    public UpdateActivityEntryCommandHandler(IActivityTypeRepository types, IActivityEntryRepository entries)
    {
        _types = types;
        _entries = entries;
    }

    public async Task<Result<ActivityEntryDto>> Handle(UpdateActivityEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = await _entries.GetOwnedAsync(request.UserId, request.EntryId, cancellationToken).ConfigureAwait(false);
        if (entry is null)
        {
            return Result<ActivityEntryDto>.Failure(
                new Error(ActivityErrorCodes.EntryNotFound, "Запись не найдена."));
        }

        if (request.OccurredAt is { } at)
        {
            entry.OccurredAtUtc = NormalizeUtc(at);
        }

        if (request.DurationMinutes.HasValue)
        {
            entry.DurationMinutes = request.DurationMinutes;
        }

        if (request.RepetitionsCount.HasValue)
        {
            entry.RepetitionsCount = request.RepetitionsCount;
        }

        if (request.CaloriesBurned.HasValue)
        {
            entry.CaloriesBurned = ActivityMath.RoundCalories(request.CaloriesBurned.Value);
        }
        else if (request.DurationMinutes.HasValue || request.RepetitionsCount.HasValue)
        {
            var type = await _types.GetByIdAsync(entry.ActivityTypeId, cancellationToken).ConfigureAwait(false);
            if (type is null)
            {
                return Result<ActivityEntryDto>.Failure(
                    new Error(ActivityErrorCodes.ActivityTypeNotFound, "Тип активности не найден."));
            }

            if (!ActivityMath.TryResolveCaloriesBurned(
                    type,
                    entry.DurationMinutes,
                    entry.RepetitionsCount,
                    caloriesBurnedOverride: null,
                    out var calories,
                    out var msg))
            {
                return Result<ActivityEntryDto>.Failure(
                    new Error(ActivityErrorCodes.InvalidBurn, msg ?? "Некорректные данные для пересчёта калорий."));
            }

            entry.CaloriesBurned = calories;
        }

        await _entries.UpdateAsync(entry, cancellationToken).ConfigureAwait(false);
        return Result<ActivityEntryDto>.Success(entry.ToDto());
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
