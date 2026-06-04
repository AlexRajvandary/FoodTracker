using FoodTracker.Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodTracker.Infrastructure.Persistence.Configurations;

public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
{
    public void Configure(EntityTypeBuilder<ActivityType> entity)
    {
        entity.ToTable("activity_types");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        entity.Property(e => e.Description).HasMaxLength(2000);
        entity.Property(e => e.CaloriesPerHour).HasPrecision(18, 4);
        entity.Property(e => e.CaloriesPer100Reps).HasPrecision(18, 4);
        entity.HasIndex(e => e.Name);
    }
}
