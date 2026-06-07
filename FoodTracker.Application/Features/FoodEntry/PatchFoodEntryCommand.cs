using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.FoodEntry
{
    public sealed class PatchFoodEntryCommand : IRequest<Result<FoodEntryDto>>
    {
        public DateTime? ConsumedAtUtc { get; set; }
        public double? GramsConsumed { get; set; }
        public Guid FoodItemId { get; set; }
        public Guid UserId { get; set; }
    }
}