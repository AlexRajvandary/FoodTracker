using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Common.Results;
using MediatR;

namespace FoodTracker.Application.Features.Diary
{
    public class GetPeriodBalanceQuery : IRequest<Result<PeriodBalanceDto>>
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}