using FoodTracker.Application.Abstractions.Persistence;

namespace FoodTracker.Infrastructure.Persistence.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dataContext.SaveChangesAsync(cancellationToken);
    }
}