using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodTracker.Infrastructure.Persistence;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("FoodTracker__ConnectionStrings__PostgreSql")
            ?? "Host=localhost;Port=5432;Database=FoodTracker;Username=postgres;Password=postgres";

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new DataContext(options);
    }
}
