using FoodTracker.Domain.Activities;
using FoodTracker.Domain.Auth;
using FoodTracker.Domain.Nutrition;
using FoodTracker.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Infrastructure.Persistence;

public sealed class DataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<UserAuthProvider> UserAuthProviders => Set<UserAuthProvider>();

    public DbSet<FoodItem> FoodItems => Set<FoodItem>();

    public DbSet<FoodEntry> FoodEntries => Set<FoodEntry>();

    public DbSet<ActivityType> ActivityTypes => Set<ActivityType>();

    public DbSet<ActivityEntry> ActivityEntries => Set<ActivityEntry>();

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
