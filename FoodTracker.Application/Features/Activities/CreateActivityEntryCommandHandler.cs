using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Activities;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Activities;

public sealed class CreateActivityEntryCommandHandler : IRequestHandler<CreateActivityEntryCommand, Result<ActivityEntryDto>>
{
    private readonly IActivityTypeRepository _types;
    private readonly IActivityEntryRepository _entries;

    public CreateActivityEntryCommandHandler(IActivityTypeRepository types, IActivityEntryRepository entries)
    {
        _types = types;
        _entries = entries;
    }

    public async Task<Result<ActivityEntryDto>> Handle(CreateActivityEntryCommand request, CancellationToken cancellationToken)
    {
        var type = await _types.GetByIdAsync(request.ActivityTypeId, cancellationToken).ConfigureAwait(false);
        if (type is null)
        {
            return Result<ActivityEntryDto>.Failure(
                new Error(ActivityErrorCodes.ActivityTypeNotFound, "Тип активности не найден."));
        }

        if (!ActivityMath.TryResolveCaloriesBurned(
                type,
                request.DurationMinutes,
                request.RepetitionsCount,
                request.CaloriesBurned,
                out var calories,
                out var msg))
        {
            return Result<ActivityEntryDto>.Failure(
                new Error(ActivityErrorCodes.InvalidBurn, msg ?? "Некорректные данные для расчёта калорий."));
        }

        var entry = new ActivityEntry
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            ActivityTypeId = type.Id,
            ActivityName = type.Name,
            OccurredAtUtc = NormalizeUtc(request.OccurredAt),
            DurationMinutes = request.DurationMinutes,
            RepetitionsCount = request.RepetitionsCount,
            CaloriesBurned = calories,
            CreatedAtUtc = DateTime.UtcNow,
        };

        await _entries.AddAsync(entry, cancellationToken).ConfigureAwait(false);
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
