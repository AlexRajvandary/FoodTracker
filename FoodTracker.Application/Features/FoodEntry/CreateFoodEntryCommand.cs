using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.FoodEntry
{
    public sealed class CreateFoodEntryCommand : IRequest<Result<FoodEntryDto>>
    {
        public DateTime ConsumedAtUtc { get; set; }
        public Guid FoodId { get; set; }
        public double? GramsConsumed { get; set; }
        public double? PortionsConsumed { get; set; }
        public Guid UserId { get; init; }
    }
}