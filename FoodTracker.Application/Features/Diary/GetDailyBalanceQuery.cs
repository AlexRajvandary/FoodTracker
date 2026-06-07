using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Diary
{
    public class GetDailyBalanceQuery : IRequest<Result<DailyBalanceDto>>
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
    }
}